<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="Result.aspx.vb"
    Inherits="FlightDom_FltResult" MasterPageFile="~/MasterAfterLogin.master" ValidateRequest="false" %>

<%--<%@ Register Src="~/UserControl/FltSearch.ascx" TagPrefix="uc1" TagName="FltSearch" %>--%>
<%@ Register Src="~/UserControl/FltSearchmdf.ascx" TagPrefix="uc1" TagName="FltSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Styles/flightsearch.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" type="text/css" />
    <%--    <link href="<%= ResolveUrl("css/itz.css") %>" rel="stylesheet" type="text/css" />--%>

    <%--    <link href="../Custom_Design/css/my_design.css" rel="stylesheet" />--%>
    <link href="../icofont/icofont.css" rel="stylesheet" />
    <link href="../icofont/icofont.min.css" rel="stylesheet" />

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


    <style type="text/css">

        .bdr-fltdet {
            border: 1px solid #ccc;
    padding: 17px;
    border-radius: 10px;
        }

        .one-way-select {
            position: fixed;
            bottom: 0;
            z-index: 999999;
            background: #eee;
            width: 100%;
            left: 0;
            right: 0;
            padding: 25px;
            box-shadow: 0px 1px 10px 1px rgb(6 6 6 / 58%);
        }



        .new-fare-details {
            width: 100%;
            font-size: 12px;
            padding: 12px 0;
            float: left;
            margin: 0px;
        }

        .new-fare-d-1 {
            width: 35%;
            padding: 0px;
            float: left;
            margin: 0;
            border: 1px solid #d9d9d9;
            display: table;
            font-size: 13px;
            font-weight: 600;
        }

        .nw-b2b-fared {
            margin: 0px;
            padding: 0px 0px 5px;
            float: left;
            border-bottom: 1px solid #d9d9d9;
            width: 100%;
        }

        .nw-pad-b2 {
            padding: 10px;
            float: left;
            width: 92%;
        }

        .nw-far-1 {
            margin: 0px;
            padding: 0px;
            width: 58%;
            font-weight: 600;
            float: left;
        }

        .nw-far-2 {
            margin: 0px;
            padding: 0px;
            width: 40%;
            float: right;
            text-align: right;
        }

        .nw-b2b-fared {
            margin: 0px;
            padding: 0px 0px 5px;
            float: left;
            border-bottom: 1px solid #d9d9d9;
            width: 100%;
        }

        .can-bnew-rg {
            width: 60%;
            float: right;
            margin: 0px;
            padding: 0px;
        }

        .can-b2b-tr {
            width: 100%;
            margin: 0 0;
            padding: 0px;
            float: left;
        }

        .lef-b2b-cane {
            width: 48%;
            padding: 0px;
            margin: 0px;
            float: left;
        }

        .b2b-ca-char {
            width: 100%;
            padding: 0 0 6px;
            font-size: 12px;
            font-weight: 600;
        }

        .b2b-can-tabe {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
            width: 100%;
            float: left;
            background: #fff;
        }

        .rig-b2b-cane {
            width: 48%;
            padding: 0px;
            margin: 0px;
            float: right;
        }

        .tr-b2b-cancgr {
            width: 100%;
            float: left;
            margin: 0px;
            padding: 0 0 0;
        }

        .trm-had {
            margin: 0px;
            padding: 10px 0;
            font-size: 18px;
            font-weight: 600;
        }

        .terms-b2b2-cancahe {
            font-size: 10px;
            color: #000;
            padding: 0;
            height: 67px;
            overflow-x: hidden;
        }
    </style>

    <style type="text/css">
        .falsebookbutton {
            display: inline-block;
            /*background: #ff7e00;*/
            background: rgb(255 192 0);
            border: rgb(255 192 0);
            color: #fff;
            text-align: center;
            padding: 10px;
            border-radius: 5px;
        }

            .falsebookbutton:hover {
                background: #797979;
                border: #797979;
            }

        .buttonfltbk {
            display: inline-block;
            background: #e60000;
            border: #ff3131;
            color: #fff;
            text-align: center;
            padding: 10px;
            border-radius: 5px;
        }

            .buttonfltbk:hover {
                background: #797979;
                border: #797979;
            }

        #loading-msg {
            width: 100%;
            position: absolute;
            left: 0;
            /*bottom: 220px;*/
            text-align: center;
            color: #333;
        }
    </style>

    <!--Tabs css-->

    <style type="text/css">
        .tabs {
            max-width: 100% !important;
        }

        .tabs-nav li {
            float: left !important;
            width: 25% !important;
        }

            .tabs-nav li:first-child a {
                border-right: 0 !important;
                border-top-left-radius: 6px !important;
            }

            .tabs-nav li:last-child a {
                border-top-right-radius: 6px !important;
            }

        .tb-btn {
            background: #eaeaed;
            border: 1px solid #cecfd5;
            color: #0087cc;
            display: block;
            font-weight: 600;
            padding: 10px 0;
            text-align: center;
            text-decoration: none;
        }

            .tb-btn:hover {
                color: #ff7b29;
            }

        .tab-active, .tb-btn {
            background: #fff !important;
            border-bottom-color: transparent !important;
            color: #2db34a !important;
            cursor: default !important;
        }

        .tabs-stage {
            /*border: 1px solid #cecfd5 !important;*/
            border-radius: 0 0 6px 6px !important;
            border-top: 0 !important;
            clear: both !important;
            /*padding: 24px 30px !important;*/
            position: relative !important;
            top: -1px !important;
        }
    </style>


    <style type="text/css">
        .modal-window {
            position: fixed;
            background-color: rgb(0 0 0 / 25%);
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 999;
            visibility: hidden;
            opacity: 0;
            pointer-events: none;
            -webkit-transition: all 0.3s;
            transition: all 0.3s;
        }

            .modal-window:target {
                visibility: visible;
                opacity: 1;
                pointer-events: auto;
            }

            .modal-window > div {
                width: 100%;
                position: absolute;
                top: 50%;
                left: 50%;
                -webkit-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
                padding: 2em;
                background: #ffffff;
            }

            .modal-window header {
                font-weight: bold;
            }

            .modal-window h1 {
                font-size: 150%;
                margin: 0 0 15px;
            }

        .modal-close {
            color: #aaa;
            line-height: 50px;
            font-size: 80%;
            position: absolute;
            right: 0;
            text-align: center;
            top: 0;
            width: 70px;
            text-decoration: none;
        }

            .modal-close:hover {
                color: black;
            }

        /* Demo Styles */



        a {
            color: inherit;
        }



        .modal-window div:not(:last-of-type) {
            margin-bottom: 15px;
        }

        small {
            color: #aaa;
        }

        .btn {
            background-color: #ff0054;
            padding: 1em 1.5em;
            border-radius: 3px;
            text-decoration: none;
        }

            .btn i {
                padding-right: 0.3em;
            }
    </style>


    <style type="text/css">
        .ico11 {
            font-size: 11px;
        }

        .padB5 {
            padding-bottom: 5px;
        }

        .db {
            display: block;
        }

        .fl {
            float: left;
        }

        @media (min-width: 992px) .padLR0 {
            padding-left: 0;
            padding-right: 0;
        }

        .padLR0 {
            padding-left: 0;
            padding-right: 0;
        }

        .alignItemsEnd {
            -webkit-align-items: flex-end;
            -ms-align-items: flex-end;
            align-items: flex-end;
        }

        .justifyBetween {
            -webkit-justify-content: space-between;
            -ms-justify-content: space-between;
            justify-content: space-between;
        }

        .dF {
            display: -webkit-flex;
            display: -ms-flexbox;
            display: -ms-flex;
            display: flex;
        }

        .borderRight {
            border-right: 1px solid #e6e6e6;
        }

        .width50 {
            width: 50%;
        }

        .flightImagesNew {
            max-height: 30px;
        }

        .flightImagesNew {
            max-width: 100%;
            max-height: 30px;
            padding-right: 10px;
        }

        .flightImagesNew {
            max-width: 100%;
            max-height: 30px;
        }
    </style>

    <link href="CSS/newcss/main.css" rel="stylesheet" />

    <style type="text/css">
        .backdrop {
            background-color: #fff;
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
        }

        .spinner {
            /* position: absolute;*/
            /*top: calc(50% - 12.5px);
            left: calc(35% - 25px);*/
            /*width: 50px;*/
            /*height: 50px;*/
            /*     border-top: 8px solid aliceblue;
            border-right: 8px solid aliceblue;
            border-bottom: 8px solid aliceblue;
            border-left: 8px solid #8c618d;
            border-radius: 50%;
            animation-name: spin;
            animation-duration: 3s;
            animation-iteration-count: infinite;
            animation-timing-function: linear;*/
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
                border-left: 8px solid deeppink;
            }

            25% {
                transform: rotate(360deg);
                border-left: 8px solid gold;
            }

            50% {
                transform: rotate(720deg);
                border-left: 8px solid palegreen;
            }

            75% {
                transform: rotate(1080deg);
                border-left: 8px solid aqua;
            }

            100% {
                transform: rotate(1440deg);
                border-left: 8px solid deeppink;
            }
        }

        .logo {
            position: absolute;
            top: calc(50% + 35px);
            left: calc(50% - 25px);
            font-family: sans-serif;
            color: gray;
            letter-spacing: 0.1em;
        }
    </style>


    <style type="text/css">
        .pad {
            padding-right: 2px;
            padding-left: 2px;
        }

        .f16 {
            color: #000;
            font-family: Lato-Bold,Arial,Helvetica,sans-serif;
            font-size: 11px;
            position: relative;
            /*margin-bottom: 5px;*/
            font-weight: bold;
        }


        .spn {
            color: red;
            background: #eee !important;
            padding: 4px;
            border-radius: 4px;
            cursor: pointer;
        }

        .spn1 {
            color: red;
            background: #525252;
            padding: 4px;
            border-radius: 4px;
            cursor: pointer;
        }

        .spnBtnShow {
            background: #ffcfcf;
            padding: 2px;
            border-radius: 2px;
        }

        .spnBtnHide {
            background: #cfffd7;
            padding: 2px;
            border-radius: 2px;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>



    <script type="text/javascript">
        // Show the first tab by default
        $('.tabs-stage div').hide();
        $('.tabs-stage div:first').show();
        $('.tabs-nav li:first').addClass('tab-active');

        // Change tab class and display content
        $('.tabs-nav a').on('click', function (event) {
            event.preventDefault();
            $('.tabs-nav li').removeClass('tab-active');
            $(this).parent().addClass('tab-active');
            $('.tabs-stage div').hide();
            $($(this).attr('href')).show();
        });
    </script>




    <script type="text/javascript">
        $(document).ready(function (e) {
            $(".img-check").click(function () {
                $(this).toggleClass("check");
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#flip").click(function () {
                $("#panel").slideToggle();



            });
        });
    </script>




    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("#flip2").click(function () {
        //        $("#panel1").slideToggle();
        //        displayFDt();


        //    });
        //});


        function newwin(id) {

            debugger;
            $("#panel_" + id + "").slideToggle();
        }
    </script>

    <script type="text/javascript">
        $('.openmodale').click(function (e) {
            e.preventDefault();
            $('.modale').addClass('opened');
        });
        $('.closemodale').click(function (e) {
            e.preventDefault();
            $('.modale').removeClass('opened');
        });
    </script>


    <div class="theme-hero-area front">
        <div class="theme-hero-area-bg-wrap">
            <div class="theme-hero-area-bg theme-hero-area-bg-blur" style="background-color: #672b00; filter: none"></div>
            <div class="theme-hero-area-mask theme-hero-area-mask-half"></div>
        </div>
        <div class="theme-hero-area-body" style="padding: 13px;">
            <div class="container">
                <div class="theme-search-area _mob-h theme-search-area-white">
                    <div class="row" data-gutter="10">
                        <%--  <div class="col-md-2 back-search" style="margin-top: 13px;">
                           <a href="#" onclick="history.back(-1)" class="" style="background: #e8e8e8; margin-top: 1px; color: #000;padding: 6px;border-radius: 25px;">Back to Search</a>

                        </div>--%>

                        <div id="displaySearchinput" class="lft">
                        </div>

                        <div class="col-md-2">
                            <a href="#" id="show-hide" class="theme-search-area-submit _tt-uc theme-search-area-submit-curved theme-search-area-submit-sm theme-search-area-submit-no-border theme-search-area-submit-primary" style="background: #e8e8e8; margin-top: 1px; color: #000;">Modify Search</a>
                            <div class="row mob-filter" style="position: absolute; right: 0; top: -25px;">
                                <div class="col-xs-2"><a href="#" id="show-hide-mob"><i class="icofont-ui-edit"></i></a></div>
                                <div class="col-xs-2"><a href="#" id="show-filter"><i class="icofont-filter"></i></a></div>
                            </div>
                        </div>

                    </div>
                    <div class="re-search" style="display: none; overflow: visible !important;">
                        <div id="fltrDiv">
                            <div id="searchtext" class="clear passenger">
                                <div class="container">
                                    <uc1:FltSearch runat="server" ID="FltSearch" />
                                </div>


                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="lft bld colormn " style="display: none;">
        <div id="divSearchText1">
        </div>
    </div>

    <div id="FilterLoad"></div>

    <div class="w100" id="toptop">
        <div id="MainSFR">
            <div id="MainSF">
                <div class="w100">
                    <input type="checkbox" class="chkNonstop" style="display: none" name="chkNonstop" id="chkNonstop" value="Y" />

                    <div class="row row-col-static" id="lftdv1" data-gutter="20" style="padding: 12px;">



                        <div class="col-md-3 ">
                            <div class="sticky-col _mob-h1">
                                <div class="theme-search-results-sidebar">
                                    <div class="theme-search-results-sidebar-sections _mb-20 _br-2 theme-search-results-sidebar-sections-white-wrap">

                                        <span class="hidden-md hidden-lg hidden-sm fliters hidden" onclick="openNav()"><i class="fa fa-filter" aria-hidden="true"></i>Filter</span>
                                        <div id="mysidenavssss" class="sidenavssss">
                                            <%--                                <a href="javascript:void(0)" class="closebtnsss" onclick="closeNav()">X </a>--%>
                                            <div id="FilterBox lft">
                                                <div class="jplist-panel">
                                                    <div class="passengersss  wht w210 lft asbc lftflt " id="passengersss">
                                                        <div class="clear">
                                                        </div>
                                                        <div id="dsplm" class="large-12 medium-12 small-12 columns">
                                                            <%--<input type="button" id="ModifySearch" value="Modify Search" onclick="DiplayMsearch1('DivMsearch');" class="pnday daybtn f10" />--%>



                                                            <a href="#" id="hide-filter" style="position: absolute; left: 31px; font-size: 20px;">X</a>
                                                            <a href="#" class="" data-control-type="reset" data-control-name="reset" data-control-action="reset" style="float: right; margin-right: 9px; color: orange;">Reset All </a>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div class="w95 auto OnewayReturn" style="padding-top: 10px;">


                                                            <div class="large-12 medium-12 small-12 columns passenger">
                                                                <%--<button type="button" class="jplist-reset-btn cursorpointer bld" data-control-type="reset" data-control-name="reset" data-control-action="reset" style="border: none; background: none;">
                                     <img src="../images/reset.png" style="position: relative; top: 3px;" />  &nbsp; Reset All Filters
                                        
                                    </button>--%>
                                                            </div>


                                                            <div class="theme-search-results-sidebar-section">
                                                                <div id="flterTab" style="display: none;">
                                                                    <div style="display: flex">
                                                                        <div id="flterTabO" class="spn1">
                                                                            Outbound
                                                                        </div>
                                                                        <div class="lft w2">&nbsp;</div>
                                                                        <div id="flterTabR" class="spn">
                                                                            Inbound
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>



                                                            <div class="theme-search-results-sidebar-section" id="FltrPrice">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBP">Price</h5>

                                                                <div class="theme-search-results-sidebar-section-price">
                                                                    <div id="FBP1" class="w100 lft ">
                                                                        <div class="clear2"></div>
                                                                        <div class="fo">
                                                                            <div class="clsone">
                                                                                <div class="jplist-range-slider" data-control-type="range-slider" data-control-name="range-slider"
                                                                                    data-control-action="filter" data-path=".price">
                                                                                    <div class="clear1"></div>
                                                                                    <div class="ui-slider w90 mauto" data-type="ui-slider">
                                                                                    </div>
                                                                                    <div class="clear1"></div>
                                                                                    <div style="margin-top: 10px;">
                                                                                        <div style="float: left">
                                                                                            <span class="lft">₹&nbsp; <span class="value lft" data-type="prev-value"></span></span>

                                                                                        </div>
                                                                                        <div style="float: right">

                                                                                            <span class="rgt">₹&nbsp;<span class="value rgt" data-type="next-value"></span></span>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="hidden" data-control-type="default-sort" data-control-name="sort" data-control-action="sort"
                                                                                    data-path=".price" data-order="asc" data-type="number">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="fr">
                                                                            <div class="jplist-range-slider" data-control-type="range-sliderR" data-control-name="range-slider"
                                                                                data-control-action="filter" data-path=".price">
                                                                                <div class="clear1"></div>
                                                                                <div class="ui-slider w90 mauto" data-type="ui-slider">
                                                                                </div>
                                                                                <div class="clear1"></div>
                                                                                <div class="lft w45" style="float: left;">
                                                                                    <span class="lft">₹&nbsp;</span>
                                                                                    <span class="value lft" data-type="prev-value"></span>
                                                                                </div>
                                                                                <div class="rgt " style="float: right;">

                                                                                    <span class="rgt">₹&nbsp; </span>
                                                                                    <span class="value rgt" data-type="next-value"></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="hidden" data-control-type="default-sort" data-control-name="sort" data-control-action="sort"
                                                                                data-path=".price" data-order="desc" data-type="number">
                                                                            </div>
                                                                        </div>

                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="theme-search-results-sidebar-section" id="fltrTime">

                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBDT">Departure Time
                                                                </h5>
                                                                <div id="FBDT1" class="w100 lft" style="overflow: hidden;">
                                                                    <div class="fo">
                                                                        <div class="jplist-group prc_val ft1-sec takeoffTime fltTmg" data-control-type="DTimefilterO" data-control-action="filter" data-control-name="DTimefilterO" data-path=".dtime" data-logic="or" style="position: relative;">
                                                                            <div class="tm-dt1">
                                                                                <div class="tm11">
                                                                                    <div class="tm-m11 ftRst">
                                                                                        <div class="mor-n1 abc1 bdrs ftRstM">
                                                                                            <input value="0_6" id="CheckboxT1" type="checkbox" title="Early Morning" style="display: none;" />
                                                                                            <label for="CheckboxT1"><i class="icofont-sun-rise"></i>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                Before
                                                                            <br>
                                                                                                6 AM
                                                                                            </div></label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm22">
                                                                                    <div class="tm-m12 ftRst">

                                                                                        <div class="mor1-n2 abc2t bdrs ftRstM">
                                                                                            <input value="6_12" id="CheckboxT2" type="checkbox" title="Morning" style="display: none;" />
                                                                                            <label for="CheckboxT2"><i class="icofont-sun"></i>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                6 AM -<br>
                                                                                                12 PM
                                                                                            </div>
                                                                                                </label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm33">
                                                                                    <div class="tm-m22 ftRst">
                                                                                        <div class="mor2-n3 abc3t bdrs ftRstM">
                                                                                            <input value="12_18" id="CheckboxT3" type="checkbox" title="Mid Day" style="display: none;" />
                                                                                            <label for="CheckboxT3"><i class="icofont-sun-set"></i>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                12 PM -<br>
                                                                                                6 PM
                                                                                            </div>
                                                                                                </label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm11-n">
                                                                                    <div class="tm-m33 ftRst">
                                                                                        <div class="mor3-n4 abc4t bdrs ftRstM">
                                                                                            <input value="18_0" id="CheckboxT4" type="checkbox" title="Evening" style="display: none;" />
                                                                                            <label for="CheckboxT4"><i class="icofont-night"></i>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                After
                                                                            <br>
                                                                                                6 PM
                                                                                            </div>
                                                                                                </label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="fr" style="display: none;">
                                                                        <div class="jplist-group prc_val ft1-sec takeoffTime fltTmg" data-control-type="DTimefilterR" data-control-action="filter" data-control-name="DTimefilterR" data-path=".atime" data-logic="or">

                                                                            <div class="tm-dt1">
                                                                                <div class="tm11">
                                                                                    <div class="tm-m11 ftRst">
                                                                                        <div class="mor-n1 abc1 bdrs ftRstM">
                                                                                            <input value="0_6" id="CheckboxT1R" type="checkbox" title="Early Morning" style="display: none;" />
                                                                                            <label for="CheckboxT1R"></label>


                                                                                            <div class="fil-sbtxt">
                                                                                                Before
                                                                            <br>
                                                                                                6 AM
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm22">
                                                                                    <div class="tm-m12 ftRst">

                                                                                        <div class="mor1-n2 abc2t bdrs ftRstM">
                                                                                            <input value="6_12" id="CheckboxT2R" type="checkbox" title="Morning" style="display: none;" />
                                                                                            <label for="CheckboxT2R"></label>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                6 AM -<br>
                                                                                                12 PM
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm33">
                                                                                    <div class="tm-m22 ftRst">
                                                                                        <div class="mor2-n3 abc3t bdrs ftRstM">
                                                                                            <input value="12_18" id="CheckboxT3R" type="checkbox" title="Mid Day" style="display: none;" />
                                                                                            <label for="CheckboxT3R"></label>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                12 PM -<br>
                                                                                                6 PM
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tm11-n">
                                                                                    <div class="tm-m33 ftRst">
                                                                                        <div class="mor3-n4 abc4t bdrs ftRstM">
                                                                                            <input value="18_0" id="CheckboxT4R" type="checkbox" title="Evening" style="display: none;" />
                                                                                            <label for="CheckboxT4R"></label>

                                                                                            <div class="clr"></div>
                                                                                            <div class="clr"></div>
                                                                                            <div class="fil-sbtxt">
                                                                                                After
                                                                            <br>
                                                                                                6 PM
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <%--<div class="large-12 medium-12 small-12 columns" id="fltrTime" >
                                                
                                                <div class="bld closeopen" onclick="fltrclick(this.id)" id="FBDT"><i class="fa fa-clock-o" aria-hidden="true"></i>  Departure Time</div>
                                                <div id="FBDT1" class="w100 lft">
                                                    <div class="fo">
                                                        <div class="jplist-group"
                                                            data-control-type="DTimefilterO"
                                                            data-control-action="filter"
                                                            data-control-name="DTimefilterO"
                                                            data-path=".dtime" data-logic="or">
                                                            <div class="clear"></div>

                                                            <div class="lft wss20 abc1 bdrs bdrss" >
                                                                <input value="0_6" id="CheckboxT1" type="checkbox" title="Early Morning" />
                                                                <label for="CheckboxT1"></label>
                                                                <span>Early Morning</span><span style="float:right;">00 - 06</span>
                                                            </div>
                                                            <div class="lft wss20 abc2t bdrs">

                                                                <input value="6_12" id="CheckboxT2" type="checkbox" title="Morning" />
                                                                <label for="CheckboxT2"></label>
                                                                <span>Morning</span><span style="float:right;">06 - 12</span>
                                                            </div>

                                                            <div class="lft wss20 abc3t bdrs">
                                                                <input value="12_18" id="CheckboxT3" type="checkbox" title="Mid Day" />
                                                                <label for="CheckboxT3"></label>
                                                                <span>Mid-Day</span><span style="float:right;">12 - 18</span>
                                                            </div>
                                                            <div class="lft wss20 abc4t bdrs" >
                                                                <input value="18_0" id="CheckboxT4" type="checkbox" title="Evening" />
                                                                <label for="CheckboxT4"></label>
                                                                <span>Evening</span><span style="float:right;">18 - 00</span>
                                                            </div>

                                                            	
</div>


                                                        </div>
                                                    </div>
                                                    <div class="fr">
                                                        <div class="jplist-group"
                                                            data-control-type="DTimefilterR"
                                                            data-control-action="filter"
                                                            data-control-name="DTimefilterR"
                                                            data-path=".atime" data-logic="or">

                                                            <div class="lft wss20 abc1 bdrs">
                                                                <input value="0_6" id="CheckboxT1R" type="checkbox" title="Early Morning" />
                                                                <label for="CheckboxT1"></label>
                                                                <span>00 - 06</span>
                                                            </div>
                                                            <div class="lft wss20 abc2t bdrs">
                                                                <input value="6_12" id="CheckboxT2R" type="checkbox" title="Morning" />
                                                                <label for="CheckboxT2"></label>
                                                                <span>06 - 12</span>
                                                            </div>

                                                            <div class="lft wss20 abc3t bdrs">
                                                                <input value="12_18" id="CheckboxT3R" type="checkbox" title="Mid Day" />
                                                                <label for="CheckboxT3"></label>
                                                                <span>12 - 18</span>
                                                            </div>
                                                            <div class="lft wss20 abc4t bdrs">
                                                                <input value="18_0" id="CheckboxT4R" type="checkbox" title="Evening" />
                                                                <label for="CheckboxT4"></label>
                                                                <span>18 - 00</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="clear">
                                                    </div>
                                                </div>--%>
                                                            <div class="clear">
                                                            </div>

                                                            <hr />

                                                            <div class="theme-search-results-sidebar-section">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBA">Airline
                                                                </h5>
                                                                <div class="w100 lft " id="FBA1">

                                                                    <div id="airlineFilter" class="fo theme-search-results-sidebar-section-checkbox-list">
                                                                        <div class="jplist-group" data-control-type="AirlinefilterO" data-control-action="filter" data-control-name="AirlinefilterO" data-path=".airlineImage" data-logic="or">


                                                                            <div class="lft w8">
                                                                                <input value="Air Asia" id="CheckboxAO01" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="Air Asia">Air Asia</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="Air India" id="CheckboxAO11" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="Air India">Air India</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="Goair" id="CheckboxAO21" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="Goair">Goair</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="Indigo" id="CheckboxAO31" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="Indigo">Indigo</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="SpiceJet" id="CheckboxAO41" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="SpiceJet">SpiceJet</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="Vistara" id="CheckboxAO51" type="checkbox">
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px;">
                                                                                <label for="Vistara">Vistara</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="airlineFilterR" class="fr theme-search-results-sidebar-section-checkbox-list"></div>
                                                                    <div class="clear"></div>
                                                                </div>

                                                            </div>
                                                            <hr />
                                                            <div class="theme-search-results-sidebar-section">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBS">Stops
                                                                </h5>
                                                                <div class="w100 lft" id="FBS1">
                                                                    <div class="clear"></div>
                                                                    <div id="stopFlter" class="fo theme-search-results-sidebar-section-checkbox-list"></div>
                                                                    <div id="stopFlterR" class="fr theme-search-results-sidebar-section-checkbox-list"></div>
                                                                </div>

                                                            </div>


                                                            <hr />

                                                            <div class="theme-search-results-sidebar-section" style="display: none;">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBFT">Fare Rule
                                                                </h5>

                                                                <div class="w100 lft" id="FBFT1">
                                                                    <div class="fo">
                                                                        <div class="jplist-group"
                                                                            data-control-type="RfndfilterO"
                                                                            data-control-action="filter"
                                                                            data-control-name="RfndfilteO"
                                                                            data-path=".rfnd" data-logic="or" style="left: 12px; position: relative;">
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="r" id="CheckboxR1" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxR1">Refundable</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="n" id="CheckboxR2" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxR2">Non Refundable</label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="fr">
                                                                        <div class="jplist-group"
                                                                            data-control-type="RfndfilterR"
                                                                            data-control-action="filter"
                                                                            data-control-name="RfndfilterR"
                                                                            data-path=".rfnd" data-logic="or">
                                                                            <div class="clear2"></div>
                                                                            <div class="lft w8">
                                                                                <input value="r" id="Checkbox1" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxR1">Refundable</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="n" id="Checkbox2" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxR2">Non Refundable</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <%--<hr />--%>
                                                            <div class="theme-search-results-sidebar-section" style="display: none;">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="FBTY">Fare Type</h5>
                                                                <div class="theme-search-results-sidebar-section-checkbox-list" id="FBTY1">
                                                                    <div class="fo">
                                                                        <div class="jplist-group"
                                                                            data-control-type="FareTypefilterO"
                                                                            data-control-action="filter"
                                                                            data-control-name="FareTypefilterO"
                                                                            data-path=".srf" data-logic="or">
                                                                            <div class="clear"></div>

                                                                            <div class="lft w80">
                                                                                <label for="CheckboxFTY1" style="width: 100%;">
                                                                                    <input value="NRMLF" id="CheckboxFTY1" type="checkbox" />&nbsp;Normal Fare
                                                                                </label>
                                                                            </div>
                                                                            <div class="clear"></div>

                                                                            <div class="lft w80">
                                                                                <label for="CheckboxFTY2" style="width: 100%;">
                                                                                    <input value="SRF" id="CheckboxFTY2" type="checkbox" />&nbsp;Special Return Fare
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="fr">
                                                                        <hr />
                                                                        <br />
                                                                        <div class="jplist-group"
                                                                            data-control-type="FareTypefilterR"
                                                                            data-control-action="filter"
                                                                            data-control-name="FareTypefilterR"
                                                                            data-path=".srf" data-logic="or">
                                                                            <div class="clear2"></div>
                                                                            <div class="lft w8">
                                                                                <input value="NRMLF" id="CheckboxFTYR1" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxFTYR1">Normal Fare</label>
                                                                            </div>
                                                                            <div class="clear"></div>
                                                                            <div class="lft w8">
                                                                                <input value="SRF" id="CheckboxFTYR2" type="checkbox" />
                                                                            </div>
                                                                            <div class="lft w80" style="padding-top: 3px">
                                                                                <label for="CheckboxFTYR2">Special Return Fare</label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="clear"></div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>

                                                            </div>

                                                            <%--<hr />--%>


                                                            <div class="theme-search-results-sidebar-section">
                                                                <h5 class="theme-search-results-sidebar-section-title" onclick="fltrclick(this.id)" id="DAFT">Airline Fare Type
                                                                </h5>
                                                                <div class="theme-search-results-sidebar-section-checkbox-list" id="DAFT1">
                                                                    <div class="clear"></div>
                                                                    <div id="AirlineFareType" class="fo FareTypeO theme-search-results-sidebar-section-checkbox-list"></div>
                                                                    <div id="AirlineFareTypeR" class="fr FareTypeR theme-search-results-sidebar-section-checkbox-list"></div>
                                                                    <div class="clear1">
                                                                    </div>

                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="w95 auto SpecialRTF" id="divFilterRTF" style="padding-top: 10px; display: none;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6-5 ">
                            <div class="jplist-panel">


                                <div class="clear"></div>

                                <div class="lft" id="refinetitle" style="display: none;"></div>

                                <div id="divMatrixRtfO" class="divMatrix w100"></div>

                                <div id="RoundTripH" class="flightbox">
                                    <div class="row">
                                        <div class="laegr-12 medium-12 small-18" style="display: none;">
                                            <div class="laegr-6 medium-6 small-6 columns">
                                                <div style="border: 1px solid #fff; box-shadow: 0 1px 2px 0 #ccc; background: #005999; color: #fff; min-height: 60px; width: 98%; padding: 17px 10px 0px 11px; height: auto; margin-bottom: 8px; transition: box-shadow 200ms cubic-bezier(.4, 0, .2, 1); border: 1px solid rgba(0,0,0,0.125); border-radius: 4px;">
                                                    <div id="RTFTextFrom" class="lft destination1"></div>

                                                </div>
                                            </div>
                                            <div class="laegr-6 medium-6 small-6 columns">
                                                <div style="border: 1px solid #fff; box-shadow: 0 1px 2px 0 #ccc; background: #005999; color: #fff; min-height: 60px; width: 98%; padding: 17px 10px 0px 11px; height: auto; margin-bottom: 8px; transition: box-shadow 200ms cubic-bezier(.4, 0, .2, 1); border: 1px solid rgba(0,0,0,0.125); border-radius: 4px;">

                                                    <div id="RTFTextTo" class="lft destination1"></div>

                                                    <div class="rgt hidden-xs" style="margin-right: 13px;">
                                                        <div class="auto lft">
                                                            <span onclick="ShowHideDiscount('show');" class="spnBtnShow" style="cursor: pointer;">Show&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                            <span onclick="ShowHideDiscount('hide');" style="display: none; cursor: pointer;" class="spnBtnHide">Hide&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>




                                    <div class="nav-container">

                                        <div class="nav">




                                            <div class="jplist-panel" style="position: fixed; bottom: 0; width: 100%; border: 1px solid #eee; right: 0; left: 0; z-index: 109; background: #002645;">


                                                <div id="fltselct" style="display: none;">
                                                    <%-- <div class="f16 w30 lft bld">Your Selection</div>--%>
                                                    <div id="fltbtn" class="w70 rgt">




                                                        <div class="bld w33 lft difffare" id="FareDiff"></div>

                                                        <div class="lft w60 msg1" id="msg1">
                                                            PLEASE SELECT ONE<br />
                                                        </div>



                                                        <div id="Divproc" class="bld" style="display: none;">
                                                            <img alt="Booking In Progress" src="~/Images/loading_bar.gif" />
                                                        </div>
                                                        <div class="gridViewToolTip1 hide" id="fareBrkup" title="">ss</div>
                                                    </div>

                                                    <div class="detls">


                                                        <div id="selctcntnt" class="mauto" style="display: flex!important;">
                                                            <div class="flexCol width70 justifyEnd">
                                                                <div class="ico12 fb padB10 quicks" style="margin-bottom: 10px; color: #fff;">Your Selection</div>
                                                                <div class="dF">
                                                                    <div class="dF justifyBetween alignItemsEnd  padLR0 borderRight">

                                                                        <div id="fltgo" class="sec1" style="border-right: 1px solid #aeaeae; color: #fff; width: 355px;"></div>

                                                                    </div>

                                                                    <div class="dF justifyBetween alignItemsEnd padLR0 borderRight">
                                                                        <div id="fltbk" class="sec1" style="border-right: 1px solid #aeaeae; color: #fff; width: 355px;">
                                                                        </div>
                                                                    </div>



                                                                    <div class="col-md-4" style="color: #fff;">
                                                                        <div class="">

                                                                            <div class="">
                                                                                <div class="bld w33 lft prevfare" id="prevfare" style="font-size: 13px; color: #fff;"></div>
                                                                            </div>


                                                                            <div class="">
                                                                                <div class="bld w28 lft currentfare" id="totalPay" style="font-size: 13px; color: #fff;"></div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2" style="display: none;">
                                                                        <div class="gridViewToolTipSRF bld w5 lft" id="showfare">
                                                                            <%--                                                                <img src='<%= ResolveClientUrl("~/images/icons/faredetails.png")%>' style="padding-top: 5px; padding-left: 10px; cursor: pointer;position:absolute;" alt="" />--%>
                                                                            <a type="button" class="btn btn-default">Fare Breakup</a>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="col-md-3">
                                                                            <input type="button" class="detls1 falsebookbutton" value="Book" id="FinalBook" style="float: left;" />
                                                                        </div>
                                                                        <%-- <div class="col-md-2">
                                                            <input type="button" class="detls1" value="Show/Hide" style="Float: left;margin-left: 10px;" />
                                                                </div>--%>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <%-- <div class="clearfix"> </div>--%>
                                                </div>

                                            </div>
                                            <div class="">
                                                <div class="laegr-12 medium-12 small-18">
                                                    <!--headrow-->
                                                    <div class="theme-search-results-sort _mob-h clearfix col-md-6 col-xs-6" style="padding: 0px 0px !important;">
                                                        <div class="col-md-12 net-fare" style="position: absolute;">
                                                            <i class="fa fa-plane" aria-hidden="true" style="transform: rotate(225deg);"></i><a href="#" id="RtfFromPrevDay" class="pnday lft" style="color: #000; font-size: 10px;"><span id="op-dt" style="font-size: 10px;"></span></a>
                                                            <a href="#" id="RtfFromNextDay" class="pnday lft" style="color: #000; font-size: 10px; float: right"><span id="on-dt" style="font-size: 10px;"></span><i class="fa fa-plane" aria-hidden="true" style="transform: rotate(45deg)"></i></a>
                                                        </div>
                                                        <ul class="theme-search-results-sort-list" style="margin-left: 0px !important; margin-top: 24px;">
                                                            <li class="srtarw abdd" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=" "
                                                                    data-control-type="sortAirline"
                                                                    data-control-name="sortAirline"
                                                                    data-control-action="sort"
                                                                    data-path=".airlineImage"
                                                                    data-order="asc"
                                                                    data-type="text" style="padding: 0 22px; font-size: 10px;">Airline 
                                                                       <%-- <span>A → Z</span>--%>
                                                                </a>

                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=" "
                                                                    data-control-type="sortDeptime"
                                                                    data-control-name="sortDeptime"
                                                                    data-control-action="sort"
                                                                    data-path=".deptime"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Depart 
                                                                            <%--<span>Min- → Max</span>--%>
                                                                </a>


                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=" "
                                                                    data-control-type="sortTotdur"
                                                                    data-control-name="sortTotdur"
                                                                    data-control-action="sort"
                                                                    data-path=".totdur"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Duration 
                                                                            <%--<span>Min → Max</span>--%>
                                                                </a>

                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class="  "
                                                                    data-control-type="sortArrtime"
                                                                    data-control-name="sortArrtime"
                                                                    data-control-action="sort"
                                                                    data-path=".arrtime"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Arrival 

                                                                            <%--<span>Short → Long</span>--%>
                                                                </a>

                                                            </li>

                                                            <li class="bdd srtarw hidden-xs net-fare" onclick="myfunction(this)">
                                                                <a
                                                                    class=" "
                                                                    data-control-type="sortCITZ1"
                                                                    data-control-name="sortCITZ1"
                                                                    data-control-action="sort"
                                                                    data-path=".price"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Fare 
                                                                        <%--<span>Low → High</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="net-fare">
                                                                <span onclick="ShowHideDiscount('show');" class="spnBtnShow" style="cursor: pointer; color: #000; z-index: 1011; font-size: 10px;">Show</span>
                                                                <span onclick="ShowHideDiscount('hide');" style="display: none; cursor: pointer; color: #000; font-size: 10px;" class="spnBtnHide">Hide</span>
                                                            </li>

                                                        </ul>



                                                    </div>

                                                    <div class="theme-search-results-sort _mob-h clearfix col-md-6 col-xs-6" style="padding: 0px 0px !important;">

                                                        <div class="col-md-12 net-fare" style="position: absolute;">
                                                            <i class="fa fa-plane" aria-hidden="true" style="transform: rotate(225deg);"></i><a href="#" id="RtfToPrevDay" class="pnday lft" style="color: #000; font-size: 10px;"><span id="rp-dt" style="font-size: 10px;"></span></a>
                                                            <a href="#" id="RtfToNextDay" class="pnday lft" style="color: #000; font-size: 10px; float: right"><span id="rn-dt" style="font-size: 10px;"></span><i class="fa fa-plane" aria-hidden="true" style="transform: rotate(45deg)"></i></a>
                                                        </div>

                                                        <ul class="theme-search-results-sort-list" style="margin-left: 0px !important; margin-top: 24px;">

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=""
                                                                    data-control-type="sortAirlineR"
                                                                    data-control-name="sortAirlineR"
                                                                    data-control-action="sort"
                                                                    data-path=".airlineImage"
                                                                    data-order="asc"
                                                                    data-type="text" style="padding: 0 22px; font-size: 10px;">Airline 
                                                                        <%--<span>A → Z</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=""
                                                                    data-control-type="sortDeptimeR"
                                                                    data-control-name="sortDeptimeR"
                                                                    data-control-action="sort"
                                                                    data-path=".deptime"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Depart 
                                                                            <%--<span>Min → Max</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=""
                                                                    data-control-type="sortTotdurR"
                                                                    data-control-name="sortTotdurR"
                                                                    data-control-action="sort"
                                                                    data-path=".totdur"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Duration 
                                                                           <%-- <span>Min → Max</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="abdd srtarw" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=""
                                                                    data-control-type="sortArrtimeR"
                                                                    data-control-name="sortArrtimeR"
                                                                    data-control-action="sort"
                                                                    data-path=".arrtime"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Arrival
                                                                            <%--<span>Short → Long</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="abdd srtarw net-fare" onclick="myfunction(this)">
                                                                <a href="#"
                                                                    class=""
                                                                    data-control-type="sortCITZR"
                                                                    data-control-name="sortCITZR"
                                                                    data-control-action="sort"
                                                                    data-path=".price"
                                                                    data-order="asc"
                                                                    data-type="number" style="padding: 0 22px; font-size: 10px;">Fare
                                                                        <%--<span>Low → High</span>--%>
                                                                </a>
                                                            </li>

                                                            <li class="net-fare">
                                                                <span onclick="ShowHideDiscount('show');" class="spnBtnShow" style="cursor: pointer; color: #000; z-index: 1011; font-size: 10px;">Show</span>
                                                                <span onclick="ShowHideDiscount('hide');" style="display: none; cursor: pointer; color: #000; font-size: 10px;" class="spnBtnHide">Hide</span>
                                                            </li>

                                                        </ul>

                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>




                                        </div>
                                    </div>

                                    <div class="">

                                        <div class="col-md-6 col-xs-6 pad">
                                            <div id="divFrom1" class="listO w100">
                                                <%-- <div class="dvsrc">
                                        <img src="~/Images/fltloding.gif" />
                                    </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-xs-6 pad">
                                            <div id="divTo1" class="listR w100">
                                                <%--<div class="dvsrc textaligncenter">
                                        <img src="~/Images/fltloding.gif" />
                                    </div>--%>
                                            </div>

                                            <%--<div class="dvsrc textaligncenter">
                                    <img src="~/Images/fltloding.gif" />
                                </div>--%>
                                        </div>


                                    </div>
                                </div>
                                <div class="flightbox" style="display: block;" id="onewayH">
                                    <div class="" style="width: 99%;">



                                        <div class="jplist-panel passenger">
                                            <div id="DivLoadP" class="w100" style="margin: 0px 0px 16px 0px; display: none;"><span style="position: absolute; text-align: center; margin: 0 0 0 0; color: #61b3ff; width: 100%; padding: -3px;">We are processing, Please wait....</span></div>
                                            <%-- <div class="clspMatrix lft" id="clspMatrix"><span style="padding-left: 20px;">Matrix</span></div>--%>
                                            <div class="clear"></div>
                                            <div id="divMatrixRtfR" class="divMatrix" style="display: none"></div>
                                            <%--  <div class="clear1"></div>--%>
                                            <div id="divMatrix"></div>
                                            <%--<div class="clear1"></div>--%>
                                            <div class="w100 auto passenger" style="display: none;">
                                                <!--DELTPBOM-->
                                                <div class="w100 ptop10" style="background: #005999; padding: 1px 18px 0px 0px; border-radius: 4px; overflow: hidden; min-height: 60px; border: 1px solid #ccc; width: 99%;">
                                                    <div class="lft" style="margin-top: 16px;">


                                                        <%--<div id="displaySearchinput" class="lft"></div>--%>
                                                        <div class="lft" style="font-size: 12px; color: #fff; position: relative; top: 4px;">
                                                            <span style="color: #fff;">&nbsp;&nbsp;|</span>  <span id="spanShow" onclick="ShowHideDiscount('show');" class="spnBtnShow" style="cursor: pointer; color: #fff; z-index: 1011;">SHOW NET</span>
                                                            <span id="spanHide" onclick="ShowHideDiscount('hide');" style="display: none; cursor: pointer; color: #fff;" class="spnBtnHide">HIDE NET</span>
                                                        </div>
                                                        <%-- <div class="lft plft10">
                                                        <div class="lft ">
                                                            Del<br />
                                                            <span class="f10 txtgray">Fri 7 Mar</span>
                                                        </div>
                                                        <div class="lft">
                                                            <img src="../Images/rarrow.png" /></div>
                                                        <div class="lft plft10">
                                                            Bom<br />
                                                            <span class="f10 txtgray">Fri 7 Mar </span>
                                                        </div>


                                                    </div>
                                                    <div class="bdrdot lft">&nbsp;</div>

                                                    <div class="lft plft10">
                                                        <div class="lft ">
                                                            Del<br />
                                                            <span class="f10 txtgray">Fri 7 Mar</span>
                                                        </div>
                                                        <div class="lft">
                                                            <img src="../Images/arrow.png" /></div>
                                                        <div class="lft plft10">
                                                            Bom<br />
                                                            <span class="f10 txtgray">Fri 7 Mar </span>
                                                        </div>


                                                    </div>
                                                    <div class="bdrdot lft">&nbsp;</div>

                                                    <div class="lft plft10">
                                                        <div class="lft ">
                                                            Del<br />
                                                            <span class="f10 txtgray">Fri 7 Mar</span>
                                                        </div>
                                                        <div class="lft">
                                                            <img src="../Images/arrow.png" /></div>
                                                        <div class="lft plft10">
                                                            Bom<br />
                                                            <span class="f10 txtgray">Fri 7 Mar </span>
                                                        </div>


                                                    </div>
                                                    <div class="bdrdot lft">&nbsp;</div>



                                                    <div class="lft plft10">
                                                        <div class="lft ">
                                                            Del<br />
                                                            <span class="f10 txtgray">Fri 7 Mar</span>
                                                        </div>
                                                        <div class="lft">
                                                            <img src="../Images/arrow.png" /></div>
                                                        <div class="lft plft10">
                                                            Bom<br />
                                                            <span class="f10 txtgray">Fri 7 Mar </span>
                                                        </div>


                                                    </div>--%>
                                                    </div>
                                                    <%--<div id="RTFSAirMain" class="hide box-return">--%>
                                                    <div id="RTFSAirMain" class="box-return" style="display: none;">
                                                        <div class="w15 lft">&nbsp;</div>
                                                        <div class="bld underlineitalic colormn lft" style="display: none;">Special Return Fares</div>
                                                        <div id="splLoading">Loading......</div>
                                                        <div class="clear"></div>
                                                        <div id="RTFSAir">Loading.....</div>
                                                        <div class="clear"></div>
                                                    </div>

                                                    <div class="rgt passenger" id="prexnt" style="margin-top: 20px; color: #000;">
                                                    </div>

                                                </div>



                                                <%--<div class="lft padding1s cursorpointer clspMatrix" id="clspMatrix"></div>--%>


                                                <%-- <div class="lft w48" id="displaySearchinput">sdasd</div>--%>
                                            </div>
                                        </div>



                                    </div>
                                    <div class="nav-container">
                                        <div class="nav">

                                            <div class="jplist-panel">

                                                <div class="theme-search-results-sort _mob-h clearfix">
                                                    <%-- <h5 class="theme-search-results-sort-title">Sort by:</h5>--%>
                                                    <span class="theme-search-results-sort-title prv" style="position: absolute; margin-top: 8px; margin-left: -16px;"><a href="#" id="PrevDay" class="pnday" style="color: #000; font-size: 12px;"><span id="prev-date" style="font-size: 10px; background: #797979; padding: 3px; border-radius: 2px; color: #fff; font-size: 11px;"></span></a></span>

                                                    <ul class="theme-search-results-sort-list">

                                                        <li class="abdd srtarw" onclick="myfunction(this)">
                                                            <a href="#" class="" data-control-type="sortAirline" data-control-name="sortAirline" data-control-action="sort" data-path=".airlineImage" data-order="asc" data-type="text">Airline 
                                                        <span class="sort">A &rarr; Z</span>
                                                            </a>
                                                        </li>

                                                        <li class="abdd srtarw" onclick="myfunction(this)">

                                                            <a href="#" class=""
                                                                data-control-type="sortDeptime"
                                                                data-control-name="sortDeptime"
                                                                data-control-action="sort"
                                                                data-path=".deptime"
                                                                data-order="asc"
                                                                data-type="number">Departure <span class="sort">Min-Time &rarr; Max-Time</span>
                                                            </a>
                                                        </li>

                                                        <li class="abdd srtarw" onclick="myfunction(this)">

                                                            <a href="#"
                                                                class=""
                                                                data-control-type="sortArrtime"
                                                                data-control-name="sortArrtime"
                                                                data-control-action="sort"
                                                                data-path=".arrtime"
                                                                data-order="asc"
                                                                data-type="number">Arrival <span class="sort">Min-Time &rarr; Max-Time</span>
                                                            </a>

                                                        </li>



                                                        <li class="abdd srtarw" onclick="myfunction(this)">

                                                            <a href="#"
                                                                class=""
                                                                data-control-type="sortTotdur"
                                                                data-control-name="sortTotdur"
                                                                data-control-action="sort"
                                                                data-path=".totdur"
                                                                data-order="asc"
                                                                data-type="number">Duration <span class="sort">Short &rarr; Long</span>
                                                            </a>
                                                        </li>



                                                        <li class="abdd srtarw hidden-xs " onclick="myfunction(this)">
                                                            <a href="#"
                                                                class=""
                                                                data-control-type="sortCITZ"
                                                                data-control-name="sortCITZ"
                                                                data-control-action="sort"
                                                                data-path=".price"
                                                                data-order="asc"
                                                                data-type="number">Fare <span class="sort">Low &rarr; High</span>
                                                            </a>
                                                        </li>

                                                        <li class="net-fare">
                                                            <div class="auto lft" style="margin-top: 8px; float: right;">
                                                                <span onclick="ShowHideDiscount('show');" class="spnBtnShow" style="cursor: pointer;">Show Net Fare</span>
                                                                <span onclick="ShowHideDiscount('hide');" style="display: none; cursor: pointer;" class="spnBtnHide">Hide Net Fare</span>
                                                            </div>
                                                        </li>
                                                    </ul>


                                                    <span class="theme-search-results-sort-title nxt" style="float: right !important; margin-top: 8px; margin-right: -15px;"><a href="#" id="NextDay" class="pnday" style="color: #000; font-size: 12px;"><span id="next-date" style="font-size: 10px; background: #797979; padding: 3px; border-radius: 2px; color: #fff; font-size: 11px;"></span></a></span>




                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div id="mainDiv">
                                        <div id="divResult" class="list">
                                            <div id="divFrom" class="list" style="width: 100%;">
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                    </div>


                                    <%-- <div class="jplist-no-results jplist-hidden">
                                <div class='clear1'></div>
                                <div class='clear1'></div>
                                <div class='w90 mauto padding1 brdr'>
                                    <div class='clear1'></div>
                                    <div class='clear1'></div>
                                    <span class='vald f20'>Sorry, we could not find a match for your query. Please modify your search.</span> &nbsp;<span onclick='DiplayMsearch(this.id);' class='underlineitalic cursorpointer'>Modify Search</span><div class='clear'></div>
                                </div>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="render">
        </div>
        <div class="clear">
        </div>
    </div>

    <%--<div id="fltselct" class="hide">
        <div class="clear1"></div>
        <div id="totalPay" class="f16">
            Your Selection
            <div class="clear1"></div>
        </div>
        <div id="selctcntnt" class="w70 mauto">
            <div class="clear1">
            </div>
            <div id="fltgo" class="w50 lft">
            </div>
            <div id="fltbk" class="w45 rgt">
            </div>
            <div id="fltbtn">
                <input type="button" value="Book" class="button1 hide" id="FinalBook" />
                <div id="Divproc" class="hide bld">
                    <img alt="Booking In Progress" src="~/Images/loading_bar.gif" />
                </div>
            </div>
        </div>
    </div>--%>



    <div id="waitMessage" style="display: none;">
        <div style="text-align: center; z-index: 101111111111111; font-size: 12px; font-weight: bold; padding: 20px;">

            <div class="backdrop">

                <div id="searchquery" style="color: #000; font-size: 18px; text-align: center; margin-top: 140px;">
                </div>

                <div>
                    <img class="spinner" src="../Advance_CSS/Icons/morphing-animation.gif" style="width: 300px" />
                </div>
                <span id="loading-msg"></span>
            </div>





        </div>
    </div>





    <%-- <div id="divMail">
        <a href="#" class="topopup pop_button1" id="btnFullDetails">Mail All Result</a>
        <a href="#" class="topopup pop_button1" id="btnSendHtml">Mail Selected Result</a>
    </div>--%>
    <div id="backgroundPopup">
    </div>
    <div id="toPopup" class="flight_head">
        <div class="close">
        </div>
        <span class="ecs_
            ">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content">
            <!--your content start-->
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <h4 style="text-align: center; color: #FFFFFF; background-color: #20313f; font-weight: bold; padding-top: 5px; padding-bottom: 5px;">Send Mail</h4>
                    </td>
                </tr>
                <tr>
                    <td class="textsmall" style="width: 120px; padding-left: 10px;"></td>
                    <td class="textsmall">
                        <input type='radio' name='choices' checked="checked" value='A' />
                        All Result
                         <input type='radio' name='choices' value='S' />Selected Result
                    </td>
                </tr>

                <tr>
                    <td class="textsmall" style="width: 120px; padding-left: 10px;">From:
                    </td>
                    <td>
                        <input type="text" class="headmail" id="txtFromMail" name="txtFromMail" />
                    </td>
                </tr>
                <tr>
                    <td class="textsmall" style="width: 120px; padding-left: 10px;">To:
                    </td>
                    <td>
                        <input type="text" class="headmail" id="txtToMail" name="txtToMail" />
                    </td>
                </tr>
                <tr>
                    <td class="textsmall" style="width: 120px; padding-left: 10px;">Subject:
                    </td>
                    <td>
                        <input type="text" class="headmail" id="txtSubj" name="txtSubj" />
                    </td>
                </tr>
                <tr>
                    <td class="textsmall" style="width: 120px; padding-left: 10px;">Message:
                    </td>
                    <td>
                        <textarea id="txtMsg" class="headmail" name="txtMsg" rows="4" cols="20"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="margin-left: 20px;"></td>
                    <td align="right">
                        <%--<input type="button" class="pop_button" id="btnCancel" name="btnCancel" value="Cancel" />--%>
                        <input type="button" class="buttonfltbk" id="btnSendMail" name="btnSendMail" value="Send Details" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%--<div id="divabc">&nbsp;</div>--%>
                        <label id="lblMailStatus" style="display: none; color: Red;">
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <!--your content end-->
    </div>
    <div id="FareBreakupHeder" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="FareBreakupHederLabel" aria-hidden="true" style="display: none;">



        <div class="modal-dialog" role="document" style="height: 400px; overflow-x: scroll; top: 2px; background: #fff; border-radius: 4px; -webkit-box-shadow: 0 2px 0 0px rgba(0,0,0,.08); -moz-box-shadow: 0 2px 0 0px rgba(0,0,0,.08); border: 1px solid #dad8d8;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="FareBreakupHederLabel">Fare Breakup</h5>
                    <button type="button" class="close FareBreakupHederClose" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>

                </div>
                <div class="modal-body" id="FareBreakupHederId" style="padding: 2px 10px">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary FareBreakupHederClose">Close</button>
                </div>
            </div>
        </div>

    </div>

    <div id="ConfmingFlight" class="CfltFare" style="display: none;">
        <div id="divLoadcf" class="">
        </div>
    </div>

    <div class="one-way-select hide">

        <div class="col-md-12" id="hdvhgvhgvfh">
        </div>

    </div>

    <div class="clear"></div>
    <%--    <a href="#toptop"><span class="toptop" style="position: fixed; bottom: 4px; right: 20px; height: 50px; font-size: 20px; width: 50px; border-radius: 50%; cursor: pointer; padding: 13px 15px; background: rgb(0, 75, 145); color: rgb(255, 255, 255); display: block;"><i class="fa fa-chevron-up" aria-hidden="true"></i></span></a>--%>
    <div class="clear1"></div>
    <input type="hidden" id="hdnMailString" name="hdnMailString" />
    <input type="hidden" id="hdnAllOrSelecte" name="hdnAllOrSelecte" />
    <input type="hidden" id="hdnOnewayOrRound" name="hdnOnewayOrRound" />
    <asp:Literal ID="henAgcDetails" runat="server" Visible="false"></asp:Literal>
    <input type="hidden" id="hdnSRFPriceLineNoO" name="hdnSRFPriceLineNoO" />
    <input type="hidden" id="hdnSRFAircodeO" name="hdnSRFAircodeO" />
    <input type="hidden" id="hdnSRFPriceLineNoR" name="hdnSRFPriceLineNoR" />
    <input type="hidden" id="hdnSRFAircodeR" name="hdnSRFAircodeR" />

    <input type="hidden" id="hdnSRFPriceLineNoO" name="hdnSRFPriceLineNoO" />
    <input type="hidden" id="hdnSRFAircodeO" name="hdnSRFAircodeO" />
    <input type="hidden" id="hdnSRFPriceLineNoR" name="hdnSRFPriceLineNoR" />
    <input type="hidden" id="hdnSRFAircodeR" name="hdnSRFAircodeR" />


    <script type="text/javascript">
        $(document).ready(function () {

            function high() {
                //$(".theme-hero-area-body").css("height", "80px");
            }
            window.onload = high;
            $("#show-hide").click(function () {

                var k = "";
                if (k == "") {
                    $(".re-search").slideToggle();
                    $(".theme-hero-area-body").css("height", "100%");
                    var k = 1;
                }
            });
            $("#show-hide-mob").click(function () {

                var k = "";
                if (k == "") {
                    $(".re-search").slideToggle();
                    $(".theme-hero-area-body").css("height", "100%");
                    var k = 1;
                }
            });

            $("#hide").click(function () {

                var k = "";
                if (k == "") {
                    $(".re-search").hide();
                    //$(".theme-hero-area-body").css("height", "100%");
                    var k = 1;
                }
            });


            $("#show-filter").click(function () {

                var c = "";
                if (c == "") {
                    $("._mob-h1").slideToggle();
                    //$(".theme-hero-area-body").css("height", "100%");
                    var c = 1;
                }
            });
            $("#hide-filter").click(function () {

                var d = "";
                if (d == "") {
                    $("._mob-h1").hide();
                    //$(".theme-hero-area-body").css("height", "100%");
                    var d = 1;
                }
            });

        });
    </script>


    <script type="text/javascript">
        <%--var UrlBase = '<%=ResolveUrl("~/")%>';--%>
        //var UrlBase = window.location.protocol + "//" + location.host + "/";
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/json2.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/JSLINQ.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jplist.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/SortAD.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/TextFilterGroup.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/handleQueryString.js?v=3.9")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/async.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/LZCompression.js?y=9.8")%>"></script>
    <%--<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/FlightResultsNew2.1.js?v=1.7")%>"></script>--%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/FlightResultsNew.js?v=2")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.tooltip.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/gridview-readonly-script.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/FlightMailing.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/pako.min.js") %>"></script>

    <%--   <script src="../Scripts/script.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        (function () {
            var loaderText = document.getElementById("loading-msg");
            var refreshIntervalId = setInterval(function () {
                loaderText.innerHTML = getLoadingText();
            }, 2500);

            function getLoadingText() {
                var strLoadingText;
                var arrLoadingText = ["Please Wait", "While we are fetching best fare for you"
                ];
                var rand = Math.floor(Math.random() * arrLoadingText.length);
                return arrLoadingText[rand];
            }
        })();
    </script>


    <script type="text/javascript">
        function openNav() {

            document.getElementById("mysidenavssss").style.width = "375px";
            document.getElementById("passengersss").style.display = "block";

        }

        function closeNav() {
            document.getElementById("mysidenavssss").style.width = "0";
            document.getElementById("passengersss").style.display = "none";
        }
    </script>
    <%--   <script type="text/javascript">
        $(document).ready(function () {
            $(".detls1").click(function () {
                $(".detls").toggle();
            });
        });
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".abc1").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT1") {
                    $(this).toggleClass("bdrss");
                }
            });
            $(".abc2t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT2") {
                    $(this).toggleClass("bdrss");
                }
            });
            $(".abc3t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT3") {
                    $(this).toggleClass("bdrss");
                }

            });
            $(".abc4t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT4") {
                    $(this).toggleClass("bdrss");
                }

            });

        });

        $(document).ready(function () {
            $(".abc1").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT1R") {
                    $(this).toggleClass("bdrss");
                }
            });
            $(".abc2t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT2R") {
                    $(this).toggleClass("bdrss");
                }
            });
            $(".abc3t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT3R") {
                    $(this).toggleClass("bdrss");
                }

            });
            $(".abc4t").unbind('click').bind('click', function (e) {

                if (e.target.id != "CheckboxT4R") {
                    $(this).toggleClass("bdrss");
                }

            });

        });
    </script>


    <script type="text/javascript">

        debugger;
        var urlParams = new URLSearchParams(location.search);

        //  urlParams.has('type');  // true
        // urlParams.get('NStop');    // 1234
        var NStop = urlParams.get('NStop'); //GetQueryStringParams('NStop')
        //function GetQueryStringParams(sParam)
        //{
        //    debugger;
        //        var sPageURL = window.location.search.substring(1);
        //        var sURLVariables = sPageURL.split('&');
        //        for (var i = 0; i < sURLVariables.length; i++)
        //        {
        //            var sParameterName = sURLVariables[i].split('=');
        //            if (sParameterName[0] == sParam)
        //            {
        //                return sParameterName[1];
        //            }
        //        }
        //    }​




        if (NStop == "TRUE") {
            $('#dsplm').hide();
        }


        $("p:odd").removeClass("blue under");
    </script>

    <script type="text/javascript">

        jQuery("document").ready(function ($) {
            debugger;

            //$(".onewayss").removeClass("col-md-4 nopad text-search mltcs").addClass("col-md-5 nopad text-search mltcs");

            //$("#rdbOneWay").click(function () {

            //    $(".onewayss").removeClass("col-md-4 nopad text-search mltcs").addClass("col-md-5 nopad text-search mltcs");
            //    $(".sk").removeAttr(disabled = "disabled");
            //});

            //$("#rdbRoundTrip").click(function () {

            //    $(".onewayss").removeClass("col-md-5 nopad text-search mltcs").addClass("col-md-4 nopad text-search mltcs");

            //});
            //$("#rdbMultiCity").click(function () {
            //    $(".onewayss").removeClass("col-md-4 nopad text-search mltcs").addClass("col-md-5 nopad text-search mltcs");
            //});

            var nav = $('.nav-container');
            $(window).scroll(function () {
                if ($(this).scrollTop() > 175) {
                    $(".toptop").fadeIn();
                    if ($("#lftdv1").is(":visible") == false) {
                        nav.addClass("f-nav1");
                    }
                    else {
                        nav.addClass("f-nav");
                    }
                } else {
                    $(".toptop").fadeOut();
                    nav.removeClass("f-nav");
                    nav.removeClass("f-nav1");
                }
            });

            //var Triptype = getUrlVars()["TripType"];
            //if (Triptype == "rdbOneWay") {
            //    $(".onewayss").removeClass("col-md-4 nopad text-search mltcs").addClass("col-md-5 nopad text-search mltcs");
            //}



            //if (Triptype == "rdbOneWay") {
            //    $(".onewayss").removeAttr(disable = "disable");
            //}

            //if (Triptype == "rdbRoundTrip") {
            //    $(".onewayss").removeAttr(disable = "disable");
            //}


            //    if (Triptype == "rdbRoundTrip") {
            //        $(".onewayss").removeClass("col-md-5 nopad text-search mltcs").addClass("col-md-4 nopad text-search mltcs");
            //    }
            //});
            function getUrlVars() {
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
                return vars;
            }
    </script >
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>--%>
</asp:Content>
