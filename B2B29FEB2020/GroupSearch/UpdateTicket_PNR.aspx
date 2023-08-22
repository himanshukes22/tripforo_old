<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="UpdateTicket_PNR.aspx.cs" Inherits="GroupSearch_UpdateTicket_PNR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script language="javascript" type="text/javascript">
        function focusObj(obj) {
            if (obj.value == "Airline PNR") obj.value = "";
        }
        function blurObj(obj) {
            if (obj.value == "") obj.value = "Airline PNR";
        }
        function focusObj1(obj) {
            if (obj.value == "GDS PNR") obj.value = "";
        }
        function blurObj1(obj) {
            if (obj.value == "") obj.value = "GDS PNR";
        }
        function focusObj2(obj) {
            if (obj.value == "Ticket No.") obj.value = "";
        }
        function blurObj2(obj) {
            if (obj.value == "") obj.value = "Ticket No.";
        }
    </script>
    <script language="javascript" type="text/javascript">
        function Validate() {
            if ((document.getElementById("ctl00_ContentPlaceHolder1_txt_gdspnr").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txt_gdspnr").value == "GDS PNR")
                || (document.getElementById("ctl00_ContentPlaceHolder1_txt_INGDS").value == "")) {
                alert('GDS PNR can not be blank,Please fill the gds pnr name');
                return false;
            }
            if ((document.getElementById("ctl00_ContentPlaceHolder1_txt_airlinepnr").value == "") || (document.getElementById("ctl00_ContentPlaceHolder1_txt_INAirLine").value == "Airline PNR")) {
                alert('Airline PNR can not be blank or 0,Please fill airline pnr');
                return false;
            }
            $("#waitMessage").show();
        }
    </script>
    <div class="large-12 medium-12 small-12">
        <div id="Div_Exec" runat="server">
            <div></div>
            <div id="div_flightdetails" runat="server">
                <table border="1">
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #014c90;'>Flight Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="FlightDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="100">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeparture_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeparture_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeparture_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div></div>
            <div id="div1" runat="server">
                <table border="1">
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #014c90;'>Booking Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="BookingDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Trip Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adult">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdultCount" runat="server" CssClass="" Text='<%#Eval("AdultCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Child">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChildCount" runat="server" Text='<%#Eval("ChildCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Infant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInfantCount" runat="server" Text='<%#Eval("InfantCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExpectedPrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpactedPrice" runat="server" Text='<%#Eval("ExpactedPrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartnerparice" runat="server" Text='<%#Eval("BookedPrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_status" runat="server" Text='<%#Eval("PaymentStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div></div>
        </div>
        <div id="div_BookingDetails" runat="server">
            <table border="1" width="100%">
                <tr>
                    <td style='font-size: 13px; width: 15%; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #014c90;'>Booking Details</td>
                </tr>
                <tr>
                    <td style='font-size: 13px; width: 15%; text-align: center;'>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="BookingDetails111" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                    PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trip Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adult">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdultCount" runat="server" CssClass="" Text='<%#Eval("AdultCount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Child">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChildCount" runat="server" Text='<%#Eval("ChildCount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Infant">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfantCount" runat="server" Text='<%#Eval("InfantCount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ExpactedPrice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpactedPrice" runat="server" Text='<%#Eval("ExpactedPrice")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Partner Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpartnerparice" runat="server" Text='<%#Eval("BookedPrice")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Eval("PaymentStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#014c90" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divpnr" runat="server">



            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td colspan="2">Pnr Details:
                                </td>
                            </tr>
                            <tr>
                                <td>Airline PNR :<asp:TextBox ID="txt_airlinepnr" onfocus="focusObj(this);" onblur="blurObj(this);" Text="Airline PNR" MaxLength="8" onkeypress="return checkit(event)" runat="server"></asp:TextBox>
                                    &nbsp;
                                </td>
                                <td>GDS PNR : 
                        <asp:TextBox ID="txt_gdspnr" MaxLength="8" onfocus="focusObj1(this);" onblur="blurObj1(this);" Text="GDS PNR" onkeypress="return checkit(event)" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table width="100%" id="tbl_inboundpnr" runat="server" visible="false">
                            <tr>
                                <td>InBound Pnr Details:
                                </td>
                            </tr>
                            <tr>
                                <td>Airline PNR :<asp:TextBox ID="txt_INAirLine" MaxLength="8" onfocus="focusObj(this);" onblur="blurObj(this);" Text="Airline PNR" onkeypress="return checkit(event)" runat="server"></asp:TextBox>
                                    &nbsp;
                                </td>
                                <td>GDS PNR : 
                        <asp:TextBox ID="txt_INGDS" MaxLength="8" onfocus="focusObj1(this);" onblur="blurObj1(this);" Text="GDS PNR" onkeypress="return checkit(event)" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>



        </div>
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>Pax info with Ticket No:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="UpdateTicket" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Title" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_title" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                                        <asp:Label ID="lbl_counter" runat="server" Text='<%#Eval("Counter")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pax Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ticket Number">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Ticket" MaxLength="10" onkeypress="return checkit(event)" onfocus="focusObj2(this);" onblur="blurObj2(this);" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:TextBox>
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
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%" id="tbl_Inboundpaxinfo" runat="server" visible="false">
                        <tr>
                            <td>InBound Pax info with Ticket No:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="InbounTicketUpdate" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Title" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_title" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                                        <asp:Label ID="lbl_counter" runat="server" Text='<%#Eval("Counter")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pax Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ticket Number">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Ticket" MaxLength="10" onkeypress="return checkit(event)" onfocus="focusObj2(this);" onblur="blurObj2(this);" CssClass="ticket" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:TextBox>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
        </div>

        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_update" runat="server" Text="Update"  CssClass="buttonfltbk" OnClick="btn_update_Click" OnClientClick="return Validate();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="waitMessage" style="display: none;">
        <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000;">
            <div style="position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58 || charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Ticket has been updated successfully !!");
                    window.opener.location.reload('ExecRequestDetails.aspx');
                    window.close();
                }
                    break;
                case 2: {
                    alert("Ticket has been Updated Successfully, but E-mailID not found !!");
                    window.opener.location.reload('ExecRequestDetails.aspx');
                    window.close();
                }
                    break;
            }
        }
    </script>
</asp:Content>

