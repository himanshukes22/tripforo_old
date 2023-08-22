var FlightHandler;

$(document).ready(function () {
    // FlightHandler = new FlightResult();
    // FlightHandler.BindEvents();
});



var FlightResult = function () {
    this.btnSendHtml = $("#btnSendHtml");
    this.btnSendMail = $("#btnSendMail");
    this.btnCancel = $("#btnCancel");
    this.btnFullDetails = $("#btnFullDetails");
    this.emailbtn = $(".emailbtn");


}
FlightResult.prototype.DocText = function (result, linenum, type) {

    var st = "";


    if (type == "S") {

        var fltOneWayArray = JSLINQ(result)
                      .Where(function (item) { return item.LineNumber == linenum; })
                      .Select(function (item) { return item });

        if (fltOneWayArray.items.length > 0) {
            st += '<table style="border:1px solid blue;width:643px;">';
            for (var i = 0; i < fltOneWayArray.items.length ; i++) {
                st += '<tr>';
                st += '<td  style="padding:5px 10px; text-align:center;width:91px;"><img src="../AirLogo/sm' + fltOneWayArray.items[i].ValiDatingCarrier + '.gif" /><br/><span>' + fltOneWayArray.items[i].ValiDatingCarrier + '</span> - ' + fltOneWayArray.items[i].FlightIdentification + '<br/><div class="airlineImage">' + fltOneWayArray.items[i].AirLineName + '</div></td>';
                st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Departure_Date + '<br/> ' + fltOneWayArray.items[i].DepartureTime + '<br/>' + fltOneWayArray.items[i].DepartureCityName + '</td>';
                st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Arrival_Date + '<br/> ' + fltOneWayArray.items[i].ArrivalTime + '<br/>' + fltOneWayArray.items[i].ArrivalCityName + '</td>';
                st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].TotDur + '</td>';
                if (i == 0) {
                    st += '<td style="padding:5px 10px; text-align:center;font-size:20px;white-space:nowrap;"><img src="../Images/rs.png"  style="margin-top:3px;float:left" /> ' + fltOneWayArray.items[i].TotalFare + '</td>';
                    st += '<td style="padding:5px 10px; text-align:center;0px;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Stops + '</td>';
                    st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].AdtFareType + '</td>';
                }
                else {
                    st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                    st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                    st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                }

                //st += '<td style="padding:5px 10px; text-align:center;width:0px;"></td>';
                //st += '<td style="padding:5px 10px; text-align:center;width:0px;"></td>';
                st += "</tr>";
            }
            st += "</table>";
        }
    }
    else if (type == "A") {

        try
        {
            for (var j = 0; j < result.length; j++) {
                var fltOneWayArray = JSLINQ(result)
                                   .Where(function (item) { return item.LineNumber == result[j].LineNumber; })
                                   .Select(function (item) { return item });

                if (fltOneWayArray.items.length > 0) {
                    st += '<table style="border:1px solid blue;width:643px;">';
                    for (var i = 0; i < fltOneWayArray.items.length ; i++) {
                        st += '<tr>';
                        st += '<td  style="padding:5px 10px; text-align:center;width:91px;"><img src="../AirLogo/sm' + fltOneWayArray.items[i].ValiDatingCarrier + '.gif" /><br/><span>' + fltOneWayArray.items[i].ValiDatingCarrier + '</span> - ' + fltOneWayArray.items[i].FlightIdentification + '<br/><div class="airlineImage">' + fltOneWayArray.items[i].AirLineName + '</div></td>';
                        st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Departure_Date + '<br/> ' + fltOneWayArray.items[i].DepartureTime + '<br/>' + fltOneWayArray.items[i].DepartureCityName + '</td>';
                        st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Arrival_Date + '<br/> ' + fltOneWayArray.items[i].ArrivalTime + '<br/>' + fltOneWayArray.items[i].ArrivalCityName + '</td>';
                        st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].TotDur + '</td>';
                        if (i == 0) {
                            st += '<td style="padding:5px 10px; text-align:center;font-size:20px;white-space:nowrap;"><img src="http://b2b.ITZ.com/Images/rs.png"  style="margin-top:3px;float:left" /> ' + fltOneWayArray.items[i].TotalFare + '</td>';
                            st += '<td style="padding:5px 10px; text-align:center;0px;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].Stops + '</td>';
                            st += '<td style="padding:5px 10px; text-align:center;white-space:nowrap;width:91px;">' + fltOneWayArray.items[i].AdtFareType + '</td>';
                        }
                        else {
                            st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                            st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                            st += '<td style="padding:5px 10px; text-align:center;width:91px;"></td>';
                        }

                        //st += '<td style="padding:5px 10px; text-align:center;width:0px;"></td>';
                        //st += '<td style="padding:5px 10px; text-align:center;width:0px;"></td>';
                        st += "</tr>";
                    }
                    st += "</table>";
                }
            }
        }
        catch (exx) {

            alert(exx);
        }
            

    }
    return st;


};


