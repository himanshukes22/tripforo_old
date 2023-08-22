<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfigureMails.aspx.vb" Inherits="SprReports_ConfigureMails" MasterPageFile="~/MasterAfterLogin.master" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="Styles/tables.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }

        .nowrapgrdview {
            white-space: nowrap;
        }
    </style>
     <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
    <div class="large-3 medium-3 small-12 columns">
                    <uc1:LeftMenu runat="server" ID="LeftMenu" />
                </div>
           
    
    <div class=" large-8 medium-8 small-12 columns end">
        <div class="large-12 medium-12 small-12 heading">
             <div class="large-12 medium-12 small-12 heading1">Configure Mails Here</div>
            <div class="clear1"></div>

                <div class="large-1 medium-1 small-3 columns">Module Type
                </div>
                <div class="large-2 medium-2 small-9  columns">
                    <asp:DropDownList ID="ddlModuleType" runat="server">
                        <asp:ListItem Value="">-----Select-----</asp:ListItem>
                        <%--<asp:ListItem Value="Ticketed">Ticketed</asp:ListItem>--%>
                        <asp:ListItem Value="Hold">Hold</asp:ListItem>
                        <asp:ListItem Value="Reissue">Reissue</asp:ListItem>
                        <asp:ListItem Value="Refund">Refund</asp:ListItem>
                        <asp:ListItem Value="Failed">Failed</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="large-1 medium-1 small-3 large-push-1 medium-push-1 columns">To Email
                </div>
                <div class="large-2 medium-2 small-9 large-push-1 medium-push-1 columns">
                    <%--<input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px;" />--%>
                    <asp:TextBox ID="txtToEmail" runat="server"></asp:TextBox>
                </div>
                <div class="large-1 medium-1 small-3 large-push-2 medium-push-2 columns">Is Active
                </div>
                <div class="large-2 medium-2 small-9 large-push-2 medium-push-2 columns">
                    <asp:RadioButton ID="rbtnIsActiveTrue" Text="Yes" runat="server" GroupName="grpIsAvtive" Checked="true" />
                    <asp:RadioButton ID="rbtnIsActiveFalse" Text="No" runat="server" GroupName="grpIsAvtive" />
                </div>
           
            <div class="clear"></div>
                <div class="large-2 medium-2 small-6 large-push-10 medium-push-10 small-push-6">
                    <asp:Button ID="btnConfigureMail" runat="server" Text="Submit" CssClass="button" OnClientClick="return configValidation();" />
                </div>

            
        </div>
         <div class="clear"></div>
    </div>
         <div class="clear"></div>
    </div>
        <%--<div class="clear1"></div>--%>

        <div class="large-10 medium-12 small-12 large-push-1">
            <div id="divReport" runat="server" visible="false">
                <%-- style="overflow: scroll; max-height: 250px; width: 100%; float: left;"--%>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="GridViewStyle mtop20" GridLines="None" PageSize="10" border="1">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Sno" runat="server" Text='<%#Eval("Sno")%>'></asp:Label>(View)</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Module" runat="server" Text='<%#Eval("ModuleType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_ToEmail" runat="server" Text='<%#Eval("ToEmail") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_ToEmail" runat="server" Text='<%#Eval("ToEmail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active/Deactive">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlActDact" runat="server" SelectedValue='<%#Eval("IsActive")%>'>
                                    <asp:ListItem Value="True">Activate</asp:ListItem>
                                    <asp:ListItem Value="False">Deactivate</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_IsActive" runat="server" Text='<%#Eval("IsActive")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated By">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UpdatedBy" runat="server" Text='<%#Eval("UpdatedBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit/Delete" ItemStyle-CssClass="nowrapgrdview">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%#Eval("Sno")%>' OnClientClick="return confirmUpdate(this);"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>/
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="Delete" CommandArgument='<%#Eval("Sno")%>' OnClientClick="return confirmDelete();"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" Height="50px" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </div>
        </div>

    
    <script type="text/javascript">
        function confirmUpdate(thisObj) {
            var txtEm;
            var objtoFoc;
            var cntEm = 0;
            var thisObjRow = $(thisObj).parent().parent();
            var txtEmObj = $($(thisObjRow).find("td")[1]).find("input[type='text']");
            if ($.trim($(txtEmObj).val()) == "") {
                alert("Email is required");
                $("#" + $.trim($(txtEmObj).attr("id"))).focus();
                return false;
            }
            else {
                var upd = confirm('Are you sure to update this configuration');
                if (upd == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function confirmDelete() {
            var upd = confirm('Are you sure to delete this configuration');
            if (upd == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function configValidation() {
            if ($.trim($("#<%=ddlModuleType.ClientID%>").val()) == "") {
                alert("Module type is required");
                $("#<%=ddlModuleType.ClientID%>").focus();
                return false;
            }
            if ($.trim($("#<%=txtToEmail.ClientID%>").val()) == "") {
                alert("Email is required");
                $("#<%=txtToEmail.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
