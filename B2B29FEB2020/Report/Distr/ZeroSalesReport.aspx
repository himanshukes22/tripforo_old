<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="ZeroSalesReport.aspx.vb" Inherits="SprReports_Distr_ZeroSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input[type="text"], input[type="password"], select {
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
    <link href="../../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .txtBox {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }

        .txtCalander {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>

    <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
        <tr>
            <td>
                <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;" width="100%">
                    <tr>
                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold; padding-bottom: 15px;"
                            colspan="4">Zero Sales Report
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">From Date
                        </td>
                        <td width="130">
                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                style="width: 100px" />
                        </td>
                        <td width="90" style="font-weight: bold">To Date
                        </td>
                        <td>
                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="5px"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Agent Id
                        </td>
                        <td>
                            <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 150px" onfocus="focusObj(this);"
                                onblur="blurObj(this);"
                                defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                        </td>
                        <td colspan="2" align="right">
                            <asp:Button ID="btn_serach" runat="server" Text="Search" CssClass="button" />
                            &nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="Export" CssClass="button" />
                        </td>
                    </tr>

                    <tr>
                        <td style="color: #FF0000" colspan="4">* N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin: auto;">
        <tr>
            <td>
                <%--<asp:GridView ID="Grid_Distr" runat="server">
      </asp:GridView>--%>
                <asp:GridView ID="Grid_ZeroSales" runat="server" AutoGenerateColumns="False" PageSize="500"
                    CssClass="table table-hover" GridLines="None" Font-Size="12px">
                    <Columns>


                        <asp:TemplateField HeaderText="AGENCY&nbsp;NAME" SortExpression="AGENCYNAME">
                            <ItemTemplate>
                                <%#Eval("AGENCYNAME")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AGENCY&nbsp;ID" SortExpression="AGENTID">
                            <ItemTemplate>

                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("AGENTID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AGENTTYPE" HeaderText="AGENT&nbsp;TYPE" SortExpression="user_id" />
                        <asp:BoundField DataField="CREDITLIMIT" HeaderText="CREDIT&nbsp;LIMIT" SortExpression="Crd_Limit" />
                        <asp:BoundField DataField="REGISTRATIONDATE" HeaderText="REGISTRATION&nbsp;DATE" SortExpression="timestamp_create" />
                        <asp:BoundField DataField="TRANSACTIONDATE" HeaderText="TRANSACTION&nbsp;DATE " SortExpression="timestamp_create" />
                        <asp:BoundField DataField="MOBILENO" HeaderText="MOBILE&nbsp;NO" SortExpression="Mobile" />
                        <asp:BoundField DataField="EMAILID" HeaderText="EMAILID" SortExpression="Email" />
                        <asp:BoundField DataField="SALESID" HeaderText="SALESID" SortExpression="SalesExecID" />
                    </Columns>



                </asp:GridView>
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

