<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="HoldHotelBooking.aspx.vb" Inherits="SprReports_Hotel_HoldHotelBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <div class="mtop80"></div>
    <div class="row">
        <div class="col-md-12 text-center search-text  ">
         Check Hold Hotel Status
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="GrdReport" runat="server" AutoGenerateColumns="False" GridLines="None" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Order ID">
                                            <ItemTemplate>
                                                <a href='../../Hotel/BookingSummaryHtl.aspx?OrderId=<%#Eval("OrderId")%>' rel="lyteframe"
                                                    rev="width: 830px; height: 400px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                    <asp:Label ID="lblBID" runat="server" Text='<%#Eval("OrderID")%>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                                        <asp:BoundField DataField="RoomName" HeaderText="Room Name" />
                                        <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                                          <asp:BoundField DataField="BookingID" HeaderText="Booking ID" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                         <asp:TemplateField HeaderText="Check Hotel Status">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkCheckHotelStatus" runat="server" Font-Strikeout="False" Font-Overline="False"
                        CommandArgument='<%#Eval("OrderID") %>' CommandName='<%#Eval("HotelName") %>' ToolTip="Check Hotel Status" OnClick="lnkCheckHotelStatus_Click">
                                 <img src="../../Images/refund.jpg" border="0" />
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
        </div>
   
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
</asp:Content>
