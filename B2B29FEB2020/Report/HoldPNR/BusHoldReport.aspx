<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="BusHoldReport.aspx.vb" Inherits="SprReports_HoldPNR_BusHoldReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
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

        <asp:UpdatePanel ID="BusholdPnr" runat="server">
            <ContentTemplate>

                <table cellspacing="10" cellpadding="0" border="0" class="tbltbl" width="950px">
                    <tr>
                        <td align="center" class="Heading" height="50px" style="font-size: 25px;">Bus Hold Request
                        </td>
                    </tr>
                    <tr>
                        <td id="td_Reject" runat="server" align="right" visible="false" style="padding: 10px; border: 2px solid #004b91;">
                            <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine" Height="60px" Width="350px"
                                BackColor="#FFFFCC"></asp:TextBox><br />
                            <br />
                            <asp:Button ID="btn_Comment" runat="server" Text="Comment" OnClientClick="return Validate();"
                                CssClass="buttonfltbk" />&nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="buttonfltbks" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">

                            <asp:GridView ID="GrdBusReport" runat="server" AutoGenerateColumns="False" PageSize="500"
                                CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                <Columns>
                                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <a href='../BS/TicketCopy.aspx?oid=<%#Eval("ORDERID")%>'
                                        rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                        style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                        color: #004b91">
                                     (Summary)
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Agentid" HeaderText="Agent&nbsp;Id" />

                                    <asp:BoundField DataField="Agency_Name" HeaderText="Agency&nbsp;Name" />
                                    <asp:TemplateField HeaderText="ORDERID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblORDERID" runat="server" Text='<%#Eval("ORDERID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="TRIPID" HeaderText="Trip&nbsp;Id" />
                                    <asp:BoundField DataField="SOURCE" HeaderText="Source" />
                                    <asp:BoundField DataField="DESTINATION" HeaderText="Destination" />
                                    <asp:BoundField DataField="BUSOPERATOR" HeaderText="Bus&nbsp;Operator" />
                                    <asp:BoundField DataField="SEATNO" HeaderText="Seat&nbsp;No" />
                                    <%-- <asp:BoundField DataField="FARE" HeaderText="Fare" />--%>
                                    <asp:TemplateField HeaderText="Fare">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFare" runat="server" Text='<%#Eval("Ta_Net_Fare")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="TICKETNO" HeaderText="Ticket&nbsp;No" />--%>
                                    <%--<asp:BoundField DataField="PNR" HeaderText="PNR" />--%>
                                    <asp:BoundField DataField="STATUS" HeaderText="Booking&nbsp;Status" />
                                    <asp:BoundField DataField="JOURNEYDATE" HeaderText="Journey&nbsp;Date" />
                                    <asp:BoundField DataField="CREATED_DATE" HeaderText="Booking&nbsp;Date" />
                                    <asp:TemplateField HeaderText="Accept">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="Accept" CommandArgument='<%#Eval("OrderId") %>'
                                                Font-Bold="True" Font-Underline="False">Confirm</asp:LinkButton>
                                            ||&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="LB_Reject" runat="server" CommandName="Reject" CommandArgument='<%#Eval("OrderId") %>'
                                        Font-Bold="True" Font-Underline="False" ForeColor="Red">Reject</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                              
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="BusholdPnr">
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
</asp:Content>
