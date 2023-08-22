<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentGateway.aspx.cs" Inherits="PaymentGateway" %>

<%--<%@ PreviousPageType VirtualPath="~/FlightDom/PriceDetails.aspx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#nonseamless").submit();
        });
    </script>
<%--<script type = "text/javascript" >
    function preventBack() { window.history.forward(); }
    setTimeout("preventBack()", 0);
    window.onunload = function () { null };
</script>--%>
    <title>Please Wait...</title>
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <%--<form id="nonseamless" method="post" name="redirect" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction"> --%>
    <%--<form id="nonseamless" method="post" name="redirect" action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction"> --%>
    <form id="nonseamless" method="post" name="redirect" action="<%=strCCAveUrl%>">
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>" />
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>" />


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
