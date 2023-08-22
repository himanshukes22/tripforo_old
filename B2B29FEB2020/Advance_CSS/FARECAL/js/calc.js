

var resulmain; var resulmain2;
tjq(document).ready(function ()  {
    tjq("#btnOpenCal").click(function () {
        $("#cal-controler").css("display", "block");
        let isfieldactive = false;

        var value = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
        var getvalue = value.options[value.selectedIndex].value;
        if (getvalue == "0" || getvalue == "") {
            alert("Please select sector first!");
            return false;
        }
        else {
            isfieldactive = true;
        }

        if (isfieldactive) {
            //tjq("#demoEvoCalendar").removeClass("evo-calendar calendar-initialized").html("");
			tjq("#demoEvoCalendar").removeClass("evo-calendar calendar-initialized").html("").after("<script>$(document.body).on('click', '#CloseCal', function (e) { $('#cal-controler').css('display', 'none'); });</script>");
            tjq("#waitMessageF").show();
            var e = this;

            var ee = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
            var HidSector = ee.options[ee.selectedIndex].value;

            document.getElementById("hidtxtDepCity1F").value = HidSector.split("-")[0]
            document.getElementById("hidtxtArrCity1F").value = HidSector.split("-")[1]

            var hidtxtDepCity1 = HidSector.split("-")[0]
            var hidtxtArrCity1 = HidSector.split("-")[1]




            var ee = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
            var HidSectorD = ee.options[ee.selectedIndex].text;

            document.getElementById("txtDepCity1F").value = HidSectorD.split("-")[0]
            document.getElementById("txtArrCity1F").value = HidSectorD.split("-")[1]

            var txtDepCity1F = HidSectorD.split("-")[0]
            var txtArrCity1F = HidSectorD.split("-")[1]


            var t = tjq("input[name='TripTypeF']:checked").val();


            var eee = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
            var HidSector1 = eee.options[eee.selectedIndex].value;
            var array = HidSector1.split("-")

            var t = tjq("input[name='TripTypeF']:checked").val();
            if (array.length == 3) {
                t = "rdbRoundTripF";
            }
            else {
                t == "rdbOneWayF"
            }


            var n;
            var r;
            var i;

            n = "TRUE"


            r = "FALSE"

            i = "FALSE"


            var s = "TripType=" + t + "&txtDepCity1=" + txtDepCity1F + "&txtArrCity1=" + txtArrCity1F + "&hidtxtDepCity1=" + hidtxtDepCity1 + "&hidtxtArrCity1=" + hidtxtArrCity1 + "&Adult=" + tjq("#Adult").val();
            s += "&Child=" + tjq("#Child").val() + "&Infant=" + tjq("#Infant").val() + "&Cabin=" + "" + "&txtAirline=" + tjq("#txtAirline").val() + "&hidtxtAirline=" + tjq("#hidtxtAirline").val() + "&txtDepDate=" + tjq("#txtDepDate").val() + "&txtRetDate=" + tjq("#txtRetDate").val() + "&RTF=" + n + "&NStop=" + i + "&RTF=" + n + "&Trip=" + Trip + "&GRTF=" + r;



            //s += "&txtDepCity2=" + e.txtDepCity2.val() + "&hidtxtDepCity2=" + e.hidtxtDepCity2.val() + "&txtArrCity2=" + e.txtArrCity2.val() + "&hidtxtArrCity2=" + e.hidtxtArrCity2.val() + "&txtDepDate2=" + e.txtDepDate2.val();
            //s += "&txtDepCity3=" + e.txtDepCity3.val() + "&hidtxtDepCity3=" + e.hidtxtDepCity3.val() + "&txtArrCity3=" + e.txtArrCity3.val() + "&hidtxtArrCity3=" + e.hidtxtArrCity3.val() + "&txtDepDate3=" + e.txtDepDate3.val();
            //s += "&txtDepCity4=" + e.txtDepCity4.val() + "&hidtxtDepCity4=" + e.hidtxtDepCity4.val() + "&txtArrCity4=" + e.txtArrCity4.val() + "&hidtxtArrCity4=" + e.hidtxtArrCity4.val() + "&txtDepDate4=" + e.txtDepDate4.val();
            //s += "&txtDepCity5=" + e.txtDepCity5.val() + "&hidtxtDepCity5=" + e.hidtxtDepCity5.val() + "&txtArrCity5=" + e.txtArrCity5.val() + "&hidtxtArrCity5=" + e.hidtxtArrCity5.val() + "&txtDepDate5=" + e.txtDepDate5.val();
            //s += "&txtDepCity6=" + e.txtDepCity6.val() + "&hidtxtDepCity6=" + e.hidtxtDepCity6.val() + "&txtArrCity6=" + e.txtArrCity6.val() + "&hidtxtArrCity6=" + e.hidtxtArrCity6.val() + "&txtDepDate6=" + e.txtDepDate6.val();


            var fgg = {
                // "Trip": "",
                // "TripType": "",
                //  "Trip1": "",
                "TripType1": "rdbOneWay",
                "DepartureCity": txtDepCity1F,
                "ArrivalCity": txtArrCity1F,
                "HidTxtDepCity": hidtxtDepCity1,
                "HidTxtArrCity": hidtxtArrCity1,
                "Adult": tjq("#AdultF").val(),
                "Child": tjq("#ChildF").val(),
                "Infant": tjq("#InfantF").val(),
                "Cabin": "",
                "AirLine": "",
                "HidTxtAirLine": "",
                "DepDate": '01/01/1900',//tjq("#txtDepDate").val(),
                "RetDate": '01/01/1900',//tjq("#txtDepDate").val(),
                "RTF": "true",
                "GDSRTF": "false",
                "NStop": "false",
                "UID": "",
                "DISTRID": "SPRING",
                "UserType": "",
                "TypeId": "",
                "OwnerId": "",
                "TDS": "0",
                "AgentType": "",
                "IsCorp": "false",
                "Provider": "",
                "SessionId": "",
                "DepartureCity2": "",
                "ArrivalCity2": "",
                "HidTxtDepCity2": "",
                "HidTxtArrCity2": "",
                "DepDate2": "",
                "DepartureCity3": "",
                "ArrivalCity3": "",
                "HidTxtDepCity3": "",
                "HidTxtArrCity3": "",
                "DepDate3": "",
                "DepartureCity4": "",
                "ArrivalCity4": "",
                "HidTxtDepCity4": "",
                "HidTxtArrCity4": "",
                "DepDate4": "",
                "DepartureCity5": "",
                "ArrivalCity5": "",
                "HidTxtDepCity5": "",
                "HidTxtArrCity5": "",
                "DepDate5": "",
                "DepartureCity6": "",
                "ArrivalCity6": "",
                "HidTxtDepCity6": "",
                "HidTxtArrCity6": "",
                "DepDate6": "",
                "CheckReprice": false,

            }

            var comUrl = UrlBase + "FTBSER.asmx/FTBFARE";
            tjq.ajax({
                url: comUrl,
                data: "{'objreq':'" + JSON.stringify(fgg) + "'}",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function (data) {
                    var comResult = data.d;
                    var res = tjq.parseJSON(data.d);
                    if (res != "") {
                        var fltdetailsList = new Array();
                        var res2 = tjq.parseJSON(res[0])
                        resulmain = res2.result;
                        for (var i = 0; i < res2.result[0].length; i++) {
                            fltdetailsList.push({
                                id: "d8jai7s" + i,
                                name: res2.result[0][i].TotalFare,
                                description: res2.result[0][i].DepartureCityName + "(" + res2.result[0][i].OrgDestFrom + ")-" + res2.result[0][i].ArrivalCityName + "(" + res2.result[0][i].OrgDestTo + ")",
                                date: '20' + res2.result[0][i].DepartureDate.substring(6, 4) + '-' + res2.result[0][i].DepartureDate.substring(4, 2) + '-' + res2.result[0][i].DepartureDate.substring(0, 2),
                                type: "Flight",
                                everyYear: 0,
                                fare: res2.result[0][i].TotalFare,
                                ValiDatingCarrier: res2.result[0][i].ValiDatingCarrier,
                                MarketingCarrier: res2.result[0][i].MarketingCarrier,
                                OrgDestFrom: res2.result[0][i].OrgDestFrom,
                                FlightIdentification: res2.result[0][i].FlightIdentification,
                                AirLineName: res2.result[0][i].AirLineName,
                                RBD: res2.result[0][i].RBD,
                                Departure_Date: res2.result[0][i].Departure_Date,
                                DepartureTime: res2.result[0][i].DepartureTime,
                                DepartureTerminal: res2.result[0][i].DepartureTerminal,
                                TotDur: res2.result[0][i].TotDur,
                                Stops: res2.result[0][i].Stops,
                                DepartureLocation: res2.result[0][i].DepartureLocation,
                                ArrivalLocation: res2.result[0][i].ArrivalLocation,
                                ArrivalTime: res2.result[0][i].ArrivalTime,
                                Arrival_Date: res2.result[0][i].Arrival_Date,
                                ArrivalTerminal: res2.result[0][i].ArrivalTerminal,
                                TotalFare: res2.result[0][i].TotalFare,
                                AdtFar: res2.result[0][i].AdtFar,
                                AdtFareType: res2.result[0][i].AdtFareType,
                                AvailableSeats1: res2.result[0][i].AvailableSeats1,
                                LineNumber: res2.result[0][i].LineNumber,
                                 FareDet: res2.result[0][i].FareDet,
                            });
                        }
                        tjq("#fxdid").removeClass("theme-search-area-tabs col-md-6");
                        tjq("#fxdid").addClass("theme-search-area-tabs col-md-12");
                        tjq("#fxdid .tab-content").css("width", "100%")
                        abcdf(fltdetailsList);
                        tjq("#sidebarToggler").click();
                    }
                    else {
                        tjq("#waitMessageF").hide();
                        tjq("#demoEvoCalendar").html("<p class='text-center' style='font-size: 20px;margin: 60px; color:red;'>Sorry! We are unable to fetching flight details based on the information you provided, Please try another sector.</p>")
                    }

                    //  for (var i = 0;i<)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    tjq("#fxdid").removeClass("theme-search-area-tabs col-md-12");
                    tjq("#fxdid").addClass("theme-search-area-tabs col-md-6");
                    tjq("#fxdid .tab-content").css("width", "500px")
                    tjq("#waitMessageF").hide();
                    return textStatus;
                }


            });
        }
    });


    tjq(document).on("click", ".btnbookf", function () {
        var clsb = $.trim(tjq(this).attr("class"));

        
        var lineNum = clsb.indexOf("atr") != -1 ? tjq.trim(tjq(this).attr("title"))+"S" : tjq.trim(tjq(this).attr("title"));
        $("#modal-btn-confirm").attr("rel", lineNum);
        $("#mi-modal").modal('show');
    });

    tjq(document).on("click", "#acgggh", function () {
        $("#collapseExample4").toggleClass('hide');
        $("#acggghSearch").toggleClass('hide');
    });

    tjq(document).on("click", "#acggghSearch", function () {
        $("#collapseExample4").toggleClass('hide');
        $("#acggghSearch").toggleClass('hide');
        $("#fltpx").html('<div> <div style="text-align: center; z-index: 101111111111111; font-size: 12px; font-weight: bold; padding: 20px;"> <div class="backdrop"> <div id="searchquery" style="color: #000; font-size: 18px; text-align: center;"> </div> <div> <p class="text-center" style="font-size: 25px; margin-top: 60px; color: #5ea51d;"> We are fetching flight details based on the information you provided. <i class="fa fa-spinner fa-pulse"></i> </p> </div> <span id="loading-msg"></span> </div> </div> </div>');
        $("#pass-pax").val("")
        $("#pax-mod").modal('show');
        var fltSelectedArray4 = JSLINQ(resulmain2[0]);
        var fltSelectedArray2 = fltSelectedArray4.items;
        let isfieldactive = false;
        var datastrdstfrom = fltSelectedArray2[0].DepartureCityName + '(' + fltSelectedArray2[0].OrgDestFrom+')';
        var datastrdstto = fltSelectedArray2[0].ArrivalCityName + '(' + fltSelectedArray2[0].OrgDestTo + ')';
        var datastrdate = fltSelectedArray2[0].DepartureDate.substring(0, 2) + '/' + fltSelectedArray2[0].DepartureDate.substring(4, 2) + '/' + '20' + fltSelectedArray2[0].DepartureDate.substring(6, 4) ;

       // var datastrdate = '20' + fltSelectedArray2[0].DepartureDate.substring(6, 4) + '-' + fltSelectedArray2[0].DepartureDate.substring(4, 2) + '-' + fltSelectedArray2[0].DepartureDate.substring(0, 2);
        var datastrcarrier = '(' + fltSelectedArray2[0].ValiDatingCarrier + ')';
        var dataloopcount = "1"
        var Amt = 0;
        ;
        var Avls = 0;
        var sst = { datastrdstfrom: datastrdstfrom, datastrdstto: datastrdstto, datastrdate: datastrdate, datastrcarrier: datastrcarrier, dataloopcount: dataloopcount, Amt: Amt, Avls: Avls }
   //     $("#pass-pax").val(JSON.stringify(sst))
        var value = datastrdstfrom + "-" + datastrdstto;


        isfieldactive = true;


        if (isfieldactive) {


            var e = this;

            var ee = datastrdstfrom + "-" + datastrdstto;
            var HidSector = fltSelectedArray2[0].OrgDestFrom + ",IN-" + fltSelectedArray2[0].OrgDestTo + ",IN";


            var hidtxtDepCity1 = HidSector.split("-")[0]
            var hidtxtArrCity1 = HidSector.split("-")[1]



            var t = "rdbOneWayF"



            var n;
            var r;
            var i;

            n = "TRUE"


            r = "FALSE"

            i = "FALSE"


            var fgg = {
                // "Trip": "",
                // "TripType": "",
                //  "Trip1": "",
                "TripType1": "rdbOneWay",
                "DepartureCity": datastrdstfrom,
                "ArrivalCity": datastrdstto,
                "HidTxtDepCity": hidtxtDepCity1,
                "HidTxtArrCity": hidtxtArrCity1,
                "Adult": tjq("#AdultF1").val(),
                "Child": tjq("#ChildF1").val(),
                "Infant": tjq("#InfantF1").val(),
                "Cabin": "",
                "AirLine": "",
                "HidTxtAirLine": "",
                "DepDate": datastrdate,
                "RetDate": datastrdate,
                "RTF": "true",
                "GDSRTF": "false",
                "NStop": "false",
                "UID": "",
                "DISTRID": "SPRING",
                "UserType": "",
                "TypeId": "",
                "OwnerId": "",
                "TDS": "0",
                "AgentType": "",
                "IsCorp": "false",
                "Provider": "",
                "SessionId": "",
                "DepartureCity2": "",
                "ArrivalCity2": "",
                "HidTxtDepCity2": "",
                "HidTxtArrCity2": "",
                "DepDate2": "",
                "DepartureCity3": "",
                "ArrivalCity3": "",
                "HidTxtDepCity3": "",
                "HidTxtArrCity3": "",
                "DepDate3": "",
                "DepartureCity4": "",
                "ArrivalCity4": "",
                "HidTxtDepCity4": "",
                "HidTxtArrCity4": "",
                "DepDate4": "",
                "DepartureCity5": "",
                "ArrivalCity5": "",
                "HidTxtDepCity5": "",
                "HidTxtArrCity5": "",
                "DepDate5": "",
                "DepartureCity6": "",
                "ArrivalCity6": "",
                "HidTxtDepCity6": "",
                "HidTxtArrCity6": "",
                "DepDate6": "",
                "CheckReprice": false,

            }

            var comUrl = UrlBase + "FTBSER.asmx/FTBFARE";
            tjq.ajax({
                url: comUrl,
                data: "{'objreq':'" + JSON.stringify(fgg) + "'}",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function (data) {
                    var comResult = data.d;
                    var res = tjq.parseJSON(data.d);
                    if (res != "") {
                        var fltdetailsList = new Array();
                        var res2 = tjq.parseJSON(res[0])
                        resulmain2 = res2.result;
                        for (var i = 0; i < res2.result[0].length; i++) {
                            fltdetailsList.push({
                                id: "d8jai7s" + i,
                                name: res2.result[0][i].TotalFare,
                                description: res2.result[0][i].DepartureCityName + "(" + res2.result[0][i].OrgDestFrom + ")-" + res2.result[0][i].ArrivalCityName + "(" + res2.result[0][i].OrgDestTo + ")",
                                date: '20' + res2.result[0][i].DepartureDate.substring(6, 4) + '-' + res2.result[0][i].DepartureDate.substring(4, 2) + '-' + res2.result[0][i].DepartureDate.substring(0, 2),
                                type: "Flight",
                                everyYear: 0,
                                fare: res2.result[0][i].TotalFare,
                                ValiDatingCarrier: res2.result[0][i].ValiDatingCarrier,
                                MarketingCarrier: res2.result[0][i].MarketingCarrier,
                                OrgDestFrom: res2.result[0][i].OrgDestFrom,
                                FlightIdentification: res2.result[0][i].FlightIdentification,
                                AirLineName: res2.result[0][i].AirLineName,
                                RBD: res2.result[0][i].RBD,
                                Departure_Date: res2.result[0][i].Departure_Date,
                                DepartureTime: res2.result[0][i].DepartureTime,
                                DepartureTerminal: res2.result[0][i].DepartureTerminal,
                                TotDur: res2.result[0][i].TotDur,
                                Stops: res2.result[0][i].Stops,
                                DepartureLocation: res2.result[0][i].DepartureLocation,
                                ArrivalLocation: res2.result[0][i].ArrivalLocation,
                                ArrivalTime: res2.result[0][i].ArrivalTime,
                                Arrival_Date: res2.result[0][i].Arrival_Date,
                                ArrivalTerminal: res2.result[0][i].ArrivalTerminal,
                                TotalFare: res2.result[0][i].TotalFare,
                                AdtFar: res2.result[0][i].AdtFar,
                                AdtFareType: res2.result[0][i].AdtFareType,
                                AvailableSeats1: res2.result[0][i].AvailableSeats1,
                                LineNumber: res2.result[0][i].LineNumber,
                                FareDet: res2.result[0][i].FareDet,
                            });
                        }

                        createLayout2(fltdetailsList);

                    }
                    else {

                    }

                    //  for (var i = 0;i<)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                    return textStatus;
                }


            });
        }

    });
    $("#modal-btn-cancel").click(function () {
        $("#mi-modal").modal('hide');
    });

    $("#pax-confirm").click(function () {
        $("#flt-info").trigger('click');
    });

    $("#modal-btn-confirm").click(function () {
        $("#modal-btn-confirm").html("Processing...<i class='fa fa-spinner fa-pulse'></i>");
        var lineNum = $.trim(tjq(this).attr("rel"));
       // var lineNum = $.trim(this.rel); //.split('_');
        // var result = resulmain;
        var result = lineNum.indexOf("S") != -1 ? resulmain2 : resulmain;

       // var result = resulmain;
        ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

        tjq("#searchquery").hide();

        //   var lineNum = tjq.trim(tjq(".btnbookf").attr("title"));
        lineNum = lineNum.replace("S", "");
        var lineNo = tjq.trim(lineNum.split('api')[0])
        var fltSelectedArray = JSLINQ(result[0])
            .Where(function (item) { return item.LineNumber == lineNum || item.LineNumber == lineNo; })
            .Select(function (item) { return item });
        var arr = new Array(fltSelectedArray.items);
        var iscahem = arr[0][0].LineNumber;
        for (var i = 0; i < arr[0].length; i++) {
            arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber;
            arr[0][i].LineNumber = arr[0][i].LineNumber;


        }
        if (arr[0][0].Trip == "I") {
            var urlParams = new URLSearchParams(location.search);
            var NStop = urlParams.get('NStop');
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails_LZCmp";
            tjq.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {


                    if (e.d.ChangeFareO.TrackId == "0") {
                        alert("Fare Changed Please Try again");

                    } else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'I');
                    }

                    else if (NStop == "TRUE") {
                        window.location = UrlBase + "FlightInt/CustomerInfoFixDep.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                    else {
                        window.location = UrlBase + "International/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                },
                error: function (e, t, n) {

                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
        else {
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
            tjq.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }




                    if (e.d.ChangeFareO.TrackId == "0") {
                        var r = confirm("Fare changed Please Try again.");
                        if (r == true) {
                            location.reload();
                        }
                        tjq('#ConfmingFlight').hide();
                        tjq(document).ajaxStop(tjq.unblockUI)
                    }
                    else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
                    }
                    else {

                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                    }



                },
                error: function (e, t, n) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }
                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
    });


    tjq(document).on("click", ".BookSectorTicket", function () {
        $("#collapseExample4").addClass('hide');
        $("#acggghSearch").addClass('hide');
        $("#fltpx").html('<div> <div style="text-align: center; z-index: 101111111111111; font-size: 12px; font-weight: bold; padding: 20px;"> <div class="backdrop"> <div id="searchquery" style="color: #000; font-size: 18px; text-align: center;"> </div> <div> <p class="text-center" style="font-size: 25px; margin-top: 60px; color: #5ea51d;"> We are fetching flight details based on the information you provided. <i class="fa fa-spinner fa-pulse"></i> </p> </div> <span id="loading-msg"></span> </div> </div> </div>');
        $("#pass-pax").val("")
        $("#pax-mod").modal('show');
        resulmain2 = null;
        tjq("#sapnTotPaxF1").val("1 Traveler(s)");
        tjq("#AdultF1").val("1");
        tjq("#ChildF1").val("0");
        tjq("#InfantF1").val("0");
        let isfieldactive = false;
        var datastrdstfrom = $(this).attr("data-strdstfrom").replace(',', '');
        var datastrdstto = $(this).attr("data-strdstto").replace(',', '');
        var datastrdate = $(this).attr("data-strdate");
        var datastrcarrier = $(this).attr("data-strcarrier");
        var dataloopcount = $(this).attr("data-loopcount");
        var HideDepCountryCode = $(this).attr("hidedep");
        var HideArrCountryCode = $(this).attr("hidearr");
        var Amt = $($(this).find(".price-list")).text().replace("₹", "").trim().replace(/([a-zA-Z ])/g, "");
;
        var Avls = $($(this).find(".seat-list")).text().trim();
        var sst = { datastrdstfrom: datastrdstfrom, datastrdstto: datastrdstto, datastrdate: datastrdate, datastrcarrier: datastrcarrier, dataloopcount: dataloopcount, Amt: Amt, Avls: Avls }
        $("#pass-pax").val(JSON.stringify(sst))
        var value = datastrdstfrom + "-" + datastrdstto;


        isfieldactive = true;


        if (isfieldactive) {


            var e = this;

            var ee = datastrdstfrom + "-" + datastrdstto;
            var HidSector = $(this).attr("data-strdstfrom").split(',')[1].replace('(', '').replace(')', '') + "," + HideDepCountryCode + "-" + $(this).attr("data-strdstto").split(',')[1].replace('(', '').replace(')', '') + "," + HideArrCountryCode;


            var hidtxtDepCity1 = HidSector.split("-")[0]
            var hidtxtArrCity1 = HidSector.split("-")[1]



            var t = "rdbOneWayF"



            var n;
            var r;
            var i;

            n = "TRUE"


            r = "FALSE"

            i = "FALSE"


            var fgg = {
                // "Trip": "",
                // "TripType": "",
                //  "Trip1": "",
                "TripType1": "rdbOneWay",
                "DepartureCity": datastrdstfrom,
                "ArrivalCity": datastrdstto,
                "HidTxtDepCity": hidtxtDepCity1,
                "HidTxtArrCity": hidtxtArrCity1,
                "Adult": tjq("#AdultF1").val(),
                "Child": tjq("#ChildF1").val(),
                "Infant": tjq("#InfantF1").val(),
                "Cabin": "",
                "AirLine": "",
                "HidTxtAirLine": "",
                "DepDate": datastrdate,
                "RetDate": datastrdate,
                "RTF": "true",
                "GDSRTF": "false",
                "NStop": "false",
                "UID": "",
                "DISTRID": "SPRING",
                "UserType": "",
                "TypeId": "",
                "OwnerId": "",
                "TDS": "0",
                "AgentType": "",
                "IsCorp": "false",
                "Provider": "",
                "SessionId": "",
                "DepartureCity2": "",
                "ArrivalCity2": "",
                "HidTxtDepCity2": "",
                "HidTxtArrCity2": "",
                "DepDate2": "",
                "DepartureCity3": "",
                "ArrivalCity3": "",
                "HidTxtDepCity3": "",
                "HidTxtArrCity3": "",
                "DepDate3": "",
                "DepartureCity4": "",
                "ArrivalCity4": "",
                "HidTxtDepCity4": "",
                "HidTxtArrCity4": "",
                "DepDate4": "",
                "DepartureCity5": "",
                "ArrivalCity5": "",
                "HidTxtDepCity5": "",
                "HidTxtArrCity5": "",
                "DepDate5": "",
                "DepartureCity6": "",
                "ArrivalCity6": "",
                "HidTxtDepCity6": "",
                "HidTxtArrCity6": "",
                "DepDate6": "",
                "CheckReprice": false,

            }

            var comUrl = UrlBase + "FTBSER.asmx/FTBFARE";
            tjq.ajax({
                url: comUrl,
                data: "{'objreq':'" + JSON.stringify(fgg) + "'}",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function (data) {
                    var comResult = data.d;
                    var res = tjq.parseJSON(data.d);
                    if (res != "") {
                        var fltdetailsList = new Array();
                        var res2 = tjq.parseJSON(res[0])
                        resulmain2 = res2.result;
                        for (var i = 0; i < res2.result[0].length; i++) {
                            fltdetailsList.push({
                                id: "d8jai7s" + i,
                                name: res2.result[0][i].TotalFare,
                                description: res2.result[0][i].DepartureCityName + "(" + res2.result[0][i].OrgDestFrom + ")-" + res2.result[0][i].ArrivalCityName + "(" + res2.result[0][i].OrgDestTo + ")",
                                date: '20' + res2.result[0][i].DepartureDate.substring(6, 4) + '-' + res2.result[0][i].DepartureDate.substring(4, 2) + '-' + res2.result[0][i].DepartureDate.substring(0, 2),
                                type: "Flight",
                                everyYear: 0,
                                fare: res2.result[0][i].TotalFare,
                                ValiDatingCarrier: res2.result[0][i].ValiDatingCarrier,
                                MarketingCarrier: res2.result[0][i].MarketingCarrier,
                                OrgDestFrom: res2.result[0][i].OrgDestFrom,
                                FlightIdentification: res2.result[0][i].FlightIdentification,
                                AirLineName: res2.result[0][i].AirLineName,
                                RBD: res2.result[0][i].RBD,
                                Departure_Date: res2.result[0][i].Departure_Date,
                                DepartureTime: res2.result[0][i].DepartureTime,
                                DepartureTerminal: res2.result[0][i].DepartureTerminal,
                                TotDur: res2.result[0][i].TotDur,
                                Stops: res2.result[0][i].Stops,
                                DepartureLocation: res2.result[0][i].DepartureLocation,
                                ArrivalLocation: res2.result[0][i].ArrivalLocation,
                                ArrivalTime: res2.result[0][i].ArrivalTime,
                                Arrival_Date: res2.result[0][i].Arrival_Date,
                                ArrivalTerminal: res2.result[0][i].ArrivalTerminal,
                                TotalFare: res2.result[0][i].TotalFare,
                                AdtFar: res2.result[0][i].AdtFar,
                                AdtFareType: res2.result[0][i].AdtFareType,
                                AvailableSeats1: res2.result[0][i].AvailableSeats1,
                                LineNumber: res2.result[0][i].LineNumber,
                                FareDet: res2.result[0][i].FareDet,
                            });
                        }

                        createLayout(fltdetailsList);

                    }
                    else {

                    }

                    //  for (var i = 0;i<)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                    return textStatus;
                }


            });
        }


        //let strdstfrom = tjq(this).data("strdstfrom");
        //let strdstto = tjq(this).data("strdstto");
        //let strdate = tjq(this).data("strdate");
        //let strcarrier = tjq(this).data("strcarrier");
        //let loopcount = tjq(this).data("loopcount");
        //alert(loopcount);

    });

    tjq(document).on("click", "#flt-info", function () {
        var clsb = $.trim(tjq(this).attr("class"));

        var result = clsb.indexOf("flt-info2") != -1 ? resulmain2 : resulmain;
        $('#1api6ENRMLITZNRML_Fare').html("");
        var lineNum = clsb.indexOf("flt-info2") != -1 ? $.trim(tjq(this).attr("rel")) + "S" : $.trim(tjq(this).attr("rel"));

        //   var lineNum = tjq.trim(tjq(this).attr("title"));
        $("#fare-summ").attr("rel", lineNum);
        $("#fare_Det").attr("rel", lineNum);
        $("#bag_det").attr("rel", lineNum);
        $("#can_flt").attr("rel", lineNum);
        lineNum = lineNum.replace("S", "");
        var lineNo = tjq.trim(lineNum.split('api')[0])
        var fltSelectedArray = JSLINQ(result[0])
            .Where(function (item) { return item.LineNumber == lineNum || item.LineNumber == lineNo; })
            .Select(function (item) { return item });
        var arr = new Array(fltSelectedArray.items);
        var iscahem = arr[0][0].LineNumber;
        for (var i = 0; i < arr[0].length; i++) {
            arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber;
            arr[0][i].LineNumber = arr[0][i].LineNumber;
        }

        $('#1api6ENRMLITZNRML_Fare').html(CreateFareBreakUp(arr[0][0]));
        $("#flt-details").modal('show');


    });
    $('.fltDetailslink').click(function (event) {
        var lineNums = $.trim(this.rel); //.split('_');
        // var result = resulmain;
        var result = lineNums.indexOf("S") != -1 ? resulmain2 : resulmain;
        $('#1api6ENRMLITZNRML_fltdt').html("");
        debugger;
        if (this.rel != null) {
            // $('#' + this.rel + '_').slideUp();

            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            lineNums = lineNums.replace("S", "");

            // var airc = lineNums[1] + "_" + lineNums[2];
            var OB = JSLINQ(result[0])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });

            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });

            var R = JSLINQ(OB.items)
            //.Where(function (item) { return item.Flight == "2"; })
            //.Select(function (item) { return item });


            var fta = JSLINQ(OB.items)
                .Select(function (item) { return item.Flight });

            var ft = Math.max.apply(Math, fta.items);

            var str1 = '<div class="bdr-fltdet">';
            if (O.items.length > 0) {

                var totDur = "";

                if (O.items[0].Provider == "TB") {
                    totDur = GetTotalDuration(O.items);
                }
                else {
                    totDur = MakeupTotDur(O.items[0].TotDur)
                }
                //str1 += '<div class="large-12 medium-12 small-12 bld" style="position: relative;right: 20px;top: -10px;float: left;width: 112%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;"><span class="f20">' + O.items[0].DepartureCityName + ' → ' + O.items[O.items.length - 1].ArrivalCityName + '</span> | <span class="f16" style="color: #979797;font-size: 11px;">' + totDur + '</span></div><div class="clear1"></div>';

                //str1 += '<div class=""><div class="large-12 medium-12 small-12"><span class="f16">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span></div><div class="clear"></div>';
                for (var i = 0; i < O.items.length; i++) {
                    if (i >= 1 && O.items.length > 1 && i < O.items.length) {
                        str1 += GetLayOver(O.items, i);
                    }

                    if ((O.items[i].MarketingCarrier == '6E') && ($.trim(O.items[i].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" />' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>'
                    }

                    var ftme = calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + '';
                    if (O.items[0].Trip == "I" && O.items[0].Provider == "1G") {
                        ftme = O.items[i].TripCnt + ' HRS';
                    }
                    //else if (O.items[0].Trip == "I") {
                    //    ftme = "&nbsp;";
                    //}

                    //sanjeet//
                    //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="col-md-3 col-xs-3">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time">' + O.items[i].DepartureCityName + '<span>' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span></p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Departure_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].DepartureTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';

                    str1 += '<div class="col-md-4 col-xs-2">';
                    str1 += '<div class="fly">';
                    str1 += '<div class="fly1">';
                    str1 += '<span class="jtm">' + ftme + '</span>';
                    str1 += '</div>';
                    str1 += '<div class="fly2">';
                    str1 += '<span class="fart fart3">' + O.items[i].AdtFareType + '</span>';
                    str1 += '</div>';
                    str1 += '</div>';
                    str1 += '</div>';


                    //ftme = O.items[i].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';

                    str1 += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '</span> ' + O.items[i].ArrivalCityName + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Arrival_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].ArrivalTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';


                }
            }
            for (var ff = 2; ff <= ft; ff++) {
                var R = JSLINQ(OB.items)
                    .Where(function (item) { return item.Flight == ff.toString(); })
                    .Select(function (item) { return item });

                if (R.items.length > 0) {

                    var totDur = "";

                    if (R.items[0].Provider == "TB") {
                        totDur = GetTotalDuration(R.items);
                    }
                    else {
                        totDur = MakeupTotDur(R.items[0].TotDur)
                    }

                    str1 += '</div><div class="" style="position:relative;top:12px;"><div class="large-12 medium-12 small-12 bld" style="position: relative;top: -10px;float: left;width: 100%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + totDur + '</div><div class="clear"></div>';
                    for (var j = 0; j < R.items.length; j++) {

                        if (j >= 1 && R.items.length > 1 && j < R.items.length) {
                            str1 += GetLayOver(R.items, j);
                        }

                        if ((R.items[j].MarketingCarrier == '6E') && ($.trim(R.items[j].sno).search("INDIGOCORP") >= 0)) {
                            str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        else {
                            str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        var Ftmer = calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS';
                        debugger;
                        if (R.items[0].Trip == "I" && R.items[0].Provider == "1G") {
                            Ftmer = R.items[j].TripCnt + ' HRS';


                        }
                        //else if (R.items[0].Trip == "I") {
                        //    Ftmer = "&nbsp;";
                        //}
                        //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + Ftmer + '</div>'
                        //Kunal Work
                        //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'

                        str1 += '<div class="col-md-3 col-xs-3">';
                        str1 += '<div class="theme-search-results-item-flight-section-meta">';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-time">' + R.items[j].DepartureLocation + '<span>' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span></p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + R.items[j].Departure_Date + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + R.items[j].DepartureTerminal + '</p>';
                        str1 += '</div > ';
                        str1 += '</div > ';

                        str1 += '<div class="col-md-4 col-xs-2">';
                        str1 += '<div class="fly">';
                        str1 += '<div class="fly1">';
                        str1 += '<span class="jtm">' + ftme + '</span>';
                        str1 += '</div>';
                        str1 += '<div class="fly2">';
                        str1 += '<span class="fart fart3">' + O.items[j].AdtFareType + '</span>';
                        //str1 += '<span class="fmeal">Free Meal</span>';
                        str1 += '</div>';
                        str1 += '</div>';
                        str1 += '</div>';

                        str1 += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
                        str1 += '<div class="theme-search-results-item-flight-section-meta">';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-time"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '</span> ' + R.items[j].ArrivalLocation + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + R.items[j].Arrival_Date + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + R.items[j].ArrivalTerminal + '</p>';
                        str1 += '</div > ';
                        str1 += '</div > ';
                    }
                }
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
            //    }
            //} catch (ex) { }
            str1 += '<div class="clear"></div>';
            str1 += '</div></div><div class="clear"></div>';
            str1 += '<div class="clear"></div>';
            $('#1api6ENRMLITZNRML_fltdt').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + this.rel + '_fltdt').show();
            //$('#' + this).hide();
        }
    });




    $('.fltBagDetails').click(function (event) {
        //var result = resulmain;
        var lineNums = $.trim(this.rel); //.split('_');

        var result = lineNums.indexOf("S") != -1 ? resulmain2 : resulmain;
        $('#1api6ENRMLITZNRML_bag').html("");
        if (this.rel != null) {
            //$('#' + this.rel + '_').slideUp();
            lineNums = lineNums.replace("S", "");
            var OB = JSLINQ(result[0])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });

            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });

            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });
            var str1 = '<div>';
            if (O.items.length > 0) {
                str1 += '<div class=""><div></div>';
                str1 += '<table class="rtable"><tr><td  class="w50 f16 bld">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
                for (var i = 0; i < O.items.length; i++) {
                    str1 += '<tr><td>' + O.items[i].DepartureCityName + '-' + O.items[i].ArrivalCityName + '(' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + ')</td>'
                    str1 += '<td>' + BagInfo(O.items[i].BagInfo) + '</td></tr>';
                }
                str1 += '</table>';
                str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';

                str1 += '</div>';
            }
            if (R.items.length > 0) {
                str1 += '<div class="depcity1">';
                str1 += '<table class="w100 f12">';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<tr><td class="w50">' + R.items[j].DepartureCityName + '-' + R.items[j].ArrivalCityName + '(' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + ')</td>'
                    str1 += '<td>' + BagInfo(R.items[j].BagInfo) + '</td></tr>';
                }
                str1 += '</table>';

                str1 += '</div>';
            }
            //str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            //str1 += ' <div class"rgt" onclick="Close(\'' + this.rel + '_\');" >X</div
            $('#1api6ENRMLITZNRML_bag').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + this.rel + '_bag').show();
            // $('#' + this.id).toggleClass("fltDetailslink1");
            //$('#' + this).hide();
        }
    });

    $('.fareRuleToolTip').click(function (event) {

        var lineNums = $.trim(this.rel); //.split('_');

        var result = lineNums.indexOf("S") != -1 ? resulmain2 : resulmain;
        $('#1api6ENRMLITZNRML_canc').html("");
        lineNums = lineNums.replace("S", "");
        var lineNum = $.trim(this.rel).replace("S", "");
        //$('#' + lineNum[0] + '_').show();
        var Divhide = '<div>';
        //Divhide += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + lineNum[0] + '_\');" title="Click to close Details"><i class="fa fa-times-circle"></i></span><div></div>';
        $("#1api6ENRMLITZNRML_canc").html("<div align='center'><img alt='loading' width='50px' height='50px' src='../images/loadingAnim.gif'/></div>");
        Divhide += '</div>';
        if (lineNum[0] != null) {
            if (lineNum[1] == "O") {
                //$('#' + lineNum[0] + '_').slideDown();
            }
            if (lineNum[1] == "R") {
                //$('#' + lineNum[0] + '_RO').slideDown();
            }
            var fltSelectedArray;
            if (lineNum[1] == "R") {
                fltSelectedArray = JSLINQ(result[1])
                    .Where(function (item) { return item.LineNumber == lineNum[0]; })
                    .Select(function (item) { return item });
            }
            else {
                fltSelectedArray = JSLINQ(result[0])
                    .Where(function (item) { return item.LineNumber == lineNum[0]; })
                    .Select(function (item) { return item });
            }


            var NewArr = new Array(fltSelectedArray.items);

            if (fltSelectedArray.items.length > 0) {
                var n = UrlBase + "FareRuleService.asmx/GetFareRule";
                $.ajax({
                    url: n,
                    type: "POST",
                    data: JSON.stringify({
                        JsonArr: NewArr,
                        sno: fltSelectedArray.items[0].sno,
                        Provider: fltSelectedArray.items[0].Provider
                    }),
                    dataType: "json",
                    type: "POST",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {
                        var str1 = '<div>';
                        if (e.d.Response == null) {
                            str1 += '<div class=""><div></div>';
                            str1 += '<div>fare rule not available.</div>';
                            str1 += '</div>';
                        }

                        else {
                            str1 += '<div class=""><div></div>';
                            str1 += '<div>' + e.d.Response.FareRules[0].FareRuleDetail + '</div>'
                            str1 += '</div>';
                        }

                        $("#1api6ENRMLITZNRML_canc").html(str1);

                    },
                    error: function (e, t, n) {
                        alert("Please try after sometime!")
                        //$('#' + lineNum[0] + '_').hide();
                    }
                })

            }
            else {
                $("#1api6ENRMLITZNRML_canc").html("fare rule not available.");


            }
        }
    });



    //tjq(document).on("click", ".btnbookf", function () {


    //    if (window.confirm("Are you sure you want to continue with this flight?")) {



    //        var result = resulmain;
    //        ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

    //        tjq("#searchquery").hide();

    //        var lineNum = tjq.trim(tjq(this).attr("title"));
    //        var lineNo = tjq.trim(lineNum.split('api')[0])
    //        var fltSelectedArray = JSLINQ(result[0])
    //            .Where(function (item) { return item.LineNumber == lineNum || item.LineNumber == lineNo; })
    //            .Select(function (item) { return item });
    //        var arr = new Array(fltSelectedArray.items);
    //        var iscahem = arr[0][0].LineNumber;
    //        for (var i = 0; i < arr[0].length; i++) {
    //            arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber;
    //            arr[0][i].LineNumber = arr[0][i].LineNumber;


    //        }
    //        if (arr[0][0].Trip == "I") {
    //            var urlParams = new URLSearchParams(location.search);
    //            var NStop = urlParams.get('NStop');
    //            var compressedArr = (JSON.stringify(arr));
    //            var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails_LZCmp";
    //            tjq.ajax({
    //                url: t,
    //                type: "POST",

    //                data: JSON.stringify({
    //                    a: compressedArr
    //                }),
    //                dataType: "json",
    //                type: "POST",
    //                contentType: "application/json; charset=utf-8",
    //                success: function (e) {
    $(document.body).on('click', ".day", function (e) { if (!tjq(".evo-calendar").hasClass("sidebar-hide")) { tjq(".evo-calendar").addClass("sidebar-hide") } });

    //                    if (e.d.ChangeFareO.TrackId == "0") {
    //                        alert("Fare Changed Please Try again");

    //                    } else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
    //                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'I');
    //                    }

    //                    else if (NStop == "TRUE") {
    //                        window.location = UrlBase + "FlightInt/CustomerInfoFixDep.aspx?" + e.d.ChangeFareO.TrackId + ",I";
    //                    }
    //                    else {
    //                        window.location = UrlBase + "International/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId + ",I";
    //                    }
    //                },
    //                error: function (e, t, n) {

    //                    alert(t)
    //                    window.location = UrlBase + "Search.aspx";
    //                }
    //            })
    //        }
    //        else {
    //            var compressedArr = (JSON.stringify(arr));
    //            var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
    //            tjq.ajax({
    //                url: t,
    //                type: "POST",

    //                data: JSON.stringify({
    //                    a: compressedArr
    //                }),
    //                dataType: "json",
    //                type: "POST",
    //                contentType: "application/json; charset=utf-8",
    //                success: function (e) {
    //                    for (var i = 0; i < arr[0].length; i++) {
    //                        arr[0][i].LineNumber = iscahem;
    //                    }




    //                    if (e.d.ChangeFareO.TrackId == "0") {
    //                        var r = confirm("Fare changed Please Try again.");
    //                        if (r == true) {
    //                            location.reload();
    //                        }
    //                        tjq('#ConfmingFlight').hide();
    //                        tjq(document).ajaxStop(tjq.unblockUI)
    //                    }
    //                    else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
    //                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
    //                    }
    //                    else {

    //                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
    //                    }



    //                },
    //                error: function (e, t, n) {
    //                    for (var i = 0; i < arr[0].length; i++) {
    //                        arr[0][i].LineNumber = iscahem;
    //                    }
    //                    alert(t)
    //                    window.location = UrlBase + "Search.aspx";
    //                }
    //            })
    //        }
    //    }
    //});
});


