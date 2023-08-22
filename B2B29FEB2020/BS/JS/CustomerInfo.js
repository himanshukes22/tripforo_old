var CHandler; var pax;
$(document).ready(function () {
    CHandler = new CusHelper;
    CHandler.BindEvents();
    $("#hidFaredetailsXml");
});
var CusHelper = function () {
    this.bpID; this.dpID;
    this.tripid; this.boardinglocation;
    this.dropinglocation;
    this.ladiesseat;
    this.mob;
    this.email;
    this.idtype;
    this.idnum;
    this.src;
    this.dest;
    this.srcid;
    this.destid;
    this.busop;
    this.orderid;
    this.inventory = new Array();
    this.primary;
    this.jrdate;
    this.admincom;
    this.tatds;
    this.totalfare;
    this.tatotalfare;
    this.tanetfare;
    this.sumFare;
    this.idproofRequired;
}

CusHelper.prototype.BindEvents = function () {
    var h = this;
    var pgUrl = window.location.search.substring(1);
    var id = pgUrl.split('=');
    h.orderid = id[1];
    h.getSeatfareDetails();
}

CusHelper.prototype.getSeatfareDetails = function () {
    var h = this; h.sumFare = 0;
    var url = UrlBase + "BS/WebServices/RBAutoSearch.asmx/getSeatFareDetails";
    $.ajax({
        url: url,
        data: "{'orderid':'" + h.orderid + "'}",
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function (data) {
            var result = data.d; var boardId; var dropId; var source; var desti;
            var mytable = "<table width='100%' cellpadding='0' cellspacing='0' border='0' style='border:1px solid #ccc; box-shadow:1px 1px 5px #333; padding:20px; border-radius:10px; -o-border-radius:10px; -moz-border-radius:10px; -webkit-border-radius:10px;'>";
            if (result.length != 0) {
                mytable += "<tr>";
                mytable += "<td style='padding-bottom:20px;'>";

                mytable += "<table width='80%' cellpadding='2' cellspacing='2' style='color:#888;'>";
                mytable += "<tr>";
                var sour = result[0].cityname.split('(');
                var desti = result[0].dest_cityname.split('(');
                mytable += "<td colspan='3' style='font-size:16px; font-weight:bold; line-height:35px;'>" + sour[0] + " <img src='Images/arrow.png' />&nbsp;" + desti[0] + "</td>";
                mytable += "</tr>";
                mytable += "<tr>";
                boardId = result[0].boardpoint.split('&');
                dropId = result[0].droppoint.split('&');
                source = result[0].cityname.split('(');
                desti = result[0].dest_cityname.split('(');
                h.bpID = boardId[1];
                h.dpID = dropId[1];
                mytable += "<td>" + boardId[0] + "</td>";
                mytable += "<td></td>";
                mytable += "<td>" + dropId[0] + "</td>";
                mytable += "</tr>";
                mytable += "<tr>";
                mytable += "<td colspan='3' class='breakupp' style='text-align:left; color:#108406;'><div>Fare Breakup</div><div class='farebreakup' style='border: 1px solid #128607; line-height:16px;'></div></td>";
                mytable += "</tr>";
                mytable += "</table>";

                mytable += "</td>";
                mytable += "</tr>";
                var seat = result[0].seat.split(',');
                var Orifare = result[0].originalfare.split(',');
                var fareMrkp = result[0].fare.split(',');
                if (seat.length != 0 && Orifare.length != 0) {
                    mytable += "<tr>";
                    mytable += "<td>";

                    mytable += "<table width='100%' cellpadding='2' cellspacing='5' border='0' align='center'>";
                    mytable += "<tr style='font-size:12px; font-weight:bold; color:#888;'><td>Title</td><td>Full Name</td><td>Age</td><td>Gender</td><td>Seat</td><td align='center'>Fare</td></tr>";
                    for (var i = 0; i <= seat.length - 1; i++) {
                        if (seat.length == 0) {
                            mytable += "<tr>";
                            mytable += "<td colspan='6' class='pcls'>Primary Passenger:</td>";
                            mytable += "</tr>";
                            if (seat[i] != "") {
                                mytable += "<tr>";
                                mytable += "<td><select id='dptitle" + i + "' class='txt' style='width:70px;'><option value='Mr'>Mr</option><option value='Ms'>Ms</option></td>";
                                mytable += "<td><input type='text' class='txt' id='txtprimarypaxname" + i + "' name='txtpaxname" + i + "' title='primary' placeholder='Passenger Name'/></td>";
                                mytable += "<td><input type='text' class='txt' id='txtpaxage" + i + "' name='txtpaxage" + i + "' style='width:50px;' placeholder='Age'/></td>";
                                mytable += "<td style='width:50px;'><select id='dpgender" + i + "' class='txt' style='width:65px;'><option value='MALE'>Male</option><option value='FEMALE'>Female</option>";
                                mytable += "<td><div style='float:left; font-size:14px; font-weight:bold; color:#888;'><img src='Images/2.png' /></div><div style='float:left;'>" + seat[i] + "</div></td>";
                                if (fareMrkp[i] != "") {
                                    mytable += "<td align='center' style='font-size:14px; font-weight:bold; color:#888;'><img src='images/rupee.png' /> " + fareMrkp[i] + "</td>";
                                    h.sumFare += parseInt(Orifare[i]);
                                }
                            }
                            mytable += "</tr>";
                        }
                        else {
                            if (i == 0) {
                                if (seat[i] != "") {
                                    mytable += "<tr>";
                                    mytable += "<td colspan='6' class='pcls'>Primary Passenger:" + (i + 1) + "</td>";
                                    mytable += "</tr>";
                                    mytable += "<tr>";
                                    mytable += "<td><select id='dptitle" + i + "' class='txt' style='width:70px;'><option value='Mr'>Mr</option><option value='Ms'>Ms</option></td>";
                                    mytable += "<td><input type='text' id='txtprimarypaxname" + i + "' name=txtpaxname" + i + " title='primary' class='txt'  placeholder='Passenger Name' /></td>";
                                    mytable += "<td><input type='text' id='txtpaxage" + i + "' name=txtpaxage" + i + " class='txt' style='width:50px;' placeholder='Age'/></td>";
                                    mytable += "<td><select id='dpgender" + i + "' class='txt' style='width:65px;'><option value='MALE'>Male</option><option value='FEMALE'>Female</option>";
                                    mytable += "<td style='font-size:14px; font-weight:bold; color:#888;'><img src='Images/2.png' />&nbsp;&nbsp;" + seat[i] + "</td>";
                                    if (fareMrkp[i] != "") {
                                        mytable += "<td align='center' style='font-size:14px; font-weight:bold; color:#888;'><img src='images/rupee.png' /> " + fareMrkp[i] + "</td>";
                                        h.sumFare += parseInt(Orifare[i]);
                                    }
                                }
                            }
                            else {
                                if (seat[i] != "") {
                                    mytable += "<tr>";
                                    mytable += "<td colspan='6' class='pcls'>Passenger:" + (i + 1) + ":</td>";
                                    mytable += "</tr>";
                                    mytable += "<tr>";
                                    mytable += "<td><select id='dptitle" + i + "' class='txt' style='width:70px;'><option value='Mr'>Mr</option><option value='Ms'>Ms</option></td>";
                                    mytable += "<td><input type='text' id='txtpaxname" + i + "' name=txtpaxname" + i + " class='txt'  placeholder='Passenger Name'/></td>";
                                    mytable += "<td><input type='text' id='txtpaxage" + i + "' name=txtpaxage" + i + " class='txt' style='width:50px;' placeholder='Age'/></td>";
                                    mytable += "<td><select id='dpgender" + i + "' class='txt' style='width:65px;'><option value='MALE'>Male</option><option value='FEMALE'>Female</option>";
                                    mytable += "<td style='font-size:14px; font-weight:bold; color:#888;'><img src='Images/2.png' />&nbsp;&nbsp;" + seat[i] + "</td>";
                                    if (fareMrkp[i] != "") {
                                        mytable += "<td align='center' style='font-size:14px; font-weight:bold; color:#888;'><img src='images/rupee.png' /> " + fareMrkp[i] + "</td>";
                                        h.sumFare += parseInt(Orifare[i]);
                                    }
                                }
                            }
                        }
                        mytable += "</tr>";
                    }
                }
                mytable += "<tr>";
                mytable += "<td colspan='6' style='height:35px;'>";
                mytable += "</tr>";
                if (result[0].idproofRequired == "true") {
                    mytable += "<tr style='font-size:12px; font-weight:bold; color:#888;'><td colspan='2'>Email ID</td><td>Mobile No.</td><td colspan='2'>ID Proof</td><td>ID No.</td><td colspan='2'>Address</td></tr>";
                    mytable += "<tr>";
                    mytable += "<td colspan='6' class='pcls'>Primary Passenger Contact Details:</td>";
                    mytable += "</tr>";
                    mytable += "<tr>";
                    mytable += "<td colspan='2'><input type='text' id='txtemail' name='txtemail' class='txt' style='width:230px;'  placeholder='Email Address' /></td>";
                    mytable += "<td><input type='text' id='txtmob' name='txtmob' class='txt' placeholder='Mobile Number'/></td>";
                    mytable += "<td colspan='2'><select id='idproof' name='idproof' class='txt'>";
                    mytable += "<option value='PAN_CARD'>Pan Card</option>";
                    mytable += "<option value='VOTER_CARD'>Votter Id</option>";
                    mytable += "<option value='DRIVING_LICENCE'>Driving Licence</option>";
                    mytable += "<option value='RATION_CARD'>Ration Card</option>";
                    mytable += "<option value='AADHAR'>Aadhar Card</option>";
                    mytable += "</td>";
                    mytable += "<td><input type='text' id='txtcard' name='txtcard' class='txt' placeholder='ID Number' /></td>";
                    mytable += "<td><input type='text' id='txtaddress' name='txtaddress' class='txt' placeholder='Address' /></td>";
                    mytable += "</tr>";
                }
                else {
                    mytable += "<tr style='font-size:12px; font-weight:bold; color:#888;'><td colspan='2'>Email ID</td><td>Mobile No.</td><td colspan='2'>Address</td></tr>";
                    mytable += "<tr>";
                    mytable += "<td colspan='6' class='pcls'>Primary Passenger Contact Details:</td>";
                    mytable += "</tr>";
                    mytable += "<tr>";
                    mytable += "<td colspan='2'><input type='text' id='txtemail' name='txtemail' class='txt' style='width:230px;'  placeholder='Email Address' /></td>";
                    mytable += "<td><input type='text' id='txtmob' name='txtmob' class='txt' placeholder='Mobile Number'/></td>";
                    mytable += "<td><input type='text' id='txtaddress' name='txtaddress' class='txt' placeholder='Address' /></td>";
                    mytable += "</tr>";
                }

                mytable += "<tr>";
                //mytable += "<td colspan='4'></td>";
                mytable += "<td colspan='6'><div id='div_Progress' style='display: none'><b>Booking Is In Progress.</b> Please do not press 'refresh' or 'back'<img alt='Booking Is In Progress' src='images/smload3.gif' /></div><div id='div_submit'><input type='button' id='btnbook' name='btnbook' value='BOOK' class='btn'/></div></td>";
                mytable += "</tr>";
                mytable += "</table>";
                mytable += "</td>";
                mytable += "</tr>";
                mytable += "</table>";
            }
            $("#custInfo").html(mytable);

            $(".breakupp").mouseover(function () {
                h.getCommandTds("", h.sumFare);
                $(this).addClass("breakup2");
            });
            $(".breakupp").mouseout(function () {
                $(this).addClass("breakupp");
                $(this).removeClass("breakup2");
            });
            $(document).on("click", ".btnbooksss", function (e) {
          
                h.inventory.length = 0;
                for (var j = 0; j <= seat.length - 1; j++) {
                    pax = new Array();

                    if (seat[j] != "") {
                        pax.push("Title:" + $("#dptitle" + j + ">option:selected").text());
                        if (j == 0) {
                            if ($("#txtprimarypaxname" + j).val() == "") {
                                alert('Please provide your Name');
                                $("#txtprimarypaxname" + j).focus();
                                return false;
                            }
                            else {
                                pax.push("Name:" + $("#txtprimarypaxname" + j).val());
                                h.primary = "true";
                                if ($("#txtprimarypaxname" + j).attr("title") != "") {
                                    pax.push("Primary:" + $("#txtprimarypaxname" + j).attr("title"));
                                }
                            }
                        }
                        else {
                            if ($("#txtpaxname" + j).val() == "") {
                                alert('Please provide your Name');
                                $("#txtpaxname" + j).focus();
                                return false;
                            }

                            else {
                                pax.push("Name:" + $("#txtpaxname" + j).val());
                            }

                        }

                        if ($("#txtpaxage" + j).val() == "") {
                            alert('Please provide your age');
                            $("#txtpaxage" + j).focus();
                            return false;
                        }
                        else {
                            pax.push("Age:" + $("#txtpaxage" + j).val());
                        }
                        pax.push("Gender:" + $("#dpgender" + j + ">option:selected").val());
                        pax.push("Seat:" + seat[j]);
                        pax.push("Fare:" + fareMrkp[j]);
                        pax.push("Original:" + Orifare[j]);
                        h.inventory.push(pax);
                    }

                }
                h.tripid = result[0].tripID;
                h.orderid = result[0].orderID;
                h.src = source[0];
                h.dest = desti[0];
                h.srcid = source[1].replace(')', '');
                h.destid = desti[1].replace(')', '');
                h.busop = result[0].busoperator;
                h.ladiesseat = result[0].ladiesSeat;
                h.boardinglocation = boardId[0];
                h.dropinglocation = dropId[0];
                h.jrdate = result[0].DateOfJourney;
                h.admincom = result[0].adcomm;
                h.tatds = result[0].taTds;
                h.totalfare = result[0].totFare;
                h.tatotalfare = result[0].taTotFare;
                h.tanetfare = result[0].taNetFare;
                h.idproofRequired = result[0].idproofRequired;

                if ($("#txtemail").val() == "") {
                    alert('Please provide your email');
                    $("#txtemail").focus();
                    return false;
                }
                else {
                    h.email = $("#txtemail").val();
                }
                if ($("#txtmob").val() == "") {
                    alert('Please provide your mobile number');
                    $("#txtmob").focus();
                    return false;
                }
                else {
                    h.mob = $("#txtmob").val();
                }

                if (result[0].idproofRequired == "true") {
                    h.idtype = $("#idproof>option:selected").val()
                    if ($("#txtcard").val() == "") {
                        alert('Please provide your ID card Number');
                        $("#txtcard").focus();
                        return false;
                    }
                    else {
                        h.idnum = $("#txtcard").val();
                    }
                    if ($("#txtaddress").val() == "") {
                        alert('Please provide your Address');
                        $("#txtaddress").focus();
                        return false;
                    }
                    else {
                        h.address = $("#txtaddress").val();
                    }
                }
                else {
                    if ($("#txtaddress").val() == "") {
                        alert('Please provide your Address');
                        $("#txtaddress").focus();
                        return false;
                    }
                    else {
                        h.address = $("#txtaddress").val();
                    }
                    h.idtype = "";
                    h.idnum = "";
                }

                if (confirm("Are you sure!")) {
                    $("#div_Submit").hide();
                    $("#div_Progress").show();
                    h.getBlockticketId();

                }
                else {
                    return false;
                }



            });
        }
    });
}
CusHelper.prototype.getBlockticketId = function () {
    var h = this;
    var url = UrlBase + "BS/WebServices/RBAutoSearch.asmx/getBlockTicketresponse";
    $.ajax({
        url: url,
        data: JSON.stringify({ orderid: h.orderid, busoperator: h.busop, ladiesseat: h.ladiesseat, tripid: h.tripid, bplocation: h.boardinglocation, dplocation: h.dropinglocation, jrdate: h.jrdate, bpId: h.bpID, dpid: h.dpID, dest: h.dest, destid: h.destid, idtype: h.idtype, idcard: h.idnum, paxmob: h.mob, paxemail: h.email, inventorylist: h.inventory, src: h.src, srcid: h.srcid, admincom: h.admincom, tatds: h.tatds, totfare: h.totalfare, tatotfare: h.tatotalfare, tanetfare: h.tanetfare, idproofRquired: h.idproofRequired, isprimary: h.primary, address: h.address }),
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: false,
        success: function (data) {
            var result = $.parseJSON(data.d);
            if (result.Error == undefined) {
                var tinNo = result.tin;
                document.getElementById("__VIEWSTATE").name = "NOVIEWSTATE";
                document.forms[0].action = UrlBase + "BS/TicketCopy.aspx?tin=" + tinNo + "&oid=" + h.orderid + "";
                document.forms[0].submit();
            }
            else {
                alert(result.Error);
            }
        }
    });
}

