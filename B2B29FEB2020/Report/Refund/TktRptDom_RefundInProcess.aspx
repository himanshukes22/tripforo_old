<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="TktRptDom_RefundInProcess.aspx.vb" Inherits="Reports_Refund_TktRptDom_RefundInProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function Validate() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_InProccess_grdview_ctl02_txtRemark").value == "") {

                alert('Remark can not be blank,Please Fill Remark');
                document.getElementById("ctl00_ContentPlaceHolder1_InProccess_grdview_ctl02_txtRemark").focus();
                return false;
            }
            if (confirm("Are you sure you want to Reject!"))
                return true;
            return false;
        }
    </script>
    <div class="w100">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <h2 style="color: #000">Accepted Refund Details</h2>
                </td>
            </tr>
            <tr>
                <td class="clear1"></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="InProccess_grdview" runat="server" AllowPaging="False" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                        <Columns>
                            <asp:TemplateField HeaderText="Agent ID">
                                <ItemTemplate>
                                    <asp:Label ID="AgentID" runat="server" Text='<%#Eval("UserID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Agency Name" DataField="Agency_Name"></asp:BoundField>
                            <asp:TemplateField HeaderText="Order Id">
                                <ItemTemplate>
                                    <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pax ID">
                                <ItemTemplate>
                                    <asp:Label ID="TID" runat="server" Text='<%#Eval("Counter")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pax Type">
                                <ItemTemplate>
                                    <asp:Label ID="PaxType" runat="server" Text='<%#Eval("pax_type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pax Name">
                                <ItemTemplate>
                                    <asp:Label ID="PaxName" runat="server" Text='<%#(Eval("Title").ToString()+" " + Eval("pax_fname").ToString()+" " + Eval("pax_lname").ToString()).ToUpper()%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PNR">
                                <ItemTemplate>
                                    <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("pnr_locator")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" Text='<%#Eval("Tkt_No") %>' ForeColor="Red"
                                        Font-Bold="true" Font-Size="11px" CommandName="lnkupdate" CommandArgument='<%#Eval("Counter")%>'
                                        OnClick="lnkupdate_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Airline" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="SECTOR" DataField="Sector"></asp:BoundField>
                            <asp:TemplateField HeaderText="Total Fare">
                                <ItemTemplate>
                                    <a id="ancher1" href='TktRptIntl_RefundFair.aspx?Counter=<%#Eval("Counter") %>' onclick="wopen('', 'popup',400, 300); return false;"
                                        rel="lyteframe" rev="width: 300px; height: 210px; overflow:hidden;" style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: Red; font-weight: bold;">
                                        <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFare") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Net Fare" DataField="TotalFareAfterDiscount">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Journey Date" DataField="Journey_Date"></asp:BoundField>
                            <asp:BoundField HeaderText="Partner Name" DataField="PartnerName"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="Request Date" DataField="SubmitDate"></asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnlreject" runat="server" Text="Reject" ForeColor="Red" CommandName="reject"
                                        CommandArgument='<%#Eval("Counter") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reject Remark">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" Height="47px" TextMode="MultiLine" Width="200px"
                                        Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="btnCanFee_Click" OnClientClick="return Validate();" Visible="false"
                                        CommandArgument='<%# Eval("Counter") %>' CommandName="submit"><img src="../../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                    <asp:LinkButton ID="lnkHides" runat="server" OnClick="lnkHides_Click" Visible="false"
                                        CommandName="lnkHides" CommandArgument='<%# Eval("Counter") %>'><img src="../../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Remarks" ControlStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegardingCancel" runat="server" Text='<%#Eval("RegardingCancel")%>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PaymentMode">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PG Charges">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Charges" runat="server" Text='<%#Eval("PgCharges") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
