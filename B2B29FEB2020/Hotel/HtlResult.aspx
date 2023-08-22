<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HtlResult.aspx.vb" Inherits="Hotel_HtlResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />
    <link href="css/normalize.css" rel="stylesheet" type="text/css" />
    <link href="css/jplist.min.css" rel="stylesheet" type="text/css" />
    <link href="css/B2Bhotelengine.css?v=0.0" rel="stylesheet" />
    <link href="../CSS/A2BT/basic.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <link href="../Styles/jquery.qtip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        Array.prototype.unique = function () {
            var a = []; var l = this.length;
            for (var i = 0; i < l; i++) {
                for (var j = i + 1; j < l; j++) {
                    // If this[i] is found later in the array
                    if (this[i] === this[j])
                        j = ++i;
                }
                a.push(this[i]);
            }
            return a;
        };
    </script>
    <style type="text/css">
        body {
            background-color: #eee !important;
        }
        .fss {
            color: #fff!important;
        }
    </style>

    <div class="modifyss" id="htlmodify">
        <div class="main-content">
            <div class="large-2 medium-2 small-2 columns">
                <div class="f16 fss">City</div>
                <div class="cityname"><%=Request("htlCity")%></div>
            </div>
            <div class="large-2 medium-2 small-2 columns">
                <div class="f16 fss">Check In</div>
                <div class="Fcheckin"><%=Request("htlcheckin")%></div>
            </div>
            <div class="large-2 medium-2 small-2 columns">
                <div class="f16 fss">Check Out</div>
                <div class="FcheckOut"><%=Request("htlcheckout")%></div>
            </div>
            <div class="large-1 medium-1 small-2 columns">
                <div class="f16 fss">Night</div>
                <div class='nightcount'><%=Request("Nights")%></div>
            </div>
            <div class="large-1 medium-1 small-2 columns" id="Roomss" runat="server">
                <div class="f16 fss">Room</div>
                <div><%=Request("numRoom")%></div>
            </div>
            <div class="large-2 medium-2 small-2 columns" id="NoofPeople" runat="server">
                <div class="f16 fss">Guest/s</div>
                <div>
                    <%=Request("TotAdt")%>  Adult<span id="Noofchilds" class="chdtotal hide">, <%=Request("TotChd")%> Child</span>
                </div>
            </div>
            <div class="large-2 medium-2 small-2 columns">
                <input type="button" id="Hotel_Modify" value="Modify" class="button" />
            </div>
            <div class="clear1"></div>
        </div>
        <div class="clear1"></div>
    </div>
    <div class="large-12 medium-12 small-12 gray">
        <div class="ResultPageLoding_box" id="divloading">
            <div class="ResultPageLoding center padding1">
                <div class="clear3"></div>
                <h1 class="text-center" style="font-size: 20px;">PLEASE WAIT</h1>
                <span>Please do not close or refresh this window.</span>
                <div class="clear1"></div>
                <img alt="loading" height="55" width="55" src="../images/Loadinganim.gif" />
                <div class="clear1"></div>
                <div class="f16" style="color: #004b91; font-size:13px!important; font-weight:bold; ">
                    <span>City - &nbsp; <%=Request("htlCity")%></span>
                    <div class="clear1"></div>
                    <div class="large-12" style="float: left;">CheckIn - &nbsp; <%=Request("htlcheckin")%> - CheckOut -  &nbsp; <%=Request("htlcheckout")%></div>
                    <%--<div class="large-6" style="float:left;"></div>--%>
                    <div class="clear1"></div>
                    <span>Room - &nbsp; <%=Request("numRoom")%></span>
                    <div class="clear1"></div>
                </div>
            </div>
            <div class="clear1"></div>
            <div class="ResultPageLodingPromo">
                <img alt="loading" src="Images/hotel-Promo.jpg" /></div>
        </div>
        <div class="clear"></div>
        <div class="clear"></div>
        <div id="main-content" class="large-12 medium-12 small-12">
            <div class="specialnote"></div>
            <div id="page-content">
                <div id="demo" class="jplist">
                    <%--<div class="collapse ShowhideNoResult sortotion large-1 medium-1 small-12 columns res" id="khulaband"><span id="khula">Collapse</span><span id="band" class="hide">Expand</span></div>--%>
                    <div class="wht lft" id="filterdiv" style="padding: 10px; width: 25%;">
                        <div class="padding1">
                            <div class="jplist-ios-button">
                                <i class="fa fa-sort"></i>
                                jPList Actions
                            </div>
                            <div class="jplist-panel panel-top sgt-box-bg">
                                <div class="bld f16">
                                    <img src="Images/filter.png" />
                                    Filter Search
                                </div>

                                <button type="button" class="jplist-reset-btn" data-control-type="reset" data-control-name="reset" data-control-action="reset">
                                    Reset Filter
                                    <img src="Images/reset.png" style="position: relative; top: 3px;">
                                </button>
                                <div class="clear1"></div>
                                <div style="padding-bottom: 20px;" class="f16 jplist-group" data-control-type="checkbox-text-filter" data-control-action="filter"
                                    data-control-name="DealHotel" data-path=".DealHotel" data-logic="or">
                                    <input value="DealHotel" id="DealHotel" type="checkbox" />
                                    <label for="">Filter By Deal Hotel</label>
                                </div>
                                <div class="clear3"></div>
                                <hr />
                                <div class="f16">Filter By Hotel Name </div>
                                <!-- filter by Hotel name -->
                                <div class="text-filter-box">
                                    <input data-path=".title" type="text" value="" placeholder="Filter by Hotel Name"
                                        data-control-type="textbox" data-control-name="title-filter" data-control-action="filter" />
                                </div>
                                <div class="clear3"></div>
                                <hr />

                                <div class="f16">Filter By Star Ratings</div>
                                <div style="padding-bottom: 10px;" class="jplist-group w100" data-control-type="checkbox-text-filter" data-control-action="filter"
                                    data-control-name="starRating" data-path=".starRating" data-logic="or">
                                    <input value="5" id="5" type="checkbox" class="marginfori" />
                                    <label for="5">
                                        <img src="Images/5star.png" /></label>
                                    <input value="4" id="4" type="checkbox" class="marginfori" />
                                    <label for="4">
                                        <img src="Images/4star.png" /></label>
                                    <input value="3" id="3" type="checkbox" class="marginfori" />
                                    <label for="3">
                                        <img src="Images/3star.png" /></label>
                                    <input value="2" id="2" type="checkbox" class="marginfori" />
                                    <label for="2">
                                        <img src="Images/2star.png" /></label>
                                    <input value="1" id="1" type="checkbox" class="marginfori" />
                                    <label for="1">
                                        <img src="Images/1star.png" /></label>
                                    <input value="0" id="Checkbox1" type="checkbox" class="marginfori" />
                                    <label for="0">
                                        <img src="Images/0star.png" /></label>
                                </div>
                                <div class="clear3"></div>
                                <hr />
                                <div class="f16">Filter By Price Range</div>
                                <!-- filter by Price -->
                                <div class="clear3"></div>
                                <div class="jplist-range-slider w95" data-control-type="range-slider-likes"
                                    data-control-name="range-slider-likes" data-control-action="filter" data-path=".like">
                                    <div class="ui-slider" data-type="ui-slider"></div>
                                    <div class="clear1"></div>
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                                <div class="clear3"></div>
                                <hr />
                                <div class="f16">Filter By Location </div>
                                <div class="text-filter-box">
                                    <input data-path=".location" type="text" value="" placeholder="Filter by Location Name"
                                        data-control-type="textbox" data-control-name="location-filter" data-control-action="filter" />
                                    <div class="clear"></div>
                                </div>
                                <!-- checkbox filters By location -->
                                <div class="jplist-group w95 padding2s" data-control-type="checkbox-text-filter" data-control-action="filter"
                                    data-control-name="keywords" data-path=".location" data-logic="or">
                                    <div id="divlocation">
                                    </div>
                                </div>

                                <div class="clear1"></div>
                                <hr />
                                <!-- filter by Aminites -->
                                <div class="f16">Filter By Amenities</div>
                                <div class="text-filter-box w95 padding2s" data-control-type="checkbox-text-filter" data-control-action="filter"
                                    data-control-name="aminites" data-path=".aminites" data-logic="or">

                                    <input value="Spa/Massage/Wellness/Sauna" id="SA" type="checkbox" />
                                    <label for="Spa/Massage/Wellness/Sauna">Spa/Massage/Wellness/Sauna</label>
                                    <div class="clear"></div>
                                    <input value="Internet/Wi-Fi" id="IN" type="checkbox" />
                                    <label for="Internet/Wi-Fi">Internet/Wi-Fi</label>
                                    <div class="clear"></div>
                                    <input value="Business Facilities" id="BC" type="checkbox" />
                                    <label for="Business Facilities">Business Facilities</label>
                                    <div class="clear"></div>
                                    <input value="Disabled Facilities" id="DF" type="checkbox" />
                                    <label for="Disabled Facilities">Disabled Facilities</label>
                                    <div class="clear"></div>
                                    <input value="Restaurant/Bar" id="MB" type="checkbox" />
                                    <label for="Restaurant/Bar">Restaurant/Bar</label>
                                    <div class="clear"></div>
                                    <input value="Swimming Pool" id="OP" type="checkbox" />
                                    <label for="Swimming Pool">Swimming Pool</label>
                                    <div class="clear"></div>
                                    <input value="Tub Bath" id="IP" type="checkbox" />
                                    <label for="Tub Bath">Tub Bath</label>
                                    <div class="clear"></div>
                                    <input value="Parking" id="Parking" type="checkbox" />
                                    <label for="Parking">Parking</label>
                                    <div class="clear"></div>
                                    <input value="Gym" id="Gym" type="checkbox" />
                                    <label for="Gym">Gym</label>
                                    <div class="clear"></div>
                                    <input value="Travel & Transfers" id="TA" type="checkbox" />
                                    <label for="Travel & Transfers">Travel & Transfers</label>
                                    <div class="clear"></div>

                                    <div class='hideFacilty'>
                                        <input value="Laundry Services" id="LY" type="checkbox" />
                                        <label for="Laundry Services">Laundry Services</label>
                                        <div class="clear"></div>
                                        <input value="Baby Facilities" id="BS" type="checkbox" />
                                        <label for="Baby Facilities">Baby Facilities</label>
                                        <div class="clear"></div>
                                        <input value="Beauty Parlour" id="BP" type="checkbox" />
                                        <label for="Beauty Parlour">Beauty Parlour</label>
                                        <div class="clear"></div>
                                        <input value="Room Services" id="RS" type="checkbox" />
                                        <label for="Room Services">Room Services</label>
                                        <div class="clear"></div>
                                        <input value="Sports" id="Sports" type="checkbox" />
                                        <label for="Sports">Sports</label>
                                        <div class="clear"></div>
                                        <input value="Tea/Coffee" id="TC" type="checkbox" />
                                        <label for="Tea/Coffee">Tea/Coffee</label>
                                        <div class="clear"></div>
                                        <input value="TV" id="TV" type="checkbox" />
                                        <label for="TV">TV</label>
                                        <div class="clear"></div>
                                        <input value="AC" id="AC" type="checkbox" />
                                        <label for="AC">AC</label>
                                        <div class="clear"></div>
                                        <input value="Lift" id="Lift" type="checkbox" />
                                        <label for="Lift">Lift</label>
                                        <div class="clear"></div>
                                        <input value="Phone" id="Phone" type="checkbox" />
                                        <label for="Phone">Phone</label>
                                        <div class="clear"></div>
                                        <input value="Lobby" id="Lobby" type="checkbox" />
                                        <label for="Lobby">Lobby</label>
                                        <div class="clear"></div>
                                        <input value="Hair Dryer" id="HD" type="checkbox" />
                                        <label for="Hair Dryer">Hair Dryer</label>
                                    </div>
                                    <div class="clear1"></div>
                                    <a href='javascript:void(0);' class='ShowMoreFacilty padding1s f16 '>More Amenities</a>
                                    <div class="clear1"></div>
                                </div>
                                <div class="clear1"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class=" w70 lft" id="resultdiv" style="margin-left: 10px; padding: 10px; width: 72%;">



                        <div class="clear"></div>
                        <div class="jplist-panel w100 ShowhideNoResult bgw" style="padding-left: 10px;">
                            <div class="jplist-views lft w24"
                                data-control-type="views"
                                data-control-name="views"
                                data-control-action="views"
                                data-default="list-view">
                                <button style="background: url('Images/list-btn.png') no-repeat 50% 50%;" type="button" class="jplist-view list-view other-view" data-type="list-view" title="List View"></button>
                                <button style="background: url('Images/grid-btn.png') no-repeat 50% 50%;" type="button" class="jplist-view grid-view other-view" data-type="grid-view" title=" Grid View"></button>
                                <button type="button" class="map-view bgw  hide" title="Map View">
                                    &nbsp;&nbsp;
                                    <img src="Images/map.png" />
                                    &nbsp;&nbsp;</button>
                                <%--<div class="bld f16 jplist-group deal_view" data-control-type="checkbox-text-filter" data-control-action="filter"
                                    data-control-name="DealHotel" data-path=".DealHotel" data-logic="or">
                                     <input value="DealHotel" id="DealHotel" type="checkbox" class="marginfori" />
                                     <label for="">Deal Hotel</label>
                                </div>--%>
                                <%--<button type="button" class="Deal-view dealhtl padding1s" title="Deal View" id="divdishtl"> <span class="lft bld f16"> 98 </span> &nbsp;&nbsp; <span class="rgt"> <img src="Images/deal1.png" /> </span> </button>--%>
                                <%--<button style="background: url('Images/deal3.jpg') no-repeat 50% 50%;" type="button" class="Deal-view dealhtl padding1s" title="Deal View" id="divdishtl"></button>--%>
                            </div>
                            <div class="w30 lft sortotion" style="margin: 12px 0 0 10px;">
                                <div style="line-height: 30px;" class="f14 lft">Sort By &nbsp;&nbsp;</div>
                                <div class="jplist-drop-down rgt" data-control-type="drop-down" data-control-name="sort" data-control-action="sort" style="margin: 0 3px;">
                                    <ul>
                                       <%-- <li><span data-path="default">Sort by</span></li>--%>
                                        <li><span data-path=".like" data-order="asc" data-type="number" data-default="true">Price Asc</span></li>
                                        <li><span data-path=".like" data-order="desc" data-type="number">Price Desc</span></li>
                                        <li><span data-path=".Populerty" data-order="asc" data-type="number">Popularity</span></li>
                                        <li><span data-path=".starRating" data-order="asc" data-type="text">Star Asc</span></li>
                                        <li><span data-path=".starRating" data-order="desc" data-type="text">Star Desc</span></li>
                                        <li><span data-path=".title" data-order="asc" data-type="text">Hotel Name A-Z</span></li>
                                        <li><span data-path=".title" data-order="desc" data-type="text">Hotel Name Z-A</span></li>
                                    </ul>
                                </div>


                            </div>
                            <div class="clear"></div>
                            <div class="jplist-drop-down ShowhideNoResult"
                                data-control-type="drop-down"
                                data-control-name="paging"
                                data-control-action="paging"
                                data-control-animate-to-top="true">
                                <ul>
                                    <li><span data-number="10">10 per page </span></li>
                                    <li><span data-number="20" data-default="true">20 per page </span></li>
                                    <li><span data-number="30">30 per page </span></li>
                                    <li><span data-number="40">40 per page </span></li>
                                    <li><span data-number="all">view all </span></li>
                                </ul>
                            </div>
                            <!-- pagination results -->
                            <div style="margin-top: 12px"
                                class="jplist-label ShowhideNoResult"
                                data-type="{start} - {end} of {all}"
                                data-control-type="pagination-info"
                                data-control-name="paging"
                                data-control-action="paging">
                            </div>

                            <!-- pagination -->
                            <div style="margin-top: 2px"
                                class="jplist-pagination ShowhideNoResult"
                                data-control-type="pagination"
                                data-control-name="paging"
                                data-control-action="paging"
                                data-control-animate-to-top="true">
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class=" jplist-panel Hotel_GridListView">


                            <div class="list w100" id="divhtldtls">
                            </div>

                            <div class="clear"></div>
                            <div id="divnoresult" class="box jplist-no-results text-shadow align-center bld f20 headercolor">
                                <p>Hotel not found, Please modify your search.</p>
                            </div>
                            <div class="clear1"></div>
                            <div class="jplist-panel w100 panel-bottom ShowhideNoResult bgw">
                                <div class="jplist-drop-down"
                                    data-control-type="drop-down"
                                    data-control-name="paging"
                                    data-control-action="paging"
                                    data-control-animate-to-top="true">
                                    <ul>
                                        <li><span data-number="10">10 per page </span></li>
                                        <li><span data-number="20" data-default="true">20 per page </span></li>
                                        <li><span data-number="30">30 per page </span></li>
                                        <li><span data-number="40">40 per page </span></li>
                                        <li><span data-number="all">view all </span></li>
                                    </ul>
                                </div>
                                <!-- pagination results -->
                                <div style="margin-top: 12px"
                                    class="jplist-label"
                                    data-type="{start} - {end} of {all}"
                                    data-control-type="pagination-info"
                                    data-control-name="paging"
                                    data-control-action="paging">
                                </div>

                                <!-- pagination -->
                                <div style="margin-top: 2px"
                                    class="jplist-pagination"
                                    data-control-type="pagination"
                                    data-control-name="paging"
                                    data-control-action="paging"
                                    data-control-animate-to-top="true">
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="Hotel_MapView brdr4">
                            <div class='lft w20'>
                                <div class="Hotelmapdetails">

                                    <div class='clear1'></div>
                                    <div class="Hotelmapd">
                                        <div class='clear1'></div>
                                    </div>

                                </div>
                                <div class="clear1"></div>
                                <div id="next" class="lft bld headercolor f16">
                                    <a href="#" id="counter" class="counter">&nbsp;</a>
                                </div>
                            </div>
                            <div class="w80 rgt">
                                <div id="MapDiv" style="width: 100%; border: 2px solid #0066CC; padding: 2px; box-shadow: 1px 1px 4px #000;">
                                    <table cellpadding="0" cellspacing="0" class="allHotelmap">
                                        <tr>
                                            <td>
                                                <div id="map_canvas2" style="position: relative; width: 740px; height: 830px; background: url('<%= ResolveUrl("~/images/loading_bar.gif")%>'); background-repeat: no-repeat; background-position: center;">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="basic-modal-content" style="  overflow: auto; height: 505px; width: 900px;">
            <div style="width: 830px; height: 22px; float: left; vertical-align: top; font-weight: bold; font-size: 14px;">
                <div id="selected_Hotel" style="float: left; border-right: 1px #333333 dotted;">
                </div>
                <div style="float: left;" id="mapss">
                    <img src="images/marker2.png" alt="" />&nbsp;
                </div>
                <div style="float: left;" id="selected_City">
                </div>
            </div>
            <div id="map_canvas" style="width: 850px; height: 445px; float: left;">
            </div>
            <div id="cacellationPolicy" style="float: left; margin: 40px;">
            </div>
        </div>
        <div class="Hotel_ModifyOuter">
            <div class="Hotel_Modify mauto">
                <div class="closex2">
                    <img src="../Images/close.png" class="HidRoomPopup" />
                </div>
                <div class="large-10 medium-10 small-12 large-push-1 medium-push-1">

                    <div class="large-5 medium-5 small-12 columns">

                        <div class="large-12 medium-12 small-12 columns">
                            Select City:<br />

                            <input type="text" id="htlCity" name="htlCity" value="" />
                            <input type="hidden" id="htlcitylist" name="htlcitylist" value="" />
                            <input type="hidden" id="contrycode" name="contrycode" value="" />
                            <input type="hidden" id="CountryName" name="CountryName" value="" />
                        </div>
                        <div class="clear"></div>


                        <div class="large-6 medium-6 small-6 columns">
                            Check In Date:<br />
                            <input type="text" id="htlcheckin" name="htlcheckin" value="" readonly="readonly" class="txtCalander" />
                        </div>

                        <div class="large-6 medium-6 small-6 columns">
                            Check Out Date:<br />
                            <input type="text" id="htlcheckout" name="htlcheckout" value="" readonly="readonly" class="txtCalander" />
                            <div style="float: right;" id="nights"><%=Request("Nights")%> Night</div>
                        </div>

                    </div>
                    <div class="large-5 medium-5 small-12 columns">

                        <div class="f20">
                            No. of Rooms & Guests
                        </div>
                        <div class="clear1"></div>
                        <div class="psgoptionhtl w100">
                            <ul class="passengerul">
                                <li class="passengerli"></li>
                            </ul>
                            <ul class="passengerul rgt">
                                <li class="passengerli w100">
                                    <div id="hot-search-params" class="w100">
                                    </div>
                                    <input type="hidden" name="rooms" id="rooms" />
                                    <input type="hidden" name="chds" id="chds" />
                                    <input type="hidden" name="adts" id="adts" />

                                    <script src="JS/ModifySearch.js?v=0.2" type="text/javascript"></script>

                                </li>
                            </ul>
                        </div>


                        <div class="large-7 medium-7 small-12 rgt">
                            <input type="button" id="btnHotel" value="Find Hotel" class="button" />
                            <input type="hidden" name="SearchType" id="SearchType" value="htl" />
                            <input type="hidden" id="htlstar" name="htlstar" />
                            <input type="hidden" id="htlname" name="htlname" />
                            <input type="hidden" id="mrdsch" name="mrdsch" value="Y" />
                            <input type="hidden" name="ReqType" id="ReqType" value="MS" />
                            <input id="CountryName" type="hidden" name="CountryName" runat="server" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
        
         <div id="RoomPopup">
                <div class="closex2">
                    <img src="../Images/close.png" class="HidRoomPopup" />
                </div>
                <div style="padding: 11px;">
                    <div class="w50 lft">
                        <div class="f20"><span id="selectedHotel"></span> &nbsp;&nbsp; <span class="selectedHotelStarrating"></span></div>
                         <div class="clear"></div>
                         <div class="f14"><span class="selectedHotelAddress"></span></div>
                        <div class="clear"></div>
                        <a href="#" class="underlineitalic showRoomPrices hide">Select your prefered room below</a>
                    </div>
                    <div class="rgt w45">
                        <div class="lft w30">
                            Checkin : &nbsp; <span class="bld"><%=Request("htlcheckin")%></span>
                        </div>
                        <div class="lft w30">
                            CheckOut: <span class="bld"><%=Request("htlcheckout")%></span>
                        </div>
                        <div class="lft w10">
                            <span class="bld f20"><%=Request("Nights")%></span> Night
                        </div>

                        <div class="lft w10">
                            <span class="bld f20"><%=Request("numRoom")%></span> Room
                        </div>
                        <div class="lft w10">
                            <span class="bld f20"><%=Request("TotAdt")%></span>
                            Adult
                        </div>
                        <div class="lft w10">
                            <span class="bld f20 chdtotal hide"><%=Request("TotChd")%> Child</span>
                        </div>
                    </div>                 
                </div>
               <div class="clear"></div>
                <div id="RoomInformation">
                    <div class="roomtab" id="RoomPhototab"> &nbsp;&nbsp; PHOTOS &nbsp;&nbsp; </div>
                    <div class="roomtab roomtab1" id="RoomRatestab"> &nbsp;&nbsp; ROOMS AND RATES &nbsp;&nbsp; </div>                   
                    <div class="roomtab" id="RoomDisciptipntab"> &nbsp;&nbsp; OVERVIEW &nbsp;&nbsp; </div>
                    <div class="roomtab" id="RoomAmenitiestab"> &nbsp;&nbsp; AMENITIES &nbsp;&nbsp; </div> 
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
                <div id="RoomDetals"></div>
                <div class="clear"></div>
            </div>

        <div class="HotelFareBrekup">
            <div class="lft bld f16">&nbsp; <%=Request("numRoom")%> &nbsp; Room Per Night Rate</div>
            <div class="closex2">
                <img src="../Images/close.png" class="HidRoomPopup2" />
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>SUN
                        </th>
                        <th>MON
                        </th>
                        <th>TUE
                        </th>
                        <th>WED
                        </th>
                        <th>THU
                        </th>
                        <th>FRI
                        </th>
                        <th>SAT
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td id="SUNDAY"></td>
                        <td id="MONDAY"></td>
                        <td id="TUESDAY"></td>
                        <td id="WEDNESDAY"></td>
                        <td id="THURSDAY"></td>
                        <td id="FRIDAY"></td>
                        <td id="SATURDAY"></td>
                    </tr>
                    <tr id="weeks2" class="hide">
                        <td id="SUNDAY2"></td>
                        <td id="MONDAY2"></td>
                        <td id="TUESDAY2"></td>
                        <td id="WEDNESDAY2"></td>
                        <td id="THURSDAY2"></td>
                        <td id="FRIDAY2"></td>
                        <td id="SATURDAY2"></td>
                    </tr>
                    <tr id="weeks3" class="hide">
                        <td id="SUNDAY3"></td>
                        <td id="MONDAY3"></td>
                        <td id="TUESDAY3"></td>
                        <td id="WEDNESDAY3"></td>
                        <td id="THURSDAY3"></td>
                        <td id="FRIDAY3"></td>
                        <td id="SATURDAY3"></td>
                    </tr>
                </tbody>
            </table>
            <table class="lft  w50">
                <tr>
                    <td align="center">
                        <div id="PriceBreakups"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="fademenu">
            &nbsp;
        </div>
        <div id="Recentbooked" style="display: none;"></div>
    </div>


    <script type="text/javascript">
        $('#htlCity').val('<%=Request("htlCity")%>');
        $('#htlcheckin').val('<%=Request("htlcheckin")%>');
        $('#htlcheckout').val('<%=Request("htlcheckout")%>');
        $('#htlcitylist').val('<%=Request("htlcitylist")%>');
        $('#htlstar').val('<%=Request("htlstar")%>');
        $('#htlname').val('<%=Request("htlname")%>');
        $('#adts').val('<%=Request("TotAdt")%>');
        $('#chds').val('<%=Request("TotChd")%>');
        $('#rooms').val('<%=Request("numRoom")%>');
        var totchd = parseInt('<%=Request("TotChd")%>');
        var totadt = parseInt('<%=Request("TotAdt")%>');
        var totrm = parseInt('<%=Request("numRoom")%>');
        var rm, ad, ch;
        if (totrm > 1) { rm = 'Rooms'; } else { rm = 'Room'; }
        if (totadt > 1) { ad = 'Adults'; } else { ad = 'Adult'; }
        if (totchd > 1) { ch = 'Children'; } else { ch = 'Child'; }
        if (totchd > 0) {
            $('#tot_guest').html(totrm + " " + rm + ", " + totadt + " " + ad + ", " + totchd + " " + ch);
            $('#Noofchilds').show();
        }
        else { $('#tot_guest').html(totrm + " " + rm + ", " + totadt + " " + ad); }
        setgustDetail(totrm, '<%=Request("Guest")%>');
        setNumRooms(totrm);

        if (parseInt('<%=Request("TotChd")%>') > 0) {
            $(".chdtotal").show();
        }
    </script>

    <script src="JS/jHotelResults.js?v=1.0" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js") %>"> </script>
    <script src="<%= ResolveUrl("JS/jplist.min.js")%>" type="text/javascript"></script>
    <script src="JS/modernizr.min.js" type="text/javascript"></script>
   <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8Ym8Qgbo7FvrCDLe-zKHzkcFstbAJeIM&libraries=places" ></script>
    <script type="text/javascript" src="<%=ResolveUrl("JS/jHotelFareBreakup.js?v=0.0")%>"></script>
    <script src="JS/HtlResultMap.js" type="text/javascript"></script>
    <script src="JS/JHotelRoomDetails.js?v=1.0" type="text/javascript"></script>
    <script src="../Scripts/json2.js" type="text/javascript"></script>
    <script src="../JS/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="JS/JSLINQ.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("JS/HtlSearchQuery.js?v=0.0")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.qtip.min.js") %>"></script>

</asp:Content>

