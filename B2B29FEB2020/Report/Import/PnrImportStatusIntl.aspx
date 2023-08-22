<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="PnrImportStatusIntl.aspx.vb" Inherits="Reports_Import_PnrImportStatusIntl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />



    <div class="divcls w70 auto">
        <table class="boxshadow w100">
            <tr>
                <td align="left" style="color: #fff; font-weight: bold; padding-top: 7px;" colspan="6">Search International Import Booking 
                </td>
            </tr>
            <tr>
                <td class="clear1" colspan="6"></td>
            </tr>
            <tr>
                <td width="90" style="font-weight: bold" height="25">From Date
                </td>
                <td width="130">

                    <input type="text" name="From" id="From" class="txtCalander"
                        readonly="readonly" style="width: 100px" />

                </td>
                <td width="70" style="font-weight: bold">&nbsp;&nbsp;To Date
                </td>
                <td width="110px">

                    <input type="text" name="To" id="To" class="txtCalander" readonly="readonly"
                        style="width: 100px" />
                </td>
                <td width="80" style="font-weight: bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        PNR
                </td>
                <td>
                    <asp:TextBox ID="txt_PNR" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>

            <tr>

                <td width="90" style="font-weight: bold" height="25px">OrderId
                                
                </td>
                <td width="130">

                    <asp:TextBox ID="txt_OrderId" runat="server" Width="100px"></asp:TextBox>

                </td>
                <td align="left" width="70" style="font-weight: bold">&nbsp;&nbsp; 
                                Airline
                </td>
                <td width="110px">
                    <asp:TextBox ID="txt_AirPNR" runat="server" Width="100px"></asp:TextBox>
                </td>

                <td id="td_Agency" runat="server" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="font-weight: bold" width="80px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agency</td>
                            <td align="left">
                                <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%><input
                                    type="text" id="txtAgencyName" name="txtAgencyName" style="width: 150px" onfocus="focusObj(this);"
                                    onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>



            <tr>
                <td colspan="6">

                    <td id="tr_ExecID" runat="server" colspan="6">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="font-weight: bold" width="90px" height="30">Exec ID :
                                </td>
                                <td align="left" width="130px">
                                    <asp:DropDownList ID="ddl_ExecID" runat="server" Width="105px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="font-weight: bold" width="70px">&nbsp;&nbsp; Status :
                                </td>
                                <td align="left" colspan="1">
                                    <asp:DropDownList ID="ddl_Status" runat="server" Width="105px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>




                </td>
            </tr>

            <tr>
                <td align="right" colspan="6">
                    <asp:Button ID="btn_export" runat="server" Text="Export" CssClass="buttonfltbks" ToolTip="Export by Date" />&nbsp;&nbsp;   
                    <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" />
                </td>
            </tr>

            <tr>
                <td style="color: #FF0000" colspan="6">* N.B: To get Today's booking without above parameter,do not fill any field,only click on search your booking.
                </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <table width="1000px" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridImportProxyDetail" runat="server" AutoGenerateColumns="False"
                                 CssClass="table table-hover" GridLines="None" Font-Size="12px" PageSize="30" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <a id="ancher" runat="server" rel="lyteframe" rev="width: 500px; height: 300px; overflow:hidden;"
                                                target="_blank" style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #800000; font-weight: bold;"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OrderId">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OrderId" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PNR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PnrNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AgentID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AgentID" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ag_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Ag_Name" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sector">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Depart" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alert Message">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AlertMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Status" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Exec" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ReqTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accepted Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AcceptedDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Updated Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UpTime" runat="server"></asp:Label>
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
