<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="BookingConfimation.aspx.vb" Inherits="FlightDom_BookingConfimation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="css/transtour.css" rel="stylesheet" type="text/css" />
    <link href="css/core_style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/itz.css" rel="stylesheet" />
    <link href="../CSS/newcss/main.css" rel="stylesheet" />
    <link href="../CSS/foundation.css" rel="stylesheet" />

    <script src='../Hotel/JS/jquery-1.3.2.min.js' type='text/javascript'></script>
    <script src='../Hotel/JS/jquery-barcode.js' type='text/javascript'></script>







  <%-- <link rel="stylesheet" href="../css/main2.css" type="text/css" />

    <script type="text/javascript" src="../js/chrome.js"></script>--%>
<%--
    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Booking Details</title><style type='text/css'>	 .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
           // prtContent.innerHTML = strOldOne;
        }
    </script>

    <table cellpadding="0" cellspacing="0" border="0" align="center" style="background: #fff;
        width: 950px; padding: 20px;">
        <tr>
            <td width="10" height="10" valign="top">
                <img src="../images/box-tpr.jpg" width="10" height="10" />
            </td>
            <td style="background: url(../images/box-tp.jpg) repeat-x left bottom;" height="10">
            </td>
            <td valign="top">
                <img src="../images/box-tpl.jpg" width="10" height="10" />
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px; background: url(../images/boxl.jpg) repeat-y left bottom;">
            </td>
            <td style="padding: 10px; background: #fff;">
                <div style='width: 650px; font-size: 12px; font-family: Verdana, Arial, Helvetica, sans-serif;
                    margin: 10px auto 10px auto; text-align: right'>
                    <a href='javascript:;' class='lnk' onclick='javascript:callprint("divprint");'>Click
                        Here To Print</a></div>
                <div id='divprint'>
                    <asp:Label ID="lblTkt" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 10px; height: 10px; background: url(../images/boxr.jpg) repeat-y left bottom;">
            </td>
        </tr>
        <tr>
            <td width="10" height="10" valign="top">
                <img src="../images/box-bl.jpg" width="10" height="10" />
            </td>
            <td style="background: url(../images/box-bottom.jpg) repeat-x left bottom;" height="10">
            </td>
            <td valign="top">
                <img src="../images/box-br.jpg" width="10" height="10" />
            </td>
        </tr>
    </table>--%>
      <asp:HiddenField ID="basetaxfarenrm" runat="server" Value="0" />
</asp:Content>
