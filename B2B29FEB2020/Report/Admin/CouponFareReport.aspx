<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="CouponFareReport.aspx.vb" Inherits="SprReports_Admin_CouponFareReport"
    Title="Coupon Fare Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
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
    <style>
        input[type="text"], input[type="password"], select
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset style="border: solid thin #000; width: 50%; padding: 20px;">
                    <legend style="font-size: 15px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;
                        color: #004b91;">Coupon Fare Report</legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr class="rowstyle">
                            <td align="left" style="padding-bottom: 10px">
                                &nbsp; From Date:
                            </td>
                            <td align="left">
                                <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                    style="width: 100px" />
                            </td>
                            <td align="left">
                                &nbsp;&nbsp; To Date:
                            </td>
                            <td colspan="2" align="left">
                                <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                            </td>
                        </tr>
                        <tr class="rowstyle">
                            <td>
                                &nbsp;Enter TrackID:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_tid" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td align="center">
                                Select Report Status:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="dropdown" Width="120px">
                                    <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="false" Value="false"></asp:ListItem>
                                    <asp:ListItem Text="Booked" Value="Booked"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btn_submit" runat="server" Text="Search" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="color:Green; font-size:14px; font-weight:bold">
                            Total Profit &nbsp;:&nbsp;<asp:Label ID="lblprft" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdCoupon" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                                <Columns>
                                    <asp:BoundField HeaderText="PNR" DataField="Cou_PNR" />
                                    <asp:BoundField HeaderText="Flight No" DataField="Flight_No" />
                                    <asp:BoundField HeaderText="Source" DataField="Source" />
                                    <asp:BoundField HeaderText="Destination" DataField="Destination" />
                                    <asp:BoundField HeaderText="Departure Date" DataField="Departure_date" />
                                    <asp:BoundField HeaderText="Adult Fare" DataField="Adult_fare" />
                                    <asp:BoundField HeaderText="Child Fare" DataField="Child_fare" />
                                    <asp:BoundField HeaderText="Infant Fare" DataField="Infant_fare" />
                                    <asp:BoundField HeaderText="Total Fare" DataField="Total_fare" />
                                    <asp:BoundField HeaderText="Portal Fare" DataField="Portal_fare" />
                                    <asp:TemplateField HeaderText="Profit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfarediff" runat="server" Text='<%#Eval("profit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Departure Time" DataField="flt_Depart" />
                                    <asp:BoundField HeaderText="Arrival Time" DataField="flt_Arr" />
                                    <asp:BoundField HeaderText="Via" DataField="Via" />
                                    <asp:BoundField HeaderText="TrackID" DataField="Track_ID" />
                                    <asp:BoundField HeaderText="Created Date" DataField="Created_date" />
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
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
