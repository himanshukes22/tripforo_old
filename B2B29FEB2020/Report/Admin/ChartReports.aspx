<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true"
    CodeFile="ChartReports.aspx.cs" Inherits="SprReports_ChartReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet"
        type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .txtBox {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 1px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }

        .txtCalander {
            width: 100px;
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <table style="width: 100%; margin: 0 auto;" align="center">
        <tr>
            <td>
                <table cellspacing="10" cellpadding="10" align="center" class="tbltbl" style="background: #fff; width: 100%;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="10" cellspacing="10" width="100%">
                                <tr>
                                    <td style="font-weight: bold;">
                                        <label for="FromDate" id="fromDate">
                                            From Date:</label>
                                    </td>
                                    <td>
                                        <input type="text" name="txtFromDate" id="txtFromDate" class="txtCalander" readonly="readonly"
                                            style="width: 95px; height: 18px;" />
                                    </td>
                                    <td style="width: 45px; font-weight: bold;">
                                        <label for="ToDate" id="ToDate">
                                            To Date:</label>
                                    </td>
                                    <td>
                                        <input type="text" name="txtToDate" id="txtToDate" class="txtCalander" readonly="readonly"
                                            style="width: 95px; height: 18px;" />
                                    </td>
                                    <td style="font-weight: bold;">Service Type:
                                    </td>
                                    <td>
                                        <select class="CheckoutDDL" style="width: 110px;" id="drpServiceType" name="drpServiceType">
                                            <option value="0">Select Servicert Type</option>
                                            <option value="All">ALL Service</option>
                                            <option value="Flight">Flight</option>
                                            <option value="Hotel">Hotel </option>
                                            <option value="Rail">Rail</option>
                                            <option value="Bus">Bus </option>
                                            <option value="Utilty">Utilty</option>
                                        </select>
                                    </td>
                                    <td style="font-weight: bold;">Report Type:
                                    </td>
                                    <td>
                                        <select class="CheckoutDDL" style="width: 110px;" id="drpService" name="drpService">
                                        </select>
                                    </td>
                                    <td style="font-weight: bold; display: none;" id="tdairline">Airline :
                                    </td>
                                    <td style="display: none;" id="tdairlineval">
                                        <input type="text" name="txtAirline" value="" id="txtAirline" style="width: 92px;"
                                            class="txtBox" />
                                        <input type="hidden" id="hidtxtAirline" name="hidtxtAirline" value="" />
                                    </td>
                                    <td style="font-weight: bold; display: none;" id="tdagnid">Agency Name
                                    </td>
                                    <td style="display: none;" id="tdagnval">
                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 92px" onfocus="focusObj(this);"
                                            onblur="blurObj(this);" value="Agency Name or ID" />
                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                    </td>
                                    <td style="font-weight: bold; display: none" id="tdtoptype">Top Type:
                                    </td>
                                    <td style="display: none" id="tddrptopval">
                                        <select class="CheckoutDDL" style="width: 92px;" id="drpTopType" name="drpTopType">
                                            <option value="10">Top 10 </option>
                                            <option value="20">Top 20 </option>
                                            <option value="50">Top 50 </option>
                                            <option value="100">Top 100 </option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="10" width="100%">
                                <tr>
                                    <td align="right">
                                        <img src="../images/loadingAnim.gif" alt="Loading.." id="wait" style="display: none;" />
                                    </td>
                                    <td align="right">
                                        <button class="button" type="button" id="Btnreport" onclick="GetGraph();">
                                            Get Chart
                                        </button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--  <tr id="wait" style="visibility: hidden;">
                        <td align="center">                          
                            <img src="../images/loadingAnim.gif" alt="Loading.." />

                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto; display: none;" id="tblChart">
        <tr>
            <td align="center">
                <div id="visualization" style="width: 1300px; height: 400px;">
                </div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <div id="divColumnChart" style="width: 1300px; height: 400px;">
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

    <script type="text/javascript" src="//www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>

    <script type="text/javascript">

        $(document).ready(function () {


            $("#drpServiceType").change(function () {

                var sertype = "";


                if ($("#drpServiceType option:selected").val() != 0) {

                    sertype = $.trim($("#drpServiceType option:selected").val());
                }
                else {
                    alert("Select Service Type.");
                    $("#drpServiceType").focus();
                    return false;
                }

                if (sertype == "All") {

                    $('#drpService').html("<option value='TopAgent'>All Top Agent</option> <option value='DailyTotalSale'>Daily Sale All Service</option>");

                }

                if (sertype == "Flight") {

                    $('#drpService').html("<option value='0'>Select </option> <option value='AgentFlight'>Top Agent </option><option value='AgentCityFlight'>Sale By Agent City </option><option value='FlightName'>Total Sale By Airline </option> <option value='DRBySale'>Daily Flight Sale</option> <option value='DRByAirline'>Daily Sale By Airline</option> <option value='DRByAgent'>Daily FlightSale By Agent</option>");

                }


                if (sertype == "Hotel") {

                    $('#drpService').html("<option value='0'>Select </option> <option value='AgentHotel'>Top Agent  </option><option value='AgentCityHotel'>Sale By Agent City </option><option value='HotelCity'>Total sale By City</option><option value='DRHTlSale'>Daily Hotel Sale </option> <option value='DRHTlAgent'>Daily Hotel sale By Agentt Sale</option> ");

                }



                if (sertype == "Rail") {

                    $('#drpService').html(" <option value='0'>Select </option> <option value='AgentTrain'>Top Agent </option><option value='AgentCityRail'>Sale By Agent City </option><option value='RailClass'>Total sale By class  </option> <option value='DRRailSale'>Daily Rail Sale </option><option value='DRRAilAgent'>Daily Rail Sale By Agent</option> ");

                }


                if (sertype == "Bus") {

                    $('#drpService').html(" <option value='0'>Select </option> <option value='AgentBus'>Top Agent Bus</option> <option value='AgentCityBus'>Sale By Agent City </option> <option value='BusCity'>Sale By city </option> <option value='DRBusSale'>Daily Bus Sale </option><option value='DRBusAgent'>Daily Bus Sale By Agent</option> ");

                }

                if (sertype == "Utilty") {

                    $('#drpService').html(" <option value='0'>Select </option> <option value='AgentUtility'>Top Agent</option><option value='AgentCityUtilty'>Sale By Agent City </option> <option value='DRUtiltY'>Daily Utility Sale </option><option value='DRUtiltyagent'>Daily Utility Sale By Agent</option>");

                }


            });

            $("#drpService").change(function () {

                var service = "";


                if ($("#drpService option:selected").val() != 0) {

                    service = $.trim($("#drpService option:selected").val());
                }
                else {
                    alert("Select Report Type.");
                    $("#drpService").focus();
                    return false;
                }

                if (service == "DRByAirline") {

                    $("#tdairline").show();
                    $("#tdairlineval").show();

                }

                else {
                    $("#tdairline").hide();
                    $("#tdairlineval").hide();

                }


                if (service == "DRByAgent" || service == "DRRAilAgent" || service == "DRHTlAgent" || service == "DRBusAgent" || service == "DRUtiltyagent") {
                    $("#tdagnid").show();
                    $("#tdagnval").show();


                }

                else {
                    $("#tdagnid").hide();
                    $("#tdagnval").hide();

                }


                if (service == "AgentFlight" || service == "AgentHotel" || service == "AgentTrain" || service == "AgentBus" || service == "AgentUtility") {


                    $("#tdtoptype").show();
                    $("#tddrptopval").show();



                }

                else {
                    $("#tdtoptype").hide();
                    $("#tddrptopval").hide();

                }




            });



        });


        function monthWise() {
            $("#tblChart").hide();
            var v = $("#rbtMonth").val();
            if (document.getElementById('rbtMonth').checked) {
                $("#fromDate").hide()
                $("#From").hide()
                $("#ToDate").hide()
                $("#To").hide()
                $("#year").show()
                $("#drpYear").show()
                $("#Month").show()
                $("#drpMonth").show()
            } else {
                $("#year").hide()
                $("#drpYear").hide()
                $("#Month").hide()
                $("#drpMonth").hide()
                $("#fromDate").show()
                $("#From").show()
                $("#ToDate").show()
                $("#To").show()
            }

        }

        function GetGraph() {
             

            $("#wait").show();
            $("#tblChart").hide();
            var h = "";
            var year = "";
            var month = "";
            var month2 = "";
            var fromMonth = "";
            var toMonth = "";
            var service = "";
            var yearMonth = "";
            var fDate = "";
            var toDate = "";
            var Airlinecode = "";
            var AgentID = "";
            var toptype = "";


            if ($("#txtFromDate").val() != "") {

                fDate = $("#txtFromDate").val();
                month = fDate.split('/');
                fromMonth = month[1];
            }
            else {
                $("#wait").hide();
                alert("Select from date.");
                $("#txtFromDate").focus();
                return false;
            }
            if ($("#txtToDate").val() != "") {

                toDate = $("#txtToDate").val();
                month2 = toDate.split("/")
                toMonth = month2[1];
            }
            else {
                $("#wait").hide();
                alert("Select to date.");
                $("#txtToDate").focus();
                return false;
            }


            if ($("#drpServiceType option:selected").val() != 0) {

                service = $.trim($("#drpServiceType option:selected").val());
            }
            else {

                alert("Select Service type.");
                $("#wait").hide();
                $("#drpServiceType").focus();
                return false;
            }


            if ($("#drpService option:selected").val() != 0) {

                service = $.trim($("#drpService option:selected").val());
            }
            else {
                alert("Select Report type.");
                $("#wait").hide();
                $("#drpService").focus();
                return false;
            }




            if (service == "DRByAgent" || service == "DRRAilAgent" || service == "DRHTlAgent" || service == "DRBusAgent" || service == "DRUtiltyagent" || service == "DRByAirline" || service == "DailyTotalSale" || service == "DRHTlSale" || service == "DRBySale" || service == "DRRailSale" || service == "DRUtiltY" || service == "DRBusSale") {
                if (fromMonth != toMonth) {
                    $("#wait").hide();
                    alert("Select To Month Same As From Month");
                    $("#txtToDate").focus();
                    return false;

                }

            }


            if (service == "DRByAirline") {
                if ($("#hidtxtAirline").val() != "") {
                    Airlinecode = $("#hidtxtAirline").val();
                }
                else {
                    alert("Select AirlineCode");
                    $("#txtAirline").focus();
                    return false;
                }

            }



            if (service == "DRByAgent" || service == "DRRAilAgent" || service == "DRHTlAgent" || service == "DRBusAgent") {
                if ($("#hidtxtAgencyName").val() != "") {
                    AgentID = $("#hidtxtAgencyName").val();
                }
                else {
                    alert("SelectAgent ID");
                    $("#txtAgencyName").focus();
                    return false;
                }

            }


            if ($("#drpTopType option:selected").val() != 0) {

                toptype = parseInt($("#drpTopType option:selected").val());
            }

            $.ajax({

                type: 'POST',
                dataType: 'json',
                url: 'ChartReports.aspx/GetData',
                contentType: 'application/json; charset=utf-8',
                data: "{'service': '" + service + "','fDate': '" + fDate + "','toDate': '" + toDate + "','Airlinecode': '" + Airlinecode + "' ,'AgentID': '" + AgentID + "' ,'toptype': '" + toptype + "'}",
                cache: false,
                async: false,
                success:
                    function (response) {

                        $("#wait").hide();
                        // document.getElementById('wait').style.visibility == 'hidden';
                        drawVisualization(response.d);
                        if (service == "DRBySale" || service == "DRByAirline" || service == "DRRailSale" || service == "DRByAgent" || service == "DRRAilAgent" || service == "DRHTlSale" || service == "DRHTlAgent" || service == "DRBusSale" || service == "DRBusAgent" || service == "DRUtiltY" || service == "DRUtiltyagent" || service == "DailyTotalSale") {
                            drawColumnChartDAilyFltrpt(response.d);
                        }

                        else {
                            drawColumnChart(response.d);
                        }


                    },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    //alert(textStatus);

                    alert("No record found");

                }

            });
        }

        function drawVisualization(dataValues) {
            $("#tblChart").show();
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
            }

            var options = {

                is3D: true,
            };

            new google.visualization.PieChart(document.getElementById('visualization')).
                draw(data, { title: "View in Pie Chart" }, options);
        }

        function drawColumnChart(dataValues) {
            $("#tblChart").show();
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Amount');
            data.addColumn({ 'type': 'string', 'role': 'tooltip', 'p': { 'html': true } });

            for (var j = 0; j < dataValues.length; j++) {
                data.addRow([dataValues[j].ColumnName, dataValues[j].Value, dataValues[j].ToolTip]);
            }
            var options = {
                tooltip: { isHtml: true }
            };
            new google.visualization.ColumnChart(document.getElementById('divColumnChart')).

                 draw(data, { title: "View in Column Chart" });
        }

        function drawColumnChartDAilyFltrpt(dataValues) {

            $("#tblChart").show();
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Total sale');
            data.addColumn({ 'type': 'string', 'role': 'tooltip', 'p': { 'html': true } });

            for (var j = 0; j < dataValues.length; j++) {
                data.addRow([dataValues[j].ColumnName, dataValues[j].Value, dataValues[j].ToolTip]);
            }

            var options = {
                tooltip: { isHtml: true },
                is3D: true,
            };


            //google.visualization.events.addListener(chart, "error", function errorHandler(e) {
            //    google.visualization.errors.removeError(e.id);
            //});
            new google.visualization.LineChart(document.getElementById('divColumnChart')).

                 draw(data, { title: "View in Line Chart" }, options);
        }

        $(function () {
            $("#txtFromDate").datepicker(
                   {
                       numberOfMonths: 1,

                       autoSize: true, dateFormat: 'dd/mm/yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: 0, minDate: '-1y', navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true, onSelect: UpdateToDate
                   }
              )

        });
        $(function () {
            $("#txtToDate").datepicker(
                   {
                       numberOfMonths: 1,

                       autoSize: true, dateFormat: 'dd/mm/yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: 0, minDate: '-1y', navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                   }
              )

        });
        function UpdateToDate(dateText, inst) {
            $("#txtToDate").datepicker("option", { minDate: dateText });
        }

    </script>

</asp:Content>
