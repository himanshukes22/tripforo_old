<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HtlCancelation.aspx.vb" Inherits="HtlCancelation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../Hotel/css/HotelStyleSheet.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0" align="center" style="margin: 0 auto;">
        <tr>
            <td style="background: url(../../Hotel/images/a.png) no-repeat; height: 11px; width: 14px;">
            </td>
            <td style="background: url(../../Hotel/images/a1.png) repeat; height: 11px;">
            </td>
            <td style="background: url(../../Hotel/images/b.png) no-repeat; height: 11px; width: 14px;">
            </td>
        </tr>
        <tr>
            <td style="background: url(../../Hotel/images/m1.png); width: 14px;">
            </td>
            <td style="background: #fff;">
                <table style='width: 100%;' cellspacing="10" id="tblCancale">
                    <tr>
                        <td colspan="3" align="center" style="background: #eee; height: 25px; line-height: 25px;
                            font-weight: bold;">
                            Cancle your Hotel Booking
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter Booking ID :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancle" runat="server" CssClass="psb_dd"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnCancle" runat="server" CssClass="button" Text="Cancle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cancletion Confirm No :
                        </td>
                        <td>
                            <asp:Label ID="lblConfi" runat="server" Text="" CssClass="chzn-select"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background: url(../../Hotel/images/m2.png); width: 14px;">
            </td>
        </tr>
        <tr>
            <td style="background: url(../../Hotel/images/c.png) no-repeat; height: 11px; width: 14px;">
            </td>
            <td style="background: url(../../Hotel/images/c1.png) repeat; height: 11px;">
            </td>
            <td style="background: url(../../Hotel/images/d.png) no-repeat; height: 11px; width: 14px;">
            </td>
        </tr>
    </table>
</asp:Content>