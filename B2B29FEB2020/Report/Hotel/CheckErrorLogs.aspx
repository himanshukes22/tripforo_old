<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="CheckErrorLogs.aspx.cs" Inherits="SprReports_Hotel_CheckErrorLogs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>Directory Files</td>
            <td colspan="2">File Name       (D:\AirAsia_Req_Res\03-January-2019      D:\AirArabia_Req_Res\03-January-2019    C:\ITZError_Folder_\04-Jan-2019)</td>
         
        </tr>
         <tr>
            <td><asp:TextBox ID="txtExtention" runat="server"  Height="20px" Width="400px"></asp:TextBox></td>
            <td> <asp:TextBox ID="txtpath" runat="server"  Height="20px" Width="400px"></asp:TextBox></td>
            <td><asp:Button ID="btnAllfile" runat="server" Text="Directory Name" Height="40px" Width="110px" aling="left" OnClick="btnAllfile_Click"/><asp:Button ID="btnShowtext" runat="server" Text="Show file Data" Height="40px" Width="110px" aling="right" OnClick="btnShowtext_Click"/></td>
        </tr>
         <tr>
            <td colspan="3"><asp:TextBox ID="txtDirectoryfiles"  textMode="MultiLine" runat="server"  Height="110px" Width="1100px"></asp:TextBox></td>
        </tr>
         <tr>
            <td colspan="3"><asp:TextBox ID="txtFileData"  textMode="MultiLine" runat="server"  Height="470px" Width="1360px"></asp:TextBox></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
