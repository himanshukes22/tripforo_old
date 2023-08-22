<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="WinYatraRpt.aspx.vb" Inherits="WinYatraRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../../chosen/chosen.css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../chosen/jquery-1.6.1.min.js"></script>

    <script src="../../chosen/chosen.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>

    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <div class="divcls">
        <table>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0" style="margin: 0 227px;">
                        <tr>
                            <td valign="top" align="right">
                                <img src="../../images/box-tpr.jpg" width="10" height="10" />
                            </td>
                            <td style="background-position: top; background: url(../../images/box-tp.jpg) repeat-x left top;">
                            </td>
                            <td valign="top">
                                <img src="../../images/box-tpl.jpg" width="10" height="10" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-position: right top; background: url(../../images/boxl.jpg) repeat-y left bottom;
                                background-attachment: scroll;" align="right">
                            </td>
                            <td>
                                <table cellspacing="5" cellpadding="5" align="center" style="background: #fff;">
                                    <tr>
                                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold" id="titles"
                                            runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="83">
                                                        From Date
                                                    </td>
                                                    <td width="138">
                                                        <asp:TextBox ID="From" runat="server" Width="100px"></asp:TextBox>
                                                        <a href="javascript:cal1.popup();">
                                                            <img src="../../images/Cal.gif" alt="Click Here to Pick up the date" width="16" height="16"
                                                                border="0" align="middle" /></a>
                                                    </td>
                                                    <td width="56">
                                                        To Date
                                                    </td>
                                                    <td width="138">
                                                        <asp:TextBox ID="To" runat="server" Width="100px"></asp:TextBox>
                                                        <a href="javascript:cal2.popup();">
                                                            <img src="../../images/Cal.gif" alt="Click Here to Pick up the date" width="16" height="16"
                                                                border="0" align="middle" /></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" id="td_Agency" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="83">
                                                        Agency Name
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlAgencyName" data-placeholder="Choose Agency Name..." TabIndex="2"
                                                            runat="server" AutoPostBack="false" CssClass="chzn-select">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="120">
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #FF0000">
                                            * N.B: To get all booking without above parameter,do not fill any field,<br />
                                            &nbsp; only click on search your booking.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 10px; height: 10px; background: url(../../images/boxr.jpg) repeat-y left bottom;">
                            </td>
                        </tr>
                        <tr>
                            <td height="10" valign="top" class="style2">
                                <img src="../../images/box-bl.jpg" width="10" height="10" />
                            </td>
                            <td style="background: url(../../images/box-bottom.jpg) repeat-x left bottom;" height="10">
                            </td>
                            <td valign="top">
                                <img src="../../images/box-br.jpg" width="10" height="10" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table cellspacing="0" cellpadding="0" style="background: #fff; width: 1155px;">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        
                                        <asp:GridView ID="RptGrd" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px"
                                            AllowPaging="True" PageSize="25">
                                            <Columns>
                                                <asp:TemplateField HeaderText="INVOICE ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="InvoiceID" runat="server" Text='<%#Eval("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AIRLINE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAirLine" runat="server" Text='<%#Eval("AirLine") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FLIGHT NUMBER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlightNo" runat="server" Text='<%#Eval("FltNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TICKET NUMBER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTktNo" runat="server" Text='<%#Eval("TktNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SECTOR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="USER ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AGENCY NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAgency" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PNR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpnr" runat="server" Text='<%#Eval("PNR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DEPARTURE DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeptDate" runat="server" Text='<%#Eval("DepartDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BASIC FARE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBFare" runat="server" Text='<%#Eval("BaseFare") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YQ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYQ" runat="server" Text='<%#Eval("YQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AIRPORT CHARGE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAirPort" runat="server" Text='<%#Eval("AirPort") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TRANSACTION FEES">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsrvcharge" runat="server" Text='<%#Eval("TransFee") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DISCOUNT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDis" runat="server" Text='<%#Eval("Dis") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SPECIAL DISCOUNT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSFDis" runat="server" Text='<%#Eval("SFDis") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TDS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTDS" runat="server" Text='<%#Eval("TDS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TKT REC AMT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTktRecAmt" runat="server" Text='<%#Eval("TotalDis") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAX NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaxFname" runat="server" Text='<%#Eval("PaxName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAX TYPE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaxType" runat="server" Text='<%#Eval("PaxType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBMIT DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubmit" runat="server" Text='<%#Eval("BookingDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PRS DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrs" runat="server" Text='<%#Eval("BookingDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TRIP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTrip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BSP STK">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBspStk" runat="server" Text="N"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="XO BILL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblXO" runat="server" Text="B"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PURCHASE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurchase" runat="server" Text="P"></asp:Label>
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
                                <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var cal1 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_From']);
        cal1.year_scroll = true;
        cal1.time_comp = true;
        var cal2 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_To']);
        cal2.year_scroll = true;
        cal2.time_comp = true;	
    </script>

</asp:Content>
