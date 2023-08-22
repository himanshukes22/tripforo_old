<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="CustomerInfoFixDep.aspx.vb" Inherits="FlightInt_CustomerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link rel="stylesheet" href="../css/main2.css" type="text/css" />
    <link href="../CSS/customerinfo.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/chrome.js"></script>
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/CSS/newcss/SeatPopup.css")%>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/CSS/newcss/Seat.css")%>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/JSLINQ.js")%>"></script>
    <style type="text/css">
        .bld1 {
            font-weight: 600;
        }

        .hidepo {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
        function ddshow(id) {
            $("#ddhidebox").fadeIn(500);
            $("#closeclose").delay(1000).animate({ left: "81.7%" }, 500);
            $("#" + id + "Details").delay(500).animate({ left: "20%" }, 1000);
        }
        function ddhide() {
            $("#ddhidebox").fadeOut(500);
            $("#closeclose").animate({ left: "100%" }, 1000);
            $("#div_fareddDetails").animate({ left: "-100%" }, 1000);
            $("#ctl00_ContentPlaceHolder1_divtotFlightDetails").animate({ left: "-100%" }, 1000);
        }
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
            if (charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode == 32 || charCode == 08) {
                return true;
            }
            else {
                //alert("please enter Alphabets  only");
                return false;
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function showhide(layer_ref, a, b) {
            var c = layer_ref.id;
            c = c.replace(a, b);

            hza = document.getElementById(c);
            if (hza.style.display == 'none') {
                hza.style.display = 'block';
            }
            else {
                hza.style.display = 'none';
            }
        }
        function onkeyUpAdt() {
        }
    </script>

    <script language="javascript" type="text/javascript">
        function focusObj(obj) {
            if (obj.value == "First Name") obj.value = "";
        }

        function blurObj(obj) {
            if (obj.value == "") obj.value = "First Name";
        }

        function focusObj1(obj) {
            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObj1(obj) {
            if (obj.value == "") obj.value = "Last Name";
        }

        function focusObjM(obj) {
            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjM(obj) {
            if (obj.value == "") obj.value = "Middle Name";
        }

        function focusObjAir(obj) {
            if (obj.value == "Airline") obj.value = "";
        }

        function blurObjAir(obj) {
            if (obj.value == "") obj.value = "Airline";
        }

        function focusObjNumber(obj) {
            if (obj.value == "Number") obj.value = "";
        }

        function blurObjNumber(obj) {
            if (obj.value == "") obj.value = "Number";
        }
        //
        function focusObjNumber_Passport(obj) {
            if (obj.value == "Passport No") obj.value = "";
        }

        function blurObjNumber_Passport(obj) {
            if (obj.value == "") obj.value = "Passport No";
        }

        function focusObjNumber_Nationality(obj) {
            if (obj.value == "Nationality") obj.value = "";
        }

        function blurObjNumber_Nationality(obj) {
            if (obj.value == "") obj.value = "Nationality";
        }

        function focusObjNumber_Issuing(obj) {
            if (obj.value == "Issuing Country") obj.value = "";
        }

        function blurObjNumber_Issuing(obj) {
            if (obj.value == "") obj.value = "Issuing Country";
        }
        //
        function focusObjCFName(obj) {
            if (obj.value == "First Name") obj.value = "";
        }

        function blurObjCFName(obj) {
            if (obj.value == "") obj.value = "First Name";
        }
        function focusObjCMName(obj) {
            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjCMName(obj) {
            if (obj.value == "") obj.value = "Middle Name";
        }
        function focusObjCLName(obj) {
            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObjCLName(obj) {
            if (obj.value == "") obj.value = "Last Name";
        }

        function focusObjIFName(obj) {
            if (obj.value == "First Name") obj.value = "";
        }

        function blurObjIFName(obj) {
            if (obj.value == "") obj.value = "First Name";
        }

        function focusObjIMName(obj) {
            if (obj.value == "Middle Name") obj.value = "";
        }

        function blurObjIMName(obj) {
            if (obj.value == "") obj.value = "Middle Name";
        }
        function focusObjILName(obj) {
            if (obj.value == "Last Name") obj.value = "";
        }

        function blurObjILName(obj) {
            if (obj.value == "") obj.value = "Last Name";
        }

        function focusObjPF(obj) {
            if (obj.value == "First Name") obj.value = "";
        }

        function focusObjPL(obj) {
            if (obj.value == "Last Name") obj.value = "";
        }

        function focusObjPE(obj) {
            if (obj.value == "Email Id") obj.value = "";
        }

        function focusObjPM(obj) {
            if (obj.value == "Mobile No") obj.value = "";
        }

        function blurObjPF(obj) {
            if (obj.value == "") obj.value = "First Name";
        }

        function blurObjPL(obj) {
            if (obj.value == "") obj.value = "Last Name";
        }

        function blurObjPE(obj) {
            if (obj.value == "") obj.value = "Email Id";
        }

        function blurObjPM(obj) {
            if (obj.value == "") obj.value = "Mobile No";
        }
    </script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var btnclk = 0;
            var vcc = document.getElementById('ctl00_ContentPlaceHolder1_hdn_vc').value;
            var psprt = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hdn_Psprt').value);

            if (psprt > 0) {
                $('.psprt').show();
            }
            else { $('.psprt').hide(); }

            //if (vcc == "6E" | vcc == "G8" || vcc == "SG") {

            //    $('#adtreq').show();
            //}
            //else { $('#psprt').hide(); }

            $("#ctl00_ContentPlaceHolder1_book").click(function (e) {

                var elem = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("input");
                var elemSelect = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("select");


                for (var j = 0; j < elemSelect.length; j++) {

                    if (elemSelect[j].id.indexOf("ddl_ATitle") > 0) {

                        if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                            //alert('First Name can not be blank for Adult');
                            //$("#dialog").dialog();
                            jAlert('Please select the Title.', 'Alert');
                            elemSelect[j].focus();
                            return false;
                        }
                    }

                }
                var Adult = parseInt(0);
                var Child = parseInt(0);

                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtAFirstName") > 0) {
                        Adult++;
                        if (elem[i].value == "" || elem[i].value == "First Name") {
                            jAlert('First Name can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                            //alert('First Name can not be blank for Adult');
                            //$("#dialog").dialog();
                            jAlert('First Name required atleast one characters for Adult.', 'Alert');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtALastName") > 0) {

                        if (elem[i].value == "" || elem[i].value == "Last Name") {
                            jAlert('Last Name can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                            //alert('Last Name can not be blank for Adult');
                            jAlert('Last Name required atleast one characters.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_AdtDOB") > 0) {
                        jAlert('Age can not be blank for Adult', 'Alert');
                        elem[i].focus();
                        return false;
                    }
                    // for Adlut Passport No/ Issue Country / Nationality / Gender / Expiring Date
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_Passport_Adl") > 0) {
                        Adult++;

                        if ((elem[i].value == "" || elem[i].value == "Passport No") && psprt > 0) {
                            jAlert('Passport No can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }

                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Adl") > 0) {
                        Adult++;
                        if ((elem[i].value == "" || elem[i].value == "Nationality") && psprt > 0) {
                            jAlert('Nationality can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }

                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Adl") > 0) {
                        Adult++;
                        if ((elem[i].value == "" || elem[i].value == "Issuing Country") && psprt > 0) {
                            jAlert('Issuing Country can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }

                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Adl") > 0) {
                        Adult++;
                        if ((elem[i].value == "" || elem[i].value == "Ex Date") && psprt > 0) {
                            jAlert('Passport Expring Date can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                }
                //Check if Repeater Child Exsists
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Child_ctl00_txtCFirstName')) {

                    for (var j = 0; j < elemSelect.length; j++) {

                        if (elemSelect[j].id.indexOf("ddl_CTitle") > 0) {

                            if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                jAlert('Please select the Title.', 'Alert');
                                elemSelect[j].focus();
                                return false;
                            }
                        }

                    }
                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtCFirstName") > 0) {
                            Child++;
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                jAlert('First Name can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                jAlert('First Name required atleast one characters for Child.', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtCLastName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "Last Name") {
                                jAlert('Last Name can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                                jAlert('Last Name required atleast one characters for Child.', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_chDOB") > 0) {
                            jAlert('Age can not be blank for Child', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        // for child Passport No/ Issue Country / Nationality / Gender / Expiring Date
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Passport_Chd") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Passport No") && psprt > 0) {
                                jAlert('Passport No can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Chd") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Nationality") && psprt > 0) {
                                jAlert('Nationality can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Chd") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Issuing Country") && psprt > 0) {
                                jAlert('Issuing Country can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Chd") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Ex Date") && psprt > 0) {
                                jAlert('Passport Expring Date can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                    }
                }
                //Check if Repeater Infant Exsists
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName')) {

                    for (var j = 0; j < elemSelect.length; j++) {

                        if (elemSelect[j].id.indexOf("ddl_ITitle") > 0) {

                            if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                jAlert('Please select the Title.', 'Alert');
                                elemSelect[j].focus();
                                return false;
                            }
                        }

                    }
                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtIFirstName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                jAlert('First Name can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                jAlert('First Name required atleast one characters for infant.', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtILastName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "Last Name") {
                                jAlert('Last Name can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                                jAlert('Last Name required atleast one characters for infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_InfantDOB") > 0) {
                            jAlert('Age can not be blank for Infant', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        // for Infant Passport No/ Issue Country / Nationality / Gender / Expiring Date
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Passport_Inf") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Passport No") && psprt > 0) {
                                jAlert('Passport No can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Inf") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Nationality") && psprt > 0) {
                                jAlert('Nationality can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Inf") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Issuing Country") && psprt > 0) {
                                jAlert('Issuing Country can not be blank for Infand', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Inf") > 0) {
                            Adult++;
                            if ((elem[i].value == "" || elem[i].value == "Ex Date") && psprt > 0) {
                                jAlert('Passport Expring Date can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                    }
                }

                if ($("#ctl00_ContentPlaceHolder1_DropDownListProject option:selected").val() == "Select") {
                    jAlert("Please Select Project Id", 'Alert');
                    $("#ctl00_ContentPlaceHolder1_DropDownListProject").focus();
                    return false;
                }


                if ($("#ctl00_ContentPlaceHolder1_DropDownListBookedBy option:selected").val() == "Select") {
                    jAlert("Please Select Booked By", 'Alert');
                    $("#ctl00_ContentPlaceHolder1_DropDownListBookedBy").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").value == "") {
                    jAlert('Please Enter Primary Guest First Name', 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").value == "") {
                    jAlert('Please Enter Primary Guest Last Name', 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value == "") {
                    jAlert('Please Enter EmailID', 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                    return false;
                }

                var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value;
                var matchArray = emailid.match(emailPat);
                if (matchArray == null) {
                    jAlert("Your email address seems incorrect. Please try again.", 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "Mobile No") {
                    jAlert('Please Enter Mobile No', 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                    return false;
                }
                else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value != "" && $.trim(document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value).length < 10 == "Mobile No") {
                    jAlert('Please Enter atleast 10 digit in Mobile', 'Alert');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                    return false;
                }

                ///////
                if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value != "") {
                    if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value.length != 15) {
                        jAlert('Please Enter valid GST NO', 'Alert');
                        document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").focus();
                        return false;
                    }
                    else {
                        if (gStValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value)) {

                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value == "") {
                                jAlert('Please Enter Gst Company Name', 'Alert');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                                return false;
                            }
                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value.length > 35) {
                                //jAlert('Please Enter Gst Company Name', 'Alert');
                                alert('Company Name length should not more than 35 characters.\nPlease enter valid length');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                                return false;
                            }

                            var iCharsadd = "!`@#$%^&*()+=-[]\\\';,.{}|\":<>?~_/";
                            var data = document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value;
                            for (var i = 0; i < data.length; i++) {
                                if (iCharsadd.indexOf(data.charAt(i)) != -1) {
                                    alert("Only alpha numeric is allowed in gst company name. \nPlease enter valid company name.");
                                    //document.getElementById("txtCallSign").value = "";
                                    return false;
                                }
                            }

                            //var GstCmpName = document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value;
                            //if (alphanumeric(GstCmpName) == false) {
                            //    alert("Only alpha numeric is allowed in gst company name. \nPlease enter valid company name.");
                            //    document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                            //    return false;
                            //}



                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value == "") {
                                jAlert('Please Enter Gst Company Address', 'Alert');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                                return false;
                            }

                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value.length > 35) {
                                alert('Address length should not more than 35 characters.\nPlease enter valid length');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                                return false;
                            }
                            var AddressFormate1 = AddressValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value);
                            if (AddressFormate1 != "") {
                                alert(AddressFormate1);
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                                return false;
                            }

                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value == "") {
                                jAlert('Please Enter Gst Pin Code', 'Alert');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").focus();
                                return false;
                            }
                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value.length > 7) {
                                alert('Please enter valid Pin Code');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").focus();
                                return false;
                            }

                            var PinCode = NumericValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value);
                            if (PinCode != "") {
                                alert(PinCode);
                                return false;
                            }



                            if ($("#ctl00_ContentPlaceHolder1_ddlStateGst option:selected").val() == "select") {
                                jAlert("Please Select GST State", 'Alert');
                                $("#ctl00_ContentPlaceHolder1_ddlStateGst").focus();
                                return false;
                            }
                            if ($("#ctl00_ContentPlaceHolder1_ddlCityGst option:selected").val() == "select") {
                                jAlert("Please Select GST City", 'Alert');
                                $("#ctl00_ContentPlaceHolder1_ddlCityGst").focus();
                                return false;
                            }


                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value == "") {
                                jAlert('Please Enter Gst Phone No', 'Alert');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").focus();
                                return false;
                            }
                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value.length > 10) {
                                alert('Contact length should not more than 10.\nPlease enter valid length');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").focus();
                                return false;
                            }
                            var Contact = NumericValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value);
                            if (Contact != "") {
                                alert(Contact);
                                return false;
                            }

                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value == "") {
                                jAlert('Please Enter Gst Email ID', 'Alert');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                                return false;
                            }
                            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value.length > 35) {
                                alert('Email Id length should not more than 35.\nPlease enter valid length');
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                                return false;
                            }

                            var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                            var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value;
                            var matchArray = emailid.match(emailPat);
                            if (matchArray == null) {
                                alert("Email address seems incorrect. Please try again.");
                                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                                return false;
                            }
                        }
                        else {
                            jAlert('Enter valid GST No.', 'Alert');
                            document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").focus();
                            return false;
                        }
                    }
                }

                //////


                // For Adult checking Nationality/IssuingCountry
                $('.Hdn_Nationality_Adl').each(function () {

                    if ($(this).val() == "") {
                        alert('Nationality is invalid for adult, please select the valid nationality!!');
                        $(this).focus();
                        btnclk = 2;
                        return false;
                    }
                });

                if (btnclk != 2) {
                    $('.Hdn_IssuingCountry_Adl').each(function () {

                        if ($(this).val() == "") {
                            alert('IssuingCountry is invalid for adult, please select the valid IssuingCountry!!');
                            $(this).focus();
                            btnclk = 2;
                            return false;
                        }
                    });
                }

                // For Child checking Nationality/IssuingCountry
                if (btnclk != 2) {
                    $('.Hdn_Nationality_Chd').each(function () {

                        if ($(this).val() == "") {
                            alert('Nationality is invalid for child, please select the valid nationality!!');
                            $(this).focus();
                            btnclk = 2;
                            return false;
                        }
                    });
                }
                if (btnclk != 2) {
                    $('.Hdn_IssuingCountry_Chd').each(function () {

                        if ($(this).val() == "") {
                            alert('IssuingCountry is invalid for child, please select the valid IssuingCountry!!');
                            $(this).focus();
                            btnclk = 2;
                            return false;
                        }
                    });
                }

                // For infant checking Nationality/IssuingCountry
                if (btnclk != 2) {
                    $('.Hdn_Nationality_Inf').each(function () {

                        if ($(this).val() == "") {
                            alert('Nationality is invalid for Infant, please select the valid nationality!!');
                            $(this).focus();
                            btnclk = 2;
                            return false;
                        }
                    });
                }

                if (btnclk != 2) {
                    $('.Hdn_IssuingCountry_Inf').each(function () {

                        if ($(this).val() == "") {
                            alert('IssuingCountry is invalid for Infant, please select the valid IssuingCountry!!');
                            $(this).focus();
                            btnclk = 2;
                            return false;
                        }
                    });
                }
                /////////////////////////////
                MealBagPriceAdd(Adult, Child);

                if (btnclk == 0) {
                    e.preventDefault();

                    $('#ctl00_ContentPlaceHolder1_book').unbind('click').click();;
                    document.getElementById("div_Submit").style.display = "none";
                    document.getElementById("div_Progress").style.display = "block";

                    btnclk = 1;


                    //jConfirm('Are you sure!', 'Confirmation', function (r) {



                    //    if (r) {
                    //        $('#ctl00_ContentPlaceHolder1_book').unbind('click').click();;
                    //        document.getElementById("div_Submit").style.display = "none";
                    //        document.getElementById("div_Progress").style.display = "block";

                    //        btnclk = 1;
                    //    }
                    //    else {
                    //        //$('#ctl00_ContentPlaceHolder1_book').bind('click');
                    //        btnclk == 0;
                    //    }

                    //});
                }
                else {
                    btnclk = 0;
                    return false;
                }

                //if (confirm("Are you sure!")) {
                //    document.getElementById("div_Submit").style.display = "none";
                //    document.getElementById("div_Progress").style.display = "block";
                //    return true;
                //}
                //else {
                //    return false;
                //}
            });
        });

        function MealBagPriceAdd(Adult, Child) {

            ///// MEAL
            var A_ML_O = ""; var A_ML_I = "";
            var A_ML_O_txt = ""; A_ML_I_txt = "";
            var C_ML_O = ""; var C_ML_I = "";
            var C_ML_O_txt = ""; C_ML_I_txt = "";
            var C_ML_O = ""; var C_ML_I = "";
            var C_ML_O_txt = ""; C_ML_I_txt = "";
            var MLAdult_f_O = parseFloat(0); MLChild_f_O = parseFloat(0);
            var MLAdult_f_I = parseFloat(0); MLChild_f_I = parseFloat(0);
            var A_ML_O_Seg2 = "", A_ML_I_Seg2 = "", A_ML_O_txt_Seg2 = "", A_ML_I_txt_Seg2 = "";
            var C_ML_O_Seg2 = "", C_ML_I_Seg2 = "", C_ML_O_txt_Seg2 = "", C_ML_I_txt_Seg2 = "";
            var MLAdult_f_O_Seg2 = parseFloat(0), MLChild_f_O_Seg2 = parseFloat(0), MLAdult_f_I_Seg2 = parseFloat(0), MLChild_f_I_Seg2 = parseFloat(0);

            //BAGAGE

            var A_BG_O = ""; var A_BG_I = "";
            var A_BG_O_txt = ""; A_BG_I_txt = "";
            var C_BG_O = "", C_BG_I = "";
            var C_BG_O_txt = "", C_BG_I_txt = "";
            var BGAdult_f_O = parseFloat(0); BGChild_f_O = parseFloat(0);
            var BGAdult_f_I = parseFloat(0); BGChild_f_I = parseFloat(0);
            //////////////////////////////Start AirAsia Meal Baggage for second Segment
            var A_BG_O_Seg2 = "", A_BG_I_Seg2 = "", A_BG_O_txt_Seg2 = "", A_BG_I_txt_Seg2 = "";
            var C_BG_O_Seg2 = "", C_BG_I_Seg2 = "", C_BG_O_txt_Seg2 = "", C_BG_I_txt_Seg2 = "";
            var BGAdult_f_O_Seg2 = parseFloat(0), BGChild_f_O_Seg2 = parseFloat(0), BGAdult_f_I_Seg2 = parseFloat(0), BGChild_f_I_Seg2 = parseFloat(0);
            //////////////////////////////End AirAsia Meal Baggage for second Segment

            var Total = parseFloat(0);
            var elemddl = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("select");
            //document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_div_ADT')
            //ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_div_ADT_Ib

            //int  Adt =0;
            for (var i = 0; i < elemddl.length; i++) {
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_Meal_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_ML_O = A_ML_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_ML_O_txt = A_ML_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_Meal_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_ML_I = A_ML_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_ML_I_txt = A_ML_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_EB_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_BG_O = A_BG_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_BG_O_txt = A_BG_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_EB_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_BG_I = A_BG_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_BG_I_txt = A_BG_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                //////////////////////////////Start AirAsia Meal Baggage for second Segment
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_Meal_Ob_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_ML_O_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_ML_O_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_Meal_Ib_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_ML_I_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_ML_I_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_EB_Ob_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_BG_O_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_BG_O_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_EB_Ib_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_BG_I_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_BG_I_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                //////End AirAsia Meal Baggage for second Segment
                //CHILD
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_Meal_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_ML_O = C_ML_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_ML_O_txt = C_ML_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_Meal_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_ML_I = C_ML_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_ML_I_txt = C_ML_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_EB_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_BG_O = C_BG_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_BG_O_txt = C_BG_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_EB_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_BG_I = C_BG_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_BG_I_txt = C_BG_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                //////////////////////////////Start AirAsia Meal Baggage for second Segment for child
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_Meal_Ob_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_ML_O_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_ML_O_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_Meal_Ib_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_ML_I_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_ML_I_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_EB_Ob_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_BG_O_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_BG_O_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_EB_Ib_Seg2") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_BG_I_Seg2 += elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_BG_I_txt_Seg2 += elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                //////End AirAsia Meal Baggage for second Segment for child
            } //for end
            var Result = "";
            //Calcualte Fares of Meal and Baggage
            //if (A_ML_O != "" || C_ML_O != "" || A_ML_I != "" || C_ML_I != "" || A_BG_O != "" || C_BG_O != "" || A_BG_I != "" || C_BG_I != "") {
            //OUTBOUND 
            //if (A_ML_O != "" || C_ML_O != "" || A_BG_O != "" || C_BG_O != "") {
            if (A_ML_O != "") { MLAdult_f_O = CalcFare(A_ML_O_txt, MLAdult_f_O); }
            if (C_ML_O != "") { MLChild_f_O = CalcFare(C_ML_O_txt, MLChild_f_O); }
            if (A_BG_O != "") { BGAdult_f_O = CalcFare(A_BG_O_txt, BGAdult_f_O); }
            if (C_BG_O != "") { BGChild_f_O = CalcFare(C_BG_O_txt, BGChild_f_O); }

            //////Start AirAsia Beal Baggage for second Segment
            if (A_ML_O_Seg2 != "") { MLAdult_f_O_Seg2 = CalcFare(A_ML_O_txt_Seg2, MLAdult_f_O_Seg2); }
            if (C_ML_O_Seg2 != "") { MLChild_f_O_Seg2 = CalcFare(C_ML_O_txt_Seg2, MLChild_f_O_Seg2); }
            if (A_BG_O_Seg2 != "") { BGAdult_f_O_Seg2 = CalcFare(A_BG_O_txt_Seg2, BGAdult_f_O_Seg2); }
            if (C_BG_O_Seg2 != "") { BGChild_f_O_Seg2 = CalcFare(C_BG_O_txt_Seg2, BGChild_f_O_Seg2); }
            //////End AirAsia Beal Baggage for second Segment
            $("#<%= lbl_OB_TOT.ClientID %>").val(parseFloat(MLAdult_f_O + MLChild_f_O + BGAdult_f_O + BGChild_f_O + MLAdult_f_O_Seg2 + MLChild_f_O_Seg2 + BGAdult_f_O_Seg2 + BGChild_f_O_Seg2));

            // $("#<%= lbl_OB_TOT.ClientID %>").val(parseFloat(MLAdult_f_O + MLChild_f_O + BGAdult_f_O + BGChild_f_O));
            //}
            //INBOUND 
            //if (A_ML_I != "" || C_ML_I != "" || A_BG_I != "" || C_BG_I != "") {
            //UPdate Fare Display divFareDtlsR
            if (A_ML_I != "") { MLAdult_f_I = CalcFare(A_ML_I_txt, MLAdult_f_I); }
            if (C_ML_I != "") { MLChild_f_I = CalcFare(C_ML_I_txt, MLChild_f_I); }
            if (A_BG_I != "") { BGAdult_f_I = CalcFare(A_BG_I_txt, BGAdult_f_I); }
            if (C_BG_I != "") { BGChild_f_I = CalcFare(C_BG_I_txt, BGChild_f_I); }
            //////Start AirAsia Meal Baggage for second Segment
            if (A_ML_I_Seg2 != "") { MLAdult_f_I_Seg2 = CalcFare(A_ML_I_txt_Seg2, MLAdult_f_I_Seg2); }
            if (C_ML_I_Seg2 != "") { MLChild_f_I_Seg2 = CalcFare(C_ML_I_txt_Seg2, MLChild_f_I_Seg2); }
            if (A_BG_I_Seg2 != "") { BGAdult_f_I_Seg2 = CalcFare(A_BG_I_txt_Seg2, BGAdult_f_I_Seg2); }
            if (C_BG_I_Seg2 != "") { BGChild_f_I_Seg2 = CalcFare(C_BG_I_txt_Seg2, BGChild_f_I_Seg2); }
            //////End AirAsia Meal Baggage for second Segment
            Total = parseFloat(MLAdult_f_O + BGAdult_f_O + MLChild_f_O + BGChild_f_O + MLAdult_f_O_Seg2 + BGAdult_f_O_Seg2 + MLChild_f_O_Seg2 + BGChild_f_O_Seg2) + parseFloat(MLAdult_f_I + BGAdult_f_I + MLChild_f_I + BGChild_f_I + MLAdult_f_I_Seg2 + BGAdult_f_I_Seg2 + MLChild_f_I_Seg2 + BGChild_f_I_Seg2);

            //Result= Result+ Create_MealBag_Table(MLAdult_f_I,BGAdult_f_I,MLChild_f_I,BGChild_f_I,"IB");
            //Total = parseFloat(MLAdult_f_O + BGAdult_f_O + MLChild_f_O + BGChild_f_O) + parseFloat(MLAdult_f_I + BGAdult_f_I + MLChild_f_I + BGChild_f_I);
            //Result = Result+ "<div align='center'>" + Total+ "</div>"

            //Insert Meal Bagage Rows inside table - Normal Round Trip
            if (document.getElementById("trtotfareR") != null) {
                //                        if (parseFloat(MLAdult_f_I + MLChild_f_I) > 0) {
                //                            Insert_Row("IB_FT", "mpr", parseInt(document.getElementById("trtotfareR").rowIndex) - 1, "MealFare", parseFloat(MLAdult_f_I + MLChild_f_I));
                //                        }

                //                        if (parseFloat(BGAdult_f_I + BGChild_f_I) > 0) {
                //                            Insert_Row("IB_FT", "bpr", parseInt(document.getElementById("trtotfareR").rowIndex) - 1, "BagageFare", parseFloat(BGAdult_f_I + BGChild_f_I));
                //                        }

                //                        //Total Fare Calculation
                //                        var Tot_f_I = parseFloat(0); Net_f_I = parseFloat(0);
                //                        if ($("#<%= TOT_IB_Fare.ClientID %>").val() == "") {
                //                            Tot_f_I = parseFloat(document.getElementById("trtotfareR").getElementsByTagName("td")[1].innerText);
                //                            $("#<%= TOT_IB_Fare.ClientID %>").val(Tot_f_I);
                //                        }
                //                        else {
                //                            document.getElementById("trtotfareR").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat($("#<%= TOT_IB_Fare.ClientID %>").val()));
                //                        }
                //                        //Net Fare
                //                        if ($("#<%= NET_IB_Fare.ClientID %>").val() == "") {
                //                            Net_f_I = parseFloat(document.getElementById("trnetfareR").getElementsByTagName("td")[1].innerText);
                //                            $("#<%= NET_IB_Fare.ClientID %>").val(Net_f_I);
                //                        }
                //                        else {
                //                            document.getElementById("trnetfareR").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat($("#<%= NET_IB_Fare.ClientID %>").val()));
                //                        }

                //                        document.getElementById("trtotfareR").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat(document.getElementById("trtotfareR").getElementsByTagName("td")[1].innerText) + parseFloat(MLAdult_f_I + MLChild_f_I + BGAdult_f_I + BGChild_f_I));
                //                        document.getElementById("trnetfareR").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat(document.getElementById("trnetfareR").getElementsByTagName("td")[1].innerText) + parseFloat(MLAdult_f_I + MLChild_f_I + BGAdult_f_I + BGChild_f_I));
                $("#<%= lbl_IB_TOT.ClientID %>").val(parseFloat(MLAdult_f_I + BGAdult_f_I + MLChild_f_I + BGChild_f_I));
            }
            else //RTF Case - Relace with New Value of OutBound + Inbound 
            {
                //                        if (parseFloat(MLAdult_f_I + MLChild_f_I) > 0) {
                //                            Insert_Row("OB_FT", "mp", parseInt(document.getElementById("trtotfare").rowIndex) - 1, "MealFare", parseFloat(MLAdult_f_O + MLChild_f_O + MLAdult_f_I + MLChild_f_I));
                //                        }
                //                        if (parseFloat(BGAdult_f_I + BGChild_f_I) > 0) {
                //                            Insert_Row("OB_FT", "bp", parseInt(document.getElementById("trtotfare").rowIndex) - 1, "BagageFare", parseFloat(BGAdult_f_O + BGChild_f_O + BGAdult_f_I + BGChild_f_I));
                //                        }

                var Tot_f_O = parseFloat(0); Net_f_O = parseFloat(0);
                var Tot_f_I = parseFloat(0); Net_f_I = parseFloat(0);
                //Total Fare
                //                        if ($("#<%= TOT_OB_Fare.ClientID %>").val() == "") {
                //                            Tot_f_O = parseFloat(document.getElementById("trtotfare").getElementsByTagName("td")[1].innerText);
                //                            $("#<%= TOT_OB_Fare.ClientID %>").val(Tot_f_O);
                //                    }
                //                    else {
                //                        document.getElementById("trtotfare").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat($("#<%= TOT_OB_Fare.ClientID %>").val()));
                //                        }
                //                        //Net Fare
                //                        if ($("#<%= NET_OB_Fare.ClientID %>").val() == "") {
                //                            Net_f_O = parseFloat(document.getElementById("trnetfare").getElementsByTagName("td")[1].innerText);
                //                            $("#<%= NET_OB_Fare.ClientID %>").val(Net_f_O);
                //                        }
                //                        else {
                //                            document.getElementById("trnetfare").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat($("#<%= NET_OB_Fare.ClientID %>").val()));
                //                    }

                //                    document.getElementById("trtotfare").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat(document.getElementById("trtotfare").getElementsByTagName("td")[1].innerText) + parseFloat(Total));
                //                    document.getElementById("trnetfare").getElementsByTagName("td")[1].innerText = parseFloat(parseFloat(document.getElementById("trnetfare").getElementsByTagName("td")[1].innerText) + parseFloat(Total));
                $("#<%= lbl_OB_TOT.ClientID %>").val(parseFloat(Total));
            }

            //            }
            //                //INBOUND end

            //            alert("Total Fare after Meal Selection has Been Changed Please Refer Price Itineary");
            //        }//IF end- Fare and Meal Calculation End

            $("#<%= lbl_A_MB_OB.ClientID %>").val(A_ML_O + "#" + A_BG_O);
            $("#<%= lbl_A_MB_IB.ClientID %>").val(A_ML_I + "#" + A_BG_I);
            $("#<%= lbl_C_MB_OB.ClientID %>").val(C_ML_O + "#" + C_BG_O);
            $("#<%= lbl_C_MB_IB.ClientID %>").val(C_ML_I + "#" + C_BG_I);

            $("#<%= lbl_A_MB_OB_Seg2.ClientID %>").val(A_ML_O_Seg2 + "#" + A_BG_O_Seg2);
            $("#<%= lbl_A_MB_IB_Seg2.ClientID %>").val(A_ML_I_Seg2 + "#" + A_BG_I_Seg2);
            $("#<%= lbl_C_MB_OB_Seg2.ClientID %>").val(C_ML_O_Seg2 + "#" + C_BG_O_Seg2);
            $("#<%= lbl_C_MB_IB_Seg2.ClientID %>").val(C_ML_I_Seg2 + "#" + C_BG_I_Seg2);

            return false;
        }
        function CalcFare(text, fare) {
            var TotFare = parseFloat(0);
            var Coll = text.split(",")
            for (var j = 0; j < Coll.length - 1; j++) {
                if (Coll[j] == "") {
                }
                else {
                    TotFare = parseFloat(TotFare) + parseFloat(Coll[j].split("INR")[1]);
                }
            }
            return TotFare;
        }

        function Insert_Row(Tablename, rowid, Pos, CellName, CellVale) {
            // Find a <table> element with id="myTable":
            if (document.getElementById(rowid) == null) {
                var table = document.getElementById(Tablename);
                // Create an empty <tr> element and add it to the 1st position of the table:
                var row = table.insertRow(Pos);
                row.id = rowid;
                // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);

                // Add some text to the new cells:
                cell1.innerHTML = CellName;
                cell2.innerHTML = CellVale;
            }
            else {//Replace Value
                document.getElementById(rowid).getElementsByTagName("td")[1].innerText = CellVale
            }
        }

    </script>
    <%--<script type="text/javascript">
        function National_Country() {
            // For Adult checking Nationality/IssuingCountry
            var Hdn_Nationality_Adl = $('.Hdn_Nationality_Adl').each(function () {

                if (Hdn_Nationality_Adl.val == "") {
                    alert('Nationality is invalid for adult, please select the valid nationality!!');
                    Hdn_Nationality_Adl.focus();
                    return false;
                }
            })
            var Hdn_IssuingCountry_Adl = $('.Hdn_IssuingCountry_Adl').each(function () {

                if (Hdn_IssuingCountry_Adl.val == "") {
                    alert('IssuingCountry is invalid for adult, please select the valid IssuingCountry!!');
                    Hdn_IssuingCountry_Adl.focus();
                    return false;
                }
            })

            // For Child checking Nationality/IssuingCountry
            var Hdn_Nationality_Chd = $('.Hdn_Nationality_Chd').each(function () {

                if (Hdn_Nationality_Chd.val == "") {
                    alert('Nationality is invalid for child, please select the valid nationality!!');
                    Hdn_Nationality_Chd.focus();
                    return false;
                }
            })
            var Hdn_IssuingCountry_Chd = $('.Hdn_IssuingCountry_Chd').each(function () {

                if (Hdn_IssuingCountry_Chd.val == "") {
                    alert('IssuingCountry is invalid for child, please select the valid IssuingCountry!!');
                    Hdn_IssuingCountry_Chd.focus();
                    return false;
                }
            })

            // For infant checking Nationality/IssuingCountry
            var Hdn_Nationality_Inf = $('.Hdn_Nationality_Inf').each(function () {

                if (Hdn_Nationality_Inf.val == "") {
                    alert('Nationality is invalid for Infant, please select the valid nationality!!');
                    Hdn_Nationality_Inf.focus();
                    return false;
                }
            })
            var Hdn_IssuingCountry_Inf = $('.Hdn_IssuingCountry_Inf').each(function () {

                if (Hdn_IssuingCountry_Inf.val == "") {
                    alert('IssuingCountry is invalid for Infant, please select the valid IssuingCountry!!');
                    Hdn_IssuingCountry_Inf.focus();
                    return false;
                }
            })
        }
    </script>--%>
    <link type="text/css" href="../styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" />

    <div style="background-color: #eee;">
        <div style="display: none">
            <asp:Label ID="lbl_adult" runat="server"></asp:Label>
            <asp:Label ID="lbl_child" runat="server"></asp:Label>
            <asp:Label ID="lbl_infant" runat="server"></asp:Label>
            <asp:HiddenField ID="lbl_A_MB_OB" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_A_MB_IB" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_C_MB_OB" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_C_MB_IB" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_OB_TOT" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_IB_TOT" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="TOT_OB_Fare" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="NET_OB_Fare" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="TOT_IB_Fare" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="NET_IB_Fare" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_A_MB_OB_Seg2" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_A_MB_IB_Seg2" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_C_MB_OB_Seg2" EnableViewState="true" runat="server" />
            <asp:HiddenField ID="lbl_C_MB_IB_Seg2" EnableViewState="true" runat="server" />

        </div>
        <div class="large-10 medium-10 small-12 large-push-1 medium-push-1" id="abcd">
            <div class="row">

                <div style="width: 30%; float: right; padding: 10px 10px 10px 10px;">
                    <div id="divtotalpax" runat="server">
                    </div>

                    <div class="row " style='padding: 0px 10px 10px 10px;'>

                        <div id="div_fareddDetails" class="bgs">

                            <div id="div_fare" runat="server" class="w101" style="padding-top: 0px 10px 10px 10px;">
                            </div>
                            <div id="div_fareR" runat="server" class="w101" style="padding-top: 10px;">
                            </div>
                            <div class="clear1"></div>
                        </div>
                    </div>
                </div>

            </div>
            <div style="width: 70%; float: left; margin-top: 20px;">
                <div class="large-12 medium-1 small-1 headbgs">
                    <span id="pttextADT"><i class="fa fa-plane" aria-hidden="true"></i>Flight Details</span>
                    <a href="#abc" style="float: right;"><i class="fa fa-angle-double-down" aria-hidden="true"></i>Passanger Details</a>
                </div>
                <div id="ddhidebox" class="bgs" style="border: 1px solid #ccc;">
                    <div id="divtotFlightDetails" runat="server">
                    </div>

                </div>

                <div id="divFltDtls1" runat="server" class="large-12 medium-12 small-12 columns" style="padding-top: 10px; display: none;">
                </div>
                <div id="abc">
                    <div id="tbl_Pax" runat="server" style="margin-top: 10px; padding: 0px;" class="w100 bgs">
                        <div id="td_Adult" runat="server">
                            <asp:Repeater ID="Repeater_Adult" runat="server" OnItemCreated="Repeater_Adult_ItemCreated">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="large-10 medium-10 small-12 headbgs">
                                            <i class="fa fa-user-o" aria-hidden="true"></i>
                                            <asp:Label ID="pttextADT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>
                                            <a href="#abcd" style="float: right;"><i class="fa fa-angle-double-up" aria-hidden="true"></i>Flight Details</a>
                                        </div>
                                        <%-- <div class="large-2 medium-2 small-12 columns bld passenger">OutBound:</div>--%>
                                        <div class="clear1"></div>
                                        <div class="large-12 medium-12 small-12">
                                            <div class="large-1 medium-1 small-3 columns">
                                                <asp:DropDownList ID="ddl_ATitle" runat="server">
                                                    <asp:ListItem Value="" Selected="True">Title</asp:ListItem>
                                                    <asp:ListItem Value="Mr">Mr.</asp:ListItem>                                                    
                                                    <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                    <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                    <%-- <asp:ListItem Value="Miss">Dr.</asp:ListItem>
                                        <asp:ListItem Value="Miss">Prof.</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>



                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txtAFirstName" runat="server" value="First Name"
                                                    onfocus="focusObj(this);" onblur="blurObj(this);" defvalue="First Name" autocomplete="off"
                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red">
                                                *
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns ">
                                                <asp:TextBox ID="txtAMiddleName" runat="server" value="Middle Name"
                                                    onfocus="focusObjM(this);" onblur="blurObjM(this);" defvalue="Middle Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red">
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txtALastName" runat="server" value="Last Name"
                                                    onfocus="focusObj1(this);" onblur="blurObj1(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>

                                            <div class="abreds red">
                                                *
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox CssClass="adtdobcss" value="DOB" ID="Txt_AdtDOB" runat="server"></asp:TextBox>
                                            </div>

                                            <div id="adtreq" class="abreds red">*</div>
                                        </div>
                                        <div class="clear1"></div>
                                        <div class="large-12 medium-12 small-12">
                                            <div class="large-1 medium-1 small-3 columns">
                                                <asp:DropDownList CssClass="txtdd1" ID="ddl_AGender" runat="server">
                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                              <div  id="divFdA" runat="server">

                                            <div class="large-2 medium-2 small-3 columns">
                                                <asp:TextBox ID="txt_Passport_Adl" runat="server" value="Passport No"
                                                    onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No" onkeypress="return keyRestrictWhiteSpace(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txt_Nationality_Adl" runat="server" value="Nationality"
                                                    onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityADL"></asp:TextBox>
                                                <input type="hidden" id="Hdn_Nationality_Adl" name="Hdn_Nationality_Adl" class="Hdn_Nationality_Adl" runat="server" value="IN" />
                                            </div>

                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txt_IssuingCountry_Adl" runat="server" value="Issuing Country"
                                                    onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryADL"></asp:TextBox>
                                                <input type="hidden" id="Hdn_IssuingCountry_Adl" name="Hdn_IssuingCountry_Adl" class="Hdn_IssuingCountry_Adl" runat="server" value="IN" />
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns ">
                                                <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Adl" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>
                                             </div>

                                        </div>
                                        <div class="row" id="A_SG" style="display:none">
                                            <div class="large-12 medium-12 small-12" id="div_ADT" runat="server" style="display: none;">

                                                <div class="large-12 medium-12 small-12  lft blue bld" style="padding-left: 10px;">
                                                    <span style="color: #004b91; float: left; width: 11%;">OutBound:---</span>
                                                    <div runat="server" id="Seg1_Ob_MealBaggage" style="color: black; float: left; width: 20%;"></div>
                                                </div>
                                                <div class="clear"></div>
                                                <div class="large-12 medium-12 small-12">

                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Meal">
                                                        Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_A_Meal_Ob" runat="server" CssClass="Ddl_A_Meal_Ob">
                                                </asp:DropDownList>
                                                        <asp:HiddenField ID="Ddl_A_Meal_Obhid" runat="server" />
                                                    </div>

                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Bag">
                                                        Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1 Ddl_A_EB_Ob" ID="Ddl_A_EB_Ob" runat="server">
                                                </asp:DropDownList>
                                                        <asp:HiddenField ID="Ddl_A_EB_Obhid" runat="server" />
                                                    </div>

                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Seat" style="display:none">
                                                        Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1 SeattypeA" ID="SeattypeA" runat="server">
                                                    </asp:DropDownList>
                                                        <asp:HiddenField ID="SeattypeAhid" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="large-12 medium-12 small-12" runat="server" id="Seg2_Ob" style="display: none;">
                                                    <div class="clear"></div>
                                                    <div runat="server" id="Seg2_Ob_MealBaggage"></div>
                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_A_ExtraMeal">
                                                        Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_A_Meal_Ob_Seg2" runat="server">
                                                </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_A_ExtraBag">
                                                        Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1" ID="Ddl_A_EB_Ob_Seg2" runat="server">
                                                </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <div runat="server" id="tranchor1">
                                                <div class="clear">
                                                </div>


                                                <div class="large-12 medium-12 small-12  lft blue bld" style="padding-left: 10px;">
                                                    OutBound:<span style="color: #004b91">---</span>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div class="large-12 medium-12 small-12 columns ">
                                                    <a runat="server" id='anchor1' class="cursorpointer" onclick="showhide(this,'anchor1','div1');">Meal Preference/Seat Preference/Frequent Flyer</a>
                                                </div>
                                            </div>
                                            <div id="A_ALL" runat="server">
                                                <div id="div1" runat="server" style="display: none;">

                                                    <div class="large-12 medium-12 small-12">
                                                        <div class="large-3 medium-3 small-2 columns ">
                                                            Meal &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_AMealPrefer" runat="server">
                                                        <asp:ListItem Value="">No Pref.</asp:ListItem>
                                                        <asp:ListItem Value="VGML">Vegetarian</asp:ListItem>
                                                        <asp:ListItem Value="AVML">Asian</asp:ListItem>
                                                        <asp:ListItem Value="SFML">Seafood</asp:ListItem>
                                                        <asp:ListItem Value="KSML">Kosher</asp:ListItem>
                                                        <asp:ListItem Value="MOML">Muslim</asp:ListItem>
                                                        <asp:ListItem Value="HNML">Hindu</asp:ListItem>
                                                        <asp:ListItem Value="LFML">Low Fat</asp:ListItem>
                                                        <asp:ListItem Value="LCML">Low Calorie</asp:ListItem>
                                                        <asp:ListItem Value="LPML">Low Protein</asp:ListItem>
                                                        <asp:ListItem Value="GFML">Gluten Free</asp:ListItem>
                                                        <asp:ListItem Value="HFML">High Fiber</asp:ListItem>
                                                        <asp:ListItem Value="DBML">Diabetic</asp:ListItem>
                                                        <asp:ListItem Value="NLML">Non-lactose</asp:ListItem>
                                                        <asp:ListItem Value="PRML">Low Purin</asp:ListItem>
                                                        <asp:ListItem Value="RVML">Raw Vegetarian</asp:ListItem>
                                                        <asp:ListItem Value="CHML">Child</asp:ListItem>
                                                        <asp:ListItem Value="BLML">Bland</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
                                                        <%-- <div class="large-2 medium-2 small-4 columns ">
                                                
                                            </div>--%>
                                                        <div class="large-2 medium-2 small-12 columns large-push-1 medium-push-1">
                                                            Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_ASeatPrefer" runat="server">
                                                        <asp:ListItem Value="">Any</asp:ListItem>
                                                        <asp:ListItem Value="W">Window</asp:ListItem>
                                                        <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>


                                                        <div class="large-5 medium-5 small-12 columns">
                                                            Frequent Flyer &nbsp;<br />
                                                            <span class="lft">
                                                                <asp:TextBox ID="txt_AAirline" runat="server" value="Airline"
                                                                    onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline" CssClass="Airlineval"></asp:TextBox>
                                                                &nbsp;
                                                  <input type="hidden" id="hidtxtAirline_int" name="hidtxtAirline_int" runat="server" value="" /></span>
                                                            <span class="lft">
                                                                <asp:TextBox ID="txt_ANumber" runat="server" MaxLength="10" value="Number"
                                                                    onfocus="focusObjNumber(this);" onblur="blurObjNumber(this);" defvalue="Number" Width="147px"></asp:TextBox></span>
                                                        </div>
                                                        <%-- <div class="large-2 medium-2 small-4 columns ">
                                                
                                            </div>
                                            <div class="large-2 medium-2 small-4 columns ">
                                               
                                            </div>--%>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="clear1"></div>
                                        <div class="row" id="A_SG_IB" style="display:none">
                                            <div id="div_ADT_Ib" runat="server" style="display: none">

                                                <div class="large-12 medium-12 small-12 columns blue bld">
                                                    <span style="color: #004b91; padding-left: 10px; float: left; width: 11%;">InBound:---</span>
                                                    <div runat="server" id="Seg1_Ib_MealBaggage" style="color: black; padding-left: 10px; float: left; width: 20%;"></div>
                                                </div>
                                                <div class="large-12 medium-12 small-12">
                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Ib_ExtraMeal">
                                                        Meal &nbsp;
                                                    <asp:DropDownList ID="Ddl_A_Meal_Ib" runat="server">
                                                    </asp:DropDownList>
                                                    </div>

                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Ib_ExtraBag">
                                                        Excess Baggage  &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="Ddl_A_EB_Ib" runat="server">
                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_R_Seat" style="display:none;">
                                                        Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="SeattypeR" runat="server">
                                                    </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="clear"></div>

                                                <div class="large-12 medium-12 small-12" runat="server" id="Seg2_Ib" style="display: none">
                                                    <div class="clear"></div>
                                                    <div runat="server" id="Seg2_Ib_MealBaggage"></div>
                                                    <div class="large-4 medium-4 small-2 columns" runat="server" id="Seg2_A_Ib_ExtraMeal">
                                                        Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_A_Meal_Ib_Seg2" runat="server">
                                                </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 medium-4 small-12 columns" runat="server" id="Seg2_A_Ib_ExtraBag">
                                                        Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1" ID="Ddl_A_EB_Ib_Seg2" runat="server">
                                                </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="clear"></div>
                                            </div>

                                            <div runat="server" id="tranchor1_R" style="display: none">

                                                <div class="large-12 medium-12 small-12 columns bld blue">
                                                    InBound:<span style="color: #004b91">---</span>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div class="large-12 medium-12 small-12 columns">
                                                    <a runat="server" id='anchor1_R' onclick="showhide(this,'anchor1_R','div1_R');" class="cursorpointer">Meal Preference/Seat Preference/Frequent Flyer</a>
                                                </div>
                                            </div>
                                            <div id="A_ALL_R" runat="server" style="display: none;">
                                                <div id="div1_R" runat="server" class="w100" style="display: none;">
                                                    <div class="large-12 medium-12 small-12 columns">
                                                        <div class="large-1 medium-1 small-2 columns">
                                                            Meal
                                                        </div>
                                                        <div class="large-2 medium-2 small-4 columns">
                                                            <asp:DropDownList CssClass="txtdd1" ID="ddl_AMealPrefer_R" runat="server" Width="100px">
                                                                <asp:ListItem Value="">No Pref.</asp:ListItem>
                                                                <asp:ListItem Value="VGML">Vegetarian</asp:ListItem>
                                                                <asp:ListItem Value="AVML">Asian</asp:ListItem>
                                                                <asp:ListItem Value="SFML">Seafood</asp:ListItem>
                                                                <asp:ListItem Value="KSML">Kosher</asp:ListItem>
                                                                <asp:ListItem Value="MOML">Muslim</asp:ListItem>
                                                                <asp:ListItem Value="HNML">Hindu</asp:ListItem>
                                                                <asp:ListItem Value="LFML">Low Fat</asp:ListItem>
                                                                <asp:ListItem Value="LCML">Low Calorie</asp:ListItem>
                                                                <asp:ListItem Value="LPML">Low Protein</asp:ListItem>
                                                                <asp:ListItem Value="GFML">Gluten Free</asp:ListItem>
                                                                <asp:ListItem Value="HFML">High Fiber</asp:ListItem>
                                                                <asp:ListItem Value="DBML">Diabetic</asp:ListItem>
                                                                <asp:ListItem Value="NLML">Non-lactose</asp:ListItem>
                                                                <asp:ListItem Value="PRML">Low Purin</asp:ListItem>
                                                                <asp:ListItem Value="RVML">Raw Vegetarian</asp:ListItem>
                                                                <asp:ListItem Value="CHML">Child</asp:ListItem>
                                                                <asp:ListItem Value="BLML">Bland</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="large-1 medium-1 small-2 columns">
                                                            Seat
                                                        </div>
                                                        <div class="large-2 medium-2 small-4 columns">
                                                            <asp:DropDownList CssClass="txtdd1" ID="ddl_ASeatPrefer_R" runat="server">
                                                                <asp:ListItem Value="">Any</asp:ListItem>
                                                                <asp:ListItem Value="W">Window</asp:ListItem>
                                                                <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="large-2 medium-2 small-4 columns">
                                                            Frequent Flyer
                                                        </div>
                                                        <div class="large-2 medium-2 small-4 columns">
                                                            <asp:TextBox ID="txt_AAirline_R" runat="server"
                                                                value="Airline" onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline" CssClass="AirlineVal_R"></asp:TextBox>
                                                            <input type="hidden" id="hidtxtAirline_R_int" name="hidtxtAirline_R_int" runat="server" value="" />
                                                        </div>
                                                        <div class="large-2 medium-2 small-4 columns">
                                                            <asp:TextBox ID="txt_ANumber_R" runat="server" MaxLength="10"
                                                                value="Number" onfocus="focusObjNumber(this);" onblur="blurObjNumber(this);"
                                                                defvalue="Number"></asp:TextBox>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="clear1"></div>
                        <div id="td_Child" runat="server">
                            <asp:Repeater ID="Repeater_Child" runat="server" OnItemCreated="Repeater_Child_ItemCreated">
                                <ItemTemplate>

                                    <div class="large-10 medium-10 small-12 headbgs">
                                        <i class="fa fa-user-o" aria-hidden="true"></i>
                                        <asp:Label ID="pttextCHD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>

                                    </div>


                                    <div class="row">
                                        <div class="clear1"></div>
                                        <div class="large-12 medium-12 small-12">
                                            <div class="large-1 medium-1 small-3 columns">
                                                <asp:DropDownList ID="ddl_CTitle" runat="server">
                                                    <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                    <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns ">
                                                <asp:TextBox ID="txtCFirstName" runat="server" value="First Name"
                                                    onfocus="focusObjCFName(this);" onblur="blurObjCFName(this);" defvalue="First Name"
                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red">
                                                *
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txtCMiddleName" runat="server" value="Middle Name"
                                                    onfocus="focusObjCMName(this);" onblur="blurObjCMName(this);" defvalue="Middle Name"
                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red">
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txtCLastName" runat="server" value="Last Name"
                                                    onfocus="focusObjCLName(this);" onblur="blurObjCLName(this);" defvalue="Last Name"
                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                            </div>

                                            <div class="abreds red">
                                                *
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns ">
                                                <asp:TextBox CssClass="txtboxx chddobcss" value="DOB" ID="Txt_chDOB" runat="server"> </asp:TextBox>
                                            </div>
                                            <div class="abreds red">
                                                *
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                        <div class="large-12 medium-12 small-12">
                                            <div class="large-1 medium-1 small-3 columns">
                                                <asp:DropDownList CssClass="txtdd1" ID="ddl_CGender" runat="server">
                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                             <div id="divFdC" runat="server">

                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txt_Passport_Chd" runat="server" value="Passport No"
                                                    onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No" onkeypress="return keyRestrictWhiteSpace(event)"></asp:TextBox>
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>
                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txt_Nationality_Chd" runat="server" value="Nationality"
                                                    onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityCHD"></asp:TextBox>
                                                <input type="hidden" id="Hdn_Nationality_Chd" name="Hdn_Nationality_Chd" class="Hdn_Nationality_Chd" runat="server" value="IN" />
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox ID="txt_IssuingCountry_Chd" runat="server" value="Issuing Country"
                                                    onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryCHD"></asp:TextBox>
                                                <input type="hidden" id="Hdn_IssuingCountry_Chd" name="Hdn_IssuingCountry_Chd" class="Hdn_IssuingCountry_Chd" runat="server" value="IN" />
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>

                                            <div class="large-2 medium-2 small-8 columns">
                                                <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Chd" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="abreds red ">
                                                <div class="psprt">*</div>
                                            </div>
                                            </div>

                                        </div>

                                        <div class="w100" style="display:none">
                                            <div id="C_SG">
                                                <div id="div_CHILD" runat="server" class="large-12 medium-12 small-12" style="display: none;">
                                                    <div class="clear"></div>
                                                    <div class="large-12 medium-12 small-12  lft blue bld" style="padding-left: 10px;">
                                                        <span style="color: #004b91">OutBound:---</span>
                                                        <div runat="server" id="Seg1_C_Ob_MealBaggage" style="color: black; float: left; width: 20%;"></div>
                                                    </div>


                                                    <div class="large-12 medium-12 small-12">
                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_C_Meal">
                                                            Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_C_Meal_Ob" runat="server" CssClass="Ddl_C_Meal_Ob">
                                                </asp:DropDownList>
                                                            <asp:HiddenField ID="Ddl_C_Meal_Obhid" runat="server" />
                                                        </div>
                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_C_Bag">
                                                            Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1 Ddl_C_EB_Ob" ID="Ddl_C_EB_Ob" runat="server">
                                                </asp:DropDownList>
                                                            <asp:HiddenField ID="Ddl_C_EB_Obhid" runat="server" />
                                                        </div>

                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_A_Seat">
                                                            Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1 SeattypeOC" ID="SeattypeOC" runat="server">
                                                    </asp:DropDownList>
                                                            <asp:HiddenField ID="SeattypeOChid" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="large-12 medium-12 small-12" runat="server" id="Seg2_C_Ob" style="display: none;">
                                                        <div class="clear"></div>
                                                        <div class="large-12 medium-12 small-12  lft blue bld" style="padding-left: 10px;">
                                                            <span style="color: #004b91">InBound:---</span>
                                                            <div runat="server" id="Seg2_C_Ob_MealBaggage" style="color: black; float: left; width: 20%;"></div>
                                                        </div>
                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_ExtraCMeal">
                                                            Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_C_Meal_Ob_Seg2" runat="server" CssClass="Ddl_C_Meal_Ob_Seg2">
                                                </asp:DropDownList>
                                                            <asp:HiddenField ID="Ddl_C_Meal_Ob_Seg2hid" runat="server" />
                                                        </div>

                                                        <%--  <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_ExtraCBag">
                                                    Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1 Ddl_C_EB_Ob_Seg2" ID="Ddl_C_EB_Ob_Seg2" runat="server">
                                                </asp:DropDownList>
                                                        <asp:HiddenField ID="Ddl_C_EB_Ob_Seg2hid" runat="server" />
                                                </div>--%>

                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_ExtraSBag">
                                                            Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1 SeattypeIC" ID="SeattypeIC" runat="server">
                                                    </asp:DropDownList>
                                                            <asp:HiddenField ID="SeattypeIChid" runat="server" />
                                                        </div>
                                                    </div>


                                                    <%--  <div class="large-12 medium-12 small-12" runat="server" id="Seg2_C_Ob" style="display: none;">
                                                 <div class="clear"></div>
                                                <div runat="server" id="Seg2_C_Ob_MealBaggage"></div>
                                                <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_ExtraCMeal">
                                                    Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_C_Meal_Ob_Seg2" runat="server">
                                                </asp:DropDownList>
                                                </div>
                                                <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg2_ExtraCBag">
                                                    Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1" ID="Ddl_C_EB_Ob_Seg2" runat="server">
                                                </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                                </div>
                                                <div runat="server" id="tranchor2">
                                                    <div class="clear">
                                                    </div>
                                                    <div class="large-12 medium-12 small-12 blue bld" style="padding-left: 10px;">
                                                        OutBound:<span style="color: #004b91">---</span>
                                                    </div>
                                                    <div class="hide">
                                                    </div>
                                                    <div class="w101" style="padding-top: 10px;">
                                                        <a runat="server" id='anchor2' onclick="showhide(this,'anchor2','div2');" class="cursorpointer">Meal Preference/Seat Preference</a>
                                                    </div>
                                                </div>
                                                <div id="C_ALL" runat="server">
                                                    <div id="div2" runat="server" class="large-12 medium-12 small-12" style="display: none; padding-top: 10px;">
                                                        <div class="large-3 medium-3 small-12 columns">
                                                            Meal &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_CMealPrefer" runat="server">
                                                        <asp:ListItem Value="">No Pref.</asp:ListItem>
                                                        <asp:ListItem Value="VGML">Vegetarian</asp:ListItem>
                                                        <asp:ListItem Value="AVML">Asian</asp:ListItem>
                                                        <asp:ListItem Value="SFML">Seafood</asp:ListItem>
                                                        <asp:ListItem Value="KSML">Kosher</asp:ListItem>
                                                        <asp:ListItem Value="MOML">Muslim</asp:ListItem>
                                                        <asp:ListItem Value="HNML">Hindu</asp:ListItem>
                                                        <asp:ListItem Value="LFML">Low Fat</asp:ListItem>
                                                        <asp:ListItem Value="LCML">Low Calorie</asp:ListItem>
                                                        <asp:ListItem Value="LPML">Low Protein</asp:ListItem>
                                                        <asp:ListItem Value="GFML">Gluten Free</asp:ListItem>
                                                        <asp:ListItem Value="HFML">High Fiber</asp:ListItem>
                                                        <asp:ListItem Value="DBML">Diabetic</asp:ListItem>
                                                        <asp:ListItem Value="NLML">Non-lactose</asp:ListItem>
                                                        <asp:ListItem Value="PRML">Low Purin</asp:ListItem>
                                                        <asp:ListItem Value="RVML">Raw Vegetarian</asp:ListItem>
                                                        <asp:ListItem Value="CHML">Child</asp:ListItem>
                                                        <asp:ListItem Value="BLML">Bland</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>

                                                        <div class="large-2 medium-2 small-12 columns large-push-1 medium-push-1">
                                                            Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_CSeatPrefer" runat="server">
                                                        <asp:ListItem Value="">Any</asp:ListItem>
                                                        <asp:ListItem Value="W">Window</asp:ListItem>
                                                        <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
                                                        <div class="large-3 medium-3 small-3 columns">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="C_SG_IB">
                                                <div id="div_CHILD_Ib" runat="server" style="display: none;">
                                                    <div class="clear"></div>
                                                    <div class="large-12 medium-12 small-12 blue bld">
                                                        <span style="color: #004b91; padding-left: 10px; float: left; width: 11%;">InBound:---</span>
                                                        <div runat="server" id="Seg1_C_Ib_MealBaggage" style="color: black; padding-left: 10px; float: left; width: 20%;"></div>
                                                    </div>
                                                    <div class="clear"></div>
                                                    <div class="large-12 medium-12 small-12" style="padding-top: 10px;">
                                                        <div class="large-4 medium-4 small-4 columns" id="Seg1_C_Ib_Meal" runat="server">
                                                            Meal &nbsp;
                                                        <asp:DropDownList CssClass="txtdd1" ID="Ddl_C_Meal_Ib" runat="server">
                                                        </asp:DropDownList>
                                                        </div>
                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_C_Ib_Bag">
                                                            Excess Baggage &nbsp;
                                                        <asp:DropDownList CssClass="txtdd1" ID="Ddl_C_EB_Ib" runat="server">
                                                        </asp:DropDownList>
                                                        </div>
                                                        <div class="large-4 medium-4 small-4 columns" runat="server" id="Seg1_C_Ib_Seat">
                                                            Seat &nbsp;
                                                    <asp:DropDownList CssClass="txtdd1 SeattypeOR" ID="SeattypeOR" runat="server">
                                                    </asp:DropDownList>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="large-12 medium-12 small-12" runat="server" id="Seg2_C_Ib" style="display: none;">
                                                        <div class="clear"></div>
                                                        <div runat="server" id="Seg2_C_Ib_MealBaggage"></div>
                                                        <div class="large-4 medium-4 small-2 columns" runat="server" id="Seg2_C_Ib_ExtraMeal">
                                                            Meal &nbsp;
                                                <asp:DropDownList ID="Ddl_C_Meal_Ib_Seg2" runat="server">
                                                </asp:DropDownList>
                                                        </div>
                                                        <div class="large-4 medium-4 small-4 columns large-push-4 medium-push-4" runat="server" id="Seg2_C_Ib_ExtraBag">
                                                            Excess Baggage  &nbsp;
                                                <asp:DropDownList CssClass="txtdd1" ID="Ddl_C_EB_Ib_Seg2" runat="server">
                                                </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="tranchor2_R" runat="server" class="hide">
                                                    <div class="clear">
                                                    </div>
                                                    <div class="large-12 medium-12 small-12 blue bld">
                                                        InBound:<span style="color: #004b91">---</span>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div class="large-12 medium-12 small-12" style="padding-top: 10px;">
                                                        <a runat="server" id='anchor2_R' onclick="showhide(this,'anchor2_R','div2_R');" class="cursorpointer">Meal Preference/Seat Preference</a>
                                                    </div>
                                                </div>
                                                <div id="C_ALL_R" runat="server" style="display: none;">
                                                    <div id="div2_R" runat="server" style="display: none;">
                                                        <div class="large-12 medium-12 small-12" style="padding-top: 10px;">
                                                            <div class="large-1 medium-1 small-3 columns">
                                                                Meal
                                                            </div>
                                                            <div class="large-3 medium-3 small-3 columns">
                                                                <asp:DropDownList CssClass="txtdd1" ID="ddl_CMealPrefer_R" runat="server" Width="100px">
                                                                    <asp:ListItem Value="">No Pref.</asp:ListItem>
                                                                    <asp:ListItem Value="VGML">Vegetarian</asp:ListItem>
                                                                    <asp:ListItem Value="AVML">Asian</asp:ListItem>
                                                                    <asp:ListItem Value="SFML">Seafood</asp:ListItem>
                                                                    <asp:ListItem Value="KSML">Kosher</asp:ListItem>
                                                                    <asp:ListItem Value="MOML">Muslim</asp:ListItem>
                                                                    <asp:ListItem Value="HNML">Hindu</asp:ListItem>
                                                                    <asp:ListItem Value="LFML">Low Fat</asp:ListItem>
                                                                    <asp:ListItem Value="LCML">Low Calorie</asp:ListItem>
                                                                    <asp:ListItem Value="LPML">Low Protein</asp:ListItem>
                                                                    <asp:ListItem Value="GFML">Gluten Free</asp:ListItem>
                                                                    <asp:ListItem Value="HFML">High Fiber</asp:ListItem>
                                                                    <asp:ListItem Value="DBML">Diabetic</asp:ListItem>
                                                                    <asp:ListItem Value="NLML">Non-lactose</asp:ListItem>
                                                                    <asp:ListItem Value="PRML">Low Purin</asp:ListItem>
                                                                    <asp:ListItem Value="RVML">Raw Vegetarian</asp:ListItem>
                                                                    <asp:ListItem Value="CHML">Child</asp:ListItem>
                                                                    <asp:ListItem Value="BLML">Bland</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="large-1 medium-1 small-3 columns large-push-4 medium-push-4">
                                                                Seat
                                                            </div>
                                                            <div class="large-3 medium-3 small-3 columns">
                                                                <asp:DropDownList CssClass="txtdd1" ID="ddl_CSeatPrefer_R" runat="server">
                                                                    <asp:ListItem Value="">Any</asp:ListItem>
                                                                    <asp:ListItem Value="W">Window</asp:ListItem>
                                                                    <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear1">
                                        </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="clear1"></div>

                        <div id="td_Infant" runat="server">
                            <asp:Repeater ID="Repeater_Infant" runat="server">
                                <ItemTemplate>

                                    <div class="large-10 medium-10 small-12 headbgs">
                                        <i class="fa fa-user-o" aria-hidden="true"></i>
                                        <asp:Label ID="pttextINF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>

                                    </div>


                                    <div class="large-12 medium-12 small-3" style="padding-top: 10px;">
                                        <div class="large-1 medium-1 small-3 columns">
                                            <asp:DropDownList CssClass="txtdd1" ID="ddl_ITitle" runat="server">
                                                <%-- <asp:ListItem Value="">Title</asp:ListItem>--%>
                                                <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox ID="txtIFirstName" runat="server" value="First Name"
                                                onfocus="focusObjIFName(this);" onblur="blurObjIFName(this);" defvalue="First Name"
                                                onkeypress="return isCharKey(event)"></asp:TextBox>
                                        </div>
                                        <div class="abreds red">
                                            *
                                        </div>
                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox ID="txtIMiddleName" runat="server" value="Middle Name"
                                                onfocus="focusObjIMName(this);" onblur="blurObjIMName(this);" defvalue="Middle Name"
                                                onkeypress="return isCharKey(event)"></asp:TextBox>
                                        </div>
                                        <div class="abreds red">
                                        </div>
                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox ID="txtILastName" runat="server" value="Last Name"
                                                onfocus="focusObjILName(this);" onblur="blurObjILName(this);" defvalue="Last Name"
                                                onkeypress="return isCharKey(event)"></asp:TextBox>
                                        </div>
                                        <div class="abreds red">
                                            *
                                        </div>

                                        <div class="large-2 medium-2 small-8 columns">

                                            <asp:TextBox CssClass="txtboxx infdobcss" Value="DOB" ID="Txt_InfantDOB" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="abreds red">
                                            *
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="large-12 medium-12 small-12">
                                        <div class="large-1 medium-1 small-3 columns">
                                            <asp:DropDownList CssClass="txtdd1" ID="ddl_IGender" runat="server">
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                           <div id="divFdI" runat="server">


                                        <div class="large-2 medium-2 small-3 columns">
                                            <asp:TextBox ID="txt_Passport_Inf" runat="server" value="Passport No"
                                                onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No" onkeypress="return keyRestrictWhiteSpace(event)"></asp:TextBox>
                                        </div>
                                        <div class="abreds red ">
                                            <div class="psprt">*</div>
                                        </div>

                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox ID="txt_Nationality_Inf" runat="server" value="Nationality"
                                                onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityINF"></asp:TextBox>
                                            <input type="hidden" id="Hdn_Nationality_Inf" name="Hdn_Nationality_Inf" class="Hdn_Nationality_Inf" runat="server" value="IN" />
                                        </div>
                                        <div class="abreds red ">
                                            <div class="psprt">*</div>
                                        </div>

                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox ID="txt_IssuingCountry_Inf" runat="server" value="Issuing Country"
                                                onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryINF"></asp:TextBox>
                                            <input type="hidden" id="Hdn_IssuingCountry_Inf" name="Hdn_IssuingCountry_Inf" class="Hdn_IssuingCountry_Inf" runat="server" value="IN" />
                                        </div>
                                        <div class="abreds red ">
                                            <div class="psprt">*</div>
                                        </div>
                                        <div class="large-2 medium-2 small-8 columns">
                                            <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Inf" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="abreds red ">
                                            <div class="psprt">*</div>
                                        </div>
                                               </div>
                                    </div>
                                    <div class="clear"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div class="clear1"></div>
                        <div class=" bgs">
                            <div class="large-12 medium-12 small-12 bld">
                                Primary Guest Details
                            </div>

                            <div class="large-12 medium-12 small-12">
                                <div class="large-3 medium-3 small-6 columns ">
                                    <span id="spn_Projects" runat="server">Project Id</span>
                                </div>
                                <div class="large-3 medium-3 small-6 columns ">
                                    <span id="spn_Projects1" runat="server">
                                        <asp:DropDownList CssClass="txtdd1" ID="DropDownListProject" runat="server">
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="large-3 medium-3 small-6 columns ">
                                    <span id="Spn_BookedBy" runat="server">Booked By </span>
                                </div>
                                <div class="large-3 medium-3 small-6 columns ">
                                    <span id="Spn_BookedBy1" runat="server">
                                        <asp:DropDownList CssClass="txtdd1" ID="DropDownListBookedBy" runat="server">
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>
                            <div class="large-12 medium-12 small-12">
                                <div class="large-1 medium-1 small-3 columns ">
                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_PGTitle" runat="server">
                                        <%--<asp:ListItem Value="" Selected="True">Select Title</asp:ListItem>--%>
                                        <asp:ListItem Value="Mr">Mr.</asp:ListItem>                                        
                                        <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                        <asp:ListItem Value="Mstr">Mrs.</asp:ListItem>                                        
                                    </asp:DropDownList>
                                </div>
                                <div class="large-2 medium-2 small-8 columns">
                                    <asp:TextBox ID="txt_PGFName" runat="server" value="First Name"
                                        onfocus="focusObjPF(this);" onblur="blurObjPF(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-2 medium-2 small-11 columns">
                                    <asp:TextBox ID="txt_PGLName" runat="server" value="Last Name"
                                        onfocus="focusObjPL(this);" onblur="blurObjPL(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-2 medium-2 small-11 columns">
                                    <asp:TextBox ID="txt_Email" value="Email Id" onfocus="focusObjPE(this);"
                                        onblur="blurObjPE(this);" defvalue="Email Id" runat="server"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-1 medium-1 small-11 columns  ">
                                    <asp:TextBox ID="txt_MobNo" runat="server" value="Mobile No" onfocus="focusObjPM(this);"
                                        onblur="blurObjPM(this);" defvalue="Mobile No" onkeypress="return isNumberKey(event)"
                                        MaxLength="12"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                            </div>

                            <div class="row">
                                <div class="clear1"></div>
                                <div class="large-10 medium-10 small-12 headbgs">
                                    <span>Seat Selection</span>
                                </div>

                                <div class="clear1"></div>
                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                    <div class="col-md-6 col-xs-6">
                                        <div class="col-md-12 col-xs-12 bld p10" id="InBoundFTseat_ibSec1" runat="server"></div>
                                        <input type="hidden" id="seatSelect" name='seatSelect' runat="server" />
                                    </div>
                                    <div class="col-md-6 col-xs-6" id="InBoundFTseat_ibSec2" runat="server">
                                    </div>
                                    <div class="w100 lft" style="">
                                        <div id="SeatDetails" class="p10"></div>
                                    </div>
                                    <div class="w100 lft" style="" id="InBoundFTSeat_ibDt" runat="server">
                                        <input type="hidden" id="SeatDetails_ibDtls" name='SeatDetails_ibDtls' runat="server" />
                                        <div id="SeatDetails_ib" class="p10" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                            <%--GST DETAILS START--%>
                            <div class="clear1"></div>

                            <div  id="gstfd" runat="server">
                            <div class="large-10 medium-10 small-12 headbgs">
                                <%--<i class="fa fa-user-o" aria-hidden="true"></i>--%>
                                <span>GST Details (Optional)</span>
                            </div>

                            <div class="clear1"></div>
                            <div class="large-12 medium-12 small-12 " id="divGstDetails" runat="server">
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">GST No:</label>
                                    <asp:TextBox ID="txtGstNo" runat="server" placeholder="GST No" MaxLength="15" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                </div>
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">Company Name:</label>
                                    <asp:TextBox ID="txtGstCmpName" runat="server" placeholder="Comoany Name" MaxLength="25" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                </div>
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">Company Address:</label>
                                    <asp:TextBox ID="txtGstAddress" runat="server" placeholder="GST Address" MaxLength="30" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz/1234567890');"></asp:TextBox>
                                </div>
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">GST PIN Code:</label>
                                    <asp:TextBox ID="txtPinCode" runat="server" placeholder="GST Pin Code" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="8"></asp:TextBox>
                                </div>
                                <asp:HiddenField ID="GSTStateHid" runat="server" Value="Select State" />
                                <asp:HiddenField ID="GSTCityHid" runat="server" Value="Select City" />
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">State:</label>

                                    <select id="ddlStateGst" class="" runat="server">
                                        <option value="1">Select</option>

                                    </select>
                                </div>
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">City:</label>
                                    <select id="ddlCityGst" class="" runat="server">
                                        <option value="1">Select</option>
                                    </select>
                                    <div style="border-image: none; left: 95px; top: -53px; width: 36px; display: none; position: relative;" id="Select1Loding" runat="server">
                                        <img src="../Images/load.gif" />
                                    </div>
                                </div>
                                <%-- <div class="abreds"></div>
                                                <div class="large-2 medium-2 small-12 columns">
                                                    <label class="bld1">State:</label>
                                                     <asp:DropDownList ID="ddlStateGst" runat="server" AutoPostBack="true"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlStateGst_SelectedIndexChanged">
                                                </asp:DropDownList>  
                                                </div>
                                                 <div class="abreds"></div>
                                                 
                                                <div class="large-2 medium-2 small-12 columns">
                                                    <label class="bld1">city:</label>
                                                      <asp:DropDownList ID="ddlCityGst" runat="server" CssClass="form-control"></asp:DropDownList>     
                                                </div>--%>


                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns">
                                    <label class="bld1">GST Phone:</label>
                                    <asp:TextBox ID="txtGstPhone" runat="server" placeholder="GST Phone" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="abreds"></div>
                                <div class="large-2 medium-2 small-12 columns end">
                                    <label class="bld1">GST Email: </label>
                                    <asp:TextBox ID="txtGstEmail" runat="server" placeholder="GST Email"></asp:TextBox>
                                </div>

                            </div>
                                </div>
                            <div class="clear1"></div>
                            <div class="row" style="background-color: #fff;">
                                <div class="large-10 medium-10 small-12 headbgs"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>Remarks</div>
                                <div class="clear1"></div>
                                <div class="large-12 medium-12 small-12 ">
                                    <textarea rows="2" cols="50" placeholder="Remarks" id="txtRemarks" runat="server">
                                </textarea>

                                </div>
                            </div>

                            <div class="clear1"></div>
                            <%--GST DETAILS END--%>
                            <div class="clear1"></div>
                            <div id="div_Submit" class="large-2 medium-2 small-12 large-push-9 medium-push-9">
                                <asp:Button ID="book" runat="server" CssClass="buttonfltbk" Text="Continue" />
                            </div>
                        </div>

                    </div>
                    <div class="clear1"></div>

                </div>

                <div id="div_Progress" style="display: none">
                    <b>Processing..</b>
                    <img alt="Processing" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                </div>

            </div>
            <div class="clear1"></div>

        </div>

    </div>

    </div>
 
  
    


 <div id="toPopup1" class="" style="z-index: 1000; border: none; margin: 0px; padding: 0px; width: 100%; height: 100%; top: 0px; left: 0px; background-color: rgb(0, 0, 0 ,0.8); position: fixed; display: none">
            <div id="toPopup">
                <div class="close"></div>
                <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
                <div id="popup_content w100">
                    <div id="SeatSource"></div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="hdnCheckGST" runat="server" />
        <asp:HiddenField ID="OBTrackIds" runat="server" />
        <asp:HiddenField ID="IBTrackIds" runat="server" />
        <asp:HiddenField ID="OBValidatingCarrier" runat="server" />
        <asp:HiddenField ID="IBValidatingCarrier" runat="server" />
        <asp:HiddenField ID="HiddenField3" Value="" runat="server" />
        <asp:HiddenField ID="hdn_Psprt" runat="server" />
        <asp:HiddenField ID="hdn_vc" runat="server" />
        <asp:HiddenField ID="hdn_jrnyDate" runat="server" />
        <asp:HiddenField ID="hdnPaxListforSeat" Value="" runat="server" />
        <div id="waitMessagefc" class="hide">
            <div style="text-align: center; z-index: 1001; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; border: 1px solid #d1d1d1; border-radius: 10px;">
                <h1 class="text-center" style="font-size: 20px;">PLEASE WAIT</h1>
                <span>Please do not close or refresh this window.</span><br>
                <br>
                <img alt="loading" src="../images/loadingAnim.gif" />
                <br />
            </div>
        </div>
    <script type="text/javascript">
        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg

        }
    </script>


         <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.draggable.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
          <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/SeatMap1.3.js")%>"></script>
    <script type="text/javascript">
        var d = new Date();





        $(function () { var d = new Date(); var dd = new Date(1952, 01, 01); $(".adtdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: ('1920:' + (d.getFullYear() - 12) + ''), navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true, defaultDate: dd }) });
        $(function () {
            var jd = document.getElementById("ctl00_ContentPlaceHolder1_hdn_jrnyDate").value;
            var jda = jd.split('_')
            var jyy = jd.toString().substring(4, 6);
            var jmm = jd.toString().substring(2, 4);
            var jdd = jd.toString().substring(0, 2);

            if (jda[1] == "1G") {
                jyy = jd.toString().substring(0, 2);
                jmm = jd.toString().substring(2, 4);
                jdd = jd.toString().substring(4, 6);
            }
            var jdn = new Date('20' + jyy, (parseInt(jmm) - 1), jdd);
            var jdns = new Date(jdn.getFullYear() - 12, jdn.getMonth(), jdn.getDate());
            var jdnm = new Date(jdn.getFullYear() - 2, jdn.getMonth(), jdn.getDate());

            $(".chddobcss").each(function () {
                //$(this).val(jdns);
                $(this).datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: jdnm, minDate: jdns, yearRange: '1920:2017', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true })

            })
        });
        $(function () {
            var jd = document.getElementById("ctl00_ContentPlaceHolder1_hdn_jrnyDate").value;
            var jda = jd.split('_')
            var jyy = jd.toString().substring(4, 6);
            var jmm = jd.toString().substring(2, 4);
            var jdd = jd.toString().substring(0, 2);

            if (jda[1] == "1G") {
                jyy = jd.toString().substring(0, 2);
                jmm = jd.toString().substring(2, 4);
                jdd = jd.toString().substring(4, 6);
            }



            var jdn1 = new Date('20' + jyy, (parseInt(jmm) - 1), jdd);
            var jdnm = new Date(jdn1.getFullYear() - 2, jdn1.getMonth(), jdn1.getDate());
            $(".infdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: jdn1, minDate: jdnm, navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true, defaultDate: jdn1 })
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtAFirstName").live("keyup", function () {
                $("#ctl00_ContentPlaceHolder1_txt_PGFName").val($("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtAFirstName").val())
            });

            $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtAFirstName").live("blur", function () {
                $("#ctl00_ContentPlaceHolder1_txt_PGFName").val($("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtAFirstName").val())
            });

            $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtALastName").live("keyup", function () {
                $("#ctl00_ContentPlaceHolder1_txt_PGLName").val($("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtALastName").val())
            });

            $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtALastName").live("blur", function () {
                $("#ctl00_ContentPlaceHolder1_txt_PGLName").val($("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_txtALastName").val())
            })
            // Airline Autocomplete for International
            var aircode = $('.Airlineval').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/FetchAirlineList",
                            data: "{ 'airline': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.ALName + "(" + e.ALCode + ")";
                                    var n = e.ALCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                        // $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_hidtxtAirline_int").val(n.item.id)
                    }
                });
            });
            var aircode = $('.AirlineVal_R').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/FetchAirlineList",
                            data: "{ 'airline': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.ALName + "(" + e.ALCode + ")";
                                    var n = e.ALCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                        // $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_hidtxtAirline_int").val(n.item.id)
                    }
                });
            });

            //  Autocomplete for Adult/Child/Infant Nationality
            var countrycode = $('.NationalityADL').each(function () {

                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });
            var countrycode = $('.NationalityCHD').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });
            var countrycode = $('.NationalityINF').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });

            //  Autocomplete for Adult/Child/Infant IssuingCountry
            var countrycode = $('.IssuingCountryADL').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });
            var countrycode = $('.IssuingCountryCHD').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });
            var countrycode = $('.IssuingCountryINF').each(function () {
                $(this).autocomplete({
                    source: function (e, t) {
                        $.ajax({
                            url: UrlBase + "CitySearch.asmx/GetCountryCd",
                            data: "{ 'country': '" + e.term + "', maxResults: 10 }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (e) {
                                t($.map(e.d, function (e) {
                                    var t = e.CountryName + "(" + e.CountryCode + ")";
                                    var n = e.CountryCode;
                                    return {
                                        label: t,
                                        value: t,
                                        id: n
                                    }
                                }))
                            },
                            error: function (e, t, n) {
                                alert(t)
                            }
                        })
                    },
                    autoFocus: true,
                    minLength: 3,
                    select: function (t, n) {
                        $(this).next().val(n.item.id)
                    }
                });
            });


            $(".Ddl_A_Meal_Ob").change(function () {
                $("#" + this.id + "hid").val(this.value);
            });
            $(".Ddl_A_EB_Ob").change(function () {
                $("#" + this.id + "hid").val(this.value);
            });
            $(".SeattypeA").change(function () {
                $("#" + this.id + "hid").val(this.value);
            });
        });


        function getKeyCode(e) {
            if (window.event)
                return window.event.keyCode;
            else if (e)
                return e.which;
            else
                return null;
        }
        function keyRestrict(e, validchars) {
            var key = '', keychar = '';
            key = getKeyCode(e);
            if (key == null) return true;
            keychar = String.fromCharCode(key);
            keychar = keychar.toLowerCase();
            validchars = validchars.toLowerCase();
            if (validchars.indexOf(keychar) != -1)
                return true;
            if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
                return true;
            return false;
        }

        //Date 05-12-2017 GST Validation
        function gStValidate(gstValue) {
            var gstValid = true;
            if (gstValue.length == 15) {
                var gstStateCode = gstValue.substring(0, 2);
                var gstPANNo = gstValue.substring(2, 12);
                var gstRegisterationNumbr = gstValue.substring(12, 13);
                var gstDefaultNo = gstValue.substring(13, 14);
                var gstCheckCode = gstValue.substring(14, 15);

                if (gstStateCode.length == 2) {
                    // var k = isNumberKey(gstStateCode);
                    if (isNumberKey1(gstStateCode) == false) {
                        gstValid = false;
                    }
                }
                else { gstValid = false; }
                if (gstPANNo.length == 10) {
                    ObjVal = gstPANNo;
                    var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                    var code = /([C,P,H,F,A,T,B,L,J,G])/;
                    var code_chk = ObjVal.substring(3, 4);
                    if (ObjVal.search(panPat) == -1) {
                        gstValid = false;
                    }
                    if (code.test(code_chk) == false) {
                        gstValid = false;
                    }
                }
                else { gstValid = false; }
                if (gstRegisterationNumbr.length == 1) {
                    if (alphanumeric(gstRegisterationNumbr) == false) {
                        gstValid = false;
                    }
                }
                else { gstValid = false; }
                if (gstDefaultNo.toString().toLowerCase() != ("Z".toLowerCase() || "N".toLowerCase())) {
                    gstValid = false;
                }
                if (gstCheckCode.length == 1) {
                    if (alphanumeric(gstCheckCode) == false) {
                        gstValid = false;
                    }
                }
                else { gstValid = false; }
            }
            else {
                gstValid = false;
            }
            return gstValid;
        }

        function isNumberKey_GST(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57 || charCode == 08 || charCode == 46) {
                return true;
            }
            else {

                return false;
            }
        }
        function isNumberKey1(evt) {
            // var charCode = evt;//(evt.which) ? evt.which : event.keyCode;
            var gstValid = true;
            if (evt.match(/^\d+$/)) {
                gstValid = true;
            }
            else {

                gstValid = false;
            }
            return gstValid;

        }
        function alphanumeric(txt) {
            var gstValid = true;
            var letters = /^[0-9a-zA-Z]+$/;
            if (txt.match(letters)) {
                //alert('Your registration is correct ');
                //document.form1.text1.focus();
                gstValid = true;
            }
            else {
                //alert('Please input alphanumeric characters only');
                gstValid = false;
            }
            return gstValid;
        }
        function isCharNumberKey_GST(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode >= 48 && charCode <= 57 || charCode == 32 || charCode == 08 || charCode == 42) {
                return true;
            }
            else {
                //alert("please enter Alphabets  only");
                return false;
            }
        }
        function AddressValidate(Address) {
            var gstAddValid = "";

            var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
            var data = Address;
            for (var i = 0; i < data.length; i++) {
                if (iChars.indexOf(data.charAt(i)) != -1) {
                    gstAddValid = "Only alpha numeric is allowed. \nPlease enter valid input.";
                    //document.getElementById("txtCallSign").value = "";
                    //return false;
                }
            }
            return gstAddValid;

        }
        function NumericValidate(Address) {
            var gstAddValid = "";

            var iCharscon = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
            var data = $.trim(Address);
            for (var i = 0; i < data.length; i++) {
                if (iCharscon.indexOf(data.charAt(i)) != -1) {
                    gstAddValid = "Only numeric is allowed. \nPlease enter valid number.";
                    //document.getElementById("txtCallSign").value = "";
                    //return false;
                }
            }

            return gstAddValid;

        }
        function CharValidate(Address) {
            var gstAddValid = "";

            var iCharscon = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_1234567890";
            var data = $.trim(Address);
            for (var i = 0; i < data.length; i++) {
                if (iCharscon.indexOf(data.charAt(i)) != -1) {
                    gstAddValid = "Only character is allowed. \nPlease enter valid number.";
                    //document.getElementById("txtCallSign").value = "";
                    // return false;
                }
            }

            return gstAddValid;

        }
        function isCharKey_GST(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode == 32 || charCode == 08) {
                return true;
            }
            else {
                //alert("please enter Alphabets  only");
                return false;
            }
        }
        function keyRestrictWhiteSpace(event, validchars) {
            // var k = event ? event.which : window.event.keyCode;
            // if (k == 32) return false;
            // else {
            //   jAlert('Passport number should not have space.', 'Alert');
            //}

        }

        $(function () {
            $(".datepicker").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                })
        });
    </script>

    <script src="../chosen/chosen.jquery.js"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);

        }


        $(document).ready(function () {
            GetGSTState();
        });
        $("#ctl00_ContentPlaceHolder1_ddlCityGst").change(function () {
            var vals = $("#ctl00_ContentPlaceHolder1_ddlCityGst option:selected").val();
            $("#ctl00_ContentPlaceHolder1_GSTCityHid").val($("#ctl00_ContentPlaceHolder1_ddlCityGst option:selected").text());
        });
        $("#ctl00_ContentPlaceHolder1_ddlStateGst").change(function () {
            var vals = $("#ctl00_ContentPlaceHolder1_ddlStateGst option:selected").val();
            $("#ctl00_ContentPlaceHolder1_GSTStateHid").val($("#ctl00_ContentPlaceHolder1_ddlStateGst option:selected").text());
            if (vals == "select" || vals == "") {

            }
            else {

                $("#ctl00_ContentPlaceHolder1_ddlCityGst").hide();
                $("#ctl00_ContentPlaceHolder1_Select1Loding").show();
                $.ajax({
                    url: UrlBase + "FareRuleService.asmx/FetchGSTStateList",
                    data: JSON.stringify({ cityCode: vals }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var target = $('#ctl00_ContentPlaceHolder1_ddlCityGst');
                        target.empty();

                        $(document.createElement('option'))

                               .attr('value', 'Select')
                               .text('Select')
                               .appendTo(target);


                        $(data.d).each(function () {
                            $(document.createElement('option'))

                                .attr('value', this.CityName)
                                .text(this.CityName)
                                .appendTo(target);
                        });
                        $("#ctl00_ContentPlaceHolder1_Select1Loding").hide();
                        $("#ctl00_ContentPlaceHolder1_ddlCityGst").show();
                        $("#ctl00_ContentPlaceHolder1_ddlCityGst").trigger("chosen:updated");

                    },
                    error: function (e, t, n) {
                        $("#ctl00_ContentPlaceHolder1_Select1Loding").hide();
                        $("#ctl00_ContentPlaceHolder1_ddlCityGst").show();
                    }
                });
            }

        });
        function GetGSTState() {
            $.ajax({
                url: UrlBase + "FareRuleService.asmx/FetchGSTStateList",
                data: JSON.stringify({ cityCode: "India" }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var target = $('#ctl00_ContentPlaceHolder1_ddlStateGst');
                    target.empty();

                    $(document.createElement('option'))

                           .attr('value', 'Select')
                           .text('Select')
                           .appendTo(target);


                    $(data.d).each(function () {
                        $(document.createElement('option'))

                            .attr('value', this.AirportCode)
                            .text(this.CityName)
                            .appendTo(target);
                    });

                    $("#ctl00_ContentPlaceHolder1_ddlStateGst").trigger("chosen:updated");

                },
                error: function (e, t, n) {

                }
            });
        }
    </script>





      <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>

</asp:Content>
