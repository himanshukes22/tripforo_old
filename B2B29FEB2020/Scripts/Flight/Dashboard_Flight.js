$(document).ready(function () {
    TripType("R");
    Servicewisecounter("ServiceCount");
    $('.ticket').click(function () {
        var CMD_TYPE = this.id;
        TripType(CMD_TYPE);
    });
});
function TripType(str) {
     
    var CMD_TYPE = str;
    $.ajax({
        url: UrlBase + "FltGroupBooking.asmx/Dashboard",
        data: "{'CMD_TYPE': '" + CMD_TYPE + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var _OrderId, _PName, _Sector, _TypeDate;
            var _tkt;
            var result = "";
            var TotalCount = 0;
            var data1 = JSON.parse(data.d);
            var numberOfElements = data1.Table.length;
            if (numberOfElements > 0) {
                TotalCount = data1.Table[0].Total;
            }
            else {
                TotalCount = 0;
            }
            for (var i = 0; i < numberOfElements; i++) {
                _OrderId = data1.Table[i].OrderId;
                _PName = data1.Table[i].PName;
                _Sector = data1.Table[i].sector;
                _TypeDate = data1.Table[i].TripDate;
                if (CMD_TYPE == "BU" || CMD_TYPE == "BR" ) {
                    _tkt = data1.Table[i].tkt;
                    if (CMD_TYPE == "BR" || CMD_TYPE == "BU") {
                        result += "<tr><td id='Orderid" + i + "' class='text-left text-primary ' onClick=srcurlBUS('" + _OrderId + "','" + CMD_TYPE + "','" + _tkt + "') data-toggle='modal' data-target='#myModal' >" + _OrderId + "</td>" +
                               "<td id='pname" + i + "' class='dest-fare'>" + _PName + "</td>" +
                               "<td id='td_sector" + i + "' class='dest-fare'>" + _Sector + "</td><td  id='td_tripdate" + i + "' class='dest-fare'>" + _TypeDate + "</td>";
                            //   "<td  id='" + i + "' class='text-center dest-fare'>";
                       // result += "<i class='fa fa-times' aria-hidden='true'  onClick=srcurlBUSRefund('" + _OrderId + "','" + CMD_TYPE + "','" + _tkt + "') data-toggle='modal' data-target='#myModal'><span> REFUND</span></i>";
                    }
                    result += "</tr>";
                   // result += "</td></tr>";
                }
                else if (CMD_TYPE == "R" || CMD_TYPE == "U" || CMD_TYPE == "HR" || CMD_TYPE == "HU") {
                    result += "<tr><td id='Orderid" + i + "' class='text-left text-primary ' onClick=srcurl('" + _OrderId + "','" + CMD_TYPE + "') data-toggle='modal' data-target='#myModal' >" + _OrderId + "</td>" +
                               "<td id='pname" + i + "' class='dest-fare'>" + _PName + "</td>" +
                               "<td id='td_sector" + i + "' class='dest-fare'>" + _Sector + "</td><td  id='td_tripdate" + i + "' class='dest-fare'>" + _TypeDate + "</td>";
                               //"<td  id='" + i + "' class='text-center dest-fare'>";

                    //if (CMD_TYPE == "R" || CMD_TYPE == "U") {
                    //    result += "<i class='fa fa-share-square-o' aria-hidden='true' onClick=ReissueRefundrequest('" + data1.Table[i].OrderId + "','" + data1.Table[i].GDSPNR + "','" + data1.Table[i].PaxId + "','" + data1.Table[i].PaxType + "','REISSUE') ><span>  REISSUE </span></i>&nbsp;&nbsp; &nbsp; <i class='fa fa-times' aria-hidden='true'  onClick=ReissueRefundrequest('" + data1.Table[i].OrderId + "','" + data1.Table[i].GDSPNR + "','" + data1.Table[i].PaxId + "','" + data1.Table[i].PaxType + "','REFUND')><span> REFUND</span></i>";
                    //}
                    //else if (CMD_TYPE == "HR" || CMD_TYPE == "HU") {
                    //    result += "<i class='fa fa-times' aria-hidden='true'  onClick=HotelRefundrequest('" + data1.Table[i].OrderId + "')><span> REFUND</span></i>";
                    //}
                    //result += "</td></tr>";
                    result += "</tr>";
                }
                else if (CMD_TYPE == "B_Hold" || CMD_TYPE == "B_Refund" || CMD_TYPE == "BP") {
                    _tkt = data1.Table[i].tkt;
                    result += "<tr><td id='Orderid" + i + "' class='text-left text-primary ' onClick=srcurlBUS('" + _OrderId + "','" + CMD_TYPE + "','" + _tkt + "') data-toggle='modal' data-target='#myModal' >" + _OrderId + "</td>" +
                              "<td id='pname" + i + "' class='dest-fare'>" + _PName + "</td>" +
                              "<td id='td_sector" + i + "' class='dest-fare'>" + _Sector + "</td><td  id='td_tripdate" + i + "' class='dest-fare'>" + _TypeDate + "</td></tr>"
                }
                else {
                    result += "<tr><td id='Orderid" + i + "' class='text-left text-primary ' onClick=srcurl('" + _OrderId + "','" + CMD_TYPE + "') data-toggle='modal' data-target='#myModal' >" + _OrderId + "</td>" +
                              "<td id='pname" + i + "' class='dest-fare'>" + _PName + "</td>" +
                              "<td id='td_sector" + i + "' class='dest-fare'>" + _Sector + "</td><td  id='td_tripdate" + i + "' class='dest-fare'>" + _TypeDate + "</td></tr>"
                }
            }

            $('#' + CMD_TYPE).find('div').html(TotalCount);
            if (CMD_TYPE == "R") {
                $('#Data_Bind_R').html(result);

            }
            else if (CMD_TYPE == "U") {
                $('#Data_Bind_U').html(result);
            }
            else if (CMD_TYPE == "P") {
                $('#Data_Bind_P').html(result);
                $('#paction').hide();
            }
            else if (CMD_TYPE == "Hold") {
                $('#Data_Bind_Hold').html(result);
                $('#HoldtktAction').hide();
            }
            else if (CMD_TYPE == "Refund") {
                $('#Data_Bind_RefundTicket').html(result);
                $('#tktrefundaction').hide();
            }
            else if (CMD_TYPE == "Reissue") {
                $('#Data_Bind_Reissue').html(result);
                $('#tktreissueaction').hide();
            }
            else if (CMD_TYPE == "HR") {
                $('#Data_Bind_HR').html(result);
            }
            else if (CMD_TYPE == "HU") {
                $('#Data_Bind_HU').html(result);
            }
            else if (CMD_TYPE == "HP") {
                $('#Data_Bind_HP').html(result);
                $('#htlpast').hide();
            }
            else if (CMD_TYPE == "H_Hold") {
                $('#Data_Bind_H_Hold').html(result);
                $('#htlholdaction').hide();
            }
            else if (CMD_TYPE == "H_Refund") {
                $('#Data_Bind_H_RefundTicket').html(result);
                $('#htlrefundaction').hide();
            }
                // FOR BUS
            else if (CMD_TYPE == "BR") {
                $('#Data_Bind_BR').html(result);
            }
            else if (CMD_TYPE == "BU") {
                $('#Data_Bind_BU').html(result);
            }
            else if (CMD_TYPE == "BP") {
                $('#Data_Bind_BP').html(result);
            }
            else if (CMD_TYPE == "B_Hold") {
                $('#Data_Bind_B_Hold').html(result);
            }
            else if (CMD_TYPE == "B_Refund") {
                $('#Data_Bind_B_RefundTicket').html(result);
            }
        },
        error: function (data) {
            //alert(data)
        }
    })
};
function Servicewisecounter(str) {
    var CMD_TYPE = str;
    $.ajax({

        url: UrlBase + "FltGroupBooking.asmx/Dashboard",
        data: "{'CMD_TYPE': '" + CMD_TYPE + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var result = "";
            var TktPastTrip = 0, TktRecentTrip = 0, TktUpcomingTrip = 0, TktHold = 0, TktRefund = 0, Tktreissue = 0,
            HTLPastBooking = 0, HTLRecentBooking = 0, HTLUpcomingBooking = 0, HTLReissue = 0, HTLRefund = 0,
            BusPastTrip = 0, BusRecentTrip = 0; BusUpcomingTrip = 0, BusHold = 0, BusRefund = 0;

            var data1 = JSON.parse(data.d);
            var numberOfElements = data1.Table.length;
            if (numberOfElements > 0) {
                 
                TktPastTrip = data1.Table[0].TktPastTrip;
                TktRecentTrip = data1.Table[0].TktRecentTrip;
                TktUpcomingTrip = data1.Table[0].TktUpcomingTrip;
                TktHold = data1.Table[0].TktHold;
                TktRefund = data1.Table[0].TktRefund;
                Tktreissue = data1.Table[0].Tktreissue;
                HTLPastBooking = data1.Table[0].HTLPastBooking;
                HTLRecentBooking = data1.Table[0].HTLRecentBooking;
                HTLUpcomingBooking = data1.Table[0].HTLUpcomingBooking;
                HTLReissue = data1.Table[0].HTLReissue;
                HTLRefund = data1.Table[0].HTLRefund;
                //for bus
                BusPastTrip = data1.Table[0].BusPastTrip;
                BusRecentTrip = data1.Table[0].BusRecentTrip;
                BusUpcomingTrip = data1.Table[0].BusUpcomingTrip;
                BusHold = data1.Table[0].BusHold;
                BusRefund = data1.Table[0].BusRefund;
            }
            if (TktRecentTrip == 0) {
                $('#1').hide();
            }
            else {
                $('#1').show();
            }
            if (TktUpcomingTrip == 0) {
                $('#2').hide();
            }
            else {
                $('#2').show();
            }
            if (TktPastTrip == 0) {
                $('#3').hide();
            }
            else {
                $('#3').show();
            }
            if (TktHold == 0) {
                $('#4').hide();
            }
            else {
                $('#4').show();
            }
            if (TktRefund == 0) {
                $('#5').hide();
            }
            else {
                $('#5').show();
            }
            if (Tktreissue == 0) {
                $('#6').hide();
            }
            else {
                $('#6').show();
            }
            if (HTLRecentBooking == 0) {
                $('#7').hide();
            }
            else {
                $('#7').show();
            }
            if (HTLPastBooking == 0) {
                $('#9').hide();
            }
            else {
                $('#9').show();
            }
            if (HTLUpcomingBooking == 0) {
                $('#8').hide();
            }
            else {
                $('#8').show();
            }
            if (HTLReissue == 0) {
                $('#10').hide();
            }
            else {
                $('#10').show();
            }
            if (BusRecentTrip == 0) {
                $('#12').hide();
            }
            else {
                $('#12').show();
            }
            if (BusUpcomingTrip == 0) {
                $('#13').hide();
            }
            else {
                $('#13').show();
            }
            if (BusPastTrip == 0) {
                $('#14').hide();
            }
            else {
                $('#14').show();
            }
            if (BusHold == 0) {
                $('#15').hide();
            }
            else {
                $('#15').show();
            }
            if (BusRefund == 0) {
                $('#16').hide();
            }
            else {
                $('#16').show();
            }
            if (HTLRefund == 0) {
                $('#11').hide();
            }
            else {
                $('#11').show();
            }
            $('#R').find('div').html('(' + TktRecentTrip + ')');
            $('#U').find('div').html('(' + TktUpcomingTrip + ')');
            $('#P').find('div').html('(' + TktPastTrip + ')');
            $('#Hold').find('div').html('(' + TktHold + ')');
            $('#Refund').find('div').html('(' + TktRefund + ')');
            $('#Reissue').find('div').html('(' + Tktreissue + ')');
            $('#HR').find('div').html('(' + HTLRecentBooking + ')');
            $('#HP').find('div').html('(' + HTLPastBooking + ')');
            $('#HU').find('div').html('(' + HTLUpcomingBooking + ')');
            $('#H_Hold').find('div').html('(' + HTLReissue + ')');
            $('#H_Refund').find('div').html('(' + HTLRefund + ')');
            //for bus
            $('#BR').find('div').html('(' + BusRecentTrip + ')');
            $('#BU').find('div').html('(' + BusUpcomingTrip + ')');
            $('#BP').find('div').html('(' + BusPastTrip + ')');
            $('#B_Hold').find('div').html('(' + BusHold + ')');
            $('#B_Refund').find('div').html('(' + BusRefund + ')');
        },
        error: function (data) {
            alert("Unable to process. Please try again.")
        }
    })
};
function srcurl(orderid, cmdtype) {
    if (cmdtype == "R" || cmdtype == "U" || cmdtype == "P" || cmdtype == "Hold" || cmdtype == "Refund" || cmdtype == "Reissue") {
        var blogUrl;
        var fnlURL;
        blogUrl = "" + UrlBase + "Report/PnrSummaryIntl.aspx?OrderId=" + orderid + "%20&TransID=";
        $('#IfrmTKT').attr('src', blogUrl);
    }
    else if (cmdtype == "HR" || cmdtype == "HU" || cmdtype == "HP" || cmdtype == "H_Hold" || cmdtype == "H_Refund") {
        var blogUrl;
        var fnlURL;
        blogUrl = "" + UrlBase + "Hotel/BookingSummaryHtl.aspx?OrderId=" + orderid + "";
        $('#IfrmTKT').attr('src', blogUrl);
    }
    else if (cmdtype == "BR" || cmdtype == "BU" || cmdtype == "BP" || cmdtype == "B_Hold" || cmdtype == "B_Refund") {
        var blogUrl;
        var fnlURL;
        blogUrl = "" + UrlBase + "BS/TicketSummary.aspx?tin=" + orderid + "&oid" + orderid + "";
        $('#IfrmTKT').attr('src', blogUrl);
    }
    else {
        alert("Unable to process. Please try again.");
    }
}
//for bus only
function srcurlBUS(orderid, cmdtype, tkt) {
    if (cmdtype == "BR" || cmdtype == "BU" || cmdtype == "BP" || cmdtype == "B_Hold" || cmdtype == "B_Refund") {
        var blogUrl;
        var fnlURL;
        blogUrl = "" + UrlBase + "BS/TicketSummary.aspx?tin=" + tkt + "&oid=" + orderid + "";
        $('#IfrmTKT').attr('src', blogUrl);
    }
    else {
        alert("Unable to process. Please try again.");
    }
}
//for bus refund
function srcurlBUSRefund(orderid, cmdtype, tkt) {
    if (cmdtype == "BR" || cmdtype == "BU" || cmdtype == "BP" || cmdtype == "B_Hold" || cmdtype == "B_Refund") {
        var blogUrl;
        var fnlURL;
        blogUrl = "" + UrlBase + "BS/CancelTicket.aspx?tin==" + tkt + "&oid=" + orderid + "";
        $('#IfrmTKT').attr('src', blogUrl);
    }
    else {
        alert("Unable to process. Please try again.");
    }
}
function ReissueRefundrequest(orderid, gdspnr, paxid, PaxType, ReqType) {
    $.ajax({
        url: UrlBase + "FltGroupBooking.asmx/ReissueRefundrequest",
        data: "{ 'orderid': '" + orderid + "','gdspnr': '" + gdspnr + "','paxid': '" + paxid + "','PaxType': '" + PaxType + "','ReqType': '" + ReqType + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d.length > 0) {

                if (data.d[1] == "0") {
                    if (ReqType == "REFUND")
                        RefundPopUpLoad(paxid, ReqType, gdspnr, orderid);
                    else
                        popupLoad(paxid, ReqType, gdspnr, data.d[2], data.d[3], PaxType);
                }
                else if (data.d[1] == "Reissue request can not be accepted for past departure date." && ReqType == "REFUND") {
                    RefundPopUpLoad(paxid, ReqType, gdspnr, orderid)
                }
                    //(SecondStatus = "0" Or SecondStatus = "Reissue request can not be accepted for past departure date." Or SecondStatus = "Given ticket number is already ReIssued") Or FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And CommandName = "Refund"
                else if (((data.d[1] == "0" || data.d[1] == "Reissue request can not be accepted for past departure date." || data.d[1] == "Given ticket number is already ReIssued") || data.d[0] == "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly.") && ReqType == "REFUND") {
                    if (data.d[0] == "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." && ReqType == "REFUND") {
                        HourDeparturePopup(paxid, ReqType, gdspnr, orderid);
                    }
                    else {
                        if (ReqType == "REFUND")
                            RefundPopUpLoad(paxid, ReqType, gdspnr, orderid);
                        else
                            popupLoad(paxid, ReqType, gdspnr, data.d[2], data.d[3], PaxType);
                    }
                }
                else if (data.d[0] == "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." && ReqType == "REFUND") {
                    HourDeparturePopup(paxid, ReqType, gdspnr, orderid);
                }
                else
                    jAlert(data.d[1], 'Alert');
            }
            else
                jAlert("Unable to process. Please try again.", 'Alert');

        },
        error: function (data) {
          //  alert(data)
        }
    })
}

