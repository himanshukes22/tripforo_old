<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="DomHoldPNRReport.aspx.vb" Inherits="Reports_HoldPNR_DomHoldPNRReport" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
            <uc1:LeftMenu runat="server" ID="LeftMenu" />
        </div>
        <div class="large-8 medium-8 small-12 columns">



            <div class="large-12 medium-12 small-12 bld blue">
                Domestic Hold PNR Report
            </div>

            <div class="clear1">
            </div>

            <div class="large-12 medium-12 small-12">
                <%--<asp:GridView ID="GridProxyDetail" runat="server">
        
        
        </asp:GridView>--%>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="OrderId">
                            <ItemTemplate>
                                <a id="ancher" href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%>' target="_blank"
                                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; font-weight: bold;">
                                    <asp:Label ID="lbl_OrderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>(View)</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AgentId">
                            <ItemTemplate>
                                <asp:Label ID="lbl_AgentID" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PNR">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PNR" runat="server" Text='<%#Eval("GdsPnr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Sector" runat="server" Text='<%#Eval("sector") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Duration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TripType">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TripType" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Trip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TourCode">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TourCode" runat="server" Text='<%#Eval("TourCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalBookingCost">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TotalBookingCost" runat="server" Text='<%#Eval("TotalBookingCost") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalAfterDis">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TotalAfterDis" runat="server" Text='<%#Eval("TotalAfterDis") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaxName">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PgFName" runat="server" Text='<%#Eval("PgFName") %>'></asp:Label>
                                <asp:Label ID="lbl_PgLName" runat="server" Text='<%#Eval("PgLName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaxMobile">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PgMobile" runat="server" Text='<%#Eval("PgMobile") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PgEmail">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PgEmail" runat="server" Text='<%#Eval("PgEmail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreateDate">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CreateDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>

        </div>
    </div>
</asp:Content>

