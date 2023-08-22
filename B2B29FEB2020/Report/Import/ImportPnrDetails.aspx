<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportPnrDetails.aspx.vb"
    Inherits="ImportPnrDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
         <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:GridView ID="GridViewshow" runat="server" AutoGenerateColumns="false"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                        <Columns>
                            <asp:TemplateField HeaderText="Orgin">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_orgin" runat="server" Text='<%#Eval("Departure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Destination">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_destination" runat="server" Text='<%#Eval("Destination") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departuredate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_depdate" runat="server" Text='<%#Eval("Departdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departuretime">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_depttime" runat="server" Text='<%#Eval("DepartTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrivaldate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_arrdate" runat="server" Text='<%#Eval("ArrivalDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrivaltime">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_arrtime" runat="server" Text='<%#Eval("ArrivalTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Airline">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Airline" runat="server" Text='<%#Eval("Airline") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FlightNo">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_flno" runat="server" Text='<%#Eval("FlightNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBD">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_rbd" runat="server" Text='<%#Eval("RDB") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="paxdiv" runat="server">
                    </div>
                </td>
            </tr>
			
        </table>
    </div>
    </form>
</body>
</html>
