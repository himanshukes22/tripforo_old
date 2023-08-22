<%@ Page Language="VB" AutoEventWireup="true" CodeFile="regs_new.aspx.vb" Inherits="regs_new" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="https://fonts.googleapis.com/css?family=Rubik:300,400,500,700" rel="stylesheet" />
    <title>Registration Tripforo</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="Registration/Content/User/animation.css" rel="stylesheet" />
    <%--<link href="../www.easemytrip.com/css/header-new-main.css" rel="stylesheet" />--%>
    <script src="../ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../ajax.aspnetcdn.com/ajax/modernizr/modernizr-2.7.2.js"></script>
    <script src="Registration/Scripts/External/angular.min.js"></script>
    <script src="Registration/Scripts/Js/jquery-ui.js"></script>
    <script src="Registration/Scripts/Js/User/adminJs4730.js?v=637071017756558079"></script>

    <script src="Registration/Scripts/Js/User/AnguEmail7df5.js?v=637088359368128168"></script>

    <link href="Registration/Content/CSS/themes/jquery.ui.datepickeref06.css?v=636490425933074891" rel="stylesheet" />

    <link href="../code.jquery.com/ui/1.8.24/themes/base/jquery-ui.css" rel="stylesheet" />

    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>

    <link href="icofont/icofont.css" rel="stylesheet" />
    <link href="icofont/icofont.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Advance_CSS/css/bootstrap.css" />
    <link rel="stylesheet" href="Advance_CSS/css/styles.css" />

    <style type="text/css">
        .theme-search-area-section {
            margin-bottom: 20px !important;
        }

        .theme-search-area-section-line .theme-search-area-section-icon {
            text-align: left;
            width: 30.555555555555554px;
            margin-top: 0px;
            font-size: 20px;
        }

        .three h1 {
            font-size: 22px;
            font-weight: 500;
            letter-spacing: 0;
            line-height: 1.5em;
            padding-bottom: 6px;
            position: relative;
            margin-bottom: 25px;
        }

            .three h1:before {
                content: "";
                position: absolute;
                left: 0;
                bottom: 0;
                height: 5px;
                width: 55px;
                background-color: #7ace0b;
            }

            .three h1:after {
                content: "";
                position: absolute;
                left: 0;
                bottom: 2px;
                height: 1px;
                width: 95%;
                max-width: 255px;
                background-color: #7ace0b;
            }
    </style>








    <%--<script>
        $(document).ready(function () {
            $('#mycheckbox').change(function () {

                $('#mycheckboxdiv').toggle();
                $('#ResAddDtlDv').toggle();
                if ($("#chkTrains").is(':checked')) {
                    $('#AgntPostOfcStar').show();
                    $('#AgntPhNoStar').show();
                    $('#AgntGSTStar').show();
                }
                else {
                    $('#AgntPostOfcStar').hide();
                    $('#AgntPhNoStar').hide();
                    $('#AgntGSTStar').hide();
                }

            });
        });
        $(function () {
            $("#txtDob").datepicker({
                numberOfMonths: 1,
                dateFormat: "dd/mm/yy"
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(function () {
            $('#txtOfcPh').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
            $('#txtResPhNo').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $('#txtOfcPh').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
            $('#txtResPhNo').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });

            $('#txtpincode').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
            $('#txtAdharNo').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Validate(e) {
            var keyCode = e.keyCode || e.which;

            var lblfname = document.getElementById("Fname_txt");
            var lblmname = document.getElementById("txtmname");
            var lblLastname = document.getElementById("Lname_txt");
            var pancarholdername = document.getElementById("txtpanholdername");
            lblfname.innerHTML = "";
            lblmname.innerHTML = "";
            lblLastname.innerHTML = "";
            pancarholdername.innerHTML = "";

            //Regex for Valid Characters i.e. Alphabets.
            var regex = /^[A-Za-z]+$/;

            //Validate TextBox value against the Regex.
            var isValid = regex.test(String.fromCharCode(keyCode));
            if (!isValid) {
                lblfname.innerHTML = "Only Alphabets allowed.";
                lblmname.innerHTML = "Only Alphabets allowed.";
                lblLastname.innerHTML = "Only Alphabets allowed.";
                pancarholdername.innerHTML = "Only Alphabets allowed.";
            }

            return isValid;
        }
    </script>


       <script language="javascript" type="text/javascript">
           function CheckGStIsCheckOrNot() {
               if (document.getElementById("chkIsGST").checked) {
                   document.getElementById("divGSTInformation").style.display = "block";
               }
               else {
                   document.getElementById("divGSTInformation").style.display = "none";
                   document.getElementById("txtGstNumber").value = "";
                   document.getElementById("txtGSTCompanyName").value = "";
                   document.getElementById("txtGSTCompanyAddress").value = "";
                   document.getElementById("txtGSTCompanyAddress").value = "";
                   document.getElementById("ddlGSTState").value = "select";
                   document.getElementById("ddlGSTCity").innerHTML = "";
                   var opt = document.createElement('option');
                   opt.appendChild(document.createTextNode('--Select City--'));
                   opt.value = 'select';
                   document.getElementById("ddlGSTCity").appendChild(opt);
                   document.getElementById("txtGSTPoincoe").value = "";
                   document.getElementById("txtGSTPhoneNo").value = "";
                   document.getElementById("txtGSTEMailId").value = "";
               }
           }
           function checkUserIdStrength() {
               var thiscurrval = document.getElementById("Mob_txt").value;
               document.getElementById("TxtUserIdreg").value = thiscurrval;
           }
           function validateSearch() {

               if ($("[id$=Fname_txt]").val() == "") {
                   $("#val").text("Please Specify First Name").css("display", "block");

                   $("[id$=Fname_txt]").focus();
                   return false;
               }
               if ($("[id$=Lname_txt]").val() == "") {
                   $("#val").text("Please Specify Last Name").css("display", "block");

                   $("[id$=Lname_txt]").focus();
                   return false;
               }
               if ($("[id$=Mob_txt]").val() == "") {
                   $("#val").text("Please Specify Mobile Number").css("display", "block");

                   $("[id$=Mob_txt]").focus();
                   return false;
               }
               if ($("[id$=Email_txt]").val() == "") {
                   $("#val").text("Please  Specify Email ID").css("display", "block");
                   $("[id$=Email_txt]").focus();
                   return false;
               }
               var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
               var emailid = $("[id$=Email_txt]").val();
               var matchArray = emailid.match(emailPat);
               if (matchArray == null) {
                   $("#val").text("Your email address seems incorrect. Please try again.").css("display", "block");

                   $("[id$=Email_txt]").focus();
                   return false;
               }

               if ($("[id$=WMob_txt]").val() == "") {
                   $("#val").text("Please specify Whatsapp Mobile Number").css("display", "block");

                   $("[id$=WMob_txt]").focus();
                   return false;
               }

               if ($("[id$=Agn_txt]").val() == "") {
                   $("#val").text("Please Specify Agency Name").css("display", "block");

                   $("[id$=Agn_txt]").focus();
                   return false;
               }
               if ($("[id$=Add_txt]").val() == "") {
                   $("#val").text("Please Specify Address").css("display", "block");

                   $("[id$=Add_txt]").focus();
                   return false;
               }


               if ($("[id$=ddl_country]").val() == "India") {
                   if ($("[id$=ddl_state]").val() == "--Select State--") {
                       $("#val").text("Please Select State").css("display", "block");

                       $("[id$=ddl_state]").focus();
                       return false;
                   }
                   if ($("[id$=ddl_city]").val() == "") {
                       $("#val").text("Please Select City").css("display", "block");

                       $("[id$=ddl_city]").focus();
                       return false;
                   }

               }
               else {
                   if ($("[id$=Coun_txt]").val() == "") {
                       $("#val").text("Specify Country Name").css("display", "block");

                       $("[id$=Coun_txt]").focus();
                       return false;
                   }
                   if ($("[id$=Stat_txt]").val() == "") {
                       $("#val").text("Specify State Name").css("display", "block");

                       $("[id$=Stat_txt]").focus();
                       return false;
                   }
                   if ($("[id$=Other_City]").val() == "") {
                       $("#val").text("Specify City Name").css("display", "block");

                       $("[id$=Other_City]").focus();
                       return false;

                   }

               }

               if ($("[id$=TextBox_Area]").val() == "") {
                   $("#val").text("Please Specify District").css("display", "block");

                   $("[id$=TextBox_Area]").focus();
                   return false;
               }
               if ($("[id$=ddl_city]").val() == "") {
                   $("#val").text("Please Specify City").css("display", "block");

                   $("[id$=ddl_city]").focus();
                   return false;
               }

               if ($("[id$=Pin_txt]").val() == "") {
                   $("#val").text("Please Specify Pincode").css("display", "block");

                   $("[id$=Pin_txt]").focus();
                   return false;
               }




               //bipin
               if ($("[id$=Pan_txt]").val() == "") {
                   $("#val").text("Please Specify Pan No").css("display", "block");

                   $("[id$=Pan_txt]").focus();
                   return false;
               }

               var panregx = new RegExp('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$');
               if (!panregx.test($("[id$=Pan_txt]").val())) {
                   $("#val").text("Pan No should be alpha numeric").css("display", "block");

                   $("[id$=Pan_txt]").focus();
                   return false;
               }

               if ($("[id$=TextBox_NameOnPard]").val() == "") {
                   $("#val").text("Specify Name on Pan Card").css("display", "block");

                   $("[id$=TextBox_NameOnPard]").focus();
                   return false;
               }

               if (document.getElementById("chkIsGST").checked) {

                   if (document.getElementById("txtGstNumber").value == "") {
                       $("#val").text("Specify Gst Number").css("display", "block");
                       document.getElementById("txtGstNumber").focus();
                       return false;
                   }
                   if (document.getElementById("txtGSTCompanyName").value == "") {
                       $("#val").text("Specify Company Name").css("display", "block");
                       document.getElementById("txtGSTCompanyName").focus();
                       return false;
                   }
                   if (document.getElementById("txtGSTCompanyAddress").value == "") {
                       $("#val").text("Specify Company Address").css("display", "block");
                       document.getElementById("txtGSTCompanyAddress").focus();
                       return false;
                   }
                   if (document.getElementById("ddlGSTState").value == "select") {
                       $("#val").text("Specify State").css("display", "block");
                       document.getElementById("ddlGSTState").focus();
                       return false;
                   }
                   if (document.getElementById("ddlGSTCity").value == "select") {
                       $("#val").text("Specify City").css("display", "block");
                       document.getElementById("ddlGSTCity").focus();
                       return false;
                   }
                   if (document.getElementById("txtGSTPoincoe").value == "") {
                       $("#val").text("Specify Pin Code").css("display", "block");
                       document.getElementById("txtGSTPoincoe").focus();
                       return false;
                   }
                   if (document.getElementById("txtGSTPhoneNo").value == "") {
                       $("#val").text("Specify Phone Number").css("display", "block");
                       document.getElementById("txtGSTPhoneNo").focus();
                       return false;
                   }
                   if (document.getElementById("txtGSTEMailId").value == "") {
                       $("#val").text("Specify Email Id").css("display", "block");
                       document.getElementById("txtGSTEMailId").focus();
                       return false;
                   }

                   var gemailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                   var gemailid = document.getElementById("txtGSTEMailId").value;
                   var gmatchArray = gemailid.match(gemailPat);
                   if (gmatchArray == null) {
                       $("#val").text("Your email address seems incorrect. Please try again.").css("display", "block");
                       document.getElementById("txtGSTEMailId").focus();
                       return false;
                   }
               }


               if ($("[id$=Pass_text]").val() == "") {
                   $("#val").text("Specify Password").css("display", "block");

                   $("[id$=Pass_text]").focus();
                   return false;
               }

               if ($("[id$=cpass_txt]").val() == "") {
                   $("#val").text("Specify Confirm Password").css("display", "block");

                   $("[id$=cpass_txt]").focus();
                   return false;
               }

               //if (document.getElementById("fld_pan").value == "") {
               //    $("#val").text("Specify Pan Card Image").css("display", "block");
               //    document.getElementById("fld_pan").focus();
               //    return false;
               //}

               //if (document.getElementById("flu_CompanyAddress").value == "") {
               //    $("#val").text("Specify Company Address Image").css("display", "block");
               //    document.getElementById("flu_CompanyAddress").focus();
               //    return false;
               //}


               else {
                   var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,16}$/;
                   if (!regex.test($("[id$=Pass_text]").val())) {
                       $("#val").text("Password must contain:8 To 16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character").css("display", "block");

                       $("[id$=Pass_text]").focus();
                       return false;
                   }

                   // var patt = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8}");
               }



               if (document.getElementById("cpass_txt").value != "") {
                   if (document.getElementById("Pass_text").value != document.getElementById("cpass_txt").value) {
                       $("#val").text("Confirm Password is not same as Password").css("display", "block");
                       document.getElementById("cpass_txt").focus();
                       return false;
                   }

                   if (confirm("Are you sure!"))
                       return true;
                   return false;
               }
               if (document.getElementById("Ans_txt").value == "") {
                   $("#val").text("Specify Answer").css("display", "block");
                   document.getElementById("Ans_txt").focus();
                   return false;
               }


               //if (document.getElementById("TxtUserId").value == "") {
               //    alert('Specify Userid');
               //    document.getElementById("TxtUserId").focus();
               //    return false;
               //}



           }
           function phone_vali() {
               if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                   event.returnValue = true;
               else
                   event.returnValue = false;
           }
           function vali() {
               if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || (event.keyCode == 32) || (event.keyCode == 45))
                   event.returnValue = true;
               else
                   event.returnValue = false;
           }

           function vali1() {
               if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || (event.keyCode == 32) || (event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32))
                   event.returnValue = true;
               else
                   event.returnValue = false;
           }

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

       </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });


    </script>

    <%--    <script>
        $(function () { // this will be called when the DOM is ready
            $('.psb_dd').keyup(function () {
                debugger;
                $("#TxtUserIdreg").val($(this).val());
            });
        });
    </script>--%>
    <script type="text/javascript">
        function RefreshCaptcha() {
            var img = document.getElementById("imgCaptcha");
            img.src = "CAPTCHA.ashx?query=" + Math.random();
        }
    </script>


