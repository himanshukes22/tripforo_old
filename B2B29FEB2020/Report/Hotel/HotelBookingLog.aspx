<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="HotelBookingLog.aspx.vb" Inherits="SprReports_Hotel_HotelBookingLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="<%=ResolveUrl("~/Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet" type="text/css" />
    <table cellpadding="4" cellspacing="4" align="center" class="tbltbl" style="color: #004b91;
        font-size: 13px; font-weight: bold;">
        <tr>
            <td colspan="5" style="color: #004b91; font-size: 13px; font-weight: bold; padding-left: 4px;">
                Search Hotel booking log
            </td>
        </tr>
        
        <tr>
            <td>
                No of row:
            </td>
            <td>
                <input id="NoofRow" type="text" name="NoofRow" class="txtBox" onkeypress="return isNumberKey(event)" class="txtBox" />
            </td>
            <td style="padding-left: 4px">
                Order ID:
            </td>
            <td>
                <input id="txtOrderId" type="text" class="txtBox" name="txtOrderId" class="txtBox" />
            </td>
            <td style="padding-left: 11px">
                Agent ID:
            </td>
            <td>
                <input id="txtAgentID" type="text" name="txtAgentID" class="txtBox" />
            </td>
             <td style="padding-left: 11px">
                Provider:
            </td>
            <td>
                <asp:DropDownList ID="Provider" runat="server">
                  <asp:ListItem Text="Provider" Value="" Selected="True"></asp:ListItem>
                       <asp:ListItem Text="Desiya" Value="TG"></asp:ListItem>
                   <asp:ListItem Text="ZUMATA" Value="ZUMATA"></asp:ListItem>
                     <asp:ListItem Text="GDS" Value="GAL"></asp:ListItem>
                  <asp:ListItem Text="INNSTANT" Value="INNSTANT"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="padding-left: 11px">
                Log Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlReqType" runat="server">
                    <asp:ListItem Text="Search" Value="Search" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="RoomDetails" Value="RoomDetails"></asp:ListItem>
                    <asp:ListItem Text="PreBooking" Value="PreBooking"></asp:ListItem>
                    <asp:ListItem Text="Policy" Value="Policy"></asp:ListItem>
                    <asp:ListItem Text="Booking" Value="Booking"></asp:ListItem>
                    <asp:ListItem Text="Cancellation" Value="Cancellation"></asp:ListItem>
                     <asp:ListItem Text="PaymentCreate" Value="PaymentCreate"></asp:ListItem>
                    <asp:ListItem Text="PaymentAuthorize" Value="PaymentAuthorize"></asp:ListItem>
                    <asp:ListItem Text="BookingStaus" Value="BookingStaus"></asp:ListItem>
                    <asp:ListItem Text="ErrorLog" Value="ErrorLog"></asp:ListItem>
                </asp:DropDownList>
            </td>
            
            <td align="right">
                <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="buttonfltbk" />
            </td>
        </tr>
    </table>
    <table width="94%" style="padding-left: 22px;" >
        <tr>
            <td align="center">
                <asp:Label ID="reqResp" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<%-- <tr>
            <td id="ReqTitle" runat="server">
            </td>
        </tr>
        <tr>
            <td id="Req" runat="server">
            </td>
        </tr>
        <tr>
            <td id="RespTitle" runat="server">
            </td>
        </tr>
        <tr>
            <td id="Resp" runat="server">
            </td>
        </tr>--%>
