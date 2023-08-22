<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDSPaymentGateway.aspx.cs" Inherits="Eticketing_RDSPaymentGateway" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">





<head runat="server">
    <title></title>
    <link href="../CSS/newcss/main.css" rel="stylesheet" type="text/css" />
 <link href="../CSS/newcss/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  src="../Scripts/jquery-1.8.2.min.js" ></script>
    
        <style type="text/css">
        .txtbx{
                height: 40px;
    width: 96%;
    border-radius: 6px;
    border: thin solid #ddd;
        }
        .formbg{
            background-color: #fff;
    /*padding: 20px 10px;*/
    border-radius: 6px;
    border: thin solid #ddd;
    line-height: 160%;
    font-size: 13px;
        }

        .w45{
            width:45% !important;}
        .w35{
            width:35% !important;
        }
        .p10{
            padding: 20px 10px;
        }
        .rgt{float:right;}
        .headnew{    height: 41px;
    background-color: #337ab7;
    color: #fff;
    /*font-weight: bold;*/ 
    padding: 10px;
    font-size: 18px;
}

        .bld{
            font-weight:bold;
        }
        .w50{
            width:55% !important;
        }
    </style>
    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }

        setTimeout("preventBack()", 0);

        window.onunload = function () { null };

</script>

    <script type = "text/javascript">
        window.onload = function () {
            document.onkeydown = function (e) {
                return (e.which || e.keyCode) != 116;
            };
        }

        function Validate() {

            if ($("#TxtUserId").val() == "") {
                alert("Plese enter user id");
                $("#TxtUserId").focus();
                return false;
            }
            if ($("#TxtPassword").val() == "") {
                alert("Plese enter password");
                $("#TxtPassword").focus();
                return false;
            }
        }

</script>

    <%--<script type="text/javascript">
        window.onbeforeunload = function () {
            return "Dude, are you sure you want to leave? Think of the kittens!";
        }
</script>--%>
</head>
<body style="background-color: #f4f4f4;" onkeydown="return (event.keyCode != 116)">
    <div class="col-md-12 head-text topblc">
            CUSTOMER SUPPORT - 02233487788
        </div>
    <div class="col-md-12 col-xs-12 logo">
                                  <%--  <a>
                                         http://localhost:53943/images/logo.png</a>--%>
         <img src="../Images/logo.png" alt="Header image" border="0" />
                                    
                                </div>

    <div class="col-md-12">

    <div class="w35 auto formbg">
    <form id="form1" runat="server">
    <div id="DivLogin" runat="server"> 
        <div class="headnew">
             <span class="w50 lft">Transaction Amount - </span>
          <span class="lft w35"><asp:Label ID="lblTranAmount" runat="server" ></asp:Label> </span> <br />
        </div>
   <div class="p10">   
    <span class="bld">Payment User-ID</span>
        <asp:TextBox ID="TxtUserId" CssClass="txtbx" runat="server" MaxLength="50"></asp:TextBox>
         <br /><br />
         <span class="bld">Payment Password</span>
        <asp:TextBox ID="TxtPassword" CssClass="txtbx" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <span class="lft w45"> <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" class="btn btn-primary btn-lg" OnClientClick="return Validate();" /> </span>
       
        <span class="rgt w45"><asp:Button ID="BtnLoginCancel" runat="server" Text="Cancel" OnClick="BtnLoginCancel_Click" class="btn btn-success btn-lg" /> </span>
         <br />
        <br />
    </div>
    </div>  
   
        <div id="DivAfterLogin" runat="server"> 
       <div class="headnew">     
    <span class="w50 lft">Account Balance -</span>
         <span class="lft w35">  <asp:Label ID="lblAccountBalance" runat="server" ></asp:Label></span></div>
       <div class="p10">
         <span class="lft w50 bld">Payment User Id -</span>
           <span class="lft w45">
       <asp:Label ID="lblUserId" runat="server" ></asp:Label></span>
        <br /> <br /> 
 <span class="lft w50 bld">Transaction Amount -</span><asp:Label ID="lblTransAmtAfterLogin" runat="server" ></asp:Label><br /><br />
       <span class="lft w45">   
        <asp:Button ID="BtnPay" runat="server" Text="Pay" OnClick="BtnPay_Click" class="btn btn-primary btn-lg" />  </span>
           <span class="rgt w45">
           
           <asp:Button ID="BtnPayCancel" runat="server" Text="Cancel" OnClick="BtnPayCancel_Click" class="btn btn-success btn-lg" /> </span>
           <br />
           <br />
    </div></div>
     <asp:Label ID="lblMsg" runat="server" ></asp:Label>
         <%--<asp:Label ID="LblReferenceNo" runat="server" ></asp:Label>
         <asp:Label ID="LblMerchantCode" runat="server" ></asp:Label>
         <asp:Label ID="LblReservationId" runat="server" ></asp:Label>--%>
    <asp:HiddenField ID="HdnReferenceNo" runat="server" />
    <asp:HiddenField ID="HdnMerchantCode" runat="server" />
    <asp:HiddenField ID="HdnReservationId" runat="server" />
    <asp:HiddenField ID="HdnReturnUrl" runat="server" />
    <asp:HiddenField ID="HdnTrnsAmount" runat="server" />
        

    
    </form>
         <%--<div class="clear1">  <a href="BankRequest.aspx">Bank Request</a></div> --%>
        </div>

        </div>
   
    
</body>
</html>
