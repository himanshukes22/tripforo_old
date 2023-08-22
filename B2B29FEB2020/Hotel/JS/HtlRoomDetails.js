$(window).scroll(function() {
    if ($(this).scrollTop() > 100) {
        $('#htlmodify').addClass("htlmodify");
    } else {
        $('#htlmodify').removeClass("htlmodify");
    }
});

//Show Room Details Strat
$(document).on("click", ".showRoomdetailss", function(e) {
    $("#RoomPopup_box").fadeIn(1000);
    $("#RoomPopup").delay(4000).slideDown(1000);

    $("#RoomDetals").html("<div style='position:relative; width: 100%; background: url(../images/Loadinganim.gif); background-repeat: no-repeat; background-position: center; text-align:center;'><img alt='loading' src='../images/Loadinganim.gif' /></div>");
    var hotelname = this.title; $("#selectedHotel").text(hotelname);
    var Reqitem = { "HotelName": this.title, "HotelCode": this.id }
    $.ajax({
        url: UrlBase + "Hotel/HotelSearchs.asmx/SelectedHotelSearch",
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(Reqitem)
    })
    .success(function(result) {
        
        $("*").removeClass("roomtab1");
        $('#RoomRatestab').addClass("roomtab1");
        //RoomMapView($("#RRoomMaptab").text())
        $('.simplemodal-close').trigger('click');
        //$.modal.close();
        try { 
            var room = result.d.RoomDetails;
            if (room != null) {
                if (room.length > 0) {
                    if (room[0].HtlError == null) {
                        var RoomHtml = "<div id='RRoomRatestab' class='RoomTabCSS'>";
                        var i = 0;
                        for (var rm = 0; rm < room.length; rm++) {
                            var brekups = " <a href='#' onclick=\"farebrekups('" + room[rm].TotalRoomrate + "','" + room[rm].MrkRateBreakups + "','" + room[rm].DiscountAMT + "','" + room[rm].DiscRoomrateBreakups + "','" + room[rm].Provider + "','" + result.d.SelectedHotelDetail.StarRating + "','" + room[rm].AgentMarkupAmt + "','" + room[rm].MrkTaxes + "','" + room[rm].ExtraGuest_Charge + "')\" style='color:#004b91;' >Price Breakup</a>";

                            if (room[rm].Provider == "TG") {
                                RoomHtml += "<div class='RoomImg'><img src='" + room[rm].RoomImage + "' title='" + room[rm].RoomName + "' /></div><div class='roomhed'>";
                                i++;
                                if (i == 1)
                                    RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='lft w20'><h1>Total room rates</h1></div>";

                                RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                                //Inclusion and room name start
                                RoomHtml += "<div class='inclu' id='inclu" + i + "'><div class='lft w70'>";
                                RoomHtml += "<div class='lft w90'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                                if (room[rm].inclusions != "")
                                    RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                                if (room[rm].discountMsg != "")
                                    RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                                RoomHtml += "</div></div>";
                                //Inclusion and room name end
                                RoomHtml += "<div class='rgt w10'><a class='book' style='color:#fff;' href='HotelCheckOut.aspx?&RoomCode=" + room[rm].RoomTypeCode + "&RoomRatePlan=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=TG'>Book</a></div>";

                                RoomHtml += "<div class='rgt w20'>";
                                RoomHtml += "<div class='rspr'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";

                                if (room[rm].DiscountAMT > 0)
                                    RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";
                                RoomHtml += "<div >";

                                RoomHtml += "</div>";

                                RoomHtml += "<div class='clear1'></div></div><div class='clear1'></div>"; //roomDiscPrice close                       
                                //RoomResponse.Append("<div id='R-" + i + "' class='rmdtl w70 RoomDetailsSowHide'><h1>Room Details</h1></div>");
                                RoomHtml += "<div id='P-" + i + "' class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Room Details</h1></div>";
                                RoomHtml += "<div class='clear'></div>";
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                if (room[rm].Smoking == "true")
                                    RoomHtml += "<div> Non Smoking room </div>";
                                RoomHtml += "<div class='lft w98'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + result.d.SelectedHotelDetail.RoomAmenities + "</div>";

                                RoomHtml += "<div class='clear1'></div>";
                                RoomHtml += "<div>"; //cancelltion policy strat 
                                if (room[rm].RoomDescription != "")
                                    RoomHtml += "<div><div class='clear1'></div><h1>Room Description</h1>" + room[rm].RoomDescription + "</div>";
                                else
                                    RoomHtml += "<div><div class='clear1'></div></div>";
                                RoomHtml += "<div class='clear1'></div><div><h1>Cancellation Policy</h1>" + room[rm].CancelationPolicy + "</div>";
                                RoomHtml += "</div>"; //cancelltion policy end
                                RoomHtml += "<div class='clear'></div></div></div>"; //Hide div end 
                                RoomHtml += "<div class='clear'></div></div><div class='clear'></div>"; // maindRoom end
                            }
                            else if (room[rm].Provider == "GTA") {
                                RoomHtml += "<div>";
                                RoomHtml += "<div class='RoomImg'><img src='" + result.d.SelectedHotelDetail.ThumbnailUrl + "' title='" + room[rm].RoomName + "' /></div><div class='roomhed'>";
                                i++;
                                if (i == 1) {
                                    RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='rgt w20'><h1>Total room rates</h1></div>";
                                }
                                RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                                RoomHtml += "<div class='inclu' id='inclu" + i + "'><div class='lft w70'>";
                                RoomHtml += "<div class='lft'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                                if (room[rm].inclusions != "")
                                    RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                                if (room[rm].discountMsg != "")
                                    RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                                if (room[rm].Smoking != "") {
                                    RoomHtml += "<div ><span class='lft bld'>Child bed information: </span>";
                                    if (room[rm].Smoking == "true")
                                        RoomHtml += "** Child share existing bedding **";
                                    else
                                        RoomHtml += "** Child provided with extra bed **";
                                    RoomHtml += "</div><div class='clear'></div>";
                                }

                                RoomHtml += "</div></div>"; //RoomNameInclusion close

                                RoomHtml += "<div class='rgt w10'><a Class='book' style='color:#fff;'  href='HotelCheckOutInt.aspx?&RoomCode=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=GTA'>Book</a></div>";

                                RoomHtml += "<div class='rgt w20'>";
                                RoomHtml += "<div class='rspr'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";
                                if (room[rm].DiscountAMT > room.TotalRoomrate)
                                    RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";

                                RoomHtml += "<div class='clear1'></div></div><div class='clear1'></div>"; //roomDiscPrice close
                                //RoomResponse.Append("<div id='R-" + i + "' class='rmdtl w70 RoomDetailsSowHide'><h1>Room Details</h1></div>");
                                RoomHtml += "<div id='P-" + i + "' class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Room Details</h1></div>";
                                RoomHtml += "<div class='clear'></div>";
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                RoomHtml += "<div >" + room[rm].EssentialInformation + "</div>";
                                RoomHtml += "<div class='lft w95'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + result.d.SelectedHotelDetail.RoomAmenities + "</div>";
                                //RoomHtml += "<div class='rgt w29'>FareBreakup</div>";
                                RoomHtml += "<div class='clear1'></div>";

                                if (room[rm].RoomDescription != "")
                                    RoomHtml += "<div><div class='clear1'></div><h1>Room Description</h1>" + room[rm].RoomDescription + "</div>";
                                else
                                    RoomHtml += "<div><div class='clear1'></div></div>";

                                RoomHtml += "<div class='clear1'></div><div><h1>Cancellation Policy</h1>" + room[rm].CancelationPolicy + "</div>";
                                RoomHtml += "<div class='clear'></div></div>";
                                RoomHtml += "</div></div>"; //Hide div end 
                                RoomHtml += "<div class='clear'></div></div><div class='clear'></div>"; // maindRoom end
                            }
                            else if (room[rm].Provider == "RoomXML") {

                                RoomHtml += "<div>";
                                RoomHtml += "<div class='RoomImg'><img src='" + result.d.SelectedHotelDetail.ThumbnailUrl + "' title='" + room[rm].RoomName.replace("<div class='clear1'></div>", "").replace("<div class='clear1'></div>", "").replace("<div class='clear1'></div>", "").replace("<div class='clear1'></div>", "") + "' /></div><div class='roomhed'>";
                                i++;
                                if (i == 1) {
                                    RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='rgt w20'><h1>Total room rates</h1></div>";
                                }
                                RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                                RoomHtml += "<div class='inclu' id='inclu" + i + "'><div class='lft w70'>";
                                RoomHtml += "<div class='lft'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                                if (room[rm].inclusions != "")
                                    RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                                if (room[rm].discountMsg != "")
                                    RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                                if (room[rm].Smoking != "") {
                                    RoomHtml += "<div ><span class='lft bld'>Child bed information: </span>";
                                    if (room[rm].Smoking == "true")
                                        RoomHtml += "** Child share existing bedding **";
                                    else
                                        RoomHtml += "** Child share existing bedding **";
                                    RoomHtml += "</div><div class='clear'></div>";
                                }

                                RoomHtml += "</div></div>"; //RoomNameInclusion close

                                RoomHtml += "<div class='rgt w10'><a Class='book' style='color:#fff;'  href='HotelCheckOutInt.aspx?&RoomCode=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=RX'>Book</a></div>";

                                RoomHtml += "<div class='rgt w20'>";
                                RoomHtml += "<div class='rspr'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";
                                if (room[rm].DiscountAMT > room.TotalRoomrate)
                                    RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";

                                RoomHtml += "<div class='clear1'></div></div><div class='clear1'></div>"; //roomDiscPrice close
                                //RoomResponse.Append("<div id='R-" + i + "' class='rmdtl w70 RoomDetailsSowHide'><h1>Room Details</h1></div>");
                                RoomHtml += "<div id='P-" + i + "' title='" + room[rm].RatePlanCode + "' class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Cancellation Policy</h1></div>";
                                RoomHtml += "<div class='clear'></div>";
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                // RoomHtml += "<div >" + room[rm].EssentialInformation + "</div>";
                                // RoomHtml += "<div class='lft w95'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + result.d.SelectedHotelDetail.RoomAmenities + "</div>";
                                //RoomHtml += "<div class='rgt w29'>FareBreakup</div>";
                                // RoomHtml += "<div class='clear1'></div>";

                                if (room[rm].RoomDescription != "")
                                    RoomHtml += "<div><div class='clear1'></div><h1>Room Description</h1>" + room[rm].RoomDescription + "</div>";
                                else
                                    RoomHtml += "<div><div class='clear1'></div></div>";
                                //room[rm].CancelationPolicy
                                RoomHtml += "<div class='clear1'></div><div id='RXCanpolicy" + i + "'></div>";
                                RoomHtml += "<div class='clear'></div></div>";
                                RoomHtml += "</div></div>"; //Hide div end 
                                RoomHtml += "<div class='clear'></div></div><div class='clear'></div>"; // maindRoom end
                            }
                            else if (room[rm].Provider == "RZ") {
                                RoomHtml += "<div class='RoomImg'><img src='" + room[rm].RoomImage + "' title='" + room[rm].RoomName + "' /></div><div class='roomhed'>";
                                i++;
                                if (i == 1)
                                    RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='lft w20'><h1>Total room rates</h1></div>";

                                RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                                //Inclusion and room name start
                                RoomHtml += "<div class='inclu' id='inclu" + i + "'><div class='lft w70'>";
                                RoomHtml += "<div class='lft w90'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                                if (room[rm].inclusions != "")
                                    RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                                if (room[rm].discountMsg != "")
                                    RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                                RoomHtml += "</div></div>";
                                //Inclusion and room name end
                                RoomHtml += "<div class='rgt w10'><a class='book' style='color:#fff;' href='HotelCheckOut.aspx?&RoomCode=" + room[rm].RoomTypeCode + "&RoomRatePlan=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=RZ'>Book</a></div>";

                                RoomHtml += "<div class='rgt w20'>";
                                RoomHtml += "<div class='rspr'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";

                                if (room[rm].DiscountAMT > 0)
                                    RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";
                                RoomHtml += "<div >";

                                RoomHtml += "</div>";

                                RoomHtml += "<div class='clear1'></div></div><div class='clear1'></div>"; //roomDiscPrice close                       
                                //RoomResponse.Append("<div id='R-" + i + "' class='rmdtl w70 RoomDetailsSowHide'><h1>Room Details</h1></div>");
                                RoomHtml += "<div id='P-" + i + "' class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Room Details</h1></div>";
                                RoomHtml += "<div class='clear'></div>";
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                //  if (room[rm].Smoking == "true")
                                //  RoomHtml += "<div> Non Smoking room </div>";
                                RoomHtml += "<div class='lft w98'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + room[rm].Smoking + "</div>";

                                RoomHtml += "<div class='clear1'></div>";
                                RoomHtml += "<div>"; //cancelltion policy strat 
                                RoomHtml += "<div><div class='clear1'></div></div>";
                                RoomHtml += "<div class='clear1'></div><div><h1>Cancellation Policy</h1>" + room[rm].CancelationPolicy + "</div>";
                                RoomHtml += "</div>"; //cancelltion policy end
                                RoomHtml += "<div class='clear'></div></div></div>"; //Hide div end 
                                RoomHtml += "<div class='clear'></div></div><div class='clear'></div>"; // maindRoom end
                            }
                            if (room[rm].Provider == "EX") {
                                RoomHtml += "<div class='RoomImg'><img src='" + room[rm].RoomImage + "' title='" + room[rm].RoomName + "' /></div><div class='roomhed'>";
                                i++;
                                if (i == 1)
                                    RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='lft w20'><h1>Total room rates</h1></div>";

                                RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                                //Inclusion and room name start
                                RoomHtml += "<div class='inclu' id='inclu" + i + "'><div class='lft w70'>";
                                RoomHtml += "<div class='lft w90'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                                if (room[rm].inclusions != "")
                                    RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                                if (room[rm].discountMsg != "")
                                    RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                                RoomHtml += "</div></div>";
                                //Inclusion and room name end
                                RoomHtml += "<div class='rgt w10'><a class='book' style='color:#fff;' href='HotelCheckOutInt.aspx?RoomCode=" + room[rm].RoomTypeCode + "&RoomRatePlan=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=EX'>Book</a></div>";

                                RoomHtml += "<div class='rgt w20'>";
                                RoomHtml += "<div class='rsprBreakup'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";

                                if (room[rm].DiscountAMT > 0)
                                    RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";
                                RoomHtml += "<div >";

                                RoomHtml += "</div>";

                                RoomHtml += "<div class='clear1'></div></div><div class='clear1'></div>"; //roomDiscPrice close                       
                                //RoomResponse.Append("<div id='R-" + i + "' class='rmdtl w70 RoomDetailsSowHide'><h1>Room Details</h1></div>");
                                RoomHtml += "<div id='P-" + i + "' class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Room Details</h1></div>";
                                RoomHtml += "<div class='clear'></div>";

                                RoomHtml += "<div id='CP-" + i + "' class='rgt bld'>" + room[rm].EssentialInformation + "</div>";
                                RoomHtml += "<div class='clear'></div>";
                                
                                
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                if (room[rm].Smoking == "true")
                                    RoomHtml += "<div> Non Smoking room </div>";
                                RoomHtml += "<div class='lft w98'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + result.d.SelectedHotelDetail.RoomAmenities + "</div>";

                                RoomHtml += "<div class='clear1'></div>";
                                RoomHtml += "<div>"; //cancelltion policy strat 
                                if (room[rm].RoomDescription != "")
                                    RoomHtml += "<div><div class='clear1'></div><h1>Room Description</h1>" + room[rm].RoomDescription + "</div>";
                                else
                                    RoomHtml += "<div><div class='clear1'></div></div>";
                                RoomHtml += "<div class='clear1'></div><div><h1>Cancellation Policy</h1>" + room[rm].CancelationPolicy + "</div>";
                                RoomHtml += "</div>"; //cancelltion policy end
                                RoomHtml += "<div class='clear'></div></div></div>"; //Hide div end 
                                RoomHtml += "<div class='clear'></div></div><div class='clear'></div>"; // maindRoom end
                            }
                        } //for loop end
                        if (i > 0)
                            RoomHtml += "</div>";
                        else
                            RoomHtml += "Room Rate Not found Please try another Hotel</div>";

                        RoomHtml += "<div class='clear'>";
                        //Hotel Pictur start
                        RoomHtml += "<div id='RRoomPhototab' class='w95 mauto  roomppboxall RoomTabCSS'>";
                        RoomHtml += "<div class='lft w30'><img src='" + result.d.SelectedHotelDetail.ThumbnailUrl + "' class='w100' id='imgimgimg' alt='' /></div>";
                        RoomHtml += "<div class='rgt w66'>" + result.d.SelectedHotelDetail.HotelImage + "</div>";
                        RoomHtml += "</div>";
                        //Hotel Pictur End
                        RoomHtml += "<div id='RRoomDisciptipntab' class='w95 mauto  roomppboxall RoomTabCSS'><div class='clear1'></div>" + result.d.SelectedHotelDetail.HotelDescription + "</div>";
                        RoomHtml += "<div id='RRoomAmenitiestab' class='w95 mauto  roomppboxall RoomTabCSS'>" + result.d.SelectedHotelDetail.HotelAmenities + "</div>";
                        RoomHtml += "<div id='RRoomMaptab' class='w95 mauto  roomppboxall RoomTabCSS'>" + hotelname + "<div id='RoomMap_canvas'></div></div>";
                        RoomHtml += "</div>";
                        $('#RoomDetals div').empty('');
                        $("#RoomDetals").html(RoomHtml);
                    }
                    else {
                        if (room[0].HtlError == "SessionExpired")
                            window.location.href = UrlBase + 'Login.aspx';
                        else
                            $("#RoomDetals").text(room[0].HtlError);
                    }
                }
                else
                { $("#RoomDetals").text("Room Rate Not found Please try another Hotel"); }
            }
            else
            { $("#RoomDetals").text("Room Rate Not found Please try another Hotel"); }

        }
        catch (err) { }
    })
    .error(function(xhr, status) {
        $("#RoomDetals").html("Sory Room details not available");
    });
});
//Show Room Details End
//Room Details Sow Hide strat
$(document).on("click", ".RoomDetailsSowHide", function(e) {
    var roomid = this.id.split('-')[1];
    var options = {};
    if ($("#Roomssdtl" + roomid + "").css('display') == 'block') {
        $("#" + this.id).removeClass('rmdtl1');
        $("#" + this.id).addClass('rmdtl');
    }
    else if ($("#Roomssdtl" + roomid + "").css('display') == 'none') {
        $("#" + this.id).removeClass('rmdtl');
        $("#" + this.id).addClass('rmdtl1');
        if (this.title != "" && $("#RXCanpolicy" + roomid).html() == "") {
            $("#RXCanpolicy" + roomid).html("<div style='position:relative; width: 100%; background: url(../images/Loadinganim.gif); background-repeat: no-repeat; background-position: center; text-align:center;'><img alt='loading' src='../images/Loadinganim.gif' /></div>");
            $.ajax({
                url: 'HotelSearchs.asmx/RoomXMLCancellationPolicy',
                contentType: 'application/json; charset=utf-8',
                type: 'POST', dataType: 'json',
                data: "{'Quadid': '" + this.title + "'}",
                success: function(data) {
                    if (data.d != "") {
                        $("#RXCanpolicy" + roomid).html(data.d);
                    }
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        }
    }
    $("#Roomssdtl" + roomid + "").toggle('blind', options, 0);
});
//Room Details Sow Hide End

//Room tab Sow Hide strat
$(document).on("click", ".roomtab", function(e) {
    $("#RRoomRatestab").hide();
    $("#RRoomDisciptipntab").hide();
    $("#RRoomAmenitiestab").hide();
    $("#RRoomPhototab").hide();
    $("#RRoomMaptab").hide();

    $("#R" + this.id).show();
    $("*").removeClass("roomtab1");
    $('#' + this.id).addClass("roomtab1");
});
$('.showRoomPrices').click(function() {
    $("#RRoomRatestab").show();
    $("#RRoomDisciptipntab").hide();
    $("#RRoomAmenitiestab").hide();
    $("#RRoomPhototab").hide();
    $("#RRoomMaptab").hide();
    $("*").removeClass("roomtab1");
    $('#RoomRatestab').addClass("roomtab1");
});
//Room tab Sow Hide end
$(document).on("click", "#alllocationbtn", function(e) {
    var strlocss = $(this).text();
    if (strlocss.indexOf("Show") >= 0)
        $(this).text(strlocss.replace("Show", "Hide"));
    else
        $(this).text(strlocss.replace("Hide", "Show"));

    var options = {};
    $("#alllocationDiv").toggle('blind', options, 500);
});

function ShowHtlImg(id) {
    var imgimg = id.src;
    $("#imgimgimg").attr('src', imgimg);
}

// Hotel Aminites Start
function TGHotelServise(HotelCode) {
    var servisers = "";
    $.ajax({
        url: 'HotelSearchs.asmx/HotelServices',
        contentType: 'application/json; charset=utf-8', type: 'POST', dataType: 'json',
        data: "{'HotelCode': '" + HotelCode + "'}",
        success: function(data) {
            servisers = data.d;
        }
    });
    return servisers;
}
// Hotel Aminites end

//Hotel Description Show hide start
$(document).on("click", ".Hoteldisssss", function(e) {
    if (this.textContent == " View More..")
        this.textContent = " View Less..";
    else
        this.textContent = " View More..";
    var options = {};
    $(this).prev().toggle('blind', options, 500);
});
//Hotel Description Show hide end