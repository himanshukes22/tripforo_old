<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="LccRResult.aspx.vb" Inherits="LccRF_LccRResult"  validaterequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/css/main2.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/css/LccRF.css") %>" rel="stylesheet" type="text/css" />
    <div style="width: 970px; margin: auto;">
        <table cellspacing="10" cellpadding="0" class="maintbl">
            <%--<tr>
                <td colspan="2" class="item">
                    
                </td>
            </tr>--%>
            <tr>
                <td height="30" class="maintd" style="border: 1px solid #CCCCCC;">
                    <asp:Label ID="DepLCCHdr" runat="server"></asp:Label>
                </td>
                <td height="30" class="maintd" style="border: 1px solid #CCCCCC;">
                    <asp:Label ID="RetLCCHdr" runat="server"></asp:Label>
                </td>
                <td valign="top" style="width: 15%;" rowspan="2">
                    <div id="floatdiv" style="width:160px;">
                        <div id="lccfltdtls">
                            <table cellspacing="5" cellpadding="10" style="margin: auto; border: 1px solid #ccc;
                                background: #eee; width: 100%; vertical-align: top;">
                                <tr>
                                    <td style="border: 1px solid #ccc; background: #eee; border-radius: 10px; -moz-border-radius: 10px;
                                        -webkit-border-radius: 10px; -o-border-radius: 10px; width: 20%; text-align: center;"
                                        class="hdr">
                                        <div id="totFarelbl" runat="server">
                                        </div>
                                        <%--<div>
                                            <a id="farebrekup" runat="server" class="frule">Fare Breakup</a></div>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <div id="fltlblO" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <div id="fltlblR" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <a id="book" href="lccCheckOut.aspx?LinD=1&LinR=1" onclick="return LccIsBookable();">
                                            <img src="../Images/book.gif" alt="" /></a><br />
                                        <div id="isBookableMsg">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" style="border-right: 1px solid #CCCCCC; border-left: 1px solid #ccc;
                    width: 43%;">
                    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tbl" id="LCCResOTbl">
                        <asp:Repeater ID="LCCResO" runat="server">
                            <HeaderTemplate>
                                <tr class="tr">
                                    <td width="104" height="25" class="hdr">
                                        &nbsp;
                                    </td>
                                    <td width="115" height="25" class="hdr">
                                        &nbsp;
                                    </td>
                                    <td width="81" height="25" class="hdr">
                                        Departs
                                    </td>
                                    <td width="79" height="25" class="hdr">
                                        Arrives
                                    </td>
                                    <td width="89" height="25" class="hdr">
                                        Price
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="tr" id="OWTR" runat="server">
                                    <td style="height: 35px; line-height: 35px;">
                                        <table width="90%" border="0" cellspacing="1" cellpadding="1" style="float: right;">
                                            <tr>
                                                <td style="width: 23%; height: 35px; line-height: 35px; padding-left: 5px;">
                                                    <input id="deplcc1" runat="server" type="radio" />
                                                    <%-- <asp:RadioButton ID="deplcc1" runat="server" GroupName="DEP" />--%>
                                                </td>
                                                <td style="width: 77%; height: 35px; line-height: 20px; text-align: center;">
                                                    <div>
                                                        <asp:Image ID="airlogoO" runat="server" /></div>
                                                    <div>
                                                        <asp:Label ID="airlineO" runat="server"></asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <a id="depfltdtls" class="hyprlnk" runat="server">Details+</a></div>
                                        <div>
                                        </div>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <asp:Label ID="departsO" runat="server"></asp:Label></div>
                                        <div>
                                        </div>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <asp:Label ID="arrivesO" runat="server"></asp:Label></div>
                                        <div>
                                            <a href="javascript:" onmouseover="return lccFRRule();"
                                                onmouseout="HideContent('uniquename3'); return true;" class="frule">fare Rule</a></div>
                                    </td>
                                    <td style="height: 35px; line-height: 20px;">
                                        <div class="prc">
                                            <a id="prcO" runat="server"></a>
                                        </div>
                                        <div class="refnd">
                                            <asp:Label ID="faretypeO" runat="server"></asp:Label>
                                            <input type="hidden" id="fltdO" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <input type="hidden" id="selecteddep" name="selecteddep" runat="server" value="" />
                </td>
                <td valign="top" style="width: 42%; border-right: 1px solid #ccc; border-left: 1px solid #ccc;
                    border-bottom: 1px solid #ccc;">
                    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tbl" id="LCCResRTbl">
                        <asp:Repeater ID="LCCResR" runat="server">
                            <HeaderTemplate>
                                <tr class="tr">
                                    <td width="104" height="25" class="hdr">
                                        &nbsp;
                                    </td>
                                    <td width="115" height="25" class="hdr">
                                        &nbsp;
                                    </td>
                                    <td width="81" height="25" class="hdr">
                                        Departs
                                    </td>
                                    <td width="79" height="25" class="hdr">
                                        Arrives
                                    </td>
                                    <td width="89" height="25" class="hdr">
                                        Price
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="tr" id="RTTR" runat="server">
                                    <td style="height: 35px; line-height: 35px;">
                                        <table width="90%" border="0" cellspacing="1" cellpadding="1" style="float: right;">
                                            <tr>
                                                <td width="23%" style="padding-left: 5px;">
                                                    <%-- <asp:RadioButton ID="retlcc1" runat="server" GroupName="RET" />--%>
                                                    <input id="retlcc1" runat="server" name="ret" type="radio" />
                                                </td>
                                                <td style="line-height: 20px; width: 77%; text-align: center;">
                                                    <div>
                                                        <asp:Image ID="airlogoR" runat="server" /></div>
                                                    <div>
                                                        <asp:Label ID="airlineR" runat="server"></asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <a id="retfltdtls" class="hyprlnk" runat="server">Details+</a></div>
                                        <div>
                                        </div>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <asp:Label ID="departsR" runat="server"></asp:Label></div>
                                        <div>
                                        </div>
                                    </td>
                                    <td class="item" valign="top">
                                        <div>
                                            <asp:Label ID="arrivesR" runat="server"></asp:Label></div>
                                        <div>
                                            <a href="javascript:" onmouseover="return lccFRRule();"
                                                onmouseout="HideContent('uniquename3'); return true;" class="frule">fare Rule</a></div>
                                    </td>
                                    <td style="height: 35px; line-height: 20px;">
                                        <div class="prc">
                                            <a id="prcR" runat="server"></a>
                                        </div>
                                        <div class="refnd">
                                            <asp:Label ID="faretypeR" runat="server"></asp:Label><input type="hidden" id="fltdR"
                                                runat="server" /></div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <input type="hidden" id="selectedret" name="selectedret" runat="server" value="" />
                </td>
            </tr>
        </table>
    </div>
    <div id="uniquename3" class="lccfrdiv" style="display: none">
    </div>

    <script type="text/javascript" src="<%=ResolveUrl("~/js/LccRF.js") %>"></script>
