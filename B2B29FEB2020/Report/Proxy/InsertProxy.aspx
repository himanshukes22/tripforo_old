<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="InsertProxy.aspx.vb" Inherits="Reports_Proxy_InsertProxy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <%--<style>
        input[type="text"], input[type="password"], select
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
                   }
    </style>--%>
    <style type="text/css">
        .input_text, .input_textarea, .input_file, .status_textarea, .drop_select, #statusUpdate
        {
            font-size: 1em;
            font-family: "lucida grande", "lucida sans unicode", arial, verdana, tahoma, sans-serif;
            border: 1px solid #a6a6a6;
            color: #5e5e5e;
            border-radius: 2px 2px 2px 2px;
            -moz-border-radius: 2px 2px 2px 2px;
            -webkit-border-bottom-left-radius: 2px;
            -webkit-border-bottom-right-radius: 2px;
            -webkit-border-top-left-radius: 2px;
            -webkit-border-top-right-radius: 2px;
        }

            .input_text:hover, .input_textarea:hover, .input_file, .status_textarea:hover, .drop_select, #statusUpdate:hover
            {
                border: 1px solid #7a7a7a;
            }

            .input_text:focus, .input_file, .input_textarea:focus, .status_textarea:focus, .drop_select, #statusUpdate:focus
            {
                -moz-box-shadow: 0 0 7px #007ebf;
                -webkit-box-shadow: 0 0 7px #007ebf;
                box-shadow: 0 0 7px #007ebf;
                border: 1px solid #004b91;
            }
    </style>

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
    </script>

    <table cellspacing="0" cellpadding="0" align="center" style="background: #fff; width: 100%;">
        <tr>
            <td style="width: 20%"></td>
            <td style="width: 60%">
                <table cellspacing="0" cellpadding="0" align="center" style="background: #fff; width: 950px;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="center" valign="top" style="padding-top: 10px; font-size: 24px; color: #004b91; font-weight: bold;">Update Proxy Detail(<asp:Label ID="lbl_ProxyType" runat="server"></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 100px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Agency Detail</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agent ID :
                                                                </td>
                                                                <td id="td_AgentID" runat="server" width="200px"></td>
                                                                <td class="TextBig" width="200px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Available Card Limit :
                                                                </td>
                                                                <td id="td_CardLimit" runat="server" class="Text">
                                                                    <asp:Label ID="lbl_CrdLimit" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agent Name :
                                                                </td>
                                                                <td id="td_AgentName" runat="server" class="Text" width="200px"></td>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address :
                                                                </td>
                                                                <td id="td_AgentAddress" runat="server" class="Text"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Street :
                                                                </td>
                                                                <td id="td_Street" runat="server" class="Text" width="200px"></td>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Mobile No :
                                                                </td>
                                                                <td id="td_AgentMobNo" runat="server" class="Text"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email :
                                                                </td>
                                                                <td id="td_Email" runat="server" class="Text" width="200px"></td>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Agency Name :
                                                                </td>
                                                                <td class="Text">
                                                                    <asp:Label ID="lbl_AgencyName" runat="server" Visible="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agent Type :
                                                                </td>
                                                                <td id="td_AgentType" runat="server" class="Text" width="200px"></td>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <span width="100%" id="spn_Projects" runat="server">Project Id : </span>
                                                                </td>
                                                                <td class="Text">
                                                                    <span id="spn_Projects1" runat="server"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <span width="100%" id="Span_BookedBy" runat="server">Booked By : </span>
                                                                </td>
                                                                <td class="Text">
                                                                    <span id="Span_BookedBy1" runat="server"></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" runat="server" id="pnl_Depart">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Departure Details</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                                                        <tr>
                                                                            <td class="TextBig" width="250px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Booking type :
                                                                            </td>
                                                                            <td id="td_BookingType" runat="server" class="Text" height="30px" style="font-weight: normal"></td>
                                                                            <td class="TextBig" width="150px">&nbsp;&nbsp;&nbsp;Travel Type :
                                                                            </td>
                                                                            <td id="td_TravelType" runat="server" class="Text" style="font-weight: normal"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="200px" class="TextBig" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Departure:
                                                                            </td>
                                                                            <td id="td_From" runat="server" class="Text" style="font-weight: normal"></td>
                                                                            <td class="TextBig" width="150px">&nbsp;&nbsp;&nbsp;Destination :
                                                                            </td>
                                                                            <td id="td_To" runat="server" class="Text" style="font-weight: normal"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="200px" class="TextBig" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Departure Date :
                                                                            </td>
                                                                            <td id="td_DepartDate" runat="server" class="Text" style="font-weight: normal"></td>
                                                                            <td width="150px" class="TextBig">&nbsp;&nbsp; Departure Time :
                                                                            </td>
                                                                            <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_DeptTime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                                    Width="80px" MaxLength="4"></asp:TextBox>
                                                                                &nbsp;Eg:HHMM
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Arrival Date :
                                                                            </td>
                                                                            <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_ArivalDate" runat="server" Width="80px"
                                                                                    MaxLength="8"></asp:TextBox>
                                                                                &nbsp;Eg:DD/MM/YY
                                                                            </td>
                                                                            <td width="150px" class="TextBig">&nbsp;&nbsp; Arival Time :
                                                                            </td>
                                                                            <td class="TextBig" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_ArivalTime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                                    Width="80px" MaxLength="4"></asp:TextBox>
                                                                                &nbsp;Eg:HHMM
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticketing Airline :
                                                                            </td>
                                                                            <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_TktingAirline" runat="server" Width="80px"
                                                                                    MaxLength="2"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txt_TktingAirline"
                                                                                    ErrorMessage="*Please Fill Airline"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td width="150px" class="TextBig"></td>
                                                                            <td class="TextBig" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2" runat="server" id="td_SpecialRT" visible="false">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Returning Details</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr id="tr_SpecialRT" runat="server">
                                                                <td colspan="3">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="200px" class="TextBig" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Departure:
                                                                            </td>
                                                                            <td id="td_SpecialDep" runat="server" class="Text" style="font-weight: normal"></td>
                                                                            <td class="TextBig" width="150px">&nbsp;&nbsp;&nbsp;Destination :
                                                                            </td>
                                                                            <td id="td_SpecialDest" runat="server" class="Text" style="font-weight: normal"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="200px" class="TextBig" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Return Date :
                                                                            </td>
                                                                            <td id="td_SpecialRetDate" runat="server" class="Text" style="font-weight: normal"></td>
                                                                            <td width="150px" class="TextBig">&nbsp;&nbsp; Return Time :
                                                                            </td>
                                                                            <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_SpecialRetTime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                                    Width="80px" MaxLength="4"></asp:TextBox>
                                                                                &nbsp;Eg:HHMM
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Arrival Date :
                                                                            </td>
                                                                            <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_SpecialArrDate" runat="server" Width="80px"
                                                                                    MaxLength="8"></asp:TextBox>
                                                                                &nbsp;Eg:DD/MM/YY
                                                                            </td>
                                                                            <td width="150px" class="TextBig">&nbsp;&nbsp; Arival Time :
                                                                            </td>
                                                                            <td class="TextBig" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal">
                                                                                <asp:TextBox CssClass="input_text" ID="txt_SpecialArrTime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                                    Width="80px" MaxLength="4"></asp:TextBox>
                                                                                &nbsp;Eg:HHMM
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>

                                                            </tr>

                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td colspan="2" runat="server" id="PanelRetrurn">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Returning Details</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Return Date :
                                                                </td>
                                                                <td id="td_RetDate" runat="server" class="Text" style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';"></td>
                                                                <td class="TextBig" width="150px" height="30px">&nbsp;&nbsp;&nbsp;Return Time :
                                                                </td>
                                                                <td class="Text" style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                    <asp:TextBox CssClass="input_text" ID="txt_RetTime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                        Width="80px" MaxLength="4"></asp:TextBox>
                                                                    &nbsp;Eg:HHMM
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Arrival Date :
                                                                </td>
                                                                <td class="Text" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal">
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReADate" runat="server" Width="80px" MaxLength="8"></asp:TextBox>
                                                                    &nbsp;Eg:DD/MM/YY
                                                                </td>
                                                                <td class="TextBig" width="150px" height="30px">&nbsp;&nbsp;&nbsp;Arrival Time :
                                                                </td>
                                                                <td class="Text" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal">
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReATime" runat="server" onKeyPress="return isNumberKey(event)"
                                                                        Width="80px" MaxLength="4"></asp:TextBox>
                                                                    &nbsp;Eg:HHMM
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticketing Airline :
                                                                </td>
                                                                <td style="font-weight: normal; font-size: 13px; font-family: 'trebuchet MS';">
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReTktingAirline" runat="server" Width="80px"
                                                                        MaxLength="2"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txt_ReTktingAirline"
                                                                        ErrorMessage="*Please Fill Airline"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="150px" class="TextBig"></td>
                                                                <td class="TextBig" style="font-family: 'trebuchet MS'; font-size: 13px; font-weight: normal"></td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;No Of Passanger</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="TextBig" width="200px" height="25px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Adult :<asp:Label ID="lbl_FName"
                                                                    runat="server" Visible="False"></asp:Label><asp:Label ID="lbl_LName" runat="server"
                                                                        Visible="False"></asp:Label><asp:Label ID="lbl_Title" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                                <td id="td_Adult" runat="server" class="Text"></td>
                                                                <td class="TextBig" width="200px" height="25px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Child :
                                                                </td>
                                                                <td id="td_Child" runat="server" class="Text"></td>
                                                                <td class="TextBig" width="200px" height="25px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Infant :
                                                                </td>
                                                                <td id="td_Infrant" runat="server" class="Text"></td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                                        <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Passanger Details</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td class="TextBig" height="25px">&nbsp;&nbsp;&nbsp;&nbsp; Mobile No
                                                                                        </td>
                                                                                        <td class="Text">
                                                                                            <asp:TextBox CssClass="input_text" ID="txt_MobNo" runat="server" Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="TextBig">&nbsp;&nbsp;&nbsp; Email
                                                                                        </td>
                                                                                        <td class="Text">
                                                                                            <asp:TextBox CssClass="input_text" ID="txt_Email" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Text" align="center">
                                                                                <fieldset style="padding: 10px; border: 1px solid #C0C0C0;">
                                                                                    <legend style="width: 80px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Adult Details</legend>
                                                                                    <asp:GridView ID="GridViewAdult" runat="server" AutoGenerateColumns="false" Width="98%"
                                                                                        CssClass="mGrid">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="SirName">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="FirstName">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="LastName">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Age">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_Age" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="TicketNo">
                                                                                                <ItemTemplate>
                                                                                                    <%--<asp:TextBox ID="txt_TktNo" runat="server" Text=""></asp:TextBox>--%>
                                                                                                    &nbsp;&nbsp;
                                                                                                    <asp:TextBox CssClass="input_text" ID="txt_Ticket" runat="server"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                                                        ControlToValidate="txt_Ticket"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="ReturnTicketNo">
                                                                                                <ItemTemplate>
                                                                                                    <%--<asp:TextBox ID="txt_TktNo" runat="server" Text=""></asp:TextBox>--%>
                                                                                                    &nbsp;&nbsp;<asp:TextBox ID="txt_ReTicket" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_ReTicket"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#CCCCCC" />
                                                                                    </asp:GridView>
                                                                                </fieldset>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 20px" height="15px"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_Child" runat="server">
                                                                <td class="Text" align="center">
                                                                    <fieldset style="padding: 10px; border: 1px solid #C0C0C0;">
                                                                        <legend style="width: 80px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Child Details</legend>
                                                                        <asp:GridView ID="GridViewChild" runat="server" AutoGenerateColumns="false" Width="98%"
                                                                            CssClass="mGrid">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SirName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_CSirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FirstName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_CFirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LastName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_CLastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Age">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_CAge" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TicketNo">
                                                                                    <ItemTemplate>
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:TextBox CssClass="input_text" ID="txt_CTktNo" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txt_CTktNo"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ReturnTicketNo">
                                                                                    <ItemTemplate>
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:TextBox CssClass="input_text" ID="txt_ReCTktNo" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txt_ReCTktNo"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle BackColor="#CCCCCC" />
                                                                        </asp:GridView>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_Infrant" runat="server">
                                                                <td class="Text" align="center">
                                                                    <fieldset style="padding: 10px; border: 1px solid #C0C0C0;">
                                                                        <legend style="width: 80px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Infant Details</legend>
                                                                        <asp:GridView ID="GridViewInfrant" runat="server" AutoGenerateColumns="false" Width="98%"
                                                                            CssClass="mGrid">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SirName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_ISirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FirstName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_IFirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LastName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_ILastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Age">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_IAge" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TicketNo">
                                                                                    <ItemTemplate>
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:TextBox CssClass="input_text" ID="txt_ITktNo" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txt_ITktNo"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ReturnTicketNo">
                                                                                    <ItemTemplate>
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:TextBox CssClass="input_text" ID="txt_ReITktNo" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txt_ReITktNo"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle BackColor="#CCCCCC" />
                                                                        </asp:GridView>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px"></td>
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
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                <legend style="border: thin solid #004b91; width: 115px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Other Details</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                    <tr>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Class :
                                        </td>
                                        <td id="td_Class" runat="server" class="Text" width="100px"></td>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Airline :
                                        </td>
                                        <td id="td_Airline" runat="server" class="Text"></td>
                                        <td class="TextBig" width="200px" height="30px">&nbsp; Classes :
                                        </td>
                                        <td id="td_Classes" runat="server" class="Text"></td>
                                    </tr>
                                    <tr>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Payment Mode :
                                        </td>
                                        <td id="td_PMode" runat="server" class="Text"></td>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Sector :
                                        </td>
                                        <td class="Text" id="td_Sector" runat="server"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr id="tr_OneWay" runat="server">
                                        <td class="TextBig" width="150px" height="30px">&nbsp; GDS PNR :
                                        </td>
                                        <td class="Text" width="100px">
                                            <asp:TextBox CssClass="input_text" ID="txt_GDSPNR" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Airline PNR :
                                        </td>
                                        <td id="td2" runat="server" class="Text">
                                            <asp:TextBox CssClass="input_text" ID="txt_AirlinePNR" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="TextBig" width="100px" height="30px">&nbsp; Flight_No :
                                        </td>
                                        <td class="Text">
                                            <asp:TextBox CssClass="input_text" ID="txt_Flight" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trReturn" runat="server">
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Return GDS PNR :
                                        </td>
                                        <td class="Text">
                                            <asp:TextBox CssClass="input_text" ID="txt_ReGDSPNR" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="TextBig" width="150px" height="30px">&nbsp; Return Airline PNR:
                                        </td>
                                        <td id="td1" runat="server" class="Text">
                                            <asp:TextBox CssClass="input_text" ID="txt_ReAirlinePNR" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="TextBig" width="200px" height="30px">&nbsp; Return Flight_No :
                                        </td>
                                        <td class="Text">
                                            <asp:TextBox CssClass="input_text" ID="txt_ReFlight" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextBig">&nbsp; Remark :
                                        </td>
                                        <td id="td_Remark" runat="server" class="Text" colspan="3"></td>
                                        <td class="TextBig" width="100px" height="30px" id="td_special_flight" runat="server" visible="false">&nbsp; Flight_No_RoundTrip :
                                        </td>
                                        <td class="Text"  id="td_special_flight1" runat="server" visible="false">
                                            <asp:TextBox CssClass="input_text" ID="txt_SpecialFlight" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" id="pnl_OneWay">
                            <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                <legend style="border: thin solid #004b91; width: 250px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp; Fare Details One Way(<asp:Label
                                    ID="lbl_Oneway" runat="server"></asp:Label><asp:Label ID="lbl_onewaydate" runat="server"></asp:Label>)
                                </legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Per Adult Fare
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" height="30px">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ABaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_ABaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_AYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_AYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_AYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_AYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_AWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_AWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_AOT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_AOT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ATotal" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table id="tbl_Child" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Per Child Fare
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" height="30px">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_CBaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_CBaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_CYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_CYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_CYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_CYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_CWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_CWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_COT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_COT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_Ctotal" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" id="tbl_Infrant" runat="server">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Per Infant Fare
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" height="30px">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_IBaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_IBaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_IYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_IYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_IYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txt_IYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_IWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt_IWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_IOT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_IOT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ITotal" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFCC">
                                            <table width="100%">
                                                <tr>

                                                    <td width="80px">&nbsp; Proxy Charge
                                                    </td>
                                                    <td>
                                                        <asp:TextBox CssClass="input_text" ID="txt_ProxyChargeOW" runat="server" Width="40px" Text="0"
                                                            onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                    </td>


                                                    <td width="180px" style="color: #FF3300">&nbsp;&nbsp;Total Special Fare Discount
                                                    </td>
                                                    <td style="font-size: 12px" valign="middle" width="300px">
                                                        <asp:TextBox CssClass="input_text" ID="txt_SFDis" runat="server" Width="60px" Text="0"
                                                            onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                        &nbsp; (Special Fare Discount * No of Pax)
                                                    </td>
                                                    <td id="td_rbd" runat="server" visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="50px">RBD
                                                                </td>
                                                                <td width="170px" style="font-weight: bold">
                                                                    <asp:TextBox CssClass="input_text" ID="rbd" runat="server" Width="90px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorrbd" runat="server" ControlToValidate="rbd"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;&nbsp; Eg=> A:B:C
                                                                </td>
                                                                <td width="90px" style="font-weight: bold">Service Charge
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_srvcharge" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td id="pnl_Roundtrip" runat="server">
                            <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                <legend style="border: thin solid #004b91; width: 260px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp; RoundTrip Fare
                                    Details (
                                    <asp:Label ID="lbl_Return" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_ReturnDate" runat="server"></asp:Label>
                                    )</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Per Adult Fare&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" height="30px">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReABaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_ReABaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReAYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_ReAYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReAYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_ReAYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReAWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txt_ReAWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReAOT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txt_ReAOT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReATotal" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table id="tbl_Rechild" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Per Child Fare
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig" height="30px">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCBaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txt_ReCBaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_ReCYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txt_ReCYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_ReCWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCOT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txt_ReCOT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReCtotal" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" id="tbl_ReInfrant" runat="server">
                                                            <tr>
                                                                <td colspan="12" class="TextBig" style="background-color: #CCCCCC" height="23px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Per Infant Fare
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TextBig">&nbsp; Base Fare
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReIBaseFare" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_ReIBaseFare"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YQ
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReIYQ" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_ReIYQ"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">YR
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReIYR" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_ReIYR"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">WO
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReIWO" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_ReIWO"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">OT
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReIOT" runat="server" Width="60px" onKeyPress="return isNumberKey(event)"
                                                                        Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_ReIOT"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="TextBig">Total
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_ReITotal" runat="server" Width="60px"
                                                                        onKeyPress="return isNumberKey(event)" Height="20px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFCC">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80px">&nbsp; Proxy Charge
                                                    </td>
                                                    <td>
                                                        <asp:TextBox CssClass="input_text" ID="txt_ProxyChargeRT" runat="server" Width="40px" Text="0"
                                                            onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                    </td>
                                                    <td style="color: #FF3300" width="180px">&nbsp; Total Special Fare Discount
                                                    </td>
                                                    <td style="font-size: 12px" valign="middle">
                                                        <asp:TextBox CssClass="input_text" ID="txt_ReSFDis" runat="server" Width="60px" Text="0"
                                                            onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                        &nbsp; (Special Fare Discount * No of Pax)
                                                    </td>
                                                    <td id="td_rerbd" runat="server" visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="50px">RBD
                                                                </td>
                                                                <td width="170px" style="font-weight: bold">
                                                                    <asp:TextBox CssClass="input_text" ID="re_rbd" runat="server" Width="90px" Height="20px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1rbd1" runat="server" ControlToValidate="re_rbd"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;&nbsp; Eg=> A:B:C
                                                                </td>
                                                                <td width="90px" style="font-weight: bold">Service Charge
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="input_text" ID="txt_resrvcharge" runat="server" Width="60px"
                                                                        Height="20px" onKeyPress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="Proxy" align="right" style="background-color: #FFFF99" valign="middle">
                            <asp:Button ID="Button1" runat="server" Text="Calculation" Height="30px" />
                            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td id="pnl_onewaycal" runat="server">
                            <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                <legend style="border: thin solid #004b91; width: 140px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;One Way Calculations</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Sevice Tax<asp:Label ID="lbl_STax" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ServiceTax" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp; Transaction Fee<asp:Label ID="lblTF" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_TransFee" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Discount
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_TotalDiscount" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp; TDS
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_TDS" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tr_mgtfee" runat="server">
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp;Management Fee</td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_mgtfee" runat="server"></asp:Label>

                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp;Admin Markup</td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_mrkadmin" runat="server"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Total Booking Cost
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_TBC" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp; Total Booking Cost After Discount
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_TBCAFTRD" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td id="pnl_roundtripcal" runat="server">
                            <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                <legend style="border: thin solid #004b91; width: 160px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Round Trip Calculations</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Sevice Tax<asp:Label ID="Lbl_ReSTax" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReServiceTax" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp; Transaction Fee<asp:Label ID="lblReTF" runat="server" Visible="False"></asp:Label><asp:Label
                                                        ID="LabelTDS" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReTransFee" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Discount<asp:Label ID="lbl_ReDis" runat="server" Visible="False"></asp:Label><asp:Label
                                                        ID="lbl_ReDis_YQ" runat="server" Visible="False"></asp:Label><asp:Label ID="lbl_ReDis_B_YQ"
                                                            runat="server" Visible="False"></asp:Label><asp:Label ID="lbl_ReCB" runat="server"
                                                                Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReTotalDiscount" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp;&nbsp;TDS<asp:Label ID="lbl_ReDiscount" runat="server" Visible="False"></asp:Label><asp:Label
                                                        ID="lbl_ReDiscount_YQ" runat="server" Visible="False"></asp:Label><asp:Label ID="lbl_ReDiscount_B_YQ"
                                                            runat="server" Visible="False"></asp:Label><asp:Label ID="lbl_ReCashBack" runat="server"
                                                                Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReTDS" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tr_remgtfee" runat="server">
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp;Management Fee</td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_remgtfee" runat="server"></asp:Label>

                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp;Admin Markup</td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_readminmrk" runat="server"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TextBig">&nbsp;&nbsp; Total Booking Cost
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReTBC" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="TextBig">&nbsp;&nbsp;&nbsp; Total Booking Cost After Discount
                                                    </td>
                                                    <td class="Text">
                                                        <asp:Label ID="lbl_ReTBCAFTRD" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="50px">
                            <%--  <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>--%>
                            <div id="div_Submit">
                                <asp:Button ID="btn_UpdateProxy" runat="server" Text="Update Proxy" OnClientClick="return InsertProxy()"
                                    Font-Bold="True" Height="40px" Width="180px" Visible="False" />
                            </div>
                            <div id="div_Progress" style="display: none">
                                <b>Booking In Progress.</b> Please do not 'refresh' or 'back' button
                                <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                            </div>
                            <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                            <%--<asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
                                <ProgressTemplate>
                                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                        padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                        z-index: 1000;">
                                    </div>
                                    <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                        z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                        font-weight: bold; color: #000000">
                                        Please Wait....<br />
                                        <br />
                                        <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                        <br />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20%"></td>
        </tr>
    </table>
</asp:Content>
