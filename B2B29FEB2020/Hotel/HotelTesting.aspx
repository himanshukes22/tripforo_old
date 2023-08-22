<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HotelTesting.aspx.vb" Inherits="Hotel_HotelTesting" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <table>
            <tr><td style=" font-weight:bold;">URL:<asp:TextBox ID="TextBox3" runat="server"  Height="22px" Width="551px"></asp:TextBox>Action:<asp:TextBox ID="TextBox4" runat="server"  Height="22px" Width="551px"></asp:TextBox></td></tr>
            
            <tr><td><asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="110px" Width="1343px"></asp:TextBox></td></tr>
            <tr><td style=" font-weight:bold;">Response XML&nbsp;&nbsp;&nbsp; <asp:Button ID="Button1" runat="server" Text="Post text/xml Search Hotel" Height="40px" Width="155px"/>
                &nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="JSON GET Search Hotel" Height="40px" Width="155px"/>
                &nbsp;&nbsp;&nbsp; <asp:Button ID="Button3" runat="server" Text="Post application/xml Search Hotel" Height="40px" Width="155px"/>
                  &nbsp;&nbsp;&nbsp; <asp:Button ID="Button4" runat="server" Text="Post text/xml Search" Height="40px" Width="155px"/>
                 &nbsp;&nbsp;&nbsp; <asp:Button ID="Button5" runat="server" Text="Post JSON Search" Height="40px" Width="155px"/>
                </td></tr>

            <tr><td><asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="434px" Width="1343px"></asp:TextBox></td></tr>

        </table>       
        </div>
    
    </form>
    <asp:Literal ID="XmlText" runat="server"></asp:Literal>
    <asp:Xml ID="Xml1" runat="server"  >
     
    </asp:Xml>
</body>
</html>
