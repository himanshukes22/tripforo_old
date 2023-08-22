<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="LeadPaxInfo.aspx.vb" Inherits="SprReports_LeadPaxInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
        <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../css/main2.css" rel="stylesheet" type="text/css" />

     <div class="divcls">
        <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl"
            width="80%">
            <tr>
                <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                    Pax Booking Report
                </td>
            </tr>
            
            
            <tr>
                <td>
                    <table>
                        <tr>
                            <td id="td_agency" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="90px" style="font-weight: bold">
                                            Agency Name
                                        </td>
                                        <td align="left" width="260px">
                                            &nbsp;&nbsp;&nbsp;
                                            <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" />
                </td>
            </tr>
            
        </table>
        <br />
        <div align="center">
            <table border="0" cellpadding="0" cellspacing="0">
               
                <tr>
                    <td>
                        <asp:UpdatePanel ID="fltupdpanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gridpaxinfo" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    Width="100%" BackColor="White" AllowPaging="True" PageSize="50">
                                    <Columns>
                                    
                                       
                                        <asp:BoundField HeaderText="Refrence No" DataField="ReferenceNo"></asp:BoundField>
                                        <asp:BoundField HeaderText="Name" DataField="Name"></asp:BoundField>
                                        <asp:BoundField HeaderText="Age" DataField="Age"></asp:BoundField>
                                         <asp:BoundField HeaderText="Mobile" DataField="PASSENGERMOBILE"></asp:BoundField>
                                        <asp:BoundField HeaderText="ID Type" DataField="IdType"></asp:BoundField>
                                        <asp:BoundField HeaderText="ID Number" DataField="IdNumber"></asp:BoundField>
                                        <asp:BoundField HeaderText="Agent ID" DataField="AgentID"></asp:BoundField>
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
                        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="fltupdpanel">
                            <ProgressTemplate>
                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                    padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                    z-index: 1000;">
                                </div>
                                <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                    z-index: 1001; background-color: #fff; border: solid 1px #000;">
                                    Please wait....<br />
                                    <br />
                                    <img alt="loading" src="../images/load.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
    </div>

     <script type="text/javascript">
         var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

    <script src="<%= ResolveUrl("~/Rail/JS/IRCautocomplete.js") %>" type="text/javascript"></script>
</asp:Content>

