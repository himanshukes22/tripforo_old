<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreditNoteRail.aspx.vb" Inherits="SprReports_Accounts_CreditNoteRail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
    <div id='divprint'>
        <div id="div_invoice" runat="server">
            <asp:Label ID="lbl_Detail" runat="server"></asp:Label>
            <asp:Label ID="lbl_PNR" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
                <td align="center" height="30px">
                    <a href="#" onclick='javascript:callprint("divprint");'>
                        <img src='../../Images/print_booking.jpg' border='0' /></a>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_PDF" runat="server" Text="Convert To PDF" CssClass="buttonfltbk" CausesValidation="False"
                        Visible="False" OnClick="btn_PDF_Click" />
                    <asp:Button ID="btn_Word" runat="server" Text="Export To Word" CssClass="buttonfltbk" CausesValidation="False"
                        OnClick="btn_Word_Click" />
                    <asp:Button ID="btn_Excel" runat="server" Text="Export To Excel" CssClass="buttonfltbk" CausesValidation="False"
                        OnClick="btn_Excel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 10px auto auto 100px; border: 1px #ffffff solid; width: 75%;
        background-color: #FFFFFF; text-align: center">
        <table width="50%" border="0" cellspacing="2" cellpadding="2" bgcolor="#20313f" align="center">
            <tr>
                <td colspan="2" style="color: #FFFF66; font-size: 12px" height="40px">
                    <strong style="font-family: arial, Helvetica, sans-serif; font-weight: bold; font-size: 14px;">
                        Send E-Mail:</strong>
                </td>
            </tr>
            <tr>
                <td width="70%" style="color: #ffffff; font-size: 12px; font-weight: bold; font-family: arial, Helvetica, sans-serif;"
                    align="right">
                    Email-ID :
                    <asp:TextBox ID="txt_email" runat="server" CssClass="textboxflight"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                        ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <br />
                </td>
                <td width="30%" valign="top" align="left">
                    <asp:Button ID="btn" runat="server" Text="Send" CssClass="buttonfltbk" OnClick="btn_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;"
                    align="right">
                    <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                        ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="color: #ffffff; font-size: 12px" height="20px">
                    <asp:Label ID="mailmsg" runat="server" Font-Names="Arial"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '','left=0,top=0,width=950,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><title></title><body onload='window.print()'>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    </script>