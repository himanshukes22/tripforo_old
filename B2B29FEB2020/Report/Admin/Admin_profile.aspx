<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Admin_profile.aspx.vb" Inherits="Reports_Admin_Admin_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../js/chrome.js"></script>

    <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl">
        <tr>
            <td bgcolor="#20313f" height="25px" colspan="4">
                <h2>Admin Details</h2>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodytext">User Id :
            </td>
            <td align="left">
                <asp:TextBox ID="uid" runat="server" Enabled="false" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Password :
            </td>
            <td align="left">
                <asp:TextBox ID="pwd" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right" class="bodytext">Credit Limit :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_crd" runat="server" Enabled="false" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">New Credit Amount :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_crd_val" runat="server" Enabled="false" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right" class="bodytext">Agent Name :
            </td>
            <td align="left">
                <asp:TextBox ID="Fname" runat="server" Enabled="false" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Address :
            </td>
            <td align="left">
                <asp:TextBox ID="Address" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodytext">City :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_city" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Post Code :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_pcode" runat="server" MaxLength="9" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodytext">Country :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_country" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Mail ID :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_email" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodytext">Phone No. :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_hphone" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Work Phone No. :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_wphone" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodytext">Mobile No. :
            </td>
            <td align="left">
                <asp:TextBox ID="txt_mobile" runat="server" CssClass="lgntextbox1"></asp:TextBox>
            </td>
            <td align="right" class="bodytext">Grade :
            </td>
            <td align="left">
                <asp:TextBox ID="grade" runat="server" MaxLength="1" Enabled="false" CssClass="lgntextbox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td colspan="2" align="left"></td>
            <td colspan="1" align="right">
                <asp:ImageButton ID="Book" runat="server" ImageUrl="../../images/update.gif" />
            </td>
        </tr>
    </table>

</asp:Content>
