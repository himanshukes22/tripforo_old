<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateHoldPNRIntl.aspx.vb"
    Inherits="Reports_HoldPNR_UpdateHoldPNRIntl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58 || charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divcls">
        <table width="800px" border="0" cellpadding="0px" cellspacing="0">
            <tr>
                <%--<td style="width:10px">
      </td>--%>
                <td style="width: 800px">
                    <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                    <tr>
                                        <td class="Proxy" style="padding-left: 10px" id="td_agencyinfo" runat="server" >
                                           <%-- Agency Informaton --%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_AgenctName" style="padding-left: 10px; padding-top: 10px;" height="20px"
                                            runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                                <tr>
                                                    <td id="td_Add" height="20px" style="padding-left: 10px" runat="server" class="Text">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td_City" height="20px" style="padding-left: 10px" runat="server" class="Text">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td_Mobile" height="20px" style="padding-left: 10px" runat="server" class="Text">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="td_Email" height="20px" style="padding-left: 10px; padding-bottom: 10px;"
                                                        runat="server" class="Text">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #004b91; padding-top: 10px; padding-bottom: 10px;">
                                <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                    <tr>
                                        <td class="Text" style="padding-left: 10px" width="150">
                                            GDSPNR
                                        </td>
                                        <td class="Text" width="160">
                                            <asp:TextBox ID="txt_GDSPNR"  onkeypress="return checkit(event)" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_GDSPNR"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="Text" width="150">
                                            Booking Date
                                        </td>
                                        <td id="td_BookingDate" class="Text" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" class="Text" style="padding-left: 10px">
                                            Airline PNR
                                        </td>
                                        <td width="160">
                                            <asp:TextBox ID="txt_AirlinePNR"  onkeypress="return checkit(event)" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_AirlinePNR"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="Text">
                                            Status
                                        </td>
                                        <td id="td_Status" runat="server" class="Text">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                    <tr>
                                        <td class="Proxy" style="padding-left: 10px">
                                            Traveller Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Repeater ID="Repeater_Traveller" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 250px; height: 25px" class="TextBig">
                                                                Passenger Name
                                                            </td>
                                                            <td style="width: 120px; height: 25px" class="TextBig">
                                                                Type
                                                            </td>
                                                            <td class="TextBig" style="height: 25px">
                                                                Pax DOB
                                                            </td>
                                                            <td class="TextBig" style="height: 25px">
                                                                Ticket Number
                                                            </td>
                                                            <td class="TextBig" style="height: 25px" align="center">
                                                                Net Fare
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                                        <tr>
                                                            <td id="Td1" style="width: 250px" height="30px" visible="false" runat="server">
                                                                <asp:Label ID="lbl_TransID" runat="server" Text='<%#Eval("PaxId")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 250px" height="30px">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 120px">
                                                                <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 120px">
                                                                <asp:Label ID="lbl_DOB" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_TktNo" runat="server" onkeypress="return checkit(event)"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_TktNo"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Label ID="lbl_Fare" runat="server" Text='<%#Eval("TotalAfterDis")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="40px">
                                                        <asp:Button ID="btn_update" runat="server" Text="Update PNR" CssClass="buttonfltbk" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                    <tr>
                                        <td class="Proxy" height="25px" style="padding-left: 10px">
                                            Flight Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Label ID="lbl_FlightInfo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table width="100%" border="0" cellpadding="0px" cellspacing="0">
                                    <tr>
                                        <td class="Proxy" height="25px" style="padding-left: 10px" id="td_fareinfo" runat="server">
                                           <%-- Fare Information--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; padding-right: 10px;">
                                            <asp:Label ID="lbl_FareInfo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <%-- <td style="width:10px">
      </td>--%>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
