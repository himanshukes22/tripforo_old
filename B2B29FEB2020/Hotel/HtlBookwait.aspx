<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HtlBookwait.aspx.vb" Inherits="Hotel_HtlBookwait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="background: #fff; width: 25%; margin: auto; border: 1px solid #ccc; margin-top: 100px; text-align: center; padding: 2%;">
            <table cellspacing="10" cellpadding="0" style="width: 100%">
                <tr>
                    <td>
                        <img alt="loading" src="../images/Loadinganim.gif" /></td>
                </tr>
                <tr>
                    <td>We are processing your request...
                    </td>
                </tr>
               
                <tr>
                    <td>Hotel Name: <%=Session("HtlName")%>                    </td>
                </tr>
                <tr>
                    <td>Check In: <%=Session("sCheckIn1").ToString%>
                    </td>
                </tr>
                <tr>
                    <td>Check Out: <%=Session("sCheckOut1").ToString%><br />
                    </td>
                </tr>
                <tr>
                    <td>Please wait. Do not close this window.<br />
                    </td>
                </tr>
            </table>
            <div style="clear: both;"></div>
        </div>

        <script language="javascript" type="text/javascript">
            document.location.href = 'ITZHotelBooking.aspx';
        </script>
        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    </form>
</body>
</html>
