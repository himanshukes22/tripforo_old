<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="LccCheckOut.aspx.vb" Inherits="LccRF_LccCheckOut" Title="Untitled Page"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/customerinfo.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" />
    <style type="text/css">
        .adtdobcss
        {
            width: 100px;
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
        .chddobcss
        {
            width: 100px;
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
        .infdobcss
        {
            width: 100px;
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>

    <script type="text/javascript">
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
            if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
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
    </script>

    <script language="javascript" type="text/javascript">


        function paxValidate() {

            var elem = document.getElementById('ctl00_ContentPlaceHolder1_tbl_Pax').getElementsByTagName("input");

            for (var i = 0; i < elem.length; i++) {
                if (elem[i].type == "text" && elem[i].id.indexOf("txtAFirstName") > 0) {
                    if (elem[i].value == "" || elem[i].value == "First Name") {
                        alert('First Name can not be blank for Adult');
                        elem[i].focus();
                        return false;
                    }

                }
                if (elem[i].type == "text" && elem[i].id.indexOf("txtALastName") > 0) {

                    if (elem[i].value == "" || elem[i].value == "Last Name") {
                        alert('Last Name can not be blank for Adult');
                        elem[i].focus();
                        return false;
                    }
                }
//                if (elem[i].type == "text" && elem[i].value == "" && elem[i].id.indexOf("Txt_AdtDOB") > 0) {
//                    alert('Age can not be blank for Adult');
//                    elem[i].focus();
//                    return false;

//                }

            }


            if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Child_ctl00_txtCFirstName')) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtCFirstName") > 0) {
                        if (elem[i].value == "" || elem[i].value == "First Name") {
                            alert('First Name can not be blank for Child');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtCLastName") > 0) {
                        if (elem[i].value == "" || elem[i].value == "Last Name") {
                            alert('Last Name can not be blank for Child');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].value == "" && elem[i].id.indexOf("Txt_chDOB") > 0) {
                        alert('Age can not be blank for Child');
                        elem[i].focus();
                        return false;

                    }

                }
            }




            if (document.getElementById('ctl00_ContentPlaceHolder1_Repeater_Infant_ctl00_txtIFirstName')) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtIFirstName") > 0) {
                        if (elem[i].value == "" || elem[i].value == "First Name") {
                            alert('First Name can not be blank for Infant');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txtILastName") > 0) {
                        if (elem[i].value == "" || elem[i].value == "Last Name") {
                            alert('Last Name can not be blank for Infant');
                            elem[i].focus();
                            return false;
                        }

                    }
                    if (elem[i].type == "text" && elem[i].value == "" && elem[i].id.indexOf("Txt_InfantDOB") > 0) {
                        alert('Age can not be blank for Infant');
                        elem[i].focus();
                        return false;

                    }

                }
            }




            if ($("#ctl00_ContentPlaceHolder1_DropDownListProject option:selected").val() == "Select") {
                alert("Please Select Project Id");
                $("#ctl00_ContentPlaceHolder1_DropDownListProject").focus();
                return false;
            }


            if ($("#ctl00_ContentPlaceHolder1_DropDownListBookedBy option:selected").val() == "Select") {
                alert("Please Select Booked By");
                $("#ctl00_ContentPlaceHolder1_DropDownListBookedBy").focus();
                return false;
            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").value == "") {
                alert('Please Enter Primary Guest First Name');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_PGFName").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").value == "") {
                alert('Please Enter Primary Guest Last Name');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_PGLName").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value == "") {
                alert('Please Enter EmailID');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                return false;
            }

            var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Your email address seems incorrect. Please try again.");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Email").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").value == "") {
                alert('Please Enter Mobile No');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_MobNo").focus();
                return false;
            }


            if (confirm("Are you sure!")) {
                document.getElementById("div_Submit").style.display = "none";
                document.getElementById("div_Progress").style.display = "block";
                return true;
            }
            else {
                return false;
            }

        }
    </script>

    <table cellpadding="0" cellspacing="0" border="0" align="center" style="background: #fff;
        width: 950px; padding: 20px; margin: 0px auto;">
        <tr>
            <td width="10" height="10" valign="top">
                <img src="../images/box-tpr.jpg" width="10" height="10" />
            </td>
            <td style="background: url(../images/box-tp.jpg) repeat-x left bottom;">
            </td>
            <td valign="top">
                <img src="../images/box-tpl.jpg" width="10" height="10" />
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px; background: url(../images/boxl.jpg) repeat-y left bottom;">
            </td>
            <td style="padding: 10px; background: #fff;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="display: none">
                            <asp:Label ID="lbl_adult" runat="server"></asp:Label>
                            <asp:Label ID="lbl_child" runat="server"></asp:Label>
                            <asp:Label ID="lbl_infant" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; padding-left: 0px; padding-right: 10px; padding-top: 10px;"
                            valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="border: 2px solid #004b91;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #CCCCCC; color: #000000; font-weight: bold; height: 25px">
                                                    Flight Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divFltDtls" runat="server" style="padding: 3px 0px 3px 0px">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #004b91;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #CCCCCC; color: #000000; font-weight: bold; height: 25px">
                                                    Fare Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divFareDtls" runat="server" style="padding: 3px 0px 3px 0px">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divFareDtlsR" runat="server" style="padding: 3px 0px 3px 0px">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 70%; padding-top: 10px;" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tbl_Pax" runat="server">
                                <tr>
                                    <td style="border: thin solid #004b91" id="td_Adult" runat="server">
                                        <asp:Repeater ID="Repeater_Adult" runat="server">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="background-color: #CCCCCC; color: #000000" height="30px">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label CssClass="TextBig" ID="pttextADT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Text" style="width: 60px; padding-left: 13px;" height="30px">
                                                                        Name
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:DropDownList ID="ddl_ATitle" runat="server">
                                                                            <%--  <asp:ListItem Value="" Selected="True">Title</asp:ListItem>--%>
                                                                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                                            <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                            <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                            <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtAFirstName" runat="server" Width="120px" value="First Name" onfocus="focusObj(this);"
                                                                            onblur="blurObj(this);" defvalue="First Name" autocomplete="off" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        <%--<asp:TextBox ID="txtAFirstName" runat="server" Width="120px"></asp:TextBox>--%><%--<asp:RequiredFieldValidator
                                     ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAFirstName"></asp:RequiredFieldValidator>--%>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtAMiddleName" runat="server" Width="120px" value="Middle Name"
                                                                            onfocus="focusObjM(this);" onblur="blurObjM(this);" defvalue="Middle Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtALastName" runat="server" Width="120px" value="Last Name" onfocus="focusObj1(this);"
                                                                            onblur="blurObj1(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td style="width: 60px; padding-left: 13px;" class="Text">
                                                                                    D.O.B.
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_AdtDOB" runat="server" CssClass="adtdobcss"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td colspan="4" style="padding-left: 13px;" height="40px">
                                                                                    <a runat="server" id='anchor1' onclick="showhide(this,'anchor1','div1');" style="font-family: arial, Helvetica, sans-serif;
                                                                                        font-size: 12px; color: #004b91; cursor: pointer">Meal Preference/Seat Preference/Frequent
                                                                                        Flyer</a>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <div id="div1" runat="server" style="display: none;">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 60px; padding-left: 13px;" class="Text">
                                                                                                    Meal
                                                                                                </td>
                                                                                                <td style="width: 120px;">
                                                                                                    <asp:DropDownList ID="ddl_AMealPrefer" runat="server" Width="100px">
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
                                                                                                </td>
                                                                                                <td style="width: 50px;" class="Text">
                                                                                                    Seat
                                                                                                </td>
                                                                                                <td width="105px">
                                                                                                    <asp:DropDownList ID="ddl_ASeatPrefer" runat="server" Width="95px">
                                                                                                        <asp:ListItem Value="">Any</asp:ListItem>
                                                                                                        <asp:ListItem Value="W">Window</asp:ListItem>
                                                                                                        <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 105px;" class="Text">
                                                                                                    Frequent Flyer
                                                                                                </td>
                                                                                                <td width="35px">
                                                                                                    <asp:TextBox ID="txt_AAirline" runat="server" Width="40px" MaxLength="2" value="Airline"
                                                                                                        onfocus="focusObjAir(this);" onblur="blurObjAir(this);" defvalue="Airline"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_ANumber" runat="server" Width="65px" MaxLength="10" value="Number"
                                                                                                        onfocus="focusObjNumber(this);" onblur="blurObjNumber(this);" defvalue="Number"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #004b91" id="td_Child" runat="server">
                                        <asp:Repeater ID="Repeater_Child" runat="server">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="background-color: #CCCCCC; color: #000000" height="30px">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label CssClass="TextBig" ID="pttextCHD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Text" style="width: 60px; padding-left: 13px;" height="30px">
                                                                        Name
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:DropDownList ID="ddl_CTitle" runat="server">
                                                                            <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                            <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtCFirstName" runat="server" Width="120px" value="First Name" onfocus="focusObjCFName(this);"
                                                                            onblur="blurObjCFName(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtCMiddleName" runat="server" Width="120px" value="Middle Name"
                                                                            onfocus="focusObjCMName(this);" onblur="blurObjCMName(this);" defvalue="Middle Name"
                                                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCLastName" runat="server" Width="120px" value="Last Name" onfocus="focusObjCLName(this);"
                                                                            onblur="blurObjCLName(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td style="width: 60px; padding-left: 13px;" class="Text">
                                                                                    D.O.B.
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_chDOB" runat="server" CssClass="chddobcss"> </asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-left: 13px;" height="40px">
                                                                        <a runat="server" id='anchor2' onclick="showhide(this,'anchor2','div2');" style="font-family: arial, Helvetica, sans-serif;
                                                                            font-size: 12px; color: #004b91; cursor: pointer;">Meal Preference/Seat Preference</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <div id="div2" runat="server" style="display: none;">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 60px; padding-left: 13px;" class="Text">
                                                                                                    Meal
                                                                                                </td>
                                                                                                <td style="width: 120px">
                                                                                                    <asp:DropDownList ID="ddl_CMealPrefer" runat="server" Width="100px">
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
                                                                                                </td>
                                                                                                <td style="width: 60px;" class="Text">
                                                                                                    Seat
                                                                                                </td>
                                                                                                <td width="110px">
                                                                                                    <asp:DropDownList ID="ddl_CSeatPrefer" runat="server" Width="95px">
                                                                                                        <asp:ListItem Value="">Any</asp:ListItem>
                                                                                                        <asp:ListItem Value="W">Window</asp:ListItem>
                                                                                                        <asp:ListItem Value="A">Aisle</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #004b91" id="td_Infant" runat="server">
                                        <asp:Repeater ID="Repeater_Infant" runat="server">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="background-color: #CCCCCC; color: #000000" height="30px">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label CssClass="TextBig" ID="pttextINF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Text" style="width: 60px; padding-left: 13px;" height="30px">
                                                                        Name
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:DropDownList ID="ddl_ITitle" runat="server">
                                                                            <%-- <asp:ListItem Value="">Title</asp:ListItem>--%>
                                                                            <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                            <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtIFirstName" runat="server" Width="120px" value="First Name" onfocus="focusObjIFName(this);"
                                                                            onblur="blurObjIFName(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                    <td width="140px">
                                                                        <asp:TextBox ID="txtIMiddleName" runat="server" Width="120px" value="Middle Name"
                                                                            onfocus="focusObjIMName(this);" onblur="blurObjIMName(this);" defvalue="Middle Name"
                                                                            onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtILastName" runat="server" Width="120px" value="Last Name" onfocus="focusObjILName(this);"
                                                                            onblur="blurObjILName(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35px">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 60px; padding-left: 13px;" class="Text">
                                                                        D.O.B.
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_InfantDOB" runat="server" CssClass="infdobcss"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="border: thin solid #004b91">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="6" height="40px" style="font-family: arial, Helvetica, sans-serif; font-size: 18px;
                                                    font-weight: bold">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;Primary Guest Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="padding-left: 15px" class="Text" height="30px" width="35px">
                                                            </td>
                                                            <td width="95px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="padding-left: 15px" class="Text" height="30px" width="75px">
                                                                <span id="spn_Projects" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                    font-weight: bold; font-size: 12px;">Project Id </span>
                                                            </td>
                                                            <td width="130px">
                                                                <span id="spn_Projects1" runat="server">
                                                                    <asp:DropDownList ID="DropDownListProject" runat="server">
                                                                    </asp:DropDownList>
                                                                </span>&nbsp;
                                                            </td>
                                                            <td class="Text" style="padding-left: 10px" width="80px">
                                                                <span id="Spn_BookedBy" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                    font-weight: bold; font-size: 12px;">Booked By </span>
                                                            </td>
                                                            <td>
                                                                <span id="Spn_BookedBy1" runat="server">
                                                                    <asp:DropDownList ID="DropDownListBookedBy" runat="server" Height="20px">
                                                                    </asp:DropDownList>
                                                                </span>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="padding-left: 15px" class="Text" height="30px" width="35px">
                                                                Title
                                                            </td>
                                                            <td width="95px">
                                                                <asp:DropDownList ID="ddl_PGTitle" runat="server">
                                                                    
                                                                    <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                                    <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                                    <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                    <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                    <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding-left: 15px" class="Text" height="30px" width="75px">
                                                                First Name
                                                            </td>
                                                            <td width="130px">
                                                                <asp:TextBox ID="txt_PGFName" runat="server" onkeypress="return isCharKey(event)"
                                                                    Width="115px"></asp:TextBox>
                                                            </td>
                                                            <td class="Text" style="padding-left: 10px" width="80px">
                                                                Last Name
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_PGLName" runat="server" onkeypress="return isCharKey(event)"
                                                                    Width="105px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px" width="35px">
                                                </td>
                                                <td width="95px">
                                                </td>
                                                <td style="padding-left: 15px" class="Text" height="30px" width="75px">
                                                    EmailID
                                                </td>
                                                <td width="130px">
                                                    <asp:TextBox ID="txt_Email" runat="server" Width="115px"></asp:TextBox>
                                                </td>
                                                <td class="Text" style="padding-left: 10px" width="80px">
                                                    Mobile No
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txt_MobNo" runat="server" Width="105px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" height="40px">
                                                    <div id="div_Progress" style="display: none">
                                                        <b>Refund In Progress.</b> Please do not 'refresh' or 'back' button
                                                        <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                                                    </div>
                                                    <div id="div_Submit">
                                                        <asp:Button ID="book" runat="server" Text="Book" CssClass="button" OnClientClick="return paxValidate()" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 10px; height: 10px; background: url(../images/boxr.jpg) repeat-y left bottom;">
            </td>
        </tr>
        <tr>
            <td width="10" height="10" valign="top">
                <img src="../images/box-bl.jpg" width="10" height="10" />
            </td>
            <td style="background: url(../images/box-bottom.jpg) repeat-x left bottom;" height="10">
            </td>
            <td valign="top">
                <img src="../images/box-br.jpg" width="10" height="10" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="../js/chrome.js"></script>

    <script type="text/javascript">
        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg

        }
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript">
        $(function() { var d = new Date(); $(".adtdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: ('1952:' + (d.getFullYear() - 12) + ''), navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
        $(function() { $(".chddobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '-2y', minDate: '-10y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
        $(function() { $(".infdobcss").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '+0y', minDate: '-2y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });

		  
    </script>

</asp:Content>
