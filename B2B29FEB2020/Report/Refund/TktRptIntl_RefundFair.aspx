﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TktRptIntl_RefundFair.aspx.vb" Inherits="TktRptIntl_RefundFair" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large; font-weight: bold;
                    font-style: italic; background-color: #009F00" class="style1">
                    Fare Details :
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="1">
                        <tr>
                            <td colspan="2" style="font-family: Arial, Helvetica, sans-serif; font-size: medium;
                                font-weight: bold">
                                Base Fare:&nbsp;<asp:Label ID="lblbasefare" runat="server"></asp:Label></td>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                                Tax :&nbsp;<asp:Label ID="lbltax" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-family: Arial, Helvetica, sans-serif; font-size: medium;
                                font-weight: bold">
                                YQ:&nbsp;<asp:Label ID="lblyq" runat="server"></asp:Label></td>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                                ServiceTax:&nbsp;<asp:Label ID="lblservicetax" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-family: Arial, Helvetica, sans-serif; font-size: medium;
                                font-weight: bold">
                                TransactionFee:&nbsp;<asp:Label ID="lbltranfee" runat="server"></asp:Label></td>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                                Discount:&nbsp;<asp:Label ID="lbldiscount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-family: Arial, Helvetica, sans-serif; font-size: medium;
                                font-weight: bold">
                                CB:&nbsp;<asp:Label ID="lblcb" runat="server"></asp:Label></td>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                                TDS:&nbsp;<asp:Label ID="lbltds" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                                TotalFare:&nbsp;<asp:Label ID="lbltotalfare" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTktNo" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
