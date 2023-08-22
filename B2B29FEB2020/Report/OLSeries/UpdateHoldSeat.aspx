<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="UpdateHoldSeat.aspx.vb" Inherits="SprReports_OLSeries_UpdateHoldSeat" EnableEventValidation="false" ValidateRequest="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <div align="center">
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
         <table width="1000px" border="0" cellpadding="0" cellspacing="0">
            <tr>
         <td class="Heading" align="center">
         Update Offline Hold Seats
         </td>
         </tr>
            <tr>
            
                <td align="center">
                    <asp:GridView ID="grd_Updateholdseat" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="Counter" CssClass="GridViewStyle" Width="100%" ForeColor="#333333">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblcounter" runat="server" Text='<%#Eval("Counter")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Executive Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblExecid" runat="server" Text='<%#Eval("Executive_ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Airline Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblairlinename" runat="server" Text='<%#Eval("AirlineName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Itinerary">
                                <ItemTemplate>
                                    <asp:Label ID="lblairlinecode" runat="server" Text='<%#Eval("Airline_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector">
                                <ItemTemplate>
                                    <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Availiable Seat">
                                <ItemTemplate>
                                    <asp:Label ID="lblavlseat" runat="server" Text='<%#Eval("Available_Seat")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Offline Hold">
                                <ItemTemplate>
                                    <asp:Label ID="lblofflinehold" runat="server" Text='<%#Eval("Offline_Hold")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Offline Confirm">
                                <ItemTemplate>
                                    <asp:Label ID="lblofflineconfirm" runat="server" Text='<%#Eval("Offline_Confirm")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dep Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldepdate" runat="server" Text='<%#Eval("Depart_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ret Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblseriesid" runat="server" Text='<%#Eval("SeriesID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Enter no of Seats" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtconfirmholdseat" runat="server" Visible="false" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update Hold Seat">
                                <ItemTemplate>
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CommandName="Confirm" CssClass="newbutton_2" />
                                    <asp:Button ID="btnRelease" runat="server" Text="Release" CommandName="Release" CssClass="newbutton_2"/>
                                    <asp:Button ID="btnConfirmUpdate" runat="server" Text="Update" CommandName="ConfirmUpdate" Visible="false" CssClass="newbutton_2" />
                                    <asp:Button ID="btnConfirmCancel" runat="server" Text="Cancel" CommandName="ConfirmCancel" Visible="false" CssClass="newbutton_2" />
                                    <asp:Button ID="btnReleaseUpdate" runat="server" Text="Update" CommandName="ReleaseUpdate" Visible="false" CssClass="newbutton_2"/>
                                    <asp:Button ID="btnReleaseCancel" runat="server" Text="Cancel" CommandName="ReleaseCancel" Visible="false" CssClass="newbutton_2" />
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
                </td>
                
                
            </tr>
        </table>
         </ContentTemplate>
                    </asp:UpdatePanel>
                     <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                        <ProgressTemplate>
                            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                z-index: 1000;">
                            </div>
                            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                font-weight: bold; color: #000000">
                                Please Wait....<br />
                                <br />
                                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                <br />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
        
    </div>
</asp:Content>
