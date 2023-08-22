<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="SalesExecRegDetail.aspx.vb" Inherits="Reports_Admin_SalesExecRegDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 80%">
                        <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl"
                            width="900">
                            <tr>
                                <td bgcolor="#20313f" height="25px">
                                    <h2>
                                        Sales Executive Registration Details</h2>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" border>
                                        <tr>
                                            <td style="width: 0%">
                                            </td>
                                            <td style="width: 100%">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 2%">
                                                        </td>
                                                        <td style="width: 96%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="Grid_SalesExecReg" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                            OnRowCommand="GridRowCommand" PageSize="1000"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FirstName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_FirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_FirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LastName">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_LastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="RegDate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_RegDate" runat="server" Text='<%# Eval("RegDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Location">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Location" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_Loc" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MobileNo">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_MobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_MobileNo" runat="server" Text='<%# Bind("MobileNo") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="EmailID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Email" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_Email" runat="server" Text='<%# Bind("EmailId") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Password">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Password" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
           <asp:TextBox ID="txt_Password" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
            </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="120px" ItemStyle-Height="35px">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="Status" CommandArgument='<%#Eval("ID") %>'
                                                                                            Text='<%# Bind("Status") %>' Font-Bold="True" Font-Underline="False" OnClick="ITZ_Accept_Click"> </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Height="35px" Width="120px"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <RowStyle CssClass="RowStyle" />
                                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                            <HeaderStyle CssClass="HeaderStyle" />
                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 0%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                z-index: 1000;">
            </div>
            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                font-weight: bold; color: #000000">
                Please Wait....<br />
                <br />
                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
