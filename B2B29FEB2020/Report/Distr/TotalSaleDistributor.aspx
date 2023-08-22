<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="TotalSaleDistributor.aspx.vb" Inherits="SprReports_Distr_TotalSaleDistributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
        <style type="text/css">
        input[type="text"], input[type="password"], select, radio, legend, fieldset
        {
            border: 1px solid #004b91;
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
                <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;" width="100%">
                    <tr>
                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                            Stockist Total Sale Report
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            From Date
                        </td>
                        <td width="130">
                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                style="width: 100px" />
                        </td>
                        <td width="90" style="font-weight: bold">
                            To Date
                        </td>
                        <td >
                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            Stockist Id:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_DistrID" runat="server" CssClass="drpBox">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" align="right">
                            <asp:Button ID="btn_serach" runat="server" Text="Search" CssClass="button" />&nbsp;<asp:Button ID="btn_export" runat="server" Text="Export" CssClass="button" />
                        </td>
                    </tr>
                    <%--  <tr>
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
                                    <td width="90px" style="font-weight: bold">
                                        Status
                                    </td>
                                    <td width="120px">
                                        <asp:DropDownList ID="ddl_Status" runat="server">
                                            
                                            <asp:ListItem Value="BOOKED">Booked</asp:ListItem>
                                            <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                            <asp:ListItem Value="Hold">Hold</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100px" id="td_Agency" style="font-weight: bold" runat="server">
                                        Agency Name
                                    </td>
                                    <td colspan="2" id="td_Agency1" runat="server">
                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 100px" onfocus="focusObj(this);"
                                            onblur="blurObj(this);" defvalue="Agency Name or ID" value="Agency Name or ID" />
                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />&nbsp;&nbsp;<asp:Button
                                            ID="btn_export" runat="server" CssClass="button" Text="Export" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
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
    <table border="0" cellpadding="0" cellspacing="0" style="margin: auto;">
        <tr>
            <td>
                <%--<asp:GridView ID="Grid_Distr" runat="server">
      </asp:GridView>--%>
                <asp:GridView ID="Grid_Distr" runat="server" AutoGenerateColumns="False" PageSize="500"
                    CssClass="table table-hover" GridLines="None" Font-Size="12px">
                    <Columns>
                        <asp:BoundField DataField="DISTRIBUTORID" HeaderText="DISTRIBUTOR&nbsp;ID" />
                        <asp:BoundField DataField="AIRTICKET" HeaderText="AIR&nbsp;TICKET" />
                        <asp:BoundField DataField="AIRSALE" HeaderText="AIR&nbsp;SALE" />
                        <asp:BoundField DataField="RAILTICKET" HeaderText="RAIL&nbsp;TICKET" />
                        <asp:BoundField DataField="RAILSALE" HeaderText="RAIL&nbsp;SALE" />
                        <asp:BoundField DataField="HOTELBOOKED" HeaderText="HOTELBOOKED" />
                        <asp:BoundField DataField="HOTELSALE" HeaderText="HOTEL&nbsp;SALE" />
                        <asp:BoundField DataField="RECHARGENO" HeaderText="RECHARGE&nbsp;NO" />
                        <asp:BoundField DataField="RECHARGESALE" HeaderText="RECHARGE&nbsp;SALE" />
                        <asp:BoundField DataField="BILLCOUNT" HeaderText="BILL&nbsp;COUNT" />
                        <asp:BoundField DataField="BILLSALE" HeaderText="BILL&nbsp;SALE" />
                        <asp:BoundField DataField="BUSTICKET" HeaderText="BUS&nbsp;TICKET" />
                        <asp:BoundField DataField="BUSSALE" HeaderText="BUS&nbsp;SALE" />
                        <%-- <asp:BoundField DataField="SEATNO" HeaderText="Seat&nbsp;No" />
                        <asp:BoundField DataField="FARE" HeaderText="Fare" />
                        <asp:BoundField DataField="TICKETNO" HeaderText="Ticket&nbsp;No" />
                        <asp:BoundField DataField="PNR" HeaderText="PNR" />
                        <asp:BoundField DataField="BOOKINGSTATUS" HeaderText="Booking&nbsp;Status" />
                        <asp:BoundField DataField="JOURNEYDATE" HeaderText="Journey&nbsp;Date" />
                        <asp:BoundField DataField="CREATEDDATE" HeaderText="Booking&nbsp;Date" />--%>
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
