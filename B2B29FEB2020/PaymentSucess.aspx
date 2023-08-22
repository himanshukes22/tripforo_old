<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentSucess.aspx.cs" Inherits="PaymentSucess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>Please Wait...</title>
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
    <script type = "text/javascript" >
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
</script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="wait" style="text-align: center; margin-top: 50px; font-size: 16px;">
            <img src="Images/logo.png" /><br />           
             transaction is under process,please wait...
             <br />
            <br />
            <br />
            <img src='<%=ResolveUrl("~/images/wait.gif")%>' alt="" /><br />
            <br />
            <br />
            Do not "close the window" or press "refresh" or "browser back button".
        </div>
    </form>
</body>
</html>
