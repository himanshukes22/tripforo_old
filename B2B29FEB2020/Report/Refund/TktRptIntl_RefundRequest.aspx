<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="TktRptIntl_RefundRequest.aspx.vb" Inherits="Reports_Refund_TktRptIntl_RefundRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function Validate() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_Acept_grdview_ctl02_txtRemark").value == "") {

                alert('Remark can not be blank,Please Fill Remark');
                document.getElementById("ctl00_ContentPlaceHolder1_Acept_grdview_ctl02_txtRemark").focus();
                return false;
            }
            if (confirm("Are you sure you want to Reject!"))
                return true;
            return false;
        }
    </script>
    <div class="w80 auto">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h2 style="color: #000">International Refund Request Details</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="Acept_grdview" runat="server" AllowPaging="False" AllowSorting="True"
                        AutoGenerateColumns="False"  OnRowCommand="RowCommand"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
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
                                    <asp:Label ID="TktNo" runat="server" Text='<%#Eval("Tkt_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Airline" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="SECTOR" DataField="Sector"></asp:BoundField>
                            <asp:TemplateField HeaderText="Total Fare">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFare") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Net Fare" DataField="TotalFareAfterDiscount">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:BoundField>
                            <%--                                        <asp:BoundField HeaderText="Booking Date" DataField="Booking_date"></asp:BoundField>--%>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>

                            <asp:BoundField HeaderText="Request Date" DataField="SubmitDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Journey Date" DataField="Journey_Date"></asp:BoundField>
                            <asp:BoundField HeaderText="Partner Name" DataField="PartnerName"></asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkreissue" runat="server" ForeColor="#004b91" Font-Strikeout="False"
                                        Font-Overline="False" Font-Size="11px" CommandArgument='<%#Eval("Counter") %>'
                                        CommandName="Accept" Text="Accept"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkreject" runat="server" ForeColor="Red" Font-Strikeout="False"
                                        Font-Overline="False" Font-Size="11px" CommandArgument='<%#Eval("Counter") %>'
                                        CommandName="Reject" Text="Reject"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reject Remark">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" Height="47px" TextMode="MultiLine" Width="250px"
                                        Visible="false" MaxLength="500"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="btnCanFee_Click" Visible="false" OnClientClick="return Validate();"
                                        CommandArgument='<%# Eval("Counter") %>' CommandName="submit"><img src="../../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                    <asp:LinkButton ID="lnkHides" runat="server" OnClick="lnkHides_Click" Visible="false"
                                        CommandName="lnkHides" CommandArgument='<%# Eval("Counter") %>'><img src="../../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Customer Remarks" DataField="RegardingCancel"></asp:BoundField>
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
