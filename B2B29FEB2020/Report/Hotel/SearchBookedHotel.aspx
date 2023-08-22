<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SearchBookedHotel.aspx.vb"
    MasterPageFile="~/MasterAfterLogin.master" Inherits="SprReports_Hotel_SearchBookedHotel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Hotel/css/HotelStyleSheet.css" rel="stylesheet" type="text/css" />
    
       <div>
        <table cellpadding="4" cellspacing="4" align="center" class="tbltbl" style="font-weight: bold;">
            <tr>
                <td align="left" style="color: #004b91; font-size: 20px; font-weight: bold; padding-left: 20px;">
                    Search GTA booked hotel
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="padding-left: 20px">
                                From:
                            </td>
                            <td >
                                <input type="text" name="From" id="From" class="txtCalander" value="" readonly="readonly" />
                                 <input id="hidtxtDepDate" name="hidtxtDepDate" value="" type="hidden" />
                            </td>
                            <td style="padding-left: 11px">
                                To:
                            </td>
                            <td>
                                <input type="text" name="To" id="To" value="" class="txtCalander" readonly="readonly" />
                                 <input id="hidtxtRetDate" name="hidtxtRetDate" value="" type="hidden" />
                            </td>
                            <td style="padding-left: 20px">
                                Order ID:
                            </td>
                            <td> 
                                <input type="text" name="orderidTxt" id="orderidTxt" value="" class="txtBox" />
                            </td>
                              <td style="padding-left: 20px">
                                Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="BookingStatus" runat="server" class="CheckoutDDL" Width="110px">
                                    <asp:ListItem Text="Confirm" Value="C" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pendin Confirm" Value="PC"></asp:ListItem>
                                    <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <td style="padding-left: 20px">
                                Result Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ReqType" runat="server" class="CheckoutDDL" Width="110px">
                                    <asp:ListItem Text="Result Type" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="XML Type" Value="XMLtype"></asp:ListItem>
                                    <asp:ListItem Text="Grid Type" Value="SpringType"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <td>
                                <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="buttonfltbk" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
        <div>
        <table cellpadding="0" cellspacing="0" class="tbltbl">
            <tr>
                <td id="BookedHotel" runat="server"></td>
            </tr>
        </table>
        </div>
    </div>
    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
        document.getElementById("From").value = currDate;
        document.getElementById("To").value = currDate;
        document.getElementById("hidtxtDepDate").value = currDate;
        document.getElementById("hidtxtRetDate").value = currDate;
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.8.custom.min.js" type="text/javascript"></script>
    <script src="../../Hotel/JS/HotelMarkup.js" type="text/javascript"></script>
</asp:Content>
