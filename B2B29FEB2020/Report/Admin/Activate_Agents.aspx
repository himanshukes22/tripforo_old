<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Activate_Agents.aspx.vb" Inherits="Reports_Admin_Activate_Agents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../js/chrome.js"></script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl" width="900px">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" align="center">
                            <tr>
                                <td width="120px" class="h2" style="font-size: 12px">SearchAgency :</td>
                                <td valign="middle" width="200px" align="left">

                                    <input type="text" runat="server" class="textboxflight" id="AgName" name="AgName"
                                        value="" style="width: 180px; height: 20px" />
                                </td>
                                <td width="120px">
                                    <asp:Button ID="searchAgency" runat="server" Text="Submit" CssClass="button" />
                                </td>
                                <td>
                                    <asp:Button ID="export" runat="server" Text="Export" CssClass="button" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="cmdcrd" runat="server" Text="Update Credit" BackColor="#DA251d" Font-Bold="True"
                                                    ForeColor="White" Style="display: none;" />
                                                <br />
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                                    AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PageSize="15" CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                                                    <Columns>
                                                        <asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />
                                                        <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name" />
                                                        <asp:BoundField DataField="user_id" HeaderText="Agency ID" />
                                                        <%--<asp:TemplateField HeaderText="Agency ID">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ForeColor="RED" ID="TextBox1" runat="server" Text='<%# Bind("user_id") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="#7A4815" Text='<%# Bind("user_id") %>'
                                                                CommandArgument='<%# Bind("user_id") %>' OnClick="LinkButton2_Click" OnClientClick="javascript:MyFunction();">LinkButton</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                        <%--<asp:BoundField DataField="Crd_Limit" HeaderText="Available Balance" />--%>
                                                        <asp:BoundField DataField="Crd_Trns_Date" HeaderText="Transaction Date" />
                                                        <asp:TemplateField HeaderText="Activation">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ForeColor="RED" ID="TextBox1" runat="server" Text='<%# Bind("pnr_locator") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#7A4815" Text='<%# Bind("Agent_status") %>'
                                                                    CommandArgument='<%# Bind("user_id") %>' OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Online Tkt">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ForeColor="RED" ID="TextBox2" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="#7A4815" Text='<%# Bind("Online_Tkt") %>'
                                                                    CommandArgument='<%# Bind("user_id") %>' OnClick="LinkButton3_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr style="display: none">
                                            <td style="font-family: tahoma; font-size: 14px; font-weight: bold; color: #20313f;">Total Available Balance Rs.<strong> <font color="Red">
                                                <%=Format(CDbl(total_Credit), "####")%>
                                            </font></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