</head>
<body>


    <form runat="server" class="rcw-form container-fluid ff-box-shadow">

        <nav class="navbar navbar-default navbar-inverse navbar-theme navbar-theme-abs navbar-theme-transparent navbar-theme-border" id="main-nav">
            <div class="container">
                <div class="navbar-inner nav">




                    <div class="navbar-header">
                        <button class="navbar-toggle collapsed" data-target="#navbar-main" data-toggle="collapse" type="button" area-expanded="false">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="search.aspx">
                            <img src="Advance_CSS/Icons/logo(ft).png" alt="Image Alternative text" title="Image Title" />
                        </a>
                    </div>
                    <div class="collapse navbar-collapse" id="navbar-main">
                        <ul class="nav navbar-nav">

                            <li>
                                <a href="about-us.html" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">About</a>

                            </li>
                            <li>
                                <a href="contact.html" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Contact</a>

                            </li>




                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                        </ul>
                    </div>
                </div>
            </div>
        </nav>


        <div class="theme-page-section theme-page-section-gray theme-page-section-lg" id="table_reg" runat="server" style="margin-top: 40px;">

            <div class="container">


                <div class="theme-coming-soon-header">
                    <h2>Get Started - Register as a Agent</h2>
                </div>
                <div id="val" style="display: none; text-align: center; font-size: 20px; color: rgb(229 0 0); background-color: rgb(238 207 207); padding: 8px; margin-bottom: 6px; border-radius: 6px;"></div>
                <div id="divErrorMsg" runat="server" visible="false" style="font-size: 20px; text-align: center; color: rgb(229 0 0); background-color: rgb(238 207 207); padding: 8px; margin-bottom: 6px; border-radius: 6px;"></div>


                <div>
                    <div class="row">
                        <div class="three">
                            <h1>Personal Details</h1>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-substitute"></i>
                                    <asp:DropDownList ID="tit_drop" CssClass="theme-search-area-section-input" placeholder="Title" runat="server">
                                        <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                        <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                        <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-id"></i>

                                    <asp:TextBox ID="Fname_txt" CssClass="theme-search-area-section-input" runat="server" placeholder="First Name *" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-id"></i>
                                    <asp:TextBox ID="Lname_txt" CssClass="theme-search-area-section-input" placeholder="Last Name *" runat="server" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ipod-touch"></i>

                                    <asp:TextBox ID="TextBox4" CssClass="theme-search-area-section-input" placeholder="+91" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ipod-touch"></i>
                                    <asp:TextBox ID="Mob_txt" CssClass="theme-search-area-section-input" onKeyUp="checkUserIdStrength()" placeholder="Mobile No. * (Your user id )" runat="server" MaxLength="10"></asp:TextBox>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ui-email"></i>
                                    <asp:TextBox ID="Email_txt" CssClass="theme-search-area-section-input" placeholder="Email *" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ipod-touch"></i>
                                    <asp:TextBox ID="Ph_txt" CssClass="theme-search-area-section-input" placeholder="Phone No. (Optional)" runat="server" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-brand-whatsapp"></i>
                                    <asp:TextBox ID="WMob_txt" CssClass="theme-search-area-section-input" placeholder="whatsapp No. *" runat="server" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ui-email"></i>
                                    <asp:TextBox ID="Aemail_txt" CssClass="theme-search-area-section-input" runat="server" placeholder="Email 2(Optional)"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-fax"></i>
                                    <asp:TextBox ID="Fax_txt" CssClass="theme-search-area-section-input" placeholder="Fax No.(Optional)" runat="server" Style="position: static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="three">
                            <h1>Agency Details</h1>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-building"></i>
                                    <asp:TextBox ID="Agn_txt" CssClass="theme-search-area-section-input" runat="server" placeholder="Agency Name *" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-map-pins"></i>
                                    <asp:TextBox ID="Add_txt" CssClass="theme-search-area-section-input" runat="server" placeholder="Agency Address *" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-map"></i>
                                    <asp:DropDownList ID="ddl_country" CssClass="theme-search-area-section-input" runat="server" AutoPostBack="True">
                                        <%-- <asp:ListItem Selected="True">--Select Country--</asp:ListItem>--%>
                                        <asp:ListItem Selected="True" Value="India">India</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="Coun_txt" CssClass="psb_dd form-control " runat="server"
                                        Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-map"></i>
                                    <%-- <input type="text" class="input_bx" name="txtDob" id="txtDob" placeholder="Date of Birth" />--%>
                                    <asp:DropDownList ID="ddl_state" CssClass="theme-search-area-section-input" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="Stat_txt" CssClass="psb_dd form-controlrt" runat="server"
                                        Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-building-alt"></i>
                                    <asp:TextBox ID="TextBox_Area" CssClass="theme-search-area-section-input" runat="server" placeholder="District *" MaxLength="20" Style="position: static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3" style="display: none;">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-map"></i>
                                    <asp:TextBox ID="Other_City" CssClass="theme-search-area-section-input " runat="server" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-building-alt"></i>
                                    <input type="text" id="ddl_city" runat="server" class="theme-search-area-section-input" placeholder="City" style="position: static" />
                                    <asp:TextBox ID="TextBox1" CssClass="psb_dd form-control " runat="server" Style="position: static"
                                        Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-search-map"></i>
                                    <asp:TextBox ID="Pin_txt" CssClass="theme-search-area-section-input" runat="server" placeholder="Pin Code" MaxLength="8" Style="position: static"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                    </div>


                    <div class="row">
                        <div class="three">
                            <h1>Business Information</h1>
                        </div>


                        <div class="col-md-3" style="display: none">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ui-v-card"></i>
                                    <asp:TextBox ID="Web_txt" CssClass="theme-search-area-section-input" placeholder="Business WebSite" runat="server" Style="display: none"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ui-v-card"></i>
                                    <asp:TextBox ID="Pan_txt" Style="text-transform: uppercase;" CssClass="theme-search-area-section-input" placeholder="PAN No. *" MaxLength="10" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-ui-v-card"></i>
                                    <asp:TextBox ID="TextBox_NameOnPard" CssClass="theme-search-area-section-input" placeholder="Name On PAN *" runat="server" MaxLength="40"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="input-group col_2 mgr14 revealOnScroll" data-animation="fadeInUp" style="display: none;">
                            <asp:FileUpload ID="fld_pan" runat="server" CssClass="theme-search-area-section-input" />

                            <div style="display: none;">
                                <asp:DropDownList ID="Stat_drop" CssClass="input_bx" runat="server" Height="20px" Width="150px">
                                    <asp:ListItem Value="TA" Selected="True">Travel Agent</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="" style="font-size: 11px; color: #0e4faa;">
                                ( Pancard image must be in JPG formate )
                            </div>

                        </div>

                        <div class="input-group col_2   revealOnScroll" data-animation="fadeInUp" style="display: none;">
                            <asp:FileUpload ID="flu_CompanyAddress" runat="server" CssClass="theme-search-area-section-input" />
                            <div style="font-size: 11px; color: #0e4faa;">( Company address image must be in JPG formate)</div>

                        </div>

                        <div class="input-group col_2  mgr14 revealOnScroll" data-animation="fadeInUp" style="display: none;">
                            <asp:FileUpload ID="fld_1" runat="server" CssClass="theme-search-area-section-input" />
                            <div style="font-size: 11px; color: #0e4faa;">( Logo must be in PNG formate and  Size should be (90*70) pixels)</div>

                        </div>



                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-bag-alt"></i>
                                    <asp:DropDownList ID="Sales_DDL" CssClass="theme-search-area-section-input" runat="server" class="input_bx">
                                        <asp:ListItem Value="--Refer By--">--Refer By--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                    <asp:TextBox ID="Rem_txt" CssClass="theme-search-area-section-input" placeholder="Remark (Optional)" runat="server" Style="position: static"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="input-group col_2 revealOnScroll" data-animation="fadeInUp" style="display: none">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input_bx" Style="display: none"></asp:DropDownList>

                        </div>
                        <asp:DropDownList ID="DD_Branch" runat="server" CssClass="input_bx" Style="display: none"></asp:DropDownList>

                        <div class="input-group col_12 revealOnScroll" data-animation="fadeInUp">
                            <p style="color: #000">
                                Do you have GST information ? &nbsp;
                                    <%--<input type="checkbox" id="chkIsGST" class="cssisgst" onclick="CheckGStIsCheckOrNot();" />--%>
                                <asp:CheckBox ID="chkIsGST" runat="server" CssClass="cssisgst" onclick="CheckGStIsCheckOrNot();" />
                            </p>
                        </div>


                        <div class="row" id="divGSTInformation" runat="server" style="display: none;">
                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGstNumber" CssClass="theme-search-area-section-input" placeholder="GST No." runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGSTCompanyName" CssClass="theme-search-area-section-input" placeholder="Company Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGSTCompanyAddress" CssClass="theme-search-area-section-input" placeholder="Company Address" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:DropDownList ID="ddlGSTState" CssClass="theme-search-area-section-input" runat="server" class="input_bx" onchange="javascript:ChangeStateEvent();">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>



                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:DropDownList ID="ddlGSTCity" CssClass="theme-search-area-section-input" runat="server" class="input_bx" onchange="javascript:ChangeCityEvent();">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnSelectedCity" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGSTPoincoe" CssClass="theme-search-area-section-input" placeholder="Pin Code" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGSTPhoneNo" CssClass="theme-search-area-section-input" placeholder="Phone No" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="theme-search-area-section theme-search-area-section-line">
                                    <div class="theme-search-area-section-inner">
                                        <i class="theme-search-area-section-icon icofont-edit-alt"></i>
                                        <asp:TextBox ID="txtGSTEMailId" CssClass="theme-search-area-section-input" placeholder="Email Id" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>


                    <div class="row" runat="server">
                        <div class="three">
                            <h1>Authentication Information</h1>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-user-male"></i>
                                    <%--<asp:TextBox ID="TxtUserIdreg" value="{{name}}" runat="server" Style="position: static" MaxLength="20" CssClass="theme-search-area-section-input" placeholder="User Id *" ReadOnly="true"></asp:TextBox>--%>
                                    <asp:TextBox ID="TxtUserIdreg" runat="server" Style="position: static" MaxLength="20" CssClass="theme-search-area-section-input" placeholder="User Id *" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-lock"></i>
                                    <asp:TextBox ID="Pass_text" runat="server" Style="position: static"
                                        TextMode="Password" MaxLength="16" CssClass="theme-search-area-section-input" placeholder="Password *"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                                <div class="theme-search-area-section-inner">
                                    <i class="theme-search-area-section-icon icofont-lock"></i>
                                    <asp:TextBox ID="cpass_txt" CssClass="theme-search-area-section-input" placeholder="Confirm Password *" runat="server" Style="position: static"
                                        TextMode="Password" MaxLength="16"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <asp:Button ID="submit" runat="server" Text="Register" OnClientClick="return validateSearch()"
                                CssClass="btn-sbmt btn btn-dark hasDatepicker" />

                        </div>

                    </div>



                    <div class="col_12 t-center">
                        <p class="para">If you are already register, please click hear to login : <a href="Login.aspx">Login</a> </p>
                    </div>


                </div>






            </div>


        </div>
    </form>







    <div id="table_Message" runat="server" visible="false" style="margin-top:6%;">

        <style type="text/css">
            @import url(https://fonts.googleapis.com/css?family=Nunito);

            /* Take care of image borders and formatting */

            img {
                max-width: 600px;
                outline: none;
                text-decoration: none;
                -ms-interpolation-mode: bicubic;
            }

            html {
                margin: 0;
                padding: 0;
            }

            a {
                text-decoration: none;
                border: 0;
                outline: none;
                color: #bbbbbb;
            }

                a img {
                    border: none;
                }

            /* General styling */

            td, h1, h2, h3 {
                font-family: Helvetica, Arial, sans-serif;
                font-weight: 400;
            }

            td {
                text-align: center;
            }

            body {
                -webkit-font-smoothing: antialiased;
                -webkit-text-size-adjust: none;
                width: 100%;
                height: 100%;
                color: #666;
                background: #fff;
                font-size: 16px;
                height: 100vh;
                width: 100%;
                padding: 0px;
                margin: 0px;
            }

            table {
                border-collapse: collapse !important;
            }

            .headline {
                color: #444;
                font-size: 36px;
            }

            .force-full-width {
                width: 100% !important;
            }
        </style>


        <style media="screen" type="text/css">
            @media screen {
                td, h1, h2, h3 {
                    font-family: 'Nunito', 'Helvetica Neue', 'Arial', 'sans-serif' !important;
                }
            }
        </style>
        <style media="only screen and (max-width: 480px)" type="text/css">
            /* Mobile styles */
            @media only screen and (max-width: 480px) {

                table[class="w320"] {
                    width: 320px !important;
                }
            }
        </style>
        <style type="text/css"></style>

        </head>
  <body bgcolor="#fff" class="body" style="padding: 20px; margin: 0; display: block; background: #ffffff; -webkit-text-size-adjust: none">
      <table align="center" cellpadding="0" cellspacing="0" height="100%" width="100%">
          <tbody>
              <tr>
                  <td align="center" bgcolor="#fff" class="" valign="top" width="100%">
                      <center class=""><table cellpadding="0" cellspacing="0" class="w320" style="margin: 0 auto;" width="600">
<tbody><tr>
<td align="center" class="" valign="top"><table cellpadding="0" cellspacing="0" style="margin: 0 auto;" width="100%">
</table>
<table bgcolor="#fff" cellpadding="0" cellspacing="0" class="" style="margin: 0 auto; width: 100%;">
<tbody style="margin-top: 15px;">
  <tr class="">
<td class="">
<img alt="robot picture" class="" height="155" src="../Advance_CSS/Icons/logo(ft).png" style="height: 75px;">
</td>
</tr>
<tr class=""><td class="headline">Welcome to Tripforo!</td></tr>
<tr>
<td>
<center class=""><table cellpadding="0" cellspacing="0" class="" style="margin: 0 auto;" width="100%"><tbody class=""><tr class="">
<td class="" style="color:#444; font-weight: 400;text-align: left;"><br><br>
    <p>Dear Sir</p>
    <br>
    <p>Thank you for registering with www.tripforo.com</p>
    <%--<p>Your Agency ID </p>--%>
    <p>Your User ID : <%=CID%></p>
    <p>Your Password : <%=Password%></p>
    <p>Your registered mobile is : <%=Mobile%></p>
    <p>Your registered email is  : <%=Email %></p>
    <p>We will activate your portal account within 24 hours after thorough verification. For any assistance, please call on +91 93 3192 2333 Or email on sales@tripforo.com</p>
     <br>
    
    <p>Thanks</p>
    <p>Team Tripforo</p>



    <br />
   <%-- Your login credentials will be sent on your register mail id & mobile number, after activation of your agency account.

    <br />
    <br />
    Our backend team will activate your account within 24 hour, for further process & support you can contact us on +91 79 4910 9999

 <br>
  Your registration reference id is :
<br>
<span style="font-weight:bold;">User Id: &nbsp;</span><span style="font-weight:lighter;" class=""><%=CID%></span> 
 <br>
  <span style="font-weight:bold;">Password: &nbsp;</span><span style="font-weight:lighter;" class=""><%=Password%></span>--%>
<br><br>  
<br></td>
</tr>
</tbody></table></center>
</td>
</tr>
<tr>
<td class="">
<div class="">
<a style="background-color:#674299;border-radius:4px;color:#fff;display:inline-block;font-family:Helvetica, Arial, sans-serif;font-size:18px;font-weight:normal;line-height:50px;text-align:center;text-decoration:none;width:350px;-webkit-text-size-adjust:none;" href="Login.aspx">Thank you!</a>
</div>
 <br>
</td>
</tr>
</tbody>
  
  </table>

<table bgcolor="#fff" cellpadding="0" cellspacing="0" class="force-full-width" style="margin: 0 auto; margin-bottom: 5px:">
<tbody>
<tr>
<td class="" style="color:#444;
                    ">
<p>The password was auto-generated, however feel free to change it 
  
    <a href="" style="text-decoration: underline;">
      here</a>
  
  </p>
  </td>
</tr>
</tbody></table></td>
</tr>
</tbody></table></center>
                  </td>
              </tr>
          </tbody>
      </table>

      <table width='450' border='0' cellspacing='0' cellpadding='0' height='84' style='background-color: #0e4ca1; display: none; border-radius: 15px; color: #FFF; font-family: Arial, Helvetica, sans-serif; font-size: 22px;'>
          <tr align='center' valign='middle'>
              <td>USER NAME:  <%=CID%> </td>
          </tr>
          <tr align='center' valign='middle'>
              <td>PASSWORD: <%=Password%></td>
          </tr>

      </table>
    </div>


    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
        var autoCity = UrlBase + "AutoComplete.asmx/GETCITYSTATE";
        $("#ddl_city").autocomplete({
            source: function (request, response) {

                //if ($("#ddl_city").val()!=data.d.item) {
                //    $("#ddl_city").focus();
                //    alert("Please Select appropriate city");
                //    return false;
                //}
                $.ajax({
                    url: autoCity,
                    data: "{ 'INPUT': '" + $("#ddl_state").val() + "','SEARCH': '" + request.term + "' }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {

                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item, value: item, id: $("#ddl_state").val() }
                            }))

                        }
                        else {
                            response([{ label: 'City Not Found', val: -1 }]);
                        }
                        //response($.map(data.d, function (item) {
                        //    return { label: item, value: item, id: $("#ddl_state").val() }
                        //}))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                        alert(textStatus);
                    }
                })
            },
            autoFocus: true,
            minLength: 3,
            select: function (event, ui) {
                if (ui.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            },
            autoFocus: true,
            minLength: 3,
            change: function (event, ui) {
                if (ui.item == null) {
                    this.value = '';
                    alert('Please select City from the City list');
                }
            }
        });

    </script>


    <script>

        $(function () {
            var $window = $(window),
                win_height_padded = $window.height() * 1.1,
                isTouch = Modernizr.touch;

            if (isTouch) { $('.revealOnScroll').addClass('animated'); }

            $window.on('scroll', revealOnScroll);

            function revealOnScroll() {
                var scrolled = $window.scrollTop(),
                    win_height_padded = $window.height() * 1.1;

                // Showed...
                $(".revealOnScroll:not(.animated)").each(function () {
                    var $this = $(this),
                        offsetTop = $this.offset().top;

                    if (scrolled + win_height_padded > offsetTop) {
                        if ($this.data('timeout')) {
                            window.setTimeout(function () {
                                $this.addClass('animated ' + $this.data('animation'));
                            }, parseInt($this.data('timeout'), 10));
                        } else {
                            $this.addClass('animated ' + $this.data('animation'));
                        }
                    }
                });
                // Hidden...
                $(".revealOnScroll.animated").each(function (index) {
                    var $this = $(this),
                        offsetTop = $this.offset().top;
                    if (scrolled + win_height_padded < offsetTop) {
                        $(this).removeClass('animated fadeInUp flipInX lightSpeedIn bounceInUp rollIn bounceInRight')
                    }
                });
            }

            revealOnScroll();
        });
    </script>
    <script>
        $('.palceholder').click(function () {
            $(this).siblings('input').focus();
            $(this).siblings('textarea').focus();
        });
        $('.input_bx').focus(function () {
            $(this).siblings('.palceholder').hide();
        });
        $('.input_bx').blur(function () {
            var $this = $(this);
            if ($this.val().length == 0)
                $(this).siblings('.palceholder').show();
        });
        $('.input_bx').blur();
    </script>
    <script src="Registration/Scripts/Js/User/custom-file-input.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>    
        CheckGStIsCheckOrNot();
        ChangeStateEvent();

        function ChangeCityEvent() {
            var ddlGSTCityval = document.getElementById("ddlGSTCity");
            document.getElementById("hdnSelectedCity").value = ddlGSTCityval.options[ddlGSTCityval.selectedIndex].text;
        }

        function ChangeStateEvent() {
            debugger;
            var ddlGSTStateval = document.getElementById("ddlGSTState").value;

            if (ddlGSTStateval != null) {
                var UrlBase = '<%=ResolveUrl("~/") %>';
                $.ajax({
                    url: UrlBase + "FareRuleService.asmx/FetchGSTStateList",
                    data: JSON.stringify({ cityCode: ddlGSTStateval }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var target = document.getElementById("ddlGSTCity");
                        target.innerHTML = "";

                        $(document.createElement('option'))

                            .attr('value', 'select')
                            .text('--Select City--')
                            .appendTo(target);


                        $(data.d).each(function () {
                            $(document.createElement('option'))

                                .attr('value', this.CityName)
                                .text(this.CityName)
                                .appendTo(target);
                        });

                    }
                });
            }
        }

    </script>
</body>
</html>
