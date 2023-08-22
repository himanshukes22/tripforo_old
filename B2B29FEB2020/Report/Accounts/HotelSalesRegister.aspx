<%@ Page Language="VB" MasterPageFile="~/MasterForHome.master" AutoEventWireup="false" CodeFile="HotelSalesRegister.aspx.vb" Inherits="SprReports_Accounts_HotelSalesRegister" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="<%=ResolveUrl("~/Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet" type="text/css" /> --%>
    <link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class=" ">
            <div class="col-md-12 text-center search-text  ">
                Hotel Sales Register
            </div>
        </div>
        <div class="row" style="padding-top: 30px;">
            <div class="col-md-9 col-sm-12 col-md-push-1">
                <div class="form-inlines">
                    <div class="form-groups col-md-3 col-xs-12">
                        <input type="text" name="From" id="From" placeholder="From Date" class="form-control" readonly="readonly" />
                    </div>
                    <div class="form-groups col-md-3 col-xs-12">
                        <input type="text" name="To" placeholder="To Date" id="To" class="form-control" readonly="readonly" />
                    </div>
                    <div class="form-groups col-md-3 col-xs-12">
                        <asp:TextBox ID="txt_OrderId" placeholder="OrderId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups col-md-3 col-xs-12">
                        <asp:TextBox ID="txt_bookingID" placeholder="Booking ID" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups col-md-3 col-xs-12">
                        <asp:TextBox ID="txt_htlcode" placeholder="Hotel Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups col-md-3 col-xs-12">
                        <asp:TextBox ID="txt_roomcode" placeholder="Room Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups col-md-3 col-xs-12" id="TDAgency" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>
                </div>
            </div>
            <div class="col-md-2 col-md-push-1 col-xs-12">
                <asp:Button ID="btn_result" runat="server" class="buttonfltbks" Text="Search Result" />
                <asp:Button ID="btn_Export" runat="server" class="buttonfltbk" Text="Export" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-5 col-sm-12">
                &nbsp;
            </div>
            <div class="col-md-5 col-xs-12">
                <div class="col-md-6 col-xs-12">
                    Total Amount :
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
                </div>
                <div class="col-md-6 col-xs-12">
                    Total Hotel Booked :
                                <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
                </div>
            </div>

        </div>
        <div class="clear1"></div>

        <div class="col-sm-12 col-xs-12">
            <asp:GridView ID="GrdReport" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px" AllowPaging="True" PageSize="20">
                <Columns>
                    <asp:TemplateField HeaderText="OrderID">
                        <ItemTemplate>
                            <a href='HotelInvoiceDetails.aspx?OrderId=<%#Eval("OrderId") %>&amp;AgentID=<%#Eval("AgentId") %>' rel="lyteframe"
                                rev="width: 686px; height: 551px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">Invoice</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BookingID" HeaderText="BookingID" ReadOnly="true" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="AgentID" HeaderText="Agent ID " />
                    <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                    <asp:BoundField DataField="PurchaseCost" HeaderText="Purchase Cost" />
                    <asp:BoundField DataField="NetCost" HeaderText="Net Cost" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                    <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                    <asp:BoundField DataField="RoomName" HeaderText="Room Name" />
                    <asp:BoundField DataField="StarRating" HeaderText="Star" />
                    <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                </Columns>

            </asp:GridView>
        </div>

    </div>
    <style>
        .form-control, select {
            display: block !important;
            width: 100% !important;
            height: 38px !important;
            padding: 6px 12px !important;
            font-size: 14px !important;
            line-height: 1.42857143 !important;
            color: #a2a2a2 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;
            border-radius: 0px !important;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075) !important;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075) !important;
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s !important;
        }
    </style>
    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getFullYear()) + '-' + (myDate.getMonth() + 1) + '-' + myDate.getDate();
        //document.getElementById("From").value = currDate;
        //document.getElementById("To").value = currDate;
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>
