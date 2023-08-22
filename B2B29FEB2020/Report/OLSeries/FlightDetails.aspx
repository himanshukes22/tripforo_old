<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="FlightDetails.aspx.vb" Inherits="SprReports_OLSeries_FlightDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script src="../../Utility/JS/jquery.simplemodal.js" type="text/javascript"></script>

    <%--<script src="../../Utility/JS/jquery.js" type="text/javascript"></script>--%>
    <link href="../../Utility/css/basic.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../Utility/css/basic.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Utility/css/basic_ie.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Utility/css/demo.css" rel="stylesheet" type="text/css" />--%>

    <script type='text/javascript'>

        function openDialog() {

            $(function() {
                //                alert('hi');
                $('#basic-modal-content').modal();
                return false;

            });
        }
        function closeDialog() {
            $(function() {
                $('.simplemodal-close').trigger('click');
                $.modal.close();
                //                $.model.destroy();
            });
        }
    </script>

    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <div>
        <table width="100%" style="padding-bottom: 5px">
            <tr>
                <td align="center" class="Heading">
                    Series Departure
                </td>
            </tr>
        </table>
        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
            <tr>
                <td>
                    <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;">
                        <tr>
                            <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                                Search Series Departure
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="90" style="font-weight: bold" height="25">
                                            From Date
                                        </td>
                                        <td width="130">
                                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                                style="width: 100px" />
                                        </td>
                                        <td width="90" style="font-weight: bold">
                                            To Date
                                        </td>
                                        <td width="110px">
                                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                        </td>
                                        <td style="font-weight: bold">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td id="Td1" width="90" style="font-weight: bold" height="25" runat="server">
                                            Airline
                                        </td>
                                        <td id="Td2" width="130" runat="server">
                                            <asp:DropDownList ID="ddl_Airline" runat="server" Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td id="agencyrow" runat="server" colspan="1" width="380">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="90" style="font-weight: bold">
                                                        Sector
                                                    </td>
                                                    <td align="left">
                                                        <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddl_sector" runat="server" Width="100px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </br>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grd_flight" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    Width="98%" Font-Size="11px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Counter" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lblairline" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available Seats">
                                            <ItemTemplate>
                                                <asp:Label ID="lblavlseats" runat="server" Text='<%#Eval("Available_Seat") %>'></asp:Label>
                                                <asp:Label ID="lbl_indicator" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hold Seats">
                                            <ItemTemplate>
                                                <asp:Label ID="lblholdseats" runat="server" Text='<%#Eval("Hold_Seat")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Seats">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsoldseats" runat="server" Text='<%#Eval("Confirm_Seat")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Itinerary" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaircode" runat="server" Text='<%#Eval("Airline_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Itinerary">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbItinerary" runat="server" Text="View Itinerary" CommandName="getItinerary">                                        
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%--                                                <asp:Button ID="btn_book" runat="server" CommandName="Book" Text="BOOK" CommandArgument='<%#Eval("Counter") %>' />
--%>
                                                <asp:LinkButton ID="lnk" runat="server" Style="font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 12px; font-weight: bold; color: #004b91">
<a href='FlightQueryForm.aspx?Counter=<%#Eval("Counter")%> '
                                                rel="lyteframe" rev="width: 950px; height: 400px; overflow:hidden;" 
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;
                                                color: #004b91; text-decoration: underline;" >BookNow</a>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" Font-Overline="False" Font-Size="11px" Font-Underline="False"
                                        Height="35px" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#C6ECC6" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="basic-modal-content" style="padding: 20px; overflow: auto; height: 180px;
        width: 600px;">
        <table width="100%">
            <tr>
                <td valign="middle" style="height: 150px; font-family: arial, Helvetica, sans-serif;
                    font-size: 11px; color: #000000;">
                    <asp:Label ID="lblShowItnry" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function txtairline_onclick() {

        }

// ]]>
    </script>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
