<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceDetails.aspx.cs" Inherits="GroupSearch_InvoiceDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
   <%-- <title></title>--%>
    <script type="text/javascript" lang='javascript'>
        function callprint(strid) {
            var prtContent = $('#' + strid);
            //var sst = '<html><head><title>Ticket Details</title><link rel="stylesheet" href="http://www.RWT.co/CSS/itz.css" type="text/css" media="print"></style></head><body>';
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
           // WinPrint.document.write('<html><head><title>Booing Details</title>');
            WinPrint.document.write('</head><body>' + prtContent.html() + '</body></html>');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" runat="server" style="margin: 5px auto; border: 1px #20313f solid; width: 90%; background-color: #FFFFFF; padding: 5px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 10%">
                                    <a href='javascript:;' onclick='javascript:callprint("divprint");'>
                                        <img src='../Images/print_booking.jpg' border='0' alt="" /></a></td>
                </tr>
            </table>
        </div>
        <div id="divprint" runat="server" style="margin: 5px auto; border: 1px #20313f solid; width: 90%; background-color: #FFFFFF; padding: 5px;">
                    <div id="div_mail" runat ="server">
                        <asp:Label ID="invoicedetails" runat="server"></asp:Label>
                    </div>
                </div>
    </form>
</body>
</html>
