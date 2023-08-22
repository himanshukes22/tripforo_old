<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketSummary.aspx.cs" Inherits="BS_TiketSummary"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="CSS/CommonCss.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Bus Ticket Details</title></head><body>" + divticketcopy.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main1" id="divticketcopy" style="font-family: arial, Helvetica, sans-serif;
        font-size: 12px; color: #000000" runat="server">
    </div>
    <div id="divexport">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:Arial; font-size:13px;" >
            <tr>
                <td style="width: 10%">
                    <p>
                    </p>
                </td>
                <td style="width: 80%" bgcolor="White">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="padding-left: 10px; font-family: arial, Helvetica, sans-serif;
                                font-size: 12px; color: #000000">
                                <a href='javascript:;' onclick='javascript:callprint("divticketcopy");'>
                                    <img src='../Images/print_booking.jpg' border='0'></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_export" runat="server" Text="ExportToWord" OnClick="btn_export_Click" 
                                      CssClass="buttonfltbk"  OnClientClick="return DivInner_exp();" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%">
                </td>
            </tr>
        </table>
    </div>
    <div style=" 
        padding: 10px;" id="divmail">
        <table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#eee" style="height: 80px; font-family:Arial; "
            align="center">
            <tr>
                <td colspan="2" style="color: #424242; font-size: 12px;">
                    <strong style="padding-left: 10px">Send E-Mail:</strong>
                </td>
            </tr>
            <tr>
                <td width="40%" style="color: #424242; font-size: 12px; padding-left: 15px;">
                    Email-ID :
                    <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                        ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>--%>
                    <br />
                    <div style="text-align: left; color: #EC2F2F">
                       <%-- <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                            ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>--%>
                    </div>
                </td>
                <td width="60%" valign="middle" style="padding-left:100px">
                    <asp:Button ID="btn" runat="server" Text="Send" CssClass="btn-warning" OnClick="btn_Click" OnClientClick="return DivInner();">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="color: #424242; font-size: 12px; padding-left: 15px;">
                    <asp:Label ID="mailmsg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <input type="hidden" id="Hidden1" name="Hidden1" />
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    var UrlBase = '<%=ResolveUrl("~/") %>';
</script>

<script src="JS/jquery-1.4.4.min.js" type="text/javascript"></script>

<script src="JS/Ticketcopy.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    function DivInner() {
         
        if ($("#txt_email").val() == "" || $("#txt_email").val() == " ") {
            alert("Please Provide Valid Email Id.")
            return false;
        }

        var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        var emailid = document.getElementById("txt_email").value;
        var matchArray = emailid.match(emailPat);
        if (matchArray == null) {
            alert("Your email address seems incorrect. Please try again.");
            document.getElementById("txt_email").focus();
            return false;
        }
        $("#Hidden1").val($("#divticketcopy")[0].innerHTML);
    }
    function DivInner_exp() {
        $("#Hidden1").val($("#divticketcopy")[0].innerHTML);
    }
</script>