<script type="text/javascript">
    var ns = (navigator.appName.indexOf("Netscape") != -1);
    var d = document;
    var px = document.layers ? "" : "px";
    function JSFX_FloatDiv(id, sx, sy) {
        var el = d.getElementById ? d.getElementById(id) : d.all ? d.all[id] : d.layers[id];
        window[id + "_obj"] = el;
        if (d.layers) el.style = el;
        el.cx = el.sx = sx; el.cy = el.sy = sy;
        el.sP = function(x, y) { this.style.left = x + px; this.style.top = y + px; };
        el.flt = function() {
            var pX, pY;
            pX = (this.sx >= 0) ? 0 : ns ? innerWidth :
    document.documentElement && document.documentElement.clientWidth ?
    document.documentElement.clientWidth : document.body.clientWidth;
            pY = ns ? pageYOffset : document.documentElement &&
        document.documentElement.scrollTop ?
    document.documentElement.scrollTop : document.body.scrollTop;
            if (this.sy < 0)
                pY += ns ? innerHeight : document.documentElement &&
        document.documentElement.clientHeight ?
    document.documentElement.clientHeight : document.body.clientHeight;
            this.cx += (pX + this.sx - this.cx) / 8; this.cy += (pY + this.sy - this.cy) / 8;
            this.sP(this.cx, this.cy);
            setTimeout(this.id + "_obj.flt()", 1);
            //this.id + "_obj.flt()";
        }
        return el;
    }
    JSFX_FloatDiv("floatdiv", 180, 280).flt();

</script>
</asp:Content>