function GetLayOver(items, position) {
    var t = this;
    var str1 = "";

    var layoverTime = calFlightDur(items[position - 1].ArrivalTime.replace(":", ""), items[position].DepartureTime.replace(":", ""));

    //str1 = '<div class="layover"> + <span> <i class="ico ico-time">&nbsp;</i> Layover: <strong> </strong> </span> <span class="pipe"> | </span> <time> Time: <strong></strong> </time>  </div>';
    str1 = '<div class="stm"><span class="lay">Layover:' + items[position].DepartureCityName + ' (' + items[position].DepartureLocation + ') - ' + layoverTime + '</span></div>';
    return str1;
};

function calFlightDur (departTime, arrivalTime) {
    var durH = parseInt(arrivalTime.slice(0, 2), 10) - parseInt(departTime.slice(0, 2), 10)
    if (durH < 0) {

        durH = durH + 24;
    }
    var durT = parseInt(arrivalTime.slice(2), 10) - parseInt(departTime.slice(2), 10)
    if (durT < 0) {
        durT = 60 + durT;
        durH = durH - 1;
    }

    var maxH = getFourDigitTime($.trim(durH.toString()));
    var maxT = getFourDigitTime($.trim(durT.toString()));
    return [maxH.slice(2), ":", maxT.slice(2)].join('');
};

