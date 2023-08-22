<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DealSheetDomIntl.aspx.vb"
    Inherits="DealSheetDomIntl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div class="divcls">
        <table style="width: 100%; border: 0px;" cellpadding="0" cellspacing="0" align="right">
            <tr>
                <td>
                    <img src="../../images/logo.png" alt="Header image" border="0" />
                </td>
                <td align="right" style="line-height: 20px;">
                    <b>Email:</b> <a href="mailto:b2bhelp@RWT.com">b2bhelp@RWT.com</a><br />
                    <b>Customer Support:</b> + 91 11 46464140
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 5%">
                </td>
                <td style="width: 90%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30px" style="padding-left: 30px; font-family: arial, Helvetica, sans-serif;
                                            font-size: 18px; font-weight: bold; color: #000000; background-color: #CCCCCC;">
                                            Domestic Deal Sheet
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding: 10px 10px 15px 10px">
                                            <asp:GridView ID="GridView_Domestic" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Airline">
                                                        <ItemTemplate>
                                                            <%--<img src="<%# ResolveClientUrl("~/AirLogo/sm")+ Eval("Airline")+".gif" %>" alt='<%# Eval("Airline") %>' />
                                                            <br />--%>
                                                            <asp:Label ID="lblairlineC" runat="server" Text='<%# Eval("Airline") %>'></asp:Label>(<asp:Label
                                                                ID="Labelair" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Dis" HeaderText="Basic Fare" />
                                                    <asp:BoundField DataField="Dis_YQ" HeaderText="Fuel Surcharge" />
                                                    <asp:BoundField DataField="Dis_B_YQ" HeaderText="Basic Fare + Fuel Surcharge" />
                                                    <asp:BoundField DataField="CB" HeaderText="Cash Back" />
                                                </Columns>
                                                
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20px">
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #004b91">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left: 30px; font-family: arial, Helvetica, sans-serif; font-size: 18px;
                                            font-weight: bold; color: #000000; background-color: #CCCCCC;" height="30px">
                                            International Deal Sheet
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="white-space: nowrap;">
                                            <div style="font-size: 14px; font-weight: bold; padding-bottom: 10px">
                                                IATA Commission & PLB</div>
                                            <div>
                                                <asp:GridView ID="GridView_Commission" runat="server" AutoGenerateColumns="False"
                                                    CssClass="GridViewStyle" GridLines="None" Width="800px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Airline">
                                                            <ItemTemplate>
                                                                <%--<img src="<%# ResolveClientUrl("~/AirLogo/sm")+ Eval("AirlineCode")+".gif" %>" alt='<%# Eval("AirlineCode") %>' /><br />--%>
                                                                <asp:Label ID="lblairlineC" runat="server" Text='<%# Eval("AirlineCode") %>'></asp:Label>(
                                                                <asp:Label ID="Labeldd" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CommisionOnBasic" HeaderText="CommOnBasic" />
                                                        <asp:BoundField DataField="CommissionOnYq" HeaderText="CommOnYQ" />
                                                        <asp:BoundField DataField="CommisionOnBasicYq" HeaderText="Comm(Basic+ YQ)" />
                                                        <asp:BoundField DataField="PLBOnBasic" HeaderText="PLBOnBasic" />
                                                        <asp:BoundField DataField="PlbOnBasicYq" HeaderText="PLB(Basic + YQ)" />
                                                        <asp:BoundField DataField="RBD" HeaderText="Restricted Class" />
                                                        <asp:BoundField DataField="Sector" HeaderText="Restricted Sector" />
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle" />
                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <HeaderStyle CssClass="HeaderStyle" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="center">
                                            <div style="font-size: 14px; font-weight: bold; padding-bottom: 10px">
                                                PLB</div>
                                            <div>
                                                <asp:GridView ID="GridView_PLB" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                                    GridLines="None" Width="750px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Airline">
                                                            <ItemTemplate>
                                                                <img src="http://www.travelsbyte.com/AirLogo/sm<%# Eval("AirlineCode") %>.gif" /><br />
                                                                 <asp:Label ID="lblairlineP" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PLBOnBasic" HeaderText="PLBOnBasic" />
                                                        <asp:BoundField DataField="PlbOnBasicYq" HeaderText="PLB(Basic + YQ)" />
                                                        <asp:BoundField DataField="RBD" HeaderText="Restricted Class" />
                                                        <asp:BoundField DataField="Sector" HeaderText="Restricted Sector" />
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle" />
                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <HeaderStyle CssClass="HeaderStyle" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
