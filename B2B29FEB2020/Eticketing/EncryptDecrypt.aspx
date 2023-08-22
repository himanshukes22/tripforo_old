<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncryptDecrypt.aspx.cs" Inherits="Eticketing_EncryptDecrypt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div><h1>AES 256 Encrytion/Decrytion algo (in CBC mode) </h1></div>
        <br />
        <table>
            <tr>
                <td>Enter Text</td>
                 <td>
                     <asp:TextBox ID="TxtString" runat="server" TextMode="MultiLine" style="width: 1000px; height: 100px;"></asp:TextBox></td>
            </tr>
             <tr>
                <td></td>
                 <td>
                     <asp:Button ID="BtnEncrypt" runat="server" Text="Encrypt" OnClick="BtnEncrypt_Click" /> &nbsp; &nbsp; <asp:Button ID="BtnDecrypt" runat="server" Text="Decrypt" OnClick="BtnDecrypt_Click" />  &nbsp; &nbsp;<asp:Button ID="BtnClear" runat="server" Text="Reset" OnClick="BtnClear_Click" /> </td>
            </tr>
        </table>
    <br />
        <div style="width:50%;">
            <table style="width:100%;">
                <tr><td>
                    <u>Response:</u>                    
                    </td>                    
                </tr>
                <tr><td><asp:Label ID="LblMessage" runat="server"></asp:Label></td></tr>
            </table>
            </div>

    </div>
    </form>
</body>
</html>
