<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="TravelCalender.aspx.vb" Inherits="SprReports_Agent_TravelCalender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <link href="<%=ResolveUrl("~/Styles/fullcalendar.css") %>" rel='stylesheet' />

    <script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/fullcalendar.min.js") %>"></script>
    <link href="../../CSS/jquery-ui-1.8.8.custom.css" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/PopupStyle.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PopupScript.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
    <script src="../../Scripts/Search3_cel.js"></script>

  

    <%--  <script src="../../Scripts/jquery-ui-1.8.8.custom.min.js"></script>
          <script src="../../Scripts/jquery-1.4.4.min.js"></script>
          <script src="../../Scripts/change.min.js"></script>--%>
    <script type="text/javascript">

        function closex() {

            $('#myModal').hide();
        }

        function filter_Cal() {
            debugger;
            var VPNR = $('#ctl00_ContentPlaceHolder1_txt_PNR').val()
            var VPAXNAME = $('#ctl00_ContentPlaceHolder1_txt_PaxName').val()
            var DEPCITY = $('#hidtxtDepCity1').val()
            var ARRCITY = $('#hidtxtArrCity1').val()
            var AIRLINE = $('#hidtxtAirline').val()
            var Agentid = $('#txtAgencyName').val()
            var loginid = '<%=  Session("UID") %>';
            var user_Type = '<%= Session("User_Type").ToString%>';

            if (Agentid === undefined) {
                Agentid = "";
            }

            if (VPAXNAME != "" && $("#ddmonth").val() == " ") {
                alert('Please Select Month')
                return false;

            }

            if ($("#ddmonth").val() == " ") {
                var date = new Date();
                var ddmonth = date.getMonth();
                ddmonth = ddmonth + 1
            }
            else {
                var e = document.getElementById("ddmonth");
                var ddmonth = e.options[e.selectedIndex].value;


            }

            if ($("#ddyear").val() == " ") {
                var date = new Date();
                var ddyear1 = date.getFullYear();
                var ddyear = ddyear1.toString().substring(2)
            }
            else {
                var e = document.getElementById("ddyear");
                var ddyear = e.options[e.selectedIndex].value;
            }



            //var date = new Date();
            //var d = date.getDate();
            //var m = date.getMonth();
            //var y = date.getFullYear();
            // var m1 = m + 1;
            GetCalender_fltr(Agentid, loginid, ddmonth, user_Type, ddyear, VPNR, VPAXNAME, DEPCITY, ARRCITY, AIRLINE)

        }

        function GetCalender_fltr(Agentid, loginid, ddmonth, user_Type, ddyear, VPNR, VPAXNAME, DEPCITY, ARRCITY, AIRLINE) {
            debugger;
            var yr = new Date().getFullYear();

            if (ddmonth >= 12) {
                if (ddmonth == 12) {
                    //                month1 = parseInt(month1 - 11);
                    //                yr = parseInt(yr + 1);

                    //                validDate = yr.toString() + "-" + month1.toString() + "-1"
                }
                else {
                    ddmonth = parseInt(ddmonth - 12);
                    yr = parseInt(yr + 1);

                    validDate = yr.toString() + "-" + ddmonth.toString() + "-1"
                }

            }
            // alert(validDate);
            //            validDate = yr.toString() + "-" + parseInt(month1 + 1).toString() + "-1"
            //            var validdate = validDate.split('-');
            //            if (parseInt(validdate[1]) == 0) {

            //                validDate = validdate[0] + "-" + (parseInt(validdate[1]) + 1).toString() + "-" + validdate[2];
            //            }

            var eventDataArray = new Array();


            $.ajax({
                //Absolute Url name TravelCalender.aspx

                //url: "../../AgencySearch.asmx/GetFlightDetails_new_fltr",

                url: "../../TravelCalSrv.asmx/GetFlightDetails_new_fltr",
                // url: "../../AgencySearch.asmx/GetFlightDetails",  oldlink
                //url: "TravelCalender.aspx/GetFlightDetails",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{'Agentid':' " + Agentid + "','loginid':'" + loginid + "','user_Type':'" + user_Type + "','ddmonth':'" + ddmonth + "','ddyear':'" + ddyear + "','VPNR':'" + VPNR + "','VPAXNAME':'" + VPAXNAME + "','DEPCITY':'" + DEPCITY + "','ARRCITY':'" + ARRCITY + "','AIRLINE':'" + AIRLINE + "'}",
                success: function (data) {
                    debugger;
                    if (data.d.length == 0) {
                        $('#calendar').fullCalendar('destroy');
                    }
                    else {
                        for (var i = 0; i < data.d.length; i++) {
                            var date = convertToDate(data.d[i].DepDate);
                            var TTmonth = ""
                            var TTYear = ""
                            if (VPNR != '') {
                                TTmonth = data.d[i].DepDate
                                TTmonth = TTmonth.substring(2, 4);

                                TTYear = data.d[i].DepDate
                                TTYear = TTYear.substring(4, 6);
                            }
                            else {
                                TTmonth = ddmonth
                                TTYear = ddyear
                            }
                            eventDataArray.push({
                                title: +data.d[i].Pnr + " Booking(s)",
                                // title: "DepTime: " + data.d[i].DepTime.splice(2, 0, ":") + "Hrs" + "\n   " + data.d[i].OrderID + "\n  Sector: " + data.d[i].Sector,
                                start: date,
                                // <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                //link: "../../Report/PnrSummary_Intl.aspx?OrderId=" + data.d[i].OrderID + " &TransID="
                                link: "../../Report/TicketCalender.aspx?DepDate=" + data.d[i].DepDate + "&loginid=" + loginid + "&user_Type=" + user_Type + "&Agentid=" + Agentid + "&ddmonth=" + ddmonth + "&ddyear=" + ddyear + "&VPNR=" + VPNR + "&VPAXNAME=" + VPAXNAME + "&DEPCITY=" + DEPCITY + "&ARRCITY=" + ARRCITY + "&AIRLINE=" + AIRLINE + ""
                                //  link: "../../Report/Ticket_Report.aspx?DepDate=" + data.d[i].DepDate + "&TransID=" // rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                // onclick: "<div onclick='popitup(" + data.d[i].DepDate + ")'> When you click this, it will pop up</div>"
                                //link: "../../Report/PnrSummaryIntl.aspx?OrderId=" + data.d[i].OrderID + " &TransID="
                            });



                        }


                        $('#calendar').fullCalendar('destroy');
                        $('#calendar').fullCalendar({
                            header: {
                                left: '  ',
                                center: 'title',
                                right: ' '
                            },
                            year: '20' + TTYear,
                            month: TTmonth - 1,
                            events: eventDataArray,
                            eventClick: function(calEvent, jsEvent, view) {
                           // eventMouseover: function (calEvent, jsEvent, view) {

                                //  window.open(calEvent.link, "_blank", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, height=600,width=1000, left=100px,top=200px");
                                // alert('Event: ' + calEvent.link);
                                // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                                //   alert('View: ' + view.name);
                                // change the border color just for fun
                                //$(this).css('border-color', 'red');

                                var blogUrl;
                                var fnlURL;
                                //blogUrl = "" + UrlBase + "Report/PnrSummaryIntl.aspx?OrderId=" + orderid + "%20&TransID=";
                                $('#IfrmTKT').attr('src', calEvent.link);
                                $('#hhh').show();
                                $('#myModal').show();


                            },
                            //eventClick: function (calEvent, jsEvent, view) {
                            //    if (calEvent.title == "MORE...") {

                            //        GetToolTip(agentId, calEvent.link, jsEvent.pageY - 150, jsEvent.pageX - 100);
                            //    }

                            //    // window.open(calEvent.link, "_blank");
                            //    // alert('Event: ' + calEvent.link);
                            //    // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                            //    //   alert('View: ' + view.name);

                            //    // change the border color just for fun
                            //    //$(this).css('border-color', 'red');

                            //}


                        });



                    }


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#calendar').fullCalendar('destroy');
                    //alert(textStatus);
                }

            });

        }




        $(document).ready(function () {
            debugger;
            var date = new Date(); //TT
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();
            var m1 = m + 1;
            var loginid = '<%= Session("UID")%>';
            var user_Type = '<%= Session("User_Type").ToString%>';
            var Agentid = "";
            GetCalender(loginid, user_Type, Agentid, y.toString() + "-" + m.toString() + "-1", m);
            var newMonth = date.getMonth();

            //        $('#calendar').fullCalendar({
            //            header: {
            //                // left:''// 'prev today',
            //                center: 'title'//,
            //                //  right:'' //'next'
            //            },
            //            editable: true
            //        });

            $('#PrevButton').attr("disabled", true);

            $('#NextButton').click(function () {
                var compDate = new Date();

                newMonth = newMonth + 1;
                var newDate = new Date(y, newMonth, 1);
                var newmonth1 = newMonth + 1;
                var newDate1 = new Date(y, newMonth, compDate.getDate());
                //alert(newDate);
                // $('#calendar').fullCalendar('destroy');
                // $('#calendar').fullCalendar('next');
                GetCalender(loginid, user_Type, Agentid, y.toString() + "-" + newMonth.toString() + "-1", newMonth);
                if (newDate1.toDateString() == compDate.toDateString()) {
                    // alert(compDate);
                    $('#PrevButton').attr("disabled", true);

                }
                else {
                    $('#PrevButton').attr("disabled", false);
                }



            });

            $('#PrevButton').click(function () {
                var compDate = new Date();


                newMonth = newMonth - 1;
                var newDate = new Date(y, newMonth, 1);
                var newDate1 = new Date(y, newMonth, compDate.getDate());
                // alert(newDate1.toDateString());
                // alert(compDate.toDateString());

                //alert(compDate.toDateString());
                //alert(newDate1.toDateString());

                //alert(newDate);
                // $('#calendar').fullCalendar('destroy');
                // $('#calendar').fullCalendar('next');
                GetCalender(loginid, user_Type, Agentid, y.toString() + "-" + newMonth.toString() + "-1", newMonth);
                if (newDate1.toDateString() == compDate.toDateString()) {

                    $('#PrevButton').attr("disabled", true);
                }
                else {
                    $('#PrevButton').attr("disabled", false);

                }

            });


        });



        function GetCalender(loginid, user_Type, Agentid, curentdate, month1) {
            debugger;
            var yr = new Date().getFullYear();

            var validDate = curentdate;  //TT
            if (month1 >= 12) {
                if (month1 == 12) {
                    //                month1 = parseInt(month1 - 11);
                    //                yr = parseInt(yr + 1);

                    //                validDate = yr.toString() + "-" + month1.toString() + "-1"
                }
                else {
                    month1 = parseInt(month1 - 12);
                    yr = parseInt(yr + 1);

                    validDate = yr.toString() + "-" + month1.toString() + "-1"
                }

            }
            // alert(validDate);
            //            validDate = yr.toString() + "-" + parseInt(month1 + 1).toString() + "-1"
            //            var validdate = validDate.split('-');
            //            if (parseInt(validdate[1]) == 0) {

            //                validDate = validdate[0] + "-" + (parseInt(validdate[1]) + 1).toString() + "-" + validdate[2];
            //            }

            var eventDataArray = new Array();


            $.ajax({
                //Absolute Url name TravelCalender.aspx
                url: "../../TravelCalSrv.asmx/GetFlightDetails_new",
               // url: "../../AgencySearch.asmx/GetFlightDetails_new",
                // url: "../../AgencySearch.asmx/GetFlightDetails",  oldlink
                //url: "TravelCalender.aspx/GetFlightDetails",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{'loginid':' " + loginid + "','user_Type':'" + user_Type + "','agentID':' " + Agentid + "','date1':'" + validDate + "'}",
                success: function (data) {
                    debugger;
                    for (var i = 0; i < data.d.length; i++) {
                        var date = convertToDate(data.d[i].DepDate);
                        //if (data.d[i].Sector == "more") {

                        //    // alert(data.d[i].Sector);
                        //    var mon = date.getMonth() + 1;
                        //    eventDataArray.push({
                        //        title: 'MORE...',
                        //        start: date,
                        //        link: date.getFullYear().toString() + "-" + mon.toString() + "-" + date.getDate().toString()


                        //    });

                        //}
                        // else {

                        eventDataArray.push({
                            title: +data.d[i].Pnr + " Booking(s)",
                            // title: "DepTime: " + data.d[i].DepTime.splice(2, 0, ":") + "Hrs" + "\n   " + data.d[i].OrderID + "\n  Sector: " + data.d[i].Sector,
                            start: date,
                            // <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                //link: "../../Report/PnrSummary_Intl.aspx?OrderId=" + data.d[i].OrderID + " &TransID="
                            link: "../../Report/TicketCalender.aspx?DepDate=" + data.d[i].DepDate + "&loginid=" + loginid + "&user_Type=" + user_Type + "&Agentid=" + Agentid + "&ddmonth=&ddyear=&VPNR=&VPAXNAME=&DEPCITY=&ARRCITY=&AIRLINE="// rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                // onclick: "<div onclick='popitup(" + data.d[i].DepDate + ")'> When you click this, it will pop up</div>"
                                //link: "../../Report/PnrSummaryIntl.aspx?OrderId=" + data.d[i].OrderID + " &TransID="
                            });
                            //  }

                            //                    if (i > 0) {
                            //                        if (data.d[i - 1].DepDate != data.d[i].DepDate) {
                            //                            if (i < data.d.length) {
                            //                                if (data.d[i].DepDate == data.d[i + 1].DepDate) {
                            //                                    alert(data.d[i].DepDate + ", " + data.d[i + 1].DepDate);
                            //                                    var date1 = convertToDate(data.d[i - 1].DepDate);
                            //                                    var mon = date1.getMonth() + 1;
                            //                                    eventDataArray.push({
                            //                                        title: 'MORE...',
                            //                                        start: date1,
                            //                                        link: date1.getFullYear().toString() + "-" + mon.toString() + "-" + date1.getDate().toString()


                            //                                    });

                            //                                }

                            //                            }



                            //                        }

                            //                    }


                        }


                    $('#calendar').fullCalendar('destroy');
                    $('#calendar').fullCalendar({
                        header: {
                            left: '  ',
                            center: 'title',
                            right: ' '
                        },
                        year: yr,
                        month: month1,
                        events: eventDataArray,
                        eventClick: function(calEvent, jsEvent, view) {
                       // eventMouseover: function (calEvent, jsEvent, view) {
                         
                            //  window.open(calEvent.link, "_blank", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, height=600,width=1000, left=100px,top=200px");
                            // alert('Event: ' + calEvent.link);
                            // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                            //   alert('View: ' + view.name);
                            // change the border color just for fun
                            //$(this).css('border-color', 'red');

                            var blogUrl;
                            var fnlURL;
                            //blogUrl = "" + UrlBase + "Report/PnrSummaryIntl.aspx?OrderId=" + orderid + "%20&TransID=";
                            $('#IfrmTKT').attr('src', calEvent.link);
                            $('#hhh').show();
                            $('#myModal').show();
                        },
                        //eventClick: function (calEvent, jsEvent, view) {
                        //    if (calEvent.title == "MORE...") {

                        //        GetToolTip(agentId, calEvent.link, jsEvent.pageY - 150, jsEvent.pageX - 100);
                        //    }

                        //    // window.open(calEvent.link, "_blank");
                        //    // alert('Event: ' + calEvent.link);
                        //    // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                        //    //   alert('View: ' + view.name);

                        //    // change the border color just for fun
                        //    //$(this).css('border-color', 'red');

                        //}


                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#calendar').fullCalendar('destroy');
                    //alert(textStatus);
                }

            });


            }

            function convertToDate(DepDate) {

                var d = DepDate.substring(0, 2);
                var m = DepDate.substring(2, 4);
                var y = "20" + DepDate.substring(4, 6);
                var date = new Date(y, m - 1, d);
                return date;

            }

            String.prototype.splice = function (idx, rem, s) {
                return (this.slice(0, idx) + s + this.slice(idx + Math.abs(rem)));
            };


            function GetToolTip(agentId, date, top, left) {






                //alert(document.location.href);
                //	            // alert("hi");
                //	            var existip = $("#tooltip");

                //	            if (existip != null) {
                //	                if ($("#tooltip").show() == true) {

                //	                    $("#tooltip").hide()
                //	                }
                //	                else {
                //	                    $("#tooltip").show()
                //	                }
                //	            }


                //   alert(param);
                // var param1 = param.split(',');
                //  alert(param1[0] + param1[1]);
                //$("#tooltip").css("display:none;");


                $.ajax({
                    url: "../../AgencySearch.asmx/GetFlightDetailsByDateForCal",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: "{'agentID':' " + agentId + "','date1':'" + date + "'}",

                    success: function (data) {


                        var targetDiv = $('[rel~=tip2]');

                        if (targetDiv.length > 0) {
                            targetDiv.remove();


                        }

                        var targetDiv = $('[rel~=tip1]');

                        if (targetDiv.length > 0) {
                            targetDiv.remove();
                            // $('body').append('<div id="tooltip" rel="hi1"></div>');

                        }
                        //alert("hi");

                        var result = data.d;
                        var table = "";


                        ////table = "<a href='javascript:rem1();' style='color:#fff'>X</a><br/><table  width='300px' height='150px' cellpadding='0' cellspacing='0'>"
                        table = "<a href='javascript:rem1();' style='color:#000;font-size:14px;text-decoration:none;'><img src='../../Utility/img/basic/x.png' style='text-decoration:none;border:0;float:right;' /></a><table cellpadding='5' cellspacing='0'>"
                        table += "<tr style='font-size:14px;font-weight:bold;'><td>SNO</td><td>&nbsp;DEP TIME</td><td></td><td style='text-align:left;'>ORDERID</td><td></td><td>SECTOR</td></tr>"
                        for (var i = 0; i < result.length; i++) {

                            table += "<tr id='" + i + "'>";
                            table += "<td style='font-size:12px;font-weight:bold;' >" + parseInt(i + 1).toString() + "&nbsp;&nbsp;</td><td style='font-size:12px'>" + result[i].DepTime.splice(2, 0, ":") + "Hrs" + "</td><td>&nbsp;</td>";

                            table += "<td style='font-size:12px'><a target='_blank'  href=../../Report/PnrSummaryIntl.aspx?OrderId=" + data.d[i].OrderID + " &TransID=>" + result[i].OrderID + " </a></td></td><td>&nbsp;</td>";
                            table += "<td style='font-size:12px'>" + result[i].Sector + "<br/></td>";
                            table += "</tr>";
                        }
                        // table += "<tr><td align='left'><a href='PkgDetails.aspx?ID=" + param + "&price=" + result[0].PkgPrice + "#reviews' style='color:#fff;font-size:10px'>Read all reviews </a></td></tr>"
                        table += "</table>";

                        // alert(table);





                        tip = table; //"<a href='javascript:rem();'>X</a><br/><a href='PkgDetails.aspx?ID=" + param1[0] + "'>View Details</a>";



                        tooltip = $('<div id="tooltip"   rel="tip2"></div>');
                        if (!tip || tip == '')
                            return false;

                        //  target.removeAttr('alt');
                        tooltip.css('opacity', 0)
                           .html(tip)
                           .appendTo('body');

                        tooltip.focus();
                        var init_tooltip = function () {
                            // alert(e.pageY);
                            var yTop = 0;
                            if (window.event == null) {
                                // alert(e.pageY);
                                // yTop = e.pageY - 100;

                            }
                            else {
                                //yTop = window.event.clientY;
                            }
                            // yTop = window.event.clientY;
                            // alert(yTop);
                            // e = window.event || e;
                            if ($(window).width() < tooltip.outerWidth() * 1.5)
                                tooltip.css('max-width', $(window).width() / 2);
                            else
                                tooltip.css('max-width', 340);
                            var top1 = null;
                            var ad = null;
                            if (top > yTop) {
                                if (yTop > 200) {
                                    top1 = top;
                                    // ad = -10;

                                }
                                else {

                                    top1 = yTop - top - 100;
                                    // ad = -10;

                                }
                            }
                            else {

                                top1 = top;
                            }
                            if (top > 200) {
                                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                    ad = 35;
                                }
                                else {
                                    ad = 30;
                                }

                            }
                            else {
                                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                    ad = -2;
                                }
                                else {
                                    ad = 0;
                                }

                            }


                            var pos_left = left + 100 - (tooltip.outerWidth() / 2),
                               pos_top = top - 10 - 20;

                            if (pos_left < 0) {
                                pos_left = left + 100 - 15;
                                tooltip.addClass('left');
                            }
                            else
                                tooltip.removeClass('left');

                            if (pos_left + tooltip.outerWidth() > $(window).width()) {
                                pos_left = left - tooltip.outerWidth() + target.outerWidth() / 2 + 20;
                                tooltip.addClass('right');
                            }
                            else
                                tooltip.removeClass('right');

                            if (pos_top < 0) {

                                var pos_top = top + 20;
                                tooltip.addClass('top');
                            }
                            else
                                tooltip.removeClass('top');
                            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                if (pos_top < 200) {
                                    if (window != null)

                                        window.scrollTo(left, 0);
                                    // alert(e.pageY/2);

                                    // tooltip.scrollTop();


                                }

                                else {

                                    window.scrollTo(left, top - 200);


                                }

                            }

                            tooltip.css({ left: pos_left, top: pos_top })
                               .animate({ top: '+=' + ad, opacity: 1 }, 50);

                        };

                        init_tooltip();
                        $(window).resize(init_tooltip);

                        var remove_tooltip = function () {
                            tooltip.animate({ top: '-=10', opacity: 0 }, 50, function () {
                                $(this).remove();
                            });

                            //target.attr('alt', param);
                        };


                        // target.bind('mouseover', call);
                        // target.bind('mouseover', call_tooltip);
                        // target.bind('mouseout', remove_tooltip);
                        tooltip.bind('mouseleave', remove_tooltip);
                        //target.bind('mouseleave', remove_tooltip);
                        //alert(target.parent());
                    }

                });
            }


            function rem1() {


                $("#tooltip").remove();
            }

    </script>

     <style>
        label {
            /*color: orange;*/
        }

        table {
            width: 100%;
            border-collapse: collapse;
            /*margin:50px auto;*/
        }

        tr:nth-of-type(odd) {
            background: #fff;
        }

        th {
            background: #0952a4;
            color: white;
            /*font-weight: bold;*/
        }

        td, th {
            padding: 2px;
            /*border: 1px solid #ccc;*/
            text-align: left;
            /*font-size: 18px;*/
        }


        @media only screen and (max-width: 760px), (min-device-width: 768px) and (max-device-width: 1024px) {

            table {
                width: 100%;
            }


            table, thead, tbody, th, td, tr {
                display: block;
                padding: inherit;
            }


                thead tr {
                    position: absolute;
                    top: -9999px;
                    left: -9999px;
                }

            tr {
                border: 1px solid #ccc;
            }

            td {
                border: none;
                border-bottom: 1px solid #eee;
                position: relative;
                padding-left: 50%;
            }

                td:before {
                    position: absolute;
                    top: 6px;
                    left: 6px;
                    width: 45%;
                    padding-right: 10px;
                    white-space: nowrap;
                    content: attr(data-column);
                    color: #000;
                    font-weight: bold;
                }
        }
    </style>




    <style>
        div.close {
            z-index: 99999 !important;
        }

        .modal {
            z-index: 1000!important;
            border: none!important;
            margin: 0px!important;
            padding: 0px!important;
            width: 100%!important;
            height: 100%!important;
            top: 0px!important;
            left: 0px!important;
            background-color: rgb(0, 0, 0)!important;
            
            position: fixed!important;
        }
        .modal-dialog {
            height:500px!important;
        }

        .modal-content {
            position: relative;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0, 0, 0, .2);
            border-radius: 6px;
            outline: 0;
            -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
            box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
        }

        .modal-header {
            min-height: auto;
            padding: 0px 0px 0px 0px;
            border-bottom: 1px solid #e5e5e5;
        }

        body {
            /*background-image: url(../../Images/bg3.jpg);*/
            margin-top: 0px;
            /*text-align: center;*/
            font-size: 14px;
            font-family: 'Quicksand', sans-serif !important;
        }

        .form-groups {
            display: inline-block;
            margin-bottom: 10px;
            vertical-align: middle;
            width: 16%;
        }

        [type='text'], [type='password'], [type='date'], [type='datetime'], [type='datetime-local'], [type='month'], [type='week'], [type='email'], [type='number'], [type='search'], [type='tel'], [type='time'], [type='url'], [type='color'], textarea {
            font-size: 13px !important;
            color: #555 !important;
            font-weight: lighter;
            
        }

        #calendar {
            width: 100%;
            margin: 0 auto;
        }

        #tooltip {
            /*font-size: 0.9em;*/
            font-size: 12px;
            font-family: Calibri,arial;
            border: 2px solid #161946;
            text-align: left;
            text-shadow: 0 1px rgba( 0, 0, 0, .5 );
            line-height: 1.5;
            background: #FFF;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 3px 5px rgba( 0, 0, 0, .3 );
            -moz-box-shadow: 0 3px 5px rgba( 0, 0, 0, .3 );
            box-shadow: 0 3px 5px rgba( 0, 0, 0, .3 );
            position: absolute;
            z-index: 100;
            padding: 15px;
            padding: 3px 4px 15px 15px;
        }

            #tooltip:after {
                width: 0;
                height: 0;
                border-left: 10px solid transparent;
                border-right: 10px solid transparent;
                border-top-color: #333;
                border-top: 10px solid rgba( 0, 0, 0, .7 );
                content: '';
                position: absolute;
                left: 50%;
                bottom: -10px;
                margin-left: -10px;
            }

            #tooltip.top:after {
                border-top-color: transparent;
                border-bottom-color: #333;
                border-bottom: 10px solid rgba( 0, 0, 0, .6 );
                top: -20px;
                bottom: auto;
                margin: 0;
                padding: 0;
            }

            #tooltip.left:after {
                left: 10px;
                margin: 0;
            }

            #tooltip.right:after {
                right: 10px;
                left: auto;
                margin: 0;
            }

        div.close {
            height: 32px;
            left: 11px;
            position: relative;
            width: 38px;
            top: -2px;
        }
    </style>


    <ol class="breadcrumb-arrow">

        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">Travel Calander</a></li>

        
        


    </ol>

    <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div  class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="close" onclick="return closex()" data-dismiss="modal"></div>

                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
                    <h4 class="modal-title" id="myModalLabel"></h4>
                </div>
            
                <div class="modal-body" id="hhh">
                    <%--<div class="holds-the-iframe">--%>
                     
                    <iframe id="IfrmTKT" width="570" height="500" style="overflow-y: scroll; background: #ffffff url(http://mentalized.net/activity-indicators/indicators/simon-claret/progress_bar.gif) no-repeat 50% 5%;"></iframe>
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="card-main">
   
            <div class="card-body">
            <div class="inner-box">
                <div class="row">                           
                    <div class="col-md-3">
                        <%--<label>Select Year</label>--%>
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        <select class="theme-search-area-section-input" id="ddyear" name="ddyear">
                            <option value=" " selected>Select Year</option>
                            <option value="10">2010</option>
                            <option value="11">2011</option>
                            <option value="12">2012</option>
                            <option value="13">2013</option>
                            <option value="14">2014</option>
                            <option value="15">2015</option>
                            <option value="16">2016</option>
                            <option value="17">2017</option>
                            <option value="18">2018</option>
                            <option value="19">2019</option>
                            <option value="20">2020</option>
                        </select>
                                </div>
                             </div>
                    </div>
                    <div class="col-md-3">                        
                        <%--<label>Select Month</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        <select class="theme-search-area-section-input" id="ddmonth" name="ddmonth">
                            <option value=" " selected>Select Month</option>
                            <option value="1">January</option>
                            <option value="2">February</option>
                            <option value="3">March</option>
                            <option value="4">April</option>
                            <option value="5">May</option>
                            <option value="6">June</option>
                            <option value="7">July</option>
                            <option value="8">August</option>
                            <option value="9">September</option>
                            <option value="10">October</option>
                            <option value="11">November</option>
                            <option value="12">December</option>
                        </select>
                                </div>
                            </div>
                    </div>
                    <div class="col-md-3">                                 
                       <%-- <label>PNR</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                 
                    <div class="col-md-3">
                          <%--<label>Pax Name</label>--%>
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                        <asp:TextBox ID="txt_PaxName" placeholder="Pax First Name" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                             </div>
                    </div>

                <div class="col-md-3">         
                      <%--<label>Departure</label>--%>
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-map"></i>
                     <input type="text" name="txtDepCity1" class="theme-search-area-section-input" placeholder="Enter Your Departure City" onclick="this.value = '';" id="txtDepCity1" defvalue="Enter Your Departure City" onfocus="focusObjAIRS(this);" onblur="blurObjAIRS(this);" autocomplete="off" />
                    <input type="hidden" id="hidtxtDepCity1" name="hidtxtDepCity1" value="" />  
                                </div>
                        </div>
                </div>
                <div class="col-md-3">  
                      <%--<label>Destination</label>--%>
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-map"></i>
                    <input type="text" name="txtArrCity1" onclick="this.value = '';" id="txtArrCity1" class="theme-search-area-section-input" placeholder="Enter Your Destination City" defvalue="Enter Your Destination City" onfocus="focusObjAIRD(this);" onblur="blurObjAIRD(this);" autocomplete="off"    />
                    <input type="hidden" id="hidtxtArrCity1" name="hidtxtArrCity1" value="" />                  
                </div>
                        </div>
                    </div>
           
                <div class="col-md-3">   
                     <%-- <label>Airlines</label>--%>
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <input type="text" placeholder="Search By Airlines" class="theme-search-area-section-input" name="txtAirline" value="" id="txtAirline"   defvalue="Search By Airlines" onfocus="focusObjAIR(this);" onblur="blurObjAIR(this);" autocomplete="off" />
                        <input type="hidden" id="hidtxtAirline" name="hidtxtAirline" value="" /> 
                                </div>
                        </div>
                </div>

                <div class="col-md-3" id="td_Agency" runat="server">
                   <%--   <label>Agency Name</label>--%>
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                    <input type="text" class="theme-search-area-section-input" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName"
                        defvalue="Agency Name or ID" onfocus="focusObj(this);" onblur="blurObj(this);" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                </div>
                        </div>
                </div>



             
    </div>
                <br />
                

                <div class="row">
                       <div class="col-md-3">
                      <div class="btn-search">
                            <%--<input type="button" id="searchvalue" class="btn cmn-btn" value="Search Result" onclick="return filter_Cal()" />--%>
                              <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn" ><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                     </div>
            </div>
                </div>
                    
                <br />
                <%-- <div class="form-groups">
                        <asp:TextBox ID="txt_AirPNR" placeholder="Airline" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups" id="tdTripNonExec2" runat="server">
                        <asp:DropDownList class="form-control" ID="ddlTripDomIntl" runat="server">
                            <asp:ListItem Value="">-----Select-----</asp:ListItem>
                            <asp:ListItem Value="D">Domestic</asp:ListItem>
                            <asp:ListItem Value="I">International</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-groups" id="Div1" runat="server">
                        <asp:DropDownList class="form-control" ID="DropDownListDate" runat="server">
                           <asp:ListItem Value="B" Selected="True">Booking Date</asp:ListItem>
                           <asp:ListItem Value="J">Journey Date</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="color: #FF0000">
                        * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                    </div>
                    <div class="form-groups" id="td_Agency" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>--%>
            </div>
                </div>
            
            <%--  <div class="row" style="padding: 10px 10px 10px 10px;">
                    <div class="col-md-10">
                        <div style="color: #FF0000">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                        </div>
                    </div>

                </div>--%>
       
   
        </div>
    
   <div class="" style="border-radius: 5px;box-shadow: 1px 1px 15px rgb(0 0 0 / 45%);">
        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl">
            <tr class="">
                <td align="left">
                    <span  id="PrevButton" style="position: absolute;cursor:pointer;"><i class="icofont-dotted-left icofont-4x"></i></span>
                </td>
                    <td align="right">
                        <span id="NextButton" style="float:right;position: absolute;right: 15px;cursor:pointer;"><i class="icofont-dotted-right icofont-4x"></i></span>
                    </td>
               </tr>
             <tr>
                <td colspan="2">
                    <div id='calendar' class="travel-calender">
                    </div>
                </td>
            </tr>
        </table>
       </div>





</asp:Content>
