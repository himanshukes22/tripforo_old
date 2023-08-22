<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HotelBookingConfirmation.aspx.vb" Inherits="HotelBookingConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function printSelection(node) {
        $(".CacncelSelection").hide();
      var content=node.innerHTML
      var pwin=window.open('','print_content','width=740,height=443');
      pwin.document.open();
      pwin.document.write('<html><body onload="window.print()">'+content+'</body></html>');
      pwin.document.close();
      $(".CacncelSelection").show();
    }
</script>
    <style type="text/css">
        li{list-style:none;}

    </style>

    <table style="width: 60%; margin: 0 auto;" align="center">
        <tr>
            <td>
               <fieldset style="padding: 10px; text-align:center; border: 2px dotted #dbecf3; -webkit-border-radius:10px; -moz-border-radius:10px; -o-border-radius:10px; -khtml-border-radius:10px;">
                    <legend style="border: 2px dotted #dbecf3; font-weight: bold; padding: 5px 20px;">
                    <a href="" onclick="printSelection(document.getElementById('hidtktCopy'));return false">Print </a></legend>
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td colspan="2" style="text-align: center; height: 20px; background-color: #0e4faa;
                                color: #fff; font-weight: bold; -webkit-border-radius: 10px 10px 0 0;
                                    -moz-border-radius: 10px 10px 0 0; -o-border-radius: 10px 10px 0 0; -khtml-border-radius: 10px 10px 0 0; height:25px;">
                                BOOKING CONFIRMATION
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color:#fff;" >
                             <table style="width: 100%; text-align: center; margin: auto;">
                                    <tr bgcolor="#eee" style="font-weight: bold; height: 22px; line-height: 22px; ">
                                        <td style="border: 1px solid #eee;">
                                            Booking ID 
                                        </td>
                                        <td style="border: 1px solid #eee;">
                                            Order ID 
                                        </td>
                                        <td style=" border: 1px solid #eee;">
                                             Status 
                                        </td>
                                        <td style="border: 1px solid #eee;">
                                            Booking Reference
                                        </td>
                                        <td style="border: 1px solid #eee;">
                                            Total Amount
                                        </td>
                                    </tr>
                                    <tr  bgcolor="#fff" style="  height: 22px; line-height: 22px;">
                                        <td style=" border: 1px solid #eee; background-color:#fff; " ID="lblbookingID" runat="server"></td>
                                        <td style=" border: 1px solid #eee; background-color:#fff;" ID="TdOrderid" runat="server"></td>
                                        <td style=" border: 1px solid #eee;background-color:#fff;" ID="lblStatus" runat="server"></td>
                                          <td style=" border: 1px solid #eee;background-color:#fff;" ID="lblBookingReference" runat="server"></td>
                                        <td style=" border: 1px solid #eee;background-color:#fff;" ID="lblTotal2" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" id="HoldBookingTD" runat="server" visible="false" style=" font-weight:bold; font-size:16px;color:Red;">
                                           Note: Your hotel booking is inprocess, for any inconvenience of booking again. Please call our customer care.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20px" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color: #0e4faa;">
                                <div style=" text-align: center; margin: auto; font-weight: bold;
                                    height: 25px; line-height: 25px; -webkit-border-radius: 10px 10px 0 0;
                                    -moz-border-radius: 10px 10px 0 0; -o-border-radius: 10px 10px 0 0; -khtml-border-radius: 10px 10px 0 0;
                                    color: #fff;">
                                    HOTEL DETAILS
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table align="center" width="100%" cellspacing="5" style="background-color: #fff;">
                                    <tr bgcolor="#eee" style="font-weight: bold;height: 22px; line-height: 22px;">
                                        <td width="40%" align="center" style="border: 1px solid #eee;">
                                            Hotel Name
                                        </td>
                                        <td width="20%" align="center" style="border: 1px solid #eee;">
                                            Check In Date
                                        </td>
                                        <td width="20%" align="center" style=" border: 1px solid #eee;">
                                            Check Out Date
                                        </td>
                                         <td width="20%" align="center" style=" border: 1px solid #eee;">
                                            Booking Date
                                        </td>
                                    </tr>
                                    <tr bgcolor="#fff"  style="  border: 1px solid #eee; height: 22px; line-height: 22px;">
                                        <td width="40%" align="center" style="border: 1px solid #eee; background-color: #fff;">
                                            <asp:Label ID="lblHotelName" runat="server"></asp:Label>
                                        </td>
                                       <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff;">
                                            <asp:Label ID="lblcheckin" runat="server"></asp:Label>
                                        </td>
                                        <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff;">
                                            <asp:Label ID="lblCheckout" runat="server"></asp:Label>
                                        </td>
                                         <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff;">
                                            <asp:Label ID="lblBookingDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr bgcolor="#eee" style="border: 1px solid #eee; height: 22px; line-height: 22px;font-weight: bold;">
                                        <td width="40%" align="center" style=" border: 1px solid #eee;">
                                            Room Type
                                        </td>
                                       <td width="20%" align="center" style=" border: 1px solid #eee;">
                                                        Hotel Phone No
                                        </td>         
                                        <td width="20%" align="center" style=" border: 1px solid #eee;">
                                                        Hotel Address
                                        </td>
                                          <td width="20%" align="center" style=" border: 1px solid #eee;">
                                                        Meal Type
                                        </td>
                                     </tr>
                                    <tr bgcolor="#fff" style="border: 1px solid #eee; height: 22px; line-height: 22px;">
                                        <td width="40%" align="center" style="border: 1px solid #eee;background-color: #fff; ">
                                            <asp:Label ID="lblRoomType" runat="server"></asp:Label>
                                        </td>
                                         <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff; " id="htlPhone" runat="server">
                                        </td>
                                        <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff; " id="htlFax" runat="server">
                                        </td>
                                        <td width="20%" align="center" style="border: 1px solid #eee;background-color: #fff; " id="htlMealIncluded" runat="server">
                                        </td>
                                    </tr>
                                </table>
                               
                            </td>
                        </tr>
                        <tr>
                            <td height="10px" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table align="center" width="100%">
                                    <tr>
                                        <td colspan="5" style="background-color: #0e4faa;">
                                            <div style="text-align: center; margin: auto; font-weight: bold;
                                                height: 25px; line-height: 25px; -webkit-border-radius: 10px 10px 0 0;
                                                -moz-border-radius: 10px 10px 0 0; -o-border-radius: 10px 10px 0 0; -khtml-border-radius: 10px 10px 0 0;
                                                color: #fff;">
                                                GUESTS</div>
                                        </td>
                                    </tr>
                                    <tr bgcolor="#ffffff" style="border: 1px solid #eee; height: 22px; line-height: 22px;font-weight: bold;">
                                        <td width="25%" style="padding-left: 10px;  border: 1px solid #eee;" align="center">
                                            No. of Adults
                                        </td>
                                        <td width="25%" style="padding-left: 10px; border: 1px solid #eee;"
                                            align="center">
                                            No. of Childs
                                        </td>
                                        <td colspan="2" width="25%" align="center" style="border: 1px solid #eee;">
                                            No. of Rooms
                                        </td>
                                        <td width="25%" align="center" style="border: 1px solid #eee;">
                                            No. of Night
                                        </td>
                                    </tr>
                                    <tr style="border: 1px solid #eee; height: 22px; line-height: 22px;">
                                        <td align="center" style="border: 1px solid #eee; ">
                                            <asp:Label ID="lblAdults" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" style="border: 1px solid #eee;">
                                            <asp:Label ID="lblChilds" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2" align="center" style="border: 1px solid #eee;">
                                            <asp:Label ID="lblRoom" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" style="border: 1px solid #eee;">
                                            <asp:Label ID="lblnight" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20px" colspan="2">
                            </td>
                        </tr>
                       
                          <tr>
                            <td colspan="2">
                                <table align="center" width="100%">
                                    <tr>
                                        <td colspan="5" style="background-color: #0e4faa;">
                                            <div style="text-align: center; margin: auto; font-weight: bold;
                                                height: 25px; line-height: 25px; -webkit-border-radius: 10px 10px 0 0;
                                                -moz-border-radius: 10px 10px 0 0; -o-border-radius: 10px 10px 0 0; -khtml-border-radius: 10px 10px 0 0;
                                                color: #fff;">
                                                CONTACT DETAILS</div>
                                        </td>
                                    </tr>
                                    <tr bgcolor="#ffffff" style="border: 1px solid #eee; height: 22px; line-height: 22px;font-weight: bold;">
                                        <td style="padding-left: 10px;  border: 1px solid #eee;" align="center">
                                            Guest Name
                                        </td>
                                        <td  style="padding-left: 10px;  border: 1px solid #eee;"
                                            align="center">
                                            Phone Number
                                        </td>
                                        <td  align="center" style=" border: 1px solid #eee;">
                                           Email ID
                                        </td>
                                    </tr>
                                    <tr style="border: 1px solid #eee; height: 22px; line-height: 22px;">
                                        <td align="center" style="border: 1px solid #eee;" id="TDGuestName" runat="server">
                                        </td>
                                        <td align="center" style="border: 1px solid #eee;" id="TDGuestMobile" runat="server">
                                        </td>
                                        <td colspan="2" align="center" style="border: 1px solid #eee;">
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20px" colspan="2">
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                             <table align="center" width="100%">
                                    <tr>
                                        <td style="background-color: #0e4faa;">
                                            <div style="text-align: center; margin: auto; font-weight: bold;
                                                height: 25px; line-height: 25px; -webkit-border-radius: 10px 10px 0 0;
                                                -moz-border-radius: 10px 10px 0 0; -o-border-radius: 10px 10px 0 0; -khtml-border-radius: 10px 10px 0 0;
                                                color: #fff;">
                                                CANCELLATION POLICY</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblRules" runat="server"></asp:Label></td>
                                    </tr>
                                    </table>
                                 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 10px;">
                                Thank you for choosing <a href="http://www.RWT.co/">Tripforo</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="hidtktCopy" style="display: none;">
                                <asp:Label ID="BookingCopy" runat="server"></asp:Label>
                                </div>
                                
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language='javascript'>
       function printpage(){window.print();}
    </script>
</asp:Content>
