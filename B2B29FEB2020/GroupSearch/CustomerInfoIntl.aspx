<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="CustomerInfoIntl.aspx.cs" Inherits="GroupSearch_CustomerInfoIntl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/chrome.js"></script>
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

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
            $("#ctl00_ContentPlaceHolder1_book").click(function (e) {
                var elem = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("input");
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
                        else if ($.trim(elem[i].value).length < 2 && elem[i].value != "First Name") {
                            jAlert('First Name required atleast two characters for Adult.', 'Alert');
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
                        else if ($.trim(elem[i].value).length < 2 && elem[i].value != "Last Name") {
                            jAlert('Last Name required atleast two characters.', 'Alert');
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
                        if (elem[i].value == "" || elem[i].value == "Passport No") {
                            jAlert('Passport No can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Adl") > 0) {
                        Adult++;
                        if (elem[i].value == "" || elem[i].value == "Nationality") {
                            jAlert('Nationality can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Adl") > 0) {
                        Adult++;
                        if (elem[i].value == "" || elem[i].value == "Issuing Country") {
                            jAlert('Issuing Country can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Adl") > 0) {
                        Adult++;
                        if (elem[i].value == "" || elem[i].value == "Ex Date") {
                            jAlert('Passport Expring Date can not be blank for Adult', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                }
                //Check if Repeater Child Exsists
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Child_ctl00_txtCFirstName')) {
                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtCFirstName") > 0) {
                            Child++;
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                jAlert('First Name can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 2 && elem[i].value != "First Name") {
                                jAlert('First Name required atleast two characters for Child.', 'Alert');
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
                            else if ($.trim(elem[i].value).length < 2 && elem[i].value != "Last Name") {
                                jAlert('Last Name required atleast two characters for Child.', 'Alert');
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
                            if (elem[i].value == "" || elem[i].value == "Passport No") {
                                jAlert('Passport No can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Chd") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Nationality") {
                                jAlert('Nationality can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Chd") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Issuing Country") {
                                jAlert('Issuing Country can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Chd") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Ex Date") {
                                jAlert('Passport Expring Date can not be blank for Child', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                    }
                }
                //Check if Repeater Infant Exsists
                if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName')) {
                    for (var i = 0; i < elem.length; i++) {
                        if (elem[i].type == "text" && elem[i].id.indexOf("txtIFirstName") > 0) {
                            if (elem[i].value == "" || elem[i].value == "First Name") {
                                jAlert('First Name can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                            else if ($.trim(elem[i].value).length < 2 && elem[i].value != "First Name") {
                                jAlert('First Name required atleast two characters for infant.', 'Alert');
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
                            else if ($.trim(elem[i].value).length < 2 && elem[i].value != "Last Name") {
                                jAlert('Last Name required atleast two characters for infant', 'Alert');
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
                            if (elem[i].value == "" || elem[i].value == "Passport No") {
                                jAlert('Passport No can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_Nationality_Inf") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Nationality") {
                                jAlert('Nationality can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }

                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_IssuingCountry_Inf") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Issuing Country") {
                                jAlert('Issuing Country can not be blank for Infand', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                        if (elem[i].type == "text" && elem[i].id.indexOf("txt_ex_date_Inf") > 0) {
                            Adult++;
                            if (elem[i].value == "" || elem[i].value == "Ex Date") {
                                jAlert('Passport Expring Date can not be blank for Infant', 'Alert');
                                elem[i].focus();
                                return false;
                            }
                        }
                    }
                }
                //if ($("#ctl00_ContentPlaceHolder1_DropDownListProject option:selected").val() == "Select") {
                //    jAlert("Please Select Project Id", 'Alert');
                //    $("#ctl00_ContentPlaceHolder1_DropDownListProject").focus();
                //    return false;
                //}
                //if ($("#ctl00_ContentPlaceHolder1_DropDownListBookedBy option:selected").val() == "Select") {
                //    jAlert("Please Select Booked By", 'Alert');
                //    $("#ctl00_ContentPlaceHolder1_DropDownListBookedBy").focus();
                //    return false;
                //}
                //if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").value == "") {
                //    jAlert('Please Enter Primary Guest First Name', 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").focus();
                //    return false;
                //}
                //if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").value == "") {
                //    jAlert('Please Enter Primary Guest Last Name', 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").focus();
                //    return false;
                //}

                //if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value == "") {
                //    jAlert('Please Enter EmailID', 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                //    return false;
                //}
                //var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                //var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value;
                //var matchArray = emailid.match(emailPat);
                //if (matchArray == null) {
                //    jAlert("Your email address seems incorrect. Please try again.", 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                //    return false;
                //}
                //if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "Mobile No") {
                //    jAlert('Please Enter Mobile No', 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                //    return false;
                //}
                //else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value != "" && $.trim(document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value).length < 10 == "Mobile No") {
                //    jAlert('Please Enter atleast 10 digit in Mobile', 'Alert');
                //    document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                //    return false;
                //}
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
                if (btnclk == 0) {
                    e.preventDefault();
                    jConfirm('Are you sure!', 'Confirmation', function (r) {
                        if (r) {
                            $('#ctl00_ContentPlaceHolder1_book').unbind('click').click();;
                            document.getElementById("div_Submit").style.display = "none";
                            document.getElementById("div_Progress").style.display = "block";
                            btnclk = 1;
                        }
                        else {
                            btnclk == 0;
                        }
                    });
                }
                else {
                    btnclk = 0;
                    return false;
                }
            });
        });
    </script>
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58 || charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
    </script>
    <link type="text/css" href="../styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" />
    <div style="margin-top: 30px;"></div>
    <div class="large-10 medium-10 small-12 large-push-1 medium-push-1">
        <div class="large-12 medium-12 small-12">
            <div id="divFltDtls1" runat="server" class="large-9 medium-9 small-12 columns" style="padding-top: 10px;">
            </div>
            <div id="divtotalpax" runat="server" class="large-3 medium-3 small-12 columns" style="padding-top: 10px;">
            </div>
        </div>
        <div class="clear"></div>
        <div class="large-12 medium-12 small-12">
            <div id="tbl_Pax" runat="server" class="w100">
                <div id="td_Adult" runat="server">
                    <asp:Repeater ID="Repeater_Adult" runat="server">
                        <ItemTemplate>
                            <div class="row">
                                <div class="large-10 medium-10 small-12 columns bld">
                                    <asp:Label ID="pttextADT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>
                                </div>
                                <div class="clear1"></div>
                                <div class="large-12 medium-12 small-12">
                                    <div class="large-1 medium-1 small-3 columns">
                                        <asp:DropDownList ID="ddl_ATitle" runat="server">
                                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                            <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                            <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="large-2 medium-2 small-8 columns">
                                        <asp:TextBox ID="txtAFirstName" runat="server" value="First Name"
                                            onfocus="focusObj(this);" onblur="blurObj(this);" defvalue="First Name" autocomplete="off"
                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-2 small-12 passenger columns ">
                                        <asp:TextBox ID="txtAMiddleName" runat="server" value="Middle Name"
                                            onfocus="focusObjM(this);" onblur="blurObjM(this);" defvalue="Middle Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-2 medium-2 small-11 columns large-push-1 medium-push-1">
                                        <asp:TextBox ID="txtALastName" runat="server" value="Last Name"
                                            onfocus="focusObj1(this);" onblur="blurObj1(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red large-push-1 medium-push-1">*</div>
                                    <div class="large-1 medium-1 small-11 columns lft large-push-1 medium-push-1">
                                        <asp:TextBox CssClass="adtdobcss" value="DOB" ID="Txt_AdtDOB" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="adtreq" class="large-1 medium-1 small-1 columns red">*</div>
                                </div>
                                <div class="clear1"></div>
                                <div class="large-12 medium-12 small-12">
                                    <div class="large-1 medium-1 small-3 columns">
                                        <asp:DropDownList CssClass="txtdd1" ID="ddl_AGender" runat="server">
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="large-2 medium-2 small-3 columns">
                                        <asp:TextBox ID="txt_Passport_Adl" runat="server" value="Passport No"
                                            MaxLength="10" onkeypress="return checkit(event)"
                                            onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-2 small-12 passenger columns">
                                        <asp:TextBox ID="txt_Nationality_Adl" runat="server" value="Nationality"
                                            onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityADL"></asp:TextBox>
                                        <input type="hidden" id="Hdn_Nationality_Adl" name="Hdn_Nationality_Adl" class="Hdn_Nationality_Adl" runat="server" value="" />
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-2 small-12 passenger columns">
                                        <asp:TextBox ID="txt_IssuingCountry_Adl" runat="server" value="Issuing Country"
                                            onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryADL"></asp:TextBox>
                                        <input type="hidden" id="Hdn_IssuingCountry_Adl" name="Hdn_IssuingCountry_Adl" class="Hdn_IssuingCountry_Adl" runat="server" value="" />
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-1 medium-2 small-12 passenger columns">
                                        <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Adl" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                </div>
                                <div class="clear1"></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="td_Child" runat="server">
                    <asp:Repeater ID="Repeater_Child" runat="server">
                        <ItemTemplate>
                            <div class="bld">
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
                                    <div class="large-2 medium-3 small-8 columns ">
                                        <asp:TextBox ID="txtCFirstName" runat="server" value="First Name"
                                            onfocus="focusObjCFName(this);" onblur="blurObjCFName(this);" defvalue="First Name"
                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-3 small-12 passenger columns ">
                                        <asp:TextBox ID="txtCMiddleName" runat="server" value="Middle Name"
                                            onfocus="focusObjCMName(this);" onblur="blurObjCMName(this);" defvalue="Middle Name"
                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-2 medium-2 small-11 columns large-push-1 medium-push-1">
                                        <asp:TextBox ID="txtCLastName" runat="server" value="Last Name"
                                            onfocus="focusObjCLName(this);" onblur="blurObjCLName(this);" defvalue="Last Name"
                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red large-push-1 medium-push-1">*</div>
                                    <div class="large-1 medium-1 small-11 columns lft large-push-1 medium-push-1">
                                        <asp:TextBox CssClass="txtboxx chddobcss" value="DOB" ID="Txt_chDOB" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                </div>
                                <div class="clear"></div>
                                <div class="large-12 medium-12 small-12">
                                    <div class="large-1 medium-1 small-3 columns">
                                        <asp:DropDownList CssClass="txtdd1" ID="ddl_CGender" runat="server">
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="large-2 medium-2 small-3 columns">
                                        <asp:TextBox ID="txt_Passport_Chd" runat="server" value="Passport No"
                                            MaxLength="10" onkeypress="return checkit(event)"
                                            onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-2 small-12 passenger columns">
                                        <asp:TextBox ID="txt_Nationality_Chd" runat="server" value="Nationality"
                                            onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityCHD"></asp:TextBox>
                                        <input type="hidden" id="Hdn_Nationality_Chd" name="Hdn_Nationality_Chd" class="Hdn_Nationality_Chd" runat="server" value="" />
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-2 medium-2 small-12 passenger columns">
                                        <asp:TextBox ID="txt_IssuingCountry_Chd" runat="server" value="Issuing Country"
                                            onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryCHD"></asp:TextBox>
                                        <input type="hidden" id="Hdn_IssuingCountry_Chd" name="Hdn_IssuingCountry_Chd" class="Hdn_IssuingCountry_Chd" runat="server" value="" />
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red">*</div>
                                    <div class="large-1 medium-2 small-12 passenger columns">
                                        <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Chd" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="large-1 medium-1 small-1 columns red ">*</div>
                                </div>
                                <div class="w100">
                                </div>
                                <div class="clear1">
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="clear"></div>
                <div id="td_Infant" runat="server">
                    <asp:Repeater ID="Repeater_Infant" runat="server">
                        <ItemTemplate>
                            <div class="bld large-12 medium-12 small-12">
                                <asp:Label ID="pttextINF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>
                            </div>
                            <div class="large-12 medium-12 small-3" style="padding-top: 10px;">
                                <div class="large-1 medium-1 small-3 columns">
                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_ITitle" runat="server">
                                        <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                        <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="large-2 medium-2 small-8 columns">
                                    <asp:TextBox ID="txtIFirstName" runat="server" value="First Name"
                                        onfocus="focusObjIFName(this);" onblur="blurObjIFName(this);" defvalue="First Name"
                                        onkeypress="return isCharKey(event)"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-2 medium-2 small-12 passenger columns">
                                    <asp:TextBox ID="txtIMiddleName" runat="server" value="Middle Name"
                                        onfocus="focusObjIMName(this);" onblur="blurObjIMName(this);" defvalue="Middle Name"
                                        onkeypress="return isCharKey(event)"></asp:TextBox>
                                </div>
                                <div class="large-2 medium-2 small-11 columns large-push-1 medium-push-1">
                                    <asp:TextBox ID="txtILastName" runat="server" value="Last Name"
                                        onfocus="focusObjILName(this);" onblur="blurObjILName(this);" defvalue="Last Name"
                                        onkeypress="return isCharKey(event)"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red large-push-1 medium-push-1">*</div>
                                <div class="large-1 medium-1 small-11 columns lft large-push-1 medium-push-1">
                                    <asp:TextBox CssClass="txtboxx infdobcss" Value="DOB" ID="Txt_InfantDOB" runat="server"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                            </div>
                            <div class="clear"></div>
                            <div class="large-12 medium-12 small-12">
                                <div class="large-1 medium-1 small-3 columns">
                                    <asp:DropDownList CssClass="txtdd1" ID="ddl_IGender" runat="server">
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="large-2 medium-2 small-3 columns">
                                    <asp:TextBox ID="txt_Passport_Inf" runat="server" value="Passport No"
                                        MaxLength="10" onkeypress="return checkit(event)"
                                        onfocus="focusObjNumber_Passport(this);" onblur="blurObjNumber_Passport(this);" defvalue="Passport No"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-2 medium-2 small-12 passenger columns">
                                    <asp:TextBox ID="txt_Nationality_Inf" runat="server" value="Nationality"
                                        onfocus="focusObjNumber_Nationality(this);" onblur="blurObjNumber_Nationality(this);" defvalue="Nationality" CssClass="NationalityINF"></asp:TextBox>
                                    <input type="hidden" id="Hdn_Nationality_Inf" name="Hdn_Nationality_Inf" class="Hdn_Nationality_Inf" runat="server" value="" />
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-2 medium-2 small-12 passenger columns">
                                    <asp:TextBox ID="txt_IssuingCountry_Inf" runat="server" value="Issuing Country"
                                        onfocus="focusObjNumber_Issuing(this);" onblur="blurObjNumber_Issuing(this);" defvalue="Issuing Country" CssClass="IssuingCountryINF"></asp:TextBox>
                                    <input type="hidden" id="Hdn_IssuingCountry_Inf" name="Hdn_IssuingCountry_Inf" class="Hdn_IssuingCountry_Inf" runat="server" value="" />
                                </div>
                                <div class="large-1 medium-1 small-1 columns red">*</div>
                                <div class="large-1 medium-2 small-12 passenger columns">
                                    <asp:TextBox CssClass="datepicker" value="Ex Date" ID="txt_ex_date_Inf" runat="server"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-1 columns red ">*</div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="clear"></div>
                <div class="row" style="visibility:hidden">
                    <div class="large-12 medium-12 small-12 bld">
                        Primary Guest Details
                    </div>
                    <div class="clear"></div>
                    <div class="large-12 medium-12 small-12">
                        <div class="large-1 medium-1 small-3 columns ">
                            <asp:DropDownList CssClass="txtdd1" ID="ddl_PGTitle" runat="server">
                                <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                <asp:ListItem Value="Mstr">Mrs.</asp:ListItem>
                                <asp:ListItem Value="Miss">Dr.</asp:ListItem>
                                <asp:ListItem Value="Miss">Prof.</asp:ListItem>
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
                        <div class="large-1 medium-1 small-11 columns">
                            <asp:TextBox ID="txt_MobNo" runat="server" value="Mobile No" onfocus="focusObjPM(this);"
                                onblur="blurObjPM(this);" defvalue="Mobile No" onkeypress="return isNumberKey(event)"
                                MaxLength="12"></asp:TextBox>
                        </div>
                        <div class="large-1 medium-1 small-1 columns red">*</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear1"></div>
        <div id="div_Progress" style="display: none">
            <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
        </div>
        <div id="div_Submit" class="large-2 medium-2 small-12 large-push-9 medium-push-9">
            <asp:Button ID="book" CssClass="buttonfltbk" runat="server" Text="Continue" OnClick="book_Click" />
        </div>
    </div>
    <div class="clear">
    </div>
    <div id="ddhidebox" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: url(../images/fade.png); display: none;">
        <div id="divtotFlightDetails" runat="server" style="width: 58%; left: -100%; position: fixed; padding: 1.5%; box-shadow: 1px 2px 10px #000; border: 5px solid #d1d1d1; margin-top: 100px; background: #f1f1f1; float: left;">
        </div>
        <div id="div_fareddDetails" style="width: 58%; border: 5px solid #d1d1d1; left: -100%; position: fixed; padding: 1.5%; box-shadow: 1px 2px 10px #000; margin-top: 100px; background: #f1f1f1; float: left;">
            <div id="div_fare" runat="server" class="w101" style="padding-top: 10px;">
            </div>
            <div id="div_fareR" runat="server" class="w101" style="padding-top: 10px;">
            </div>
        </div>
        <div style="padding: 5px; background: #f1f1f1; margin-top: 100px; font-weight: bold; position: fixed; left: 100%; cursor: pointer;"
            onclick="ddhide();" id="closeclose">
            CLOSE
        </div>
    </div>
    <div class="w100">
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
        </div>
    </div>
    <div class="clear1">
    </div>
    <asp:HiddenField ID="hdn_vc" runat="server" />
    <script type="text/javascript">
        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg
        }
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.draggable.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
    <script type="text/javascript">
        $(function () { var d = new Date(); var dd = new Date(1952, 01, 01); $(".adtdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: ('1930:' + (d.getFullYear() - 12) + ''), navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true, defaultDate: dd }) });
        $(function () { $(".chddobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '-2y', minDate: '-10y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
        $(function () { $(".infdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '+0y', minDate: '-2y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
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
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    minDate: 0,
                })
        });
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Something went worng,Please contact with admin !!");
                    window.opener.location.reload('GroupDetails.aspx')
                    window.close();
                }
                    break;
            }
        }
    </script>
</asp:Content>