CusHelper.prototype.getCommandTds = function (a, b) {
    var h = this;
    $(".farebreakup").html("<img src='" + UrlBase + "BS/images/load.gif' alt='' />");
    var comUrl = UrlBase + "BS/WebServices/RBAutoSearch.asmx/commisionList";
    $.ajax({
        url: comUrl,
        data: "{'seatNo':'" + a + "','fare':'" + b + "'}",
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function (data) {
            var comResult = data.d;
            var farebreakup = "<table cellpadding='0' cellspacing='0' class='faretbl'>";
            if (b.toString().indexOf(',') > 0) {
                farebreakup += "<tr>";
                var f = b.split(',');
                farebreakup += "<td class='bg'><b>Lowest Fare:</b>&nbsp; " + f[0] + "</td>";
            }
            else {
                farebreakup += "<tr>";
                farebreakup += "<td class='bg'><b>Fare:</b>&nbsp;" + b + "</td>";
            }


            farebreakup += "<td class='bg'><b>Service Charge:</b>&nbsp;" + comResult[0].serviceChrg + "</td>";
            farebreakup += "<td class='bg'><b>Total Fare:</b>&nbsp;" + comResult[0].taTotFare + "</td>";
            farebreakup += "</tr>";
            farebreakup += "<tr>";
            farebreakup += "<td class='bg'><b>Commission(-):</b>&nbsp;" + comResult[0].adcomm + "</td>";
            farebreakup += "<td class='bg'><b>TDS(+):</b>&nbsp;" + comResult[0].taTds + "</td>";
            farebreakup += "<td class='bg'><b>Net Fare:</b>&nbsp; " + comResult[0].taNetFare + "</td>";
            farebreakup += "</tr>";
            farebreakup += "</table>";
            $(".farebreakup").html(farebreakup);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        { return textStatus; }


    });
}




//$('#btnbook').click(function () {
//    h.inventory.length = 0;
//    for (var j = 0; j <= seat.length - 1; j++) {
//        pax = new Array();

//        if (seat[j] != "") {
//            pax.push("Title:" + $("#dptitle" + j + ">option:selected").text());
//            if (j == 0) {
//                if ($("#txtprimarypaxname" + j).val() == "") {
//                    alert('Please provide your Name');
//                    $("#txtprimarypaxname" + j).focus();
//                    return false;
//                }
//                else {
//                    pax.push("Name:" + $("#txtprimarypaxname" + j).val());
//                    h.primary = "true";
//                    if ($("#txtprimarypaxname" + j).attr("title") != "") {
//                        pax.push("Primary:" + $("#txtprimarypaxname" + j).attr("title"));
//                    }
//                }
//            }
//            else {
//                if ($("#txtpaxname" + j).val() == "") {
//                    alert('Please provide your Name');
//                    $("#txtpaxname" + j).focus();
//                    return false;
//                }

//                else {
//                    pax.push("Name:" + $("#txtpaxname" + j).val());
//                }

//            }

//            if ($("#txtpaxage" + j).val() == "") {
//                alert('Please provide your age');
//                $("#txtpaxage" + j).focus();
//                return false;
//            }
//            else {
//                pax.push("Age:" + $("#txtpaxage" + j).val());
//            }
//            pax.push("Gender:" + $("#dpgender" + j + ">option:selected").val());
//            pax.push("Seat:" + seat[j]);
//            pax.push("Fare:" + fareMrkp[j]);
//            pax.push("Original:" + Orifare[j]);
//            h.inventory.push(pax);
//        }

//    }
//    h.tripid = result[0].tripID;
//    h.orderid = result[0].orderID;
//    h.src = source[0];
//    h.dest = desti[0];
//    h.srcid = source[1].replace(')', '');
//    h.destid = desti[1].replace(')', '');
//    h.busop = result[0].busoperator;
//    h.ladiesseat = result[0].ladiesSeat;
//    h.boardinglocation = boardId[0];
//    h.dropinglocation = dropId[0];
//    h.jrdate = result[0].DateOfJourney;
//    h.admincom = result[0].adcomm;
//    h.tatds = result[0].taTds;
//    h.totalfare = result[0].totFare;
//    h.tatotalfare = result[0].taTotFare;
//    h.tanetfare = result[0].taNetFare;
//    h.idproofRequired = result[0].idproofRequired;

//    if ($("#txtemail").val() == "") {
//        alert('Please provide your email');
//        $("#txtemail").focus();
//        return false;
//    }
//    else {
//        h.email = $("#txtemail").val();
//    }
//    if ($("#txtmob").val() == "") {
//        alert('Please provide your mobile number');
//        $("#txtmob").focus();
//        return false;
//    }
//    else {
//        h.mob = $("#txtmob").val();
//    }

//    if (result[0].idproofRequired == "true") {
//        h.idtype = $("#idproof>option:selected").val()
//        if ($("#txtcard").val() == "") {
//            alert('Please provide your ID card Number');
//            $("#txtcard").focus();
//            return false;
//        }
//        else {
//            h.idnum = $("#txtcard").val();
//        }
//        if ($("#txtaddress").val() == "") {
//            alert('Please provide your Address');
//            $("#txtaddress").focus();
//            return false;
//        }
//        else {
//            h.address = $("#txtaddress").val();
//        }
//    }
//    else {
//        if ($("#txtaddress").val() == "") {
//            alert('Please provide your Address');
//            $("#txtaddress").focus();
//            return false;
//        }
//        else {
//            h.address = $("#txtaddress").val();
//        }
//        h.idtype = "";
//        h.idnum = "";
//    }

//    if (confirm("Are you sure!")) {
//        $("#div_Submit").hide();
//        $("#div_Progress").show();
//        h.getBlockticketId();

//    }
//    else {
//        return false;
//    }



//});