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
function ShowContent(d, msg) {
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

var chkselected;
function checkselectedflight(id,val,typ)
{
 var elem=document.getElementById(id).getElementsByTagName("input");
 for(var i = 0; i < elem.length; i++)
  {
   if((elem[i].type=="radio") && (elem[i].checked==true) && (elem[i].value==val))
    {chkselected=true;
     if(typ=='O')
     {document.getElementById('ctl00_ContentPlaceHolder1_selecteddep').value=val;}//setTimeout('fetchData()', 500);
     else
     {document.getElementById('ctl00_ContentPlaceHolder1_selectedret').value=val;}//setTimeout('fetchData()', 500);
    }
    else
    {
     if((elem[i].type=="radio"))
     {
      elem[i].checked=false;
     }
     else
     { 
      if((elem[i].type=="hidden")&& (chkselected==true))
      {chkselected=false;
        if(typ=='O')
         {document.getElementById('ctl00_ContentPlaceHolder1_fltlblO').innerHTML=elem[i].value;Lccfare();}
         else
         {document.getElementById('ctl00_ContentPlaceHolder1_fltlblR').innerHTML=elem[i].value;Lccfare();}
      }
      else
      {}
     }
    }
  }
}
function lccFRRule()
{
var fr="<table border='0' cellspacing='2' cellpadding='2' width='450px'><tr><td width='15%'><p>&nbsp;<strong>Cancellation Policy</strong></p></td><td width='85%'><p>Charges of Rs 750 if cancelled 4 hours prior to flight departure</p></td></tr><tr><td width='15%'><p>&nbsp;<strong>Reissuance Policy</strong></p></td><td width='85%'><p>Charges of Rs950 + fare difference when done 4 hours prior to fligth departure</p></td></tr><tr><td width='15%'><p>&nbsp;<strong>Service Fee</strong></p></td><td width='85%'><p>This service fee is in addition to the stipulated airline rescheduling / cancellationcharges. For Travel agents and Franchise, Resheduling /Cancellation Service feeis Rs 0 for domestic tickets For direct customers , Resheduling /Cancellation Servicefee is Rs 250 for domestic tickets Rescheduling and / or cancellation service feeis Rs 300 for international tickets</p></td></tr><tr><td width='15%'><p>&nbsp;<strong>Note:</strong></p></td><td width='85%'><p>Cancellation/Reshedule not allowed after onward journey flown</p></td></tr></table>";
ShowContent('uniquename3',fr); return true;
}
function lccRTFFare(tds)
{ 
var lin;
lin=document.getElementById('ctl00_ContentPlaceHolder1_selecteddep').value+'-'+document.getElementById('ctl00_ContentPlaceHolder1_selectedret').value
 return FareDtlsLccRTF('TF',lin,tds);
}
  function Lccfare()
 {
 var rt=LccRF_LccRResult.LccTotalFare(document.getElementById('ctl00_ContentPlaceHolder1_selecteddep').value,document.getElementById('ctl00_ContentPlaceHolder1_selectedret').value)
 if (rt.value==0)
 { document.getElementById('ctl00_ContentPlaceHolder1_totFarelbl').innerHTML="";}
 else
 {document.getElementById('ctl00_ContentPlaceHolder1_totFarelbl').innerHTML="Rs. " + rt.value + "/-";}
 document.getElementById('book').href='lccCheckOut.aspx?LinD=' + document.getElementById("ctl00_ContentPlaceHolder1_selecteddep").value + '&LinR=' + document.getElementById("ctl00_ContentPlaceHolder1_selectedret").value + '';
 }
 
 function LccIsBookable()
 {
  var rt=LccRF_LccRResult.IsBookable(document.getElementById('ctl00_ContentPlaceHolder1_selecteddep').value,document.getElementById('ctl00_ContentPlaceHolder1_selectedret').value)
  
  if (rt.value!=0)
   { document.getElementById('isBookableMsg').innerHTML="";return true;}
  else
   { document.getElementById('isBookableMsg').innerHTML="You Can not book round trip with different airline.<br/>Please choose same airline";return false;}
 }
 
 function FareDtlsLccRTF(triptype,lin,ts)
{
    
    var xmlHttpReq= createXMLHttpRequest(); 
    xmlHttpReq.open("GET", "FareDetails.ashx?triptype="+triptype+"&lin="+lin+"&ts="+ts+"", false); 
    xmlHttpReq.send(null); 
    var yourJSString = xmlHttpReq.responseText;
    ShowContent('uniquename3',yourJSString);
        
}
function createXMLHttpRequest() 
{ 
    try { return new XMLHttpRequest(); } catch(e) {} 
    try { return new ActiveXObject("Msxml2.XMLHTTP"); } catch (e) {} 
    try { return new ActiveXObject("Microsoft.XMLHTTP"); } catch (e) {} 
    alert("XMLHttpRequest not supported"); 
    return null; 

} 

 //***********************
 
 
 //***********validation**************
 
function ValidateChecked(oSrc, args)
  {
    if(document.getElementById("<%=TC.ClientID%>").checked == false)
    {
      document.getElementById("can").innerHTML="*Check Rules";
      args.IsValid = false;
    }
    else
     {
     document.getElementById("TC").Enabled = true; 
     document.getElementById("can").innerHTML=" ";
     }
  }
   function load()
   {
   document.getElementById('book').disabled =(document.getElementById('TC').checked == false);
   }
    function vali()
   {
   if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) ||(event.keyCode==32) ||(event.keyCode==45) ) 
          event.returnValue = true;
    else 
          event.returnValue = false;
   }
  
   function vali1()
   {
   if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) ||(event.keyCode==32) || (event.keyCode > 47 && event.keyCode < 58) ||(event.keyCode==32)  ) 
          event.returnValue = true;
    else 
          event.returnValue = false;
   }
    function phone_vali()
   {
   if ((event.keyCode > 47 && event.keyCode < 58))
          event.returnValue = true;
    else 
          event.returnValue = false;
   }
   
 