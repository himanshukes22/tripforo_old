<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="ErrorLogMsg.aspx.cs" Inherits="SprReports_ErrorLogMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>
    <table style="width: 100%">
        <tr>
            <td>Date :<asp:TextBox ID="txt_date" runat="server" CssClass="date" Width="150px" AutoPostBack="true" OnTextChanged="txt_date_TextChanged"></asp:TextBox></td>
            <td>Order ID:<asp:TextBox ID="txt_OrderID" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btn_submit" runat="server" Text="Submit" Font-Bold="True" Width="150px" OnClick="btn_submit_Click" /></td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Repeater ID="Repeater1" OnItemCommand="ItemCommand" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="ButtonEvent" CommandArgument="<%# Container.DataItem.ToString().Split('_')[1] %>" Text="<%#Container.DataItem.ToString().Split('_')[0] %>" runat="server"></asp:LinkButton>
                        || 
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Repeater ID="Repeater2" OnItemCommand="ItemCommand" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="ChildEvent" CommandArgument="<%# Container.DataItem.ToString().Split('_')[1] %>" Text="<%#Container.DataItem.ToString().Split('_')[0] %>" runat="server"></asp:LinkButton>
                        || 
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Literal ID="ltr_textread" runat="server"></asp:Literal>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
