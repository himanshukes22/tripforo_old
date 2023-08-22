<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="SeriesFlightReport.aspx.vb" Inherits="SprReports_OLSeries_SeriesFlightReport" %>

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
                                    <td width="90" style="font-weight: bold" height="25" visible="false" runat="server">
                                        Airline
                                    </td>
                                    <td width="130" visible="false" runat="server">
                                        <input type="text" id="txtairline" name="txtairline" value="" style="width: 100px" />
                                    </td>
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
                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />&nbsp;&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #FF0000" colspan="4">
                            * N.B: To get Today&#39;s Reoprt without above parameter,do not fill any field, only 
                            click on search Result</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div align="center">
        <table width="1000px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:GridView ID="grd_seriesreport" runat="server" AutoGenerateColumns="false" 
                        CssClass="GridViewStyle" Width="100%">
                        <Columns>
                           
                        <asp:TemplateField HeaderText="AgentID">
                                <ItemTemplate>
                                   <%-- <asp:Label ID="lblagentid" runat="server" Text='<%#Eval("Agent_ID") %>'></asp:Label>--%>
                                 <a href='../Admin/Update_Agent.aspx?AgentID=<%#Eval("Agent_ID") %> '
                                                rel="lyteframe" rev="width: 850px; height: 400px; overflow:hidden;" 
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                color: #004b91; text-decoration: underline;" ><asp:Label ID="lblagentid" runat="server" Text='<%#Eval("Agent_ID") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="AgencyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgencyName" runat="server" Text='<%#Eval("Agency_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactPersonName" runat="server" Text='<%#Eval("ContactPerson_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactNo" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactEmailID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactEmail_Id" runat="server" Text='<%#Eval("ContactEmail_Id")%>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Itinerary">
                                <ItemTemplate>
                                    <asp:Label ID="lblaircode" runat="server" Text='<%#Eval("Airline_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Departure Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Pax">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotpax" runat="server" Text='<%#Eval("NoOfPax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adult">
                                <ItemTemplate>
                                    <asp:Label ID="lbladt" runat="server" Text='<%#Eval("NoOfadult") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Child">
                                <ItemTemplate>
                                    <asp:Label ID="lblchd" runat="server" Text='<%#Eval("NoOfChild") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Infant">
                                <ItemTemplate>
                                    <asp:Label ID="lblinf" runat="server" Text='<%#Eval("NoOfInfant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblremark" runat="server" Text='<%#Eval("Agent_Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rejected Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblexecremark" runat="server" Text='<%#Eval("Rejected_Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
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
