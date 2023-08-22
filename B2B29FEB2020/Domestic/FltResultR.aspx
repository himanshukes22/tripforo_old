<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="FltResultR.aspx.vb" Inherits="FlightDom_FltResultR" %>
<%@ Register Src="~/UserControl/FltSearch.ascx" TagName="IBESearch" TagPrefix="Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/flightIntlRes.css" type="text/css" />
    <link rel="stylesheet" href="../css/main2.css" type="text/css" />

    <script type="text/javascript" src="../js/fareinfo.js"></script>

    <%--<script type="text/javascript" src="../js/chrome.js"></script>--%>
    
    <asp:UpdatePanel ID="fltupdpanel" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" align="center" style="background: #fff;
                width: 950px; padding: 20px; margin: 0 auto;">
                <tr>
                    <td width="10" height="10" valign="top">
                        <img src="../images/box-tpr.jpg" width="10" height="10" />
                    </td>
                    <td style="background: url(../images/box-tp.jpg) repeat-x left bottom;" height="10">
                    </td>
                    <td valign="top">
                        <img src="../images/box-tpl.jpg" width="10" height="10" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px; height: 10px; background: url(../images/boxl.jpg) repeat-y left bottom;">
                    </td>
                    <td style="padding: 10px; background: #fff;">
                        <table style="background: #fff; width: 950px; margin: auto;" align="center">
                            <%-- <tr>
                                <td align="left" colspan="2" style="clear: both; background: #f2f4f4; padding-left: 10px;
                                    font-weight: bold; height: 20px; line-height: 25px;">
                                   
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 100%; border: 1px solid #eee;">
                                    <div id="tabs" style="width: 950px">
                                        <ul>
                                            <li><a href="#tabs-1">Current Search</a></li>
                                            <li><a href="#tabs-2">modify Search</a></li>
                                            <li><a href="#tabs-3">Airline Matrix</a></li>
                                            <li><a href="#tabs-4">Selected OutBound Details</a></li>
                                        </ul>
                                        <div id="tabs-1">
                                        </div>
                                        <div id="tabs-2" style="width: 100%; border: 1px solid #eee;">
                                            <search:ibesearch id="IBE_CP" runat="server" />
                                        </div>
                                        <div id="tabs-3" style="width: 100%; border: 1px solid #eee;">
                                            <asp:PlaceHolder ID="mtrxPL" runat="server"></asp:PlaceHolder>
                                        </div>
                                        <div id="tabs-4" style="width: 100%; border: 1px solid #eee;">
                                            <%--<div id="divOutBound" runat="server">
                                            </div>--%>
                                            <div id="divOBDetails" runat="server" style="width: 800px; border: 1px #f2f4f4 solid;">
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <tr>
                        <td valign="top" align="left">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="background: #fff; width: 950px; margin: auto;" align="center">
                                        <tr>
                                            <td align="left" style="width: 700px;" valign="top">
                                                <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                    <tr>
                                                        <td align="left" style="clear: both; background: #f2f4f4; padding-left: 10px; font-weight: bold;
                                                            height: 25px; line-height: 25px;">
                                                            <asp:RadioButtonList ID="sortRbdlist" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
                                                                AutoPostBack="true" CellPadding="10" CellSpacing="10">
                                                                <asp:ListItem Value="ASC" Text="Fare" Selected="True"> </asp:ListItem>
                                                                <asp:ListItem Value="" Text="Airline"> </asp:ListItem>
                                                                <asp:ListItem Value="" Text="Dep. Time"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <div style="width: 720px; margin: auto; background: #fff;">
                                                                <asp:Xml ID="xmlRes" runat="server" TransformSource="Multicity.xsl"></asp:Xml>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" style="padding: 8px; width: 200px; border: 1px solid #eee; border-radius: 10px;
                                                -webkit-border-radius: 10px; -o-border-radius: 10px; -moz-border-radius: 10px;"
                                                rowspan="2">
                                                <div style="clear: both; color: #2a0d37; text-align: center; font-weight: bold; height: 25px;">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label></div>
                                                <div style="clear: both; background: #f2f4f4; padding-left: 10px; font-weight: bold;
                                                    height: 25px; line-height: 25px;">
                                                    Filter By Airline</div>
                                                <div id="div2" runat="server" style="float: left; padding-left: 10px">
                                                    <asp:CheckBoxList ID="chkair" runat="server" CellPadding="10" CellSpacing="10" RepeatDirection="Vertical"
                                                        RepeatLayout="Table" TextAlign="Right" AutoPostBack="true">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
                                        updating your results....<br />
                                        <br />
                                        <img alt="loading" src="../images/load.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10px; height: 10px; background: url(../images/boxr.jpg) repeat-y left bottom;">
                    </td>
                </tr>
                <tr>
                    <td width="10" height="10" valign="top">
                        <img src="../images/box-bl.jpg" width="10" height="10" />
                    </td>
                    <td style="background: url(../images/box-bottom.jpg) repeat-x left bottom;" height="10">
                    </td>
                    <td valign="top">
                        <img src="../images/box-br.jpg" width="10" height="10" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var formObj = document.getElementById("aspnetForm");

            if (!formObj.onsubmit || (formObj.onsubmit() != false)) {

                formObj.__EVENTTARGET.value = eventTarget;

                formObj.__EVENTARGUMENT.value = eventArgument;

                formObj.submit();

            }
        }
    </script>

    <script type="text/javascript">
        function SetHiddenVariable(st) {
            var jsVar;
            if (st == 0)
            { jsVar = '0-Stop'; }
            else if (st == 1)
            { jsVar = '1-Stop'; }
            else if (st == 2)
            { jsVar = '2-Stop'; }
            else
            { jsVar = st; }
            __doPostBack('callPostBack', jsVar);
        }
        function SetHiddenVariable1(st) {
            var jsVar = st + "";
            __doPostBack('callPostBack', jsVar);
        }
    </script>

    <div id="divfareDetails" class="frdiv" style="display: none">
    </div>
    <script type="text/javascript">
    var queryObj = {};
    // 
    queryObj["TripType"]='<%=Session("SearchQuery")("TripType")%>';
    queryObj["txtDepCity1"]='<%=Session("SearchQuery")("Destination")%>';
    queryObj["hidtxtDepCity1"]='<%=Session("SearchQuery")("hidDestination")%>';
    queryObj["txtArrCity1"]='<%=Session("SearchQuery")("Origin")%>';
    queryObj["hidtxtArrCity1"]='<%=Session("SearchQuery")("hidOrigin")%>';
    queryObj["txtDepDate"]='<%=Session("SearchQuery")("retDate")%>';
    queryObj["txtRetDate"]='<%=Session("SearchQuery")("Depdate")%>';
    queryObj["Adult"]='<%=Session("SearchQuery")("Adult")%>';
    queryObj["Child"]='<%=Session("SearchQuery")("Child")%>';
    queryObj["Infant"]='<%=Session("SearchQuery")("Infant")%>';
    queryObj["Cabin"]='<%=Session("SearchQuery")("Cabin")%>';
    queryObj["txtAirline"]='<%=Session("SearchQuery")("Airline")%>';
    queryObj["hidtxtAirline"]='<%=Session("SearchQuery")("hidAirline")%>';
    queryObj["NStop"]='<%=Session("SearchQuery")("NStop")%>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/SearchResult.js") %>"></script>
</asp:Content>
