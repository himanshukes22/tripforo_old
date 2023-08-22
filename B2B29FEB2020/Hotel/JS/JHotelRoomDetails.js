$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('#htlmodify').addClass("htlmodify");
    } else {
        $('#htlmodify').removeClass("htlmodify");
    }
});

//Show Room Details Strat
$(document).on("click", ".showRoomdetailss", function (e) {
    $("#RoomPopup_box").fadeIn(1000);
    $("#RoomPopup").delay(1000).slideDown(1000);

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
    .success(function (result) {
        var Urlnbew = location.protocol + '//' + location.host;
        $("*").removeClass("roomtab1");
        $('#RoomRatestab').addClass("roomtab1");
        //RoomMapView($("#RRoomMaptab").text())
        $('.simplemodal-close').trigger('click');
        //$.modal.close();
        try {
            var room = result.d.RoomDetails;
            if (room != null) {
                if (room.length > 0) {
                    if (room[0].Room_Error == null) {
                        var RoomHtml = "<div id='RRoomRatestab' class='RoomTabCSS'>";
                        var i = 0;
                        for (var rm = 0; rm < room.length; rm++) {
                            //var brekups = " <a href='#' onclick=\"farebrekups('" + room[rm].TotalRoomrate + "','" + room[rm].MrkRateBreakups + "','" + room[rm].DiscountAMT + "','" + room[rm].DiscRoomrateBreakups + "','" + room[rm].Provider + "','" + result.d.SelectedHotelDetail.StarRating + "','" + room[rm].AgentMarkupAmt + "','" + room[rm].MrkTaxes + "','" + room[rm].ExtraGuest_Charge + "'," + room[rm].AgentCommissionAmt + "," + room[rm].GSTAmt + "," + room[rm].TDSAmt + ")\" style='color:#004b91;' >Price Breakup</a>";
                            var brekups = " <a href='#' onclick=\"farebrekups('" + room[rm].TotalRoomrate + "','" + room[rm].MrkRateBreakups + "','" + room[rm].DiscountAMT + "','" + room[rm].DiscRoomrateBreakups + "','" + room[rm].Provider + "','" + result.d.SelectedHotelDetail.StarRating + "','" + room[rm].AgentMarkupAmt + "','" + room[rm].MrkTaxes + "','" + room[rm].ExtraGuest_Charge + "'," + room[rm].AgentCommissionAmt + "," + room[rm].GSTAmt + "," + room[rm].TDSAmt + ")\" style='color:#004b91;' ><img src='" + Urlnbew + "/images/icons/faredetails.png'/></a>";

                            RoomHtml += "<div class='RoomImg'><img src='" + room[rm].RoomImage + "' /></div>";
                            RoomHtml += "<div class='roomhed'>";
                            i++;
                            if (i == 1)
                                RoomHtml += "<div class='lft w70'><h1>Room Name and Inclusions</h1></div><div class='rgt w10'>&nbsp;</div><div class='lft w20'><h1>Total room rates</h1></div>";

                            RoomHtml += "<div class='clear'></div><hr /><div class='clear'></div>";
                            //Inclusion and room name start
                            RoomHtml += "<div class='lft w70'>";
                            RoomHtml += "<div class='lft w90'><span class='bld f20 headercolor'>" + room[rm].RoomName + "</span>";
                            if (room[rm].inclusions != "")
                                RoomHtml += "<div>" + room[rm].inclusions + "</div>";
                            if (room[rm].discountMsg != "")
                                RoomHtml += "<div class='dscmsg'>" + room[rm].discountMsg + "</div>";
                            if (room[rm].Smoking == "true")
                                RoomHtml += "<div> Non Smoking room </div>";
                            if (room[rm].GuaranteeType != null)
                                if (room[rm].GuaranteeType != "")
                                    if (room[rm].GuaranteeType == "Guarantee")
                                        RoomHtml += "<div><span class='bld'>Pay At Hotel</span></div>";
                                    else
                                        RoomHtml += "<div><span class='bld'>Payment Requirement: </span>" + room[rm].GuaranteeType + " </div>";
                            RoomHtml += "</div></div>";
                            //Inclusion and room name end
                            RoomHtml += "<div class='rgt w10'><a class='book' style='color:#fff;' href='" + Urlnbew + "/Hotel/HotelCheckOut.aspx?&RoomCode=" + room[rm].RoomTypeCode + "&RoomRatePlan=" + room[rm].RatePlanCode + "&RoomRPH=" + room[rm].HotelCode + "&Provider=" + room[rm].Provider + "'>Book</a></div>";
                            //Fare info start
                            RoomHtml += "<div class='rgt w20'>";
                            RoomHtml += "<div class='rspr'><img src='images/htlrs.png' />" + room[rm].TotalRoomrate + brekups + "</div>";
                            if (room[rm].DiscountAMT > 0)
                                RoomHtml += "<div class='rsprst'><img src='images/htlrs.png' />" + room[rm].DiscountAMT + "</div>";
                            RoomHtml += "<div class='clear'></div>";
                            RoomHtml += "</div><div class='clear'></div>"; //Fare info close  
                            if (room[rm].Provider == "ZUMATA" || room[rm].Provider == "ROOMXML") {
                                RoomHtml += "<div id='P-" + i + "' title='" + room[rm].RatePlanCode + "'  class='rmdtl rgt w29 RoomDetailsSowHide'><h1>Cancellation Policy</h1></div>";
                                RoomHtml += "<div class='RoomHotelCode hide'>" + room[rm].HotelCode + "</div><div class='RoomProvider hide'>" + room[rm].Provider + "</div>";
                                RoomHtml += "<div class='clear'></div>";
                                RoomHtml += "<div id='Roomssdtl" + i + "' class='dspl'>";
                                
                               // if (result.d.SelectedHotelDetail.RoomAmenities != null)
                                 //   RoomHtml += "<div class='lft w98'><div class='bld'>" + room[rm].RoomName + " includes </div><div class='clear'></div>" + result.d.SelectedHotelDetail.RoomAmenities + "</div>";
                               // if (room[rm].RoomDescription != "")
                                 //   RoomHtml += "<div><div class='clear1'></div><h1>Room Description</h1>" + room[rm].RoomDescription + "</div>";

                                RoomHtml += "<div class='clear'></div><div>";
                                if (room[rm].CancelationPolicy != "") {
                                    RoomHtml += room[rm].CancelationPolicy ;
                                }
                                else {
                                    RoomHtml += "<div id='RXCanpolicy" + i + "'></div>";
                                }
                                RoomHtml += "</div><div class='clear'></div></div>";//cancelltion policy end  
                                // RoomHtml += "</div>"; //Hide div end 
                            }
                            else
                                RoomHtml += "<div><h1>Cancellation Policy</h1>" + room[rm].CancelationPolicy + "</div>";
                          
                            RoomHtml += "<div class='clear'></div> </div> <div class='clear'></div>"; // maindRoom end
                        } //for loop end
                        //for loop end
                        if (i == 0)
                            RoomHtml += "<span class='bld'>Room Rate Not found Please try another Hotel</span></div>";

                        RoomHtml += "<div class='clear'></div><div class='clear'></div></div>";
                        //Hotel Pictur start
                        RoomHtml += "<div id='RRoomPhototab' class='w95 mauto  roomppboxall RoomTabCSS'>";
                        RoomHtml += "<div class='lft w30'><img src='" + result.d.SelectedHotelDetail.ThumbnailUrl + "' class='w100' id='imgimgimg' alt='' /><div class='clear1'></div><div id='Imagename' class='bld f16'></div></div>";
                        RoomHtml += "<div class='rgt w66'>" + result.d.SelectedHotelDetail.HotelImage + "</div>";
                        RoomHtml += "</div>";
                        //Hotel Pictur End
                        RoomHtml += "<div id='RRoomDisciptipntab' class='w95 mauto  roomppboxall RoomTabCSS'><div class='clear1'></div>" ;
                        if (result.d.SelectedHotelDetail.HotelDescription != null)
                            RoomHtml += result.d.SelectedHotelDetail.HotelDescription;
                        if (result.d.SelectedHotelDetail.Attraction != null)
                            RoomHtml += "<div class='clear1'></div><p><b>Attraction : </b>" + result.d.SelectedHotelDetail.Attraction + "</p>";
                        if (result.d.SelectedHotelDetail.CheckInTime != null)
                            RoomHtml += "<div class='clear1'></div><p><b>Check in/out : </b> Hotel Check-in Time is " + result.d.SelectedHotelDetail.CheckInTime + " , Check-out Time is " + result.d.SelectedHotelDetail.CheckOutTime;
                        if (result.d.SelectedHotelDetail.FlexibleCheckIn != null) {
                            if (result.d.SelectedHotelDetail.FlexibleCheckIn == "true")
                                RoomHtml += " . Hotel allow Flexible CheckIn </p>";
                            else
                                RoomHtml += "</p>";
                        }
                        else
                            RoomHtml += "</p>";
                        RoomHtml += "</div>";
                        RoomHtml += "<div id='RRoomAmenitiestab' class='w95 mauto  roomppboxall RoomTabCSS'>" + result.d.SelectedHotelDetail.HotelAmenities + "</div>";
                        RoomHtml += "<div id='RRoomMaptab' class='w95 mauto  roomppboxall RoomTabCSS'>" + hotelname + "<div id='RoomMap_canvas'></div></div>";

                        if (result.d.SelectedHotelDetail.HotelAddress != null)
                            $(".selectedHotelAddress").text(result.d.SelectedHotelDetail.HotelAddress);
                        if (result.d.SelectedHotelDetail.StarRating != null)
                            $(".selectedHotelStarrating").html(setStarratings(result.d.SelectedHotelDetail.StarRating));

                        $('#RoomDetals div').empty('');
                        $("#RoomDetals").html(RoomHtml);
                       
                        $("#RRoomDisciptipntab").hide();
                        $("#RRoomAmenitiestab").hide();
                        $("#RRoomPhototab").hide();
                        $("#RRoomMaptab").hide();
                    }
                    else {
                        if (room[0].Room_Error == "SessionExpired")
                            window.location.href = UrlBase + 'Login.aspx';
                        else
                            $("#RoomDetals").html("<div> <div class='clear1'></div> <span class='bld'>" + room[0].Room_Error + "</span></div>");
                    }
                }
                else { $("#RoomDetals").html("<div> <div class='clear1'></div> <span class='bld'>Room Rate Not found Please try another Hotel</span></div>"); }
            }
            else { $("#RoomDetals").html("<div> <div class='clear1'></div> <span class='bld'>Room Rate Not found Please try another Hotel</span></div>"); }

        }
        catch (err) { }
    })
    .error(function (xhr, status) {
  
        $("#RoomDetals").html("Sory Room details not available");
    });
});
//Show Room Details End
//Room Details Sow Hide strat
$(document).on("click", ".RoomDetailsSowHide", function (e) {
    var roomid = this.id.split('-')[1];
    var options = {};
    if ($("#Roomssdtl" + roomid + "").css('display') == 'block') {
        $("#" + this.id).removeClass('rmdtl1');
        $("#" + this.id).addClass('rmdtl');
    }
    else if ($("#Roomssdtl" + roomid + "").css('display') == 'none') {
        $("#" + this.id).removeClass('rmdtl');
        $("#" + this.id).addClass('rmdtl1'); debugger;
        if (this.title != "" && $("#RXCanpolicy" + roomid).html() == "") {
            $("#RXCanpolicy" + roomid).html("<div style='position:relative; width: 100%; background: url(../images/Loadinganim.gif); background-repeat: no-repeat; background-position: center; text-align:center;'><img alt='loading' src='../images/Loadinganim.gif' /></div>");
            $.ajax({
                url: 'HotelSearchs.asmx/RoomCancelationPolicy',
                contentType: 'application/json; charset=utf-8',
                type: 'POST', dataType: 'json',
                data: "{'RatePlaneCode': '" + this.title + "','HotelCode': ' " + $(this).next().text() + "', 'Provider': '" + $(this).next().next().text() + "'}",
                success: function (data) {
                    if (data.d != "") {
                        $("#RXCanpolicy" + roomid).html(data.d);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        }
    }
    $("#Roomssdtl" + roomid + "").toggle('blind', options, 0);
});
//Room Details Sow Hide End

//Room tab Sow Hide strat
$(document).on("click", ".roomtab", function (e) {
    $("#RRoomRatestab").hide();
    $("#RRoomDisciptipntab").hide();
    $("#RRoomAmenitiestab").hide();
    $("#RRoomPhototab").hide();
    $("#RRoomMaptab").hide();

    $("#R" + this.id).show();
    $("*").removeClass("roomtab1");
    $('#' + this.id).addClass("roomtab1");
});
$('.showRoomPrices').click(function () {
    $("#RRoomRatestab").show();
    $("#RRoomDisciptipntab").hide();
    $("#RRoomAmenitiestab").hide();
    $("#RRoomPhototab").hide();
    $("#RRoomMaptab").hide();
    $("*").removeClass("roomtab1");
    $('#RoomRatestab').addClass("roomtab1");
});
//Room tab Sow Hide end
$(document).on("click", "#alllocationbtn", function (e) {
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
        success: function (data) {
            servisers = data.d;
        }
    });
    return servisers;
}
// Hotel Aminites end

//Hotel Description Show hide start
$(document).on("click", ".Hoteldisssss", function (e) {
    if (this.textContent == " View More..")
        this.textContent = " View Less..";
    else
        this.textContent = " View More..";
    var options = {};
    $(this).prev().toggle('blind', options, 500);
});
//Hotel Description Show hide end