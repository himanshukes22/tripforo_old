var cX = 0; var cY = 0; var rX = 0; var rY = 0;
function UpdateCursorPosition(e) { cX = e.pageX; cY = e.pageY; }
function UpdateCursorPositionDocAll(e) { cX = event.clientX; cY = event.clientY; }
if (document.all) { document.onmousemove = UpdateCursorPositionDocAll; }
else { document.onmousemove = UpdateCursorPosition; }
function AssignPosition(d) {
    if (self.pageYOffset) {
        rX = self.pageXOffset;
        rY = self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop) {
        rX = document.documentElement.scrollLeft;
        rY = document.documentElement.scrollTop;
    }
    else if (document.body) {
        rX = document.body.scrollLeft;
        rY = document.body.scrollTop;
    }
    if (document.all) {
        cX += rX;
        cY += rY;
    }
    var nAgt = navigator.appName;
    if (nAgt == "Microsoft Internet Explorer") {
        getCursorPosition();
        d.style.left = xVal + "px";
        d.style.top = yVal + "px";
    }
    else {
        d.style.left = (cX - 150) + "px";
        d.style.top = (cY + 10) + "px";
    }
}

var xVal, yVal;

function getCursorPosition(e) {
    e = e || window.event;
    var cursor = { x: 0, y: 0 };
    if (e.pageX || e.pageY) {
        cursor.x = e.pageX;
        cursor.y = e.pageY;
    }
    else {
        cursor.x = e.clientX +
                (document.documentElement.scrollLeft ||
                document.body.scrollLeft) -
                document.documentElement.clientLeft;
        cursor.y = e.clientY +
                (document.documentElement.scrollTop ||
                document.body.scrollTop) -
                document.documentElement.clientTop;
    }
    return cursor;
}

document.onmouseup = function(e) {
    cursor = getCursorPosition();
    xVal = cursor.x;
    yVal = cursor.y;

};


function HideContent(d) {
    if (d.length < 1) { return; }
    document.getElementById(d).style.display = "none";
}
function ShowContent(d, msg) {// 
    if (d.length < 1) { return; }
    var dd = document.getElementById(d);
    AssignPosition(dd);
    //alert(msg); 
    dd.innerHTML = msg;
    //alert(dd.innerTex);t
    dd.style.display = "block";
    dd.style.display = "";
}
function ReverseContentDisplay(d) {
    if (d.length < 1) { return; }
    var dd = document.getElementById(d);
    AssignPosition(dd);
    if (dd.style.display == "none") { dd.style.display = "block"; }
    else { dd.style.display = "none"; }
}




//*********************

function railurl(type) {
//    alert('Rail service is being updated.');
//   return true;
    var xmlHttpReq = createXMLHttpRequest();
    xmlHttpReq.open("GET", "setvalue.ashx?type=RAIL", false);
    xmlHttpReq.send(null);
  // alert(xmlHttpReq.responseText);
    window.location.href = xmlHttpReq.responseText;
}
function IntFareDetails(ln) {
    //    document.getElementById("divfareDetails").style.display = 'block';
    //    document.getElementById("divfareDetails").innerHTML = 'Loading...';
    //
    var a = "<div><object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' align='middle'><param name='allowScriptAccess' value='sameDomain' /><param name='movie' value='../images/loadingnew.swf' /><embed src='../images/loadingnew.swf' quality='high' align='middle' allowscriptaccess='sameDomain' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /></object></div>";//"<div><img src='../images/load.gif' /></div>";
    
    
    ShowContent('divfareDetails', a);
    var xmlHttpReq = createXMLHttpRequest();
    xmlHttpReq.open("GET", "IntFareDtls.ashx?ln=" + ln + "", false);
    xmlHttpReq.send(null); // 
    var yourJSString = xmlHttpReq.responseText;
    ShowContent('divfareDetails', yourJSString);
}
function DomFareDetails(ln,ft) {
    //    document.getElementById("divfareDetails").style.display = 'block';
    //    document.getElementById("divfareDetails").innerHTML = 'Loading...';
    //
    var a = "<div><object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' align='middle'><param name='allowScriptAccess' value='sameDomain' /><param name='movie' value='../images/loadingnew.swf' /><embed src='../images/loadingnew.swf' quality='high' align='middle' allowscriptaccess='sameDomain' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /></object></div>"; //"<div><img src='../images/load.gif' /></div>";


    ShowContent('divfareDetails', a);
    var xmlHttpReq = createXMLHttpRequest();
    xmlHttpReq.open("GET", "DomFareDtls.ashx?ln=" + ln + "&ft=" + ft + "", false);
    xmlHttpReq.send(null); // 
    var yourJSString = xmlHttpReq.responseText;
    ShowContent('divfareDetails', yourJSString);
}
function createXMLHttpRequest() {
    try { return new XMLHttpRequest(); } catch (e) { }
    try { return new ActiveXObject("Msxml2.XMLHTTP"); } catch (e) { }
    try { return new ActiveXObject("Microsoft.XMLHTTP"); } catch (e) { }
    alert("XMLHttpRequest not supported");
    return null;

}




