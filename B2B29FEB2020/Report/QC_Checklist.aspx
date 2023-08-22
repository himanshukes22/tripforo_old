<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="QC_Checklist.aspx.vb" Inherits="SprReports_QC_Checklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
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
            background-image: url(../images/cal.gif);
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
                            Search QC Checklist Details</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold" height="25">
                                        From Date
                                    </td>
                                    <td width="130">
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                            style="width: 100px" />
                                    </td>
                                    <td width="70" style="font-weight: bold">
                                        &nbsp;&nbsp;To Date
                                    </td>
                                    <td width="110px">
                                        <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="80" style="font-weight: bold">
                                        OrderId
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_OrderId" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td align="left" width="80" style="font-weight: bold">
                                        &nbsp;&nbsp; ExecutiveId
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="DdlExecId" runat="server" Width="150px">
                                       
                                       </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />
                           &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btn_export" runat="server" Text="Export" CssClass="button" />
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #FF0000" colspan="4">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
    <table width="70%">
        <tr>
            <td align="center">
            
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                <asp:GridView ID="GvQcChecklist" runat="server" AutoGenerateColumns="false" Width="100%"
                    CssClass="GridViewStyle" AllowPaging="True" PageSize="30" >
                    <Columns>
                        <asp:TemplateField HeaderText="Order Id">
                            <ItemTemplate>
                             <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"
                                                rev="width: 900px; height: 500px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: 12px; font-weight: bold; color: #004b91">
                                                <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>(Details)</a>
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Table Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTableName" runat="server" Text='<%#Eval("TableName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Executive Id">
                            <ItemTemplate>
                                <asp:Label ID="lblExecutiveId" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                              <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>'></asp:Label>
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
                    <%--<asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                    </asp:UpdateProgress>--%>
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
