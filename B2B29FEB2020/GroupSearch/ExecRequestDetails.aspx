<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="ExecRequestDetails.aspx.cs" Inherits="GroupSearch_ExecRequestDetails" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <script type="text/javascript">
        function loadimg() {
            $("#waitMessage").show();
        }
    </script>
    <div class="large-12 medium-12 small-12" style="margin-top: 80px">
        <div class="large-3 medium-3 small-12 columns">
            <uc1:LeftMenu runat="server" ID="LeftMenu" />
        </div>
        <div class="large-6 medium-6 small-12 columns large-push-1 medium-push-1 end">
            <div class="large-2 medium-2 small-6 columns">
                Status:
            </div>
            <div class="large-3 medium-3 small-6 columns">
                <asp:DropDownList ID="ddl_status" runat="server">
                    <asp:ListItem Selected="True" Value="N">---All ---</asp:ListItem>
                    <asp:ListItem>Requested</asp:ListItem>
                    <asp:ListItem>Freezed</asp:ListItem>
                    <asp:ListItem>Paid</asp:ListItem>
                    <asp:ListItem>Ticketed</asp:ListItem>
                    <asp:ListItem>Cancelled</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                    <asp:ListItem>Cancellation Requested</asp:ListItem>
                    <asp:ListItem>Refunded</asp:ListItem>
                    <asp:ListItem>Quoted</asp:ListItem>
                    <asp:ListItem>Accepted</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="large-2 medium-2 small-6 large-push-2 medium-push-2 columns">Request Id: </div>
            <div class="large-3 medium-3 small-6 large-push-2 medium-push-2 columns">
                <asp:TextBox ID="txt_RequestID" runat="server"></asp:TextBox>
            </div>
            <div class="clear1"></div>
            <div class="large-2 medium-2 small-6 columns">Date From :</div>
            <div class="large-3 medium-3 small-6 columns">
                <asp:TextBox ID="txt_fromDate" CssClass="date" runat="server"></asp:TextBox>
            </div>

            <div class="large-2 medium-2 small-6 large-push-2 medium-push-2 columns">Date To:</div>
            <div class="large-3 medium-3 small-6 large-push-2 medium-push-2 columns">
                <asp:TextBox ID="txt_todate" CssClass="date" runat="server"></asp:TextBox>
            </div>

            <div class="clear1"></div>
            <div class="large-2 medium-2 small-6 large-push-10 medium-push-10">
                <asp:Button ID="btn_submit" runat="server" Text="Search" CssClass="buttonfltbk"   OnClick="btn_submit_Click" />
            &nbsp;<asp:Button ID="btn_export" runat="server" Text="Export" CssClass="buttonfltbk"   OnClick="btn_export_Click"/>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div id="divReport" runat="server" class="large-12 medium-12 small-12" style="margin-top: 20px">
        <div id="DivExec" runat="server"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="GrpBookingDetails" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="GrpBookingDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                    PageSize="30" OnPageIndexChanging="GrpBookingDetails_PageIndexChanging" OnRowDataBound="GrpBookingDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="RequestID">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("RequestID") %>");'>
                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Name">
                            <ItemTemplate>
                                <asp:Label ID="lblBooking" runat="server" Text='<%#Eval("BookingName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip">
                            <ItemTemplate>
                                <asp:Label ID="lblTrip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip Type">
                            <ItemTemplate>
                                <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Passangers">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfPax" runat="server" Text='<%#Eval("NoOfPax")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expected Fare">
                            <ItemTemplate>
                                <asp:Label ID="lblExpectedPrice" runat="server" Text='<%#Eval("ExpactedPrice")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey">
                            <ItemTemplate>
                                <asp:Label ID="lblJourney" runat="server" Text='<%#Eval("Journey")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJourneydate" runat="server" Text='<%#Eval("JourneyDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Status">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentStatus" runat="server" Text='<%#Eval("PaymentStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="link_Refund" runat="server" CommandArgument='<%#Eval("RequestID") %>'
                                    Font-Bold="True" Font-Underline="False" OnClick="link_Refund_Click">RefundRequest</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_BookedPrice" runat="server" Text='<%#Eval("BookedPrice")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PgCharges">
                            <ItemTemplate>
                                <asp:Label ID="lblPgCharges" runat="server" Text='<%#Eval("PgCharges")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CancellationRemarks">
                            <ItemTemplate>
                                <asp:Label ID="lblCancellationRemarks" runat="server" Text='<%#Eval("CancellationRemarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="link_Invoice" runat="server" CommandArgument='<%#Eval("RequestID") %>'
                                    Font-Bold="True" Font-Underline="False" OnClick="link_Invoice_Click">View Invoice</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
        <div id="waitMessage" style="display: none;">
        <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; /* border: 5px solid #d1d1d1; */ /* border-radius: 10px; */">
            <div style="position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>
    </div>
        <%--<div id="waitMessage" style="display: none;">
            <div class="" style="text-align: center; opacity: 0.7; position: absolute; z-index: 99999; top: 10px; width: 100%; height: 100%; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; border-radius: 10px;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>--%>
    </div>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
    <script type="text/javascript">
        function openWindow(Requestid) {
            window.open('GroupRequestidDetails.aspx?RequestID=' + Requestid, 'open_window', ' width=1024, height=720, left=0, top=0 status=yes,toolbar=no,scrollbars=yes');
        }
    </script>
</asp:Content>

