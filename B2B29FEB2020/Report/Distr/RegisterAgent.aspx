<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="RegisterAgent.aspx.vb" Inherits="SprReports_Distr_RegisterAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <%--<style>
        input[type="text"], input[type="password"], select, textarea
        {
            border: 1px solid #808080;
        }
    </style>--%>
<style>
        input[type="text"], input[type="password"], select ,textarea
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            
        }
    </style>
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
            if (document.getElementById("ctl00_ContentPlaceHolder1_City_txt").value == "") {
                alert('Specify City Name');
                document.getElementById("ctl00_ContentPlaceHolder1_City_txt").focus();
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

            if (document.getElementById("ctl00_ContentPlaceHolder1_Agn_txt").value == "") {
                alert('Specify Agency Name');
                document.getElementById("ctl00_ContentPlaceHolder1_Agn_txt").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Pan_txt").value == "") {
                alert('Specify Pan No');
                document.getElementById("ctl00_ContentPlaceHolder1_Pan_txt").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_Ans_txt").value == "") {
                alert('Specify Answer');
                document.getElementById("ctl00_ContentPlaceHolder1_Ans_txt").focus();
                return false;
            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").value == "") {
                alert('Specify Password');
                document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value == "") {
                alert('Specify Confirm Password');
                document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value != "") {
                if (document.getElementById("ctl00_ContentPlaceHolder1_Pass_text").value != document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").value) {
                    alert("Confirm Password is same as Password");
                    document.getElementById("ctl00_ContentPlaceHolder1_cpass_txt").focus();
                    return false;
                }

                if (confirm("Are you sure!"))
                    return true;
                return false;

            }

            //            alert_message = "";
            //            formObj = document.forms["flight"];
            //            var now = new Date();
            //            if (document.getElementById("Fname_txt").value == "") alert_message += " - Specify First Name\n";
            //            if (formObj.Lname_txt.value == "") alert_message += " - Specify Last Name\n";
            //            if (formObj.Add_txt.value == "") alert_message += " - Specify Address\n";
            //            if (formObj.City_txt.value == "") alert_message += " - Specify City\n";

            //            if (formObj.Mob_txt.value == "") alert_message += " - Specify Mobile Number\n";
            //            if (formObj.Email_txt.value == "") alert_message += " - Specify E-Mail\n";
            //            if (formObj.Agn_txt.value == "") alert_message += " - Specify Agency Name\n";

            //            if (formObj.Pan_txt.value == "") alert_message += " - Specify Pan Number\n";
            //            if (formObj.Ans_txt.value == "") alert_message += " - Specify Answer\n";
            //            //if(formObj.uid_txt.value == "" )    alert_message+=" - Specify User ID\n";

            //            if (formObj.Pass_txt.value == "") alert_message += " - Specify Password\n";
            //            if (formObj.cpass_txt.value == "") alert_message += " - Specify Confirm Password\n";

            //            if (alert_message != "") {
            //                alert("Please fix the following problems: \n" + alert_message);
            //                return false;
            //            }
            //            if (formObj.Pass_txt.value != "") {
            //                if (formObj.Pass_txt.value != formObj.cpass_txt.value) {
            //                    alert("Confirm Password is same as Password");
            //                    return false;
            //                }
            //            }

            //            if (formObj.Email_txt.value != "") {
            //                var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

            //                if (!formObj.Email_txt.value.match(re)) {
            //                    alert('Your email address is not in a valid format.\n Some valid formats are:\nsomeone@somewhere.com\nsome_one@some.where.net')

            //                    var nogood = 'yes';
            //                    return false;
            //                }
            //            }
            //            return true;

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
  
    </script>

    <table width="60%" border="0" cellspacing="0" cellpadding="0" align="center" id="table_reg" style="margin: auto"
        runat="server">
        <tr>
        
            <td style="background: url(images/t4.png);">
            </td>
            <td>
                <table border="0" width="100%" cellspacing="2px" cellpadding="2px" align="center"
                    style="background: #fff;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="20px"
                                ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="main-heading" align="center">
                            <table width="100%" cellpadding="2" cellspacing="2" align="center">
                                <tr>
                                    <td align="center">
                                        <h2 style="color: #000000">
                                            REGISTER SUB AGENT
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width: 100%; padding-left: 35px; padding-top: 5px; padding-bottom: 5px;
                                            margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px; -moz-border-radius: 5px;
                                            -o-border-radius: 5px;">
                                            <legend class="legend" style="margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px;
                                                -moz-border-radius: 5px; -o-border-radius: 5px;">Personal Information</legend>
                                            <table width="100%" cellpadding="5" cellspacing="5" align="center">
                                                <tr>
                                                    <td width="18%" align="left">
                                                        Title:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left" width="200px">
                                                        <asp:DropDownList ID="tit_drop" runat="server">
                                                            <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                                            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left">
                                                        First Name:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Fname_txt"  runat="server" Style="position: static"
                                                            onkeypress="return vali();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Last Name:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Lname_txt"  runat="server" Style="position: static;"
                                                            onkeypress="return vali();"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Address:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Add_txt" runat="server" 
                                                            onkeypress="return vali1();" TextMode="MultiLine" Height="35px" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        City:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="City_txt"  runat="server" Style="position: static"
                                                            onkeypress="return vali();"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        State:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Stat_txt"  runat="server" Style="position: static"
                                                            onkeypress="return vali();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Country:
                                                    </td>
                                                    <td align="left" width="240px">
                                                        <asp:TextBox ID="Coun_txt"  runat="server" Style="position: static"
                                                            onkeypress="return vali();"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Pincode:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Pin_txt"  runat="server" Style="position: static"
                                                            onkeypress="return phone_vali();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width: 100%; padding-left: 35px; padding-top: 5px; padding-bottom: 5px;
                                            margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px; -moz-border-radius: 5px;
                                            -o-border-radius: 5px;">
                                            <legend class="legend" style="margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px;
                                                -moz-border-radius: 5px; -o-border-radius: 5px;">Contact Information</legend>
                                            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                                                <tr>
                                                    <td align="left" width="18%">
                                                        Phone No.:
                                                    </td>
                                                    <td align="left" style="width: 240px">
                                                        <asp:TextBox ID="Ph_txt"  runat="server" Style="position: static"
                                                            onkeypress="return phone_vali();"></asp:TextBox>
                                                        <br />
                                                        Eg. 011 451561
                                                    </td>
                                                    <td align="left">
                                                        Mobile:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Mob_txt"  runat="server" Style="position: static"
                                                            onkeypress="return phone_vali();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Email Id:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Email_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Email 2:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Aemail_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Fax No.:&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Fax_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width: 100%; padding-left: 35px; padding-top: 5px; padding-bottom: 5px;
                                            margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px; -moz-border-radius: 5px;
                                            -o-border-radius: 5px;">
                                            <legend class="legend" style="margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px;
                                                -moz-border-radius: 5px; -o-border-radius: 5px;">Agency Information</legend>
                                            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                                                <tr>
                                                    <td align="left" width="18%">
                                                        Agency Name:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Agn_txt"  runat="server" Style="width: 217px; position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Website:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Web_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Pan No:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Pan_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Status:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="Stat_drop" runat="server" Height="20px" Width="150px">
                                                            <asp:ListItem Value="TA" Selected="True">Travel Agent</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Stax No:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Stax_txt"  runat="server" Style="position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Referred By :
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="Sales_DDL" runat="server" Height="20px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Remark:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Rem_txt"  runat="server" Style="width: 216px; position: static"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Upload Logo :
                                                    </td>
                                                    <td align="left">
                                                        <asp:FileUpload ID="fld_1" runat="server"  Height="22px" />                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td></td>
                                                <td></td>
                                                <td colspan="2" align="left">
                                                Image must be in JPG formate and
                                                        size should be (90*70) pixels
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        SecurityQuestion:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="SecQ_drop" runat="server" Width="200px" Height="20px">
                                                            <asp:ListItem Value="What is Your Pet Name?">Mr.What is Your Pet Name?</asp:ListItem>
                                                            <asp:ListItem Value="What is your Favourite Color?">What is your Favourite Color?</asp:ListItem>
                                                            <asp:ListItem Value="What is Your Date of Birth">What is Your Date of Birth</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left">
                                                        SecurityAnswer:<span style="color: #990000">*</span>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Ans_txt"  runat="server" Style="width: 216px; position: static"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width: 100%; padding-left: 35px; padding-top: 5px; padding-bottom: 5px;
                                            margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px; -moz-border-radius: 5px;
                                            -o-border-radius: 5px;">
                                            <legend class="legend" style="margin: 0 auto; border-radius: 10px; -webkit-border-radius: 10px;
                                                -moz-border-radius: 5px; -o-border-radius: 5px;">Authentication Information</legend>
                                            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                                                <tr>
                                                    <td align="left" width="18%">
                                                        Password:<span style="color: #990000">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Pass_text" runat="server" Style="position: static" onkeypress="return vali1();"
                                                            TextMode="Password" MaxLength="8" ></asp:TextBox>
                                                        <br />
                                                        Eg. abc123(not more than 8 charecter)
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        ConfirmPassword:<span style="color: #990000">*</span>&nbsp;
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="cpass_txt"  runat="server" Style="position: static"
                                                            onkeypress="return vali1();" TextMode="Password" MaxLength="8"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="submit" runat="server" Text="Submit" OnClientClick="return validateSearch()"
                                                            CssClass="menu" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background: url(images/t5.png);">
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" id="table_Message"
        runat="server" visible="false">
        <tr>
            <td>
                <tr>
                    <td style="padding: 5px 35px; line-height: 20px; height: 160px; text-align: center;">
                        <h2 style="color: Black">
                            Thanks for register with RWT<br />
                            Your sub agent user id is: <strong><%=CID%></strong></h2>
                    </td>
                </tr>
            </td>
        </tr>
    </table>
</asp:Content>

