<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="GroupPayment.aspx.cs" Inherits="GroupSearch_GroupPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <div class="main" runat="server" id="Div_Agent_FinalBookingDetails" visible="false">
        <table id="tblMainGroupFrame_final" width="100%" cellspacing="0" cellpadding="10" style="width: 100%; border-collapse: collapse;" border="1">
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td>Trip :
                    <asp:Label ID="lbltrip" runat="server" Font-Bold="True"></asp:Label></td>
                <td>Trip Type :
                    <asp:Label ID="lbltriptyp" runat="server" Font-Bold="True"></asp:Label></td>
                <td>Refrance ID :
                    <asp:Label ID="lbl_requestid" runat="server" Font-Bold="True"></asp:Label></td>
                <td>User ID :
                    <asp:Label ID="lbl_userid" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="center" style="font-size: 20px">Payable Amount :
                    <asp:Label ID="lbl_bookingfare" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="#0033CC"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr style="display: none" id="tr_ttlbooking">
                <td id="tdQuotePrice" runat="server">Booking Price :<asp:Label ID="lbl_tdQuotePrice" runat="server"></asp:Label></td>
                <td id="tdpgcharge" runat="server">PG Charges :
                    <asp:Label ID="lbl_tdpgcharge" runat="server"></asp:Label></td>
                <td id="tdtotal" runat="server">Total Booking Price :
                    <asp:Label ID="lbl_tdtotal" runat="server"></asp:Label></td>
                <td>
                    <input id="hdnbookprice" type="hidden" runat="server" />
                    <input id="hdntotal" type="hidden" runat="server" />
                    <input id="hdnPGCharge" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="finalBookingGrid" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" EnableViewState="false"
                                PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Trip From*" FooterStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbDepLoc" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                            <asp:Label ID="lbl_DepAirportName" runat="server" Text='<%#Eval("Departure_Location")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trip To*">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbArvlLoc" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                            <asp:Label ID="lbl_ArvlAirportName" runat="server" Text='<%#Eval("Arrival_Location")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Date*">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbDepdate" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbDeptime" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arrival Date*">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbArvltime" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbArvlTime" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Airline">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbAirline" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flight No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fbflgno" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_gbdcounter" runat="server" Text='<%#Eval("GbdCounter")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_fgdcounter" runat="server" Text='<%#Eval("GfdCounter")%>' Visible="false"></asp:Label>
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
            <tr>
                <th style="width: 147px">No Of Adult*
                </th>
                <th>No Of Child*
                </th>
                <th>No Of Infant*
                </th>
                <th>Expected Fare*
                </th>
                <th colspan="1">Remarks
                </th>
            </tr>
            <tr>
                <td style="width: 147px">
                    <asp:Label ID="FBtxt_totAdt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="FBtxt_totchd" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="FBtxt_totinf" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="FBtxt_fare" runat="server"></asp:Label>
                </td>
                <td colspan="1">
                    <asp:Label ID="FBtxt_remarks" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="display: none"></td>
            </tr>
            <tr id="tr_pgmode" runat="server">
                <td colspan="5" align="left">
                    <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="WL">Wallet</asp:ListItem>
                        <asp:ListItem Value="OPTCRDC">Credit Card</asp:ListItem>
                        <asp:ListItem Value="OPTDBCRD">Debit Card</asp:ListItem>
                        <asp:ListItem Value="OPTNBK">Net Banking</asp:ListItem>
                        <asp:ListItem Value="OPTCASHC">Cash Card</asp:ListItem>
                        <asp:ListItem Value="OPTMOBP">Mobile Payments</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Button ID="btn_payment" runat="server" OnClick="btn_payment_Click" Text="Pay" CssClass="loader" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
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
            <div class="" style="text-align: center; opacity: 0.7; position: absolute; z-index: 99999; top: 10px; width: 100%; height: 100%; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000;  border-radius: 10px;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>--%>
    <script type="text/javascript">
        $(".loader").click(function (e) {
            $("#waitMessage").show();
        });
    </script>
    <script type="text/javascript">
        $("#ctl00_ContentPlaceHolder1_rblPaymentMode").click(function () {
            var value = $('#ctl00_ContentPlaceHolder1_rblPaymentMode input:checked').val();
            if (value == "WL") {
                $("#tr_ttlbooking").hide();
            }
            else {
                $("#tr_ttlbooking").show();
            }
            GetPgTransCharge();
        });
        function GetPgTransCharge() {
            var checked_radio = $("[id*=ctl00_ContentPlaceHolder1_rblPaymentMode] input:checked");
            var PaymentMode = checked_radio.val();
            var BookingAmt = 0;
            var PGcharges = 0;
            var TotalAmt = 0;
            var HdnQuotePrice = 0;
            HdnQuotePrice = $("#ctl00_ContentPlaceHolder1_hdnbookprice").val();
            if (PaymentMode != "WL") {
                $.ajax({
                    type: "POST",
                    url: "GroupPayment.aspx/GetPgChargeByMode",
                    data: '{paymode: "' + PaymentMode + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "") {
                            if (data.d.indexOf("~") > 0) {
                                var pgCharge = data.d.split('~')[0]
                                var chargeType = data.d.split('~')[1]
                                if (chargeType == "F") {
                                    PGcharges = parseFloat(pgCharge).toFixed(2);
                                    TotalAmt = (parseFloat(HdnQuotePrice) + parseFloat(PGcharges)).toFixed(2);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdpgcharge').html(PGcharges);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdtotal').html(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdQuotePrice').html(HdnQuotePrice);
                                    $('#ctl00_ContentPlaceHolder1_lbl_bookingfare').html(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_hdntotal').val(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_hdnPGCharge').val(PGcharges);
                                }
                                else {
                                    //calculate pg charge Percentage of InBound
                                    PGcharges = ((parseFloat(HdnQuotePrice) * parseFloat(pgCharge)) / 100).toFixed(2);
                                    TotalAmt = (parseFloat(HdnQuotePrice) + parseFloat(PGcharges)).toFixed(2);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdpgcharge').html(PGcharges);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdtotal').html(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_lbl_tdQuotePrice').html(HdnQuotePrice);
                                    $('#ctl00_ContentPlaceHolder1_lbl_bookingfare').html(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_hdntotal').val(TotalAmt);
                                    $('#ctl00_ContentPlaceHolder1_hdnPGCharge').val(PGcharges);
                                }
                            }
                        }
                        else {
                            alert("try again");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            }
            else {
                PGcharges = "0.0000";
                TotalAmt = (parseFloat(HdnQuotePrice) + parseFloat(PGcharges)).toFixed(2);
                $('#ctl00_ContentPlaceHolder1_lbl_tdpgcharge').html(PGcharges);
                $('#ctl00_ContentPlaceHolder1_lbl_tdtotal').html(TotalAmt);
                $('#ctl00_ContentPlaceHolder1_lbl_tdQuotePrice').html(HdnQuotePrice);
                $('#ctl00_ContentPlaceHolder1_lbl_bookingfare').html(TotalAmt);
                $('#ctl00_ContentPlaceHolder1_hdntotal').val(TotalAmt);
                $('#ctl00_ContentPlaceHolder1_hdnPGCharge').val(PGcharges);
            }
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Invalid user id,please login with valid userid!!");
                    window.opener.location.reload('GroupDetails.aspx')
                    window.close();
                }
            }
        }
    </script>
</asp:Content>