FlightResult.prototype.BindEvents = function () {
    var h = this;

    ////----------Popping up div for sending html result in doc file-----------------//
    h.btnSendHtml.click(function () {
        $("#lblMailStatus").text("");
        $("#lblMailStatus").hide();
        $("#hdnAllOrSelecte").val("");
        $("#hdnAllOrSelecte").val("Selected");
        //$("#divToSendMail").fadeIn(500);
    });
    ////----------End of Popping up div for sending html result in doc file-----------------//

    ////----------Popping up div for sending html result in doc file-----------------//
    h.emailbtn.click(function () {

        $("#lblMailStatus").text("");
        $("#lblMailStatus").hide();
        $("#hdnAllOrSelecte").val("");
        $("#hdnAllOrSelecte").val("Selected");
        //$("#lblMailStatus").text("");
        //$("#lblMailStatus").hide();
        //$("#hdnAllOrSelecte").val("");
        //$("#hdnAllOrSelecte").val("All");
        // $("#divToSendMail").fadeIn(500);
    });
    ////----------End of Popping up div for sending html result in doc file-----------------//



    ////--------------Sending mail after converting resulted html to doc file-----------------------//
    h.btnSendMail.click(function () {

        $("#lblMailStatus").text("");
        $("#lblMailStatus").hide();
        if ($("#txtFromMail").val() == "") {
            $("#txtFromMail").focus();
            alert("Please enter from email id");
            return false;
        }
        else {
            var isValid = isValidEmailAddress($("#txtFromMail").val());
            if (isValid == false) {
                $("#txtFromMail").focus();
                alert("Please enter valid email id");
                return false;
            }
        }

        if ($("#txtToMail").val() == "") {
            $("#txtToMail").focus();
            alert("Please enter sending email id");
            return false;
        }
        else {
            var isValid = isValidEmailAddress($("#txtToMail").val());
            if (isValid == false) {
                $("#txtToMail").focus();
                alert("Please enter a valid email id");
                return false;
            }
        }
        if ($("#txtSubj").val() == "") {
            $("#txtSubj").focus();
            alert("Please enter subject");
            return false;
        }
        if ($("#txtMsg").val() == "") {
            $("#txtMsg").focus();
            alert("Please enter message");
            return false;
        }

        if ($("input:radio[name='choices']:checked").val() == "S") {
            var cntChecked = 0;
            var cntChecked2 = 0;
            //////$.blockUI({ message: $('#waitMessage') }); //BLOCK WAIT STARTS
            //            $("#divabc").html("<img src='http://ITZ.com/images/loadingAnim.gif'/>");

            //            $("#divabc").show();
            var mailStr = "";
            var mailStr2 = "";
            if ($("#hdnOnewayOrRound").val() == "OneWay") {
                cntChecked = 0;
                mailStr = "";
                var ChdCheck = $('.list-item'); // $("#tblResult0_wrapper").find("input[class='CheckSub']");
                //alert(ChdCheck);
                var Fare = $("#tblResult0_wrapper").find("a[class='Fare']");
                var btnBook = $("#tblResult0_wrapper").find("input[class='Book']");

                if (ChdCheck.length > 0) {
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;width:415px;">';
                    mailStr += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr += '<th  style="padding:5px 5px; text-align:center;width:91px;">Airline</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Departure Date</th>';
                    // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Price</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Stops</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Fare type</th>';
                    mailStr += "</tr></thead><tbody>";
               
                    var cntChecked = 0;
                    for (var cnt = 0; cnt < ChdCheck.length; cnt++) {

                        var chkbox = $($(ChdCheck[cnt]).html()).find(".CheckSub").attr('Checked');
                        var lineno = $($(ChdCheck[cnt]).html()).find(".CheckSub").attr('rel');
                        if (chkbox != null && chkbox.toLowerCase() == "checked") {
                            mailStr += '<tr><td colspan="7">';
                            mailStr += h.DocText(resultG[0], lineno, "S");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                            mailStr += '</td></tr>';
                            cntChecked++;
                        }
                    }
                    mailStr += '</tbody></table>';
                }
            }

            if ($("#hdnOnewayOrRound").val() == "RoundTrip") {
                mailStr = "";
                var tableString = "";
                //tableString = $("#hdnMailString").val();
                var res1Check = $('.list-item'); // $("#showresult1").find("input[class='CheckSub']");
                var res1Fare = $("#showresult1").find("a[class='Fare']");
                //var res1btnBook = $("#showresult1").find("input[class='Book']");
                var res1THead = $("#showresult1").find("thead[class='headClass']");
                var res1Rdobtn = $("#showresult1").find("input[type='radio']");

                if (res1Check.length > 0) {
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;width:415px;">';
                    mailStr += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr += '<th  style="padding:5px 5px; text-align:center;width:91px;">Airline</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Departure Date</th>';
                    // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Price</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Stops</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Fare type</th>';
                    mailStr += "</tr></thead><tbody>";
                    if (res1Check.length > 0) {
                        for (var cnt = 0; cnt < res1Check.length; cnt++) {

                            var chkbox = $($(res1Check[cnt]).html()).find(".CheckSub").attr('Checked');
                            var lineno = $($(res1Check[cnt]).html()).find(".CheckSub").attr('rel');
                            if (chkbox != null && chkbox.toLowerCase() == "checked") {
                                mailStr += '<tr><td colspan="7">';
                                mailStr += h.DocText(resultG[0], lineno, "S");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                                mailStr += '</td></tr>';
                                cntChecked++;
                            }
                        }


                        mailStr += "</tbody></table>";
                    }
                }


                ////            var Fare =$("#tblResult0_wrapper").find("a[class='Fare']");
                ////            var btnBook = $("#tblResult0_wrapper").find("input[class='Book']");
                ////            var thead = $("#tblResult0_wrapper").find("thead[class='headClass']");

                var res2check = $('.list-itemR');// $("#showresult2").find("input[class='CheckSub']");
                var Fare = $("#showresult2").find("a[class='Fare']");
                //////var btnBook = $("#showresult2").find("input[class='Book']");
                var thead = $("#showresult2").find("thead[class='headClass']");
                var res2Radio = $("#showresult2").find("input[type='radio']");

                if (res2check.length > 0) {
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr2 = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;">';
                    mailStr2 += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Airline</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Departure Date</th>';
                    // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Price</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Stops</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Fare type</th>';
                    mailStr2 += "</tr></thead><tbody>";
                    if (res2check.length > 0) {
                        for (var cnt = 0; cnt < res2check.length; cnt++) {

                            var chkbox = $($(res2check[cnt]).html()).find(".CheckSub").attr('Checked');
                            var lineno = $($(res2check[cnt]).html()).find(".CheckSub").attr('rel');
                            if (chkbox != null && chkbox.toLowerCase() == "checked") {
                                mailStr2 += '<tr><td colspan="7">';
                                mailStr2 += h.DocText(resultG[1], lineno, "S");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                                mailStr2 += '</td></tr>';
                                cntChecked++;
                            }
                        }
                        mailStr2 += "</tbody></table>";
                    }
                }
            }




            
            var strTable = "";
            var completeInfo = "";
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/getAgncyDet",
                type: "POST",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Sending mail...");
                    //strTable = "<table id='tbltblResult0_wrapper' ><tr><td style='vertical-align:top'>" + mailStr + "</td></tr></table>";
                    if ($("#hdnOnewayOrRound").val() == "OneWay") {
                        completeInfo = "";
                        completeInfo = result + "<div>" + mailStr + "</div>";
                    }
                    else {
                        completeInfo = "";
                        completeInfo = "<div style='vertical-align:top;font-size:10px; font-weight:bold;'>" + result + "</div><div><table><tr><td>" + mailStr + "</td><td>" + mailStr2 + "</td></tr></table></div>";
                    }


                    $.ajax({
                        url: UrlBase + "FltSearch1.asmx/sendEnqMailTo",
                        type: "POST",
                        data: "{'emailFrom':'" + $("#txtFromMail").val() + "','emailTo':'" + $("#txtToMail").val() + "','divhtml':'" + escape(completeInfo) + "','subj':'" + $("#txtSubj").val() + "','msg':'" + $("#txtMsg").val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var result = data.d;
                            if (result == "Success") {
                                $("#txtFromMail").val("");
                                $("#txtToMail").val("");
                                $("#txtSubj").val("");
                                $("#txtMsg").val("");
                                $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                                $("#lblMailStatus").text("Mail sent successfully");
                                $("#lblMailStatus").show();
                                //////$("#divToSendMail").fadeOut(500);
                                $(".CheckSub").attr("checked", false);
                                //////$("#divabc").hide();
                            }
                            else {
                                $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                                $("#lblMailStatus").text("Mail could not sent please try again.");
                                $("#lblMailStatus").show();
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            ////alert("Mail could not sent at the moment");
                            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                            $("#lblMailStatus").text("Mail could not sent please try again");
                            $("#lblMailStatus").show();
                        }
                    });

                }
            });

            // }

        }
        else {


            var cntChecked = 0;
            var cntChecked2 = 0;
            //////$.blockUI({ message: $('#waitMessage') }); //BLOCK WAIT STARTS
            //            $("#divabc").html("<img src='http://ITZ.com/images/loadingAnim.gif'/>");

            //            $("#divabc").show();
            var mailStr = "";
            var mailStr2 = "";
            if ($("#hdnOnewayOrRound").val() == "OneWay") {
                    mailStr = "";                           
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;width:415px;">';
                    mailStr += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr += '<th  style="padding:5px 5px; text-align:center;width:91px;">Airline</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Departure Date</th>';
                // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Price</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Stops</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Fare type</th>';
                    mailStr += "</tr></thead><tbody>";
                    mailStr += '<tr><td colspan="7">';
                    mailStr += h.DocText(resultG[0], lineno, "A");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                    mailStr += '</td></tr>';
                    mailStr += "</tbody></table>";
                  
                
            }

            if ($("#hdnOnewayOrRound").val() == "RoundTrip") {
                mailStr = "";
              
              
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;width:415px;">';
                    mailStr += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr += '<th  style="padding:5px 5px; text-align:center;width:91px;">Airline</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Departure Date</th>';
                    // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Price</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Stops</th>';
                    mailStr += '<th style="padding:5px 5px; text-align:center;width:91px;">Fare type</th>';
                    mailStr += "</tr></thead><tbody>";
                    mailStr += '<tr><td colspan="7">';
                    mailStr += h.DocText(resultG[0], lineno,"A");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                    mailStr += '</td></tr>';
                    mailStr += "</tbody></table>";

                



              
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Processing....");
                    $("#lblMailStatus").show();
                    mailStr2 = '<table><tr><hr style="width:415px;float:left;color:#000;margin-left:0px" /></tr></table><table cellspacing="0" cellpadding="10" style="width:643px;background-image: url(http://localhost:11043/springb2blatest03nov12/Images/Fbreak.jpg);background-repeat: repeat-x; border:thin solid #0d8204;">';
                    mailStr2 += '<thead style="background-color:green"><tr style="color:blue; font-weight:bold;">';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Airline</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Departure Date</th>';
                    // mailStr += '<th style="padding:5px 10px; text-align:center;">Departure Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Arrival Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Journey Time</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Price</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Stops</th>';
                    mailStr2 += '<th style="padding:5px 10px; text-align:center;width:91px;">Fare type</th>';
                    mailStr2 += "</tr></thead><tbody>";
                    mailStr2 += '<tr><td colspan="7">';
                    mailStr2 += h.DocText(resultG[1], lineno, "A");//$(ChdCheck[cnt]).html();//ChdCheck[cnt].parentElement.parentElement.parentElement.parentElement.outerHTML.replace(ChdCheck[cnt].outerHTML, '').replace(Fare[cnt].outerHTML, '').replace(btnBook[cnt].outerHTML, '');
                    mailStr2 += '</td></tr>';
                    mailStr2 += "</tbody></table>";
                
            }
            
         
            var strTable = "";
            var completeInfo = "";
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/getAgncyDet",
                type: "POST",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    $("#lblMailStatus").text("");
                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                    $("#lblMailStatus").text("Sending mail...");
                    //strTable = "<table id='tbltblResult0_wrapper' ><tr><td style='vertical-align:top'>" + mailStr + "</td></tr></table>";
                    if ($("#hdnOnewayOrRound").val() == "OneWay") {
                        completeInfo = "";
                        completeInfo = result + "<div>" + mailStr + "</div>";
                    }
                    else {
                        completeInfo = "";
                        completeInfo = "<div style='vertical-align:top;font-size:10px; font-weight:bold;'>" + result + "</div><div><table><tr><td>" + mailStr + "</td><td>" + mailStr2 + "</td></tr></table></div>";
                    }


                    $.ajax({
                        url: UrlBase + "FltSearch1.asmx/sendEnqMailTo",
                        type: "POST",
                        data: "{'emailFrom':'" + $("#txtFromMail").val() + "','emailTo':'" + $("#txtToMail").val() + "','divhtml':'" + escape(completeInfo) + "','subj':'" + $("#txtSubj").val() + "','msg':'" + $("#txtMsg").val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var result = data.d;
                            if (result == "Success") {
                                $("#txtFromMail").val("");
                                $("#txtToMail").val("");
                                $("#txtSubj").val("");
                                $("#txtMsg").val("");
                                $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                                $("#lblMailStatus").text("Mail sent successfully");
                                $("#lblMailStatus").show();
                                //////$("#divToSendMail").fadeOut(500);
                                $(".CheckSub").attr("checked", false);
                                //////$("#divabc").hide();
                            }
                            else {
                                $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                                $("#lblMailStatus").text("Mail could not sent please try again.");
                                $("#lblMailStatus").show();
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            ////alert("Mail could not sent at the moment");
                            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
                            $("#lblMailStatus").text("Mail could not sent please try again");
                            $("#lblMailStatus").show();
                        }
                    });

                }
            });


        }




        //////if ($("#hdnAllOrSelecte").val() == "All") {
        //else {
        //    //$("#div_Progress").show();
        //    var tableString = "";
        //    //////$.blockUI({ message: $('#waitMessage') }); //BLOCK WAIT STARTS
        //    if ($("#hdnOnewayOrRound").val() == "RoundTrip") {
        //        tableString = $("#hdnMailString").val();
        //        var res1Check = $("#showresult1").find("input[class='CheckSub']");
        //        var res1Fare = $("#showresult1").find("a[class='Fare']");
        //        var res1btnBook = $("#showresult1").find("input[class='Book']");
        //        var res1THead = $("#showresult1").find("thead[class='headClass']");
        //        var res1Rdobtn = $("#showresult1").find("input[type='radio']");

        //        if (res1Check.length > 0) {
        //            $("#lblMailStatus").text("");
        //            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //            $("#lblMailStatus").text("Processing...");
        //            $("#lblMailStatus").show();
        //            //                    if (res1btnBook.length > 0) {
        //            //                        for (var k = 0; k < res1Check.length; k++) {
        //            //                            tableString = tableString.replace(res1Check[k].outerHTML, "").replace(res1Fare[k].outerHTML, "").replace(res1btnBook[k].outerHTML, "");
        //            //                        }
        //            //                    }
        //            //                    else {
        //            for (var k = 0; k < res1Check.length; k++) {
        //                tableString = tableString.replace(res1Check[k].outerHTML, "").replace(res1Fare[k].outerHTML, "").replace(res1Rdobtn[k].outerHTML, "");
        //            }
        //            //                    }
        //        }


        //        ////            var Fare = $("#tblResult0_wrapper").find("a[class='Fare']");
        //        ////            var btnBook = $("#tblResult0_wrapper").find("input[class='Book']");
        //        ////            var thead = $("#tblResult0_wrapper").find("thead[class='headClass']");

        //        var res2check = $("#showresult2").find("input[class='CheckSub']");
        //        var Fare = $("#showresult2").find("a[class='Fare']");
        //        var btnBook = $("#showresult2").find("input[class='Book']");
        //        var thead = $("#showresult2").find("thead[class='headClass']");
        //        var res2Radio = $("#showresult2").find("input[type='radio']");

        //        if (res2check.length > 0) {
        //            $("#lblMailStatus").text("");
        //            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //            $("#lblMailStatus").text("Processing...");
        //            $("#lblMailStatus").show();
        //            //                    if (btnBook.length > 0) {
        //            //                        for (var l = 0; l < res2check.length; l++) {
        //            //                            tableString = tableString.replace(Fare[l].outerHTML, "").replace(btnBook[l].outerHTML, "").replace(res2check[l].outerHTML, "");
        //            //                        }
        //            //                    }
        //            //                    else {
        //            for (var l = 0; l < res2check.length; l++) {
        //                tableString = tableString.replace(Fare[l].outerHTML, "").replace(res2check[l].outerHTML, "").replace(res2Radio[l].outerHTML, "");
        //            }
        //            //                    }
        //        }
        //    }
        //    if ($("#hdnOnewayOrRound").val() == "OneWay") {
        //        tableString = "";
        //        tableString = $("#hdnMailString").val();
        //        var Fare = $("#tblResult0_wrapper").find("a[class='Fare']");
        //        var btnBook = $("#tblResult0_wrapper").find("input[class='Book']");
        //        var thead = $("#tblResult0_wrapper").find("thead[class='headClass']");
        //        var cheOneWay = $("#tblResult0_wrapper").find("input[class='CheckSub']");
        //        if (cheOneWay.length > 0) {
        //            $("#lblMailStatus").text("");
        //            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //            $("#lblMailStatus").text("Processing...");
        //            $("#lblMailStatus").show();
        //            for (var ctg = 0; ctg < cheOneWay.length; ctg++) {
        //                try
        //                {
        //                    tableString = tableString.replace(Fare[ctg].outerHTML, "").replace(btnBook[ctg].outerHTML, "").replace(cheOneWay[ctg].outerHTML, "");
        //                }
        //                catch (Err)
        //                { }
        //            }
        //        }

        //    }


        //    //            if (btnBook.length != 0) {
        //    //                for (var ct = 0; ct < $(".CheckSub").length; ct++) {
        //    //                    tableString = tableString.replace($(".CheckSub")[ct].outerHTML, "").replace(Fare[ct].outerHTML, "").replace(btnBook[ct].outerHTML, "");
        //    //                }
        //    //            }
        //    //            else {
        //    //                for (var ct = 0; ct < $(".CheckSub").length; ct++) {
        //    //                    tableString = tableString.replace($(".CheckSub")[ct].outerHTML, "").replace(Fare[ct].outerHTML, "");
        //    //                }
        //    //            }

        //    //////tableString = tableString.replace(thead[0].outerHTML, "<thead class='headClass' style='background-color:green'><tr style='color:blue; font-weight:bold;'><th align='center' style='padding:5px 10px; text-align:center;'>Airline</th><th style='padding:5px 10px; text-align:center;'>Departure Date</th><th style='padding:5px 10px; text-align:center;'>Departure Time</th><th style='padding:5px 10px; text-align:center;'>Arrival Time</th><th style='padding:5px 10px; text-align:center;'>Journey Time</th><th style='padding:5px 10px; text-align:center;'>Price</th><th style='padding:5px 10px; text-align:center;width:0px;'></th><th style='padding:5px 10px; text-align:center;width:0px;'></th></tr></thead>");
        //    /////tableString = tableString.replace("<THEAD class=headClass sizcache='0'>", "ooo");
        //    $.ajax({
        //        url: UrlBase + "FltSearch1.asmx/getAgncyDet",
        //        type: "POST",
        //        data: "{}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            var result = data.d;
        //            $("#lblMailStatus").text("");
        //            $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //            $("#lblMailStatus").text("Sending mail...");
        //            //strTable = "<table id='tbltblResult0_wrapper' ><tr><td style='vertical-align:top'>" + mailStr + "</td></tr></table>";
        //            tableString = tableString.replace("<tbody><tr><td valign='top'>", "");
        //            tableString = tableString.replace("");
        //            var completeInfo = "<div style='vertical-align:top;font-size:10px; font-weight:bold;'>" + result + "</div><hr/><div style='vertical-align:top;font-size:10px; font-weight:bold;'>" + tableString + "</div>";


        //            $.ajax({
        //                url: UrlBase + "FltSearch1.asmx/sendEnqMailTo",
        //                type: "POST",
        //                data: "{'emailFrom':'" + $("#txtFromMail").val() + "','emailTo':'" + $("#txtToMail").val() + "','divhtml':'" + escape(completeInfo) + "','subj':'" + $("#txtSubj").val() + "','msg':'" + $("#txtMsg").val() + "'}", // Data in Array or Listformat //SYNTAX -2 data: "{a:" + JSON.stringify(items) + "}",
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                success: function (data) {
        //                    var result = data.d;
        //                    if (result == "Success") {
        //                        $("#txtFromMail").val("");
        //                        $("#txtToMail").val("");
        //                        //////$("#divToSendMail").fadeOut(500);
        //                        //                                $(".CheckSub").attr("style", "display:block");
        //                        $(".CheckSub").attr("checked", false);
        //                        //alert("mail sent");
        //                        $("#txtSubj").val("");
        //                        $("#txtMsg").val("");
        //                        $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //                        $("#lblMailStatus").text("Mail sent successfully");
        //                        $("#lblMailStatus").show();
        //                        $("#waitMessage").hide();
        //                    }
        //                    else {
        //                        $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //                        $("#lblMailStatus").text("Mail could not sent please try again");
        //                        $("#lblMailStatus").show();
        //                    }
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    //////alert("Mail could not sent at the moment");
        //                    $("#lblMailStatus").attr("style", "font-size:16px;color:Red");
        //                    $("#lblMailStatus").text("Mail could not sent please try again");
        //                    $("#lblMailStatus").show();
        //                }
        //            });



        //        }
        //    });
        //    //            var tableString1 = tableString.replace($(".CheckAll")[0].outerHTML, "");
        //    //var cmpltInfo = "<div id='divcompltmailinfo' style='border:thin solid #0d8204;'>" + $("#henAgcDetails").text() + tableString1 + "</div>";
        //    //////////            $("#div_Progress").hide();
        //    //////////            $(document).ajaxStop($.unblockUI);

        //}


    });

    ////--------------End of  Sending mail after converting resulted html to doc file-----------------------//



    //---------------Cancelling of  Sending mail and getting the value of text boxes to empty------//
    h.btnCancel.click(function () {
        $("#txtFromMail").val("");
        $("#txtToMail").val("");
        $("#divToSendMail").fadeOut(500);
    });
    //---------------End of Cancelling of  Sending mail and getting the value of text boxes to empty------//




    $("a.topopup").click(function () {
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup(); // function show popup 
        }, 500); // .5 second
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

    $("div.close").click(function () {
        disablePopup();  // function close pop up
    });

    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup();  // function close pop up
        }
    });

    $('a.livebox').click(function () {
        alert('Hello World!');
        return false;
    });


    /************** start: functions. **************/
    function loading() {
        $("div.loader").show();
    }
    function closeloading() {
        $("div.loader").fadeOut('normal');
    }

    var popupStatus = 0; // set value

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


}


