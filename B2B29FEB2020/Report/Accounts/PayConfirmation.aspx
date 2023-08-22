<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="PayConfirmation.aspx.cs" Inherits="Report_Accounts_PayConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
table {
  border-collapse: collapse;
  width: 100%;
}

th, td {
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {background-color: #f2f2f2;}
</style>

    <div>
        <table>            
            <tr><td><b><h2>Payment Details</h2></b></td></tr>
            <tr><td><table><tr><td><b>Order Number:</b></td><td>
                <asp:Label ID="lblOrderNumber" runat="server"></asp:Label></td></tr>
            <tr><td><b>Amount:</b></td><td><asp:Label ID="lblAmount" runat="server"></asp:Label></td></tr>
            <tr><td><b>Status:</b></td><td><b><asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></b></td></tr></table></td></tr>
            
        </table>

    </div>

</asp:Content>

