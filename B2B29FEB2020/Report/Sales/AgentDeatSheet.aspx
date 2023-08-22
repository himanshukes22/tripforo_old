<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="AgentDeatSheet.aspx.vb" Inherits="Reports_Sales_AgentDeatSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
 <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
     <div class="divcls">
        <table cellpadding="0" cellspacing="0" align="center" style="background: #fff;">
            <tr>
                <td width="10" height="10" valign="top">
                    <img src="../../images/box-tpr.jpg" width="10" height="10" />
                </td>
                <td style="background: url(../../images/box-tp.jpg) repeat-x left bottom;" height="10">
                </td>
                <td valign="top">
                    <img src="../../images/box-tpl.jpg" width="10" height="10" />
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px; background: url(../../images/boxl.jpg) repeat-y left bottom;">
                </td>
                <td bgcolor="#20313f" height="25px">
                    <h2>
                        Agent Deal Sheet
                    </h2>
                </td>
                <td style="width: 10px; height: 10px; background: url(../../images/boxr.jpg) repeat-y left bottom;">
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px; background: url(../../images/boxl.jpg) repeat-y left bottom;">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 98%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 90%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" height="10px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" height="40px" class="TextBig" width="230px">
                                                                    Deal Search By Agency Name:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="dll_Agency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dll_Agency_SelectedIndexChanged"
                                                                        CssClass="combobox">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" height="40px" class="TextBig" width="600px">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btn_Search" runat="server" Text="Search" OnClick="btn_Search_Click"
                                                                        Visible="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="Grid_AgentDealSeat" runat="server" Width="100%"  CssClass="table table-hover" GridLines="None" Font-Size="12px"
                                                            AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Airline">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Airline" runat="server" Text='<%# Eval("Airline") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BasicFare">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_BasicFare" runat="server" Text='<%# Eval("Dis") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="FuelSurcharge">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_FuelSurcharge" runat="server" Text='<%# Eval("Dis_YQ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BasicFare+FuelSurcharge">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_FareSurcharge" runat="server" Text='<%# Eval("Dis_B_YQ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CashBack" ItemStyle-Height="35px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_CB" runat="server" Text='<%# Eval("CB") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                             
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px; height: 10px; background: url(../../images/boxr.jpg) repeat-y left bottom;">
                </td>
            </tr>
            <tr>
                <td width="10" height="10" valign="top">
                    <img src="../../images/box-bl.jpg" width="10" height="10" />
                </td>
                <td style="background: url(../../images/box-bottom.jpg) repeat-x left bottom;" height="10">
                </td>
                <td valign="top">
                    <img src="../../images/box-br.jpg" width="10" height="10" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