function getFourDigitTime (val) {

    var val1;
    if (val.toString().length == 1) { val1 = "000" + val.toString(); }
    else if (val.toString().length == 2) { val1 = "00" + val.toString(); }
    else if (val.toString().length == 3) { val1 = "0" + val.toString(); }
    else { val1 = val; }

    return val1;

};

function abcdf(dc) {

    //import { json } from "modernizr";

    var defaultTheme = getRandom(4);

    var today = new Date();
    //var dc = jQuery("#ctl00_ContentPlaceHolder1_isdfsf").val();
    var events = {}; //jQuery.parseJSON(dc);


    var active_events = [];

    var week_date = [];

    var curAdd, curRmv;

    function getRandom(a) {
        return Math.floor(Math.random() * a);
    }

    function getWeeksInMonth(a, b) {
        var c = [], d = new Date(b, a, 1), e = new Date(b, a + 1, 0), f = e.getDate();
        var g = 1;
        var h = 7 - d.getDay();
        while (g <= f) {
            c.push({
                start: g,
                end: h
            });
            g = h + 1;
            h += 7;
            if (h > f) h = f;
        }
        return c;
    }

    week_date = getWeeksInMonth(today.getMonth(), today.getFullYear())[2];



    //var dc = jQuery("#ctl00_ContentPlaceHolder1_isdfsf").val();
    events = dc;// jQuery.parseJSON(dc);


    tjq("#demoEvoCalendar").evoCalendar({
        format: "MM dd, yyyy",
        titleFormat: "MM",
        calendarEvents: events,
    });
    tjq("[data-set-theme]").click(function (b) {
        a(b.target);
    });
    tjq("#addBtn").click(function (a) {
        curAdd = getRandom(events.length);
        tjq("#demoEvoCalendar").evoCalendar("addCalendarEvent", events[curAdd]);
        active_events.push(events[curAdd]);
        events.splice(curAdd, 1);
        if (0 === events.length) a.target.disabled = !0;
        if (active_events.length > 0) tjq("#removeBtn").prop("disabled", !1);
    });
    tjq("#removeBtn").click(function (a) {
        curRmv = getRandom(active_events.length);
        tjq("#demoEvoCalendar").evoCalendar("removeCalendarEvent", active_events[curRmv].id);
        events.push(active_events[curRmv]);
        active_events.splice(curRmv, 1);
        if (0 === active_events.length) a.target.disabled = !0;
        if (events.length > 0) tjq("#addBtn").prop("disabled", !1);
    });
    a(tjq("[data-set-theme]")[defaultTheme]);
    function a(a) {
        if (a != null) {
            var b = a.dataset.setTheme;
            tjq("[data-set-theme]").removeClass("active");
            tjq(a).addClass("active");
            tjq("#demoEvoCalendar").evoCalendar("setTheme", b);
        }

    }
    var b = getRandom(tjq("[data-settings]").length);
    var c = tjq("[data-settings]")[b];
    var d = getRandom(tjq("[data-method]").length);
    var e = tjq("[data-method]")[d];
    var f = getRandom(tjq("[data-event]").length);
    var g = tjq("[data-event]")[f];
    //showSettingsSample(tjq(c).data().settings);
    //showMethodSample(tjq(e).data().method);
    //showEventSample(tjq(g).data().event);
    tjq("[data-settings]").on("click", function (a) {
        var b = tjq(a.target).closest("[data-settings]");
        var c = b.data().settings;
        showSettingsSample(c);
    });
    tjq("[data-method]").on("click", function (a) {
        var b = tjq(a.target).closest("[data-method]");
        var c = b.data().method;
        showMethodSample(c);
    });
    tjq("[data-date-val").on("click", function (a) {
        tjq(".calendar-initialized").addClass("sidebar-hide");
    });
    tjq("[data-event]").on("click", function (a) {
        var b = tjq(a.target).closest("[data-event]");
        var c = b.data().event;
        showEventSample(c);
    });
    tjq("#waitMessageF").hide()
    //  window.location.();


    function showSettingsSample(a) {
        var b = tjq("#event-settings");
        var c;
        switch (a) {
            case "theme":
                c = '<br><span class="green">// theme</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'theme'</span>: <span class=\"red\">'Theme Name'</span><br>" + "});" + "<br> ";
                break;

            case "language":
                c = '<br><span class="green">// language</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'language'</span>: <span class=\"red\">'en'</span><br>" + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// Supported language: en, es, de..</span><br>' + "});" + "<br> ";
                break;

            case "format":
                c = '<br><span class="green">// format</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'format'</span>: <span class=\"red\">'MM dd, yyyy'</span><br>" + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// some browsers doesn\'t support other format, so...</span><br>' + "});" + "<br> ";
                break;

            case "titleFormat":
                c = '<br><span class="green">// titleFormat</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'titleFormat'</span>: <span class=\"red\">'MM'</span><br>" + "});" + "<br> ";
                break;

            case "eventHeaderFormat":
                c = '<br><span class="green">// eventHeaderFormat</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'eventHeaderFormat'</span>: <span class=\"red\">'MM dd'</span><br>" + "});" + "<br> ";
                break;

            case "firstDayOfWeek":
                c = '<br><span class="green">// firstDayOfWeek</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'firstDayOfWeek\'</span>: <span class="red">0</span> <span class="green">// Sun</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// 0-6 (Sun-Sat)</span><br>' + "});" + "<br> ";
                break;

            case "todayHighlight":
                c = '<br><span class="green">// todayHighlight</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'todayHighlight\'</span>: <span class="blue">true</span><br>' + "});" + "<br> ";
                break;

            case "sidebarDisplayDefault":
                c = '<br><span class="green">// sidebarDisplayDefault</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'sidebarDisplayDefault\'</span>: <span class="blue">false</span><br>' + "});" + "<br> ";
                break;

            case "sidebarToggler":
                c = '<br><span class="green">// sidebarToggler</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'sidebarToggler\'</span>: <span class="blue">false</span><br>' + "});" + "<br> ";
                break;

            case "eventDisplayDefault":
                c = '<br><span class="green">// eventDisplayDefault</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'eventDisplayDefault\'</span>: <span class="blue">false</span><br>' + "});" + "<br> ";
                break;

            case "eventListToggler":
                c = '<br><span class="green">// eventListToggler</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>({<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="violet">\'eventListToggler\'</span>: <span class="blue">false</span><br>' + "});" + "<br> ";
                break;

            case "calendarEvents":
                c = '<br><span class="green">// calendarEvents</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'calendarEvents\'</span>, {<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;<span class=\"violet\">'calendarEvents'</span>: [<br>" + "&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;{<br>" + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">id</span>: <span class="red">\'4hducye\'</span>, <span class="green">// Event\'s id (required, for removing event)</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">description</span>: <span class="red">\'Lorem ipsum dolor sit amet..\'</span>, <span class="green">// Description of event (optional)</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">badge</span>: <span class="red">\'1-day event\'</span>, <span class="green">// Event badge (optional)</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">date</span>: <span class="blue">new</span> <span class="yellow">Date</span>(), <span class="green">// Date of event</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">type</span>: <span class="red">\'holiday\'</span>, <span class="green">// Type of event (event|holiday|birthday)</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">color</span>: <span class="red">\'#63d867\'</span>, <span class="green">// Event custom color (optional)</span><br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">everyYear</span>: <span class="blue">true</span> <span class="green">// Event is every year (optional)</span><br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;}<br>" + "&#8194;&#8194;&#8194;&#8194;&#8194;]<br>" + "});" + "<br> ";
        }
        tjq("[data-settings]").removeClass("active");
        tjq('[data-settings="' + a + '"]').addClass("active");
        b.html(c);
    }

    function showMethodSample(a) {
        var b = tjq("#method-code");
        var c;
        switch (a) {
            case "setTheme":
                c = '<br><span class="green">// setTheme</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'setTheme\'</span>, <span class="red">\'Theme Name\'</span>);' + "<br> ";
                break;

            case "toggleSidebar":
                c = '<br><span class="green">// toggleSidebar</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'toggleSidebar\'</span>);' + "<br> " + '<br><span class="green">// open sidebar</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'toggleSidebar\'</span>, <span class="blue">true</span>);' + "<br> ";
                break;

            case "toggleEventList":
                c = '<br><span class="green">// toggleEventList</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'toggleEventList\'</span>);' + "<br> " + '<br><span class="green">// close event list</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'toggleEventList\'</span>, <span class="blue">false</span>);' + "<br> ";
                break;

            case "getActiveDate":
                c = '<br><span class="green">// getActiveDate</span><br>' + '<span class="red">var</span> <span class="orange">active_date</span> = tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'getActiveDate\'</span>);' + "<br> ";
                break;

            case "getActiveEvents":
                c = '<br><span class="green">// getActiveEvents</span><br>' + '<span class="red">var</span> <span class="orange">active_events</span> = tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'getActiveEvents\'</span>);' + "<br> ";
                break;

            case "selectYear":
                c = '<br><span class="green">// selectYear</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'selectYear\'</span>, <span class="red">2021</span>);' + "<br> ";
                break;

            case "selectMonth":
                c = '<br><span class="green">// selectMonth</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'selectMonth\'</span>, <span class="red">1</span>); <span class="green">// february</span>' + "<br> ";
                break;

            case "selectDate":
                c = '<br><span class="green">// selectDate</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'selectDate\'</span>, <span class="red">\'February 15, 2020\'</span>);' + "<br> ";
                break;

            case "addCalendarEvent":
                c = '<br><span class="green">// addCalendarEvent</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'addCalendarEvent\'</span>, {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">id</span>: <span class="red">\'kNybja6\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">name</span>: <span class="red">\'Mom\\\'s Birthday\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">description</span>: <span class="red">\'Lorem ipsum dolor sit..\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">date</span>: <span class="red">\'May 27, 2020\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">type</span>: <span class="red">\'birthday\'</span><br>' + "});" + '<br><span class="green">// add multiple events</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'addCalendarEvent\'</span>, [<br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;{<br>" + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">id</span>: <span class="red">\'kNybja6\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">name</span>: <span class="red">\'Mom\\\'s Birthday\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">date</span>: <span class="red">\'May 27, 1965\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">type</span>: <span class="red">\'birthday\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">everyYear</span>: <span class="blue">true</span> <span class="green">// optional</span><br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;},<br>" + "&#8194;&#8194;&#8194;&#8194;&#8194;{<br>" + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">id</span>: <span class="red">\'asDf87L\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">name</span>: <span class="red">\'Graduation Day!\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">date</span>: <span class="red">\'March 21, 2020\'</span>,<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;&#8194;<span class="blue">type</span>: <span class="red">\'event\'</span><br>' + "&#8194;&#8194;&#8194;&#8194;&#8194;}<br>" + "]);" + "<br> ";
                break;

            case "removeCalendarEvent":
                c = '<br><span class="green">// removeCalendarEvent</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'removeCalendarEvent\'</span>, <span class="red">\'kNybja6\'</span>);' + "<br> " + '<br><span class="green">// delete multiple event</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'removeCalendarEvent\'</span>, [<span class="red">\'kNybja6\'</span>, <span class="red">\'asDf87L\'</span>]);' + "<br> ";
                break;

            case "destroy":
                c = '<br><span class="green">// destroy</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">evoCalendar</span>(<span class="violet">\'destroy\'</span>);' + "<br> ";
        }
        tjq("[data-method]").removeClass("active");
        tjq('[data-method="' + a + '"]').addClass("active");
        b.html(c);
    }

    function showEventSample(a) {
        var b = tjq("#event-code");
        var c;
        switch (a) {
            case "selectDate":
                c = '<br><span class="green">// selectDate</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">on</span>(<span class="violet">\'selectDate\'</span>, <span class="blue">function</span>(<span class="yellow">event</span>, <span class="yellow">newDate</span>, <span class="yellow">oldDate</span>) {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// code here...</span><br>' + "});" + "<br> ";
                break;

            case "selectEvent":
                c = '<br><span class="green">// selectEvent</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">on</span>(<span class="violet">\'selectEvent\'</span>, <span class="blue">function</span>(<span class="yellow">event</span>, <span class="yellow">activeEvent</span>) {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// code here...</span><br>' + "});" + "<br> ";
                break;

            case "selectMonth":
                c = '<br><span class="green">// selectMonth</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">on</span>(<span class="violet">\'selectMonth\'</span>, <span class="blue">function</span>(<span class="yellow">event</span>, <span class="yellow">activeMonth</span>, <span class="yellow">monthIndex</span>) {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// code here...</span><br>' + "});" + "<br> ";
                break;

            case "selectYear":
                c = '<br><span class="green">// selectYear</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">on</span>(<span class="violet">\'selectYear\'</span>, <span class="blue">function</span>(<span class="yellow">event</span>, <span class="yellow">activeYear</span>) {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// code here...</span><br>' + "});" + "<br> ";
                break;

            case "destroy":
                c = '<br><span class="green">// destroy</span><br>' + 'tjq(<span class="red">\'#calendar\'</span>).<span class="yellow">on</span>(<span class="violet">\'destroy\'</span>, <span class="blue">function</span>(<span class="yellow">event</span>, <span class="yellow">evoCalendar</span>) {<br>' + '&#8194;&#8194;&#8194;&#8194;&#8194;<span class="green">// code here...</span><br>' + "});" + "<br> ";
        }
        tjq("[data-event]").removeClass("active");
        tjq('[data-event="' + a + '"]').addClass("active");
        b.html(c);
    }

    tjq('[data-go*="#"]').on("click", function (a) {
        a.preventDefault();
        var b = tjq(this).data().go;
        if ("#top" === b) {
            tjq("html, body").animate({
                scrollTop: 0
            }, 500);
            return;
        } else var c = tjq(b)[0].offsetTop - tjq("header")[0].offsetHeight - 10;
        tjq("html, body").animate({
            scrollTop: c
        }, 500);
    });
}

