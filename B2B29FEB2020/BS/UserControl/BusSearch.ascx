<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusSearch.ascx.cs" Inherits="BS_UserControl_BusSearch" %>


<style>
    .flt_icon {
        background-image: url(../Images/icons/location-alt-512.png) !important;
        width: 20px;
    }
</style>

<link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
    rel="stylesheet" />
<style>
    body {
        font-family: Arial, Helvetica, sans-serif;
    }

    * {
        box-sizing: border-box;
    }

    .input-container {
        display: -ms-flexbox; /* IE10 */
        display: flex;
        width: 100%;
        margin-bottom: 15px;
        height: 37px;
    }

    .icon {
        padding: 13px;
        background: #f1f1f1;
        color: #272727;
        min-width: 40px;
        text-align: center;
        border-bottom: 0.5px solid #a7a7a7;
        border-top: 0.5px solid #a7a7a7;
        border-left: 0.5px solid #a7a7a7;
    }

    .input-field {
        width: 100%;
        padding: 10px;
        outline: none;
    }

        .input-field:focus {
            border: 0.5px solid dodgerblue;
            box-shadow: 0px 0px 30px rgba(0, 99, 222, 0.8);
        }
        
        
        .ui-datepicker-multi-2 .ui-datepicker-group {
    
    border-right: 2px dotted #ccc !important;
}

div#ui-datepicker-div.ui-datepicker 
{
    width:39%;
}
</style>

<br />

 <div class="theme-search-area theme-search-area-stacked">
<div class="theme-search-area-form">
 <div class="row" data-gutter="none">


    <div class="col-md-6 ">
        <div class="row" data-gutter="none">
            <div class="col-md-6 ">
                <div class="theme-search-area-section first theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">
                    <div class="theme-search-area-section-inner">
                        <i class="theme-search-area-section-icon lin lin-location-pin"></i>
                        <input type="text" id="txtsrc" class="theme-search-area-section-input typeahead" placeholder="Enter Your Departure City" name="txtsrc" />
                        <input type="hidden" id="txthidsrc" name="txthidsrc" />
                    </div>
                </div>
            </div>


            <div class="col-md-6">
                <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">
                    <div class="theme-search-area-section-inner">
                        <i class="theme-search-area-section-icon lin lin-location-pin"></i>
                        <input type="text" id="txtdest" name="txtdest" class="theme-search-area-section-input typeahead" placeholder="Enter Your Destination City" />
                        <input type="hidden" id="txthiddest" name="txthiddest" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-6 ">
     <div class="row" data-gutter="none">

    <div class="col-md-6 ">
        <div class="theme-search-area-section theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">
            <div class="theme-search-area-section-inner">
                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                <input type="text" class="theme-search-area-section-input datePickerStart _mob-h" placeholder="dd/mm/yyyy" name="hiddepart" id="hiddepart" value="" readonly="readonly" />
            </div>
        </div>
    </div>


      <div class="col-md-6">
        
        <input type="button" id="btnsearch" name="btnsearch" value="Search Buses" class="theme-search-area-submit _mt-0 theme-search-area-submit-no-border theme-search-area-submit-curved " style="margin-top: 25px;" />
    </div>
         </div>
        </div>


  <%--  <div class="col-md-6">

        <label>Depart Date</label>
        <div class="input-container">
            <i class="fa fa-calendar icon" aria-hidden="true"></i>
            <span id="currDate"></span>
            <input type="text" class="input-field" placeholder="dd/mm/yyyy" name="hiddepart" id="hiddepart11" value=""
                readonly="readonly" />
            <div id="divSrcDest" class="div">
            </div>
        </div>

    </div>--%>
  

</div>
    </div>
</div>

<script src="<%= ResolveUrl("~/BS/JS/jquery-1.9.1.js")%>" type="text/javascript"></script>

<script src="<%= ResolveUrl("~/BS/JS/jquery-1.4.4.min.js")%>" type="text/javascript"></script>

<script src="<%= ResolveUrl("~/BS/JS/jquery-ui-1.8.8.custom.min.js")%>" type="text/javascript"></script>

<script src="<%= ResolveUrl("~/BS/JS/BusSearch.js")%>" type="text/javascript"></script>

<%--<script type="text/javascript">
    var UrlBase = '<%=ResolveUrl("~/") %>';
    var myDate = new Date();
    var selectDate = window.location.search.substring(1);
    var currDate = (myDate.getMonth() + 1) + '/' + (myDate.getDate()) + '/' + myDate.getFullYear();
    if (selectDate == "") {
        var d = new Date(currDate);
        var dayName = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");
        var month = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
        document.getElementById("currDate").value = currDate;
        $("#month").html(month[d.getMonth()].toUpperCase());
        $("#day").html(dayName[d.getDay()]);
        $("#date").html(d.getDate());
        $("#year").html(d.getFullYear());
    }
    else {
        var collection = {}; var k = 0;
        var qarray = selectDate.split('&');
        for (var i = 0; i <= qarray.length - 1; i++) {
            var splt = qarray[i].split('=');
            if (splt.length > 0) {
                for (var j = 0; j < splt.length - 1; j++) {
                    collection[k] = splt[j + 1];
                }
                k += 1;
            }
        }
        var ddd = collection[4].split('-');
        var d = new Date(ddd[1] + "/" + ddd[2] + "/" + ddd[0]);
        var dayName = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");
        var month = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
        document.getElementById("currDate").value = currDate;
        $("#month").html(month[d.getMonth()].toUpperCase());
        $("#day").html(dayName[d.getDay()]);
        $("#date").html($.trim(ddd[2]));
        $("#year").html(d.getFullYear());
    }



</script>

<script type="text/javascript">
    var myDate = new Date();
    var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
    document.getElementById("hiddepart").value = currDate;
    var UrlBase = '<%=ResolveUrl("~/") %>';
</script>--%>

