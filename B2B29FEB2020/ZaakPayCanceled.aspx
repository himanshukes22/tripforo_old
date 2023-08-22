<%@ Page Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="ZaakPayCanceled.aspx.cs" Inherits="ZaakPayCanceled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%@ Import Namespace="System.IO" %>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Zaakpay</title>
    <link href="../stylesheets/styles-payflow.css" rel="stylesheet" type="text/css" />


    <link href="../javascripts/txnpage/sort.css" rel="stylesheet" type="text/css" />
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascripts/txnpage/sort_files/jquery.js"></script>
    <script type="text/javascript" src="../javascripts/txnpage/sort_files/interface.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <!--this is use for remove captchasecton color or image-->
    <!--popup_layer-->


    <% 
        String secretKey = "504ee4204b94486885a9701d2cb5a1e7";
        String allParamValue = null;
        Boolean isChecksumValid = false;
        allParamValue = ChecksumResponse.ChecksumCalculatorResponse.getAllNotEmptyParamValue(HttpContext.Current.Request).Trim();
        String checksum = ChecksumResponse.ChecksumCalculatorResponse.calculateChecksum(secretKey, allParamValue);
        System.Diagnostics.Debug.WriteLine("allParamValue Response : " + allParamValue);
        System.Diagnostics.Debug.WriteLine("secretKey Response : " + secretKey);
        if (checksum != null)
        {
            isChecksumValid = ChecksumResponse.ChecksumCalculatorResponse.verifyChecksum(secretKey, allParamValue, checksum);
        }

        string[] strparam = allParamValue.Split('&');

        ZaakPayAPI.ResponseData resdata = new ZaakPayAPI.ResponseData
        {
            Amount = Convert.ToDouble(strparam[0].Substring(strparam[0].IndexOf('=') + 1)),
            cardhashId = strparam[1].Substring(strparam[1].IndexOf('=') + 1),
            doRedirect = strparam[2].Substring(strparam[2].IndexOf('=') + 1),
            orderId = strparam[3].Substring(strparam[3].IndexOf('=') + 1),
            paymentmethod = strparam[4].Substring(strparam[4].IndexOf('=') + 1),
            paymentMode = strparam[5].Substring(strparam[5].IndexOf('=') + 1),
            responseCode = Convert.ToInt16(strparam[6].Substring(strparam[6].IndexOf('=') + 1)),
            responseDescription = strparam[7].Substring(strparam[7].IndexOf('=') + 1),
            checksum = Convert.ToString((isChecksumValid) ? "Yes" : "No <br/>The Transaction might have been Successfull.")
        };

        ChecksumResponse.ChecksumCalculatorResponse.insertResponseData(resdata);//ChecksumCalculatorResponse.insertResponseData(resdata);
    %>
    <center>

        <table>

            <tr>
                <td align="center">OrderId</td>
                <td align="center"><%= HttpContext.Current.Request.Params.Get("orderId")%> </td>
            </tr>
            <tr>
                <td align="center">Response Code</td>
                <%if (isChecksumValid)
                  { %>
                <td align="center"><%=HttpContext.Current.Request.Params.Get("responseCode")%></td>
                <%}
                  else
                  { %>
                <td align="center"><font color="red">***</font></td>
                <%} %>
            </tr>
            <tr>
                <td align="center">Response Description</td>
                <%if (isChecksumValid)
                  { %>
                <td align="center"><%=HttpContext.Current.Request.Params.Get("responseDescription")%></td>
                <%}
                  else
                  { %>
                <td align="center"><font color="red">The Response is Compromised.</font></td>
                <%} %>
            </tr>

            <tr>
                <td align="center">Checksum Valid</td>
                <%if (isChecksumValid)
                  { %>
                <td align="center">Yes</td>
                <%}
                  else
                  { %>
                <td align="center"><font color="red">No </font>
                    <br />
                    <br />
                    The Transaction might have been Successfull.</td>

                <%} %>
            </tr>


        </table>

    </center>

 <link rel="stylesheet" href="../css/main2.css" type="text/css" />

    <script type="text/javascript" src="../js/chrome.js"></script>

    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Booking Details</title><style type='text/css'>	 .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>
    <table cellpadding="0" cellspacing="0" border="0" align="center" style="background: #fff;
        width: 100%; padding: 20px;">
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
            <td style="padding: 10px; background: #fff; height: 300Px; text-align: center; font-size: 14px">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
    </table>


</asp:Content>