ChangedFarePopupShow = function (originalFare, updatedFare, TrackId, type, netFare, trip) {



    if (type == 'show') {
        var diff = '';
        if (parseFloat(updatedFare) > parseFloat(originalFare)) {
            diff = '(+) <img src="../Images/rsp.png" style="height: 10px;" /> ' + (parseFloat(updatedFare) - parseFloat(originalFare)) + '</div> '

        }
        else {
            diff = '(-) <img src="../Images/rsp.png" style="height: 10px;" /> ' + (parseFloat(originalFare) - parseFloat(updatedFare)) + '</div> '
        }

        var str = '<div class="f14 bld colorp">Oops! your selected flight fare has been changed';
        str += '</div><hr /> <div class="clear1"> </div><div class="w95 padding1 mauto bgpp f16">Updated fare is';
        str += ' <img src="../Images/rs.png" style="height: 12px; position:relative; top:1px;" /><span class="bld" id="spnupfare">' + updatedFare + '</span><span style="position:absolute; background:#f9f9f9; padding:5px; box-shadow:1px 2px 4px #888; margin-top:-5px;margin-left:10px;display:none;" id="divnetfare">NetFare: <img src="../Images/rs.png" style="height: 12px; position:relative; top:1px;" />' + netFare + '</span>';
        str += '</div><div class="clear1"></div><div class="clear1"></div><div class="bld w90 mauto">';
        str += ' <div class="w33 lft"> <div> Updated Fare</div> <div class="f14"> <img src="../Images/rs.png" style="height: 10px;" /> ' + updatedFare + '</div></div>';
        str += ' <div class="w33 lft"><div> Previous Fare</div><div class="f14"><img src="../Images/rs.png" style="height: 10px;" /> ' + originalFare + '</div>';
        str += '</div><div class="w33 lft colorp"><div> Fare Difference</div> <div class="f14">';
        str += diff + '</div> </div></div> <div class="clear1">';
        str += '</div><div class="clear1"> </div><div class="w95 rgt"> <ul><span class="bld">Reasons:</span>';
        str += '   <li>Airfares are dynamic and subject to change. This change is beyond our control.</li>';
        str += ' <li>The updated fare is the cheapest fare currently available on your selected flight.</li>';
        str += '</ul> </div> <div class="clear1"> </div><div class="textaligncenter">';
        str += '<input type="button" id="Cancelflt" name="Choose another flight" value="Choose another flight" class="button1" />';
        str += ' <input type="button" id="ContinueCflt" name="Continue" value="Continue" class="button1" style="margin-right:2px;" />';
        str += ' </div> <div class="clear1"></div>'
        tjq('#divLoadcf').html('');
        tjq('#divLoadcf').html(str);
        tjq('#divLoadcf').show();
        tjq('#Cancelflt').click(function () {
            tjq('#ConfmingFlight').hide();

        });
        tjq('#ContinueCflt').click(function () {
            debugger;
            if (TrackId == "") {
                tjq('#ConfmingFlight').hide();
            } else {

                if (tjq.trim(trip).toUpperCase() == "I") {
                    window.location = UrlBase + "International/PaxDetails.aspx?" + TrackId + ",I";
                }
                else {
                    window.location = UrlBase + "Domestic/PaxDetails.aspx?" + TrackId;
                }
            }

        });


        tjq('#spnupfare').mouseover(function () {
            tjq('#divnetfare').show();

        });


        tjq('#spnupfare').mouseout(function () {
            tjq('#divnetfare').hide();

        });
    }
    else if (type == 'hide') {


        var str1 = '';

        //str1 += ' <div class="clear1"> </div> ';
        //str1 += '<div class="w30 auto"  style="text-align: center; "><img alt="loading" width="50px" src="../images/loadingAnim.gif"/> </div> ';
        str1 += '<svg xmlns="http://www.w3.org/2000/svg" version="1.1">';
        str1 += '        <defs>';
        str1 += '            <filter id="gooey">';
        str1 += '                <feGaussianBlur in="SourceGraphic" stdDeviation="10" result="blur"></feGaussianBlur>';
        str1 += '                <feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 18 -7" result="goo"></feColorMatrix>';
        str1 += '                <feBlend in="SourceGraphic" in2="goo"></feBlend>';
        str1 += '            </filter>';
        str1 += '        </defs>';
        str1 += '</svg>';


        str1 += '<div class="loading" >';
        str1 += '   <p style="position: absolute;left: 0;bottom: 220px;text-align: center;width: 100%;color:#fff;">Please wait</p>';
        str1 += '   <span><i></i><i></i></span>';
        str1 += '</div>';

        tjq('#divLoadcf').html('');
        tjq('#divLoadcf').html(str1);
        tjq('#divLoadcf').show();
    }
    tjq("#ConfmingFlight").show();


};


