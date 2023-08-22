<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newBusResultaspx.aspx.cs" MasterPageFile="~/MasterAfterLogin.master" Inherits="BS_newBusResultaspx" %>

<%@ Register Src="~/BS/UserControl/BusSearch.ascx" TagName="BusSearch" TagPrefix="UC1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/BS/CSS/jquery-ui-1.8.8.custom.css")%>" rel="stylesheet" type="text/css" />

    <link href="<%=ResolveUrl("~/BS/CSS/basic.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/normalize.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/styles.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/jplist.min.css")%>" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body {
            background-color: #eee;
        }

        .jplist-range-slider .ui-slider {
            float: left;
            width: 230px;
            margin-left: 10px;
            margin-top: 10px;
        }

        .jplist-panel .jplist-group label {
            height: 28px !important;
            line-height: 22px !important;
        }
    </style>


    <div id="Modsearch" style="background: url(../images/blur-images-14.jpg); height: 70%; width: 100%; position: absolute; z-index: 99; left: 0px; top: 0px; overflow: hidden; display: none;">
        <div style="width: 74%; overflow-x: auto; margin: 50px auto;">
            <div id="mdclose" style="width: 30px; height: 30px; border-radius: 50%; border: 3px solid #ccc; position: absolute; right: 14%; top: 8px; text-align: Center; font-size: 15px; background-color: black; cursor: pointer; color: #fff;">
                X
            </div>
            <div class="clear1"></div>
            <UC1:BusSearch runat="server" ID="BusSearch" />
        </div>
    </div>

    <div class="modifyss" id="htlmodify" style="height: 48px;">
        <div class="main-content">
            <div class="bothwayselect1">
                <div class="bothwayselect" id="mSelectedResult"></div>
                <div class="clear"></div>
                <div id="oneWayDivheader" class="text-center" style="font-size: 16px;"></div>
                <div id="RoundDivsheader"></div>
            </div>
            <div class="clear1"></div>
        </div>
        <div class="clear1"></div>
    </div>

    <div id="main-content" class="large-12 medium-12 small-12">




        <div id="page-content1" class="box">
            <div id="demo" class="box jplist">
                <div class="jplist-ios-button">
                    <i class="fa fa-sort"></i>
                    jPList Actions
                </div>
                <div class="col-md-3" style="padding: 10px; width: 25%; margin-top: 15px;">
                    <div class="jplist-panel box panel-top ">
                        <div class="jplist-drop-down left hide"
                            data-control-type="drop-down"
                            data-control-name="paging"
                            data-control-action="paging">
                            <ul>
                                <li><span data-number="all" data-default="true">view all </span></li>
                            </ul>
                        </div>
                        <!-- pagination results -->
                        <div style="margin-left: 42px; display: none;" class="jplist-pagination" data-control-type="pagination"
                            data-control-name="paging"
                            data-control-action="paging">
                        </div>
                        <div style="display: none"
                            class="jplist-label"
                            data-type="Page {current} of {pages}"
                            data-control-type="pagination-info"
                            data-control-name="paging"
                            data-control-action="paging">
                        </div>
                        <div style="float: right"
                            class="jplist-views"
                            data-control-type="views"
                            data-control-name="views"
                            data-control-action="views"
                            data-default="list-view">
                            <!-- data-default="list-view" -->
                            <%--  <button style="background: url('../Images/list-btn.png') no-repeat 50% 50%;" type="button" class="jplist-view list-view" data-type="list-view" title="List View"></button>
                            <button style="background: url('../Images/grid-btn-disabled.png') no-repeat 50% 50%;" type="button" v class="jplist-view grid-view" data-type="grid-view" title="Grid View" ></button>
                            <button style="background: url('../Images/grid-btn.png') no-repeat 50% 50%;" type="button" class="jplist-view matrix-view hide" data-type="matrix-view"></button>--%>
                        </div>
                        <!-- pagination -->
                    </div>
                    <div class="jplist-panel w22 padding1 brdr bgw boxshadow lft">
                        <div>
                            <div id="dsplm" class="large-12 medium-12 small-12 columns" style="height: 46px; padding-top: 10px;padding-bottom: 10px; padding-left: 10px;">

                                <a href="#" id="ModifySearch" class="pnday msearch">Modify Search</a>

                                <a href="#" class="jplist-reset-btn f14 colorb resetall"
                                    data-control-type="reset"
                                    data-control-name="reset"
                                    data-control-action="reset">Reset All Filters</a>
                            </div>


                            <hr />

                            <div class="clear1"></div>
                            <div class="f20 colorb">
                                <i class="fa fa-filter" aria-hidden="true"></i>
                                Filter Search
                            </div>


                            <div class="clear1"></div>
                            <div onclick="setonclickCss(this.id)" class="modifytxt" id="onewAySrc"></div>
                            <div onclick="setonclickCss(this.id)" class="modifytxt1" id="ReturnwAySrc"></div>
                        </div>

                        <div class="clear1"></div>
                        <div id="filtersPrice" class="f14 bld colorb cursorpointer filters">
                            <img src="Images/next1.png" style="position: relative; top: 3px;" />Filter By Price
                        </div>
                        <div id="divsliderShow1">
                            <div
                                class="jplist-range-slider "
                                data-control-type="range-slider-price1"
                                data-control-name="range-slider-price1"
                                data-control-action="filter"
                                data-path=".frscls1">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>
                        <div id="divsliderShow2" class="hide">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-price2"
                                data-control-name="range-slider-price2"
                                data-control-action="filter"
                                data-path=".farCls2">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>

                        <div class="clear1"></div>
                        <div id="filterDepttime" class="f14 bld colorb cursorpointer filters">
                            <img src="Images/next1.png" style="position: relative; top: 3px;" />
                            Filter By Departure Time
                        </div>
                        <div id="DivDeptshow">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-dept1"
                                data-control-name="range-slider-dept1"
                                data-control-action="filter"
                                data-path=".Onedept">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>
                        <div id="DivDeptshow2" class="hide">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-dept2"
                                data-control-name="range-slider-dept2"
                                data-control-action="filter"
                                data-path=".Onedept1">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>

                        <div class="clear1"></div>
                        <div id="filterArrtime" class="f14 bld colorb cursorpointer filters">
                            <img src="Images/next1.png" style="position: relative; top: 3px;" />Filter By Arrival Time
                        </div>
                        <div id="DivArrshow">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-Arr1"
                                data-control-name="range-slider-Arr1"
                                data-control-action="filter"
                                data-path=".OnedArr">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>
                        <div id="DivArrshow2" class="hide">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-Arr2"
                                data-control-name="range-slider-Arr2"
                                data-control-action="filter"
                                data-path=".OnedArr1">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="value lft" data-type="prev-value"></div>
                                    <div class="value rgt" data-type="next-value"></div>
                                </div>
                            </div>
                        </div>

                        <div class="clear1"></div>
                        <div id="filteravlSeat" class="f14 bld colorb cursorpointer filters">
                            <img src="Images/next1.png" style="position: relative; top: 3px;" />Filter By Available Seat
                        </div>
                        <div id="DivAvlshow">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-Avl1"
                                data-control-name="range-slider-Avl1"
                                data-control-action="filter"
                                data-path=".avl1">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="lft w20">
                                        <div class="value lft" data-type="prev-value"></div>
                                        <div class="rgt">
                                            <img src="Images/3.png" />
                                        </div>
                                    </div>
                                    <div class="rgt w20">
                                        <div class="value lft" data-type="next-value"></div>
                                        <div class="rgt">
                                            <img src="Images/3.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="DivAvlshowR" class="hide">
                            <div
                                class="jplist-range-slider"
                                data-control-type="range-slider-Avl2"
                                data-control-name="range-slider-Avl2"
                                data-control-action="filter"
                                data-path=".avl2">
                                <div class="ui-slider" data-type="ui-slider"></div>
                                <div class="clear1"></div>
                                <div class="w90 mauto">
                                    <div class="lft w20">
                                        <div class="value lft" data-type="prev-value"></div>
                                        <div class="rgt">
                                            <img src="Images/3.png" />
                                        </div>
                                    </div>
                                    <div class="rgt w20">
                                        <div class="value lft" data-type="next-value"></div>
                                        <div class="rgt">
                                            <img src="Images/3.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="clear1"></div>
                        <div id="ServiceSort1"></div>
                        <div class="clear1"></div>
                        <div id="ServiceSort2" class="hide"></div>
                        <div class="clear1"></div>
                        <div id="TravelerSort1"></div>
                        <div class="clear1"></div>
                        <div id="TravelerSort2" class="hide"></div>
                    </div>
                </div>
                <div class="col-md-9" style="margin-left: 10px; padding: 10px; width: 72%;">
                    <div id="busResult" class="Busbox1box2 w77 rgt">

                        <div class="clear"></div>
                        <div id="oneWayDiv"></div>
                        <div id="RoundDivs"></div>
                        <div class="hide" id="matrix"></div>
                    </div>
                    <div class="w79 rgt em7" style="margin-bottom: 10%;"></div>
                    <div class="jplist-panel box panel-bottom hide" style="background-color: #f1f1f1; padding-bottom: 30px; padding-top: 30px; border: solid;">
                        <div class="jplist-drop-down hide"
                            data-control-type="drop-down"
                            data-control-name="paging"
                            data-control-action="paging"
                            data-control-animate-to-top="true">
                            <ul>
                                <li><span data-number="all" data-default="true">view all </span></li>
                            </ul>
                        </div>
                        <!-- pagination results -->
                        <div
                            class="jplist-label"
                            data-type="{start} - {end} of {all}"
                            data-control-type="pagination-info"
                            data-control-name="paging"
                            data-control-action="paging">
                        </div>
                        <!-- pagination -->
                        <div
                            class="jplist-pagination"
                            data-control-type="pagination"
                            data-control-name="paging"
                            data-control-action="paging"
                            data-control-animate-to-top="true">
                        </div>
                    </div>
                </div>
                <div id="frare" class="popup-bus hide"></div>
                <div></div>
                <div id="divseat" class="divseat" style="display: none;">
                    <%--  <img src="<%= ResolveUrl("~/BS/Images/cls.png")%>" class="cls" align="right" />--%>
                </div>
                <div class="showSresul">
                </div>
                <div class="viewShow hide" id="viewshoeD">
                </div>
                <%--   <div id="tooltip" class="hide">gdf gjjjjjjjjjjjj  jgggg jjjjhytutyuytfghgf fghfh fdghdf hdfgd </div>--%>

                <div class="ResultPageLoding_box" id="divloading">
                    <div class="ResultPageLoding text-center padding1">
                        <div class="clear3"></div>
                        <h1 class="text-center" style="font-size: 20px;">PLEASE WAIT</h1>
                        <span>Please do not close or refresh this window.</span>
                        <div class="clear1"></div>
                        <img alt="loading" height="55" width="55" src="../images/Loadinganim.gif" />
                        <div class="clear1"></div>
                        <div id="source" class="divsrc">
                        </div>
                    </div>


                </div>



                <%--<div id="basic-modal-content" class="waittd hide">
                    We are Searching For Best Price..Please Wait
                <div class="clear1"></div>
                    <img src="../Images/loadingAnim.gif" />
                    <div class="clear1"></div>
                    <div id="source" class="divsrc">
                    </div>
                </div>--%>
                <div id="forDetails" style="display: none;" class="forDetails ">
                    Close Me...
                <img src="<%= ResolveUrl("~/BS/Images/cls.png")%>" class="fordetailscls" align="right" />
                </div>


            </div>

        </div>

    </div>




    <a href="#toptop"><span class="toptop" style="position: fixed; bottom: 20px; right: 20px; cursor: pointer; padding: 5px 10px; background: #43566f; color: #fff; display: none;">Top</span></a>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.tooltip.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/gridview-readonly-script.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/BS/JS/jquery-1.10.0.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/BS/JS/modernizr.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/BS/JS/jplist.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/SortAD.js")%>"></script>
    <link href="CSS/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="JS/jquery-ui.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/BS/JS/newresult.js") %>"> </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/BS/JS/jquery.bpopup.min.js") %>"> </script>

    <script type="text/javascript">
        jQuery("document").ready(function ($) {
            var nav = $('.bothwayselect1');
            $(window).scroll(function () {
                if ($(this).scrollTop() > 175) {
                    $(".toptop").fadeIn();
                    nav.addClass("bothwayselect1fix");
                } else {
                    $(".toptop").fadeOut();
                    nav.removeClass("bothwayselect1fix");
                }
            });
        });
        function ShowHotels() { $("#divloading").hide(); $("#main-content").show(); }
        function HideHotels() { $("#divloading").show(); $("#main-content").hide(); }
        $(document).ready(function () {
            $("#ModifySearch").click(function () {
                $("#Modsearch").slideDown();
            });
            $("#mdclose").click(function () {
                $("#Modsearch").slideUp();
            });
        });
    </script>


</asp:Content>

