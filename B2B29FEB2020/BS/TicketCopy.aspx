﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true"
    CodeFile="TicketCopy.aspx.cs" Inherits="BS_TicketCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/CommonCss.css")%>" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Bus Ticket Details</title></head><body>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    </script>

    <div class="main1" id="divticketcopy" style="margin-left:15px">
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 80%" bgcolor="White" id="divmail">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="padding-left: 10px">
                                <a href='javascript:;' onclick='javascript:callprint("divticketcopy");'>
                                    <img src='../Images/print_booking.jpg' border='0'></a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%">
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script src="<%=ResolveUrl("~/BS/JS/jquery-1.9.1.js")%>" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/BS/JS/jquery-1.4.4.min.js")%>" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/BS/JS/Ticketcopy.js")%>" type="text/javascript"></script>

</asp:Content>
