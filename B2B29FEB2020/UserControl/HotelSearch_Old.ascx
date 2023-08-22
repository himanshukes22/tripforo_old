<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HotelSearch_Old.ascx.vb"
    Inherits="UserControl_HotelSearch_Old" %>
<link href="<%=ResolveUrl("~/Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet"
    type="text/css" />
<link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet"
    type="text/css" />

<script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/Hotel/JS/HtlSearchQuery.js") %>" type="text/javascript"></script>

<script type="text/javascript">
    var UrlBase = '<%=ResolveUrl("~/") %>';
    
    $(function() {
        $("#htlcheckin").datepicker(
		     { numberOfMonths: 2,
		         showButtonPanel: true, autoSize: true, dateFormat: 'yy-mm-dd', closeText: 'X', duration: 'slow', gotoCurrent: true,
		         hideIfNoPrevNext: true, maxDate: '+1y', minDate: '+0d', navigationAsDateFormat: false, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
		         selectOtherMonths: false, showOn: "button", buttonImage: 'Images/cal.gif', buttonImageOnly: true
		     }
		).datepicker("setDate", new Date());
    });
    $(function() {
        $("#htlcheckout").datepicker(
		     { numberOfMonths: 2,
		         showButtonPanel: true, autoSize: true, dateFormat: 'yy-mm-dd', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
		         changeYear: false, hideIfNoPrevNext: true, maxDate: '+1y', minDate: '+0d', navigationAsDateFormat: false, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
		         selectOtherMonths: false, showOn: "button", buttonImage: 'Images/cal.gif', buttonImageOnly: true
		     }
		).datepicker("setDate", new Date().getDate + 1);
    });
	
</script>

<table cellpadding="0" cellspacing="0" align="right" style="width: 94%;">
    <tr>
        <td style="width: 11px; height: 27px; background: url(images/tpbg.jpg);">
        </td>
        <td style="height: 27px; background: url(images/tpbg1.jpg); text-align: center;"
            align="center">
            Book Domestic And International Hotels
        </td>
        <td style="width: 11px; height: 27px; background: url(images/tpbg3.jpg);">
        </td>
    </tr>
    <tr>
        <td style="width: 11px; height: 27px; background: url(images/tpbgm1.jpg);">
        </td>
        <td>
            <table align="center" cellpadding="11" cellspacing="20">
                <tr>
                    <td>
                        <table align="center" style="background: #fff;">
                            <tr>
                                <td>
                                    <table align="center" style="background: #fff;">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="5" cellspacing="5" style="width: 300px;">
                                                    <tr>
                                                        <td style="width: 180px;">
                                                            Select City :
                                                        </td>
                                                        <td>
                                                            <input type="text" id="htlCity" name="htlCity" value=""  />
                                                            <input type="hidden" id="htlcitylist" name="htlcitylist" value=""  />
                                                             <input type="hidden" id="contrycode" name="contrycode" value=""  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="checkin_hotel">
                                                                Check In Date :</label>
                                                        </td>
                                                        <td>
                                                            <input type="text" id="htlcheckin" name="htlcheckin" value="" class="txtCalander" />
                                                            <input type="hidden" name="hidhtlcheckin" id="hidhtlcheckin" value=""  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="checkout_hotel">
                                                                Check Out Date:
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <input type="text" id="htlcheckout" name="htlcheckout" value="" class="txtCalander" />
                                                            <input type="hidden" name="hidhtlcheckout" id="hidhtlcheckout" value="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width: 0px;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <a href="#" id="buttonAddOpt"><strong>Additional Option</strong></a>
                                                            <table align="center" id="effect" style="display: none;" cellpadding="5" cellspacing="10">
                                                                <tr>
                                                                    <td>
                                                                        Hotel Name
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="htlname" name="htlname" value="" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Star Rating
                                                                    </td>
                                                                    <td>
                                                                        <select id="htlstar" name="htlstar">
                                                                            <option value="0">Select Star Rating</option>
                                                                            <option value="1">1 Star</option>
                                                                            <option value="2">2 Stars</option>
                                                                            <option value="3">3 Stars</option>
                                                                            <option value="4">4 Stars</option>
                                                                            <option value="5">5 Stars</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="20px">
                                            </td>
                                            <td valign="top">
                                                <ul class="hoteltitleul">
                                                    <li class="hoteltitleli3">
                                                        <label>
                                                            Number of Rooms & Guests</label>
                                                    </li>
                                                </ul>
                                                <div id="hot-search-params">
                                                </div>
                                                <input type="hidden" name="rooms" id="rooms" />
                                                <input type="hidden" name="chds" id="chds" />

                                                <script src="Hotel/JS/hotelpasg.js" type="text/javascript"></script>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <div>
                                                    <input type="hidden" name="ReqType" id="ReqType" value="S" />
                                                    <asp:Button ID="btnHotel" runat="server" Text="Find Hotel" class="button" OnClientClick="return validateHotel()" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="CalendarControl">
            </div>
        </td>
        <td style="width: 11px; height: 27px; background: url(images/tpbgm2.jpg);">
        </td>
    </tr>
    <tr>
        <td style="width: 11px; height: 14px; background: url(images/tpbg4.jpg);">
        </td>
        <td style="height: 14px; background: url(images/tpbgb.jpg);">
        </td>
        <td style="width: 11px; height: 14px; background: url(images/tpbg6.jpg);">
        </td>
    </tr>

    <script type="text/javascript">

        $(function() {
            // run the currently selected effect
            function runEffect(ii) {
                // get effect type from 
                var selectedEffect = 'blind';
                // most effect types need no options passed by default
                var options = {};
                // run the effect
                $("#effect").toggle(selectedEffect, options, 500);

            };

            // set effect from select menu value
            $("#buttonAddOpt").click(function() {
                runEffect();
                return false;
            });
        });
    </script>

</table>
