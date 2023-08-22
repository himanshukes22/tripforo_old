<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IntInvoiceDetails.aspx.vb" Inherits="IntInvoiceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<meta name="robots" content="noindex,nofollow" />
<meta name="viewport" content="width=device-width; initial-scale=1.0;" />
    <%-- <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700" />

    <style type="text/css">
        @import url(https://fonts.googleapis.com/css?family=Open+Sans:400,700);

        @media print {
            body {
                font-family: 'Open Sans', sans-serif !important;
                text-transform: uppercase !important;
            }
        }

        td, th {
            /*padding: 4px !important;*/
            text-align: inherit !important;
        }

        body {
            margin: 0;
            padding: 0;
            background: #e1e1e1;
            text-transform: uppercase;
            font-family: 'Open Sans', sans-serif;
        }

        div, p, a, li, td {
            -webkit-text-size-adjust: none;
        }

        .ReadMsgBody {
            width: 100%;
            background-color: #ffffff;
        }

        .ExternalClass {
            width: 100%;
            background-color: #ffffff;
        }

        body {
            width: 100%;
            height: 100%;
            background-color: #e1e1e1;
            margin: 0;
            padding: 0;
            -webkit-font-smoothing: antialiased;
        }

        html {
            width: 100%;
        }

        p {
            padding: 0 !important;
            margin-top: 0 !important;
            margin-right: 0 !important;
            margin-bottom: 0 !important;
            margin-left: 0 !important;
        }

        .visibleMobile {
            display: none;
        }

        .hiddenMobile {
            display: block;
        }

        @media only screen and (max-width: 600px) {
            body {
                width: auto !important;
            }

            table[class=fullTable] {
                width: 96% !important;
                clear: both;
            }

            table[class=fullPadding] {
                width: 85% !important;
                clear: both;
            }

            table[class=col] {
                width: 45% !important;
            }

            .erase {
                display: none;
            }
        }

        @media only screen and (max-width: 420px) {
            table[class=fullTable] {
                width: 100% !important;
                clear: both;
            }

            table[class=fullPadding] {
                width: 85% !important;
                clear: both;
            }

            table[class=col] {
                width: 100% !important;
                clear: both;
            }

                table[class=col] td {
                    text-align: left !important;
                }

            .erase {
                display: none;
                font-size: 0;
                max-height: 0;
                line-height: 0;
                padding: 0;
            }

            .visibleMobile {
                display: block !important;
            }

            .hiddenMobile {
                display: none !important;
            }
        }

        .btn-kun {
            background: #ff6262;
    border: #ff6262;
    padding: 10px;
    border-radius: 5px;
    text-align: center;
    color: #fff;
        }

        .btn-kun:hover {
            background: #444444;
    border: #ff6262;
    padding: 10px;
    border-radius: 5px;
    text-align: center;
    color: #fff;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Ticket Details</title></head><body>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
        $(document).ready(function () {

            $("#txt_email").focus(function () {

                $("#mailmsg").hide();

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id='divprint'>
            <div id="div_invoice" runat="server">
                <asp:Label ID="lbl_IntInvoice" runat="server"></asp:Label>
            </div>
        </div>
        


        <table width="1000" border="0" cellpadding="0" cellspacing="0" align="center" class="fullTable" bgcolor="#ffffff" style="border-radius: 10px 10px 10px 10px;">
            <tbody>

                <tr class="spacer">
                    <td height="50">

                    </td>

                </tr>
                <tr>
                    <td>
                        <table width="800" border="0" cellpadding="0" cellspacing="0" align="center" class="fullPadding">
                            <tbody>
                                <tr>
                                     <td align="center" height="30px">
                        <a href='javascript:;' onclick='javascript:callprint("divprint");' class="btn-kun">Print</a>
                    </td>

                                    <td>
                                        <asp:Button CssClass="btn-kun" ID="btn_PDF" runat="server" Text="Convert To PDF"
                        CausesValidation="False" Visible="False" />
                                    </td>

                                    <td>
                                        <asp:Button ID="btn_Word" CssClass="btn-kun" runat="server" Text="Export To Word"
                            CausesValidation="False" />
                                    </td>
                               
                                <td>
                                    <asp:Button ID="btn_Excel" CssClass="btn-kun" runat="server" Text="Export To Excel"
                            CausesValidation="False" />
                                </td>
                                 </tr>

                               

                            </tbody>

                       </table>

                        <table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#20313f" align="center" style="margin-bottom: -53px;margin-top: 33px;border-radius: 0 0 10px 10px;">
                <tr>
                    <td colspan="2" style="color: #ffffff; font-size: 12px">
                        <strong>Send E-Mail:</strong>
                    </td>
                </tr>
                <tr>
                    <td width="70%" style="color: #ffffff; font-size: 12px">Email-ID :
                    <asp:TextBox ID="txt_email" runat="server" CssClass="textboxflight"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                            ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        <div style="text-align: center; color: #EC2F2F">
                            <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                                ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                        </div>
                    </td>
                    <td width="30%" valign="top">
                        <asp:Button ID="btn" runat="server" Text="Send"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="color: #ffffff; font-size: 12px">
                        <asp:Label ID="mailmsg" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

                    </td>

                </tr>
                <tr class="spacer">
                    <td height="50">

                    </td>

                </tr>

            </tbody>

        </table>


            <table border="0" cellpadding="0" cellspacing="0" width="900px">
                <tr>
                   
                </tr>
                <tr>
                    <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                        
                        
                    </td>
                </tr>
            </table>
        




        <div style="margin: 10px auto auto 100px; border: 1px #ffffff solid; width: 75%; background-color: #FFFFFF; text-align: center">
            
        </div>
    </form>
</body>
</html>
