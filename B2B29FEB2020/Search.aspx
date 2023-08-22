<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Search" MasterPageFile="~/MasterForHome.master" %>

<%@ Register Src="~/UserControl/FltSearch.ascx" TagName="IBESearch" TagPrefix="Search" %>
<%@ Register Src="~/UserControl/HotelSearch.ascx" TagPrefix="Search" TagName="HotelSearch" %>
<%@ Register Src="~/BS/UserControl/BusSearch.ascx" TagName="BusSearch" TagPrefix="Searchsss" %>
<%@ Register Src="~/UserControl/FltSearchFixDep.ascx" TagName="IBESearchDep" TagPrefix="SearchDep" %>
<%@ Register Src="~/UserControl/Fare_Cal.ascx" TagName="FareCal" TagPrefix="FDCAL" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Cont1" runat="server">
    <link type="text/css" href="Advance_CSS/dropdown_search_box/css/select2.min.css" rel="stylesheet" />
    <link href="Advance_CSS/css/Search.css" rel="stylesheet" />
    <link type="text/css" href="Custom_Design/css/search.css" rel="stylesheet" />
    <link href="Advance_CSS/css/Fair-Calander.css" rel="stylesheet" />
    <link href="icofont/icofont.css" rel="stylesheet" />
    <link href="icofont/icofont.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>



    <style type="text/css">
        #container {
            overflow-x: auto;
            width: 100%;
            overflow: hidden;
        }

        .T-fare, .tot-fair, .price-list {
            text-transform: uppercase;
        }

        body, html {
            font-family: 'Quicksand', sans-serif !important;
        }
    </style>

    <%--<script type="text/javascript">

        $(document).ready(function () {
            $('.minus').click(function () {
                var $input = $(this).parent().find('input');
                var $inputid = $input.attr('id');
                var count = parseInt($input.val()) - 1;
                if ($inputid != "Adult") {
                    count = count <= 0 ? 0 : count;
                }
                else {
                    count = count < 1 ? 1 : count;
                }
                $input.val(count);
                $input.change();
                AddAllPax();
                return false;
            });
            $('.plus').click(function () {
                var $input = $(this).parent().find('input');
                let inpcount = parseInt($input.val()) + 1;
                $input.val(inpcount);
                $input.change();
                AddAllPax();
                return false;
            });

            AddAllPax();

            function AddAllPax() {
                let adultinp = $("#Adult").val();
                let childinp = $("#Child").val();
                let infantinp = $("#Infant").val();

                $("#sapnTotPax").val(parseInt(adultinp) + parseInt(childinp) + parseInt(infantinp) + " Traveler(s)");


            }
        });
    </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.minus1').click(function () {
                var $input = $(this).parent().find('input');
                var $inputid = $input.attr('id');
                var count = parseInt($input.val()) - 1;
                if ($inputid != "Adult") {
                    count = count <= 0 ? 0 : count;
                }
                else {
                    count = count < 1 ? 1 : count;
                }
                $input.val(count);
                $input.change();
                AddAllPax1();
                return false;
            });
            $('.plus1').click(function () {
                var $input = $(this).parent().find('input');
                let inpcount = parseInt($input.val()) + 1;
                $input.val(inpcount);
                $input.change();
                AddAllPax1();
                return false;
            });

            AddAllPax1();

            function AddAllPax1() {
                let adultinp = $("#AdultF1").val();
                let childinp = $("#ChildF1").val();
                let infantinp = $("#InfantF1").val();

                $("#sapnTotPaxF1").val(parseInt(adultinp) + parseInt(childinp) + parseInt(infantinp) + " Traveler(s)");


            }
        });
    </script>

    <style type="text/css">
        .mySlides img {
            vertical-align: middle;
            width: 100% !important;
        }
        /* Slideshow container */
        .slideshow-container {
            max-width: 1000px;
            position: relative;
            margin: auto;
        }

        /* Next & previous buttons */
        .prev, .next {
            cursor: pointer;
            position: absolute;
            top: 50%;
            width: auto;
            padding: 5px;
            margin-top: -22px;
            color: white;
            font-weight: bold;
            font-size: 18px;
            transition: 0.6s ease;
            border-radius: 0 3px 3px 0;
            user-select: none;
        }

        /* Position the "next button" to the right */
        .next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }

            /* On hover, add a black background color with a little bit see-through */
            .prev:hover, .next:hover {
                background-color: rgba(0,0,0,0.8);
            }

        /* Caption text */
        /*  .text {
            color: #f2f2f2;
            font-size: 15px;
            padding: 8px 12px;
            position: absolute;
            bottom: 8px;
            width: 100%;
            text-align: center;
        }*/

        /* Number text (1/3 etc) */
        .numbertext {
            color: #f2f2f2;
            font-size: 12px;
            padding: 8px 12px;
            position: absolute;
            top: 0;
        }

        /*.fade {
            -webkit-animation-name: fade;
            -webkit-animation-duration: 1.5s;
            animation-name: fade;
            animation-duration: 1.5s;
        }*/

        @-webkit-keyframes fade {
            from {
                opacity: .4;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes fade {
            from {
                opacity: .4;
            }

            to {
                opacity: 1;
            }
        }

        /* On smaller screens, decrease text size */
        @media only screen and (max-width: 300px) {
            .prev, .next, .text {
                font-size: 11px;
            }
        }
    </style>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background: #fff!important">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="slideshow-container">
                        <%=NotificationContent %>
                        <%--<a class="prev" onclick="plusSlides(-1)" style="background: rgb(238 49 87);">&#10094;</a>
                        <a class="next" onclick="plusSlides(1)" style="background: rgb(238 49 87);">&#10095;</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script type="text/javascript">	
			function isEmpty(el) { return !$.trim(el.html()) }
			if (isEmpty($('.mySlides'))) {
				
			}
			else {
				 $(window).load(function () {$('#exampleModal').modal('show'); });
			}
		
			//$(window).load(function () {
				//$('#exampleModal').modal('show'); 			
        //});
			$('.carousel').carousel();
	
            var slideIndex = 1;
            showSlides(slideIndex);

            function plusSlides(n) {
                showSlides(slideIndex += n);
            }
            function showSlides(n) {
                var i;
                var slides = document.getElementsByClassName("mySlides");
                var dots = document.getElementsByClassName("dot");
                if (n > slides.length) { slideIndex = 1 }
                if (n < 1) { slideIndex = slides.length }
                for (i = 0; i < slides.length; i++) {
                    slides[i].style.display = "none";
                }
                for (i = 0; i < dots.length; i++) {
                    dots[i].className = dots[i].className.replace(" active", "");
                }
                slides[slideIndex - 1].style.display = "block";
                dots[slideIndex - 1].className += " active";
            }
        </script>--%>

    <%--<a class="btn btn-default" id="btn-confirm" style="position: absolute; top: 73px; z-index: 999;">Confirm</a>--%>
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel1" aria-hidden="true" id="mi-modal" style="z-index: 99990 !important;">
        <div class="modal-dialog modal-sm" style="margin-top: 10%; width: 30%;">
            <div class="modal-content">
                <%--  <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Confirmar</h4>
      </div>--%>
                <div class="modal-body">
                    <h5>Are you sure you want to continue with this flight?</h5>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="modal-btn-cancel" style="float: left; background: #ff0000">Cancel</button>
                    <button type="button" class="btn btn-success" id="modal-btn-confirm">Confirm</button>

                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel2" aria-hidden="true" id="flt-details" style="z-index: 9999 !important;">
        <div class="modal-dialog modal-sm" style="margin-top: 10%; width: 70%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <%-- <h4 class="modal-title" id="myModalLabel">Confirmar</h4>--%>
                </div>
                <div class="modal-body">
                    <div class="tabs">
                        <div value="1api6ENRMLITZNRML" class="fare-groups-wrap 1api6ENRMLITZNRML_faredetailmasterall " id="1api6ENRMLITZNRML_faredetailmaster" style="margin-bottom: 40px;">
                            <div class="gridViewToolTip1 lft" title="1api6ENRMLITZNRML_O"></div>
                            <ul class="fare-groups nav navbar-nav" role="tablist" style="border-bottom: 1px solid #CCC;">
                                <li class="fgf sel nav-item active" id="1api6ENRMLITZNRML_Allll"><a href="#1api6ENRMLITZNRML_Fare" data-toggle="tab" role="tab" class="div_cls d collapsible gridViewToolTip nav-link active" style="padding: 0px;" id="fare-summ">Fare Details</a>
                                </li>
                                <li class="fgf nav-item"><a href="#1api6ENRMLITZNRML_fltdt" data-toggle="tab" role="tab" class="div_cls d collapsible fltDetailslink nav-link" id="fare_Det" style="padding: 0px;">Flight Details</a>
                                </li>
                                <li class="fgf nav-item"><a href="#1api6ENRMLITZNRML_bag" data-toggle="tab" role="tab" class="div_cls d collapsible fltBagDetails nav-link" id="bag_det" style="padding: 0px;">Baggage</a>
                                </li>
                                <li class="fgf nav-item"><a href="#1api6ENRMLITZNRML_canc" data-toggle="tab" role="tab" class="div_cls d collapsible fareRuleToolTip cursorpointer nav-link" style="padding: 0px;" id="can_flt">Cancellation</a><div class="fade" title="1api6ENRMLITZNRML_O"></div>

                                </li>
                                <div class="ui_block clearfix"></div>
                            </ul>
                        </div>
                        <div class="tabs-stage tab-content">
                            <div class="depcity tab-pane active" role="tabpanel" id="1api6ENRMLITZNRML_Fare" style="margin-top: -11px;">
                                <div><span onclick="Close('1api6ENRMLITZNRML_');" title="Click to close Details"></span></div>
                                <div class="new-fare-details " id="FareBreak">
                                    <img src="Images/loading_bar.gif" />
                                </div>
                                <div class="clear"></div>
                                <div class="clear"></div>
                            </div>
                            <div class="depcity tab-pane" role="tabpanel" id="1api6ENRMLITZNRML_fltdt" style="margin-top: -11px;"></div>
                            <div class="depcity tab-pane" role="tabpanel" id="1api6ENRMLITZNRML_bag" style="margin-top: -11px;"></div>
                            <div class="depcity tab-pane" role="tabpanel" id="1api6ENRMLITZNRML_canc" style="margin-top: -11px;"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel3" aria-hidden="true" id="pax-mod">
        <div class="modal-dialog modal-sm" style="margin-top: 10%; width: 50%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">



                    <%--<h5>Please select travelers first to make booking.</h5>--%>

                    <div class="row" data-gutter="none">
                        <div id="fltpx">
                        </div>
                        <div class="col-md-12" id="collapseExample4">


                            <input class="aumd-tb div1" id="sapnTotPaxF1" placeholder=" Traveller" type="text" disabled="disabled" />

                            <div id="div_Adult_Child_Infant1hj" class="myText">
                                <div class="innr_pnl dflex">
                                    <div class="main_dv pax-limit" style="margin-left: 70px;">
                                        <label>
                                            <span>Adult</span>

                                        </label>
                                        <div class="number">
                                            <span class="minus1">-</span>
                                            <input type="text" class="inp" value="1" min="1" name="Adult" id="AdultF1">
                                            <span class="plus1">+</span>
                                        </div>

                                    </div>
                                    <div class="main_dv pax-limit" style="margin-left: 70px;">
                                        <label>
                                            <span>Child<span class="light-grey">(2+ 12 yrs)</span></span>

                                        </label>

                                        <div class="number">
                                            <span class="minus1">-</span>
                                            <input type="text" class="inp" value="0" min="0" name="Child" id="ChildF1">
                                            <span class="plus1">+</span>
                                        </div>

                                    </div>
                                    <div class="main_dv pax-limit" style="margin-left: 70px;">

                                        <label>
                                            <span>Infant <span class="light-grey">(below 2 yrs)</span></span>

                                        </label>

                                        <div class="number">
                                            <span class="minus1">-</span>
                                            <input type="text" class="inp" value="0" min="0" name="Infant" id="InfantF1">
                                            <span class="plus1 Infant1">+</span>
                                        </div>

                                    </div>

                                </div>
                            </div>


                        </div>



                    </div>

                </div>
                <div class="modal-footer hidden">
                    <%--<button type="button" class="btn btn-danger" id="pax-confirm" style="float: left; background: #ff0000">Cancel</button>--%>
                    <button type="button" class="btn btn-success" id="pax-confirm">Confirm</button>

                </div>
            </div>
        </div>
    </div>

    <input type="hidden" value="" id="pass-pax" />

    <div class="theme-hero-area theme-hero-area-primary">

        <div class="theme-hero-area-body">
            <div class="_pt-250 _pb-200 _pv-mob-50" style="background-image: url(../Advance_CSS/pattern.jpg); background-repeat: no-repeat; background-attachment: fixed; background-position: center;">
                <div class="container flight-wrapper">
                    <div class="theme-search-area-tabs vertical_search_engine">


                        <div class="tab-content flight-box">

                            <div>
                                <h4 style="padding: 15px;">Book Your Flight</h4>
                            </div>

                            <div class="tab-pane fade in active" id="tab1default1">

                                <Search:IBESearch ID="IBESearch2" runat="server" />
                                <SearchDep:IBESearchDep runat="server" ID="FixDep" />




                            </div>
                            <%--    <div class="tab-pane fade in active" id="tab1default">
                               
                            </div>--%>


                            <div class="tab-pane fade" id="tab2default">
                                <Searchsss:BusSearch ID="Bus2" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="right_data">
                        <div class="offer-banner trip-section fluid-full-width moduleArea hidden">
                            <div class="Trip-book why_tripforo moduleArea">
                                <ul class="nav navbar-nav">
                                    <li>
                                        <a href="/design/Home.aspx" role="button">Home</a>
                                    </li>
                                    <li>
                                        <a href="/design/Home.aspx" role="button">Flight</a>

                                    </li>
                                    <li>
                                        <a href="About-Us.aspx" role="button">About</a>

                                    </li>
                                    <li>
                                        <a href="Contact.aspx" role="button">Contact</a>

                                    </li>

                                    <li class="navbar-nav-item-user dropdown hidden">
                                        <a class="dropdown-toggle" href="account.html" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                            <i class="fa fa-user-circle-o navbar-nav-item-user-icon"></i>My Account
                                        </a>
                                        <ul class="dropdown-menu">
                                            <li>
                                                <a href="Account.aspx">Preferences</a>
                                            </li>
                                            <li>
                                                <a href="#">Notifications</a>
                                            </li>
                                            <li>
                                                <a href="#">Payment Methods</a>
                                            </li>
                                            <li>
                                                <a href="#">Travelers</a>
                                            </li>
                                            <li>
                                                <a href="#">History</a>
                                            </li>

                                        </ul>
                                    </li>

                                </ul>
                            </div>
                        </div>

                        <FDCAL:FareCal ID="FDCAL1" runat="server" />

                        <div class="offer-banner trip-section fluid-full-width moduleArea" id="div_LatestOffers">

                            <div class="Trip-book why_tripforo moduleArea">
                                <span class="module_heading" style="font-size: 15px; color: #000; float: left; margin-top: -15px; margin-bottom: 5px;">Latest Offer!</span>

                                <div class='alert_msg info_msg fl' id="avlsector_id" style="display: none;">
                                    <b class='status_info fl'>
                                        <i class='icofont-location-pin icofont-2x'></i></b>
                                    <span class='status_cont'>Avilable Sector</span>
                                     <button id="slideFront" type="button" style="float:right;border:none;background:none;"><i class="icofont-dotted-right icofont-2x"></i></button>
                                    <button id="slideBack" type="button" style="float:right;border:none;background:none;"><i class="icofont-dotted-left icofont-2x"></i></button>
                                   
                                </div>

                                <div class="tab">
                                    <div id="container">
                                        <style>
                                            div#field-dropdown-autocomplete {
                                                width: 30%;
                                                display: none;
                                            }
                                        </style>
                                        <select name="field-dropdown" style="display: none;" id="field-dropdown">
                                            <option value="">Select</option>
                                        </select>

                                    </div>
                                    <div class="tab__content" id="BindDepArrDetails">
                                        <p class="text-center" style="font-size: 20px;">Please wait... <i class="fa fa-spinner fa-pulse"></i></p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <script type="text/javascript">
                            var button = document.getElementById('slideFront');
                            button.onclick = function () {
                                var container = document.getElementById('container');
                                sideScroll(container, 'right', 15, 600, 30);
                            };

                            var back = document.getElementById('slideBack');
                            back.onclick = function () {
                                var container = document.getElementById('container');
                                sideScroll(container, 'left', 15, 600, 30);
                            };

                            function sideScroll(element, direction, speed, distance, step) {
                                scrollAmount = 0;
                                var slideTimer = setInterval(function () {
                                    if (direction == 'left') {
                                        element.scrollLeft -= step;
                                    } else {
                                        element.scrollLeft += step;
                                    }
                                    scrollAmount += step;
                                    if (scrollAmount >= distance) {
                                        window.clearInterval(slideTimer);
                                    }
                                }, speed);
                            }

                        </script>


                        <section class="offer-banner trip-section fluid-full-width moduleArea hidden">
                         <div class="anniversaryBannerText">
                         <a target="_blank" rel="noopener" title="Paisa Bazaar" href="#">
                             <img src="Advance_CSS/Cover.jpg" class="conta iner"></a>

                         </div></section>

                        <%--<div class="trending-trip trending-trip-box carousalModuleArea ">
                            <span class="module_heading">Popular Fixed Departure Flight</span>

                            <div class="popular_cities">
                                <div class="ytCarousel">
                                    <div class="VueCarousel" slidecount="4">
                                        <div class="VueCarousel-wrapper">
                                            <div class="VueCarousel-inner" style="transform: translateX(0px); transition: transform 0.5s ease 0s; flex-basis: 188px; visibility: visible;">
                                                <a class="VueCarousel-slide items" title="Delhi to Goa" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Mumbai</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 3,622</span>
                                                        </p>
                                                    </div>
                                                </a>
                                                <a class="VueCarousel-slide items" title="Delhi to Bagdogra" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Bagdogra</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,453</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Ranchi" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Patna</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,390</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Chennai" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Ahmedabad</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,502</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Lucknow" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Lucknow</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">Rs. 1,827</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Pune" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Pune</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,474</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Raipur" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Raipur</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,794</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Mumbai" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Mumbai</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">₹ 2,729</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Vishakhapatnam" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Vishakhapatnam</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">Rs. 3,319</span>
                                                        </p>
                                                    </div>
                                                </a><a class="VueCarousel-slide items" title="Delhi to Indore" href="#">
                                                    <div class="origin_destination">
                                                        <div class="elip">Delhi</div>
                                                        <div class="time"><i class="icofont-airplane icofont-rotate-180"></i></div>
                                                        <div class="elip">Indore</div>
                                                    </div>
                                                    <div class="price_go dest">
                                                        <p class="low_price">
                                                            <span class="rs">Rs. 1,250</span>
                                                        </p>
                                                    </div>
                                                </a>
                                            </div>
                                        </div>
                                        <!---->
                                        <div class="VueCarousel-navigation"><a href="javascript:void(0);" class="VueCarousel-navigation-button VueCarousel-navigation-prev VueCarousel-navigation--disabled"></a><a href="javascript:void(0);" class="VueCarousel-navigation-button VueCarousel-navigation-next"></a></div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                        <div class="trending-trip trending-trip-box carousalModuleArea">
                            <span class="module_heading">Limited Offer Expire Soon!</span>
                            <div class="slider" id="slider">
                                <div class="slide divLimitedOfferExpireSoon" id="slide">
                                    <p class="text-center" style="font-size: 15px; margin: 75px 10px 10px 300px;">Please wait... <i class="fa fa-spinner fa-pulse"></i></p>
                                </div>
                            </div>
                            <span class="ctrl-btn pro-prev"><i class="icofont-rounded-left" style="font-size: 24px;"></i></span>
                            <span class="ctrl-btn pro-next"><i class="icofont-rounded-right" style="font-size: 24px;"></i></span>
                        </div>

                        <div class="theme-page-section theme-page-section-xxl theme-page-section-gray" style="display: none;">
                            <div class="container">
                                <div class="theme-page-section-header">
                                    <h5 class="theme-page-section-title">Travel Inspirations</h5>
                                    <p class="theme-page-section-subtitle">Our latest travel tips, hacks and insights</p>
                                </div>
                                <div class="row row-col-gap" data-gutter="10">
                                    <div class="col-md-3 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/city-sun-hot-child_350x260.jpg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">day ago</p>
                                                    <h5 class="theme-blog-item-title">Booking hotel in India</h5>
                                                    <p class="theme-blog-item-desc">Massa auctor vehicula sodales orci volutpat feugiat litora suspendisse ultrices</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/woman-hiker-outside-forest_730x435.jpg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">3 days ago</p>
                                                    <h5 class="theme-blog-item-title">Canada: forest trip</h5>
                                                    <p class="theme-blog-item-desc">Curabitur dictumst ridiculus convallis ullamcorper purus velit lectus vulputate lacus</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/man-wearing-black-and-red-checkered_350x435.jpeg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">week ago</p>
                                                    <h5 class="theme-blog-item-title">Total Solar Eclipse</h5>
                                                    <p class="theme-blog-item-desc">Adipiscing sociosqu quam rutrum aenean commodo neque lacinia taciti praesent</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/plate-flight-sky-sunset_350x435.jpeg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">mounth ago</p>
                                                    <h5 class="theme-blog-item-title">Mix up your cabin classes</h5>
                                                    <p class="theme-blog-item-desc">Nisi iaculis sagittis morbi duis lacus mollis morbi torquent inceptos</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/man_back_350x260.jpg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">2 weeks ago</p>
                                                    <h5 class="theme-blog-item-title">Alaska days</h5>
                                                    <p class="theme-blog-item-desc">Lorem ante neque elit dictumst magnis volutpat cubilia varius volutpat</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 ">
                                        <div class="theme-blog-item _br-3 theme-blog-item-full">
                                            <a class="theme-blog-item-link" href="blog-post.html"></a>
                                            <div class="banner _h-45vh  banner-">
                                                <div class="banner-bg" style="background-image: url(img/the_best_mode_of_transport_here_in_maldives_350x260.jpg);"></div>
                                                <div class="banner-caption banner-caption-bottom banner-caption-grad">
                                                    <p class="theme-blog-item-time">mounth ago</p>
                                                    <h5 class="theme-blog-item-title">Hawaii lifestyle</h5>
                                                    <p class="theme-blog-item-desc">Nec vestibulum sit feugiat quam suscipit lacus fringilla etiam facilisis</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="offer-banner trip-section fluid-full-width moduleArea hidden">
                            <div class="Trip-book why_tripforo moduleArea">
                                <span class="whyBook_title">Why book with Tripforo</span>
                                <div class="row row-col-mob-gap" data-gutter="60">
                                    <div class="col-md-4 ">
                                        <div class="feature feature-white feature-center">
                                            <i class="feature-icon feature-icon-line feature-icon-box feature-icon-round lin lin-plane"></i>
                                            <div class="feature-caption">
                                                <h5 class="feature-title">Cheap Flight Ticket</h5>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 ">
                                        <div class="feature feature-white feature-center">
                                            <i class="feature-icon feature-icon-line feature-icon-box feature-icon-round lin lin-badge"></i>
                                            <div class="feature-caption">
                                                <h5 class="feature-title">We Have Experience</h5>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 ">
                                        <div class="feature feature-white feature-center">
                                            <i class="feature-icon feature-icon-line feature-icon-box feature-icon-round lin lin-wallet"></i>
                                            <div class="feature-caption">
                                                <h5 class="feature-title">Online Payment Safety</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Advance_CSS/dropdown_search_box/js/select2.min.js"></script>

    <script type="text/javascript">
        "use strict";

        productScroll();

        function productScroll() {
            let slider = document.getElementById("slider");
            let next = document.getElementsByClassName("pro-next");
            let prev = document.getElementsByClassName("pro-prev");
            let slide = document.getElementById("slide");
            let item = document.getElementById("slide");

            for (let i = 0; i < next.length; i++) {
                //refer elements by class name

                let position = 0; //slider postion

                prev[i].addEventListener("click", function () {
                    //click previos button
                    if (position > 0) {
                        //avoid slide left beyond the first item
                        position -= 1;
                        translateX(position); //translate items
                    }
                });

                next[i].addEventListener("click", function () {
                    if (position >= 0 && position < hiddenItems()) {
                        //avoid slide right beyond the last item
                        position += 1;
                        translateX(position); //translate items
                    }
                });
            }

            function hiddenItems() {
                //get hidden items
                let items = getCount(item, false);
                let visibleItems = slider.offsetWidth / 210;
                return items - Math.ceil(visibleItems);
            }
        }

        function translateX(position) {
            //translate items
            slide.style.left = position * -210 + "px";
        }

        function getCount(parent, getChildrensChildren) {
            //count no of items
            let relevantChildren = 0;
            let children = parent.childNodes.length;
            for (let i = 0; i < children; i++) {
                if (parent.childNodes[i].nodeType != 3) {
                    if (getChildrensChildren)
                        relevantChildren += getCount(parent.childNodes[i], true);
                    relevantChildren++;
                }
            }
            return relevantChildren;
        }

    </script>

    <script type="text/javascript">
        $(window).load(function () {
            if (localStorage.getItem('isnotification') != 'notyshown') {
                $('#exampleModal').modal('show');
                localStorage.setItem('isnotification', 'notyshown');
            }
        });
    </script>
</asp:Content>