function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
}


//////////////if ($(".CheckAll").attr("checked") == true) {
//////////////    //$(".CheckSub").attr("style", "display:none");
//////////////    var tableString = $("#hdnMailString").val();
//////////////    for (var ct = 0; ct < $(".CheckSub").length; ct++) {
//////////////        tableString = tableString.replace($(".CheckSub")[ct].outerHTML, "");
//////////////    }
//////////////    var tableString1 = tableString.replace($(".CheckAll")[0].outerHTML, "");
//////////////    var cmpltInfo = "<div id='divcompltmailinfo' style='border:thin solid #0d8204;'>" + $("#henAgcDetails").text() + tableString1 + "</div>";
//////////////    $.ajax({
//////////////        url: UrlBase + "FltSearch1.asmx/sendEnqMailTo",
//////////////        type: "POST",
//////////////        data: "{'emailFrom':'" + $("#txtFromMail").val() + "','emailTo':'" + $("#txtToMail").val() + "','divhtml':'" + escape(tableString1) + "'}", // Data in Array or Listformat //SYNTAX -2 data: "{a:" + JSON.stringify(items) + "}",
//////////////        contentType: "application/json; charset=utf-8",
//////////////        dataType: "json",
//////////////        success: function(data) {
//////////////            var result = data.d;
//////////////            if (result == "Success") {
//////////////                $("#txtFromMail").val("");
//////////////                $("#txtToMail").val("");
//////////////                $("#divToSendMail").fadeOut(500);
//////////////                ////$(".CheckSub").attr("style", "display:block");
//////////////                $(".CheckSub").attr("checked", false);
//////////////                alert("mail sent");
//////////////            }
//////////////            else {
//////////////                alert("Please try again");
//////////////            }
//////////////        },
//////////////        error: function(XMLHttpRequest, textStatus, errorThrown) {
//////////////            alert("Mail could not sent at the moment");
//////////////        }
//////////////    });
//////////////}