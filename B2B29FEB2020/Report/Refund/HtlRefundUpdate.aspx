<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HtlRefundUpdate.aspx.vb"
    Inherits="HtlRefundUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Hotel/JS/HotelRefund.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0">
            <tr>
                <td>
                    <table cellspacing="4" cellpadding="0" style="">
                        <tr>
                            <td align="center" style="font-weight: bold; font-size: large; height: 31px;">Update Hotel Cancellation
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" style="padding: 0 5px">
                                    <tr>
                                        <td style="font-weight: bold; font-size: medium;">Agent Detail
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table align="left">
                                                <tr>
                                                    <td class="TextBig" height="20px">Agent ID:
                                                    </td>
                                                    <td id="td_AgentID" runat="server" width="110px"></td>
                                                    <td class="TextBig">Agency Name:
                                                    </td>
                                                    <td id="td_AgencyName" runat="server" width="220px"></td>
                                                    <td class="TextBig">Agent Name:
                                                    </td>
                                                    <td id="td_AgentName" runat="server" width="220px"></td>
                                                    <td class="TextBig">Available Credit Limit:
                                                    </td>
                                                    <td id="td_CardLimit" runat="server"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="TextBig">Address:
                                                    </td>
                                                    <td id="td_AgentAddress" runat="server" width="425px"></td>
                                                    <td class="TextBig">Email:
                                                    </td>
                                                    <td id="td_Email" runat="server" width="256px"></td>
                                                    <td class="TextBig">Mobile No:
                                                    </td>
                                                    <td id="td_AgentMobNo" runat="server"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding: 0 5px">
                    <asp:GridView ID="grd_HtlUpdate" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                        <Columns>
                            <asp:TemplateField HeaderText="Order ID">
                                <ItemTemplate>
                                    <a href='../../Hotel/BookingSummaryHtl.aspx?OrderId=<%#Eval("OrderId")%> &BID=<%#Eval("BookingID")%>'
                                        rel="lyteframe" rev="width: 830px; height: 400px; overflow:hidden;" target="_blank"
                                        style="font-family: Arial, Helvetica, sans-serif; color: #004b91">
                                        <asp:Label ID="lblOrderId" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblBID" runat="server" Text='<%#Eval("BookingID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                            <asp:BoundField DataField="RoomName" HeaderText="Room Name" />
                            <asp:BoundField DataField="PgTitle" HeaderText="Title" />
                            <asp:BoundField DataField="PgFirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="PgLastName" HeaderText="Surname" />
                            <asp:BoundField DataField="PgEmail" HeaderText="Email_ID" />
                            <asp:TemplateField HeaderText="Net Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetCost" runat="server" Text='<%#Eval("NetCost")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                            <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0" style="padding: 11px 5px">
                        <tr>
                            <td style="font-weight: bold;">Cancellation Charge:
                            </td>
                            <td>
                                <input id="txtRefundCharge" type="text" name="txtRefundCharge" style="width: 92px; height: 25px;" onkeypress="return isNumberKey(event)" />
                            </td>
                            <td style="font-weight: bold">Service Charge:
                            </td>
                            <td>
                                <input id="txtServiceCharge" type="text" name="txtServiceCharge" style="width: 92px; height: 25px;" onkeypress="return isNumberKey(event)" />
                            </td>
                            <td style="font-weight: bold;">Remarks:
                            </td>
                            <td>
                                <textarea id="txtRemark" cols="31" rows="2" name="txtRemark"></textarea>
                            </td>
                            <td align="center">
                                <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return RefundUpdateValidate();"
                                    CssClass="button" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tbltbl" style="padding: 11px 5px">
                    <asp:Label ID="lblCancelPoli" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
