<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CancellationReportMass.aspx.vb" Inherits="SprReports_Refund_CancellationReport" MasterPageFile="~/MasterForHome.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%-- <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }
    </style>

    <div class="mtop80"></div>
    <div class="row">
        <div class="col-md-12 text-center search-text  ">
            Mass Refund Reports
        </div>
    </div>
    <div class="row ">
        <div class="col-md-9 col-xs-12 col-md-push-1">
            <div class="form-inlines">
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" name="From" id="From" placeholder="From Date" class="form-control" readonly="readonly" />
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" name="To" placeholder="To Date" id="To" class="form-control" readonly="readonly" />
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_PNR" placeholder="PNR" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_OrderId" placeholder="OrderId" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_PaxName" placeholder="Pax Name" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_TktNo" placeholder="TicketNo" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_AirPNR" placeholder="Airline" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-groups col-md-3 col-xs-12" id="tdTripNonExec2" runat="server">
                    <asp:DropDownList class="form-control" ID="ddlTripRefunDomIntl" runat="server">
                        <asp:ListItem Value="">-----Select-----</asp:ListItem>
                        <asp:ListItem Value="D">Domestic</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-groups col-md-3 col-xs-3">
                    <asp:DropDownList ID="ddl_Status" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>

                <div id="td_Agency" runat="server" class="form-groups col-md-3 col-xs-12">
                    <div id="tr_ExecID" runat="server" class="w100">
                        <div class="form-groups col-md-6 col-xs-6">
                            <asp:DropDownList class="form-control" ID="ddl_ExecID" runat="server">
                            </asp:DropDownList>
                        </div>


                    </div>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" class="form-control col-md-6 col-xs-6" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
                <div class="form-groups" id="tdTripNonExec1" runat="server"></div>
            </div>
        </div>
        <div class="col-md-2 col-xs-12 col-md-push-1">
            <asp:Button ID="btn_result" runat="server" class="buttonfltbks" Text="Search Result" />
            <asp:Button ID="btn_export" runat="server" class="buttonfltbk" Text="Export" />
        </div>
        <div class="row" style="padding: 10px 10px 10px 10px;">
            <div class="col-md-9 col-md-push-1 col-xs-12">
                <div style="color: #FF0000">
                    * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                </div>
            </div>

        </div>
    </div>

    <div class="w95 lft" id="divReport" runat="server" visible="false">
        <%-- style="overflow: scroll; max-height: 220px;padding:5px;"--%>
        <%--<table cellspacing="0" cellpadding="0" border="0" style="background: #fff; width: 1000px;"
                align="center">
                <tr>
                    <td>--%>
        <%--<div>--%>
        <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd_report" runat="server" AutoGenerateColumns="False" CssClass="table table-hover msi" GridLines="None" Font-Size="12px" AllowPaging="true" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="Credit Node">
                            <ItemTemplate>
                                <%-- <a target="_blank" href="../Accounts/CreditNodeDomDetailsMass.aspx?PNRNO=<%#Eval("pnr_locator")%>">
                                    <asp:Label ID="lblRefundID" runat="server" Text='<%#Eval("pnr_locator")%>' ForeColor="#004b91"
                                        Font-Bold="True" Font-Underline="True"></asp:Label></a>--%>
                                <a target="_blank" href="../Accounts/CreditNodeDomDetailsMass.aspx?PNRNO=<%#Eval("pnr_locator")%>">
                                    <asp:Label ID="lblRefundID" runat="server" Text='<%#Eval("RefundID")%>' ForeColor="#004b91"
                                        Font-Bold="True" Font-Underline="True"></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer ID">
                            <ItemTemplate>
                                <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agency Name">
                            <ItemTemplate>
                                <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Type" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label>&nbsp;<asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Pax LastName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Airline">
                            <ItemTemplate>
                                <asp:Label ID="lblVC" runat="server" Text='<%#Eval("VC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector">
                            <ItemTemplate>
                                <asp:Label ID="lbldestination" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Departure Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("departure_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T Fare">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFares")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fare After Discount">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscounts")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancellation Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblcharge" runat="server" Text='<%#Eval("CancellationCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sevice Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblsrvcharge" runat="server" Text='<%#Eval("ServiceCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refunded Fare">
                            <ItemTemplate>
                                <asp:Label ID="lblrefund" runat="server" Text='<%#Eval("RefundFare") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblcancel" runat="server" Text='<%#Eval("RegardingCancel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec ID">
                            <ItemTemplate>
                                <asp:Label ID="lblexecutive" runat="server" Text='<%#Eval("ExecutiveID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec Rejected Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblRejectComment" runat="server" Text='<%#Eval("RejectComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec Updated Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblUpComment" runat="server" Text='<%#Eval("UpdateRemark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refund Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("SubmitDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldateA" runat="server" Text='<%#Eval("AcceptDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldateU" runat="server" Text='<%#Eval("UpdateDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CancelStatus" HeaderText="Cancel Status" />
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PgMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PG Charges">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Charges" runat="server" Text='<%#Eval("PgCharges") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
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
        <%--</div>--%>
        <%--</td>
                </tr>
            </table>--%>
    </div>


    <%--<script type="text/javascript">
        var cal1 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_From']);
        cal1.year_scroll = true;
        cal1.time_comp = true;
        var cal2 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_To']);
        cal2.year_scroll = true;
        cal2.time_comp = true;	
    </script>--%>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
