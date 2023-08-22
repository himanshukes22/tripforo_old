
//document.write('<script type="text/javascript" src="../Hotel/JS/HtlRoomDetails.js"></script>');

//Show Map Popup
function PopupDiv(MapStr) {
    $(function() {
        $('#basic-modal-content').modal();
        loadRepeaterMap(MapStr);
        return false;
    });
}
function loadPopupBox() {    // To Load the Popupbox
    $('#MapPopup_box').fadeIn("slow");
    $("#Mapcontainer").css({ // this is just for style
        "opacity": "0.3"
    });
}
//Load Repeater Map start
function loadRepeaterMap(htlstr) {
    var infowindows1 = "";
    $('#map_canvas').show(); $('#mapss').show();
    var strmap = htlstr.split("##");
    if (strmap.length > 1) {
        $('#selected_Hotel').text(strmap[2]);
        $('#selected_City').text(strmap[3]);
        //var map_center = new google.maps.LatLng(strmap[0], strmap[1]);
        //var myOptions = {
        //    zoom: 16, center: map_center, panControl: true, zoomControl: true, mapTypeControl: true, scaleControl: false,
        //    streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
        //}
        //var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
        var map = new google.maps.Map(document.getElementById('map_canvas'), {
            center: { lat: parseFloat(strmap[0]), lng: parseFloat(strmap[1]) },
            zoom: 16, panControl: true, zoomControl: true, mapTypeControl: true, streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        var infowindow = new google.maps.InfoWindow();
        var service = new google.maps.places.PlacesService(map);
        var roomurl = strmap[4].split('|');
        var Mapurl = "<a href='#' class='showRoomdetailss' id='" + roomurl[0] + "' title='" + roomurl[1] + "' style='color:White; font-weight:bold;'>Select</a>";

        var html_text = '<table width="280" cellpadding="0" cellspacing="0" border="0" style="color:#000;">';
        html_text += '<tr><td colspan="3"><strong>' + strmap[2] + '</strong><br />' + strmap[3] + '</td></tr>';
        html_text += '<tr><td valign="top"><img src=' + strmap[5] + ' style="width:85px;hight:85px;" /></td>'
        html_text += '<td valign="top">' + setStarratings(strmap[6]) + ' <br />';
        html_text += '<img src="Images/htlrs.png" /> <strong>' + strmap[7] + '</strong> <br />Per Room Per Night</td>';
        html_text += '<td valign="top"><div class="SelectRoomCSS">';
        html_text += '<img src="images/check.png" />' + Mapurl + '</div></td></tr></table>';

        var points = new google.maps.LatLng(strmap[0], strmap[1]);
        var marker = new google.maps.Marker({ position: points, map: map, title: strmap[2], html: html_text });
        marker.setIcon("Images/marker2.png");
        google.maps.event.addListener(marker, 'mouseover', function () {
            infowindow.setContent(this.html);
            infowindow.open(map, this);
        });
        //var markers = new google.maps.Marker({ position: map_center, map: map, title: strmap[2], html: html_text });
        //markers.setIcon("Images/marker2.png");
        //google.maps.event.addListener(markers, 'mouseover', function() {
        //    infowindows1.setContent(this.html);
        //    infowindows1.open(map, this);
        //});
    }
    else
        document.getElementById('selected_Hotel').innerText = "Address not found";
    infowindows1 = new google.maps.InfoWindow({ content: 'Loading...' });
}
function loadRepeaterMap_old(htlstr) { 
    var infowindows1 = "";
    $('#map_canvas').show(); $('#mapss').show();
    var strmap = htlstr.split("##");
    if (strmap.length > 1) {
        $('#selected_Hotel').text(strmap[2]);
        $('#selected_City').text(strmap[3]);
        var map_center = new google.maps.LatLng(strmap[0], strmap[1]);
        var myOptions = {
            zoom: 16, center: map_center, panControl: true, zoomControl: true, mapTypeControl: true, scaleControl: false,
            streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
        var roomurl = strmap[4].split('|');
        var Mapurl = "<a href='#' class='showRoomdetailss' id='" + roomurl[0] + "' title='" + roomurl[1] + "' style='color:White; font-weight:bold;'>Select</a>";
      
        var html_text = '<table width="280" cellpadding="0" cellspacing="0" border="0" style="color:#000;">';
        html_text +=  '<tr><td colspan="3"><strong>' + strmap[2] + '</strong><br />' + strmap[3] + '</td></tr>';
        html_text +=  '<tr><td valign="top"><img src=' + strmap[5] + ' style="width:85px;hight:85px;" /></td>'
        html_text +=  '<td valign="top">' + setStarratings(strmap[6]) + ' <br />';
        html_text +=  '<img src="Images/htlrs.png" /> <strong>' + strmap[7] + '</strong> <br />Per Room Per Night</td>';
        html_text +=  '<td valign="top"><div style="font-weight: bold; color: #fff; padding: 0 8px; background: url(images/btnbgk.png);';
        html_text +=  'height: 25px; line-height: 25px; -ms-border-radius: 4px; -o-border-radius: 4px;';
        html_text +=  '-moz-border-radius: 4px; -webkit-border-radius: 4px;">';
        html_text +=  '<img src="images/check.png" />' + Mapurl + '</div></td></tr></table>';

        var markers = new google.maps.Marker({ position: map_center, map: map, title: strmap[2], html: html_text });
        markers.setIcon("Images/marker2.png");
        google.maps.event.addListener(markers, 'mouseover', function() {
            infowindows1.setContent(this.html);
            infowindows1.open(map, this);
        });
    }
    else
        document.getElementById('selected_Hotel').innerText = "Address not found";
    infowindows1 = new google.maps.InfoWindow({ content: 'Loading...' });
}
//Load Repeater Map End
var HotelMapList;var map;
//All Hotel Map start
var InfoWindowArr = new Array();
var infowindows1 = "";
function loadAllHotelMapnew(response) {
   
    if (response.length > 0) {
        var strmap;
        if (response[0].Lati_Longi.length > 6)
            strmap = response[0].Lati_Longi.split("##");
        else
            strmap = response[1].Lati_Longi.split("##");
        
        var map_center = new google.maps.LatLng(strmap[0], strmap[1]);
        var myOptions = {
            zoom: 13, center: map_center, panControl: true, zoomControl: true, mapTypeControl: true, scaleControl: false,
            streetViewControl: true, overviewMapControl: true, mapTypeId: google.maps.MapTypeId.ROADMAP
        }
         map = new google.maps.Map(document.getElementById('map_canvas2'), myOptions);
        for (var i = 0; i < response.length; i++) {
            try {
                if (response[i].HtlError == null) {
                    if (response[i].Lati_Longi.length > 6) {
                        var html_tex = '<table width="280" cellpadding="0" cellspacing="0" border="0" style="color:#000;">';
                        html_tex += '<tr><td colspan="3"><strong>' + response[i].HotelName + '</strong><br />' + response[i].HotelAddress + '</td></tr>';
                        html_tex += '<tr><td valign="top"><img src=' + response[i].HotelThumbnailImg + ' style="width:85px;hight:85px;" /></td>';
                        html_tex += '<td valign="top">' + setStarratings(response[i].StarRating) + ' <br />';
                        html_tex += '<img src="Images/htlrs.png" /> <strong>' + response[i].hotelPrice + '</strong> <br />Per Room Per Night</td>';
                        html_tex += '<td valign="top"><div style="font-weight: bold; color: #fff; padding: 0 8px; background: url(images/btnbgk.jpg);';
                        html_tex += 'height: 25px; line-height: 25px; -ms-border-radius: 4px; -o-border-radius: 4px;-moz-border-radius: 4px; -webkit-border-radius: 4px;">';
                        html_tex += '<img src="images/check.png" /><a href="#" class="showRoomdetailss" id="' + response[i].HotelCode + "_" + response[i].HotelCityCode + '" title="' + response[i].HotelName + '" style="color:White; font-weight:bold;">Select</a>';
                        html_tex += '</div></td></tr></table>';

                        var strLatlong = response[i].Lati_Longi.split("##");
                        var points = new google.maps.LatLng(strLatlong[0], strLatlong[1]);
                        var markers = new google.maps.Marker({ position: points, map: map, title: response[i].HotelName, html: html_tex });

                        infowindows1 = new google.maps.InfoWindow({ content: html_tex, position: points });
                       
                        var image = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=" + (i + 1) + "|f1f1f1|000000", null, null, null, new google.maps.Size(40, 31));
                        markers.setIcon(image);
                        google.maps.event.addListener(markers, 'mouseover', function() {

                           InfoWindowArr.push(infowindows1);
                           InfoWindowArr[0].setContent(this.html);
                            InfoWindowArr[0].open(map, this);

                        });

                        google.maps.event.addListener(markers, 'mouseout', function() {
                          
                            InfoWindowArr[0].setContent(this.html);
                            InfoWindowArr[0].close(map, this);
                            InfoWindowArr.pop(infowindows1);

                        });
                        
                        
                    }
                }
            } catch (ex)
            { alert( response[i].Lati_Longi + " " + response[i].Provider + " " + response[i].HotelName); }
        }
        infowindows1 = new google.maps.InfoWindow({ content: 'Loading...' });
    }
    HotelMapList = response; ;
    showIndex();
}
//All Hotel Map End

function showIndex() {
    var pageCounter1 = ""; ;
    var rmrArr = new Array();
    try {
        var odd = (HotelMapList.length % 7) % 2;
        var noIndex = parseInt(HotelMapList.length / 7) + odd;
            for (var s = 0; s < noIndex; s++) {
                pageCounter1 += "<a class='padding2s cursorpointer' onclick='paging(" + (s * 7) + "," + ((s * 7) + 7) + ")'>" + s + " " + "</a>";
                if (s == 7 || s == 13)
                    pageCounter1 += "<div class='clear'><hr /></div>";
                else if ((s-2) % 6 == 0 && s > 14)
                    pageCounter1 += "<div class='clear'><hr /></div>";
            }
       
        $("#next").empty();
        $("#next").html(pageCounter1); 
        paging(0, 7);
    }
    catch (ex) { }
}

function paging(pageno, pagesize) {
    $('.Hotelmapdetails div').empty(''); repeater = true;
    $(".Hotelmapdetails").html(MapHotelDetailNew(pageno, pagesize));
}

//onmouseover='loadAllHotelMapnew2(" + v + ");'
//All Hotel MaDetails Start repeater=setInterval(
function MapHotelDetailNew(StartPageNo, ToPageNo) {
    var HotelDiv = "<div>";
     try {
            HotelDiv += "<div class='clear1'></div>";
            for (var v = StartPageNo; v < ToPageNo; v++) {
                HotelDiv += "<div class='clear1'></div>";
                HotelDiv += "<div class='brdr bgw' style='padding: 5px;'><a href='javascript:void(0);' class='showRoomdetailss adad' id='" + HotelMapList[v].HotelCode + "_" + HotelMapList[v].HotelCityCode + "' title='" + HotelMapList[v].HotelName + "'>";
                HotelDiv += "<div style='position:absolute; margin-top:35px; margin-left:-20px; border-radius:3px; background:#333; color:#fff; padding:2px 5px;'>" + (v + 1) + "</div>";
                HotelDiv += "<div class='w100'>";
                HotelDiv += "<div class='lft bld headercolor'>" + HotelMapList[v].HotelName + "</div>";
                HotelDiv += "<div class='clear'></div>";
                HotelDiv += "<div class='lft'>" + HotelMapList[v].Location + ", " + HotelMapList[v].HotelCity + "</div>";
                HotelDiv += "<div class='clear'></div>";

                HotelDiv += "<div class='lft w33'><img src='" + HotelMapList[v].HotelThumbnailImg + "' class='hights40 w98'/></div>";
                HotelDiv += "<div class='w66 rgt'><div class='clear'></div>";
                HotelDiv += "<div class='lft'>" + setStarratings(HotelMapList[v].StarRating) + "</div>";
                HotelDiv += "<div class='clear'></div>";
                HotelDiv += "<div class='lft'><img src='Images/htlrs.png' class='lft' />&nbsp;&nbsp;" + HotelMapList[v].hotelPrice + "</div>";
                HotelDiv += "</div>";
                HotelDiv += "</div><div class='clear'></div>";
                HotelDiv += "</a></div>";
            }
        }
        catch (ex) {}
    HotelDiv += "</div>"; 
    return HotelDiv;
}
//All Hotel Map Deatails End



var repeater = true;

//var infowindows2;
$(document).on("mouseenter", ".adad", function(e) {
    if (repeater) {
        var HtlNo = parseInt(this.childNodes[0].innerText) - 1;
        var response = HotelMapList;
        var html_text1 = '<table width="280" cellpadding="0" cellspacing="0" border="0" style="color:#000;">';
        html_text1 += '<tr><td colspan="3"><strong>' + response[HtlNo].HotelName + '</strong><br />' + response[HtlNo].HotelAddress + '</td></tr>';
        html_text1 += '<tr><td valign="top"><img src=' + response[HtlNo].HotelThumbnailImg + ' style="width:85px;hight:85px;" /></td>';
        html_text1 += '<td valign="top">' + setStarratings(response[HtlNo].StarRating) + ' <br />';
        html_text1 += '<img src="Images/htlrs.png" /> <strong>' + response[HtlNo].hotelPrice + '</strong> <br />Per Room Per Night</td>';
        html_text1 += '<td valign="top"><div style="font-weight: bold; color: #fff; padding: 0 8px; background: url(images/btnbgk.jpg);';
        html_text1 += 'height: 25px; line-height: 25px; -ms-border-radius: 4px; -o-border-radius: 4px;-moz-border-radius: 4px; -webkit-border-radius: 4px;">';
        html_text1 += '<img src="images/check.png" /><a href="#" class="showRoomdetailss" id="' + response[HtlNo].HotelCode + "_" + response[HtlNo].HotelCityCode + '" title="' + response[HtlNo].HotelName + '" style="color:White; font-weight:bold;">Select</a>';
        html_text1 += '</div></td></tr></table>';

        var strLatlong = response[HtlNo].Lati_Longi.split("##");
        var points = new google.maps.LatLng(strLatlong[0], strLatlong[1]);

        infowindows1 = new google.maps.InfoWindow({ content: html_text1, position: points });
        
        InfoWindowArr.push(infowindows1);

        InfoWindowArr[0].open(map);

        repeater = false;
    }
});
$(document).on("mouseleave", ".adad", function(e) {
    repeater = true;
    var HtlNo = parseInt(this.childNodes[0].innerText) - 1;
    InfoWindowArr[0].close(map);
    InfoWindowArr.pop(infowindows1);
    
});

function setStarratings(stars) {
    var starImg = "";
    var starratings = stars.split('.')
    for (var m = 1; m <= parseInt(starratings[0]); m++) {
        starImg += "<img src='Images/star.png' alt='Hotel Rating'/>";
        }
        if (starratings.length > 1) {
            if (starratings[1] == "5")
                starImg += "<img src='Images/star_cut.png' alt='Hotel Rating'/>";
        }
    return starImg;
}