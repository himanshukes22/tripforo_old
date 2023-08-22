<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Agent_Details.aspx.vb" Inherits="Reports_Admin_Agent_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <%-- <style type="text/css">
        input[type="text"], input[type="password"], select, radio, legend, fieldset
        {
            border: 1px solid #004b91;
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
    <%--<link href="../../CSS/lytebox.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="../../JS/lytebox.js" type="text/javascript"></script>--%>
    <%--  <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
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
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <div class="mtop80"></div>
    <div class="divcls large-12 medium-12 small-12 columns">
        <div class="large-8 medium-8 small-12 columns large-push2 medium-push-2 heading">
            <div class="large-12 medium-12 small-12">
                AGENCY DETAILS
            </div>   
                <div class="col-md-10 col-xs-10">
                <div class="clear1"></div>
                <div class="large-2 medium-2 small-6 columns">
                    Registration From :
                </div>
                <div class="large-3 medium-3 small-6 columns">
                    <input type="text" name="From" id="From" class="txtCalander" readonly="readonly" />
                </div>
                <div class="large-2 medium-2 small-6 large-push-2 columns">
                    Registration To :
                </div>
                <div class="large-3 medium-3 small-6 large-push-2 columns">
                    <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" />
                </div>
                <div class="clear1"></div>
                <div class="large-2 medium-2 small-6 columns">
                    Search Agency :
                </div>
                <div class="large-3 medium-3 small-6 columns">
                    <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID"
                        class="txtBox" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
                <div class="large-2 medium-2 small-6 large-push-2 columns" id="tr_AgentType" runat="server">
                    Agent Type :
                </div>
                <div id="tr_GroupType" runat="server" class="large-3 medium-3 small-6 large-push-2 columns">
                    <%-- <input type="text" id="txtAgentType" name="txtAgentType" />--%>
                    <asp:DropDownList ID="DropDownListType" runat="server" CssClass="drpBox">
                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                        <%--<asp:ListItem Text="Type1" Value="Type1"></asp:ListItem>
                                    <asp:ListItem Text="Type2" Value="Type2"></asp:ListItem>
                                    <asp:ListItem Text="Type3" Value="Type3"></asp:ListItem>
                                    <asp:ListItem Text="Type4" Value="Type4"></asp:ListItem>
                                    <asp:ListItem Text="Type5" Value="Type5"></asp:ListItem>
                                    <asp:ListItem Text="Type6" Value="Type6"></asp:ListItem>
                                    <asp:ListItem Text="Type7" Value="Type7"></asp:ListItem>
                                    <asp:ListItem Text="Type8" Value="Type8"></asp:ListItem>
                                    <asp:ListItem Text="Type9" Value="Type9"></asp:ListItem>
                                    <asp:ListItem Text="Type10" Value="Type10"></asp:ListItem>
                                    <asp:ListItem Text="Type11" Value="Type11"></asp:ListItem>
                                    <asp:ListItem Text="Type12" Value="Type12"></asp:ListItem>
                                    <asp:ListItem Text="Type13" Value="Type13"></asp:ListItem>
                                    <asp:ListItem Text="Type14" Value="Type14"></asp:ListItem>
                                    <asp:ListItem Text="Type15" Value="Type15"></asp:ListItem>
                                    <asp:ListItem Text="Type16" Value="Type16"></asp:ListItem>
                                    <asp:ListItem Text="Type17" Value="Type17"></asp:ListItem>
                                    <asp:ListItem Text="Type18" Value="Type18"></asp:ListItem>
                                    <asp:ListItem Text="Type19" Value="Type19"></asp:ListItem>
                                    <asp:ListItem Text="Type20" Value="Type20"></asp:ListItem>
                                    <asp:ListItem Text="Type21" Value="Type21"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="clear1"></div>
                <div class="large-2 medium-2 small-6 columns" id="tr_SalesPerson" runat="server">
                    Search Sales Person :
                </div>
                <div class="large-3 medium-3 small-6 columns" id="tr_ddlSalesPerson" runat="server">
                    <asp:DropDownList ID="DropDownListSalesPerson" runat="server" AppendDataBoundItems="true"
                        CssClass="drpBox">
                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <%--<input type="text" id="txtSalesPerson" name="txtSalesPerson" />--%>
                </div>
                <div class="large-2 medium-2 small-6 large-push-2 columns" id="td_SBS" runat="server">Search By Stockist :</div>
                <div class="large-3 medium-3 small-6 large-push-2 columns" id="td_ddlSBS" runat="server">
                    <asp:DropDownList ID="ddl_stock" runat="server" CssClass="drpBox">
                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="All Stockist" Value="ALL"></asp:ListItem>
                        <asp:ListItem Text="Stockist Agent" Value="STAG"></asp:ListItem>

                    </asp:DropDownList>
                    <%--<asp:ListItem Value="AllDistr">Search </asp:ListItem>
                        </asp:CheckBoxList>--%>
                </div>
                <div class="clear1"></div>

            
                        <div class="large-4 medium-4 small-12 columns">
                            <div class="large-6 medium-6 small-6 columns">
                                <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="buttonfltbks" />
                            </div>
                            <div class="large-6 medium-6 small-6 columns">
                                <asp:Button ID="export" runat="server" Text="Export" CssClass="buttonfltbk" /></div>
                        </div>
            </div>
        </div>
        <div class="large-4 medium-4 small-12 large-push-2 medium-push-2">
            <div>
                <asp:Label ID="TOTALCRD" runat="server" Text=""></asp:Label>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" PageSize="25" CssClass="table table-hover" GridLines="None" Font-Size="12px" HeaderStyle-ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="Debit/Credit" SortExpression="user_id" Visible="false">
                                <ItemTemplate>
                                    <a target="_blank" href="../Distr/UploadAmount.aspx?AgentID=<%#Eval("user_id")%>"
                                        rel="lyteframe" rev="width: 900px; height: 280px; overflow:hidden;" target="_blank"
                                        style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">Debit/Credit </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agency Name" SortExpression="Agency_Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("Agency_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="user ID" SortExpression="user_id">
                                <ItemTemplate>
                                    <a href='Update_Agent.aspx?AgentID=<%#Eval("user_id")%>' rel="lyteframe" rev="width: 900px; height: 400px; overflow:hidden;"
                                        target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("user_id")%>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AgencyID" HeaderText="Agency ID" SortExpression="AgencyID" />
                            <asp:BoundField DataField="Agent_Type" HeaderText="Agent Type" SortExpression="Agent_Type" />
                            <asp:BoundField DataField="Crd_Limit" HeaderText="Credit Limit" SortExpression="Crd_Limit" />
                            <asp:BoundField DataField="timestamp_create" HeaderText="Registration Date" SortExpression="timestamp_create" />
                            <asp:BoundField DataField="Crd_Trns_Date" HeaderText="Transaction Date" SortExpression="timestamp_create" />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="SalesExecID" HeaderText="Sales Ref." SortExpression="SalesExecID" />
                        </Columns>

                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
