$(document).ready(function() {
    HideHotels();
    var chkin = "", chkout = "", rooms = "", totalAdult = "", totalchild = "", NoOfRooms = "", htlname = "", htlstar = "", AdultPerRoom = "", ChildPerRoom = "";
    var ChildAge = new Array();
    var chdcnt = 0; var Allhtllist; var alldeallist;
    var roomloop = decodeURI(window.location.search).substring(1).split('&');
    var CityName = roomloop[0].split('=')[1].replace("%20", " ").replace("%20", " ").replace("%20", " ").replace("%20", " ");
    var citylist = CityName.split(',');
    $("#contrycode").val(citylist[3]);
    $("#CountryName").val(citylist[3]);
    $(".Hotel_MapView").hide();
    chkin = roomloop[1].split('=')[1];
    chkout = roomloop[2].split('=')[1];
    rooms = roomloop[3].split('=')[1];
    var chdagess = "", rmno = ""; var chdagetf1 = true, chdagetf2 = true, chdagetf3 = true; var chdagetf0 = true;
    var room = new Array(); var childa = new Array();
    for (var r = 0; r < roomloop.length; r++) {
        if (roomloop[r].indexOf("adult") > 0) {
            AdultPerRoom += $.trim(roomloop[r].split('=')[1]) + "_";
        }
        if (roomloop[r].indexOf("children") > 0) {
            ChildPerRoom += $.trim(roomloop[r].split('=')[1]) + "_";
        }
        if (roomloop[r].indexOf("TotAdt") >= 0) {
            totalAdult = roomloop[r].split('=')[1];
        }
        if (roomloop[r].indexOf("TotChd") >= 0) {
            totalchild = roomloop[r].split('=')[1];
        }
        if (roomloop[r].indexOf("htlname") >= 0) {
            htlname = roomloop[r].split('=')[1];
        }
        if (roomloop[r].indexOf("htlstar") >= 0) {
            htlstar = roomloop[r].split('=')[1];
        }

        if (roomloop[r].indexOf(".age=") > 0) {
            rmno = roomloop[r].substring(6).split(']')[0];
            if (rmno == "0" && chdagetf0 == true) {
                chdagess += "#";
                chdagetf0 = false;
            }
            if (rmno == "1" && chdagetf1 == true) {
                chdagess += "#";
                chdagetf1 = false;
            }
            if (rmno == "2" && chdagetf2 == true) {
                chdagess += "#";
                chdagetf2 = false;
            }
            if (rmno == "3" && chdagetf3 == true) {
                chdagess += "#";
                chdagetf3 = false;
            }
            chdagess += rmno + $.trim(roomloop[r].split('=')[1]) + "_";
        }
    }
    //    var chdagess = "";
    //    for (var rr = 0; rr < parseInt(rooms); rr++) {
    //        var Stotchd = "rooms[" + rr + "].children=";
    //        var stotchd = window.location.search.substring(1).split(Stotchd)[1].split('&');
    //        for (var ag = 0; ag < parseInt(stotchd[0]); ag++) {
    //            var StotchdAge = "rooms[" + rr + "].child[" + ag + "].age=";
    //            var ss1 = window.location.search.substring(1).split(StotchdAge)
    //            var SchdAge = ss1[1].split('&');
    //            chdagess += SchdAge[0] + "_";
    //        }
    //        chdagess += "#";
    //    }
    //var HtlURL = "HotelSearchs.asmx/GetHtlResult";
    var pkgHtml = ""
    var HtlURL = UrlBase + "Hotel/HotelSearchs.asmx/GetHtlResult";
    $.ajax({
        url: HtlURL,
        type: "POST",
        data: "{CheckInDate: '" + chkin + "', CheckOutDate: '" + chkout + "', HTLCity:'" + CityName + "', NoOfRooms:'" + rooms + "',TotalAdult:'" + totalAdult + "',TotalChild:'" + totalchild + "',TotalAdultperrooms:'" + AdultPerRoom + "',Totalchildperrooms:'" + ChildPerRoom + "',childage:'" + chdagess + "',HotelName :'" + htlname + "',starratings:'" + htlstar + "' }",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        //cache: false,
        //async: false,
        success: function(data) {
            var response = (data.d);
            Allhtllist = response; 
            if (response != null) {
                //if (response[0].HtlError == null) {
                $(".ShowhideNoResult").show();
                if (response.length <= 20) {
                    ShowHotels();
                }
                //$("#fademenu").show();
                //Hotel Displat HTML Seting Start
                var pkgHtml = ""; 
                for (var i = 0; i < response.length; i++) {
                    if (response[i].HtlError == null) {
                        if (i >= 18) {
                            ShowHotels();
                        }
                        pkgHtml += "<div class='list-item w100 brdr bgw'>";
                        if (response[i].DiscountMsg != "") {
                            pkgHtml += "<div class='Htldiscountrgt'>" + response[i].DiscountMsg + "</div>";
                            pkgHtml += "<p class='DealHotel hide '>DealHotel</p>";
                        }
                        pkgHtml += "<div class='clear'></div>";
                        pkgHtml += "<div class='htlbxmn'>";
                        pkgHtml += "<div class='htlbx padding1'>";
                        pkgHtml += "<div class='htlbximg'>";
                        pkgHtml += "<a href='javascript:void(0);' class='showRoomdetailss' id='" + response[i].HotelCode + "_" + response[i].HotelCityCode + "' title='" + response[i].HotelName + "'><img src='" + response[i].HotelThumbnailImg + "' /></a>";
                        pkgHtml += "</div>";
                        pkgHtml += "<div class='htlbx1'>";
                        //hotel Name and star start
                        pkgHtml += "<div class='htlttl'>";
                        pkgHtml += "<div class='ttlttl'><a href='javascript:void(0);' class='Htlname showRoomdetailss lft bld' id='" + response[i].HotelCode + "_" + response[i].HotelCityCode + "' title='" + response[i].HotelName + "' ><p class='title bld'>" + response[i].HotelName + "</p></a></div>";

                        pkgHtml += "<div class='lft statstar' style='margin:5px 0 0 3px;'>" + setStarratings(response[i].StarRating) + "</div>";
                        //map icon and policy start
                        pkgHtml += "<span class='lft padding1s'>";
                        if (response[i].Lati_Longi != "##") {
                            var MapStr = response[i].Lati_Longi + "##" + response[i].HotelName.replace("'", "") + "##" + response[i].HotelAddress.replace(", ,", " ") + "##";
                            MapStr += response[i].HotelCode + "_" + response[i].HotelCityCode + "|" + response[i].HotelName.replace("'", "") + "##";
                            MapStr += response[i].HotelThumbnailImg + "##" + response[i].StarRating + "##" + response[i].hotelPrice;
                            pkgHtml += "<a  href='#' onclick=\"PopupDiv('" + MapStr + "')\" > <img src='Images/Mapicon2.jpg' title='View on Map' style='height: 22px; width: 22px;' /></a>";
                        }
                        pkgHtml += "</span>";
                        //hotel Name and star end
                        //hotelprice start
                        pkgHtml += "<div class='rgt padding1'> ";
                        pkgHtml += "<div textContent ='" + response[i].hotelPrice + "' class='like'>";
                        pkgHtml += "<div class='rgt tital bld'>" + response[i].hotelPrice + "</div>";
                        pkgHtml += "<img src='Images/htlrs2.png' class='rgt' style='padding-top:8px;' />&nbsp;&nbsp;";
                        pkgHtml += "</div>"
                        pkgHtml += "</div>";
                        //hotelprice end
                        if (response[i].ReviewRating != "") {
                            pkgHtml += "<span class='rgt htlServisess1'>" + SetTripAdvisorRating(response[i].ReviewRating) + "</span>";
                        }
                        //map icon and policy end
                        pkgHtml += "</div>";

                        pkgHtml += "<div class='clear'></div>";
                        pkgHtml += "<div class='w75 padding1s lft HotelAddress LowCase'><span class='PropCase'>" + response[i].HotelAddress + "</span><div class='clear1'></div></div>";
                        //text start
                        pkgHtml += "<div class='w22 rgt textalignright rateRelate'>";
                        if (response[i].hotelDiscoutAmt > 0) {
                            pkgHtml += "<div class='discprice rgt'>";
                            pkgHtml += response[i].hotelDiscoutAmt;
                            pkgHtml += "</div>";
                            pkgHtml += " <div class='clear'></div>";
                        }
                        pkgHtml += "<div class='htlServisess1'>Per Room Per Night</div>";
                        pkgHtml += "<div class='SelectRoomCSS'>";
                        pkgHtml += "<a  href='javascript:void(0);' class='showRoomdetailss' style='color:#FFF;' id='" + response[i].HotelCode + "_" + response[i].HotelCityCode + "' title='" + response[i].HotelName + "' >Select room </a>";

                        pkgHtml += "</div>"
                        pkgHtml += "<div class='hide brekups cursorpointer underlineitalic w75 rgt f13 htlServisess1 headercolor' id='" + response[i].PopulerId + "_" + response[i].hotelPrice + "' title='" + response[i].StarRating + "'>Price To Agent</div>";
                        try {
                            if (response[i].StarRating != "") {

                                pkgHtml += "<p class='starRating hide'>" + response[i].StarRating.split('.')[0] + "</p>";
                            }
                            else
                                pkgHtml += "<p class='starRating hide'>" + response[i].StarRating + "</p>";
                        }
                        catch (ex) { }

                        pkgHtml += "<p class='location hide '>" + response[i].Location + "</p>";
                        pkgHtml += "<p class='Populerty hide '>" + response[i].PopulerId + "</p>";

                        pkgHtml += "</div>";
                        //text end

                        pkgHtml += "<div class='clear'></div>";
                        //HotelServise start
                        pkgHtml += "<div class='lft w100'>";
                        pkgHtml += "<span class='lft htlServisess1 w55'><p class='aminites'>";
                        if (response[i].Provider == "TG")
                            pkgHtml += response[i].HotelServices;
                        else if (response[i].Provider == "GTA")
                            pkgHtml += SetHotelService_GTA(response[i].HotelServices)
                        else if (response[i].Provider == "RoomXML")
                            pkgHtml += SetHotelService_RoomXML(response[i].HotelServices)
                        else if (response[i].Provider == "RZ")
                            pkgHtml += SetHotelService_RZ(response[i].HotelServices)
                        else if (response[i].Provider == "EX")
                            pkgHtml += SetHotelService_EX(response[i].HotelServices)

                        pkgHtml += "</p></span>";

                        pkgHtml += "<span class='rgt colorwhite'>" + response[i].Provider + "</span>";

                        pkgHtml += "<span class='w100 htlServisess1 lft'> ";

                        var inclusion = "";
                        if (response[i].inclusions != "") {
                            if (response[i].inclusions.indexOf("Pick up from the railway station") > 0 && response[i].inclusions.indexOf("Drop at the railway station") > 0)
                                inclusion += "<div class='clear'></div><span><span class='bld'>Include</span>: Pick up from the railway station, Drop at the railway station.</span><div class='clear'></div>";
                            else if (response[i].inclusions.indexOf("Pick up from the railway station") > 0)
                                inclusion += "<div class='clear'></div> <span><span class='bld'>Include</span>: Pick up from the railway station.</span><div class='clear'></div>";
                            else if (response[i].inclusions.indexOf("Drop at the railway station") > 0)
                                inclusion += "<div class='clear'></div> <span><span class='bld'>Include</span>: Drop at the railway station.</span><div class='clear'></div>";
                            else if (response[i].inclusions.indexOf("Pickup from Bus Stand") > 0)
                                inclusion += "<div class='clear'></div> <span><span class='bld'>Include</span>: Pickup from Bus Stand.</span><div class='clear'></div>";
                            else if (response[i].inclusions.indexOf("Airport Pickup") > 0)
                                inclusion += "<div class='clear'></div> <span><span class='bld'>Include</span>: Airport Pickup.</span><div class='clear'></div>";
                            else if (response[i].inclusions.indexOf("Airport Transfer") > 0)
                                inclusion += "<div class='clear'></div> <span><span class='bld'>Include</span>: Airport Transfer.</span><div class='clear'></div>";
                        }
                        pkgHtml += "<div class='lft'>" + inclusion + "</div>";
                        pkgHtml += "<div class='underlineitalic bld blue pointer rgt f12 htlServisess1' ";
                        if (response[i].Provider == "TG")
                            pkgHtml += "onclick=\"polcy('" + response[i].HotelCode + "','" + response[i].RatePlanCode + "','" + response[i].HotelName + "','" + response[i].Provider + "')\" >";
                        else if (response[i].Provider == "GTA")
                            pkgHtml += "onclick=\"polcy('" + response[i].HotelCode + "','" + response[i].HotelCityCode + "','" + response[i].HotelName + "','" + response[i].StarRating + "')\" >";
                        else if (response[i].Provider == "RZ")
                            pkgHtml += "onclick=\"polcy('" + response[i].HotelCode + "','" + response[i].RatePlanCode + "|" + response[i].RoomTypeCode + "','" + response[i].HotelName + "','" + response[i].Provider + "')\" >";
                        else if (response[i].Provider == "EX")
                            pkgHtml += "onclick=\"polcy('" + response[i].HotelCode + "','" + response[i].RoomTypeCode + "','" + response[i].HotelName + "','" + response[i].Provider + "')\" >";
                        else
                            pkgHtml += "onclick=\"polcy('" + response[i].RatePlanCode + "','" + response[i].StarRating + "','" + response[i].HotelName + "','" + response[i].Provider + "')\" >";
                        pkgHtml += "Cancellation Policy</div>"; 
                        pkgHtml += "<div  class='viewdtl underlineitalic bld blue pointer rgt f12 htlServisess1' >View Details &nbsp; &nbsp; </div>";
                        pkgHtml += "<div class='HtlDisciption lft w97 padding1s' style='display:none;' >";
                        
                        if (response[i].HotelDescription != "") {
                           //// if (response[i].HotelDescription.length > 229) {
                           //     pkgHtml += "<span>" + response[i].HotelDescription + "</span>";
                           //   //  pkgHtml += "<span id='RoomDiscDiv2'>" + response[i].HotelDescription.substring(229, response[i].HotelDescription.length) + "</span>";
                           //    // pkgHtml += "<span class='Hoteldisssss cursorpointer colormn bld' id='RoomDiscDiv' > View More..</span>";
                           // }
                           // else
                                pkgHtml += "<span class='lft'>" + response[i].HotelDescription + "</span>";
                        }
                        //pkgHtml += "<span class='rgt colorwhite'>" + response[i].Provider + "</span></div>";
                        pkgHtml += "</div>";
                        pkgHtml += "<div class='clear'></div>";
                        pkgHtml += "</div></span>";
                        //HotelServise end SetHotelService_RoomXML
                        //select button start
                         
                       

                        pkgHtml += "</div>";
                        pkgHtml += "</div>";
                        //select button and 
                        pkgHtml += "</div>";
                        pkgHtml += "</div></div>";
                       
                     
                        //pkgHtml += "<div class='clear'></div>";
                        //pkgHtml += "</div></div>";

                        // pkgHtml += "</div>";
                    }
                }
                $("#divhtldtls").html(pkgHtml);

                if (response.length < 4 && response[0].HtlError != null) {
                    if (response[0].HtlError != "SessionExpired") {
                        $("#divnoresult").show(); ShowHotels();
                        $("#divnoresult").html("<p>" + response[0].HtlError + "</p>");
                        $(".ShowhideNoResult").hide();
                    }
                    else
                        window.location.href = UrlBase + 'Login.aspx';
                }
                var strloc = "", strloc2 = "", locatopnss = "";
                var fltArray = JSLINQ(response).Select(function(item) { return item.Location; });

                var loclist = fltArray.items.unique();
                for (var j = 0; j < loclist.length; j++) {
                    var locsubstr = "";
                    if (loclist[j].length > 25)
                        locsubstr = loclist[j].substring(0, 25);
                    else
                        locsubstr = loclist[j];
                    if (j <= 10) {

                        strloc += '<div class="lft w10"> <input value="' + locsubstr + '"  id="CheckboxS' + j + 1 + '"  type="checkbox"  /> </div><div class="rgt w90"> <label for="' + locsubstr + '" title="' + loclist[j] + '">' + locsubstr + '</label></div><div class="clear"> </div>';
                    }
                    else {
                        strloc2 += '<div class="lft w10"> <input value="' + locsubstr + '"  id="CheckboxS' + j + 1 + '"  type="checkbox"  /> </div><div class="rgt w90"> <label for="' + locsubstr + '" title="' + loclist[j] + '">' + locsubstr + '</label></div><div class="clear"></div>';
                    }
                }
                if (strloc2.length > 0) {
                    locatopnss = '<div>' + strloc + '</div> <div id="alllocationDiv">' + strloc2 + '</div><div class="clear"></div><div><a href="javascript:void(0);" id="alllocationbtn" class="bld padding1s f16"> Show all ' + loclist.length + ' Location </a></div>';
                }
                else
                    locatopnss = '<div>' + strloc + '</div>';
                $("#divlocation").html(locatopnss);
                //Hotel Displat HTML Seting END

                var minprice = "", maxPrice = "", priceList = "";
                priceList = $(".like");
                if (priceList.length != 0) {
                    var arrPrice = new Array();
                    for (var pri = 0; pri < priceList.length; pri++) {
                        arrPrice.push({ id: pri, price: priceList[pri].textContent.trim().split(' ')[0] })
                    }
                    var sortprice = arrPrice.sort(function(a, b) {
                        return parseFloat(a.price) - parseFloat(b.price)
                    });

                    //var minprice = "", maxprice = "";
                    minprice = parseFloat(sortprice[0].price);
                    maxprice = parseFloat(sortprice[parseInt(sortprice.length) - 1].price);
                }
                else {
                    minprice = 0; maxprice = 0;
                }
                $('#demo').jplist({
                    //main options
                    itemsBox: '.list', itemPath: '.list-item', panelPath: '.jplist-panel'
                    //save plugin state
                            , storage: '' //'', 'cookies', 'localstorage'			
                            , storageName: 'jplist-list-grid'
                            , controlTypes: {
                                'range-slider-likes': {
                                    className: 'RangeSlider'
                                        , options: {
                                            //jquery ui range slider
                                            ui_slider: function($slider, $prev, $next) {
                                                $slider.slider({
                                                    min: minprice
                                                , max: maxprice
                                                , range: true
                                                , values: [minprice, maxprice]
                                                , slide: function(event, ui) {
                                                    $prev.html('<img src="Images/htlrs.png" /> ' + ui.values[0]);
                                                    $next.html('<img src="Images/htlrs.png" /> ' + ui.values[1]);
                                                }
                                                });
                                            }
                                        , set_values: function($slider, $prev, $next) {
                                            $prev.html('<img src="Images/htlrs.png" /> ' + $slider.slider('values', 0));
                                            $next.html('<img src="Images/htlrs.png" /> ' + $slider.slider('values', 1));
                                        }
                                        }
                                }
                                , 'checkbox-text-filter': {
                                    className: 'CheckboxTextFilter'
                                    , options: {
                                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                                    }
                                }
                            }
                });
                //                    }
                //                    else {
                //                        if (response[0].HtlError != "SessionExpired") {
                //                            $("#divnoresult").show(); ShowHotels();
                //                            $("#divnoresult").html("<p>" + response[0].HtlError + "</p>");
                //                            $(".ShowhideNoResult").hide();
                //                        }
                //                        else
                //                            window.location.href = UrlBase + 'Login.aspx';
                //                    }
                //                }
                //                else { $("#divnoresult").show(); ShowHotels(); $(".ShowhideNoResult").hide(); }
            }
            else { $("#divnoresult").show(); ShowHotels(); $(".ShowhideNoResult").hide(); }
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            $("#divnoresult").show(); ShowHotels(); $(".ShowhideNoResult").hide();
        }
    });


    //Show Map Div Start
    $(".map-view").click(function() {
        $(".Hotel_MapView").show();
        $(".Hotel_GridListView").hide();
        loadAllHotelMapnew(Allhtllist);
        //AllHotelMaps(Allhtllist);
        $("#filterdiv").hide();
        $("#khula").hide();
        $("#band").show();
        $("#resultdiv").addClass("w93");
        $("#map_canvas2").css("width", "740px");
        if ($('#DealHotel').is(":checked")) {
            $('#DealHotel').trigger('click');
        }
    });
    $(".other-view").click(function() {
        $(".Hotel_GridListView").show();
        $(".Hotel_MapView").hide();
        $('.simplemodal-close').trigger('click');
        $("#filterdiv").show();
        $("#khula").show();
        $("#band").hide();
        if ($('#DealHotel').is(":checked")) {
            $('#DealHotel').trigger('click');
        }
    });
    //    $(".deal_view").click(function() {
    //    $('.list-view').trigger('click');
    //    });
    //Show Map Div end 

    //$("#fademenu").click(function() {
    //    $("#fademenu").hide();

    //    $.ajax({
    //        url: 'HotelSearchs.asmx/RecentBookedHotel',
    //        contentType: 'application/json; charset=utf-8',
    //        type: 'POST', dataType: 'json',
    //        data: "{}",
    //        success: function(data) {
    //            if (data.d != "")
    //                $('#Recentbooked').html(data.d);
    //        }
    //    });
    //});



    //Hide Room Div Start
    $(".HidRoomPopup").click(function() {
        $("#RoomPopup").slideUp(2000);
        $("#RoomPopup_box").delay(2000).fadeOut(400);
        $(".Hotel_Modify").delay(200).fadeOut(400);
        $(".Hotel_ModifyOuter").hide();
        $(".HotelFareBrekup").hide();
    });
    //Hide Room Div end

    //Hide Room Div Start
    $(".HidRoomPopup2").click(function() {
        $(".HotelFareBrekup").hide();
    });
    //Hide Room Div end

    //Show Modify Div Start
    $("#Hotel_Modify").click(function() {
        $(".Hotel_ModifyOuter").show();
        $(".Hotel_Modify").fadeIn(200);
    });
    //Show Modify Div end

    $("#khula").click(function() {
        $("#filterdiv").hide();
        $("#khula").hide();
        $("#band").show();
        $("#resultdiv").addClass("w93");
    });
    $("#band").click(function() {
        $("#filterdiv").show();
        $("#band").hide();
        $("#khula").show();
        $("#resultdiv").removeClass("w93");
    });

    $(".ShowMoreFacilty").click(function () {
        if ($(this).text() == "More Amenities")
            $(this).text("Less Amenities");
        else
            $(this).text("More Amenities");

        var options = {};
        $(".hideFacilty").toggle('blind', options, 500);
    });

});


function ShowHotels() { $("#divloading").hide(); $("#main-content").show(); }
function HideHotels() { $("#divloading").show(); $("#main-content").hide(); }

ShowHotels();
HideHotels();