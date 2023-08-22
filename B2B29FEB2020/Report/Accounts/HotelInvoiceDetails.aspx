<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HotelInvoiceDetails.aspx.vb"
    Inherits="SprReports_Accounts_HotelInvoiceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function printSelection(node) {

            var content = node.innerHTML
            var pwin = window.open('', 'print_content', 'width=740,height=444');

            pwin.document.open();
            pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
            pwin.document.close();

            setTimeout(function () { pwin.close(); }, 1000);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id='divprint'>
            <div id="div_invoice" runat="server" style="width: 650px; margin: 0 auto; background: #ffffff;">
                <table border="0" cellpadding="0" cellspacing="0" style="font-family: Arial; border: 1px solid #ccc; font-family: 13px; padding: 4px; margin: 0 auto;">
                    <tr>
                        <td colspan="3" style="font-size: 16px; padding: 10px; text-align: center; font-weight: bold;">HOTEL INVOICE
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 227px; height: 110px; border: 2px solid #ccc; padding: 4px;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-size: 12px; font-weight: bold;" id="td_CNAME" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_CADDRESS" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_CITYZIP" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_PHONE" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_EMAIL" runat="server"></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 200px; height: 110px; border: 2px solid #ccc; padding: 4px;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-size: 12px; font-weight: bold;">Invoice No:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblinvoiceno" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 12px; font-weight: bold;">Invoice Date:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 12px; font-weight: bold;">Party Code:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPartyCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 12px; font-weight: bold;">BookingId:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBookingID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20px" colspan="2"></td>
                                </tr>

                            </table>
                        </td>
                        <td style="width: 200px; height: 110px; border: 2px solid #ccc; padding: 4px;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-weight: bold; font-size: 12px;">Agency Details</td>
                                </tr>
                                <tr>
                                    <td id="td_AgName" style="font-size: 12px; font-weight: bold;" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_Address" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_Add1" runat="server"></td>
                                </tr>
                                <tr>
                                    <td id="td_country" runat="server"></td>
                                </tr>

                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_IntInvoice" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ul>
                                            <li>E & O.E: Payment to be made to the cashier and print Official Receipt must to be Obtained. </li>
                                            <li>CHEQUE: All Cheques/Demand Drafts in Payment of bills must be crossed
                                                <br />
                                                "A/C Payee Only & all drawn in favour of "".</li>
                                            <li>This is Computer generated invoice,hence no signature required</li>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div>
                </div>
            </div>
        </div>
        <div style="width: 650px; margin: 0 auto; background: #ffffff;">
            <table style="margin: auto; width: 100%;" align="left">
                <tr>
                    <td align="left">
                        <table border="0" cellspacing="2" cellpadding="2" style="margin-top: 20px;">
                            <tr>
                                <td>
                                    <strong>Send E-Mail:</strong>
                                    <asp:TextBox ID="txt_email" runat="server" CssClass="textboxflight"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                                        ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btn" runat="server" Text="Send" CssClass="buttonfltbk"></asp:Button>&nbsp;&nbsp;<asp:Label ID="mailmsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="text-align: center; color: #EC2F2F">
                                        <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                                            ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" height="30px">
                                    <a href="" onclick="printSelection(document.getElementById('divprint'));return false">
                                        <img src='../../Images/print_booking.jpg' border='0' /></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btn_PDF" runat="server" Text="Convert To PDF" CssClass="buttonfltbk" CausesValidation="False"
                                        Visible="False" />
                                    <asp:Button ID="btn_Word" runat="server" Text="Export To Word" CausesValidation="False"
                                        CssClass="buttonfltbk" />
                                    <asp:Button ID="btn_Excel" runat="server" Text="Export To Excel" CausesValidation="False"
                                        CssClass="buttonfltbk" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
