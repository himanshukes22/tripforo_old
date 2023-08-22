<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fare_Cal.ascx.cs" Inherits="UserControl_Fare_Cal" %>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/json2.js") %>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/JSLINQ.js")%>"></script>

<link href="Advance_CSS/farecal//css/FareCal.css?v=1.1" rel="stylesheet" />
<link href="Advance_CSS/farecal//css/FareBlue.css" rel="stylesheet" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
<script src="Advance_CSS/farecal//js/jquery.noconflict.js"></script>
<script src="Advance_CSS/farecal//js/evo-calendar.min.js?v=2"></script>
<script src="Advance_CSS/farecal//js/calc.js?v=2.1"></script>



<div class="offer-banner trip-section fluid-full-width moduleArea" id="cal-controler" style="display:none;">
    <div class="Trip-book why_tripforo moduleArea" style="padding: 0px !important">

        <div id="demoEvoCalendar"></div>
        <input type="hidden" id="isdfsf" runat="server" />


        <div id="waitMessageF" style="display: none;">
            <div style="text-align: center; z-index: 101111111111111; font-size: 12px; font-weight: bold; padding: 20px;">

                <div class="backdrop">

                    <div id="searchquery" style="color: #000; font-size: 18px; text-align: center;">
                    </div>

                    <div>
                        <%--<img class="spinner" src="../Advance_CSS/Icons/morphing-animation.gif" style="width: 100% ;height:300px;" />--%>
                        <p class="text-center" style="font-size: 25px; margin-top: 60px; color: #5ea51d;">We are fetching flight details based on the information you provided. <i class="fa fa-spinner fa-pulse"></i></p>
                    </div>
                    <span id="loading-msg"></span>
                </div>





            </div>
        </div>


        <div id="ConfmingFlight" class="CfltFare" style="display: none;">
            <div id="divLoadcf" class="">
            </div>
        </div>
    </div>
</div>

