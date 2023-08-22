var cancelpnrhandler;
var popupStatus = 0;
var CalcelSegArray = new Array();
var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
$(document).ready(function () {
    cancelpnrhandler = new cancelpnrhelper();
    cancelpnrhandler.BindEvents();

});
var cancelpnrhelper = function () {
    this.btnContinue = $(".btnContinue");
  
}

cancelpnrhelper.prototype.BindEvents = function () {
    var h = this;
    h.btnContinue.click(function () {
        debugger;
        var orderid = $(this).attr('orderid');
        BlockUIFC();
        $.ajax({
            url: UrlBase + "PNRCancellation.asmx/GetCancellationSegmentDetails",
            data: "{'OrderID':'" + $.trim(orderid) + "','PaxID':'" + '0' + "','Opr':'" + 'GET' + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger;
                var myObj = JSON.parse(data.d);
               
                
               
                
                Layout(myObj, orderid);
                $.unblockUI();
            },
            error: function (e, t, n) {
                alert("Could not fetch data. Please try again.");
                $.unblockUI();

            }
        });
    });
    $("#SourceView").delegate(".selectedPaxSeg", "click", function () {
        debugger;
        if ($("#" + $(this).attr('id')).is(':checked')) {
            if (!$('.cls' + $(this).attr('root')).is(':disabled')) {
                debugger;
                var clsFlt = 'cls' + $(this).attr('root');
                var clsroot = 'cls' + $(this).attr('FltId');
                var orderid = $.trim($("#OrderID").val());
                var segment = $("#" + $(this).attr('id')).attr("root");
                var paxid = '0'; // $("#" + $(this).attr('id')).attr("paxid");
                BlockUIFC();
                $.ajax({
                    url: UrlBase + "PNRCancellation.asmx/GetTicketStatusBySegment",
                    data: "{'orderid':'" + $.trim(orderid) + "','segment':'" + $.trim(segment) + "','paxid':'" + $.trim(paxid) + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        debugger;
                        try {
                            Jsondata = JSON.parse(data.d);
                            var anyMsg = Jsondata;
                            if (anyMsg != null) {
                                if (anyMsg != "") {
                                    if ($.trim(anyMsg.split('_')[0]) != "0") {
                                        if ($.trim(anyMsg.split('_')[0]) == "5" || $.trim(anyMsg.split('_')[0]) == "6") {
                                            alert($.trim(anyMsg.split('_')[1]));
                                            $('.' + clsroot).prop("checked", false);
                                        }
                                        else {
                                            $('.' + clsFlt).prop("checked", true);
                                            $('.' + clsroot).prop("checked", true);
                                        }
                                    }
                                    else {
                                        $('.' + clsFlt).prop("checked", true);
                                        $('.' + clsroot).prop("checked", true);
                                    }
                                }
                            }
                            $.unblockUI();
                        }
                        catch (err) {
                            alert(err);
                            $.unblockUI();
                        }
                        $("#btn_confirm").trigger("click");
                    },
                    error: function (e, t, n) {
                        debugger;
                        alert(t + ',' + n);
                        $.unblockUI();
                    }
                });

            }
        }
        else {
            $('.cls' + $(this).attr('FltId')).prop("checked", false);
            $('.cls' + $(this).attr('root')).prop("checked", false);
            $("#btn_confirm").trigger("click");
        }
    });
    $("#SourceView").delegate("#btn_confirm", "click", function () {
        debugger;
        CalcelSegArray = new Array();
        $('input:checkbox.selectedPaxSeg').each(function () {
            if (this.checked)
                CalcelSegArray.push({ "PaxId": $(this).attr('paxid'), "FltId": $(this).attr('fltid'), "PaxTitle": $(this).attr('ttile'), "PaxFName": $(this).attr('fname'), "PaxLName": $(this).attr('lname'), "Sector": $(this).attr('sector'), "Segment": $(this).attr('segment'), "TicketNumber": $(this).attr('ticketnumber'), "DeptDate": $(this).attr('deptdate'), "DeptTime": $(this).attr('depttime'), "ArrDate": $(this).attr('arrdate'), "ArrTime": $(this).attr('arrtime'), "Class": $(this).attr('cls'), "VC": $(this).attr('vc'), "Provider": $(this).attr('Provider'), "root": $(this).attr('root'), "Key": $(this).attr('Key') });
        });
        var segDetails = "";
        var dt;
        var at;
        if (CalcelSegArray.length > 0) {

            segDetails += '<div class="w100 border p10 clear1" style="font-size: 12px; line-height: 18px;">'
            segDetails += '<div class="tital">Selected Segment/Pax</div>';
            segDetails += '<div class="w25 lft bld">Name</div>'
            segDetails += '<div class="w15 lft bld">Segment</div>'
            segDetails += '<div class="w15 lft bld">Sector</div>'
            segDetails += '<div class="w15 lft bld">Ticket Number</div>'
            segDetails += '<div class="w15 lft bld">Dept Date & Time</div>'
            segDetails += '<div class="w15 lft bld">Arr Date & Time</div>'
            //segDetails += '<div class="w10 lft bld">Class</div>'
            segDetails += '<div class="clear"></div>';
            for (var i = 0; i < CalcelSegArray.length; i++) {
                // Code to change DeptDate and ArrDate from ddmmyy to dd MMM yy start
                if (CalcelSegArray[i].DeptDate.length == 6) {
                    var dt1 = new Date(CalcelSegArray[i].DeptDate.substr(4, 2), parseInt(CalcelSegArray[i].DeptDate.substr(2, 2)) - 1, CalcelSegArray[i].DeptDate.substr(0, 2));
                    dt = dt1.getDate() + ' ' + months[dt1.getMonth()] + ' ' + dt1.getYear();
                }
                else {
                    dt = CalcelSegArray[i].DeptDate;
                }
                if (CalcelSegArray[i].ArrDate.length == 6) {
                    var dt1 = new Date(CalcelSegArray[i].ArrDate.substr(4, 2), parseInt(CalcelSegArray[i].ArrDate.substr(2, 2)) - 1, CalcelSegArray[i].ArrDate.substr(0, 2));
                    at = dt1.getDate() + ' ' + months[dt1.getMonth()] + ' ' + dt1.getYear();
                }
                else {
                    at = CalcelSegArray[i].ArrDate;
                }
                // Code to change DeptDate and ArrDate from ddmmyy to dd MMM yy end
                segDetails += '<div class="w25 lft">' + CalcelSegArray[i].PaxTitle + ' ' + CalcelSegArray[i].PaxFName + ' ' + CalcelSegArray[i].PaxLName + '</div>'
                segDetails += '<div class="w15 lft">' + CalcelSegArray[i].Segment + '</div>'
                segDetails += '<div class="w15 lft">' + CalcelSegArray[i].Sector + '</div>'
                segDetails += '<div class="w15 lft">' + CalcelSegArray[i].TicketNumber + '</div>'
                segDetails += '<div class="w15 lft">' + dt + ' ' + CalcelSegArray[i].DeptTime + '</div>'
                segDetails += '<div class="w15 lft">' + at + ' ' + CalcelSegArray[i].ArrTime + '</div>'
                //segDetails += '<div class="w10 lft">' + CalcelSegArray[i].Class + '</div>'
                segDetails += '<div class="clear"></div>';
            }
            segDetails += '<div class="clear1"></div> ';
            //segDetails += '<div class="w10 lft">Remark</div>';
            //segDetails += '<div><textarea name="CancelRemark" id="CancelRemark" rows="3" cols="70"></textarea></div>'
            //segDetails += '<div class="clear1"></div> ';
            segDetails += '<div><input style="float:right;" type="button" id="btnCancel" value="Cancel" /></div> ';
            segDetails += '<div><input type="button" id="btnCheckRefundAmt" value="Check Refund Amount" /></div> ';
            segDetails += '<div>';
        }
        debugger;
        var json = JSON.stringify(CalcelSegArray);
        $("#selectedSegment").empty();
        $("#selectedSegmentDetails").empty();
        $("#selectedSegment").val(json);
        $("#selectedSegmentDetails").append(segDetails);
        //selectedSegment
        debugger;
        //disablePopup();
    });
    $("#SourceView").delegate("#btnCancel", "click", function () {
       
        debugger;
        {
            var ans = confirm("Would you like to cancel?");
            if (ans) {
                $("#CancelRemark").val($.trim($("textarea#CancelRemark").val()))
                //h.formTktCanc.submit();
                BlockUIFC();
                $.ajax({
                    url: UrlBase + "PNRCancellation.asmx/CancelPNR",
                    data: "{'objPCancDetails':'" + $("#objModel").val() + "','selectedSegment':'" + $("#selectedSegment").val() + "','CancelRemark':'" + $("#CancelRemark").val() + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        debugger;
                        Jsondata = JSON.parse(data.d);
                        alert(Jsondata);
                        $.unblockUI();
                        disablePopup();
                        location.reload();
                    },
                    error: function (e, t, n) {
                        alert("Some error occure. Please try again.")
                        $.unblockUI();
                        disablePopup();
                        location.reload();
                    }
                });
            }
        }

    });
    $("#SourceView").delegate("#btnCheckRefundAmt", "click", function () {

        debugger;
        {
            var ans = true;
            if (ans) {
                BlockUIFC();
                $.ajax({
                    url: UrlBase + "PNRCancellation.asmx/CheckCancelAmount",
                    data: "{'objPCancDetails':'" + $("#objModel").val() + "','selectedSegment':'" + $("#selectedSegment").val() + "','CancelRemark':'" + $("#CancelRemark").val() + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        debugger;
                        Jsondata = JSON.parse(data.d);
                        alert(Jsondata);
                        $.unblockUI();
                        
                        //disablePopup();

                    },
                    error: function (e, t, n) {
                        alert("Some error occure. Please try again.")
                        $.unblockUI();
                        //disablePopup();
                    }
                });
            }
        }

    });
}

