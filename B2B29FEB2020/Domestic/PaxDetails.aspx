<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="PaxDetails.aspx.vb" Inherits="FlightDom_CustomerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../CSS/customerinfo.css" rel="stylesheet" type="text/css" />--%>
    <%--  <link href="<%= ResolveUrl("css/itz.css") %>" rel="stylesheet" type="text/css" />--%>
    <%--<link href="<%= ResolveUrl("css/alert.css") %>" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="../styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/CSS/newcss/SeatPopup.css")%>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/CSS/newcss/Seat.css")%>" rel="stylesheet" />
    <link href="../Advance_CSS/css/paxdetails.css" rel="stylesheet" />
    <link type="text/css" href="~/icofont/icofont.css" rel="stylesheet" />
    <link type="text/css" href="~/icofont/icofont.min.css" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/JSLINQ.js")%>"></script>
    <link href="../Custom_Design/css/skunal.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
 
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
        //ctl00_ContentPlaceHolder1_book
        $(document).ready(function () {

            var btnclk = 0;
            var vcc = document.getElementById('ctl00_ContentPlaceHolder1_hdn_vc').value;
            if (vcc == "I5" || vcc == "AK") {

                $('.adtreq').show();
            }
            else { $('.adtreq').hide(); }

            $("#ctl00_ContentPlaceHolder1_book").click(function (e) {
                debugger;
                var elem = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("input");

                var elemSelect = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("select");



                var Adult = parseInt(0);
                var Child = parseInt(0);
                var pattern = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;

                for (var j = 0; j < elemSelect.length; j++) {

                    if (elemSelect[j].id.indexOf("ddl_ATitle") > 0) {

                        if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                            //alert('First Name can not be blank for Adult');1
                            //$("#dialog").dialog();
                            //jAlert('Please select the Title.krishna', 'Alert');
                            ErrorPopup("Please select the Title.");
                            elemSelect[j].focus();
                            return false;
                        }
                    }

                }

                for (var i = 0; i < elem.length; i++) {

                    if (elem[i].type == "text" && elem[i].id.indexOf("txtAFirstName") > 0) {
                        Adult++;
                        if (elem[i].value == "" || elem[i].value == "First Name") {
                            //alert('First Name can not be blank for Adult');
                            //$("#dialog").dialog();
                            ErrorPopup("First Name can not be blank for Adult");
                            //jAlert('First Name can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                            //alert('First Name can not be blank for Adult');
                            //$("#dialog").dialog();
                            ErrorPopup("First Name required atleast one characters for Adult");
                            //jAlert('First Name required atleast one characters for Adult.', 'Alert');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtALastName") > 0) {

                        if (elem[i].value == "" || elem[i].value == "Last Name") {
                            //alert('Last Name can not be blank for Adult');
                            //jAlert('Last Name can not be blank for Adult', 'Alert');
                            ErrorPopup("Last Name can not be blank for Adult");
                            elem[i].focus();
                            return false;
                        }

                        else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                            //alert('Last Name can not be blank for Adult');
                            //jAlert('Last Name required atleast one characters.', 'Alert');
                            ErrorPopup("Last Name required atleast one characters");
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_AdtDOB") > 0) {
                        //alert('Age can not be blank for Adult');


                        if (vcc == "I5" || vcc == "AK") {
                            //jAlert('Age can not be blank for Adult', 'Alert');
                            ErrorPopup("Age can not be blank for Adult");
                            elem[i].focus();
                            return false;
                        }
                    }

                    if (elem[i].type == "text" && (elem[i].value != "") && elem[i].value != "DOB" && pattern.test(elem[i].value) == false && elem[i].id.indexOf("Txt_AdtDOB") > 0) {
                        //jAlert('Please enter Date of Birth in dd/MM/yyyy format.', 'Alert');
                        ErrorPopup("Please enter Date of Birth in dd/MM/yyyy format");
                        elem[i].focus();
                        return false;

                    }

                    //if (document.getElementById('ctl00_ContentPlaceHolder1_hdn_vc').value == "AK") {



                    //}
                }

                //Check if Repeater Child Exsists
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Child_ctl00_txtCFirstName')) {
                    for (var j = 0; j < elemSelect.length; j++) {

                        if (elemSelect[j].id.indexOf("ddl_CTitle") > 0) {

                            if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                                //alert('First Name can not be blank for Adult');2
                                //$("#dialog").dialog();
                                //jAlert('Please select the Title.', 'Alert');
                                ErrorPopup("Please select the Title");
                                elemSelect[j].focus();
                                return false;
                            }
                        }

                    }

                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtCFirstName") > 0) {
                            Child++;
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                //jAlert('First Name can not be blank for Child', 'Alert');
                                ErrorPopup("First Name can not be blank for Child");
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                //jAlert('First Name required atleast one characters for Child.', 'Alert');
                                ErrorPopup("First Name required atleast one characters for Child");
                                elem[i].focus();
                                return false;
                            }

                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtCLastName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "Last Name") {
                                //jAlert('Last Name can not be blank for Child', 'Alert');
                                ErrorPopup("Last Name can not be blank for Child");
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                                //jAlert('Last Name required atleast one characters for Child.', 'Alert');
                                ErrorPopup("Last Name required atleast one characters for Child");
                                elem[i].focus();
                                return false;
                            }


                        }
                        if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_chDOB") > 0) {
                            //jAlert('Age can not be blank for Child', 'Alert');
                            ErrorPopup("Age can not be blank for Child");
                            elem[i].focus();
                            return false;

                        }

                        if (elem[i].type == "text" && (elem[i].value != "") && pattern.test(elem[i].value) == false && elem[i].id.indexOf("Txt_chDOB") > 0) {
                            //jAlert('Please enter Date of Birth in dd/MM/yyyy format.', 'Alert');
                            ErrorPopup("Please enter Date of Birth in dd/MM/yyyy format");
                            elem[i].focus();
                            return false;

                        }

                    }
                }



                //Check if Repeater Infant Exsists ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName')) {

                    for (var j = 0; j < elemSelect.length; j++) {

                        if (elemSelect[j].id.indexOf("ddl_ITitle") > 0) {

                            if (elemSelect[j].value == "" || elemSelect[j].value == "Select") {
                                //alert('First Name can not be blank for Adult');3
                                //$("#dialog").dialog();
                                //jAlert('Please select the Title.', 'Alert');
                                ErrorPopup("Please select the Title");
                                elemSelect[j].focus();
                                return false;
                            }
                        }

                    }


                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtIFirstName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                //jAlert('First Name can not be blank for Infant', 'Alert');
                                ErrorPopup("First Name can not be blank for Infant");
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "First Name") {
                                //alert('First Name can not be blank for Adult');
                                //$("#dialog").dialog();
                                //jAlert('First Name required atleast one characters for infant.', 'Alert');
                                ErrorPopup("First Name required atleast one characters for infant");
                                elem[i].focus();
                                return false;
                            }

                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtILastName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "Last Name") {
                                //jAlert('Last Name can not be blank for Infant', 'Alert');
                                ErrorPopup("Last Name can not be blank for Infant");
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 1 && elem[i].value != "Last Name") {
                                //jAlert('Last Name required atleast one characters for infant', 'Alert');
                                ErrorPopup("Last Name required atleast one characters for infant");
                                elem[i].focus();
                                return false;
                            }

                        }
                        if (elem[i].type == "text" && (elem[i].value == "" || elem[i].value == "DOB") && elem[i].id.indexOf("Txt_InfantDOB") > 0) {
                            //jAlert('Age can not be blank for Infant', 'Alert');
                            ErrorPopup("Age can not be blank for Infant");
                            elem[i].focus();
                            return false;

                        }
                        if (elem[i].type == "text" && (elem[i].value != "") && pattern.test(elem[i].value) == false && elem[i].id.indexOf("Txt_InfantDOB") > 0) {
                            //jAlert('Please enter Date of Birth in dd/MM/yyyy format.', 'Alert');
                            ErrorPopup("Please enter Date of Birth in dd/MM/yyyy format");
                            elem[i].focus();
                            return false;

                        }

                    }
                }


                if ($("#ctl00_ContentPlaceHolder1_DropDownListProject option:selected").val() == "Select") {
                    //jAlert("Please Select Project Id", 'Alert');
                    ErrorPopup("Please Select Project Id");
                    $("#ctl00_ContentPlaceHolder1_DropDownListProject").focus();
                    return false;
                }


                if ($("#ctl00_ContentPlaceHolder1_DropDownListBookedBy option:selected").val() == "Select") {
                    ErrorPopup("Please Select Booked By");
                    $("#ctl00_ContentPlaceHolder1_DropDownListBookedBy").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").value == "First Name") {
                    ErrorPopup('Please Enter Primary Guest First Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").value == "Last Name") {
                    ErrorPopup('Please Enter Primary Guest Last Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value == "Email Id") {
                    ErrorPopup('Please Enter EmailID');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                    return false;
                }

                var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value;
                var matchArray = emailid.match(emailPat);
                if (matchArray == null) {
                    ErrorPopup("Your email address seems incorrect. Please try again.");
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "Mobile No") {
                    ErrorPopup('Please Enter Mobile No');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                    return false;
                }
                else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value != "" && $.trim(document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value).length < 10) {
                    ErrorPopup('Please Enter atleast 10 digit in Mobile');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                    return false;
                }
                ///////
                //if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value != "" || document.getElementById("ctl00_ContentPlaceHolder1_hdnCheckGST").value == "true") {
                //    if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value.length != 15) {
                //        jAlert('Please Enter valid GST NO', 'Alert');
                //        document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").focus();
                //        return false;
                //    }
                //    else {
                //        if (gStValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").value)) {

                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value == "") {
                //                jAlert('Please Enter Gst Company Name', 'Alert');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                //                return false;
                //            }
                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value.length > 35) {
                //                //jAlert('Please Enter Gst Company Name', 'Alert');
                //                alert('Company Name length should not more than 35 characters.\nPlease enter valid length');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                //                return false;
                //            }

                //            var iCharsadd = "!`@#$%^&*()+=-[]\\\';,.{}|\":<>?~_/";
                //            var data = document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value;
                //            for (var i = 0; i < data.length; i++) {
                //                if (iCharsadd.indexOf(data.charAt(i)) != -1) {
                //                    alert("Only alpha numeric is allowed in gst company name. \nPlease enter valid company name.");
                //                    //document.getElementById("txtCallSign").value = "";
                //                    return false;
                //                }
                //            }

                //            //var GstCmpName = document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").value;
                //            //if (alphanumeric(GstCmpName) == false) {
                //            //    alert("Only alpha numeric is allowed in gst company name. \nPlease enter valid company name.");
                //            //    document.getElementById("ctl00_ContentPlaceHolder1_txtGstCmpName").focus();
                //            //    return false;
                //            //}



                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value == "") {
                //                jAlert('Please Enter Gst Company Address', 'Alert');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                //                return false;
                //            }

                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value.length > 35) {
                //                alert('Address length should not more than 35 characters.\nPlease enter valid length');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                //                return false;
                //            }
                //            var AddressFormate1 = AddressValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").value);
                //            if (AddressFormate1 != "") {
                //                alert(AddressFormate1);
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstAddress").focus();
                //                return false;
                //            }

                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value == "") {
                //                jAlert('Please Enter Gst Pin Code', 'Alert');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").focus();
                //                return false;
                //            }
                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value.length > 7) {
                //                alert('Please enter valid Pin Code');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").focus();
                //                return false;
                //            }

                //            var PinCode = NumericValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtPinCode").value);
                //            if (PinCode != "") {
                //                alert(PinCode);
                //                return false;
                //            }



                //            if ($("#ctl00_ContentPlaceHolder1_ddlStateGst option:selected").val() == "select") {
                //                jAlert("Please Select GST State", 'Alert');
                //                $("#ctl00_ContentPlaceHolder1_ddlStateGst").focus();
                //                return false;
                //            }
                //            if ($("#ctl00_ContentPlaceHolder1_ddlCityGst option:selected").val() == "select") {
                //                jAlert("Please Select GST City", 'Alert');
                //                $("#ctl00_ContentPlaceHolder1_ddlCityGst").focus();
                //                return false;
                //            }


                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value == "") {
                //                jAlert('Please Enter Gst Phone No', 'Alert');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").focus();
                //                return false;
                //            }
                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value.length > 10) {
                //                alert('Contact length should not more than 10.\nPlease enter valid length');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").focus();
                //                return false;
                //            }
                //            var Contact = NumericValidate(document.getElementById("ctl00_ContentPlaceHolder1_txtGstPhone").value);
                //            if (Contact != "") {
                //                alert(Contact);
                //                return false;
                //            }

                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value == "") {
                //                jAlert('Please Enter Gst Email ID', 'Alert');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                //                return false;
                //            }
                //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value.length > 35) {
                //                alert('Email Id length should not more than 35.\nPlease enter valid length');
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                //                return false;
                //            }

                //            var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                //            var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").value;
                //            var matchArray = emailid.match(emailPat);
                //            if (matchArray == null) {
                //                alert("Email address seems incorrect. Please try again.");
                //                document.getElementById("ctl00_ContentPlaceHolder1_txtGstEmail").focus();
                //                return false;
                //            }
                //        }
                //        else {
                //            jAlert('Enter valid GST No.', 'Alert');
                //            document.getElementById("ctl00_ContentPlaceHolder1_txtGstNo").focus();
                //            return false;
                //        }
                //    }
                //}

                //////
                debugger;
                MealBagPriceAdd(Adult, Child);


                //jConfirm('Are you sure!', 'Confirmation');
                var t;

                if (btnclk == 0) {
                    // e.preventDefault();

                    //$('#ctl00_ContentPlaceHolder1_book').unbind('click').click();;
                    //document.getElementById("div_Submit").style.display = "none";
                    //document.getElementById("div_Progress").style.display = "block";

                    $("#ctl00_ContentPlaceHolder1_book").css("display", "none");
                    $("#btnBookLoader").css("display", "block");

                    btnclk = 1;
                    return true;


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


                //if () {

                //    document.getElementById("div_Submit").style.display = "none";
                //    document.getElementById("div_Progress").style.display = "block";
                //    return true;
                //}
                //else {
                //    return false;
                //}
                //if (t) {
                //    alert('ok');
                //}
            });
        });

        function ErrorPopup(errmsg) {
            debugger;
            $("#ErrBodyMsg").html("<h5 style='text-align:center;'>" + errmsg+"</h5>");
            $("#btnErrorPopup").click();
            return false;
        }

        function MealBagPriceAdd(Adult, Child) {

            ///// MEAL
            var A_ML_O = ""; var A_ML_I = "";
            var A_ML_O_txt = ""; A_ML_I_txt = "";
            var C_ML_O = ""; var C_ML_I = "";
            var C_ML_O_txt = ""; C_ML_I_txt = "";
            var MLAdult_f_O = parseFloat(0); MLChild_f_O = parseFloat(0);
            var MLAdult_f_I = parseFloat(0); MLChild_f_I = parseFloat(0);
            //BAGAGE

            var A_BG_O = ""; var A_BG_I = "";
            var A_BG_O_txt = ""; A_BG_I_txt = "";
            var C_BG_O = "", C_BG_I = "";
            var C_BG_O_txt = "", C_BG_I_txt = "";
            var BGAdult_f_O = parseFloat(0); BGChild_f_O = parseFloat(0);
            var BGAdult_f_I = parseFloat(0); BGChild_f_I = parseFloat(0);

            //Seat

            var A_SE_O = ""; var A_SE_I = "";
            var A_SE_O_txt = ""; A_SE_I_txt = "";
            var C_SE_O = ""; var C_SE_I = "";
            var C_SE_O_txt = ""; C_SE_I_txt = "";
            var SEAdult_f_O = parseFloat(0); SEChild_f_O = parseFloat(0);
            var SEAdult_f_I = parseFloat(0); SEChild_f_I = parseFloat(0);
            // //////////////////////////////Start AirAsia Meal Baggage varable declartion for second Segment 
            var A_ML_O_Seg2 = "", A_ML_I_Seg2 = "", A_ML_O_txt_Seg2 = "", A_ML_I_txt_Seg2 = "";
            var C_ML_O_Seg2 = "", C_ML_I_Seg2 = "", C_ML_O_txt_Seg2 = "", C_ML_I_txt_Seg2 = "";
            var MLAdult_f_O_Seg2 = parseFloat(0), MLChild_f_O_Seg2 = parseFloat(0), MLAdult_f_I_Seg2 = parseFloat(0), MLChild_f_I_Seg2 = parseFloat(0);

            var A_BG_O_Seg2 = "", A_BG_I_Seg2 = "", A_BG_O_txt_Seg2 = "", A_BG_I_txt_Seg2 = "";
            var C_BG_O_Seg2 = "", C_BG_I_Seg2 = "", C_BG_O_txt_Seg2 = "", C_BG_I_txt_Seg2 = "";
            var BGAdult_f_O_Seg2 = parseFloat(0), BGChild_f_O_Seg2 = parseFloat(0), BGAdult_f_I_Seg2 = parseFloat(0), BGChild_f_I_Seg2 = parseFloat(0);
            //////////////////////////////End AirAsia Meal Baggage varable declartion for second Segment 

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


                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_SE_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_SE_O = A_SE_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_SE_O_txt = A_SE_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_A_SE_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        A_SE_I = A_SE_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        A_SE_I_txt = A_SE_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }

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

                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_SE_Ob") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_SE_O = C_SE_O + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_SE_O_txt = C_SE_O_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
                    }
                }
                if (elemddl[i].type == "select-one" && elemddl[i].id.indexOf("Ddl_C_SE_Ib") > 0) {
                    if (elemddl[i].value != "" && elemddl[i].value != null && elemddl[i].value != "select") {
                        C_SE_I = C_SE_I + elemddl[i].value + "@" + elemddl[i].options[elemddl[i].selectedIndex].text.split("INR")[1] + "}";
                        C_SE_I_txt = C_SE_I_txt + elemddl[i].options[elemddl[i].selectedIndex].text + "}";
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

            if (A_SE_O != "") { SEAdult_f_O = CalcFare(A_SE_O_txt, SEAdult_f_O); }
            if (C_SE_O != "") { SEChild_f_O = CalcFare(C_SE_O_txt, SEChild_f_O); }

            //////Start AirAsia Beal Baggage for second Segment
            if (A_ML_O_Seg2 != "") { MLAdult_f_O_Seg2 = CalcFare(A_ML_O_txt_Seg2, MLAdult_f_O_Seg2); }
            if (C_ML_O_Seg2 != "") { MLChild_f_O_Seg2 = CalcFare(C_ML_O_txt_Seg2, MLChild_f_O_Seg2); }
            if (A_BG_O_Seg2 != "") { BGAdult_f_O_Seg2 = CalcFare(A_BG_O_txt_Seg2, BGAdult_f_O_Seg2); }
            if (C_BG_O_Seg2 != "") { BGChild_f_O_Seg2 = CalcFare(C_BG_O_txt_Seg2, BGChild_f_O_Seg2); }
            //////End AirAsia Meal Baggage for second Segment

            $("#<%= lbl_OB_TOT.ClientID %>").val(parseFloat(MLAdult_f_O + MLChild_f_O + BGAdult_f_O + BGChild_f_O));
            //}
            //INBOUND 
            //if (A_ML_I != "" || C_ML_I != "" || A_BG_I != "" || C_BG_I != "") {
            //UPdate Fare Display divFareDtlsR
            if (A_ML_I != "") { MLAdult_f_I = CalcFare(A_ML_I_txt, MLAdult_f_I); }
            if (C_ML_I != "") { MLChild_f_I = CalcFare(C_ML_I_txt, MLChild_f_I); }
            if (A_BG_I != "") { BGAdult_f_I = CalcFare(A_BG_I_txt, BGAdult_f_I); }
            if (C_BG_I != "") { BGChild_f_I = CalcFare(C_BG_I_txt, BGChild_f_I); }

            if (A_SE_I != "") { SEAdult_f_I = CalcFare(A_SE_I_txt, SEAdult_f_I); }
            if (C_SE_I != "") { SEChild_f_I = CalcFare(C_SE_I_txt, SEChild_f_I); }

            //////Start AirAsia Meal Baggage for second Segment
            if (A_ML_I_Seg2 != "") { MLAdult_f_I_Seg2 = CalcFare(A_ML_I_txt_Seg2, MLAdult_f_I_Seg2); }
            if (C_ML_I_Seg2 != "") { MLChild_f_I_Seg2 = CalcFare(C_ML_I_txt_Seg2, MLChild_f_I_Seg2); }
            if (A_BG_I_Seg2 != "") { BGAdult_f_I_Seg2 = CalcFare(A_BG_I_txt_Seg2, BGAdult_f_I_Seg2); }
            if (C_BG_I_Seg2 != "") { BGChild_f_I_Seg2 = CalcFare(C_BG_I_txt_Seg2, BGChild_f_I_Seg2); }
            //////End AirAsia Meal Baggage for second Segment

            //Result= Result+ Create_MealBag_Table(MLAdult_f_I,BGAdult_f_I,MLChild_f_I,BGChild_f_I,"IB");
            //Total = parseFloat(MLAdult_f_O + BGAdult_f_O + MLChild_f_O + BGChild_f_O + SEChild_f_O + SEChild_f_O) + parseFloat(MLAdult_f_I + BGAdult_f_I + MLChild_f_I + BGChild_f_I + SEChild_f_I + SEChild_f_I);
            Total = parseFloat(MLAdult_f_O + BGAdult_f_O + MLChild_f_O + BGChild_f_O + MLAdult_f_O_Seg2 + BGAdult_f_O_Seg2 + MLChild_f_O_Seg2 + BGChild_f_O_Seg2) + parseFloat(MLAdult_f_I + BGAdult_f_I + MLChild_f_I + BGChild_f_I + MLAdult_f_I_Seg2 + BGAdult_f_I_Seg2 + MLChild_f_I_Seg2 + BGChild_f_I_Seg2);
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

            $("#<%= lbl_A_MB_OB.ClientID %>").val(A_ML_O + "#" + A_BG_O + "#" + A_SE_O);
            $("#<%= lbl_A_MB_IB.ClientID %>").val(A_ML_I + "#" + A_BG_I + "#" + A_SE_I);
            $("#<%= lbl_C_MB_OB.ClientID %>").val(C_ML_O + "#" + C_BG_O + "#" + C_SE_O);
            $("#<%= lbl_C_MB_IB.ClientID %>").val(C_ML_I + "#" + C_BG_I + "#" + C_SE_I);
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

    <style type="text/css">
        .modal-header {
    /* border-radius: 5px; */
    /* color: #fff; */
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-align: start;
    -ms-flex-align: start;
    align-items: flex-start;
    -webkit-box-pack: justify;
    -ms-flex-pack: justify;
    /* justify-content: space-between; */
    padding: 1rem;
    border-bottom: 1px solid #e9ecef;
    border-top-left-radius: .3rem;
    border-top-right-radius: .3rem;
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
            if (charCode >= 48 && charCode <= 57 || charCode == 08 || charCode == 46) {
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
    </script>

    <button type="button" class="btn btn-primary" id="btnErrorPopup" style="display:none;" data-toggle="modal" data-target="#ErrorPopup"></button>
    <div class='modal fade' id='ErrorPopup' tabindex='-1' role='dialog' aria-labelledby='exampleModalLongTitle' aria-hidden='true'>
        <div class='modal-dialog modal-sm' style='margin-top: 10%; width: 30% !important;'>
            <div class='modal-content'>
                <div class="modal-header">
                    <h4 class="modal-title" style="color:red;"><i class='fa fa-exclamation-triangle'></i>&nbsp;Required Field !</h4>                    
                </div>
                <div class='modal-body' id="ErrBodyMsg"></div>
                <div class='modal-footer'><button type='button' class='btn btn-secondary' data-dismiss='modal'>Close</button>
                </div>
            </div>
        </div>
    </div>


    <div class="theme-page-section theme-page-section-lg">
        <div class="container">
            <div class="row row-col-static row-col-mob-gap" id="abcd" data-gutter="60">
                <div class="col-md-8">
                    <div class="theme-payment-page-sections">

                        <div class="row">
                            <div id="divFltDtls1" runat="server" style="margin-top: 0px; display: none;" class="bgs  large-12 medium-12 small-12 columns">
                            </div>

                        </div>
                        <div class="row">
                            <div id="ddhidebox" class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb">Ticket Details</span>
                                        <div class="padLR10 padT5 ico18 quicks fb" style="cursor: pointer; margin-top: 1px; color: #5d5d5d; padding: 5px; border-radius: 5px; float: right; margin-top: -4px; margin-right: 10px;"><i class="icofont-bubble-left"></i><span onclick="history.back(-1)" style="font-size: 15px;">Back to Result</span></div>
                                    </div>
                                </div>
                                <div id="divtotFlightDetails" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="row">


                            <div class="large-12 medium-12 small-12 columns">
                                <asp:Label ID="lbl_adult" runat="server" CssClass="hide"></asp:Label>
                                <asp:Label ID="lbl_child" runat="server" CssClass="hide"></asp:Label>
                                <asp:Label ID="lbl_infant" runat="server" CssClass="hide"></asp:Label>
                                <asp:HiddenField ID="lbl_A_MB_OB" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_A_MB_IB" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_C_MB_OB" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_C_MB_IB" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_OB_TOT" EnableViewState="true" runat="server" Value="0" />
                                <asp:HiddenField ID="lbl_IB_TOT" EnableViewState="true" runat="server" Value="0" />
                                <asp:HiddenField ID="TOT_OB_Fare" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="NET_OB_Fare" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="TOT_IB_Fare" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="NET_IB_Fare" EnableViewState="true" runat="server" />

                                <asp:HiddenField ID="lbl_A_MB_OB_Seg2" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_A_MB_IB_Seg2" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_C_MB_OB_Seg2" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="lbl_C_MB_IB_Seg2" EnableViewState="true" runat="server" />

                            </div>

                        </div>




                        <div class="row">
                            <div class="fd-l1" style="display: block; margin-top: 10px;">


                                <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10" id="abc">
                                    <div class="fl width100 padLR15 padTB10 borderBtm">
                                        <div class="fl"><span class="fl padLR10 padT5 ico18 quicks fb">Traveller Details</span></div>
                                    </div>

                                    <div id="tbl_Pax" runat="server" class="fl width100 padLR20 mobpadLR5 bkTraveller padT10">

                                        <div class="alert_msg info_msg fl"><b class="status_info fl">Note: </b><span class="status_cont">Please make sure you enter the Name as per your govt. photo id.</span></div>

                                        <div id="td_Adult" runat="server">
                                            <asp:Repeater ID="Repeater_Adult" runat="server" OnItemCreated="Repeater_Adult_ItemCreated">
                                                <ItemTemplate>



                                                    <div class="clear1"></div>
                                                    <div class="">
                                                        <div class="fl width100 padLR10 greyBg borderBtm marginB5">

                                                            <div class="fl width100 padTB10 curPoint">
                                                                <span class="fl padR5">
                                                                    <i class="fl ico24 icofont-ui-user icofont-2x"></i>
                                                                    <i class="fl fltCirc bordBlac ico8 pad1 marginLN5 icofont-check-circled"></i>

                                                                </span>
                                                                <span class="fl padLR20 ico14 padT5" id="pttextADT" runat="server"><%#DataBinder.Eval(Container.DataItem, "PaxTP")%></span>

                                                            </div>

                                                            <div class="row-col-gap">
                                                                <div class="padL30 fl width100">
                                                                    <div class="col-md-2">
                                                                        <div class="col-md-2 col-sm-3 col-xs-12 padT10 marginB5 padL20 stepLabel">
                                                                            <label class="ico14 grey">Adult Name</label>

                                                                        </div>

                                                                    </div>
                                                                    <div class="col-md-10">
                                                                        <div class="row row-col-gap" data-gutter="10">

                                                                            <div class="col-md-2 ">
                                                                                <div class="theme-payment-page-form-item form-group">
                                                                                    <i class="fa fa-angle-down"></i>
                                                                                    <asp:DropDownList ID="ddl_ATitle" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="">Title</asp:ListItem>
                                                                                        <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                                                        <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                                        <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>



                                                                            <div class="col-md-2" style="display: none;">
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_AGender" runat="server">
                                                                                    <asp:ListItem Value="" Selected="True">Gender</asp:ListItem>

                                                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                            <div class="col-md-4 ">
                                                                                <div class="theme-payment-page-form-item form-group">

                                                                                    <asp:TextBox ID="txtAFirstName" runat="server" value="First Name" CssClass="form-control"
                                                                                        onfocus="focusObj(this);" onblur="blurObj(this);" placeholder="First Name" autocomplete="off"
                                                                                        onkeypress="return isCharKey(event)"></asp:TextBox>

                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-2" style="display: none;">
                                                                                <asp:TextBox ID="txtAMiddleName" runat="server" value="Middle Name"
                                                                                    onfocus="focusObjM(this);" onblur="blurObjM(this);" placeholder="Middle Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                            </div>


                                                                            <div class="col-md-4">
                                                                                <div class="theme-payment-page-form-item form-group">

                                                                                    <asp:TextBox ID="txtALastName" runat="server" value="Last Name" CssClass="form-control"
                                                                                        onfocus="focusObj1(this);" onblur="blurObj1(this);" placeholder="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                                </div>
                                                                            </div>



                                                                            <div class="col-md-2">
                                                                                <div class="theme-payment-page-form-item form-group">

                                                                                    <asp:TextBox CssClass="adtdobcss form-control" value="DOB" ID="Txt_AdtDOB" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div id="adtreq" class="abreds red adtreq" style="display: none;">*</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div id="divFdAI" runat="server" class="hidden">

                                                        <div id="A_SG">
                                                            <div class="col-md-12" id="div_ADT" runat="server" style="display: none;">

                                                                <div class="" runat="server" id="Seg1_Ob_MealBaggage"></div>
                                                                <h3 class="theme-payment-page-form-title">OutBound:</h3>
                                                                <div class="row-col-gap" data-gutter="20">

                                                                    <div class="col-md-4 ">
                                                                        <div class="theme-payment-page-form-item form-group">
                                                                            <i class="fa fa-angle-down"></i>

                                                                            <asp:DropDownList ID="Ddl_A_Meal_Ob" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Selected="True">--Meal--</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Excess Baggage</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_A_EB_Ob" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-12" runat="server" id="Seg2_Ob" style="display: none;">
                                                                    <div class="clear"></div>
                                                                    <div class="large-12 medium-12 small-12 columns">
                                                                        <div class="large-3 medium-3 small-12 columns" runat="server" id="Seg2_Ob_MealBaggage"></div>
                                                                        <%--<div class="large-5 medium-3 small-12 columns"></div>--%>
                                                                        <%-- <div class="large-5 medium-5 small-5 columns large-push-2 medium-push-2">
                                                                        </div>--%>
                                                                    </div>
                                                                    <div class="clear"></div>
                                                                    <div class="col-md-4">
                                                                        <label>Meal</label>
                                                                        <asp:DropDownList ID="Ddl_A_Meal_Ob_Seg2" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>Excess Baggage</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_A_EB_Ob_Seg2" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div runat="server" id="tranchor1">




                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <h3 style="font-size: 12px; font-weight: 700;">OutBound:</h3>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <h3 runat="server" id='anchor1' class="" onclick="showhide(this,'anchor1','div1');" style="cursor: pointer; float: right; font-size: 12px; color: #353535; background: bisque; padding: 4px; border-radius: 5px">Meal Preference/Seat Preference/Frequent Flyer(Optional)<i class="fa fa-angle-down"></i></h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="A_ALL" runat="server">
                                                                <div id="div1" runat="server" style="display: none;">

                                                                    <div class="row row-col-gap" data-gutter="10">
                                                                        <div class="col-md-3">
                                                                            <label>Meal</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_AMealPrefer" runat="server">
                                                                                    <asp:ListItem Value="">--Select Meal--</asp:ListItem>
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
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <label>Seat</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_ASeatPrefer" runat="server">
                                                                                    <asp:ListItem Value="">--Select Seat--</asp:ListItem>
                                                                                    <asp:ListItem Value="">Any</asp:ListItem>
                                                                                    <asp:ListItem Value="W">Window</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-md-3">
                                                                            <label>Airline</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txt_AAirline" runat="server" value="Airline"
                                                                                    onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline" CssClass="Airlineval form-control"></asp:TextBox>

                                                                                <input type="hidden" id="hidtxtAirline_Dom" name="hidtxtAirline_Dom" runat="server" value="" />
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <label>Frequent Flier</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txt_ANumber" runat="server" MaxLength="10" value="Number" CssClass="form-control"
                                                                                    onfocus="focusObjNumber(this);" onblur="blurObjNumber(this);" defvalue="Airline Number" Width="147px"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>


                                                                    <div class="clear">
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div id="divFdAO" runat="server" class="hidden">
                                                        <div id="A_SG_IB">
                                                            <div id="div_ADT_Ib" class="col-md-12" runat="server" style="display: none">

                                                                <div class="col-md-12" style="padding-left: 10px;">
                                                                    <div class="large-4 medium-3 small-3 columns">
                                                                        <div class="large-6 medium-3 small-12 columns" runat="server" id="Seg1_Ib_MealBaggage"></div>
                                                                        <div class="large-3 medium-3 small-3  lft blue bld" style="padding-left: 2px;">
                                                                            <span style="color: #004b91; font-weight: 600;">InBound:-</span>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="col-md-12">


                                                                    <div class="col-md-4">
                                                                        <label>Meal</label>
                                                                        <asp:DropDownList ID="Ddl_A_Meal_Ib" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Excess Baggage</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_A_EB_Ib" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="large-12 medium-12 small-12">
                                                                    <div class="clear"></div>
                                                                    <div runat="server" id="Div_ADT_Seg2_Ib" class="col-md-12" style="display: none">
                                                                        <div class="col-md-4">
                                                                            <div runat="server" id="Seg2_Ib_MealBaggage"></div>
                                                                            <%--<div class="large-3 medium-3 small-3 columns"></div>--%>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>Meal</label>
                                                                            <asp:DropDownList ID="Ddl_A_Meal_Ib_Seg2" CssClass="form-control" runat="server">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>Excess Baggage</label>
                                                                            <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_A_EB_Ib_Seg2" runat="server">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>

                                                            <div class="row-col-gap" data-gutter="20">
                                                                <div runat="server" id="tranchor1_R" style="display: none">


                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <h3 style="font-size: 12px; font-weight: 700;">InBound:</h3>
                                                                        </div>
                                                                        <div class="col-md-8">
                                                                            <h3 runat="server" id='anchor1_R' onclick="showhide(this,'anchor1_R','div1_R');" class="cursorpointer" style="cursor: pointer; float: right; font-size: 12px; color: #353535; background: bisque; padding: 4px; border-radius: 5px;">Meal Preference/Seat Preference/Frequent Flyer</h3>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div id="A_ALL_R" runat="server" style="display: none;">
                                                                <div id="div1_R" runat="server" class="w100" style="display: none;">
                                                                    <div class="row row-col-gap" data-gutter="10">
                                                                        <div class="col-md-3">
                                                                            <label>Meal</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_AMealPrefer_R" runat="server">
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
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <label>Seat</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_ASeatPrefer_R" runat="server">
                                                                                    <asp:ListItem Value="">Any</asp:ListItem>
                                                                                    <asp:ListItem Value="W">Window</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <label>Flight</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txt_AAirline_R" runat="server" value="Airline"
                                                                                    onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline" CssClass="Airlineval form-control"></asp:TextBox>

                                                                                <input type="hidden" id="hidtxtAirline_R_dom" name="hidtxtAirline_R_dom" runat="server" value="" />
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <label>Flight No.</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txt_ANumber_R" runat="server" MaxLength="10" value="Number" CssClass="form-control"
                                                                                    onfocus="focusObjNumber(this);" onblur="blurObjNumber(this);" defvalue="Number"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <%--<div class="large-12 medium-12 small-12 columns">
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
                                                <asp:TextBox ID="txt_AAirline_R" runat="server" value="Airline"
                                                 onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline" CssClass="Airlineval_R"></asp:TextBox>
                                                <input type="hidden" id="hidtxtAirline_R_dom" name="hidtxtAirline_R_Dom" runat="server" value="" />
                                            </div>
                                            <div class="large-2 medium-2 small-4 columns">
                                                <asp:TextBox ID="txt_ANumber_R" runat="server" MaxLength="10"
                                                    value="Number" 
                                                    defvalue="Number"></asp:TextBox>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>--%>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                        <div id="td_Child" runat="server">
                                            <asp:Repeater ID="Repeater_Child" runat="server" OnItemCreated="Repeater_Child_ItemCreated">
                                                <ItemTemplate>
                                                    <div class="fl width100 padLR10 greyBg borderBtm marginB5">
                                                        <div class="fl width100 padTB10 curPoint">
                                                            <span class="fl padR5">
                                                                <i class="fl ico24 icofont-ui-user icofont-2x"></i>
                                                                <i class="fl fltCirc bordBlac ico8 pad1 marginLN5 icofont-check-circled"></i>

                                                            </span>
                                                            <span class="fl padLR20 ico14 padT5" id="pttextCHD" runat="server"><%#DataBinder.Eval(Container.DataItem, "PaxTP")%></span>
                                                        </div>


                                                        <div class="row-col-gap">
                                                            <div class="padL30 fl width100">
                                                                <div class="col-md-2">
                                                                    <div class="col-md-2 col-sm-3 col-xs-12 padT10 marginB5 padL20 stepLabel">
                                                                        <label class="ico14 grey">Child Name</label>

                                                                    </div>

                                                                </div>
                                                                <div class="col-md-10">
                                                                    <div class="row row-col-gap" data-gutter="10">
                                                                        <div class="col-md-2 ">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList ID="ddl_CTitle" runat="server" CssClass="form-control">
                                                                                    <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                                    <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txtCFirstName" runat="server" value="First Name" CssClass="form-control"
                                                                                    onfocus="focusObjCFName(this);" onblur="blurObjCFName(this);" defvalue="First Name"
                                                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-2" style="display: none;">
                                                                            <asp:TextBox ID="txtCMiddleName" runat="server" value="Middle Name" CssClass="form-control"
                                                                                onfocus="focusObjCMName(this);" onblur="blurObjCMName(this);" defvalue="Middle Name"
                                                                                onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txtCLastName" runat="server" value="Last Name" CssClass="form-control"
                                                                                    onfocus="focusObjCLName(this);" onblur="blurObjCLName(this);" defvalue="Last Name"
                                                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-2">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox CssClass="txtboxx chddobcss form-control" ID="Txt_chDOB" runat="server" value="DOB"> </asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div id="divFdCI" class="hidden" runat="server">
                                                        <div id="C_SG">

                                                            <div class="col-md-12" id="div_CHILD" runat="server" style="display: none;">
                                                                <div runat="server" id="Seg1_C_Ob_MealBaggage"></div>
                                                                <h3 class="theme-payment-page-form-title">OutBound:</h3>

                                                                <div class="row-col-gap" data-gutter="20">


                                                                    <div class="col-md-4">
                                                                        <div class="theme-payment-page-form-item form-group">
                                                                            <i class="fa fa-angle-down"></i>
                                                                            <label>Meal</label>
                                                                            <asp:DropDownList ID="Ddl_C_Meal_Ob" CssClass="form-control" runat="server">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <div class="theme-payment-page-form-item form-group">
                                                                            <i class="fa fa-angle-down"></i>
                                                                            <label>Excess Baggage</label>
                                                                            <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_C_EB_Ob" runat="server">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-md-12" runat="server" id="Seg2_C_Ob" style="display: none;">
                                                                    <div class="clear"></div>
                                                                    <div class="col-md-12">
                                                                        <div class="large-2 medium-3 small-2 columns" runat="server" id="Seg2_C_Ob_MealBaggage"></div>
                                                                        <%-- <div class="large-1 medium-3 small-2 columns"></div>--%>
                                                                        <%--<div class="large-4 medium-3 small-2 columns"></div>--%>
                                                                    </div>
                                                                    <div class="clear"></div>
                                                                    <div class="col-md-4">
                                                                        <label>Meal</label>
                                                                        <asp:DropDownList ID="Ddl_C_Meal_Ob_Seg2" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>Excess Baggage</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_C_EB_Ob_Seg2" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>


                                                            <div runat="server" id="tranchor2">




                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <h3 class="" style="font-size: 12px; font-weight: 700;">OutBound:</h3>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <h3 runat="server" id='anchor2' onclick="showhide(this,'anchor2','div2');" style="cursor: pointer; float: right; font-size: 12px; color: #353535; background: bisque; padding: 4px; border-radius: 5px; border: 1px solid #b7b7b7;">Meal Preference/Seat Preference</h3>
                                                                    </div>
                                                                </div>

                                                                <div id="C_ALL" runat="server">
                                                                    <div id="div2" runat="server" class="row row-col-gap" data-gutter="10" style="display: none;">
                                                                        <div class="col-md-4">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_CMealPrefer" runat="server">
                                                                                    <asp:ListItem Value="">---Select Meal---</asp:ListItem>
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
                                                                        </div>

                                                                        <div class="col-md-4">

                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_CSeatPrefer" runat="server">
                                                                                    <asp:ListItem Value="">---Select Seat---</asp:ListItem>
                                                                                    <asp:ListItem Value="">Any</asp:ListItem>
                                                                                    <asp:ListItem Value="W">Window</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <%--<div id="div2" runat="server" class="large-12 medium-12 small-12" style="display: none;">
                                                <div class="large-1 medium-1 small-2 columns">
                                                    Meal
                                                </div>
                                                <div class="large-2 medium-2 small-4 columns">
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_CMealPrefer" runat="server" Width="100px">
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
                                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_CSeatPrefer" runat="server">
                                                        <asp:ListItem Value="">Any</asp:ListItem>
                                                        <asp:ListItem Value="W">Window</asp:ListItem>
                                                        <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>

                                                    <div id="divFdCO" class="hidden" runat="server">
                                                        <div id="C_SG_IB">
                                                            <div id="div_CHILD_Ib" class="col-md-12" runat="server" style="display: none">

                                                                <div class="col-md-12">
                                                                    <div class="row">
                                                                        <span style="font-size12px; font-weight: 700;">InBound:</span>

                                                                    </div>
                                                                    <div class="large-2 medium-2 small-12 columns" runat="server" id="Seg1_C_Ib_MealBaggage"></div>


                                                                    <div class="row">

                                                                        <div class="col-md-4">
                                                                            <label>Meal</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>

                                                                                <asp:DropDownList ID="Ddl_C_Meal_Ib" CssClass="form-control" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>Excess Baggage</label>
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>

                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_C_EB_Ib" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-md-12" runat="server" id="Seg2_Ib" style="display: none">
                                                                    <div class="clear"></div>
                                                                    <div runat="server" id="Seg2_C_Ib_MealBaggage"></div>
                                                                    <div class="col-md-4">
                                                                        <label>Meal</label>
                                                                        <asp:DropDownList ID="Ddl_C_Meal_Ib_Seg2" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>Excess Baggage</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="Ddl_C_EB_Ib_Seg2" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>



                                                            <div id="tranchor2_R" runat="server" class="hide">
                                                                <div class="clear">
                                                                </div>
                                                                <div class="col-md-12" style="padding-left: 10px;">
                                                                    <span style="color: #004b91">OutBound:-</span>
                                                                </div>

                                                                <div style="clear: both">
                                                                </div>
                                                                <div class="w101" style="padding-top: 10px;">
                                                                    <a runat="server" id='anchor2_R' onclick="showhide(this,'anchor2_R','div2_R');" class="cursorpointer">Meal Preference/Seat Preference</a>
                                                                </div>
                                                            </div>
                                                            <div id="C_ALL_R" runat="server" style="display: none;">
                                                                <div id="div2_R" runat="server" class="col-md-12" style="display: none; padding-top: 10px;">
                                                                    <div class="col-md-4">
                                                                        <label>Meal</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_CMealPrefer_R" runat="server">
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

                                                                    <div class="col-md-4">
                                                                        <label>Seat</label>
                                                                        <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_CSeatPrefer_R" runat="server">
                                                                            <asp:ListItem Value="">Any</asp:ListItem>
                                                                            <asp:ListItem Value="W">Window</asp:ListItem>
                                                                            <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="large-3 medium-3 small-3 columns">
                                                                    </div>
                                                                </div>
                                                                <%--<div class="w101" style="padding-top: 10px;">
                                                    <div class="w10 lft">
                                                        Meal
                                                    </div>
                                                    <div class="w15 lft">
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
                                                    <div class="w5 lft">
                                                        Seat
                                                    </div>
                                                    <div class="w10 lft">
                                                        <asp:DropDownList CssClass="txtdd1" ID="ddl_CSeatPrefer_R" runat="server">
                                                            <asp:ListItem Value="">Any</asp:ListItem>
                                                            <asp:ListItem Value="W">Window</asp:ListItem>
                                                            <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="clear1">
                                                    </div>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                        <div id="td_Infant" runat="server">
                                            <asp:Repeater ID="Repeater_Infant" runat="server">
                                                <ItemTemplate>
                                                    <div class="fl width100 padLR10 greyBg borderBtm marginB5">

                                                        <div class="fl width100 padTB10 curPoint">
                                                            <span class="fl padR5">
                                                                <i class="fl ico24 icofont-ui-user icofont-2x"></i>
                                                                <i class="fl fltCirc bordBlac ico8 pad1 marginLN5 icofont-check-circled"></i>

                                                            </span>
                                                            <span class="fl padLR20 ico14 padT5" id="pttextINF" runat="server"><%#DataBinder.Eval(Container.DataItem, "PaxTP")%></span>
                                                        </div>


                                                        <div class="row-col-gap" data-gutter="20">
                                                            <div class="padL30 fl width100">
                                                                <div class="col-md-2">
                                                                    <div class="col-md-2 col-sm-3 col-xs-12 padT10 marginB5 padL20 stepLabel">
                                                                        <label class="ico14 grey">Infant Name</label>

                                                                    </div>

                                                                </div>
                                                                <div class="col-md-10">
                                                                    <div class="row row-col-gap" data-gutter="10">
                                                                        <div class="col-md-2">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <i class="fa fa-angle-down"></i>
                                                                                <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_ITitle" runat="server">
                                                                                    <asp:ListItem Value="">Title</asp:ListItem>
                                                                                    <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                                    <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txtIFirstName" runat="server" value="First Name" CssClass="form-control"
                                                                                    onfocus="focusObjIFName(this);" onblur="blurObjIFName(this);" defvalue="First Name"
                                                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3" style="display: none;">
                                                                            <asp:TextBox ID="txtIMiddleName" runat="server" value="Middle Name"
                                                                                onfocus="focusObjIMName(this);" onblur="blurObjIMName(this);" defvalue="Middle Name"
                                                                                onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox ID="txtILastName" runat="server" value="Last Name" CssClass="form-control"
                                                                                    onfocus="focusObjILName(this);" onblur="blurObjILName(this);" defvalue="Last Name"
                                                                                    onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-md-2">
                                                                            <div class="theme-payment-page-form-item form-group">
                                                                                <asp:TextBox CssClass="txtboxx infdobcss form-control" value="DOB" ID="Txt_InfantDOB" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                    </div>





                                    <!--Contact Information-->
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-3 col-xs-12 padT10 marginB5 stepLabel">
                                            <label class="ico14 grey">Email Address</label>

                                        </div>
                                        <div class="col-md-10 col-sm-9 col-xs-12 pad0">
                                            <div class="col-md-6 col-sm-7 col-xs-12 marginB10 posRel">
                                                <asp:TextBox ID="txt_Email" value="Email Id" onfocus="focusObjPE(this);" CssClass="form-control" onblur="blurObjPE(this);" defvalue="Email Id" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-sm-5 col-xs-12 mobdn padT10">
                                                <span class="ico12 greyLt">Your ticket will be sent to this email address</span>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-3 col-xs-12 padT10 marginB5 stepLabel">
                                            <label class="ico14 grey">Mobile Number</label>
                                        </div>
                                        <div class="col-md-10 col-sm-9 col-xs-12 pad0">
                                            <div class="col-md-6 col-sm-7 col-xs-12 marginB10">
                                                <div class="col-md-3 col-sm-3 col-xs-3 pad0 posRel">
                                                    <div class="form-control posAbs inputMedium"><span class="fl">+91</span><i class="icon-arrow-down txtgrey ico13 lh1-2 padT5 fr"></i></div>
                                                    <select id="mobilecode" data-field="mobilecode" data-name="mobilecode" data-fieldname="Mobile Code" name="mobilecode" class="form-control inputMedium" style="opacity: 0;">
                                                        <option value="+91" selected="">+91</option>
                                                    </select>
                                                </div>
                                                <div class="col-md-9 col-sm-9 col-xs-9 pad0">
                                                    <asp:TextBox ID="txt_MobNo" CssClass="form-control" runat="server" value="Mobile No" onfocus="focusObjPM(this);"
                                                        onblur="blurObjPM(this);" defvalue="Mobile No" onkeypress="return isNumberKey(event)"
                                                        MaxLength="12"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="large-12 medium-12 small-12">
                                            <div class="large-3 medium-3 small-12 columns lft">
                                                <span id="spn_Projects" runat="server">Project Id</span>
                                            </div>
                                            <div class="large-3 medium-3 small-12 columns lft">
                                                <span id="spn_Projects1" runat="server">
                                                    <asp:DropDownList CssClass="txtdd1" ID="DropDownListProject" runat="server">
                                                    </asp:DropDownList>
                                                </span>
                                            </div>
                                            <div class="large-3 medium-3 small-12 columns lft">
                                                <span id="Spn_BookedBy" runat="server">Booked By </span>
                                            </div>
                                            <div class="large-3 medium-3 small-12 columns lft">
                                                <span id="Spn_BookedBy1" runat="server">
                                                    <asp:DropDownList CssClass="txtdd1" ID="DropDownListBookedBy" runat="server">
                                                    </asp:DropDownList>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2" style="display: none;">
                                                    <asp:DropDownList CssClass="txtdd1 form-control" ID="ddl_PGTitle" runat="server">

                                                        <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                        <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                        <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2" style="display: none;">
                                                    <asp:TextBox ID="txt_PGFName" runat="server" value="First Name" CssClass="form-control"
                                                        onfocus="focusObjPF(this);" onblur="blurObjPF(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                </div>
                                                <%-- <div class="abreds red">
                                                    *
                                                </div>--%>
                                                <div class="col-md-2" style="display: none;">
                                                    <asp:TextBox ID="txt_PGLName" runat="server" value="Last Name" CssClass="form-control"
                                                        onfocus="focusObjPL(this);" onblur="blurObjPL(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                </div>
                                                <%--  <div class="abreds red">
                                                    *
                                                </div>--%>


                                                <div class="row-col-gap" data-gutter="20">
                                                    <div class="">
                                                        <div class="row-col-gap" data-gutter="10">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Contact Information End-->
                                </div>



                                <%--Seat Selection Start--%>
                                <div class="card" style="display: none;">
                                    <div class="card-body">
                                        <div class="header-title">Seat Selection </div>
                                        <div class="row">
                                            <div class="clear1"></div>
                                            <div class="col-lg-12 col-sm-12 col-xs-12">
                                                <div class="col-md-6 col-xs-6">
                                                    <div class="col-md-12 col-xs-12 bld p10" id="InBoundFTseat_ibSec1" runat="server"></div>
                                                    <input type="hidden" id="seatSelect" name='seatSelect' runat="server" />
                                                </div>
                                                <div class="col-md-6 col-xs-6" id="InBoundFTseat_ibSec2" runat="server">
                                                </div>
                                                <div class="w100 lft" style="">
                                                    <div id="SeatDetails" class=""></div>
                                                </div>
                                                <div class="w100 lft" style="" id="InBoundFTSeat_ibDt" runat="server">
                                                    <input type="hidden" id="SeatDetails_ibDtls" name='SeatDetails_ibDtls' runat="server" />
                                                    <div id="SeatDetails_ib" class="p10" runat="server"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Seat Selection End--%>



                                <%--GST DETAILS START by Saurav--%>


                                <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10" runat="server" id="gstfd">

                                    <div class="fl width100 padLR15 padTB10 borderBtm">
                                        <div class="fl">
                                            <span class="fl padLR10 padT5 ico18 quicks fb">Enter GST Details (Optional)</span><label class="toggle-control" style="margin-top: 12px;">
                                                <input type="checkbox" id="chkGST"></label>
                                        </div>
                                    </div>



                                    <div class="theme-payment-page-form _mb-20" id="divGstDetails" runat="server">
                                        <div id="dvGST" style="display: none;">
                                            <div class="row-col-gap" data-gutter="20">

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtGstNo" runat="server" CssClass="form-control" placeholder="GST No" MaxLength="15" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtGstCmpName" CssClass="form-control" runat="server" placeholder="Company Name" MaxLength="25" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtGstAddress" CssClass="form-control" runat="server" placeholder="GST Address" MaxLength="30" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz/1234567890');"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" placeholder="GST Pin Code" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="8"></asp:TextBox>
                                                    </div>
                                                </div>





                                                <asp:HiddenField ID="GSTStateHid" runat="server" Value="Select State" />
                                                <asp:HiddenField ID="GSTCityHid" runat="server" Value="Select City" />

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <i class="fa fa-angle-down"></i>
                                                        <select id="ddlStateGst" class="form-control" runat="server" style="color: #000 !important;">
                                                            <option value="1">--Select State--</option>
                                                        </select>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <i class="fa fa-angle-down"></i>
                                                        <select id="ddlCityGst" class="form-control" runat="server" style="color: #000 !important;">
                                                            <option value="1">--Select City--</option>
                                                        </select>
                                                        <div style="border-image: none; left: 105px; top: -35px; width: 36px; display: none; position: relative;" id="Select1Loding" runat="server">
                                                            <img src="../Images/load.gif" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtGstPhone" CssClass="form-control" runat="server" placeholder="GST Phone" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 ">
                                                    <label class="bld1"></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <asp:TextBox ID="txtGstEmail" runat="server" CssClass="form-control" placeholder="GST Email"></asp:TextBox>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="row-col-gap" data-gutter="20">

                                                <div class="col-sm-3">
                                                    <label></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <a href="#" class="btn _tt-uc btn-primary-inverse btn-block" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#TravGSTDetailModal">Select GST</a>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <label></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <span class="btn _tt-uc btn-primary-inverse btn-block" id="btnSaveTravelGst" onclick="return SaveGstOnTravel();">Save Gst</span>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <label></label>
                                                    <div class="theme-payment-page-form-item form-group">
                                                        <span class="btn _tt-uc btn-primary-inverse btn-block" id="ResetNewGST" onclick="RestratNewGST();">New GST</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="modal fade" id="TravGSTDetailModal" role="dialog" style="background: #cccccc78;">
                                    <div class="modal-dialog modal-lg">

                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">GST Details</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-12" style="border: 1px solid #ccc; width: 96%; margin-left: 13px; padding: 5px; margin-top: 4px; margin-bottom: 8px;">
                                                        <div class="row">
                                                            <div class="col-sm-12" style="border-bottom: 1px solid #ccc; padding: 3px; background: #2c529d; font-size: 14px; margin-top: -5px;">
                                                                <div class="col-sm-4">
                                                                    <label style="color: #fff">Company Name</label>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label style="color: #fff">GST No</label>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label style="color: #fff">Company Address</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="BindTravelGST">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                </div>
                                <%--GST DETAILS End by Saurav--%>
                                <!--Remarks-->
                                <div class="row" style="background-color: #fff; display: none;">
                                    <div class="large-10 medium-10 small-12 headbgs"><i class="fa fa-pencil-square-o" aria-hidden="true" style="background: #337ab7; padding: 5px; border-radius: 5px; color: white; padding-bottom: 2px;"></i>Remarks</div>
                                    <div class="clear1"></div>
                                    <div class="large-12 medium-12 small-12 ">
                                        <textarea rows="2" cols="50" placeholder="Remarks" id="txtRemarks" runat="server">
                                </textarea>

                                    </div>
                                </div>
                                <!--Remarks-->






                            </div>


                            <div id="div_Submit" class="col-md-12 col-xs-12 col-sm-12" style="margin-top: 15px;">
                                <asp:Button ID="book" runat="server" CssClass="btn btn-danger" Text="Continue" Style="float: right; width: 165px;" />

                                <button class="btn btn-danger" id="btnBookLoader" style="float: right; width: 165px; display: none;" disabled>Processing...<i class="fa fa-spinner fa-pulse"></i></button>
                            </div>
                        </div>










                    </div>


                </div>

                <div class="col-md-4">
                    <div class="sticky-col" style="">
                        <div id="divtotalpax" runat="server" style="padding: 0px; width: 100%; float: right; position: relative;">
                        </div>
                        <div style="background-color: #fff;">
                            <div id="div_fareddDetails" class="bgs">
                                <div id="div_fare" runat="server" class="lft w100">
                                </div>
                                <div id="div_fareR" runat="server" class="lft w100">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div id="dialog" title="Alert" style="border:2px solid #ddd !important; font-size:12px !important;">
  <p style="font-size:12px !important;">This is the default dialog which is useful for displaying information. The dialog window can be moved, resized and closed with the 'x' icon.</p>
</div>--%>

    <div id="toPopup1" class="" style="z-index: 1000; border: none; margin: 0px; padding: 0px; width: 100%; height: 100%; top: 0px; left: 0px; background-color: rgb(0, 0, 0 ,0.8); position: fixed; display: none">
        <div id="toPopup">
            <div class="close"></div>
            <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
            <div id="popup_content w100">
                <div id="SeatSource"></div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdn_vc" runat="server" />
    <asp:HiddenField ID="hdnCheckGST" runat="server" />
    <asp:HiddenField ID="OBTrackIds" runat="server" />
    <asp:HiddenField ID="IBTrackIds" runat="server" />
    <asp:HiddenField ID="OBValidatingCarrier" runat="server" />
    <asp:HiddenField ID="IBValidatingCarrier" runat="server" />

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
        $(function () {
            $("#chkGST").click(function () {
                if ($(this).is(":checked")) {
                    // $("#dvGST").show('fast');
                    $("#dvGST").css("display", "block");

                } else {
                    $("#dvGST").css("display", "none");

                }

            });
        });
    </script>

    <script type="text/javascript" src="../js/chrome.js"></script>

    <script type="text/javascript">
        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg

        }
    </script>

    <%--    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.draggable.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>--%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.draggable.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/SeatMap1.3.js?v=1.1")%>"></script>
    <script type="text/javascript">
        var d = new Date();

        $(function () { var d = new Date(); var dd = new Date(1952, 01, 01); $(".adtdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: ('1920:' + (d.getFullYear() - 12) + ''), navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true, defaultDate: dd }) });
        $(function () { $(".chddobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '-1y', minDate: '-12y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
        $(function () { $(".infdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '+0y', minDate: '-2y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_ddl_ATitle").change(function () {
                $("#ctl00_ContentPlaceHolder1_ddl_PGTitle").val($("#ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_ddl_ATitle").val());
            });

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
            });

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
                    }
                });
            });

            var aircode = $('.Airlineval_R').each(function () {
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
                    }
                });
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
        //function AddressValidate(Address) {
        //    var gstAddValid = "";

        //    var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
        //    var data = Address;
        //    for (var i = 0; i < data.length; i++) {
        //        if (iChars.indexOf(data.charAt(i)) != -1) {
        //            gstAddValid = "Only alpha numeric is allowed. \nPlease enter valid input.";
        //            //document.getElementById("txtCallSign").value = "";
        //            //return false;
        //        }
        //    }
        //    return gstAddValid;

        //}
        //function NumericValidate(Address) {
        //    var gstAddValid = "";

        //    var iCharscon = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
        //    var data = $.trim(Address);
        //    for (var i = 0; i < data.length; i++) {
        //        if (iCharscon.indexOf(data.charAt(i)) != -1) {
        //            gstAddValid = "Only numeric is allowed. \nPlease enter valid number.";
        //            //document.getElementById("txtCallSign").value = "";
        //            //return false;
        //        }
        //    }

        //    return gstAddValid;

        //}
        //function CharValidate(Address) {
        //    var gstAddValid = "";

        //    var iCharscon = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_1234567890";
        //    var data = $.trim(Address);
        //    for (var i = 0; i < data.length; i++) {
        //        if (iCharscon.indexOf(data.charAt(i)) != -1) {
        //            gstAddValid = "Only character is allowed. \nPlease enter valid number.";
        //            //document.getElementById("txtCallSign").value = "";
        //            // return false;
        //        }
        //    }

        //    return gstAddValid;

        //}
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
    </script>
    <script type="text/javascript">
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


    <script type="text/javascript">
        $("#hide").click(function () {
            $("p").hide();
        });

        $("#show").click(function () {
            $("p").show();
        });
    </script>

    <%--Coded By Saurav Kumar--%>
    <script type="text/javascript">
        function SaveGstOnTravel() {
            var agencyid = "<%=Session("UID")%>";

            var GSTNo = $("#<%=txtGstNo.ClientID%>").val();
            if (GSTNo.trim() == "") { alert("Please enter gst number"); $("#<%=txtGstNo.ClientID%>").focus(); return false; }

            var CompanyName = $("#<%=txtGstCmpName.ClientID%>").val();
            if (CompanyName.trim() == "") { alert("Please enter company name"); $("#<%=txtGstCmpName.ClientID%>").focus(); return false; }

            var CompanyAddress = $("#<%=txtGstAddress.ClientID%>").val();
            if (CompanyAddress.trim() == "") { alert("Please enter company address"); $("#<%=txtGstAddress.ClientID%>").focus(); return false; }

            var GSTPinCode = $("#<%=txtPinCode.ClientID%>").val();
            if (GSTPinCode.trim() == "") { alert("Please enter gst pin code"); $("#<%=txtPinCode.ClientID%>").focus(); return false; }

            var GSTState = $("#<%=ddlStateGst.ClientID%> option:selected").val();
            if (GSTState.toLowerCase() == "select" || GSTState == "0") { alert("Please select gst state"); $("#<%=ddlStateGst.ClientID%>").focus(); return false; }

            var GSTCity = $("#<%=ddlCityGst.ClientID%>  option:selected").val();
            if (GSTCity.toLowerCase() == "select" || GSTCity == "0") { alert("Please select gst city"); $("#<%=ddlCityGst.ClientID%>").focus(); return false; }

            var GSTPhone = $("#<%=txtGstPhone.ClientID%>").val();
            if (GSTPhone.trim() == "") { alert("Please enter gst phone"); $("#<%=txtGstPhone.ClientID%>").focus(); return false; }

            var GSTEmail = $("#<%=txtGstEmail.ClientID%>").val();
            if (GSTEmail.trim() == "") { alert("Please enter gst email"); $("#<%=txtGstEmail.ClientID%>").focus(); return false; }

            $("#btnSaveTravelGst").html("Please wait...<i class='fa fa-pulse fa-spinner'></i>");

            $.ajax({
                type: "Post",
                url: UrlBase + "FareRuleService.asmx/SaveGSTDeatil",
                data: '{agencyid: ' + JSON.stringify(agencyid) + ',gstno: ' + JSON.stringify(GSTNo) + ',comname: ' + JSON.stringify(CompanyName) + ',comadd: ' + JSON.stringify(CompanyAddress) + ',pin: ' + JSON.stringify(GSTPinCode) + ',state: ' + JSON.stringify(GSTState) + ',city: ' + JSON.stringify(GSTCity) + ',phone: ' + JSON.stringify(GSTPhone) + ',email: ' + JSON.stringify(GSTEmail) + '}',
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    if (data.d[0] == "true") {
                        $("#btnSaveTravelGst").css("display", "none");
                        $("#BindTravelGST").html("");
                        $("#BindTravelGST").html(data.d[1]);
                        alert("GST Details has been inserted successfully.");
                    }
                    else if (data.d[0] == "error") {
                        window.location.href = "/";
                    }
                    else {
                        alert("Failed !");
                    }
                    $("#btnSaveTravelGst").html("Save GST");
                }
            });

        }
        BindTravGSTDetils();
        function BindTravGSTDetils() {
            var agencyid = "<%=Session("UID")%>";
            var gstid = "";
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: UrlBase + "FareRuleService.asmx/GetAgencyGSTbyAgentId",
                data: '{agencyid: ' + JSON.stringify(agencyid) + ',gstid: ' + JSON.stringify(gstid) + '}',
                datatype: "json",
                success: function (data) {
                    if (data.d != null) {
                        $("#BindTravelGST").html("");
                        $("#BindTravelGST").html(data.d);
                    }
                },
                failure: function (response) {
                    alert("failed");
                }
            });
        }

        $(document.body).on('change', "input[type=radio][name=gstCompany]", function (e) {
            var gstid = $(this).val();
            var agencyid = "<%=Session("UID")%>";

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: UrlBase + "FareRuleService.asmx/GetTravGstByGstId",
                data: '{agencyid: ' + JSON.stringify(agencyid) + ',gstid: ' + JSON.stringify(gstid) + '}',
                datatype: "json",
                success: function (data) {
                    $("#<%=txtGstNo.ClientID%>").val(data.d[0]);
                    $("#<%=txtGstCmpName.ClientID%>").val(data.d[1]);
                    $("#<%=txtGstAddress.ClientID%>").val(data.d[2]);
                    $("#<%=txtPinCode.ClientID%>").val(data.d[3]);
                    $("#<%=ddlStateGst.ClientID%>").val(data.d[4]).change();
                    setTimeout(function () {
                        $("#<%=ddlCityGst.ClientID%>").val(data.d[5]);
                        $("#<%=txtGstPhone.ClientID%>").val(data.d[6]);
                        $("#<%=txtGstEmail.ClientID%>").val(data.d[7]);
                        $("#btnSaveTravelGst").css("display", "none");
                        $(".close").click();

                    }, 1000);
                    $("#ResetNewGST").css("display", "block");

                    setTimeout(function () { ResetRadioButtonClick(); }, 2000);
                },
                failure: function (response) {
                    alert("failed");
                }
            });
        });

        function ResetRadioButtonClick() {
            $("input[type=radio][name=gstCompany]").each(function () {
                var gstid = $(this).val();
                $("#GSTid_" + gstid).prop("checked", false);
            });
        }

        function RestratNewGST() {
            $("#<%=txtGstNo.ClientID%>").val('');
            $("#<%=txtGstCmpName.ClientID%>").val('');
            $("#<%=txtGstAddress.ClientID%>").val('');
            $("#<%=txtPinCode.ClientID%>").val('');
            $("#<%=ddlStateGst.ClientID%>").val('');
            $("#<%=ddlCityGst.ClientID%>").val('');
            $("#<%=txtGstPhone.ClientID%>").val('');
            $("#<%=txtGstEmail.ClientID%>").val('');
            $("#btnSaveTravelGst").css("display", "block");
            $("#ResetNewGST").css("display", "none");
        }
    </script>


    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>
</asp:Content>
