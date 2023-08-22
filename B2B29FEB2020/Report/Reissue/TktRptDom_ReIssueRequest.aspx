<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="TktRptDom_ReIssueRequest.aspx.vb" Inherits="Reports_Reissue_TktRptDom_ReIssueRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function Validate() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_ticket_grdview_ctl02_txtRemark").value == "") {
                alert('Remark can not be blank,Please Fill Remark');
                document.getElementById("ctl00_ContentPlaceHolder1_ticket_grdview_ctl02_txtRemark").focus();
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
                    <h2 style="color: #000">Reissue Request Details</h2>
                </td>
            </tr>
            <tr>
                <td class="clear1"></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="False" AllowSorting="True"
                        AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                        <Columns>
                            <asp:TemplateField HeaderText="Agent ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agency Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order ID">
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
                                    <asp:Label ID="lblpnr" runat="server" Text='<%#Eval("pnr_locator") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket No">
                                <ItemTemplate>
                                    <asp:Label ID="lbltktno" runat="server" Text='<%#Eval("Tkt_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Airline" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="SECTOR" DataField="Sector"></asp:BoundField>
                            <asp:TemplateField HeaderText="Total Fare">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFare") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Fare">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Request Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookdate" runat="server" Text='<%#Eval("Booking_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                    <asp:TextBox ID="txtRemark" runat="server" Height="47px" TextMode="MultiLine"
                                        Width="200px" Visible="false" MaxLength="500"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="btnSubmit_Click" Visible="false" OnClientClick="return Validate();"
                                        CommandArgument='<%# Eval("Counter") %>' CommandName="submit"><img src="../../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                    <asp:LinkButton ID="lnkHides" runat="server" OnClick="lnkHides_Click" Visible="false"
                                        CommandName="lnkHides" CommandArgument='<%# Eval("Counter") %>'><img src="../../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblremarks" runat="server" Text='<%#Eval("RegardingIssue")%>'></asp:Label>
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
