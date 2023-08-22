<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testapi.aspx.cs" Inherits="Testapi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblmsg"></asp:Label>
    <asp:TextBox  runat="server" TextMode="MultiLine" ID="textreq"></asp:TextBox>

      
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />

      
    </div>
    </form>
</body>
</html>
