<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="GalTKTSwitch.aspx.cs" Inherits="SprReports_Admin_GalTKTSwitch" %>

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
            width: 146px;
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
                Gal Ticketing Switch
            </div>
            <div class="clear1"></div>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_GDS_TKT_Search">
                <ProgressTemplate>
                    <div class="modal">
                        <div class="center">
                            <%-- <img src="Images/loader.gif" />--%>

                    Please Wait....
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="UP_GDS_TKT_Search">
                <ContentTemplate>
                    <div>
                        <table class="auto-style1">
                            <tr>
                                <td style="text-align: left">Trip Type :
                    <asp:DropDownList ID="ddl_gds_tktsearch" runat="server" Font-Bold="True" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddl_gds_tktsearch_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="D">Domestic</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                                                <asp:GridView ID="GV_GDS_TKT_Search" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                                                    DataKeyNames="Counter" OnPageIndexChanging="GV_GDS_TKT_Search_PageIndexChanging" Width="100%">
                                                    <%-- <AlternatingRowStyle BackColor="#FFCC66" />--%>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="HAP ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_HAP" runat="server" Text='<%#Eval("HAP") %>'></asp:Label>
                                                                <asp:Label ID="lbl_counter" runat="server" Visible="false" Text='<%#Eval("Counter") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_UserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_PCC" runat="server" Text='<%#Eval("PCC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Airline">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Airline" runat="server" Text='<%# Eval("Airline")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Online Ticketing Status">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Status" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Status_CheckedChanged" Checked='<%# bool.Parse(Eval("TicketingStatus").ToString()) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Trip Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TT" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Provider Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ProName" runat="server" Text='<%#Eval("Provider") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



</asp:Content>

