<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="BUSReissueReport.aspx.cs" Inherits="BS_BUSReissueReport" %>

<%@ Register Src="~/UserControl/BUSControl.ascx" TagPrefix="busMenu" TagName="busMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
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
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
     <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
            <busMenu:busMenu runat="server" ID="busMenu" />
        </div>
        </div>
    <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
        <tr>
            <td>
                <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;">
                    <tr>
                        <td align="left" style="color: #006600; font-size: 13px; font-weight: bold">
                           Bus Refund Report
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
                                   <%-- <td width="90px" style="font-weight: bold">
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
                                    </td>--%>
                                    <td align="right">
                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" OnClick="btn_result_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_export" runat="server" CssClass="buttonfltbk" Text="Export" OnClick="btn_export_Click" />
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
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <b>Total Amount :&nbsp;&nbsp;
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Total Ticket Count :&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_Sales" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GrdBusReport" runat="server" AutoGenerateColumns="False" PageSize="500"
                    CssClass="GridViewStyle" GridLines="None" Width="974px">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='../BS/TicketSummary.aspx?tin=<%#Eval("TICKETNO")%>&oid=<%#Eval("ORDERID")%>'
                                    rel="lyteframe" rev="width: 1000px; height: 500px; overflow:hidden;" target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                    color: #006600">
                                    <asp:Label ID="TKTNO" runat="server" Text='<%#Eval("TICKETNO")%>'></asp:Label>(Summary)
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <a href='../BS/CancelTicket.aspx?tin=<%#Eval("TICKETNO")%>&oid=<%#Eval("ORDERID")%>'
                                    rel="lyteframe" rev="width: 700px; height: 300px; overflow:hidden;" target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                    color: #006600">(CancelTicket) </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AGENTID" HeaderText="Agent&nbsp;Id" />
                       <%-- <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency&nbsp;Name" />--%>
                        <asp:BoundField DataField="ORDERID" HeaderText="Order&nbsp;Id" />
                        <asp:BoundField DataField="TRIPID" HeaderText="Trip&nbsp;Id" />
                        <asp:BoundField DataField="SOURCE" HeaderText="Source" />
                        <asp:BoundField DataField="DESTINATION" HeaderText="Destination" />
                        <asp:BoundField DataField="BUSOPERATOR" HeaderText="Bus&nbsp;Operator" />
                        <asp:BoundField DataField="SEATNO" HeaderText="Seat&nbsp;No" />
                        <asp:BoundField DataField="TA_NET_FARE" HeaderText="Fare" />
                        <asp:BoundField DataField="TICKETNO" HeaderText="Ticket&nbsp;No" />
                        <asp:BoundField DataField="PNR" HeaderText="PNR" />
                        <asp:BoundField DataField="BOOKINGSTATUS" HeaderText="Booking&nbsp;Status" />
                        <asp:BoundField DataField="JOURNEYDATE" HeaderText="Journey&nbsp;Date" />
                        <asp:BoundField DataField="CREATEDDATE" HeaderText="Booking&nbsp;Date" />
                    </Columns>
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
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

