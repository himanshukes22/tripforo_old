<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankRequest.aspx.cs" Inherits="Eticketing_BankRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            $("#nonseamless").submit();
        });
    </script>
    <title>Please Wait...</title>
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
    <form id="nonseamless" method="post" name="redirect" action="<%=strCCAveUrl%>">
        <input type="hidden" id="encRequest" name="encdata" value="<%=strEncRequest%>" /> 
        <div class="wait" style="text-align: center; margin-top: 50px; font-size: 16px;">
            <img src="Images/logo.png" /><br />
            Please be patient while we are redirecting you to payment gateway site.<br />
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
