// Hotel Details Page Map start
$(function() {
    $("#SowInMap").click(function() {
        var address = $(".locations")[0].innerText;
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': address }, function(results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var map_center = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                var myOptions = { zoom: 15, center: map_center, panControl: true, zoomControl: true, mapTypeControl: true,
                    scaleControl: false, streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
                var markers = new google.maps.Marker({ position: map_center, map: map, title: $(".Hotelnames")[0].innerText });
                markers.setIcon("Images/marker2.png");
                var infowindows1 = new google.maps.InfoWindow({ content: 'Loading...' });
                google.maps.event.addListener(markers, 'mouseover', function() {
                    infowindows1.setContent(this.html);
                    infowindows1.open(map, this);
                });
            }
            else { document.getElementById('map_canvas').innerText = "Address Not found"; }
        });
    });
});
// Hotel Details Page Map End

// Hotel Details Cancellation Plicy Start
function Cancellation(id) {
    $("#Can" + id + "").focus();
    var options = {};
    $("#Can" + id + "").toggle('blind', options, 0);
}
// Hotel DetailsCancellation Plicy end
//Image show
function ShowHtlImg(id) {
    var imgimg = id.src;
    $("#ctl00_ContentPlaceHolder1_HtlfirstImg").attr('src', imgimg);
}

// Show cot information for 1 minut
function ShowAlertMassage() {
    $("#CotMsg").show();
    $('#CotMsg').delay(60000).queue(function() { $(this).hide(); $(this).dequeue(); })
}
// Show cot information for 1 minut
function ShowRoomAlertMassage(msg) {
    $("#ExtraRoomMsg").show();
    $("#ExtraRoomMsg").text(msg);
}
//// initialize the TN3 when the DOM is ready
//$(document).ready(function() {
//    //Thumbnailer.config.shaderOpacity = 1;
//    var tn1 = $('.mygallery').tn3({
//        skinDir: "skins",
//        imageClick: "fullscreen",
//        image: {
//            maxZoom: 1.5,
//            crop: true,
//            clickEvent: "dblclick",
//            transitions: [{
//                type: "blinds"
//            }, {
//                type: "grid"
//            }, {
//                type: "grid",
//                duration: 460,
//                easing: "easeInQuad",
//                gridX: 1,
//                gridY: 8,
//                // flat, diagonal, circle, random
//                sort: "random",
//                sortReverse: false,
//                diagonalStart: "bl",
//                // fade, scale
//                method: "scale",
//                partDuration: 360,
//                partEasing: "easeOutSine",
//                partDirection: "left"
//}]
//            }
//        });
//        
//        loadMap();
//    });



// //Load Map
//    function loadMap() {
////        ;
////    var map1 = new GMap2(document.getElementById("map_canvas"));
////    //map.addControl(new GSmallMapControl());
////   // map.addControl(new GMapTypeControl());
////    geocoder = new GClientGeocoder();
////    if (geocoder) {
////        geocoder.getLatLng(Address,
////                     function(point) {
////                         if (point) {

////                             var asa = point.lat();
////                             var asa1 = point.lng();
////                         }
////                     }
////                     );
////                 }
//    
//    
//    
//      var map_center = new google.maps.LatLng(document.getElementById('ctl00_ContentPlaceHolder1_lat').value, document.getElementById('ctl00_ContentPlaceHolder1_lng').value);
//        var myOptions = {
//        zoom: 14, center: map_center, panControl: true, zoomControl: true, mapTypeControl: true, scaleControl: true,
//        streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
//        }
//       var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
//       var html_text = '<table><tr><td width="200px"><strong>' + $("#ctl00_ContentPlaceHolder1_HtlNameLbl").text() + '</strong><br />' + $("#ctl00_ContentPlaceHolder1_HtlLoc").text() + '</td></tr><tr><td> <table><tr><td><img src=' + $('img[title="Hotels"]').attr('src') + ' /></td><td>' + $("#ctl00_ContentPlaceHolder1_HtlStrImg").text() +'</td></tr></table> </td></tr></table>';
//       var point = new google.maps.LatLng(document.getElementById('ctl00_ContentPlaceHolder1_lat').value, document.getElementById('ctl00_ContentPlaceHolder1_lng').value);
//       var marker = new google.maps.Marker({
//            position: point, map: map,
//            title: $("#ctl00_ContentPlaceHolder1_HtlNameLbl").text(),
//            icon: "Images/marker.png"
//        });
//        //marker.setIcon("Images/marker.png");
//       var infowindows = new google.maps.InfoWindow({
//            content: html_text
//        });
//        google.maps.event.addListener(marker, 'mouseover', function() {
//            infowindows.open(map, marker);
//        });
//    }
//    
//    function Occupancy(OccAdt,OccChd,OccInf,OccMax) {        
//        document.getElementById("MaxAdt").innerText = OccAdt;
//        document.getElementById("MaxChd").innerText = OccChd;
//        document.getElementById("MaxInf").innerText = OccInf;
//        document.getElementById("MaxGuest").innerText = OccMax;
//    }

//    //Show Occupancy Popup
//    function PopupDiv(OccAdt,OccChd,OccInf,OccMax) {
//        // When site loaded, load the Popupbox First
//        loadPopupBox();

//        $('#MappopupBoxClose1').click(function() {
//            unloadPopupBox();
//        });

//        $('#Mapcontainer1').click(function() {
//            unloadPopupBox();
//        });

//        function unloadPopupBox() {    // TO Unload the Popupbox
//            $('#MapPopup_box1').fadeOut("slow");
//            $("#Mapcontainer1").css({ // this is just for style       
//                "opacity": "1"
//            });
//        }

//        function loadPopupBox() {    // To Load the Popupbox
//            $('#MapPopup_box1').fadeIn("slow");
//            $("#Mapcontainer1").css({ // this is just for style
//                "opacity": "0.3"
//            });
//        }
//        //Occupancy(OccAdt,OccChd,OccInf,OccMax);
//    }
//    //Hide Map Popup
//    function PopupDiv1() {

//        // When site loaded, load the Popupbox First
//        unloadPopupBox();

//        $('#MappopupBoxClose1').click(function() {
//            unloadPopupBox();
//        });

//        $('#Mapcontainer1').click(function() {
//            unloadPopupBox();
//        });

//        function unloadPopupBox() {    // TO Unload the Popupbox
//            $('#MapPopup_box1').fadeOut("slow");
//            $("#Mapcontainer1").css({ // this is just for style       
//                "opacity": "1"
//            });
//        }

//        function loadPopupBox() {    // To Load the Popupbox
//            $('#MapPopup_box1').fadeIn("slow");
//            $("#container1").css({ // this is just for style
//                "opacity": "0.3"
//            });
//        }
//    }




////Hide true false for Inclusion
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions0").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions0").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions1").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions1").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions2").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions2").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions3").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions3").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions4").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions4").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions5").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions5").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions6").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions6").click(function() {
//        runEffect();
//        return false;
//    });
//}); $(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions7").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions7").click(function() {
//        runEffect();
//        return false;
//    });
//}); $(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions8").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions8").click(function() {
//        runEffect();
//        return false;
//    });
//});
//$(function() {
//    // run the currently selected effect
//    function runEffect(ii) {
//        // get effect type from 
//        var selectedEffect = 'blind';
//        // most effect types need no options passed by default
//        var options = {};
//        // run the effect
//        $("#Inclusions9").toggle(selectedEffect, options, 500);
//    };

//    // set effect from select menu value
//    $("#btnInclusions9").click(function() {
//        runEffect();
//        return false;
//    });
//});

