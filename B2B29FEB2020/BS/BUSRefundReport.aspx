<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForHome.master" AutoEventWireup="true" CodeFile="BUSRefundReport.aspx.cs" Inherits="BS_BUSRefundReport" %>

<%@ Register Src="~/UserControl/BUSControl.ascx" TagPrefix="busMenu" TagName="busMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


 
 
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
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
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <div class="mtop80"></div>
    <%--<div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
            <busMenu:busMenu runat="server" ID="busMenu" />
        </div>
        </div>--%>
    <div class="row">
        <div class="col-md-12 text-center search-text  ">
            Bus Refund Report
        </div>
    </div>
    <div class="row ">
        <div class="col-md-10" style="padding-left: 100px;">
            <div class="form-inlines">
                <div class="form-groups">
                    <input type="text" name="From" id="From" placeholder="From Date" class="form-control" readonly="readonly" />
                </div>
                <div class="form-groups">
                    <input type="text" name="To" placeholder="To Date" id="To" class="form-control" readonly="readonly" />
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="txtPnr" placeholder="PNR" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="txtOrderID" placeholder="OrderId" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="TxtSource" placeholder="Source" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="TxtDestination" placeholder="Destination" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="txtTicketNo" placeholder="Ticket No." class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="txtBusOperator" placeholder="BusOperator" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btn_result" runat="server" class="buttonfltbks" Text="Search Result" OnClick="btn_result_Click" />
            <asp:Button ID="btn_export" runat="server" class="buttonfltbk" Text="Export" OnClick="btn_export_Click" />
        </div>
    </div>
    <table width="100%">
        <%--  <tr>
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
        </tr>--%>
        <tr>
            <td align="center">
                <asp:GridView ID="GrdBusReport" runat="server" AutoGenerateColumns="False"   PageSize="500"
                    CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='../BS/TicketSummary.aspx?tin=<%#Eval("TICKETNO")%>&oid=<%#Eval("ORDERID")%>'
                                    rel="lyteframe" rev="width: 1000px; height: 500px; overflow:hidden;" target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                    <asp:Label ID="TKTNO" runat="server" Text='<%#Eval("TICKETNO")%>'></asp:Label>(Summary)
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <a href='../BS/CancelTicket.aspx?tin=<%#Eval("TICKETNO")%>&oid=<%#Eval("ORDERID")%>'
                                    rel="lyteframe" rev="width: 700px; height: 300px; overflow:hidden;" target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">(CancelTicket) </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="AGENTID" HeaderText="Agent&nbsp;Id" />--%>
                        <%-- <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency&nbsp;Name" />--%>
                        <asp:BoundField DataField="ORDERID" HeaderText="Order&nbsp;Id" />                     
                        <asp:BoundField DataField="SOURCE" HeaderText="Source" />
                        <asp:BoundField DataField="DESTINATION" HeaderText="Destination" />
                        <asp:BoundField DataField="BUSOPERATOR" HeaderText="Bus&nbsp;Operator" />
                        <asp:BoundField DataField="SEATNO" HeaderText="Seat&nbsp;No" />
                        <asp:BoundField DataField="TA_NET_FARE" HeaderText="NetFare" />
                         <asp:BoundField DataField="CancelCharge" HeaderText="Cancellation Charge" />
                         <asp:BoundField DataField="REFUND_AMT" HeaderText="Refund Amount" />
                        <%-- <asp:BoundField DataField="REFUND_SERVICECHRG" HeaderText="REFUND_SERVICECHRG" />--%>
                        <asp:BoundField DataField="TICKETNO" HeaderText="Ticket&nbsp;No" />
                        <asp:BoundField DataField="PNR" HeaderText="PNR" />
                        <asp:BoundField DataField="BOOKINGSTATUS" HeaderText="Booking&nbsp;Status" />
                        <asp:BoundField DataField="JOURNEYDATE" HeaderText="Journey&nbsp;Date" />
                        <asp:BoundField DataField="CREATEDDATE" HeaderText="Booking&nbsp;Date" />
                        <asp:BoundField DataField="PaymentMode" HeaderText="PaymentMode" />
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

