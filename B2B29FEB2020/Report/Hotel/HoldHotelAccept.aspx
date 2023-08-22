<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HoldHotelAccept.aspx.vb" Inherits="SprReports_Hotel_HoldHotelAccept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/Hotel/css/B2Bhotelengine.css") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script src="../../Hotel/JS/HotelRefund.js" type="text/javascript"></script>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
            <%--<uc1:HotelMenu runat="server" ID="Settings"></uc1:HotelMenu>--%>
        </div>
        <div class="large-8 medium-8 small-12 columns end">
            <div class="large-12 medium-12 small-12 heading">
                <div class="large-12 medium-12 small-12 heading1">
                    Accepted for confirm of Hold hotel booking
                </div>
            </div>
        </div>

        <div class="large-12 medium-12 small-12">
            <asp:GridView ID="HoldHotelAcceptGrd" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                <Columns>
                    <asp:TemplateField HeaderText="Order ID">
                        <ItemTemplate>
                            <a href='../../Hotel/BookingSummaryHtl.aspx?OrderId=<%#Eval("OrderId")%>' rel="lyteframe"
                                rev="width: 830px; height: 400px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #006600">
                                <asp:Label ID="lblBID" runat="server" Text='<%#Eval("OrderID")%>'></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ModifyStatus" HeaderText="Status" />
                    <asp:BoundField DataField="AgentID" HeaderText="Agent ID " />
                    <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                    <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                    <asp:BoundField DataField="RoomName" HeaderText="Room Name" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                    <asp:BoundField DataField="NetCost" HeaderText="Net Cost" />
                    <asp:BoundField DataField="BookingDate" HeaderText="Request Date" />
                    <asp:TemplateField HeaderText="Update|Reject">
                        <ItemTemplate>
                            <img alt="Update" title="Update" src='<%# ResolveClientUrl("~/Images/accept.png") %>'
                                border="0" onclick="ShowHoldBookingPopup('<%#Eval("OrderID") %>', 'Update');" height="29px" />
                            <img alt="Reject" title="Reject" src='<%# ResolveClientUrl("~/Images/reject.png") %>'
                                border="0" onclick="ShowHoldBookingPopup('<%#Eval("OrderId") %>', 'Reject');" height="29px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div id="htlRfndPopup" style="height: 100%;">
            <div class="refundbox large-6 medium-6 small-12">

                <div class="bld large-6 medium-6 small-6 columns">
                    Update Hold Hotel Booking
                </div>
                <div class="large-6 medium-6 small-6 columns">
                    <a href="javascript:ShowHoldBookingPopup(' ', 'ClosePopup');">
                        <img src="<%=ResolveUrl("~/Images/close.png") %>" height="20px" />
                    </a>
                </div>
                <div class="clear"></div>
                <div class="large-12 medium-12 small-12" id="BID">

                    <div class="bld large-6 medium-6 small-6 columns">
                        Order ID:
                    </div>
                    <div class="bld large-6 medium-6 small-6 columns">
                        <input id="OrderIDS" name="OrderIDS" type="text" />
                    </div>
                    <div class="clear"></div>
                    <div class="bld large-6 medium-6 small-6 columns">
                        Remark:
                    </div>
                    <div class="large-6 medium-6 small-6 columns">
                        <textarea id="txtRemark" name="txtRemark" cols="31" rows="2" style="float: left;"></textarea>
                    </div>

                    <div id="TR_Reject" style="display: block">

                        <div align="right">
                            <asp:Button ID="btnHoldReject" runat="server" Text="Reject" class="buttonfltbk" OnClientClick="return RemarkValidate();" />
                        </div>
                    </div>
                    <div id="TRBooedUpdate" visible="false">

                        <div class="bld large-6 medium-6 small-6 columns">
                            Booking ID:
                        </div>
                        <div class="large-6 medium-6 small-6 columns">
                            <input id="txtHtlBID" name="txtHtlBID" type="text" />
                        </div>
                        <div class="clear"></div>
                        <div class="bld large-6 medium-6 small-6 columns">
                            Confirm No:
                        </div>
                        <div class="large-6 medium-6 small-6 columns">
                            <input id="txtHtlConfNo" name="txtHtlConfNo" type="text" />
                        </div>
                        <div class="clear"></div>
                        <div align="right">
                            <asp:Button ID="btnHoldUpdate" runat="server" Text="Update" class="buttonfltbk" OnClientClick="return HoldBookingValidate();"
                                Style="height: 29px" />
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>


