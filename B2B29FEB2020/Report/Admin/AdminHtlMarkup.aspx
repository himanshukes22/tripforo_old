<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageForDash.master" CodeFile="AdminHtlMarkup.aspx.vb" Inherits="SprReports_Admin_AdminHtlMarkup" %>

<%@ Register Src="~/UserControl/HotelSettings.ascx" TagPrefix="uc1" TagName="HotelSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Hotel/JS/HotelMarkup.js") %>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <link href="<%=ResolveUrl("../../Hotel/css/B2Bhotelengine.css")%>" rel="stylesheet" type="text/css" />
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">

        <div class="large-3 medium-3 small-12 columns">

            <uc1:HotelSettings runat="server" ID="Settings"></uc1:HotelSettings>

        </div>
        <div class="large-8 medium-8 small-12 columns end">
            <div class="large-12 medium-12 small-12 heading">
                <div class="large-12 medium-12 small-12 heading1">
                    HOTEL MARKUP
                                          <input type="hidden" id="From" name="From" value="" />
                    <input type="hidden" id="To" name="TO" value="" />
                    <input type="hidden" id="hidtxtDepDate" name="hidtxtDepDate" value="" />
                    <input type="hidden" id="hidtxtRetDate" name="hidtxtRetDate" value="" />
                </div>
                <div class="clear1"></div>

                <div class="large-2 medium-3 small-3 columns">Star:</div>
                <div class="large-3 medium-3 small-9  columns">
                    <asp:DropDownList runat="server" TabIndex="4" ID="ddlStar">
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="large-2 medium-2 small-3 large-push-2 medium-push-2 columns">
                    Country:
                </div>
                <div class="large-3 medium-3 small-9 large-push-2 medium-push-2 columns">
                    <input type="text" name="TR_Country" id="TR_Country" value="ALL" />
                    <input type="hidden" id="TR_Country1" name="TR_Country1" value="" />
                </div>
                <div class="clear"></div>
                <div class="large-2 medium-2 small-3 columns">City Name: </div>
                <div class="large-3 medium-3 small-9 columns end">
                    <input type="text" name="htlCity" id="htlCity" value="ALL" />
                    <input type="hidden" id="htlcitylist" name="htlcitylist" value="" />
                </div>

                <div class="clear"></div>
                <div class="large-2 medium-2 small-3 columns">Markup Type:</div>
                <div class="large-3 medium-3 small-9 columns">
                    <input type="radio" name="mrkupType" value="Fixed" id="rdbFixed" checked="checked" />
                    <asp:Label ID="lblFixed" runat="server" Text="Fixed"></asp:Label>
                    <input type="radio" name="mrkupType" value="Percentage" id="rdbPercentage" />
                    <asp:Label ID="lblPercentage" runat="server" Text="Percentage"></asp:Label>
                    <input id="mrktype" type="hidden" name="mrktype" value="Fixed" />
                </div>
                <div class="large-2 medium-2 small-3 large-push-2 medium-push-2 columns">
                    <span id="lblMrkAmt">Markup Amount</span>
                    <%--<asp:TextBox runat="server" ID="lblMrkAmt" Text="Markup Amount" BorderStyle="None" ReadOnly="True" Style="color: inherit;"></asp:TextBox>--%>
                </div>
                <div class="large-3 medium-3 small-9 large-push-2 medium-push-2 columns">
                    <asp:TextBox ID="txtAmt" runat="server" onKeyPress="return checkit(event)" MaxLength="4"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <div class="large-2 medium-2 small-3 columns">
                    Agency Name:
                </div>
                <div class="large-3 medium-3 small-9 columns end">
                    <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" value="ALL" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    <input type="hidden" id="Hidden1" name="Agency" value="AdminAgency" />

                </div>
                <div class="clear"></div>

                <div class="large-12 medium-12 small-12" id="NoRecard" runat="server"></div>
                <div class="clear"></div>
                <div class="large-4 medium-4 small-12 large-push-8 medium-push-8">
                    <div class="large-6 medium-6 small-6 columns">
                        <asp:Button ID="btn_Submit" runat="server" Text="New Entry" OnClientClick="return MarkupValidation()" CssClass="buttonfltbk" />&nbsp;&nbsp;
                    </div>

                    <div class="large-6 medium-6 small-6 columns">
                        <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="buttonfltbk" />
                        <input type="hidden" id="Agency" name="Agency" value="Agent" />
                    </div>
                </div>
                <div class="clear1"></div>
            </div>
        </div>
        <div class="clear1"></div>
        <div class="large-12 medium-12 small-12">

            <asp:UpdatePanel ID="BlockAirlineUpdPanel" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GrdMarkup" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px"  AlternatingRowStyle-CssClass="alt"
                       AllowPaging="true" PageSize="100" OnPageIndexChanging="GrdMarkup_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="AgentID" HeaderText="AGENT ID" ReadOnly="true" />
                            <%--<asp:BoundField DataField="TripType" HeaderText="TRIP TYPE" ReadOnly="true" />--%>
                            <asp:BoundField DataField="Country" HeaderText="COUNTRY" ReadOnly="true" />
                            <asp:BoundField DataField="City" HeaderText="CITY" ReadOnly="true" />
                            <asp:BoundField DataField="Star" HeaderText="STAR" ReadOnly="true" />
                            <asp:BoundField DataField="MarkupType" HeaderText="MARKUP TYPE" ReadOnly="true" />
                            <asp:TemplateField HeaderText="MARKUP">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAmt" runat="server" Text='<%# Bind("MarkupAmount")%>' onKeyPress="return checkit(event)" Width="83px" MaxLength="4"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("MarkupAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="EDIT"
                                        ImageUrl="~/Images/edit.png" ToolTip="Edit" Height="16px" Width="20px"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="UPDATE"
                                        ImageUrl="~/Images/ok.png" ToolTip="Update" Height="16px" Width="20px"></asp:ImageButton>
                                    <asp:ImageButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="CANCEL" ToolTip="Cancel" Height="16px" Width="20px"
                                        ForeColor="#20313f" Font-Strikeout="False" Font-Overline="False" Font-Bold="true" ImageUrl="../../Images/cancel1.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("MarkupID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DELETE">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lbkDelete" runat="server" CausesValidation="True" CommandName="Delete" Text="DELETE" ToolTip="Delete" Height="16px" Width="16px"
                                        ImageUrl="../../Images/delete.png" OnClientClick="return confirm('Are you sure to delete it?');"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        var HtlUrlBase = location.protocol + '//' + location.host;
    </script>
</asp:Content>
