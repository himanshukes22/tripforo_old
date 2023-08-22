<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="Grp_PendingRefund.aspx.cs" Inherits="GroupSearch_Exec_Grp_PendingRefund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <div align="center">
        <table cellspacing="10" cellpadding="0" border="0" class="tbltbl" width="950px">
            <tr>
                <asp:Label ID="lbl_Norecord" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GridRefundRequest" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" PageSize="30" OnRowCommand="GridRefundRequest_RowCommand" OnPageIndexChanging="GridRefundRequest_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SNO" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group RequestID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Group_RequestID" runat="server" Text='<%#Eval("RequestedID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Request ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_RequestID" runat="server" Text='<%#Eval("RefRequestedID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trip">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Trip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trip Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Triptype" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Journey">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_journey" runat="server" Text='<%#Eval("Journey") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Journey Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doj" runat="server" Text='<%#Eval("JouneryDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Passanges">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NOP" runat="server" Text='<%#Eval("TotalPax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking Price">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_bookingprice" runat="server" Text='<%#Eval("BookingFare") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PaymentStatus" runat="server" Text='<%#Eval("CancelStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancellation Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EXCEremarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Pmode" runat="server" Text='<%#Eval("PaymentMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PG Charges">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_pgcharge" runat="server" Text='<%#Eval("PGCharges") %>'></asp:Label>
                                    <asp:Label ID="lbl_agentid" Visible="false" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="ReqUpdate" CommandArgument='<%#Eval("RequestedID") %>'
                                        Font-Bold="True" Font-Underline="False">Refund</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reject Remark">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" Visible="false" runat="server" Height="47px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubmit_1" runat="server" Visible="false"
                                        CommandArgument='<%# Eval("RequestedID") %>' CommandName="GRPREFUNDUPDATE"><img src="../../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                    <asp:LinkButton ID="lnkHides_1" runat="server" Visible="false"
                                        CommandName="GRP_CANCEL" CommandArgument='<%# Eval("RequestedID") %>'><img src="../../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" Height="50px" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>