$(function () {
    $('.confirm').click(function (e) {
        e.preventDefault();
        if (window.confirm("Are you sure?")) {
            location.href = this.href;
        }
    });
});

 function CreateFareBreakUp(e) {
    //var t = this;
    var n = e.AdtTax - e.AdtFSur;
    var r = e.AdtFare;
    var i = e.ChdTax - e.ChdFSur;
    var s = e.ChdFare;
    var o = e.InfTax - e.InfFSur;
    var u = e.InfFare;
   // var pmf = t.DisplayPromotionalFare(e);
    //var a = e.TotMrkUp;
    var a;
    if (e.AdtFareType == 'Special Fare') { a = (e.ADTAgentMrk * e.Adult) + (e.CHDAgentMrk * e.Child); }
    else { a = e.TotMrkUp; }
    var f = e.TotCB;
    var l = e.TotTds;
    var aa = parseInt(a / (e.Adult + e.Child));
    strResult = '<div><span onclick="Close(\'' + e.LineNumber + '_\');" title="Click to close Details"></span></div>'
    strResult = strResult + "<div class='new-fare-details ' id='FareBreak'>"
    strResult = strResult + "<div class='new-fare-d-1'>"
    strResult = strResult + "<div class='nw-b2b-fared'>"
    strResult = strResult + "<div class='nw-pad-b2'>"
    strResult = strResult + "<div class='nw-far-1'><span>" + e.Adult + " </span>x Adult</div>"
    strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.AdtBfare + "</span></div>"
    strResult = strResult + "</div>";
    strResult = strResult + "</div>";


    if (e.Child > 0) {
   

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>" + e.Child + " </span>x Child</div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.ChdBFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";


    }
    if (e.Infant > 0) {
 

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>" + e.Infant + " </span>x Infant</div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.InfBfare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";


    }
 
    if (e.IsCorp == true) {
      
        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Total Taxes & Fees</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Gross Total</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Net Fare</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.NetFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



    }
    else {



        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Total Taxes & Fees</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalTax + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";
        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Gross Total</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Net Fare</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.NetFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



    }
 



    strResult = strResult + "</div>";

    //Tax
    strResult = strResult + "<div class='can-bnew-rg'>";
    strResult = strResult + "<div class='lef-b2b-cane'>"
    strResult = strResult + "<div class='b2b-ca-char'>Tax & Other Fees</div>"
    strResult = strResult + " <table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>"
    strResult = strResult + "<tbody>"


    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>SGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>CGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>IGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>Fuel Surcharge</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ " + e.TotalFuelSur + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>Transaction Charge</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "</tbody>"
    strResult = strResult + "</table>"
    strResult = strResult + "</div>"

    //Other Charges
    strResult = strResult + "<div class='rig-b2b-cane'>"
    strResult = strResult + "<div class='b2b-ca-char'>Commission</div>"
    strResult = strResult + "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>"
    strResult = strResult + "<tbody>"
    strResult = strResult + "<td scope='row' width='50%'><span>Commission</span></td>"
    strResult = strResult + " <td width='50%'>"
    strResult = strResult + "<span>₹ " + e.TotDis + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"
    strResult = strResult + "<td scope='row' width='50%'><span>TDS</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ " + l + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"


    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>CGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>SGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>IGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>Transaction Charge</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"
    strResult = strResult + "</tbody>"
    strResult = strResult + "</table>"
    strResult = strResult + "</div>"

    strResult = strResult + "<div class='tr-b2b-cancgr'>"
    strResult = strResult + "<div class='trm-had'>Terms &amp; Conditions</div>"
    strResult = strResult + "<div class='terms-b2b2-cancahe'>"
    strResult = strResult + "<ul>"
    strResult = strResult + "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>"
    strResult = strResult + "<li>The charges will be on per passenger per sector</li>"
    strResult = strResult + "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>"
    strResult = strResult + "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>"
    strResult = strResult + "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>"
    strResult = strResult + "<li>For infants there is no baggage allowance</li>"
    strResult = strResult + "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>"
    strResult = strResult + "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>"
    strResult = strResult + "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>"
    strResult = strResult + " </ul>"
    strResult = strResult + "</div>"
    strResult = strResult + "</div>"
    //strResult = strResult + ""
    //strResult = strResult + ""


    strResult = strResult + "</div>";
    strResult = strResult + "</div>";
    strResult = strResult + "<div class='clear'></div>";
    strResult = strResult + "<div class='clear'></div>";
    return strResult
};
function MakeupTotDur (totDur) {

    var tdur = "";
    if ($.trim(totDur) != "") {
        if ($.trim(totDur).search("hrs") > 0) {
            var hrsmin;

            tdur = $.trim(totDur).replace(" hrs", ":").replace(" min", "");
            hrsmin = tdur.split(":");
            tdur = ($.trim(hrsmin[0]).length > 1 ? hrsmin[0] : "0" + hrsmin[0]) + ":" + ($.trim(hrsmin[1]).length > 1 ? $.trim(hrsmin[1]) : "0" + $.trim(hrsmin[1]))

        }
        else if ($.trim(totDur).search(":") > 0) {
            tdur = $.trim(totDur); //.replace(/\s/g, '');
        }
        else {


            if (parseInt(totDur) < 60) {

                tdur = "00:" + ($.trim(totDur).length > 1 ? $.trim(totDur) : "0" + $.trim(totDur));
            }
            else {
                var hrs = $.trim(parseInt(parseInt(totDur) / 60).toString());
                var rmin = $.trim(parseInt(parseInt(totDur) % 60).toString());

                tdur = (hrs.length > 1 ? hrs : "0" + hrs) + ":" + ($.trim(rmin).length > 1 ? rmin : "0" + rmin);
            }


        }
    }


    return tdur;
};
function BagInfo (bag) {

    
    if (bag != null && bag.toString() != '') {

        var rs = '';
        if ($.trim(bag).toUpperCase().search('PC') > 0) {
            rs = $.trim(bag).replace('PC', ' Piece(s) Baggage included.');
        }
        else if (($.trim(bag).toUpperCase().search('K') > 0) && ($.trim(bag).toString().length < 4)) {
            rs = $.trim(bag).replace('K', ' Kg Baggage included.');
        }
        else {
            rs = bag;
        }
    }
    else {
        rs = "Baggage information not available."
    }
    return rs;
}

