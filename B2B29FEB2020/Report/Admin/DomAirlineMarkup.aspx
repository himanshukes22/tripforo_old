<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="DomAirlineMarkup.aspx.vb" Inherits="Reports_Admin_DomAirlineMarkup" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
        input[type="text"], input[type="password"], select
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>--%>

    <%--  <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

   

    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />--%>

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <%--<style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>--%>

    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">

            <uc1:Settings runat="server" ID="Settings" />

        </div>


        <%--<div class="large-3 medium-3 small-12 columns">
       <div class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Admin/DomAirlineMarkup.aspx")%>">Dom. Airline Markup</a>
    </div>

       <div class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Admin/IntlAirlineMarkup.aspx")%>">Intl. Airline Markup</a>
    </div>
       <%--<tr><td class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Accounts/DomSaleRegister.aspx")%>">Dom. Sale Register</a>
    </td></tr>

       <tr><td class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Accounts/IntlSaleRegister.aspx")%>">Intl. Sale Register</a>
    </td></tr>--%>





        <div class="large-8 medium-8 small-12 columns heading">
            <div class="large-12 medium-12 small-12 heading1">
                Dom Airline Markup
            </div>
            <div class="clear1"></div>

            <div class="large-12 medium-12 small-12">
                <div class="large-2 medium-2 small-4 columns">
                    Agent Type:
                </div>

                <div class="large-3 medium-3 small-8 columns">
                    <asp:DropDownList ID="DropDownListType" runat="server"></asp:DropDownList>
                    <%-- <input
                                                            type="text" id="txtAgencyName" name="txtAgencyName"  onfocus="focusObj(this);"
                                                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />--%>
                </div>
                <div class="large-2 medium-2 small-4 columns large-push-1 medium-push-1">
                    Markup Type:
                </div>
                <div class="large-3 medium-3 small-8 columns large-push-1 medium-push-1">
                    <asp:DropDownList ID="ddl_MarkupType" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                        <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                        <asp:ListItem Value="P">Percentage</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="clear1"></div>
                <div class="large-2 medium-2 small-4 columns">
                    Airline Code:
                </div>
                <div class="large-3 medium-3 small-8 columns">
                    <asp:DropDownList runat="server" ID="air">
                        <asp:ListItem Value="AI" Selected="true">Air India</asp:ListItem>
                        <asp:ListItem Value="AIS">Air India Special</asp:ListItem>
                        <asp:ListItem Value="9W">Jet Airways</asp:ListItem>
                        <asp:ListItem Value="9WS">Jet Airways Special</asp:ListItem>
                        <asp:ListItem Value="G8">GoAir</asp:ListItem>
                        <asp:ListItem Value="G8S">GoAir Special</asp:ListItem>
                        <asp:ListItem Value="6E">Indigo</asp:ListItem>
                        <asp:ListItem Value="6ES">Indigo Special</asp:ListItem>
                        <asp:ListItem Value="SG">Spice Jet</asp:ListItem>
                        <asp:ListItem Value="SGS">Spice Jet Special</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="large-2 medium-2 small-4 columns large-push-1 medium-push-1">
                    Mark Up per pax:
                </div>
                <div class="large-3 medium-3 small-8 columns large-push-1 medium-push-1">
                    <asp:TextBox runat="server" ID="mk" CssClass="combobox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                        Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                </div>

                <div class="clear1"></div>
                <div class="large-12 medium-12 small-12">
                    <asp:Label ID="lbl" runat="server" Style="color: #CC0000;" Font-Bold="True"
                        Font-Size="15px"></asp:Label>
                </div>
                <div class="large-4 medium-4 small-12 columns large-push-8 medium-push-8">
                    <div class="large-6 medium-6 small-6 columns">
                        <asp:Button ID="btnAdd" runat="server" Text="New Entry" />
                    </div>
                    <div class="large-6 medium-6 small-6 columns">
                        &nbsp;<asp:Button ID="btn_Search" runat="server" Text="Search" ToolTip ="Search with Agent Type"/>
                    </div>
                </div>


                <div class="clear1"></div>
            </div>
        </div>
        <div class="clear1"></div>

    </div>
    <div class="large-10 medium-10 small-12 large-push-1 medium-push-1">
        <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                    CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("counter")%>' CssClass="hide"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("counter")%>' CssClass="hide"></asp:Label>
                            </EditItemTemplate>

                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />--%>
                        <asp:BoundField DataField="user_id" HeaderText="Agent Type" ControlStyle-CssClass="textboxflight1"
                            ReadOnly="true">
                            <ControlStyle CssClass="textboxflight1"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Airline" HeaderText="Airline" ControlStyle-CssClass="textboxflight1"
                            ReadOnly="true">
                            <ControlStyle CssClass="textboxflight1"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MarkUp" HeaderText="Mark Up" ControlStyle-CssClass="textboxflight1">
                            <ControlStyle CssClass="textboxflight1"></ControlStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MarkUp Type">
                            <ItemTemplate>
                                <asp:Label ID="LabelMrkType" runat="server" Text='<%# Eval("MarkupType")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddl_MarkupTypeE" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                                    <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                                    <asp:ListItem Value="P">Percentage</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
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
    </div>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
