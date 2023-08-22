<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="CashBack.aspx.vb" Inherits="SprReports_Distr_CashBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
        <style type="text/css">
        input[type="text"], input[type="password"], select, radio, legend, fieldset
        {
            border: 1px solid #004b91;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>
    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <table cellspacing="10" cellpadding="0" border="0" class="tbltbl" width="500px">
        <tr>
            <td  colspan="3" style="font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                            color: #004b91;">
                DISTRIBUTOR CASH BACK
            </td>
        </tr>
        <tr>
            <td style="font-weight:bold">
                Distributor Id:
            </td>
            <td>
                <asp:DropDownList ID="ddl_DistrID" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td>
                            <asp:Button ID="btn_serach" runat="server" Text="Search" CssClass="button" />
            </td>
        </tr>
        
        <tr id="tr_details" runat="server" visible="false">
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td style="font-family: arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;
                            color: #004b91;">
                            CASH BACK AIR :
                        </td>
                        <td>
                            <asp:Label ID="lbl_CBAir" runat="server"></asp:Label>
                        </td>
                        <td style="font-family: arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;
                            color: #004b91;">
                            CASH BACK RAIL :
                        </td>
                        <td>
                            <asp:Label ID="lbl_CBRail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr_rm" runat="server">
                        <td style="font-family: arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;
                            color: #004b91;">
                            REMARK :
                        </td>
                        <td colspan="3">
                            <asp:TextBox TextMode="MultiLine" ID="txt_rm" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <asp:Button ID="btn_add" runat="server" Text="Cash Back" CssClass="button" />&nbsp; <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="button" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
