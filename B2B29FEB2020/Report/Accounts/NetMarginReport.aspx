<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="NetMarginReport.aspx.vb" Inherits="SprReports_Accounts_NetMarginReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <style>
        input[type="text"], input[type="password"], select {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>
    <style type="text/css">
        .txtBox {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }

        .txtCalander {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <table cellspacing="10" cellpadding="0" border="0" class="tbltbl">
        <tr>
            <td>
                <table cellspacing="5" cellpadding="5" align="center" style="background: #fff;">
                    <tr>
                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">Net Margin Report
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold">From Date
                                    </td>
                                    <td width="130">
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                            style="width: 100px" />
                                    </td>
                                    <td width="80" style="font-weight: bold">&nbsp;&nbsp;To Date
                                    </td>
                                    <td>
                                        <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold">Airline
                                    </td>
                                    <td width="130">
                                        <asp:TextBox ID="txt_Airline" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td width="80" style="font-weight: bold">&nbsp; Trip
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Trip" runat="server" Width="110px">
                                            <asp:ListItem Selected="True" Value="D">Domestic</asp:ListItem>
                                            <asp:ListItem Value="I">International</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="td_Agency" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold">Agency Name
                                    </td>
                                    <td align="left">
                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="120">&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" />&nbsp;&nbsp;<asp:Button
                                            ID="btn_export" runat="server" CssClass="buttonfltbk" Text="Export" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #FF0000">* N.B: To get Today's booking without above parameter,do not fill any field,<br />
                            &nbsp; only click on search your booking.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="Div_Export" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="margin: auto">
            <tr id="tr_sale" runat="server" visible="false">

                <td align="center" style="padding-top: 10px;">

                    <table border="0" cellpadding="10" cellspacing="10">
                        <tr>

                            <td>
                                <fieldset style="padding: 5px; border: thin solid #004b91;">
                                    <legend style="color: #004b91; font-weight: bold;">Net Margin Report</legend>
                                    <table border="0" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">Total Sale :
                                            </td>
                                            <td style="color: #000; font-size: 12px; padding-right: 10px;">
                                                <asp:Label ID="lbltotalsale" runat="server"></asp:Label>
                                            </td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">Total Refund :
                                            </td>
                                            <td style="color: #000; font-size: 12px; padding-right: 10px;">
                                                <asp:Label ID="lbltotalrefund" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">Net Sale :
                                            </td>
                                            <td style="color: #000; font-size: 12px; padding-right: 10px;">
                                                <asp:Label ID="lbl_NetSale" runat="server"></asp:Label>
                                            </td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">Total Ticket :
                                            </td>
                                            <td style="color: #000; font-size: 12px; padding-right: 10px;">
                                                <asp:Label ID="lbl_TotalTicket" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">RefundTicket :</td>
                                            <td style="color: #000; font-size: 12px; padding-right: 10px;">
                                                <asp:Label ID="lbl_RefundTicket" runat="server"></asp:Label></td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 10px;">ReissueTicket :</td>
                                            <td style="color: #000; font-size: 12px">
                                                <asp:Label ID="lbl_ReissueTicket" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>

                            <td id="agent_address" runat="server" visible="false">
                                <fieldset style="padding: 5px; border: thin solid #004b91;">
                                    <legend style="color: #004b91; font-weight: bold;">Agency Detail</legend>
                                    <table border="0" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Agent ID :</td>
                                            <td id="td_agentid" runat="server"></td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Type :</td>
                                            <td id="td_agenttype" runat="server"></td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Pan No :</td>
                                            <td id="td_Pan" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Address:</td>
                                            <td id="td_Address" runat="server" colspan="6"></td>
                                        </tr>

                                        <%--<tr>
                     <td></td>
                     <td id="td_Address1" runat="server" colspan="5" ></td>
                     </tr>--%>
                                        <tr>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Mobile No :</td>
                                            <td id="td_Mobile" runat="server"></td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">EmailID :</td>
                                            <td id="td_Email" runat="server"></td>
                                            <td style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; padding-right: 5px;">Sales Ref :</td>
                                            <td id="td_SalesRef" runat="server"></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>



                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lbl_report" runat="server">

                    </asp:Label>
                    <%--<asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>--%>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
