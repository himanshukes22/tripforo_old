<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelTicket.aspx.cs" Inherits="BS_CancelTicket" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/CommonCss.css" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/CSS/newcss/bootstrap.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divcan_Pax" style="width: 100%; margin: auto;" runat="server">
    </div>
    <div id="divcan" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" class="table" >
            <tr>
                <td align="center">
                    <asp:Repeater runat="server" ID="rep_paxCan">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="5" border="0" class="divcan">
                                <tr>
                                    <td align="left" class="pcls">
                                        <asp:Label ID="lblpax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="15" border="0" align="center" style="width: 100%;">
                                            <tr>
                                                <td>
                                                    Passenger:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblpaxname" Text='<%#DataBinder.Eval(Container.DataItem, "PaxName")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    Fare:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblfare" Text='<%#DataBinder.Eval(Container.DataItem, "Fare")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    Net Fare:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblnetfare" Text='<%#DataBinder.Eval(Container.DataItem, "NetFare")%>'
                                                        runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    Seat:
                                                    <img src='Images/2.png' /><asp:Label ID="lblseat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "seat")%>'></asp:Label></td>
                                                <td>
                                                    
                                                    <asp:CheckBox ID="chkChild" runat="server" Visible='<%# CheckCancelSatus(DataBinder.Eval(Container.DataItem, "Status"),"chk") %>' />
                                                    
                                                       <asp:Label ID="lnbStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Status")%>' Visible='<%# CheckCancelSatus(DataBinder.Eval(Container.DataItem, "Status"),"lbl") %>' /> 
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td align="center">
                <input type="hidden" id="HiddenField1" runat="server" />                               
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button cancel"
                        OnClick="btncancel_Click" OnClientClick="return conFrm();" />
                          <asp:Button ID="btnChkType" runat="server" Text="continue" 
                        CssClass="button cancel" onclick="btnChkType_Click" />
                        
                </td>
            </tr>
        </table>
    </div>
    <div id="divcanwait" style="display: none" class="wait">
        <div style="padding: 2% 2%; width: 46%; margin: 100px auto 0; border: 5px solid #ccc;
            background: #fff; text-align: center; font-family: Arial;">
            <strong>Cancellation is on Process..Please Wait</strong><br />
            <img src="Images/Broken circle.gif" /><br />
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="height:20px">
            </td>
        </tr>
        <tr>
            <td>
                <div id="divrefund" runat="server">
                </div>
            </td>
        </tr>
    </table>
    </form>

    <script src="JS/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="JS/Common.js" type="text/javascript"></script>
</body>
</html>
