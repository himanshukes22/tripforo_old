<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="quickshow.aspx.cs" Inherits="quickshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Advance_CSS/css/Dash.css" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/PopupStyle.css?V=1")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/")%>';        
    </script>
    <style>
        .navbar-theme-transparent .navbar-nav > li > a {
            font-size: 13px;
        }
    </style>

    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Quick Show</a></li>
    </ol>

    <div class="card-main">
        <div class="card-body report">
            <div class="inner-box">
                <div class="row">
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="From" id="From" placeholder="From Date" class="theme-search-area-section-input" readonly="readonly" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="To" placeholder="To Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" id="tdTripNonExec2" runat="server">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:DropDownList class="theme-search-area-section-input" ID="ddlFromCity" runat="server">
                                    <asp:ListItem Value="">Select From City</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:DropDownList class="theme-search-area-section-input" ID="ddlToCity" runat="server">
                                    <asp:ListItem Value="">Select To City</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:DropDownList class="theme-search-area-section-input" ID="ddlAirline" runat="server">
                                    <asp:ListItem Value="">Select Airline</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <br />
                            <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn btnprocessing" OnClick="btn_result_Click" OnClientClick="ClickProcess();">
                                <i class="fa fa-search" style="font-size: 10px;"></i>  Search
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line" style="text-align: center;">
                            <br />
                            <asp:Label ID="lblTotalRecord" runat="server" Style="font-size: 15px;">Total Record : 0</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divReport" runat="server">
        <div class="table-responsive">
            <asp:GridView ID="gvQuickShow" runat="server" OnPageIndexChanging="gvQuickShow_PageIndexChanging" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="20" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Departure Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDeparture_Date" runat="server" Text='<%# Convert.ToDateTime(Eval("Departure_Date")).ToString("dd MMM yyyy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City From">
                        <ItemTemplate>
                            <asp:Label ID="lblCity_From" runat="server" Text='<%# Eval("OrgDestFrom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City To">
                        <ItemTemplate>
                            <asp:Label ID="lblCity_To" runat="server" Text='<%# Eval("OrgDestTo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AirLine Name">
                        <ItemTemplate>
                            <asp:Label ID="lblAirLineName" runat="server" Text='<%# Eval("AirLineName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Seats">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Seats" runat="server" Text='<%# Eval("Total_Seats") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Used Seat">
                        <ItemTemplate>
                            <asp:Label ID="lblUsed_Seat" runat="server" Text='<%# Eval("Used_Seat") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Available Seat">
                        <ItemTemplate>
                            <asp:Label ID="lblAvl_Seat" runat="server" Text='<%# Eval("Avl_Seat") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script>
        function ClickProcess() {
            $(".btnprocessing").html(" Processing...<i class='fa fa-pulse fa-spinner' style='font-size: 10px;'></i>");
        }
    </script>
</asp:Content>

