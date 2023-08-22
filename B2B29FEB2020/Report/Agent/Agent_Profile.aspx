<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Agent_Profile.aspx.vb" Inherits="Reports_Agent_Agent_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .db-2-main-com {
            padding: 20px;
            position: relative;
            overflow: hidden;
        }

        .db-2-main-com-table tr {
            border-bottom: 1px solid #eaedef;
            line-height: 37px;
            padding: 10px;
        }

            .db-2-main-com-table tr td {
                padding: 16px 4px 13px 4px;
                font-size: 17.5px;
            }

        .db-2-main-com-table table {
            width: 100%;
        }

        .inputtext {
            width: 100%;
            border-radius: 4px;
            margin: 2px 0;
            outline: none;
            padding: 8px;
            box-sizing: border-box;
            transition: 0.3s;
            border: 1px solid #ccc;
        }
    </style>

    <script language="javascript" type="text/javascript">
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
        function checkpwd() {
            if (document.getElementById("").value != document.getElementById("").value) {
                alert('Please Enter Same Password');

            }
        }


        function PWSMATCH() {
            debugger;
            var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,16}$/;
            if (!regex.test(document.getElementById("ctl00_ContentPlaceHolder1_txt_password").value)) {
                alert("Password must contain:8-16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character'");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_password").focus();
                return false;
            }

            if (!regex.test(document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").value)) {
                alert("Password must contain:8-16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character'");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_password").value != document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").value) {
                alert('Please Enter Same Password');
                return false;
            }
        }



        function CheckAddress() {
            if ($("#ctl00_ContentPlaceHolder1_txtAgencyName").val() == "") {
                alert("Enter Agency Name");
                $("#ctl00_ContentPlaceHolder1_txtAgencyName").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txt_address").val() == "") {
                alert("Enter Address");
                $("#ctl00_ContentPlaceHolder1_txt_address").focus();
                return false;
            }


            //if ($("#ctl00_ContentPlaceHolder1_txt_country").val() == "") {
            //    alert("Enter Country Name");
            //    $("#ctl00_ContentPlaceHolder1_txt_country").focus();
            //    return false;
            //}


            if ($("#ctl00_ContentPlaceHolder1_ddlCountry").val() == "Other") {
                if ($("#ctl00_ContentPlaceHolder1_txt_state").val() == "") {
                    alert("Enter State");
                    $("#ctl00_ContentPlaceHolder1_txt_state").focus();
                    return false;
                }

                if ($("#ctl00_ContentPlaceHolder1_txt_city").val() == "") {
                    alert("Enter City");
                    $("#ctl00_ContentPlaceHolder1_txt_city").focus();
                    return false;
                }
            }
            else {

                if ($("#ctl00_ContentPlaceHolder1_ddlState").val() == "0") {
                    alert("Select State");
                    $("#ctl00_ContentPlaceHolder1_ddlState").focus();
                    return false;
                }

                if ($("#ctl00_ContentPlaceHolder1_ddlCity").val() == "0") {
                    alert("Select City");
                    $("#ctl00_ContentPlaceHolder1_ddlCity").focus();
                    return false;
                }
            }

            if ($("#ctl00_ContentPlaceHolder1_txtPincode").val() == "") {
                alert("Enter Pincode");
                $("#ctl00_ContentPlaceHolder1_txtPincode").focus();
                return false;
            }
        }

        function ShowHideGst() {
            var radioButtons = $('#<%=RbtApplied.ClientID%>');
            var Applied = radioButtons.find('input:checked').val();
            if (Applied == "False") {
                $("#ctl00_ContentPlaceHolder1_GST").hide();
                $("#ctl00_ContentPlaceHolder1_GST1").hide();
                $("#ctl00_ContentPlaceHolder1_GST2").hide();
                $("#ctl00_ContentPlaceHolder1_GST3").hide();
                $("#ctl00_ContentPlaceHolder1_GST4").hide();
                $("#ctl00_ContentPlaceHolder1_GST5").hide();
                $("#ctl00_ContentPlaceHolder1_GST6").hide();
                $("#ctl00_ContentPlaceHolder1_GST7").hide();
                $("#ctl00_ContentPlaceHolder1_GST8").show();
            }
            else {
                $("#ctl00_ContentPlaceHolder1_GST").show();
                $("#ctl00_ContentPlaceHolder1_GST1").show();
                $("#ctl00_ContentPlaceHolder1_GST2").show();
                $("#ctl00_ContentPlaceHolder1_GST3").show();
                $("#ctl00_ContentPlaceHolder1_GST4").show();
                $("#ctl00_ContentPlaceHolder1_GST5").show();
                $("#ctl00_ContentPlaceHolder1_GST6").show();
                $("#ctl00_ContentPlaceHolder1_GST7").show();
                $("#ctl00_ContentPlaceHolder1_GST8").hide();
            }
        }
        function CheckGST() {
            var radioButtons = $('#<%=RbtApplied.ClientID%>');
            var Applied = radioButtons.find('input:checked').val();
            if (Applied == "True") {
                if ($("#ctl00_ContentPlaceHolder1_TxtGSTNo").val() == "") {
                    alert("Enter GST No.");
                    $("#ctl00_ContentPlaceHolder1_TxtGSTNo").focus();
                    return false;
                }
                if (gStValidate($("#ctl00_ContentPlaceHolder1_TxtGSTNo").val())) {
                    if ($("#ctl00_ContentPlaceHolder1_TxtGSTCompanyName").val() == "") {
                        alert("Enter GST  company name.");
                        $("#ctl00_ContentPlaceHolder1_TxtGSTCompanyName").focus();
                        return false;
                    }
                    if ($("#ctl00_ContentPlaceHolder1_TxtGSTAddress").val() == "") {
                        alert("Enter GST  company Address");
                        $("#ctl00_ContentPlaceHolder1_TxtGSTAddress").focus();
                        return false;
                    }


                    if ($("#ctl00_ContentPlaceHolder1_ddlStateGst").val() == "select") {
                        alert("Select GST State");
                        $("#ctl00_ContentPlaceHolder1_ddlStateGst").focus();
                        return false;
                    }

                    if ($("#ctl00_ContentPlaceHolder1_ddlCityGst").val() == "select") {
                        alert("Select GST City");
                        $("#ctl00_ContentPlaceHolder1_ddlCityGst").focus();
                        return false;
                    }

                    if ($("#ctl00_ContentPlaceHolder1_txtPincodeGst").val() == "") {
                        alert("Ënter pincode");
                        $("#ctl00_ContentPlaceHolder1_txtPincodeGst").focus();
                        return false;
                    }

                    if ($("#ctl00_ContentPlaceHolder1_TxtGSTPhoneNo").val() == "") {
                        alert("Enter phone no.");
                        $("#ctl00_ContentPlaceHolder1_TxtGSTPhoneNo").focus();
                        return false;
                    }

                    if ($("#ctl00_ContentPlaceHolder1_TxtGSTEmail").val() == "") {
                        alert("Enter email.");
                        $("#ctl00_ContentPlaceHolder1_TxtGSTEmail").focus();
                        return false;
                    }
                    if (!isValidEmailAddress($("#ctl00_ContentPlaceHolder1_TxtGSTEmail").val())) {
                        alert("Enter valid email id.");
                        $("#ctl00_ContentPlaceHolder1_TxtGSTEmail").focus();
                        return false;
                    }
                }
                else {
                    alert("Enter valid GST No.");
                    $("#ctl00_ContentPlaceHolder1_TxtGSTNo").focus();
                    return false;
                }
            }
            else {
                if ($("#ctl00_ContentPlaceHolder1_TxtGSTRemark").val() == "") {
                    alert("Enter remark.");
                    $("#ctl00_ContentPlaceHolder1_TxtGSTRemark").focus();
                    return false;
                }
            }


        }


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
                    if (isNumberKey1(gstRegisterationNumbr) == false) {
                        gstValid = false;
                    }
                }
                else { gstValid = false; }
                if (gstDefaultNo.toString().toLowerCase() != "Z".toLowerCase()) {
                    gstValid = false;
                }
                if (gstCheckCode.length == 1) {
                    if (isNumberKey1(gstCheckCode) == false) {
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


        function isValidEmailAddress(emailAddress) {
            var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
            return pattern.test(emailAddress);
        };


    </script>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <ol class="breadcrumb-arrow">
                <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
                <li><a href="#">Profile</a></li>


            </ol>

            <div style="margin-top: 10px;" id="divTds" runat="server">
                <h2 style="color: #000; text-align: center">
                    <asp:LinkButton ID="lnk_tds" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#20313f" Font-Underline="True">Click here to download your TDS Certificate(2013-2014)
                    </asp:LinkButton></h2>
            </div>



            <div class="row">

                <div class="col-md-12 profile-box">
                    <div class="panel panel-info" id="trLogin" runat="server">
                        <div class="panel-heading">
                            <span class="lft">Login Details </span><span class="rgt">
                                <asp:LinkButton ID="LinkEdit" runat="server" Font-Bold="True" CssClass="rgt" Style="float: right"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton></span>
                        </div>



                        <div class="db-2-main-com db-2-main-com-table" id="trLoginDetails" runat="server">

                            <table class="responsive-table">
                                <tbody>

                                    <tr>
                                        <td><b>User Name</b></td>
                                        <td>:</td>
                                        <td id="td_username" runat="server"></td>
                                    </tr>

                                    <asp:HiddenField ID="oldpasshndfld" runat="server" />
                                    <tr id="td_login" runat="server">

                                        <td><b>Password</b></td>
                                        <td>:</td>
                                        <td>******</td>
                                    </tr>

                                    <tr class="" id="Div1" runat="server">
                                        <td><b>Logo</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:Image ID="Image111" runat="server" Height="70px" Width="90px" /></td>
                                    </tr>

                                    <tr class="" id="Div2" runat="server">
                                        <td><b>Logo Upload</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />


                                        </td>
                                        <td>
                                            <div class="btn-upload">
                                                <asp:Button ID="button_upload" runat="server" Text="Upload" CssClass="btn cmn-btn" />
                                            </div>
                                        </td>

                                        <td>
                                            <span class="text1" style="font-size: 13px; font-family: arial, Helvetica, sans-serif; font-weight: bold;">Note : </span>Image formate must be in JPEG.

                                        </td>
                                    </tr>

                                    <tr>
                                        <td><b>Status</b></td>
                                        <td>:</td>
                                        <td><span class="db-done">Active</span>
                                        </td>
                                    </tr>


                                    <div id="td_login1" runat="server" visible="false">


                                        <tr>
                                            <td><b>Old Password</b></td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txt_oldpassword" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td><b>Password</b></td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txt_password" MaxLength="16" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td><b>Confirm Password</b></td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txt_cpassword" MaxLength="16" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:Label ID="lbl_msg" runat="server" ForeColor="Red"></asp:Label>
                                                <%-- <div class="clear"></div>
                                 <div class="col-md-4 col-xs-6">
                                   
                                 </div>--%>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td class="btn-profile">

                                                <div class="btn-save">
                                                    <asp:Button ID="btn_Save" runat="server" OnClientClick="return PWSMATCH()" Text="Save" CssClass="btn cmn-btn" />
                                                </div>

                                                <div class="btn-cancel">
                                                    <asp:LinkButton ID="lnk_Cancel" runat="server" CssClass="">Cancel</asp:LinkButton>
                                                </div>

                                            </td>
                                        </tr>



                                    </div>


                                    <%--<table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td width="140px" colspan="2" style="height: 18px" align="left">&nbsp;<asp:Image ID="Image111" runat="server" Height="70px" Width="90px" />
                                    </td>
                                </tr>
                                <tr valign="middle">
                                    <td valign="middle" height="25" class="text1" align="left" style="padding-left: 10px;">Upload Logo
                                    </td>
                                    <td>

                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            &nbsp;<asp:Button ID="button_upload" runat="server" Text="Upload" />
                                        </td>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="25px" style="padding-left: 10px; color: #FF3300; font-size: 11px;"
                                        align="left">
                                        <span class="text1" style="font-size: 13px; font-family: arial, Helvetica, sans-serif; font-weight: bold;">Note : </span>Image formate must be in JPEG and Size should
                                                be (90*70) pixels
                                    </td>
                                </tr>
                            </table>--%>
                                </tbody>
                            </table>
                        </div>

                    </div>

                    <div class="  panel panel-info">
                        <div class="panel-heading">
                            <span class="lft" style="color: #fff;">Personal Details</span>
                            <span class="rgt">
                                <asp:LinkButton ID="LinkPersonalEdit" runat="server" Font-Bold="True"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton></span>
                        </div>

                        <div class="db-2-main-com db-2-main-com-table">
                            <table class="table-responsive">
                                <tbody id="td_PDetails" runat="server">
                                    <tr>
                                        <td><b>Name</b></td>
                                        <td>:</td>
                                        <td id="td_Name" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td><b>Email ID</b></td>
                                        <td>:</td>
                                        <td id="td_EmailID" runat="server"></td>
                                    </tr>

                                    <tr>
                                        <td><b>Mobile No </b></td>
                                        <td>:</td>
                                        <td id="td_Mobile" runat="server"></td>
                                    </tr>
                                    <div id="trAlternateEmailID" runat="server">
                                        <div class="col-md-2 col-xs-4">Alternate EmailID :</div>
                                        <div class="col-md-3 col-xs-8" id="td_AlternateEmailID" runat="server"></div>
                                    </div>
                                    <div class="clear"></div>

                                    <div id="tr_Landline" runat="server">
                                        <div class="col-md-2 col-xs-4"><b>Landline : </b></div>
                                        <div class="col-md-3 col-xs-8" id="td_Landline" runat="server"></div>
                                    </div>

                                    <div class="clear"></div>
                                    <div id="tr_PanCard" runat="server">
                                        <div class="col-md-2 col-xs-4">PanCard No :</div>
                                        <div class="col-md-3 col-xs-8" id="td_Pan" runat="server"></div>
                                    </div>

                                    <div class="clear"></div>
                                    <div id="tr_Fax" runat="server">
                                        <div class="col-md-2 col-xs-4">Fax :</div>
                                        <div class="col-md-3 col-xs-8" id="td_Fax" runat="server"></div>
                                    </div>
                                    <div class="clear"></div>

                                    <div id="td_PDetails1" runat="server" visible="false">
                                        <div class="col-md-2 col-xs-4">Name :</div>
                                        <div class="col-md-3 col-xs-8" id="td_Name1" runat="server"></div>
                                        <div class="clear"></div>
                                        <div class="col-md-2 col-xs-4">Email :</div>
                                        <div class="col-md-3 col-xs-8" id="td_Email1" runat="server"></div>
                                        <div class="clear"></div>
                                        <div class="col-md-2 col-xs-4">Mobile No :</div>
                                        <div class="col-md-3 col-xs-8" id="td_Mobile1" runat="server"></div>
                                        <div class="clear"></div>
                                        <div class="col-md-2 col-xs-4">
                                            <asp:Button ID="btn_SavePDetails" runat="server" Text="Save" />
                                            &nbsp;Or&nbsp;<asp:LinkButton ID="lnk_CancelPDetails" runat="server" CssClass="cancelprofile"
                                                Font-Bold="False" Font-Underline="True">Cancel</asp:LinkButton>
                                        </div>
                                    </div>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="  panel panel-info">
                        <div class="panel-heading">
                            <span class="lft">Address </span>
                            <span class="rgt" style="float: right;">
                                <asp:LinkButton ID="LinkEditAdd" runat="server" ToolTip="Click for change address details" Font-Bold="True" CssClass="rgt"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton>
                            </span>
                        </div>

                        <div class="db-2-main-com db-2-main-com-table">

                            <table class="responsive-table address-table">
                                <tbody id="td_Address" runat="server">

                                    <tr>
                                        <td><b>Agency Name</b></td>
                                        <td>:</td>
                                        <td id="tdAgencyName" runat="server"></td>
                                    </tr>

                                    <tr>
                                        <td><b>Address</b></td>
                                        <td>:</td>
                                        <td id="td_Add" runat="server"></td>
                                    </tr>

                                    <tr>
                                        <td><b>City</b></td>
                                        <td>:</td>
                                        <td id="td_City" runat="server"></td>
                                    </tr>

                                    <tr>
                                        <td><b>District</b></td>
                                        <td>:</td>
                                        <td id="tdDistrict" runat="server"></td>
                                    </tr>


                                    <tr>
                                        <td><b>Pincode</b></td>
                                        <td>:</td>
                                        <td id="tdPinCode" runat="server"></td>
                                    </tr>


                                    <tr>
                                        <td><b>State</b></td>
                                        <td>:</td>
                                        <td id="td_State" runat="server"></td>
                                    </tr>

                                    <tr>
                                        <td><b>Country</b></td>
                                        <td>:</td>
                                        <td id="td_Country" runat="server"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <div class="db-2-main-com db-2-main-com-table">
                            <table class="table-responsive">
                                <tbody id="td_Address1" runat="server" visible="false">

                                    <tr>
                                        <td><b>Agency Name :</b></td>
                                        <td>
                                            <asp:TextBox ID="txtAgencyName" MaxLength="50" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>Address :</td>
                                        <td>
                                            <asp:TextBox ID="txt_address" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>Country :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Value="India" Text="India"></asp:ListItem>
                                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>State :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txt_state" runat="server" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>City :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:TextBox ID="txt_city" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>District :</td>
                                        <td>
                                            <asp:TextBox ID="txtDistrict" runat="server" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Pincode :</td>
                                        <td>
                                            <asp:TextBox ID="txtPincode" runat="server" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="8"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="btn-profile">
                                            <div class="btn-save">
                                                <asp:Button ID="btn_Saveadd" CssClass="btn cmn-btn" OnClientClick="return CheckAddress();" runat="server" Text="Save" />
                                            </div>

                                            <div class="btn-cancel">
                                                <asp:Button ID="lnk_CancelAdd" runat="server" Text="Cancel" CssClass="cancel" />
                                            </div>

                                        </td>

                                    </tr>

                                    <div class="clear"></div>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="  panel panel-info gst-profile">
                        <div class="panel-heading">
                            <span class="lft" style="color: #fff;">GST Details  </span>
                            <span class="rgt" style="float: right">
                                <asp:LinkButton ID="LinkBtnGstUpdate" runat="server" ToolTip="Click for update GST details" Font-Bold="True"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton>
                            </span>
                        </div>


                        <div class="db-2-main-com db-2-main-com-table">
                            <table class="responsive-table gst-table">
                                <tbody id="tdGst" runat="server">

                                    <tr>
                                        <td><b>GST Applied</b></td>
                                        <td>:</td>
                                        <td id="tdGstApplied" runat="server"></td>
                                    </tr>

                                    <tr id="HGST1" runat="server">
                                        <td><b>GST NO</b></td>
                                        <td>:</td>
                                        <td id="tdGSTNO" runat="server"></td>
                                    </tr>

                                    <tr id="HGST2" runat="server">
                                        <td><b>Company Name</b></td>
                                        <td>:</td>
                                        <td id="tdGST_Company_Name" runat="server"></td>
                                    </tr>

                                    <tr id="HGST3" runat="server">
                                        <td><b>Company Address</b></td>
                                        <td>:</td>
                                        <td id="tdGST_Company_Address" runat="server"></td>
                                    </tr>

                                    <tr id="HGST4" runat="server">
                                        <td><b>City</b></td>
                                        <td>:</td>
                                        <td id="tdCityGst" runat="server"></td>
                                    </tr>

                                    <tr id="HGST5" runat="server">
                                        <td><b>State</b></td>
                                        <td>:</td>
                                        <td id="tdStateGst" runat="server"></td>
                                    </tr>

                                    <tr id="HGST6" runat="server">
                                        <td><b>Pincode</b></td>
                                        <td>:</td>
                                        <td id="tdPincodeGst" runat="server"></td>
                                    </tr>

                                    <tr id="HGST7" runat="server">
                                        <td><b>Phone No</b></td>
                                        <td>:</td>
                                        <td id="tdGST_PhoneNo" runat="server"></td>
                                    </tr>

                                    <tr id="HGST8" runat="server">
                                        <td><b>Email</b></td>
                                        <td>:</td>
                                        <td id="tdGST_Email" runat="server"></td>
                                    </tr>

                                    <tr id="HGST9" runat="server">
                                        <td><b>Remark</b></td>
                                        <td>:</td>
                                        <td id="tdGSTRemark" runat="server"></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>




                        <div id="tdGstUpdate" class="profile-gst" runat="server" visible="false">
                            <div class="col-md-8 col-xs-12">
                                <div class="col-md-2 col-xs-4"><b>GST Apply :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:RadioButtonList ID="RbtApplied" runat="server" RepeatDirection="horizontal" onclick="ShowHideGst();">
                                        <asp:ListItem Value="True" Selected="True">Applied</asp:ListItem>
                                        <asp:ListItem Value="False">Not Applied</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="clear"></div>

                            <div id="GST" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>GST No. :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTNo" runat="server" MaxLength="15" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz1234567890');" Style="text-transform:uppercase;"></asp:TextBox>
                                </div>

                            </div>

                            <div id="GST1" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Company Name :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTCompanyName" runat="server" CssClass="inputtext" MaxLength="70" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz');"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST2" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Company Address :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTAddress" runat="server" Height="50px" TextMode="MultiLine" MaxLength="65" CssClass="inputtext" onkeypress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz/1234567890');"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST3" runat="server" class="col-md-6 col-xs-12" style="min-height: 40px;">
                                <div class="col-md-4 col-xs-4"><b>State :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:DropDownList ID="ddlStateGst" runat="server" CssClass="form-controler inputtext" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlStateGst_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div id="GST4" runat="server" class="col-md-6 col-xs-12" style="min-height: 40px;">
                                <div class="col-md-4 col-xs-4"><b>City :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:DropDownList ID="ddlCityGst" CssClass="form-controler inputtext" runat="server"></asp:DropDownList>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST5" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Pincode :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="txtPincodeGst" runat="server" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST6" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Phone No :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTPhoneNo" runat="server" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST7" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Email :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTEmail" runat="server" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div id="GST8" runat="server" class="col-md-6 col-xs-12">
                                <div class="col-md-4 col-xs-4"><b>Remark :</b></div>
                                <div class="col-md-8 col-xs-8">
                                    <asp:TextBox ID="TxtGSTRemark" runat="server" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="clear"></div>
                            </div>
                            <div class="col-md-12 col-xs-12 btn-profile">
                                <br />
                                <div class="btn-save">
                                    <asp:Button ID="BtnGstSave" CssClass="btn cmn-btn" runat="server" OnClientClick="return CheckGST();" Text="Save" />
                                    <asp:Button ID="BtnGstCancel" runat="server" Text="Cancel" CssClass="btn cmn-btn" /></td>
                                </div>
                                <br />
                            </div>

                        </div>
                        <div class="clear"></div>
                    </div>

                    <br />
                    <br />
                    <br />
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="button_upload" />
            <asp:PostBackTrigger ControlID="lnk_tds" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
            </div>
            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                Please Wait....<br />
                <br />
                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