function HotelRefundrequest(orderid) {

    $.ajax({
        url: UrlBase + "Report/FlightRefundReisue.asmx/HotelRefundrequest",
        data: "{ 'orderid': '" + orderid + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d.length > 0) {
                switch (data.d[0]) {
                    case 1:
                        jAlert("This Booking ID Allready Cancelled", 'Alert');
                        break;
                    case 2:
                        jAlert("This Booking ID is Pending for Cancellation", 'Alert');
                        break;
                    case 3:
                        jAlert("This Booking ID is Allready in Cancellation InProcess", 'Alert');
                        break;
                    default:
                        var HtlDT = JSON.parse(data.d[1]);
                        if (HtlDT.Table[0].ModifyStatus == "PartialCancel") {
                            $("#amt").text(HtlDT.Table[0].NetCost + HtlDT.Table[0].PgCharges - HtlDT.Table[0].RefundAmt);
                        }
                        else {
                            $("#amt").text(HtlDT.Table[0].NetCost + HtlDT.Table[0].PgCharges);
                        }

                        if (HtlDT.Table[0].ModifyStatus == "Cancelled") {
                            jAlert("Ticket all ready canceled.", 'Alert');
                        }
                        else {
                            $("#HotelName").text(HtlDT.Table[0].HotelName);
                            $("#room").text(HtlDT.Table[0].RoomCount);
                            $("#night").text(HtlDT.Table[0].NightCount);
                            $("#adt").text(HtlDT.Table[0].AdultCount);
                            $("#chd").text(HtlDT.Table[0].ChildCount);
                            $("#policy").html("<span style=\'font-size:13px;font-weight: bold;\'>CANCELLATION POLICIES</span>" + HtlDT.Table[0].CancellationPoli);
                            $("#RemarkTitle").text("Hotel Cancellation for order id (" + orderid + ")");
                            $("#OrderIDS").val(orderid);

                            ShowHide('show', HtlDT.Table[0].CheckIN);

                        }

                        break;
                }
            }
            else
                jAlert("Unable to process. Please try again.", 'Alert');
        },
        error: function (data) {
           // alert(data)
        }
    })
}
function BusRefundrequest(orderid, tktno) {

    $.ajax({
        url: UrlBase + "Report/FlightRefundReisue.asmx/HotelRefundrequest",
        data: "{ 'orderid': '" + orderid + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d.length > 0) {
                switch (data.d[0]) {
                    case 1:
                        jAlert("This Booking ID Allready Cancelled", 'Alert');
                        break;
                    case 2:
                        jAlert("This Booking ID is Pending for Cancellation", 'Alert');
                        break;
                    case 3:
                        jAlert("This Booking ID is Allready in Cancellation InProcess", 'Alert');
                        break;
                    default:
                        var HtlDT = JSON.parse(data.d[1]);
                        if (HtlDT.Table[0].ModifyStatus == "PartialCancel") {
                            $("#amt").text(HtlDT.Table[0].NetCost + HtlDT.Table[0].PgCharges - HtlDT.Table[0].RefundAmt);
                        }
                        else {
                            $("#amt").text(HtlDT.Table[0].NetCost + HtlDT.Table[0].PgCharges);
                        }

                        if (HtlDT.Table[0].ModifyStatus == "Cancelled") {
                            jAlert("Ticket all ready canceled.", 'Alert');
                        }
                        else {
                            $("#HotelName").text(HtlDT.Table[0].HotelName);
                            $("#room").text(HtlDT.Table[0].RoomCount);
                            $("#night").text(HtlDT.Table[0].NightCount);
                            $("#adt").text(HtlDT.Table[0].AdultCount);
                            $("#chd").text(HtlDT.Table[0].ChildCount);
                            $("#policy").html("<span style=\'font-size:13px;font-weight: bold;\'>CANCELLATION POLICIES</span>" + HtlDT.Table[0].CancellationPoli);
                            $("#RemarkTitle").text("Hotel Cancellation for order id (" + orderid + ")");
                            $("#OrderIDS").val(orderid);

                            ShowHide('show', HtlDT.Table[0].CheckIN);

                        }

                        break;
                }
            }
            else
                jAlert("Unable to process. Please try again.", 'Alert');
        },
        error: function (data) {
            //alert(data)
        }
    })
}