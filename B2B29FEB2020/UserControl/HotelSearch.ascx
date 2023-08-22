<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HotelSearch.ascx.vb"
    Inherits="UserControl_HotelSearch" %>
<link href="<%=ResolveUrl("~/Hotel/css/B2Bhotelengine.css") %>" rel="stylesheet"
    type="text/css" />
<link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css?v=0.4")%>" rel="stylesheet"
    type="text/css" />

<script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/Hotel/JS/HtlSearchQuery.js?v=0.5")%>" type="text/javascript"></script>
<script type="text/javascript">
    
    $(document).ready(function () {

        $(function () {
            var HtlDatePickerOption = { numberOfMonths: 2, dateFormat: "dd/mm/dd", maxDate: "+1y", minDate: "0", showOtherMonths: true, selectOtherMonths: false };
            $("#htlcheckin").datepicker(HtlDatePickerOption).datepicker("setDate", new Date());
            $("#htlcheckout").datepicker(HtlDatePickerOption).datepicker("setDate", new Date().getDate + 1);
        });
        //    var HtlDatePickerOption = { numberOfMonths: 2, dateFormat: "dd/mm/dd", maxDate: "+1y", minDate: "0", showOtherMonths: true, selectOtherMonths: false };
        //    $("#htlcheckin").datepicker(HtlDatePickerOption).datepicker("option", {

        //        onSelect: function UpdateRoundTripMininumDate(e, t) {

        //            var dd = e.split('/');
        //            var day = parseInt(dd[0]) + 1;
        //            var newday = day.toString();
        //            if (day < 10) { newday = "0"+newday}

        //            $("#htlcheckout").datepicker("option", {
        //                minDate: newday + '/' + dd[1] + '/' + dd[2]
        //            })
        //        }

        //    }).datepicker("setDate", new Date());
        //    $("#htlcheckout").datepicker(HtlDatePickerOption).datepicker("setDate", $("#htlcheckin") + 1);

    });






</script>



<%-------------------------------------------------------------------------------%>

<h1 style="font-size: 24px; font-weight: 400;">Search Cheapest Hotel</h1>


<div class="row">
    <div class="col-md-6">
       
            <label>Select City:</label>
           <div class="input-container">
                        <i class="icon fa fa-building" aria-hidden="true"></i>
                <input type="text" id="htlCity" class="input-field" name="htlCity" value="" placeholder="Enter a city or area name"
                    data-trauncate="false" title="Where do you want to go" />
                <input type="hidden" id="htlcitylist" name="htlcitylist" value="" />
                <input type="hidden" id="contrycode" name="contrycode" value="IN" />
            </div>
        
    </div>
    
        <div class="col-md-6">
          
                <label>Check In Date</label>
                
                   <div class="input-container">
                        <i class="icon fa fa-calendar" aria-hidden="true"></i>
                    <input type="text" id="htlcheckin" name="htlcheckin" placeholder="dd/mm/yyyy" value="" class="input-field"
                        readonly="readonly" />
                    <input type="hidden" name="hidhtlcheckin" id="hidhtlcheckin" value="" />
                </div>
           
      
            </div>
        <div class=" col-md-6" id="trRetDateRow">
            
                <label>Check Out Date:</label>
                <div class="input-container">
                    
                        <i class="icon fa fa-calendar"></i>
                    
                    <input type="text" id="htlcheckout" name="htlcheckout" placeholder="dd/mm/yyyy" value="" class="input-field"
                        readonly="readonly" />
                    <input type="hidden" name="hidhtlcheckout" id="hidhtlcheckout" value="" />
                </div>
          
        </div>
    



    
        <div class="col-md-6" style="cursor: pointer;" id="hTraveller">
            <label>
                <span id="nights">1 Night</span></label>
           

                <div class="input-container" style="margin-top: -5px;">
                    <i class="fa fa-user icon" aria-hidden="true"></i>
              
                <input type="text" class="input-field" id="HsapnTotPax" placeholder="1 Room(s) -1 Guests" value="1 Room(s) -1 Guests" >
            </div>
        </div>
        <div class="col-md-12">
            <label style="color:white">.</label>
            <button type="button" id="btnHotel" name="btnSearch" value="Search" class="btn btn-danger" style="float:right">
                Search Hotels</button>
        </div>
   

