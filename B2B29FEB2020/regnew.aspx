<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="regnew.aspx.vb" Inherits="regnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .w40 {
            width: 40% !important;
        }

        .rthead {
            font-size: 25px;
            color: #fff;
            background-color: #ed131c;
            width: 100%;
            text-align: center;
            padding: 10px;
        }

        .rtmainbgs {
            /*background: #fff;*/
            border-radius: 4px;
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .rtbg {
            background-color: #0e4ca2;
        }

        .form-controlrt {
            display: block;
            width: 100%;
            height: 40px !important;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 0px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }

        .whtbg {
            background-color: #fff;
        }

        .headingregs {
            background-color: #e2e2e2;
            height: 40px;
            width: 100% !important;
            padding: 1px 10px;
            color: #000;
            font-size: 18px;
            margin-bottom: 10px;
        }
    </style>

    <link rel="stylesheet" href="chosen/chosen.css" />

    <script src="chosen/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script src="chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="CSS/jquery-ui-1.8.8.custom.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>

    <script type="text/javascript">
        function RefreshCaptcha() {
            var img = document.getElementById("imgCaptcha");
            img.src = "CAPTCHA.ashx?query=" + Math.random();
        }
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <%-- <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    ---%>

    <script language="javascript" type="text/javascript">
        function validateSearch() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_Fname_txt").value == "") {
                alert('Specify First Name');
                document.getElementById("ctl00_ContentPlaceHolder1_Fname_txt").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Lname_txt").value == "") {
                alert('Specify Last Name');
                document.getElementById("ctl00_ContentPlaceHolder1_Lname_txt").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Add_txt").value == "") {
                alert('Specify Address');
                document.getElementById("ctl00_ContentPlaceHolder1_Add_txt").focus();
                return false;
            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_country").value == "India") {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_state").value == "--Select State--") {
                    alert('Please Select State');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_state").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_city").value == "") {
                    alert('Please Select City');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_city").focus();
                    return false;
                }

            }
            else {
                if (document.getElementById("ctl00_ContentPlaceHolder1_Coun_txt").value == "") {
                    alert('Specify Country Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_Coun_txt").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_Stat_txt").value == "") {
                    alert('Specify State Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_Stat_txt").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_Other_City").value == "") {
                    alert('Specify City Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_Other_City").focus();
                    return false;

                }

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Pin_txt").value == "") {
                alert('Specify Pincode');
                document.getElementById("ctl00_ContentPlaceHolder1_Pin_txt").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_Mob_txt").value == "") {
                alert('Specify Mobile Number');
                document.getElementById("ctl00_ContentPlaceHolder1_Mob_txt").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Email_txt").value == "") {
                alert('Specify EmailID');
                document.getElementById("ctl00_ContentPlaceHolder1_Email_txt").focus();
                return false;
            }

            var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            var emailid = document.getElementById("ctl00_ContentPlaceHolder1_Email_txt").value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Your email address seems incorrect. Please try again.");
                document.getElementById("ctl00_ContentPlaceHolder1_Email_txt").focus();
                return false;
            }


if (document.getElementById("ctl00_ContentPlaceHolder1_Sales_DDL").value == "Select Sales Ref.") {
                    alert('Please Select Ref Code');
                    document.getElementById("ctl00_ContentPlaceHolder1_Sales_DDL").focus();
                    return false;
                }


           // if (document.getElementById("ctl00_ContentPlaceHolder1_Agn_txt").value == "") {
             //   alert('Specify Agency Name');
               // document.getElementById("ctl00_ContentPlaceHolder1_Agn_txt").focus();
                //return false;
            //}
            //bipin
            //if (document.getElementById("ctl00_ContentPlaceHolder1_TextBox_NameOnPard").value == "") {
            //  alert('Specify Name on Pan Card');
            //document.getElementById("ctl00_ContentPlaceHolder1_TextBox_NameOnPard").focus();
            //return false;
            //}

            //if (document.getElementById("ctl00_ContentPlaceHolder1_Pan_txt").value == "") {
            //  alert('Specify Pan No');
            //document.getElementById("ctl00_ContentPlaceHolder1_Pan_txt").focus();
            // return false;
            //}
            //if (document.getElementById("ctl00_ContentPlaceHolder1_Ans_txt").value == "") {
            //  alert('Specify Answer');
            //document.getElementById("ctl00_ContentPlaceHolder1_Ans_txt").focus();
            //return false;
            //}


            if (document.getElementById("ctl00_ContentPlaceHolder1_TxtUserId").value == "") {
                alert('Specify Userid');
                document.getElementById("ctl00_ContentPlaceHolder1_TxtUserId").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").value == "") {
                alert('Specify Password');
                document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").focus();
                return false;
            }
            else {
                var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,16}$/;
                if (!regex.test(document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").value)) {
                    alert("Password must contain:8 To 16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character'");
                    document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").focus();
                    return false;
                }

                // var patt = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8}");
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value == "") {
                alert('Specify Confirm Password');
                document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value != "") {
                debugger;
                if (document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").value != document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value) {
                    alert("Confirm Password is same as Password");
                    document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").focus();
                    return false;
                }

                if (confirm("Are you sure!"))
                    return true;
                return false;
            }

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

    <div class="rtbg">
        <div class="container" style="line-height: 25px; padding-bottom: 10px;">
            <div class="row rtmainbgs">
                <div class="rthead"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>Register Now</div>
                <div class="w100" id="table_reg" runat="server"
                    visible="true" style="margin-top: 0px; padding-bottom: 20px;">
                    <div class="row">

                        <div class="heading w80 auto">
                            <div align="center">
                                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="20px"
                                    ForeColor="#FF3300"></asp:Label>
                            </div>
                            <div class="clear1"></div>

                            <div class="col-lg-12 col-md-12 col-xs-12">

                                <div class="w100 whtbg">
                                    <div class="headingregs">
                                        <h4>Personal Information</h4>
                                    </div>
                                    <div class="col-md-12">
                                    </div>
                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Title:<span style="color: #990000">*</span>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:DropDownList ID="tit_drop" runat="server" CssClass="lft w40 form-controlrt">
                                            <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        First Name:<span style="color: #990000">*</span>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:TextBox ID="Fname_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="30" Style="position: static"
                                            onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                    </div>


                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Last Name:<span style="color: #990000">*</span>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:TextBox ID="Lname_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="30" Style="position: static;"
                                            onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                    </div>
                                    <div class="clear1">
                                    </div>
                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Address:<span style="color: #990000">*</span>
                                    </div>

                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:TextBox ID="Add_txt" runat="server" CssClass="form-controlrt" Style="height: 50px;"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Country:
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:DropDownList ID="ddl_country" runat="server" CssClass="form-controlrt" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="India">India</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="Coun_txt" CssClass="psb_dd form-controlrt " runat="server" Style="position: static"
                                            Visible="false" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        State*:
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:DropDownList ID="ddl_state" runat="server" AutoPostBack="True" CssClass=" form-controlrt">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="Stat_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');"
                                            Visible="false"></asp:TextBox>
                                    </div>

                                    <div class="clear1"></div>


                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        City:<span style="color: #990000">*</span>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <input type="text" id="ddl_city" runat="server" class="psb_dd form-controlrt" style="position: static" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');" />
                                        <asp:TextBox ID="Other_City" CssClass="psb_dd form-controlrt " runat="server" Style="position: static"
                                            Visible="false" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                    </div>


                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Area:
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:TextBox ID="TextBox_Area" CssClass="psb_dd form-controlrt" runat="server" MaxLength="20" Style="position: static"
                                            onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-1 col-sm-1 col-xs-3">
                                        Pincode*:
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-9">
                                        <asp:TextBox ID="Pin_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="8" Style="position: static"
                                            onkeypress="return keyRestrict(event,'0123456789');"></asp:TextBox>
                                    </div>

                                    <div class="clear1">
                                    </div>
                                </div>

                                <div class="clear"></div>

                                <div class="w100 whtbg">
                                    <div class="headingregs">
                                        <h4>Contact Information</h4>
                                    </div>
                                    <div class="col-md-12">


                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Phone No.:
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Ph_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="15" Style="position: static"
                                                onkeypress="return keyRestrict(event,'0123456789');"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Mobile:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Mob_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="10" Style="position: static"
                                                onkeypress="return keyRestrict(event,'0123456789');"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-3 col-xs-3">
                                            Email Id:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Email_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>

                                        <div class="clear1"></div>
                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Email 2:
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Aemail_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                             Ref Code:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:DropDownList ID="Sales_DDL" runat="server" CssClass="form-controlrt">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="Ref_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static" Visible="false" ></asp:TextBox>
                                            <asp:TextBox ID="Fax_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static" Visible="false" onkeypress="return keyRestrict(event,'0123456789');"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="clear"></div>

                                <div class="w100 whtbg" style="display: none;">
                                    <div class="headingregs">
                                        <h4>Business Information</h4>
                                    </div>
                                    <div class="col-md-12">


                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Business Name:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3  col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Agn_txt" CssClass="psb_dd form-controlrt" runat="server" MaxLength="50" onkeypress="return keyRestrict(event,' .abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                           
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            
                                        </div>

                                        <div class="col-lg-1  col-sm-1 col-xs-3">
                                        </div>
                                    </div>
                                </div>

                                <div class="w100 whtbg" style="display: none;">
                                    <div class="headingregs">
                                        <h4>Business Information</h4>
                                    </div>
                                    <div class="col-md-12">


                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Business Name:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3  col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Agn_txt_new" CssClass="psb_dd form-controlrt" runat="server" MaxLength="50" onkeypress="return keyRestrict(event,' .abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Website:
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Web_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1  col-sm-1 col-xs-3">
                                            Stax No:
                                        </div>

                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Stax_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>
                                        <div class="clear1"></div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Pan No:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Pan_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Name On Pan Card:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="TextBox_NameOnPard" CssClass="psb_dd form-controlrt" runat="server" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz');" Style="position: static" MaxLength="20"></asp:TextBox>
                                        </div>


                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Pan Image*:  
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:FileUpload ID="fld_pan" runat="server" CssClass="psb_dd" Height="22px" />

                                            <div style="display: none;">
                                                <asp:DropDownList ID="Stat_drop" CssClass="form-controlrt" runat="server" Height="20px" Width="150px">
                                                    <asp:ListItem Value="TA" Selected="True">Travel Agent</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="" style="font-size: 11px; color: #0e4faa; font-weight: bold">
                                                ( Pancard image must be in JPG formate )
                                            </div>

                                        </div>
                                        <div class="clear1"></div>
                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Ref. By :
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            
                                        </div>


                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Remark:
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Rem_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Upload Logo : 
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:FileUpload ID="fld_1" runat="server" CssClass="psb_dd" />
                                            <div style="font-size: 11px; color: #0e4faa; font-weight: bold">( Image must be in JPG formate and  Size should be (90*70) pixels)</div>

                                        </div>

                                        <div class="clear1"></div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Security Question:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-9 col-xs-9">
                                            <asp:DropDownList ID="SecQ_drop" CssClass="form-controlrt" runat="server">
                                                <asp:ListItem Value="What is Your Pet Name?">Mr.What is Your Pet Name?</asp:ListItem>
                                                <asp:ListItem Value="What is your Favourite Color?">What is your Favourite Color?</asp:ListItem>
                                                <asp:ListItem Value="What is Your Date of Birth">What is Your Date of Birth</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Security Answer:<span style="color: #990000">*</span>&nbsp;
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Ans_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"></asp:TextBox>
                                        </div>
                                        <div class="clear1"></div>

                                    </div>

                                    <div class="clear"></div>
                                </div>
                                <div class="clear"></div>

                                <div class="w100 whtbg">
                                    <div class="headingregs">
                                        <h4>Authentication Information</h4>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            User Id:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="TxtUserId" runat="server" Style="position: static" MaxLength="20" CssClass="psb_dd form-controlrt" oncopy="return false" onpaste="return false" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz1234567890');"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Password:<span style="color: #990000">*</span>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="Pass_text" runat="server" Style="position: static"
                                                TextMode="Password" MaxLength="16" CssClass="psb_dd form-controlrt"></asp:TextBox>

                                            <div style="font-size: 11px; color: #0e4faa; font-weight: bold">Eg. PaSS@123(not more than 8-16 charecter)</div>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 col-xs-3">
                                            Confirm Password:<span style="color: #990000">*</span>&nbsp;
                                        </div>
                                        <div class="col-lg-3 col-sm-3 col-xs-9">
                                            <asp:TextBox ID="cpass_txt" CssClass="psb_dd form-controlrt" runat="server" Style="position: static"
                                                TextMode="Password" MaxLength="16"></asp:TextBox>
                                        </div>
                                        <div class="clear1"></div>
                                        <div class="col-lg-2 col-sm-2 col-xs-6">Captcha Information:*</div>
                                        <div class="col-lg-2 col-sm-2 col-xs-6">
                                            <img src="CAPTCHA.ashx" id="imgCaptcha" />
                                            &nbsp;<a onclick="javascript:RefreshCaptcha();" style="cursor: pointer"><img src="Images/refresh.png" /></a>
                                        </div>

                                        <div class="col-lg-2 col-sm-2 col-xs-6">Enter Text from Image*</div>
                                        <div class="col-lg-2 col-sm-2 col-xs-6" style="margin-bottom: 21px;">
                                            <asp:TextBox ID="TextBox1" CssClass="form-controlrt" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-2 col-sm-3 col-xs-6 col-lg-push-1">
                                            <asp:Button ID="submit" Width="250" runat="server" Text="Submit" OnClientClick="return validateSearch()"
                                                CssClass="btnsss" />
                                        </div>

                                    </div>
                                    <div class="clear1"></div>
                                </div>

                                <div class="clear"></div>

                            </div>


                        </div>

                    </div>

                </div>
                <div id="table_Message" runat="server" visible="false" style="background:white;">

                    <%--<div id="divmsg" runat="server"></div>--%>
                   <%-- <div class="autoss">
                        Thanks You....!
                    </div>

                    <div class="w80 auto">
                        <div class="w100">


                            <div class="regss">
                                <b>Your user Id is : - </b>
                                <%=CID%>  Your Id is active<br />
                            </div>
                            <div class="clear1"></div>

                            <div class="regss">
                                <b>Please contact on : - </b>+91-11-48 444 444 for activation.
                            </div>


                        </div>

                    </div>--%>

                </div>

            </div>

        </div>
    </div>
    <%--<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>--%>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
        var autoCity = UrlBase + "AutoComplete.asmx/GETCITYSTATE";
        $("#ctl00_ContentPlaceHolder1_ddl_city").autocomplete({
            source: function (request, response) {

                //if ($("#ctl00_ContentPlaceHolder1_ddl_city").val()!=data.d.item) {
                //    $("#ctl00_ContentPlaceHolder1_ddl_city").focus();
                //    alert("Please Select appropriate city");
                //    return false;
                //}
                $.ajax({
                    url: autoCity,
                    data: "{ 'INPUT': '" + $("#ctl00_ContentPlaceHolder1_ddl_state").val() + "','SEARCH': '" + request.term + "' }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {

                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item, value: item, id: $("#ctl00_ContentPlaceHolder1_ddl_state").val() }
                            }))

                        }
                        else {
                            response([{ label: 'City Not Found', val: -1 }]);
                        }
                        //response($.map(data.d, function (item) {
                        //    return { label: item, value: item, id: $("#ctl00_ContentPlaceHolder1_ddl_state").val() }
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

</asp:Content>

