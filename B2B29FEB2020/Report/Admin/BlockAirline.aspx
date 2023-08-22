<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="BlockAirline.aspx.vb" Inherits="SprReports_Admin_BlockAirline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>


            <td>Origin</td>
            <td>
                <asp:TextBox ID="txt_org" runat="server"></asp:TextBox>
            </td>
            <td>Destination</td>
            <td>
                <asp:TextBox ID="txt_dest" runat="server"></asp:TextBox>
            </td>


        </tr>
        <tr>
            <td>FlightNo</td>
            <td>
                <asp:TextBox ID="txt_flight" runat="server"></asp:TextBox>
            </td>
            <td>Airline
            </td>
            <td>
                <asp:TextBox ID="txt_airline" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Trip
            </td>
            <td>
                <asp:DropDownList ID="ddl_trip" runat="server">
                    <asp:ListItem Value="D">Domestic</asp:ListItem>
                    <asp:ListItem Value="I">International</asp:ListItem>

                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" CssClass="buttonfltbk" Text="Search" />
            </td>
            <td colspan="2">
                <asp:Label ID="lblstatus" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

