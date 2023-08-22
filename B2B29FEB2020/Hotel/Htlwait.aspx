<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Htlwait.aspx.vb" Inherits="Htlwait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Searching for the Best Deal</title>
    <link href="../css/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <!--  Code Area Start-->
    <form id="form1" runat="server">
        <div style="background: #fff; width: 25%; margin: auto; border: 1px solid #ccc; margin-top: 100px; text-align: center; padding: 2%;">
            <table cellspacing="10" cellpadding="0" style="width: 100%">
                <tr>
                    <td></td>
                      <%--  <img src="../Images/logo.png" />
                         <img src="Images/fltloding.gif" />--%>
                </tr>
                <tr>
                    <td>We are processing your request...
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- <img src="../Images/wait.gif" />--%>
                         <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="95%" height="49 px" id="FlashID" title="LOGO">
                          <param name="movie" value="../Hotel/Images/wait.swf" />
                          <param name="quality" value="high" />
                          <param name="wmode" value="opaque" />
                          <param name="swfversion" value="6.0.65.0" />
                          <param name="expressinstall" value="Scripts/expressInstall.swf" />
                          <object type="application/x-shockwave-flash" data="../Hotel/Images/wait.swf" width="95%" height="49 px"> 
                            <param name="quality" value="high" />
                            <param name="wmode" value="opaque" />
                            <param name="swfversion" value="6.0.65.0" />
                            <param name="expressinstall" value="Scripts/expressInstall.swf" />
                            <div>
                              <h4>Content on this page requires a newer version of Adobe Flash Player.</h4>
                              <p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" width="95%" height="40 px" /></a></p>
                            </div>
                          </object>
                        </object>
                    </td>
                </tr>
                <tr>
                    <td>City: &nbsp;
                <%=Request("htlCity")%>
                    </td>
                </tr>
                <tr>
                    <td>CheckIn: &nbsp;
                <%=Request("htlcheckin")%>
                    </td>
                </tr>
                <tr>
                    <td>CheckOut: &nbsp;
                <%=Request("htlcheckout")%>
                    </td>
                </tr>
                <tr>
                    <td>Please do not close this window
                    </td>
                </tr>
            </table>
            <div style="clear: both;"></div>
        </div>
        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
        <script language="javascript" type="text/javascript">
            var locations = window.location.href.split("Htlwait.aspx");
            document.location.href = 'HtlResult.aspx' + locations[1] + '&htlCity=<%=Request("htlCity")%>';

           //document.location.href = 'HotelSearchResult.aspx' + locations[1] + '&htlCity=<%=Request("htlCity")%>';

        </script>
    </form>
</body>
</html>
