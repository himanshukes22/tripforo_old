<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="QCTicketReport.aspx.vb" Inherits="SprReports_Admin_QCTicketReport" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <%-- <link href="../../CSS/foundation.min.css" rel="stylesheet" />
    <link href="../../CSS/foundation.css" rel="stylesheet" /--%>>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <script type="text/javascript">

        function validate() {
            if (confirm("Are you sure you want to change status to hold!")) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    <style>
        .tooltip1 {
            position: relative;
           /* display: inline-block;
            border-bottom: 1px dotted black;*/
        }

            .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                /* Position the tooltip */
                position: absolute;
                z-index: 1;
            }

            .tooltip1:hover .tooltiptext {
                visibility: visible;
            }
    </style>





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

    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
            <uc1:LeftMenu runat="server" ID="LeftMenu" />
        </div>

        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
            <tr>
                <td>
                    <table cellspacing="3" cellpadding="3" align="center" style="background: #fff;">
                        <tr>
                            <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">Search Booking Details</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="90" style="font-weight: bold" height="25">From Date
                                        </td>
                                        <td width="130">
                                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                                style="width: 100px" />
                                        </td>
                                        <td width="70" style="font-weight: bold">&nbsp;&nbsp;To Date
                                        </td>
                                        <td width="110px">
                                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                        </td>
                                        <td width="80" style="font-weight: bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PNR
                                        </td>
                                        <td width="120">
                                            <asp:TextBox ID="txt_PNR" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" width="80" style="font-weight: bold">&nbsp;&nbsp; OrderId
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_OrderId" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="90" style="font-weight: bold" height="25">Pax Name
                                        </td>
                                        <td width="130" class="style4">
                                            <asp:TextBox ID="txt_PaxName" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" width="70" style="font-weight: bold" class="style4">&nbsp;&nbsp;Ticket No
                                        </td>
                                        <td class="style4" width="110px">
                                            <asp:TextBox ID="txt_TktNo" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <%--  <td align="left" width="80" style="font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Airline
                                    </td>
                                    <td width="120px">
                                        <asp:TextBox ID="txt_AirPNR" runat="server" Width="100px"></asp:TextBox>
                                    </td>--%>
                                        <td width="80">&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td id="td_Agency" runat="server" colspan="1" width="380">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="90" style="font-weight: bold">Agency Name
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
                                        <td align="right">
                                            <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" Width="150px" />&nbsp;&nbsp;<asp:Button
                                                ID="btn_export" runat="server" CssClass="button" Text="Export" Width="150px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #FF0000" colspan="4">* N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </br>
    <div align="center">
        <table width="1000px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <table cellspacing="5" cellpadding="0" border="0" align="center" class="tbltbl">
                        <tr>
                            <td style="font-size: 12px; font-weight: bold; color: #004b91; height: 25px; padding-right: 15px;"
                                align="left">
                                <div style="float: left;">
                                    Total Ticket Sale :
                                    <asp:Label ID="lbl_Total" runat="server"></asp:Label>
                                </div>
                                <div style="float: right; margin-left: 40px;">
                                    Total Ticket Issued :
                                    <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Booking">
                                        <ItemTemplate>

                                            <a href='Update_BookingOrder.aspx?OrderId=<%#Eval("OrderId")%> &TransID='
                                                rel="lyteframe" rev="width: 1200px; height: 500px; overflow:hidden;" target="_blank"
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">Booking&nbsp;Details
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax&nbsp;Type">
                                        <ItemTemplate>
                                            <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax&nbsp;ID">
                                        <ItemTemplate>
                                            <a href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                                rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order&nbsp;ID">
                                        <ItemTemplate>
                                            <a href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"
                                                rev="width: 900px; height: 500px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pnr">
                                        <ItemTemplate>
                                            <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticket&nbsp;No">
                                        <ItemTemplate>
                                            <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent&nbsp;ID">
                                        <ItemTemplate>
                                            <asp:Label ID="AgentID" runat="server" Text='<%#Eval("AgentId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agency&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                                        <ItemTemplate>
                                            <asp:Label ID="AgencyName" runat="server" Text='<%#Eval("AgencyName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excutive&nbsp;ID">
                                        <ItemTemplate>
                                            <asp:Label ID="ExcutiveID" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AirLine">
                                        <ItemTemplate>
                                            <asp:Label ID="Airline" runat="server" Text='<%#Eval("VC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Sector" DataField="sector"></asp:BoundField>
                                    <asp:BoundField HeaderText="Net&nbsp;Fare" DataField="TotalAfterDis">
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                                    <%--<asp:BoundField HeaderText="Modify Status"  DataField="MORDIFYSTATUS"></asp:BoundField>--%>

                                    <asp:TemplateField HeaderText="Modify Status">
                                        <ItemTemplate>



                                            <div class="tooltip1">
                                                <%#Eval("MORDIFYSTATUS")%>
                                                <span class="tooltiptext"><%#Eval("msg")%></span>
                                            </div>
                                            <%--<asp:Label ID="PaxType" runat="server" Text='<%#Eval("MORDIFYSTATUS")%>' title='<%#Eval("msg")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Trip" DataField="Trip"></asp:BoundField>
                                    <asp:BoundField HeaderText="Booking&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" DataField="CreateDate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Partner Name" DataField="PartnerName"></asp:BoundField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="LB_CahngeStatus" runat="server" CommandName="ChangeStatus" CommandArgument='<%#Eval("OrderId")%>' Visible='<%# IsVisible(Eval("Status"))%>'
                                                Font-Bold="True" Font-Underline="False" ForeColor="Red" OnClientClick="javascript:return validate();">Change Status to Hold</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                              
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
    </div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>

