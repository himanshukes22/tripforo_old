<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="UpdateImportPnrIntl.aspx.vb" Inherits="Reports_Import_UpdateImportPnrIntl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        //var hide1 = true;
        $(document).ready(function () {
            //Attach click to checkboxlist
            $("#ctl00_ContentPlaceHolder1_CheckBox1").click(function () {
                var table = document.getElementById("paxRP");
                if (table != null) {
                    for (i = 0; i < table.rows.length; i++) {
                        if (document.getElementById('ctl00_ContentPlaceHolder1_CheckBox1').checked == true)         //(hide1)
                            table.rows[i].cells[4 - 1].style.display = 'none';
                        else
                            table.rows[i].cells[4 - 1].style.display = '';
                    }
                    // hide1 = !hide1;
                }
            });
        });


        function validateptk() {

            var airpnr = $("#ctl00_ContentPlaceHolder1_txtAirPnr").val()
            var airvc = $("#ctl00_ContentPlaceHolder1_txtVC").val()

            if (airpnr == "") {
                alert("please enter Airline Pnr");
                return false;
            }
            if (airvc == "") {
                alert("please enter Airline Vc");
                return false;
            }


            var gridViewCtID = "paxRP";
            var grid = document.getElementById(gridViewCtID);
            var gridLength = grid.rows.length - 1;
            for (var i = 0; i < gridLength; i++) {
                var ddlHaul = grid.getElementsByTagName('SELECT')  //tag for ddl
                var selection = ddlHaul[i].options[ddlHaul[i].selectedIndex].text;

                if (selection == "-Select-") {
                    alert("please Select PaxType");
                    return false;
                    break;
                }
            }


            if (document.getElementById('ctl00_ContentPlaceHolder1_CheckBox1').checked == true) {

            }
            else {
                var f = document.getElementById("paxRP");
                for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {


                    if (f.getElementsByTagName("input").item(i).type == "text") {

                        if (f.getElementsByTagName("input").item(i).value == "") {
                            alert("Please Enter ticketNo");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }
                }





                //var taxgrid = document.getElementById('paxRP');
                //var rows = document.getElementsByTagName('TR');
                //total = 0;
                //for (i = 1; i < rows.length - 1; i++) {
                //    var lbl=parseInt(rows[i].children[1].children[0].value);

                //    if (lbl <= txt) {
                //        alert("Not OK");
                //        rows[i].children[2].children[0].focus();
                //        return false;
                //    }

                // var tt = $(".validatetk").val()
                //if (tt == "") {
                //     alert("please enter ticketNo");
                //  return false;
                // }

            }

        }
    </script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
                return true;
            }
            else {

                return false;
            }
        }

        function isAlphaNumeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90)) {
                return true;
            }
            else {

                return false;
            }
        }

        function istktno(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 45)) {
                return true;
            }
            else {

                return false;
            }
        }

        function isAlphabet(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90)) {
                return true;
            }
            else {

                return false;
            }
        }

        function cnfrmMsg() {
            //        var answer = confirm("Are you sure to update the pnr details?")
            //        if (answer)
            //          document.getElementById("div_Submit").style.display = "none";
            //            document.getElementById("div_Progress").style.display = "block";
            //            return true;
            //        else
            //            return false;


            if (confirm("Are you sure to update the pnr details?")) {
                document.getElementById("div_Submit").style.display = "none";
                document.getElementById("div_Progress").style.display = "block";
                return true;

            }
            else {
                return false;
            }



        }
    </script>

    <div align="center">
        <table cellspacing="10" cellpadding="0" border="0" class="tbltbl" width="950px">
                        <tr>
                            <td colspan="3">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td height="25px" width="130px" class="text1">
                                            Agency ID :<asp:Label ID="lbl_mn" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td id="td_AgencyID" runat="server" width="130px" class="Text">
                                        </td>
                                        <td width="100px" class="text1">
                                            Credit Limit :
                                        </td>
                                        <td align="left" class="Text">
                                            <asp:Label ID="crdLmt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25px" class="text1">
                                            Agency Type :
                                        </td>
                                        <td id="td_AgencyType" runat="server" class="Text">
                                        </td>
                                        <td class="text1">
                                            Agency Name :
                                        </td>
                                        <td align="left" id="td_AgencyName" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text1" height="25px" align="left">
                                Address :
                            </td>
                            <td id="td_AgencyAddress" runat="server" colspan="3" class="Text">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GridViewshow" runat="server" AutoGenerateColumns="false"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                        <Columns>
                            <asp:TemplateField HeaderText="Orgin">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_orgin" runat="server" Text='<%#Eval("Departure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Destination">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_destination" runat="server" Text='<%#Eval("Destination") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departuredate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_depdate" runat="server" Text='<%#Eval("Departdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departuretime">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_depttime" runat="server" Text='<%#Eval("DepartTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrivaldate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_arrdate" runat="server" Text='<%#Eval("ArrivalDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrivaltime">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_arrtime" runat="server" Text='<%#Eval("ArrivalTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Airline">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Airline" runat="server" Text='<%#Eval("Airline") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FlightNo">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_flno" runat="server" Text='<%#Eval("FlightNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBD">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_rbd" runat="server" Text='<%#Eval("RDB") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="AdtFareBasic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_rbdadt" runat="server" Text='<%#Eval("AdtFareBasis")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="ChdFareBasic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_rbdchd" runat="server" Text='<%#Eval("ChdFareBasis")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="InfFareBasic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_rbdinf" runat="server" Text='<%#Eval("InfFareBasis")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                            <td colspan="3">
                                <asp:GridView ID="paxdetals_Grid" runat="server" AutoGenerateColumns="false"  Visible="false" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MName" runat="server" Text='<%#Eval("MName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_LName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaxType">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DOB" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Gender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                <td colspan="3" style="padding: 15px; border: thin solid #004b91;" id="td_Ticket"
                    runat="server" visible="false">
                    <table width="100%" border="0" cellspacing="2" cellpadding="2" id="paxTbl" runat="server">
                        <tr>
                            <td width="50%" valign="top">
                                <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td width="25%" class="style2" align="left">
                                            GDS Pnr :
                                        </td>
                                        <td width="75%" class="style3" align="left" colspan="3">
                                            <asp:Label ID="lblGdsPnr" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="cabin" align="left">
                                            Airline Pnr :
                                        </td>
                                        <td width="75%" align="left" colspan="3">
                                            <asp:TextBox ID="txtAirPnr" CssClass="textboxflight" Height="20px" runat="server"
                                                onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqfvAirpnr" runat="server" ControlToValidate="txtAirPnr"
                                                ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="cabin" align="left">
                                            Ticketing Airline :
                                        </td>
                                        <td width="75%" align="left" colspan="3">
                                            <asp:TextBox ID="txtVC" CssClass="textboxflight" Height="20px" runat="server" MaxLength="2"
                                                onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqfvtxtvc" runat="server" ControlToValidate="txtVC"
                                                ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="tr_corp" runat="server">
                                        
                                            <td align="left">Project Id:</td>
                                            <td id="td_ProjectId" runat="server"> </td>
                                            <td align="left">Booked By:</td>
                                            <td id="td_BookedBy" runat="server"> </td>
                                    </tr>
                                                 <tr id="tr1" runat="server">
                                                 <td id="ckh_div" style="font-weight:bold;"><asp:CheckBox ID="CheckBox1"  runat="server"/>:Online Ticket</td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td width="50%">
                                            <table width="100%" id="paxRP" border="0" cellspacing="2" cellpadding="2" style="text-align: center">
                                                <asp:Repeater ID="paxRPTR" runat="server">
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th style="display: none">
                                                            </th>
                                                            <th>
                                                                Pax Name
                                                            </th>
                                                            <th>
                                                                Type
                                                            </th>
                                                            <th>
                                                                Ticket Number
                                                            </th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="display: none">
                                                                <asp:Label ID="PaxID" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PaxName" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PaxType" runat="server" CssClass="combobox">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                    <asp:ListItem Value="ADT" Text="Adult"></asp:ListItem>
                                                                    <asp:ListItem Value="CHD" Text="Child"></asp:ListItem>
                                                                    <asp:ListItem Value="INF" Text="Infant"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PaxTktNo" runat="server" CssClass="textboxflight validatetk" Height="20px"
                                                                    onkeypress="return istktno(event);"></asp:TextBox>


                                                                <%--<asp:RequiredFieldValidator ID="PaxTktNoRQFV" runat="server" ControlToValidate="PaxTktNo"
                                                                ErrorMessage="*" ForeColor="#ff0000" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="padding: 5px 5px;">
                                <asp:Button ID="pnrimporContinue" runat="server" Text="Continue" OnClientClick="return validateptk();" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="33%">
                                <div style="border: 1px #666666 solid; width: 95%; padding: 2px;" id="divAdult" runat="server">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="background-color: #CCCCCC; font-weight: bold; padding-left: 20px;"
                                                height="23px">
                                                Per Adult Fare
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 40%" height="30px">
                                                &nbsp;Base Fare
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ABaseFare" runat="server" CssClass="textboxflight" Height="20px"
                                                                Width="100px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" class="style1">
                                                &nbsp;YQ
                                            </td>
                                            <td class="style1">
                                                            <asp:TextBox ID="txt_AYQ" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp;YR
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_AYR" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp;WO
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_AWO" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td style="font-weight: bold;" height="30px">
                                                            &nbsp;GST
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_AGST" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp;OT
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_AOT" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp;Total
                                            </td>
                                            <td style="font-weight: bold;" height="30px">
                                                <asp:Label ID="lblATotal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td width="33%">
                                <div style="border: 1px #666666 solid; width: 95%; padding: 2px;" id="divChild" runat="server">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="background-color: #CCCCCC; font-weight: bold; padding-left: 20px;"
                                                height="23px">
                                                Per Child Fare
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 40%" height="30px">
                                                &nbsp; Base Fare
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_CBaseFare" runat="server" CssClass="textboxflight" Height="20px"
                                                                onkeypress="return isNumberKey(event)" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; YQ
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_CYQ" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; YR
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_CYR" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; WO
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_CWO" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="font-weight: bold;" height="30px">
                                                            &nbsp;GST
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CGST" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; OT
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_COT" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; Total
                                            </td>
                                            <td style="font-weight: bold;" height="30px">
                                                <asp:Label ID="lblCTotal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td width="33%">
                                <div style="border: 1px #666666 solid; width: 95%; padding: 2px;" id="divInfant"
                                    runat="server">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="background-color: #CCCCCC; font-weight: bold; padding-left: 20px;"
                                                height="23px">
                                                Per Infant Fare
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; width: 40%" height="30px">
                                                &nbsp; Base Fare
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_IBaseFare" runat="server" CssClass="textboxflight" Height="20px"
                                                                onkeypress="return isNumberKey(event)" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; YQ
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_IYQ" runat="server" CssClass="textboxflight" Height="20px" onkeypress="return isNumberKey(event)"
                                                                Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; YR
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_IYR" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; WO
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_IWO" runat="server" CssClass="textboxflight" Height="20px" Width="60px"
                                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                                        <td style="font-weight: bold;" height="30px">
                                                            &nbsp; GST
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_IGST" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;" height="30px">
                                                            &nbsp; OT
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_IOT" runat="server" CssClass="textboxflight" Height="20px" Width="100px"
                                                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                            <td style="font-weight: bold;" height="30px">
                                                &nbsp; Total
                                            </td>
                                            <td style="font-weight: bold;" height="30px">
                                                <asp:Label ID="lblITotal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding: 5px 5px;">
                    <asp:Button ID="btnCalc" runat="server" Text="Calculate" CssClass="button" />
                </td>
                <td align="center" colspan="2" style="padding: 5px 5px;">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left" id="farecalctbl" runat="server" style="border: thin solid #004b91;
                    padding: 15px;">
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td width="100px" height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Total Fare : &nbsp;
                            </td>
                            <td height="30px" width="14%">
                                <asp:Label ID="lblTtlFare" runat="server" Font-Names="Georgia"></asp:Label>
                            </td>
                            <td width="15%" height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Service Tax : &nbsp;
                            </td>
                            <td width="20%" height="30px">
                                <asp:Label ID="lblSrvTax" runat="server" Font-Names="Georgia"></asp:Label>
                            </td>
                            <td width="15%" height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Tran. Fee : &nbsp;
                            </td>
                            <td width="20%" height="30px">
                                <asp:Label ID="lblTF" runat="server" Font-Names="Georgia"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="16%" height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Total Fare After Dis.:
                            </td>
                            <td height="30px">
                                <asp:Label ID="lbltfAftrDis" runat="server" Font-Bold="False" Font-Names="Georgia"></asp:Label>
                            </td>
                            <td height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Dis. + Cash Back : &nbsp;
                            </td>
                            <td height="30px">
                                <asp:Label ID="lblTtlDis" runat="server" Font-Names="Georgia"></asp:Label>
                                +
                                <asp:Label ID="lblTtlCB" runat="server" Font-Names="Georgia"></asp:Label>
                            </td>
                            <td height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                TDS : &nbsp;
                            </td>
                            <td height="30px">
                                <asp:Label ID="lblTds" runat="server" Font-Names="Georgia"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="16%" height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Pnr Import Charges :
                            </td>
                            <td height="30px" align="left" style="font-weight: bold;">
                                <asp:TextBox ID="txtpnrImpCharge" runat="server" Text="0" CssClass="textboxflight"
                                    Width="50" Height="20px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                            <td height="30px" style="font-weight: bold; padding-left: 5px;">
                                &nbsp;Extra Charge
                            </td>
                            <td height="30px">
                                <asp:TextBox ID="txt_ExtraCharge" runat="server" Text="0" CssClass="textboxflight"
                                    Width="50" Height="20px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                &nbsp;
                            </td>
                           
                        </tr>
                         <tr id="tr_admrkmgt" runat="server" visible="false">
                          <td height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                Management Fee:
                            </td>
                            <td height="30px">
                                &nbsp;<asp:Label ID="lbl_mgtfee" runat="server"></asp:Label>
                            </td>
                                            <td height="30px" align="left" style="font-weight: bold; padding-left: 5px;">
                                                    Admin Markup :</td>
                                                <td height="30px">
                                                    &nbsp;<asp:Label ID="lbl_admrk" runat="server"  ></asp:Label>
                                                </td>
                                            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" height="35px">
                    <div id="div_Submit">
                        <asp:Button ID="btnUpdateImpPnr" runat="server" Text="Update Pnr" CssClass="button"
                            OnClientClick="return cnfrmMsg();" />
                    </div>
                    <div id="div_Progress" style="display: none">
                        <b>Booking In Progress.</b> Please do not 'refresh' or 'back' button
                        <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                    </div>
                    <div id="msgboxdiv" runat="server" style="display: none;">
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
