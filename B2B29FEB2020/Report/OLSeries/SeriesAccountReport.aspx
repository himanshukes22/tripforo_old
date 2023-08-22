<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="SeriesAccountReport.aspx.vb" Inherits="SprReports_OLSeries_SeriesAccountReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
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
    <div>
        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
            <tr>
                <td>
                    <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;">
                        <tr>
                            <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                                Search Series Departure Report
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td id="agencyrow" runat="server" colspan="1" width="380">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="90" style="font-weight: bold">
                                                        Agency Name
                                                    </td>
                                                    <td align="left">
                                                        <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%>
                                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
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
                                          <%--  <asp:Button
                                                ID="btn_export" runat="server" CssClass="button" Text="Export" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #FF0000" colspan="4">
                                * N.B: To get Today&#39;s Reoprt without above parameter,do not fill any field, only 
                                click on search Result.
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <div align="center">
            <table width="1000px" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:GridView ID="grd_SeriesAccreport" runat="server" AutoGenerateColumns="false"
                            HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdRow" CssClass="GridViewStyle"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Agency Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblairline" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblaircode" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Account ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Executive_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Executive Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Executive_Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrejectedremark" runat="server" Text='<%#Eval("AccountReject_Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Requested Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Created_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Updated Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotpax" runat="server" Text='<%#Eval("Updated_Date") %>'></asp:Label>
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
        </div>

        <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
        </script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>
