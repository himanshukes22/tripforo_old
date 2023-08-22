<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="BusMarkup.aspx.cs" Inherits="BS_BusMarkup" %>

<%@ Register Src="~/UserControl/BusControlsetting.ascx" TagPrefix="busMenu" TagName="BusControlsetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/chrome.js"></script>
    <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
    <div class="col-md-12 text-center search-text  ">
        Bus Markup
                 
    </div>
    <div class="large-12 medium-12 small-12">
    </div>

    <div style="width: 45%; margin: auto; padding: 10px;">



        <table style="width: 100%;" cellspacing="10">
            <tr>

                <td class="fontStyle">MarkUp value:
                </td>
                <td class="fontStyle">MarkUpType
                </td>
                <td class="fontStyle"></td>
            </tr>
            <tr>
                <td>

                    <asp:TextBox runat="server" ID="mk"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                        Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_mktyp" runat="server">
                        <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                        <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Button1" class="buttonfltbks" runat="server" BorderStyle="None" OnClick="btnAdd_Click" Text="Add New" /></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl" runat="server" Style="color: #CC0000;"></asp:Label>
                </td>
            </tr>
        </table>



    </div>

    <table style="width: 100%;" cellspacing="10">
        <tr>
            <td>
                <asp:UpdatePanel ID="UP" runat="server">
                    <ContentTemplate>
                        <div class="clear1"></div>
                        <div class="large-12 medium-12 small-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                Width="100%" Font-Size="12px">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />

                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_userid" runat="server" Text='<%# Eval("USERID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mark Up">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MARKUP_VALUE" runat="server" Text='<%# Eval("MARKUP_VALUE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_MARKUP_VALUE" runat="server" Text='<%# Eval("MARKUP_VALUE")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MarkUpType">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MarkUpType" runat="server" Text='<%# Eval("MARKUP_Type1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                                <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                                                <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txt_MARKUP_TYPE" runat="server" Text='<%# Eval("MARKUP_TYPE")%>'></asp:TextBox>--%>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CREATED_DATE" runat="server" Text='<%# Eval("CREATED_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>

                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
                    <ProgressTemplate>
                        <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                        </div>
                        <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                            Please Wait....<br />
                            <br />
                            <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                            <br />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>

        </tr>
    </table>
    <style type="text/css">
   
</style>
    <%-- <script src="<%= ResolveUrl("~/BS/JS/BusSearch.js")%>" type="text/javascript"></script>--%>
</asp:Content>