function Layout(d, OrderID) { //PaxId
    debugger;
    var splnote = "false";
    var res = "";
    var dt;
    var at;
    try {

        $("#SourceView").empty();
        //res += '<div class="w80 rgt">'
        if (d.length > 0 && (d[0].Error == "" || d[0].Error == null)) {
            splnote = "false";

            if (d[0].lstPaxSeg[0].VC.toUpperCase() == "SG" && d[0].lstPaxSeg[0].Class.toUpperCase() != "BUSINESS") {
                splnote = "true";
            }
            res += '<div class="hidtriptype hide">o</div>';
            //res += '<div class="w40 lft textheads"><a href="#" style="float:right;" data-toggle="modal" data-target="#fareRuleDtls" splnote="' + splnote + '" alt="' + OrderID + '" num="0" pvdr="' + d[0].lstPaxSeg[0].Provider + '" onclick="getFareRule(this,\'' + d[0].lstPaxSeg[0].Provider + '\');"><i class="fa fa-inr" aria-hidden="true"></i> FareRule</a></div> '

            res += '<div class="rgt vListContainerCancel"  style="border-top: 1px solid #e9e9e9; width:100%;  ">';
            res += '<div class="w20 lft" style="border-right: 1px solid #e9e9e9;height:250px;">';

            for (var pax = 0; pax < d.length ; pax++) {
                res += '<div class="w100 lftgreen lft"  >';
                if (pax == 0)
                    res += '<div class="clspaxdtls paxselected" id="Traveller' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d[pax].lstPaxSeg[0].PaxFName + '" Lname="' + d[pax].lstPaxSeg[0].PaxLName + '"   Title="' + d[pax].lstPaxSeg[0].PaxTitle + '" PaxId="' + d[pax].PaxId + '">' + d[pax].lstPaxSeg[0].PaxTitle + ' ' + d[pax].lstPaxSeg[0].PaxFName + ' ' + d[pax].lstPaxSeg[0].PaxLName + ' <span class="spntrvl" id="spnTraveller' + pax + '"></div>';

                else
                    res += '<div class="clspaxdtls" id="trvl' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d[pax].lstPaxSeg[0].PaxFName + '" Lname="' + d[pax].lstPaxSeg[0].PaxLName + '"   Title="' + d[pax].lstPaxSeg[0].PaxTitle + '" PaxId="' + d[pax].PaxId + '">' + d[pax].lstPaxSeg[0].PaxTitle + ' ' + d[pax].lstPaxSeg[0].PaxFName + ' ' + d[pax].lstPaxSeg[0].PaxLName + ' <span class="spntrvl" id="spnTraveller' + pax + '"></span></div>';
                res += '</div>';
                res += '<div class="clear"></div>';
            }

            res += '</div>';
            res += '<div class="w75 lft p10">';
            res += '<div class="w100 lft texthead hide">Select segment(s) to cancel for <span id="selectedtrvl"></span> </div>'
            res += '<div class="clear"></div>';

            for (var i = 0; i < d.length; i++) {

                res += '<div style="height: 200px; width: 100%; overflow-x: scroll; overflow-y: hidden;display:none; text-align: center;" class="seglayout" id="segment' + d[i].PaxId + '">';
                if (d[i].lstPaxSeg[0].Provider == "1G" && d[i].lstPaxSeg[0].TicketNumber != "") {
                    res += d[i].lstPaxSeg[0].TicketNumber;
                }
                res += '<table width="100%" style=" line-height: 27px;">';
                res += '<tr class="bld"><td>Select</td><td>Sector</td><td>Flight</td><td>Dept Date & Time</td>';//<td>Class</td>
                if (d[i].lstPaxSeg[0].Provider == "1G") {
                    res += '<td>F/B</td>';
                }
                res += '<td>Status</td></tr>';
                for (var j = 0; j < d[i].lstPaxSeg.length; j++) {
                    res += '<tr>';

                    debugger;
                    if (($.trim(d[i].lstPaxSeg[j].CancelStatus) == $.trim("") || $.trim(d[i].lstPaxSeg[j].CancelStatus) == $.trim("REJECTED") || $.trim(d[i].lstPaxSeg[j].CancelStatus) == $.trim("PRECAN") || $.trim(d[i].lstPaxSeg[j].CancelStatus) == $.trim("FAILED")) && ($.trim(d[i].lstPaxSeg[j].ReissueStatus) == $.trim("REJECTED") || $.trim(d[i].lstPaxSeg[j].ReissueStatus) == "" || $.trim(d[i].lstPaxSeg[j].ReissueStatus) == $.trim("Ticketed")))
                        res += '<td><input type="checkbox"';
                    else
                        res += '<td><input disabled type="checkbox"';

                    res += ' PaxId="' + d[i].PaxId + '" title="' + d[i].PaxId + '" id="' + d[i].PaxId + d[i].lstPaxSeg[j].FltId + '" class="selectedPaxSeg cls' + d[i].lstPaxSeg[j].FltId + ' cls' + d[i].lstPaxSeg[j].Segment.replace(':', '') + ' cls' + d[i].lstPaxSeg[j].root + '" FltId="' + d[i].lstPaxSeg[j].FltId + '" Sector="' + d[i].lstPaxSeg[j].Sector + '" segment="' + d[i].lstPaxSeg[j].Segment + '" ttile="' + d[i].lstPaxSeg[j].PaxTitle + '" fname="' + d[i].lstPaxSeg[j].PaxFName + '" lname="' + d[i].lstPaxSeg[j].PaxLName + '" ticketnumber="' + d[i].lstPaxSeg[j].TicketNumber + '" deptdate="' + d[i].lstPaxSeg[j].DeptDate + '" depttime="' + d[i].lstPaxSeg[j].DeptTime + '" arrdate="' + d[i].lstPaxSeg[j].ArrDate + '" arrtime="' + d[i].lstPaxSeg[j].ArrTime + '" cls="' + d[i].lstPaxSeg[j].Class + '" vc="' + d[i].lstPaxSeg[j].VC + '" root="' + d[i].lstPaxSeg[j].root + '" Provider="' + d[i].lstPaxSeg[j].Provider + '" Key="' + d[i].lstPaxSeg[j].Key + '"></td>';
                    if (d[i].lstPaxSeg[j].DeptDate.length == 6) {

                        var dt1 = new Date(d[i].lstPaxSeg[j].DeptDate.substr(4, 2), parseInt(d[i].lstPaxSeg[j].DeptDate.substr(2, 2)) - 1, d[i].lstPaxSeg[j].DeptDate.substr(0, 2));
                        dt = dt1.getDate() + ' ' + months[dt1.getMonth()] + ' ' + dt1.getYear();
                    }
                    else {
                        dt = d[i].lstPaxSeg[j].DeptDate;
                    }
                    if (d[i].lstPaxSeg[j].ArrDate.length == 6) {

                        var dt1 = new Date(d[i].lstPaxSeg[j].ArrDate.substr(4, 2), parseInt(d[i].lstPaxSeg[j].ArrDate.substr(2, 2)) - 1, d[i].lstPaxSeg[j].ArrDate.substr(0, 2));
                        at = dt1.getDate() + ' ' + months[dt1.getMonth()] + ' ' + dt1.getYear();
                    }
                    else {
                        at = d[i].lstPaxSeg[j].ArrDate;
                    }
                    // res += '<td>' + d[i].lstPaxSeg[j].Segment + '</td>'
                    res += '<td>' + d[i].lstPaxSeg[j].Sector + '</td>'
                    res += '<td>' + d[i].lstPaxSeg[j].VC + '/' + d[i].lstPaxSeg[j].FltNo + '</td>'
                    res += '<td>' + dt + ' ' + d[i].lstPaxSeg[j].DeptTime + '</td>'
                    //res += '<td>' + at + ' ' + d[i].lstPaxSeg[j].ArrTime + '</td>'
                    //res += '<td>' + d[i].lstPaxSeg[j].Class + '</td>'
                    if (d[i].lstPaxSeg[j].Provider == "1G") {
                        res += '<td>' + d[i].lstPaxSeg[j].ADTFareBasis + '</td>'
                    }
                    if (d[i].lstPaxSeg[j].SegmentStatus == "Live") {
                        if (d[i].lstPaxSeg[j].ReissueStatus != "")
                            res += '<td>Reissue: ' + d[i].lstPaxSeg[j].ReissueStatus + '</td>';
                        else
                            res += '<td>BOOKED</td>';
                    }
                    else {

                        if (d[i].lstPaxSeg[j].CancelStatus != "")
                            res += '<td>' + d[i].lstPaxSeg[j].CancelStatus + '</td>';
                        else if (d[i].lstPaxSeg[j].ReissueStatus != "")
                            res += '<td>Reissue: ' + d[i].lstPaxSeg[j].ReissueStatus + '</td>';
                        else
                            res += '<td>Not allowed</td>';
                    }
                    res += '</tr>';
                }
                res += '</table>';
                res += '</div>';
            }

            res += '</div>';


            // res += '<div class="w75 rgt"><ul class="pricetbs"> <li> <a class="PRICE_0"></a>Rs 0</li><li><a class="PRICE_200"></a>Rs 200</li><li> <a class="PRICE_300"></a>Rs 300</li> <li> <a class="PRICE_400"></a>Rs 400</li> <li> <a class="PRICE_600"></a>Rs 600</li><li> <a class="PRICE_800"></a>Rs 800</li><li> <a class="OCCUPIED"></a>Occupied</li><li> <a class="SELECTED"></a>Selected</li> <li> <a class=""></a><img src="' + UrlBase + '/images/Seat/Exitb.png"  />   Exit</li>   </ul> </div>'
            res += '<input type="hidden" id="selectedSegment" name="selectedSegment" />';
            //            res += '<input type="hidden" id="objModel" name="objModel" />';
            res += '<input type="hidden" id="CancelRemark" name="CancelRemark" />';
            res += '<div id="selectedSegmentDetails"></div>';
            res += '<div class="w75 rgt" style="padding-right:20px;" ><input type="button" class="btn_close rgt hide" id="btn_confirm" value="Continue"/></div>'
            res += '</div>';
            res += '<div class="clear"> </div>';
            //if (d.length > 0 && (d[0].lstPaxSeg[0].Provider == "1G" || d[0].lstPaxSeg[0].Provider == "1A")) {
            //    res += '<div class="vListContainerCancelRemark" style="font-weight: bold; font-style: italic; color: #ff0000;" >Please verify the coupon status to avoid any no show.</div>';
            //}
            res += '';

            $("#objModel").empty();
            objPCanc = new Array();
            objPCanc.push({ "OrderID": OrderID, "GdsPnr": d[0].lstPaxSeg[0].GDSPNR, "Trip": d[0].lstPaxSeg[0].Trip, "CorpID": d[0].lstPaxSeg[0].CorpId, "AirLinePNR": d[0].lstPaxSeg[0].AIRLINEPNR, "Provider": d[0].lstPaxSeg[0].Provider });
            $("#objModel").val(JSON.stringify(objPCanc));

            $("#SourceView").append(res);
            $('#segment' + d[0].PaxId).show();
            //Select a seat for ' + d.PaxListDetails[pax].FName + '</span>
            var Name = d[0].lstPaxSeg[0].PaxTitle + ' ' + d[0].lstPaxSeg[0].PaxFName + ' ' + d[0].lstPaxSeg[0].PaxLName;
            $("#selectedtrvl").html(Name);
            $.unblockUI();
            //getFareRuleForCanRes(OrderID, d[0].lstPaxSeg[0].Provider);
        }
        else {
            $("#SourceView").empty();
            res += '<div class="w60 lft text-center" style="width:100%; text-align:center; " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
            res += '<div class="w60 lft texthe">' + d[0].Error + '</div> '
            $("#SourceView").append(res);
            $.unblockUI();
        }
    }
    catch (er) {
        $("#SourceView").empty();
        res += '<div class="w60 lft text-center" style="width:100%; text-align:center; " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
        res += '<div class="w60 lft texthe"> Could not fetch details. Please try again or contact to help desk.</div> '
        $("#SourceView").append(res);
        $.unblockUI();
    }
}
function BlockUIFC() {
    $.blockUI({
        message: $("#waitMessagefc")
    });
}

    jQuery(function ($) {
        $("body").on("click", "a.topopup", function () {
            ////////
            loading(); // loading
            setTimeout(function () { // then show popup, deley in .5 second
                loadPopup(); // function show popup 
                $("#SourceView").empty();

            }, 100); // .5 second
            return false;
        });
        /* event for close the popup */
        $("div.close").hover(
                        function () {
                            $('span.ecs_tooltip').show();
                        },
                        function () {
                            $('span.ecs_tooltip').hide();
                        }
                    );
        $("body").on("click", "div.close", function () {
            disablePopup();  // function close pop up
        });
        $(this).keyup(function (event) {
            if (event.which == 27) { // 27 is 'Ecs' in the keyboard
                disablePopup();  // function close pop up
            }
        });
        /************** start: functions. **************/
        function loading() {
            $("div.loader").show();
        }
        function closeloading() {
            $("div.loader").fadeOut('normal');
        }
        function loadPopup() {

            if (popupStatus == 0) { // if value is 0, show popup
                closeloading(); // fadeout loading
                $("#toPopup").fadeIn(0500); // fadein popup div
                $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
                $("#backgroundPopup").fadeIn(0001);
                popupStatus = 1; // and set value to 1
            }
        }
        function disablePopup() {
            if (popupStatus == 1) { // if value is 1, close popup
                $("#toPopup").fadeOut("normal");
                $("#backgroundPopup").fadeOut("normal");
                popupStatus = 0;  // and set value to 0
            }
        }
        /************** end: functions. **************/
    });
   