</div>




<div class="row">
</div>

<script>
    $(document).ready(function () {
        $("#hTraveller").click(function () {
            $("#hbox").toggle();
        });
        $("#hserachbtn").click(function () {
            $("#hbox").toggle();
        });

    });
</script>
<div class="row" id="hbox" style="display: none;">

    <div class="col-md-12 col-xs-12 nopad">

        <div id="hot-search-params">
        </div>
        <input type="hidden" name="rooms" id="rooms" />
        <input type="hidden" name="chds" id="chds" />
        <div class="clear"></div>

        <div class="large-4 medium-4 small-12 rgt">
            <input type="hidden" name="ReqType" id="ReqType" value="S" />
        </div>
    </div>
    <i class="sprite-booking-engine ico-be-sub-arrow"></i>
    <div class="col-md-12 col-xs-8 nopad">
        <button type="button" onclick="Hplus()" class="btn btn-success btn-lg" id="hserachbtn" style="margin-top: 5px;">
            Done</button>
    </div>
    <div class="clear"></div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="col-md-7" onclick="more()" id="buttonAddOptss">Advanced options : Hotel, Star Rating <i class="fa fa-chevron-right" ></i></div>
        

    </div>

    <div class="col-md-12" id="effectss"  style="display: none; width: 93%; float: left; position: relative; border: 1px solid #e4e5e5; padding: 14px; margin-top: 15px; margin-bottom: 10px; left: 24px;">
    <div class="row" >

            <div class="col-md-4">
                <label>Hotel Name</label>
                <input class="form-control" type="text" id="htlname" name="htlname" value="" placeholder="Enter a hotel name"
                    data-trauncate="false" title="Where do you want to stay" />
                <input type="hidden" id="Hotelcode" name="Hotelcode" value="" />
            </div>
           
            <div class="col-md-4">
                <label>Star Rating</label>
                <select class="form-control" id="htlstar" name="htlstar" title="Hotel Class">
                    <option value="0">Select Star Rating</option>
                    <option value="1">1 Star</option>
                    <option value="2">2 Stars</option>
                    <option value="3">3 Stars</option>
                    <option value="4">4 Stars</option>
                    <option value="5">5 Stars</option>
                </select>
            </div>
        </div>
        </div>
</div>

<%---------------------------------------------------------------------------------------%>

<script type="text/javascript">

    //$("#buttonAddOptss").click(function () {
    //    $("#effectss").slideToggle();
    //});

    function Hplus() {
        debugger;
        var strmsd = document.getElementById("rooms").value;
        var strmsa = document.getElementsByClassName("adt")
        var adtcnt = 0;
        for (var i = 0; i < strmsa.length; i++) {
            adtcnt = adtcnt + parseInt(strmsa[i].options[strmsa[i].selectedIndex].value);
        }


        var strmac = document.getElementsByClassName("chd");

        var chdcnt = 0;
        for (var j = 0; j < strmac.length; j++) {
            chdcnt = chdcnt + parseInt(strmac[j].options[strmac[j].selectedIndex].value);
        }

        var abss = chdcnt + adtcnt;//+ strmas;
        document.getElementById("HsapnTotPax").value = strmsd + ' Room(s) -' + abss + ' Guests';
    }


</script>

  <script>
      function more() {
          var x = document.getElementById("effectss");
          if (x.style.display === "none") {
              x.style.display = "block";
          } else {
              x.style.display = "none";
          }
      }
        </script>

<script src="Hotel/JS/hotelpasg.js" type="text/javascript"></script>
