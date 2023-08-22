<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="BusBookingInvoiceDetails.aspx.vb" Inherits="SprReports_Accounts_BusBookingInvoiceDetails"
    Title="Bus Booking Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
        <tr>
            <td>
                <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;">
                    <tr>
                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                            Bus Register
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold" height="25">
                                        From Date
                                    </td>
                                    <td width="130">
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                            style="width: 100px" />
                                    </td>
                                    <td width="90" style="font-weight: bold">
                                        To Date
                                    </td>
                                    <td width="120px">
                                        <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                    </td>
                                    <td width="80" style="font-weight: bold">
                                        PNR
                                    </td>
                                    <td width="110">
                                        <asp:TextBox ID="txtPnr" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="left" width="80" style="font-weight: bold">
                                        OrderId
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrderID" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold" height="25">
                                        Source
                                    </td>
                                    <td width="130" class="style4">
                                        <asp:TextBox ID="TxtSource" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td width="90" style="font-weight: bold" height="25">
                                        Destination
                                    </td>
                                    <td width="120" class="style4">
                                        <asp:TextBox ID="TxtDestination" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="left" width="80" style="font-weight: bold" class="style4">
                                        Ticket No
                                    </td>
                                    <td class="style4" width="110px">
                                        <asp:TextBox ID="txtTicketNo" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="left" width="80" style="font-weight: bold">
                                        BusOperator
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBusOperator" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td width="85px" style="font-weight: bold" runat="server">
                                        Status
                                    </td>
                                    <td colspan="2" runat="server" width="100px">
                                        <select name="dropstatus" style="width: 100px">
                                            <option>-Select-</option>
                                            <option>Booked</option>
                                            <option>Cancelled</option>
                                        </select>
                                    </td>
                                    <td width="100px" id="td_Agency" style="font-weight: bold" runat="server">
                                        Agency Name
                                    </td>
                                    <td colspan="2" id="td_Agency1" runat="server">
                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 150px" onfocus="focusObj(this);"
                                            onblur="blurObj(this);" defvalue="Agency Name or ID" value="Agency Name or ID" />
                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
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
                        <td style="color: #FF0000" colspan="4">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GrdBusReport" runat="server" AutoGenerateColumns="False" PageSize="500"
                     CssClass="table table-hover" GridLines="None" Font-Size="12px">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <a href='BusInvoiceDetails.aspx?oid=<%#Eval("ORDERID")%>' rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;"
                                    target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                    font-weight: bold; color: #004b91">Invoice </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <a href='BusInvoiceDetails.aspx?oid=<%#Eval("ORDERID")%>' rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;"
                                    target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                    font-weight: bold; color: #004b91">Credit Note</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Agentid" HeaderText="AgentId" />
                        <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name" />
                        <asp:BoundField DataField="Agency_Name" HeaderText="AgencyName" />
                        <asp:BoundField DataField="ORDERID" HeaderText="Order&nbsp;Id" />
                        <asp:BoundField DataField="TRIPID" HeaderText="Trip&nbsp;Id" />
                        <%--<asp:BoundField DataField="TICKETNO" HeaderText="Ticket&nbsp;No" />--%>
                        <%--<asp:BoundField DataField="PNR" HeaderText="PNR" />--%>
                        <%-- <asp:BoundField DataField="Source" HeaderText="Source" />
                        <asp:BoundField DataField="DESTINATION" HeaderText="Destination" />--%>
                        <asp:BoundField DataField="Sector" HeaderText="Sector" />
                        <asp:BoundField DataField="BUSOPERATOR" HeaderText="Bus&nbsp;Operator" />
                        <%--<asp:BoundField DataField="SEATNO" HeaderText="Seat&nbsp;No" />--%>
                        <%--<asp:BoundField DataField="FARE" HeaderText="Fare" />--%>
                        <asp:BoundField DataField="ADMIN_COMM" HeaderText="Commission" />
                        <asp:BoundField DataField="TA_TDS" HeaderText="Tds" />
                        <asp:BoundField DataField="TA_NET_FARE" HeaderText="Net Fare" />
                        <asp:BoundField DataField="TA_TOT_FARE" HeaderText="Total Fare" />
                        <asp:BoundField DataField="BookingSTATUS" HeaderText="Status" />
                        <asp:BoundField DataField="JOURNEYDATE" HeaderText="JourneyDate" />
                        <asp:BoundField DataField="CREATEDDATE" HeaderText="BookingDate" />
                    </Columns>
                   
                </asp:GridView>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery-ui-1.8.8.custom.min.js" type="text/javascript"></script>

    <script src="../../Scripts/AgencySearch.js" type="text/javascript"></script>

</asp:Content>
