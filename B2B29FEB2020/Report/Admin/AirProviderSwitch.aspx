<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="AirProviderSwitch.aspx.cs" Inherits="SprReports_Admin_AirProviderSwitch" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 154px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 150px;
                width: 150px;
            }
    </style>

    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">

            <uc1:Settings runat="server" ID="Settings" />

        </div>
        <div class="large-8 medium-8 small-12 columns heading end">
            <div class="large-12 medium-12 small-12 heading1">
                Air Povider Switch
            </div>
            <div class="clear1"></div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal">
                        <div class="center">
                            Please Wait....
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        Trip Type :
        <asp:DropDownList ID="ddl_triptype" runat="server" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="ddl_triptype_SelectedIndexChanged">
            <asp:ListItem Value="D" Selected="True">Domestic</asp:ListItem>
            <asp:ListItem Value="I">International</asp:ListItem>
        </asp:DropDownList>
                    </div>
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_status" runat="server" Font-Bold="True" ForeColor="#009933" Width="550px"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                                    DataKeyNames="Airline" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" CssClass="table table-hover" GridLines="None" Font-Size="12px"  OnRowDataBound="GridView1_RowDataBound">
                                    <%-- <AlternatingRowStyle BackColor="#FFCC66" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Airline" runat="server" Text='<%#Eval("Airline") %>'></asp:Label>
                                                <asp:Label ID="lbl_counter" runat="server" Visible="false" Text='<%#Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RTF">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_RTF" runat="server" OnCheckedChanged="chk_RTF_CheckedChanged" AutoPostBack="true" Checked='<%# bool.Parse(Eval("RTF").ToString()) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Status" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Status_CheckedChanged" Checked='<%# bool.Parse(Eval("Status").ToString()) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Provider">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Provider" runat="server" Text='<%# Eval("Provider")%>' Visible="false"></asp:Label>
                                                <asp:RadioButtonList ID="rbl_Provider" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_Provider_SelectedIndexChanged" runat="server"></asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ProviderType" runat="server" Text='<%#Eval("AirlineType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



</asp:Content>

