<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="IntlHoldPNRRequest.aspx.vb" Inherits="Reports_HoldPNR_IntlHoldPNRRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <%--<link href="Styles/tables.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function Validate() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Reject").value == "") {

                alert('Please Fill Remark');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Reject").focus();
                return false;


            }
            if (confirm("Are you sure you want to Reject!"))
                return true;
            return false;
        }
    </script>

    <div align="center">
        <table cellspacing="5" cellpadding="0" border="0" class="tbltbl" width="950px">
            <tr>
                <td align="center" class="Heading" height="50px" style="font-size: 30px;">Hold PNR Request
                </td>
            </tr>
            <tr>
                <td id="td_Reject" runat="server" align="right" visible="false" style="padding: 10px; border: 2px solid #004b91;">
                    <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine" Height="60px" Width="350px"
                        BackColor="#FFFFCC"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="btn_Comment" runat="server" Text="Comment" OnClientClick="return Validate();"
                        CssClass="buttonfltbks" />&nbsp;&nbsp;
                                <asp:Button ID="btn_Cancel" runat="server" Text="cancel" CssClass="buttonfltbk" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <%--<asp:GridView ID="GridProxyDetail" runat="server">
        
        
        </asp:GridView>--%>
                    <asp:GridView ID="GridHoldPNRAccept" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="OrderId">
                                <ItemTemplate>
                                    <a id="ancher" href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%>' target="_blank"
                                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; font-weight: bold;">
                                        <asp:Label ID="lbl_OrderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>(View)</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AgentId">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_AgentID" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PNR">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PNR" runat="server" Text='<%#Eval("GdsPnr") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Sector" runat="server" Text='<%#Eval("sector") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Duration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TripType">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TripType" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trip">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Trip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TourCode">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TourCode" runat="server" Text='<%#Eval("TourCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalBookingCost">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TotalBookingCost" runat="server" Text='<%#Eval("TotalBookingCost") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalAfterDis">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TotalAfterDis" runat="server" Text='<%#Eval("TotalAfterDis") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pax Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PgFName" runat="server" Text='<%#Eval("PgFName") %>'></asp:Label>
                                    <asp:Label ID="lbl_PgLName" runat="server" Text='<%#Eval("PgLName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PgMobile">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PgMobile" runat="server" Text='<%#Eval("PgMobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PgEmail">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PgEmail" runat="server" Text='<%#Eval("PgEmail") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CreateDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_CreateDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accept">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="Accept" CommandArgument='<%#Eval("OrderId") %>'
                                        Font-Bold="True" Font-Underline="False">Accept</asp:LinkButton>
                                    ||&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LB_Reject" runat="server" CommandName="Reject" CommandArgument='<%#Eval("OrderId") %>'
                                                    Font-Bold="True" Font-Underline="False" ForeColor="Red">Refund</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
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
