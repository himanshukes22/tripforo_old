<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/bootstrap-theme.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap-theme.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <br /><br />
        <hr /><br /><br />
       
        <table style="align-content:center;">
            <tr>
                <td style="width:25%;"></td>
                <td style="width:50%;">
       
                     <div id="div1" runat="server">
            <asp:TextBox ID="txtUserId" runat="server" TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
           
            <asp:Button ID="btnLogin" runat="server" Text="check" OnClick="btnLogin_Click" />
        </div>

        <div id="div2" runat="server">       
        <div>
        <asp:DropDownList ID="DropDownTrip" runat="server">  
            <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>          
            <asp:ListItem Value="D" Text="Dom"></asp:ListItem>
            <asp:ListItem Value="I" Text="Int"></asp:ListItem>
        </asp:DropDownList> 
            &nbsp;
         <asp:DropDownList ID="DropDownAirline" runat="server" AppendDataBoundItems="true">
                                    </asp:DropDownList> &nbsp;
        <asp:DropDownList ID="DropDownStatus" runat="server">
            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
            <asp:ListItem Value="true" Text="ACTIVE"></asp:ListItem>
            <asp:ListItem Value="false" Text="DEACTIVE"></asp:ListItem>
        </asp:DropDownList> &nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" Text="Action" OnClick="Button1_Click" />
    </div>
        <br />
        <div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
         </div>

       </div>
                    </td>
                <td style="width:25%;"></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblTrip" runat="server"></asp:Label> &nbsp;:&nbsp; <asp:Label ID="lblProvider" runat="server"></asp:Label>&nbsp;: &nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
         <hr />
    </form>
</body>
</html>
