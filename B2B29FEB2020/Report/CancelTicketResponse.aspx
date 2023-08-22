<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CancelTicketResponse.aspx.vb"
    Inherits="SprReports_Admin_CancelTicketResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cancel Ticket Response</title>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../Images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="3" cellpadding="3" align="center" border='0' style='border: 1px solid #ccc;
            margin-top: 10px; box-shadow: 1px 1px 5px #333; padding: 10px; border-radius: 10px;
            -o-border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px;
            width: 50%; margin: auto; text-align: left;'>
            <tr>
                <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                    Search Bus Failed Details
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="90" style="font-weight: bold" height="25">
                                From Date
                            </td>
                            <td width="130">
                                <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                    style="width: 100px" />
                            </td>
                            <td width="90" style="font-weight: bold">
                                To Date
                            </td>
                            <td width="120px">
                                <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                            </td>
                            <td align="left" width="80" style="font-weight: bold">
                                OrderId
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrderID" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="40%">
                        <tr>
                            <td>
                                Agency Name:
                            </td>
                            <td>
                                <input type="text" id="txtAgencyName" name="txtAgencyName" />
                                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="color: #FF0000" colspan="4">
                    * N.B: To get Today's booking without above parameter,do not fill any field, only
                    click on search your booking.
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div style="height:30px"></div>
    <div align="center">
        <asp:GridView ID="grdfailed" AutoGenerateColumns="false" runat="server"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
            <Columns>
                <asp:BoundField HeaderText="ORDER&nbsp;ID" DataField="ORDERID" />
                <asp:BoundField HeaderText="CANCEL&nbsp;REQUEST" DataField="CANCELSEAT_REQUEST" />
                <asp:BoundField HeaderText="CANCEL&nbsp;RESPONSE" DataField="CANCELSEAT_RESPONSE" />
                <asp:BoundField HeaderText="AGENT&nbsp;ID" DataField="AGENT_ID" />
                <asp:BoundField HeaderText="CREATED&nbsp;DATE" DataField="CREATED_DATE" />
            </Columns>
            
        </asp:GridView>
    </div>
    </form>
        <script type="text/javascript">
      var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</body>
</html>
