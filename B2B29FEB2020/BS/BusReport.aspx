<%@ Page Title="" Language="VB" MasterPageFile="~/MasterForHome.master" AutoEventWireup="false"
    CodeFile="BusReport.aspx.vb" Inherits="SprReports_BusReport" %>


<%--<%@ Register Src="~/UserControl/BUSControl.ascx" TagPrefix="busMenu" TagName="busMenu" %>--%>


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
        .asvd {
            width:120%!important;
            max-width:120%!important;
        }
    </style>
    <div class="mtop80"></div>
    <div class="row">
        <div class="col-md-12 text-center search-text  ">
            Search Bus Booking Details
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
                <div class="form-groups" id="tdddl_Status" runat="server">
                    <asp:DropDownList ID="ddl_Status" class="form-control" runat="server">
                        <asp:ListItem Value="BOOKED">Booked</asp:ListItem>
                        <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                        <asp:ListItem Value="Hold">Hold</asp:ListItem>
                        <asp:ListItem Value="Request">Requested</asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                <div class="form-groups" id="td_Agency" runat="server">
                    <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btn_result" runat="server" class="buttonfltbks" Text="Search Result" />
            <asp:Button ID="btn_export" runat="server" class="buttonfltbk" Text="Export"  />
        </div>
    </div>
      <div class="row" style="padding: 10px 10px 10px 10px;">
        <div class="col-md-10" style="padding-left: 100px;">
            <div style="color: #FF0000">
                * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
            </div>
        </div>

    </div>
    <div class="row" style="padding: 10px 10px 10px 10px;">
         <div class="col-md-5">
             </div>
        <div class="col-md-5">
            Total Amount :&nbsp;&nbsp;
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Total Ticket Count :&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_Sales" runat="server"></asp:Label> 
        </div>
    </div>
  
 
    <table width="100%">
   
        <tr>
            <td align="center">
                <asp:GridView ID="GrdBusReport" runat="server" AutoGenerateColumns="False" PageSize="500"
                    CssClass="table table-hover asvd" GridLines="None" Font-Size="12px">
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
                        <asp:BoundField DataField="AGENTID" HeaderText="Agent&nbsp;Id" />
                        <asp:BoundField DataField="AGENCY_NAME" HeaderText="Agency&nbsp;Name" />
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
