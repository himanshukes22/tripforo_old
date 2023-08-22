<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HtlRefundInProcces.aspx.vb" Inherits="HtlRefundInProcces"  %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script src="../../Hotel/JS/HotelRefund.js" type="text/javascript"></script>
 <%--<link href="<%=ResolveUrl("~/Hotel/css/B2Bhotelengine.css") %>" rel="stylesheet" type="text/css" />--%>
     <div class="mtop80"></div>
<div class="large-12 medium-12 small-12">
    <div class="large-3 medium-3 small-12 columns">     
                <%--<uc1:HotelMenu runat="server" ID="Settings"></uc1:HotelMenu>--%>           
    </div>

<div class="large-8 medium-8 small-12 columns end">
            <div class="large-12 medium-12 small-12 heading">
                        <div class="large-12 medium-12 small-12 heading1">
            Accepted hotel refund details
        </div>
   
            <div id="htlRfndPopup" style="height:100%; display:none;">
                <div class="refundbox" style="width:25%;">
                    <div class="rfndClose">
                        <a href="javascript:closepopup();">
                            <img src="<%=ResolveUrl("~/Images/close.png") %>" height="20px" /></a>
                    </div>
                    <div>
                        <span style="float: left; font-size:12px;">
                            RERECT REMARK OF ORDER ID: <input id="OrderIDS" name="OrderIDS" type="text" style="border:0;font-size:12px;" />
                        </span>
                        <br />
                        <textarea id="txtRemark" name="txtRemark" cols="40" rows="4"></textarea>
                    </div>
                    <div align="right">
                    <asp:Button ID="btnRemark" runat="server" Text="Submit" CssClass="button" 
                            OnClientClick="return RemarkValidate();" style="height: 26px" />
                    </div>
                </div>
            </div></div></div>
       
        <div class="large-12 medium-12 small-12">
            <asp:GridView ID="InproccesGrd" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px"  >
                <Columns>
                <asp:TemplateField HeaderText="Order ID">
                    <ItemTemplate>
                       <a href='../../Hotel/BookingSummaryHtl.aspx?OrderId=<%#Eval("OrderId")%> &BID=<%#Eval("BookingID")%>'
                         rel="lyteframe" rev="width: 830px; height: 400px; overflow:hidden;" target="_blank"
                         style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #004b91">
                         <asp:Label ID="lblBID" runat="server" Text= '<%#Eval("OrderId")%>'></asp:Label></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Booking ID">
                    <ItemTemplate>
                         <asp:LinkButton ID="lnkupdate" runat="server" Text='<%#Eval("BookingID") %>' ForeColor="Red" CommandName="lnkupdate"
                                CommandArgument='<%#Eval("OrderID")%>' onclick="lnkupdate_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>       
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                <asp:BoundField DataField="RoomName" HeaderText="Room Name" />
                <asp:BoundField  DataField="AgencyName" HeaderText ="Agency Name" />
                <asp:BoundField DataField="NetCost" HeaderText="Net Cost" />
                <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                <asp:BoundField DataField="PgTitle" HeaderText="Title" />
                <asp:BoundField DataField="PgFirstName" HeaderText="First Name" />
                <asp:BoundField DataField="PgLastName" HeaderText="Surname" />
                <asp:BoundField DataField="PgEmail" HeaderText="Email_ID" />
                <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                <asp:BoundField DataField="RequestDate" HeaderText="Submit Date" />
                <asp:BoundField HeaderText="Accept Date" DataField="AcceptDate" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <img alt="Reject" title="Reject" src='<%# ResolveClientUrl("~/Images/reject.png") %>'  border="0" onclick="RejectRemark('<%#Eval("OrderId") %>');" />
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
   
</div>
</asp:Content>