function createLayout(resflt) {
    var result = resulmain2;
    var ddsdsd = $("#pass-pax").val();
    var stdd = $.parseJSON(ddsdsd);
    var lineNum = stdd.dataloopcount;
    var lineNo = tjq.trim(lineNum.split('api')[0])
    var fltSelectedArray = JSLINQ(result[0])
        .Where(function (item) { return parseFloat(item.TotalFare) == parseFloat(stdd.Amt) && parseFloat(item.AvailableSeats1) == parseFloat(stdd.Avls.replace("Seat Left : ","")) && item.ValiDatingCarrier==stdd.datastrcarrier.replace("(","").replace(")","").trim(); })
        .Select(function (item) { return item });
    var arr = new Array(fltSelectedArray.items);
    var iscahem = arr[0][0].LineNumber;
    for (var i = 0; i < arr[0].length; i++) {
        arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber;
        arr[0][i].LineNumber = arr[0][i].LineNumber;


    }
    var e = arr[0][0];
    
    var srla = "<div class='event-list' style='margin-top: 29px;'><div class='event-container'><div class='row'  style='font-size: 10px;font-weight: 100;width: 100%;padding-top: 8px; margin-left: 2px;'>";
    srla += "<div class='col-md-3 col-xs-3'><img alt='' src='../Airlogo/sm" + e.MarketingCarrier + ".gif'  style='width:25px;border-radius:50%;'/><p class='flight-ident'>" + e.ValiDatingCarrier + " - " + e.FlightIdentification + "</p> <p class='flight-ident'>" + e.AirLineName + "</p><p class='flight-ident'> Class - " + e.RBD + " </p></div>";
    srla += "<div class='col-md-3 col-xs-3'><p class='dep-loc' style='font-size: 20px;'>" + e.DepartureLocation + "<span class='dep-time' style='font-size: 20px;'><p>" + e.Departure_Date + "</p>" + e.DepartureTime + "</span></p><p style='font-size: 16px;'>" + e.Stops + "</p> </div>";
    srla += "<div class='col-md-3 col-xs-3'> <p class='arr-loc' style='font-size: 20px;'> " + e.ArrivalLocation + " <span class='arr-time' style='font-size: 20px;'><p>" + e.Arrival_Date + "</p>" + e.ArrivalTime + " </span></p><p style='font-size: 16px;'>" + e.TotDur + "</p> </div>";
    var Refd = e.AdtFareType.trim().toLowerCase() == "non refundable" ? "NR" : "R";

    if (e.FareDet != "") {
        srla += "<div class='col-md-3 col-xs-3'> <p class='tot-fair'>" + e.FareDet + e.TotalFare + " </p>  <p class='atr btnbookf' title='" + e.LineNumber + "'>Book</p></div>";  //" + e.AdtFar + "
    }
    else {
        srla += "<div class='col-md-3 col-xs-3'> <p class='tot-fair'>₹ " + e.TotalFare + " </p>  <p class='atr btnbookf' title='" + e.LineNumber + "'>Book</p></div>";  //" + e.AdtFar + "
    }
    srla += "<div class='flt-information2'><span class='f-type' title='" + e.AdtFareType + "'>" + Refd + "</span>  <i class='icofont-long-arrow-right'></i> <span><i title='Available Seat'><img src='/Advance_CSS/FARECAL/js/seat.png' style='width:20px;'/></i> " + e.AvailableSeats1 + "</span> <i class='icofont-long-arrow-right'></i> <span><i class='icofont-info-circle flt-info2' title='Flight Details' id ='flt-info' rel=' " + e.LineNumber + "'></i></span></div>";
    srla += "</div></div></div>";
    srla += "<div class='col-md-12' style='margin-top:10px;'></div>";
    srla += "<div class='row'><div class='col-md-3'><a id='acgggh' type='button' > Add Passenger <i class='icofont-dotted-down'></i></a></div>";
    srla += "<div class='col-md-6'></div>";
    srla += "<div class='col-md-3 '><button id='acggghSearch' class='hide' type='button'  rel=' " + e.LineNumber + "'> Search </button></div></div>";
    srla += "";

    $("#fltpx").html(srla);
}

