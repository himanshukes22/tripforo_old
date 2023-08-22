<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="BusBooking.aspx.cs" Inherits="BS_BusBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/CommonCss.css")%>" rel="stylesheet" type="text/css" />

    <div style="background-color: #eee!important; width: 100%; height: auto;">
        <div id="wait" style="margin-top: 100px;" class="wait">
            <div style="padding: 2% 2%; width: 46%; margin: 100px auto 0; border: 5px solid #ccc; background: #fff; text-align: center; font-family: Arial;">
                <strong>Booking is on Process..Please Wait</strong><br />
                <img src="Images/loaderB64.gif" /><br />
            </div>
            <div style="clear: both;">
            </div>
        </div>
    </div>

      <script type="text/javascript">
          function preventBack() { window.history.forward(); }
          setTimeout("preventBack()", 0);
          window.onunload = function () { null };
    </script>
</asp:Content>
<%--<a href="../Search.aspx">../Search.aspx</a>--%>