function createLayout2(resflt) {
    var result = resulmain2;
    var fltSelectedArray = JSLINQ(result[0])
        //.Where(function (item) { return parseFloat(item.TotalFare) == parseFloat(stdd.Amt) && parseFloat(item.AvailableSeats1) == parseFloat(stdd.Avls.replace("Seat Left : ", "")) && item.ValiDatingCarrier == stdd.datastrcarrier.replace("(", "").replace(")", "").trim(); })
        .Select(function (item) { return item });
    var arr = new Array(fltSelectedArray.items);
    var srla = "<div style='overflow-x: hidden;overflow-y: scroll;height:220px;'>";
    for (var SSS = 0; SSS < arr[0].length; SSS++) {
        var e = arr[0][SSS];
        srla += "<div class='event-list'><div class='event-container'>";
        srla += "<div class='row'  style='font-size: 10px;font-weight: 100;width: 100%;padding-top: 8px; margin-left: 2px;'>";
        srla += "<div class='col-md-3 col-xs-3'><img alt='' src='../Airlogo/sm" + e.MarketingCarrier + ".gif'  style='width:25px;'/><p class='flight-ident'>" + e.ValiDatingCarrier + " - " + e.FlightIdentification + "</p> <p class='flight-ident'>" + e.AirLineName + "</p><p class='flight-ident'> Class - " + e.RBD + " </p></div>";
        srla += "<div class='col-md-3 col-xs-3'><p class='dep-loc' style='font-size: 20px;'>" + e.DepartureLocation + " <span class='dep-time' style='font-size: 20px;'><p>" + e.Departure_Date + "</p>" + e.DepartureTime + "</span></p><p style='font-size: 16px;'>" + e.Stops + "</p> </div>";
        srla += "<div class='col-md-3 col-xs-3'> <p class='arr-loc' style='font-size: 20px;'> " + e.ArrivalLocation + " <span class='arr-time' style='font-size: 20px;'><p>" + e.Arrival_Date + "</p>" + e.ArrivalTime + " </span></p><p style='font-size: 16px;'>" + e.TotDur + "</p> </div>";
        var Refd = e.AdtFareType.trim().toLowerCase() == "non refundable" ? "NR" : "R";

        if (e.FareDet != "") {
            srla += "<div class='col-md-3 col-xs-3'> <p class='tot-fair'>" + e.FareDet + e.TotalFare + " </p>  <p class='atr btnbookf' title='" + e.LineNumber + "'>Book</p></div>";  //" + e.AdtFar + "
        }
        else {
            srla += "<div class='col-md-3 col-xs-3'> <p class='tot-fair'>₹ " + e.TotalFare + " </p>  <p class='atr btnbookf' title='" + e.LineNumber + "'>Book</p></div>";  //" + e.AdtFar + "
        }
        srla += "<div class='flt-information2'><span class='f-type' title='" + e.AdtFareType + "'>" + Refd + "</span>  <i class='icofont-long-arrow-right'></i> <span><i title='Available Seat'><img src='/Advance_CSS/FARECAL/js/seat.png' style='width:20px;'/></i> " + e.AvailableSeats1 + "</span> <i class='icofont-long-arrow-right'></i> <span><i class='icofont-info-circle flt-info2' title='Flight Details' id ='flt-info' rel=' " + e.LineNumber + "'></i></span></div>";
        srla += "</div></div></div>";
        srla += "<div class='clear'></div>";
    }
    srla += "</div>";
    srla += "<div class='col-md-12' style='margin-top:10px;'></div>";
    srla += "<div class='row'><div class='col-md-3'><a id='acgggh' type='button' > Add Passenger's <i class='icofont-dotted-down'></i></a></div>";
    srla += "<div class='col-md-6'></div>";
    srla += "<div class='col-md-3 '><button id='acggghSearch' class='hide' type='button' > Search </button></div></div>";
    srla += "";
    $("#fltpx").html(srla);
}
