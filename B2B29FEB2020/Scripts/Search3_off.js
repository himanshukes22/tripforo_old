function CheckTrip(e, t) {
    if (e != "" && t != "") {
        var n = e.split(",");
        var r = t.split(",");
        if (n[1] == "IN" && r[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
    }
}
var SHandler;
$(document).ready(function () {
    SHandler = new SearchHelper;
    SHandler.BindEvents()
});
$(document).ready(function () {
    var e = $("input[name='TripType']:checked").val();
    if (e == "rdbOneWay") {
        $("#trRetDateRow").hide()
        MultiCityShowHide();
    }
    else if (e == "rdbRoundTrip") {
        $("#trRetDateRow").show()
        MultiCityShowHide();
    }
    else {
        $("#trRetDateRow").hide()
        $("#trRetDateRow").hide()
        MultiCityShowHide();
    }


    $("#txtDepCity2").click(function () {
        $("#txtDepCity2").val($("#txtArrCity1").val());
        $("#hidtxtDepCity2").val($("#hidtxtArrCity1").val());
    });


    $("#txtDepCity3").click(function () {

        $("#txtDepCity3").val($("#txtArrCity2").val());
        $("#hidtxtDepCity3").val($("#hidtxtArrCity2").val());
    });

    $("#txtDepCity4").click(function () {
        $("#txtDepCity4").val($("#txtArrCity3").val());
        $("#hidtxtDepCity4").val($("#hidtxtArrCity3").val());
    });


    $("#txtDepCity5").click(function () {

        $("#txtDepCity5").val($("#txtArrCity4").val());
        $("#hidtxtDepCity5").val($("#hidtxtArrCity4").val());
    });

    $("#txtDepCity6").click(function () {

        $("#txtDepCity6").val($("#txtArrCity5").val());
        $("#hidtxtDepCity6").val($("#hidtxtArrCity5").val());
    });



    var i = 0;
    $("#plus").click(function () {
        if (i <= 2) {
            if (i == 0)
                $("#four").show();
            if (i == 1)
                $("#five").show();
            if (i == 2) {
                $("#six").show();
                $("#plus").hide();

            }
            $("#minus").show();
            i = i + 1;
        }
    });
    $("#minus").click(function () {
        if (i <= 3 && i != 0) {
            if (i == 1) {
                $("#four").hide();
                $("#minus").hide();

                $("#txtDepCity4").val("");
                $("#hidtxtDepCity4").val("");
                $("#txtArrCity4").val("");
                $("#hidtxtArrCity4").val("");
                $("#txtDepDate4").val("");
                $("#hidtxtDepDate4").val("");
            }

            if (i == 2) {
                $("#five").hide();
                $("#txtDepCity5").val("");
                $("#hidtxtDepCity5").val("");
                $("#txtArrCity5").val("");
                $("#hidtxtArrCity5").val("");
                $("#txtDepDate5").val("");
                $("#hidtxtDepDate5").val("");
            }
            if (i == 3) {
                $("#six").hide();
                $("#plus").show();

                $("#txtDepCity6").val("");
                $("#hidtxtDepCity6").val("");
                $("#txtArrCity6").val("");
                $("#hidtxtArrCity6").val("");
                $("#txtDepDate6").val("");
                $("#hidtxtDepDate6").val("");
            }
            i = i - 1;
        }
    });
});
var Trip;

var SearchHelper = function () {
    this.flight = $("flight");
    this.txtDepCity1 = $("#txtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.btnSearch = $("#btnSearch");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    this.hidAirline = $("#hidAirLine");
    this.rdbOneWay = $("#rdbOneWay");
    this.rdbRoundTrip = $("#rdbRoundTrip");
    this.txtDepDate = $("#txtDepDate");
    this.txtRetDate = $("#txtRetDate");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
    this.trRetDateRow = $("#trRetDateRow");
    this.TripType = $("input[name=TripType]");
    this.Adult = $("#Adult");
    this.Child = $("#Child");
    this.Infant = $("#Infant");
    this.Cabin = $("#Cabin");
    this.txtAirline = $("#txtAirline");
    this.hidtxtAirline = $("#hidtxtAirline");
    this.chkNonstop = $("#chkNonstop");
    this.chkAdvSearch = $("#chkAdvSearch");
    this.trAdvSearchRow = $("#trAdvSearchRow");
    this.LCC_RTF = $("#LCC_RTF");
    this.GDS_RTF = $("#GDS_RTF");
    this.showresult1 = $("#showresult1");
    this.showresult2 = $("#showresult2");
    this.showresult3 = $("#showresult3");
    this.AirIndia = $("#AI");
    this.JetAirways = $("#9W");
    this.JetKonnect = $("#S2");
    this.Kingfisher = $("#IT");
    this.Spicejet = $("#SG")
    this.returnDateSearch = $(".returnDateSearch");



    this.rdbMultiCity = $("#rdbMultiCity");
    this.txtDepCity1 = $("#txtDepCity1");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    //this.txtDepDate1 = $("#txtDepDate1");
    //this.hidtxtDepDate1 = $("#hidtxtDepDate1");

    this.txtDepCity2 = $("#txtDepCity2");
    this.hidtxtDepCity2 = $("#hidtxtDepCity2");
    this.txtArrCity2 = $("#txtArrCity2");
    this.hidtxtArrCity2 = $("#hidtxtArrCity2");
    this.txtDepDate2 = $("#txtDepDate2");
    this.hidtxtDepDate2 = $("#hidtxtDepDate2");

    this.txtDepCity3 = $("#txtDepCity3");
    this.hidtxtDepCity3 = $("#hidtxtDepCity3");
    this.txtArrCity3 = $("#txtArrCity3");
    this.hidtxtArrCity3 = $("#hidtxtArrCity3");
    this.txtDepDate3 = $("#txtDepDate3");
    this.hidtxtDepDate3 = $("#hidtxtDepDate3");

    //
    this.txtDepCity4 = $("#txtDepCity4");
    this.hidtxtDepCity4 = $("#hidtxtDepCity4");
    this.txtArrCity4 = $("#txtArrCity4");
    this.hidtxtArrCity4 = $("#hidtxtArrCity4");
    this.txtDepDate4 = $("#txtDepDate4");
    this.hidtxtDepDate4 = $("#hidtxtDepDate4");

    this.txtDepCity5 = $("#txtDepCity5");
    this.hidtxtDepCity5 = $("#hidtxtDepCity5");
    this.txtArrCity5 = $("#txtArrCity5");
    this.hidtxtArrCity5 = $("#hidtxtArrCity5");
    this.txtDepDate5 = $("#txtDepDate5");
    this.hidtxtDepDate5 = $("#hidtxtDepDate5");

    this.txtDepCity6 = $("#txtDepCity6");
    this.hidtxtDepCity6 = $("#hidtxtDepCity6");
    this.txtArrCity6 = $("#txtArrCity6");
    this.hidtxtArrCity6 = $("#hidtxtArrCity6");
    this.txtDepDate6 = $("#txtDepDate6");
    this.hidtxtDepDate6 = $("#hidtxtDepDate6");





};
SearchHelper.prototype.BindEvents = function () {
    var e = this;
    var t = e.hidtxtDepDate.val();
    var n = e.hidtxtRetDate.val();
    var r = {
        numberOfMonths: 1,
        dateFormat: "dd/mm/yy",
        maxDate: "+1y",
        minDate: 0,
        showOtherMonths: true,
        selectOtherMonths: false
    };
    e.txtDepDate.datepicker(r).datepicker("option", {
        onSelect: function (e) {
            SHandler.txtRetDate.datepicker("option", {
                minDate: e

            });
        },

        onClose: function (e) {


            var t = $("input[name='TripType']:checked").val();
            if (t == "rdbRoundTrip") {
                $("#txtRetDate").focus();
            }

        }

        //e.UpdateRoundTripMininumDate

    }).datepicker("setDate", t.substring(0, 10));
    e.txtRetDate.datepicker(r).datepicker("setDate", n.substr(0, 10));

    //For Multicity



    //var t1 = e.hidtxtDepDate1.val();
    //e.txtDepDate1.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate1
    //}).datepicker("setDate", t1.substring(0, 10));

    //var t2 = e.hidtxtDepDate2.val();
    //e.txtDepDate2.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate2
    //}).datepicker("setDate", t2.substring(0, 10));

    //var t3 = e.hidtxtDepDate3.val();
    //e.txtDepDate3.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate3
    //}).datepicker("setDate", t3.substring(0, 10));


    //var t4 = e.hidtxtDepDate4.val();
    //e.txtDepDate4.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate4
    //}).datepicker("setDate", t4.substring(0, 10));


    //var t5 = e.hidtxtDepDate5.val();
    //e.txtDepDate5.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate5
    //}).datepicker("setDate", t5.substring(0, 10));

    //var t6 = e.hidtxtDepDate6.val();
    ////e.txtDepDate6.datepicker(r).datepicker("option", {
    ////    onSelect: e.UpdateRoundTripMininumDate
    ////}).datepicker("setDate", t6.substring(0, 10));

    //e.txtDepDate6.datepicker(r).datepicker("setDate", t6.substr(0, 10));
    //


    var i = UrlBase + "CitySearch.asmx/FetchCityList";
    e.txtDepCity1.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {

            e.hidtxtDepCity1.val(n.item.id)
            $('#txtArrCity1').focus();
        }
    });
    e.txtArrCity1.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {

            e.hidtxtArrCity1.val(n.item.id)
            $('#txtDepDate').focus();
            
            //e.txtDepCity2.val("")            
            //e.hidtxtDepCity2.val("")
            //if (e.rdbMultiCity[0].checked)
            //{
            //    e.txtDepCity2.val(n.item.label)
            //    e.hidtxtDepCity2.val(n.item.id)
            //}

        }
    });


    e.txtDepCity2.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtDepCity2.val(n.item.id)
            $('#txtDepDate').focus();
        }
    });

    e.txtArrCity2.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtArrCity2.val(n.item.id)
            $('#txtDepDate2').focus();

        }
    });

    //
    e.txtDepCity3.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtDepCity3.val(n.item.id)
            
        }
    });

    e.txtArrCity3.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtArrCity3.val(n.item.id)
            $('#txtDepDate3').focus();
        }
    });
    //

    //
    e.txtDepCity4.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtDepCity4.val(n.item.id)
        }
    });

    e.txtArrCity4.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtArrCity4.val(n.item.id)
            $('#txtDepDate4').focus();

        }
    });
    //

    //
    e.txtDepCity5.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtDepCity5.val(n.item.id)
        }
    });

    e.txtArrCity5.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtArrCity5.val(n.item.id)
            $('#txtDepDate5').focus();

        }
    });
    //

    //
    e.txtDepCity6.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtDepCity6.val(n.item.id)
        }
    });

    e.txtArrCity6.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: i,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName + "(" + e.AirportCode + ")";
                        var n = e.AirportCode + "," + e.CountryCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtArrCity6.val(n.item.id)
            $('#txtDepDate6').focus();
        }
    });
    //

    var s = UrlBase + "CitySearch.asmx/FetchAirlineList";
    e.txtAirline.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: s,
                data: "{ 'airline': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.ALName + "(" + e.ALCode + ")";
                        var n = e.ALName + "," + e.ALCode;
                        return {
                            label: t,
                            value: t,
                            id: n
                        }
                    }))
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function (t, n) {
            e.hidtxtAirline.val(n.item.id)
        }
    });
   
    e.TripType.click(function () {



        if ($(this).val() == "rdbOneWay" || $(this).val() == "rdbRoundTrip" || $(this).val() == "rdbMultiCity") {
           
            if ($(this).val() == "rdbRoundTrip") {
                e.UpdateRoundTripMininumDate(e.txtDepDate.val());
                e.trRetDateRow.show()
                e.returnDateSearch.show();
                MultiCityShowHide();

                
                //$(".mltcs").removeClass("col-md-4 col-xs-4 text-search mltcs").addClass("col-md-12 col-xs-12 text-search mltcs");
                //$(".mltcrt").removeClass("col-md-12 col-xs-12 text-search mltcrt").addClass("col-md-12 col-xs-12 text-search mltcrt");
                $("#trRetDateRow").show();
                $(".mltcrt").show();
                $("#Return").show();
                
            }
            else if ($(this).val() == "rdbOneWay") {
                
              //  $(".mltcs").removeClass("col-md-4 col-xs-4 text-search mltcs").addClass("col-md-12 col-xs-12 text-search mltcs");
                $("#trRetDateRow").hide();
                $(".mltcOT").show();
                $("#add,#two,#three").hide();
            }            
            else if ($(this).val() == "rdbMultiCity") {
                e.trRetDateRow.hide()
                e.returnDateSearch.hide();
                MultiCityShowHide();
               // $(".mltcs").removeClass("col-md-12 col-xs-12 text-search mltcs").addClass("col-md-4 col-xs-4 text-search mltcs");
                //$(".mltcs").removeClass("col-md-6 col-xs-6 text-search mltcs").addClass("col-md-4 col-xs-4 text-search mltcs");
                //$(".mltcrt").removeClass("col-md-6 col-xs-6 text-search mltcrt").addClass("col-md-4 col-xs-4 text-search mltcs");
                $("#Return").hide();
                $(".mltcOT").show();
                $(".mltcrt").hide();
            }
            else {
                e.trRetDateRow.hide()
                e.returnDateSearch.hide();
                MultiCityShowHide();
                $(".mltcrt").show();

            }

        } else {

        }
    });
    e.chkAdvSearch.click(function () {
        if ($(this).is(":checked")) {
            if (e.hidtxtDepCity1.val() != "" && e.hidtxtArrCity1.val() != "") {
                var t = e.hidtxtDepCity1.val().split(",");
                var n = e.hidtxtArrCity1.val().split(",");
                if (t[1] == "IN" && n[1] == "IN") {
                    e.chkAdvSearch.attr("value", true)
                } else {
                    e.chkAdvSearch.attr("value", false);
                    alert("Only for Domestic Search");
                    return false
                }
            } else {
                e.chkAdvSearch.attr("value", false);
                alert("Please Select Origin And Destination");
                return false
            }
        } else {
            e.chkAdvSearch.attr("value", false)
        }
    });
    e.GDS_RTF.click(function () {
        if ($(this).is(":checked")) {
            if (e.hidtxtDepCity1.val() != "" && e.hidtxtArrCity1.val() != "") {
                var t = e.hidtxtDepCity1.val().split(",");
                var n = e.hidtxtArrCity1.val().split(",");
                if (t[1] == "IN" && n[1] == "IN") {
                    e.GDS_RTF.attr("value", true);
                    e.LCC_RTF.attr("value", false);
                    e.LCC_RTF.attr("checked", false)
                } else {
                    e.GDS_RTF.attr("value", false);
                    e.LCC_RTF.attr("value", false);
                    alert("Only for Domestic Search");
                    return false
                }
            } else {
                e.GDS_RTF.attr("value", false);
                alert("Please Select Origin And Destination");
                return false
            }
        } else {
            e.GDS_RTF.attr("value", false)
        }
    });
    e.LCC_RTF.click(function () {
        if ($(this).is(":checked")) {
            if (e.hidtxtDepCity1.val() != "" && e.hidtxtArrCity1.val() != "") {
                var t = e.hidtxtDepCity1.val().split(",");
                var n = e.hidtxtArrCity1.val().split(",");
                if (t[1] == "IN" && n[1] == "IN") {
                    e.GDS_RTF.attr("value", false);
                    e.LCC_RTF.attr("value", true);
                    e.GDS_RTF.attr("checked", false)
                } else {
                    e.GDS_RTF.attr("value", false);
                    e.LCC_RTF.attr("value", false);
                    alert("Only for Domestic Search");
                    return false
                }
            } else {
                e.LCC_RTF.attr("value", false);
                alert("Please Select Origin And Destination");
                return false
            }
        } else {
            e.LCC_RTF.attr("value", false)
        }
    });
    e.btnSearch.click(function () {
        return e.validate()
    })
};
SearchHelper.prototype.UpdateRoundTripMininumDate = function (e, t) {
    SHandler.txtRetDate.datepicker("option", {
        minDate: e
    });
    SHandler.txtDepDate2.datepicker("option", { minDate: e });
};



//SearchHelper.prototype.UpdateMultiCityMininumDate1 = function (e, t) {
//    SHandler.txtDepDate2.datepicker("option", { minDate: e });
//    SHandler.txtDepDate3.datepicker("option", { minDate: e });
//};

SearchHelper.prototype.UpdateMultiCityMininumDate2 = function (e, t) {

    //SHandler.txtDepCity3.datepicker("option", {
    //    minDate: e
    //})
    SHandler.txtDepDate3.datepicker("option", { minDate: e });
    SHandler.txtDepDate4.datepicker("option", { minDate: e });
};

SearchHelper.prototype.UpdateMultiCityMininumDate3 = function (e, t) {
    //SHandler.txtDepCity4.datepicker("option", {
    //    minDate: e
    //})
    SHandler.txtDepDate4.datepicker("option", { minDate: e });
    SHandler.txtDepDate5.datepicker("option", { minDate: e });
};

SearchHelper.prototype.UpdateMultiCityMininumDate4 = function (e, t) {
    SHandler.txtDepDate5.datepicker("option", {
        minDate: e
    })
};

SearchHelper.prototype.UpdateMultiCityMininumDate5 = function (e, t) {
    SHandler.txtDepDate6.datepicker("option", {
        minDate: e
    })
};


SearchHelper.prototype.validate = function () {
    var e = this;
    var t = $("input[name='TripType']:checked").val();
    CheckTrip(e.hidtxtDepCity1.val(), e.hidtxtArrCity1.val());
    if (t == "rdbOneWay" || t == "rdbRoundTrip" || t == "rdbMultiCity") {
        if ($.trim(e.txtDepCity1.val()) == "") {
            alert("Origin Required.");
            e.txtDepCity1.val("").focus();
            return false
        } else if (e.hidtxtDepCity1.val() == "") {
            alert("Invalid Origin.");
            e.txtDepCity1.val("").focus();
            return false
        }
        if ($.trim(e.txtArrCity1.val()) == "") {
            alert("Destination Required.");
            e.txtArrCity1.val("").focus();
            return false
        } else if (e.hidtxtArrCity1.val() == "") {
            alert("Invalid Destination.");
            e.txtArrCity1.val("").focus();
            return false
        }

        //if ((e.txtDepDate1.val() == "" || e.txtDepDate1.val() == null) && t == "rdbMultiCity") {
        //    alert("Departure Date Required.");
        //    return false
        //}

        if (e.txtDepDate.val() == "" || e.txtDepDate.val() == null) {
            alert("Departure Date Required.");
            return false
        }

        // For Multicity Validation


        if (t == "rdbMultiCity") {
            for (var i = 1; i <= 6; i++) {

                if ($('#hidtxtDepCity' + i).val() == "" && $('#hidtxtArrCity' + i).val() != "") {

                    alert("Origin city is required");
                    $('#txtDepCity' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCity' + i).val() != "" && $('#hidtxtArrCity' + i).val() == "") {

                    alert("Destination city is required.");
                    $('#txtArrCity' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCity' + i).val() != "" && $('#hidtxtArrCity' + i).val() != "" && $('#txtDepDate' + i).val() == "") {

                    alert("Departure Date is required.");
                    $('#txtDepDate' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCity' + i).val() == "" && $('#hidtxtArrCity' + i).val() != "" && $('#txtDepDate' + i).val() != "") {

                    alert("Origin city is required.");
                    $('#txtDepCity' + i).focus();
                    return false;
                    break;

                }


                if ($('#hidtxtDepCity' + i).val() != "" && $('#hidtxtArrCity' + i).val() == "" && $('#txtDepDate' + i).val() != "") {
                    alert("Destination city is required.");
                    $('#txtArrCity' + i).focus();
                    return false;
                    break;

                }


                if ($.trim($('#hidtxtDepCity' + i).val()) != "" && $.trim($('#hidtxtArrCity' + i).val()) != "") {
                    if ($.trim($('#hidtxtDepCity' + i).val()) == $.trim($('#hidtxtArrCity' + i).val())) {
                        alert("Origin and destination must not be the same.");
                        $('#txtArrCity' + i).focus();
                        return false;
                    }
                }

                //var result1 = validateInput($('#FltSearch_From' + i), "ErrorMessage", "Please select valid Origin.", $('#FltSearch_hidFrom' + i), 3);
                //var result3 = validateInput($('#FltSearch_To' + i), "ErrorMessage", "Please select valid Destination.", $('#FltSearch_hidTo' + i), 3);


                //if (result1 == true && result3 == true) {
                //}
                //else {
                //    return false;
                //}


            }

        }


        //End Multicity






        if (t == "rdbRoundTrip") {
            if (e.txtRetDate.val() == "" || e.txtRetDate.val() == null) {
                alert("Return Date Required.");
                return false
            }
            var n = ChangeDateFormat(e.txtDepDate.val());
            var r = ChangeDateFormat(e.txtRetDate.val());
            if (r < n) {
                alert("Return Date Should Be Greater Than Departure Date.");
                return false
            }
        }
        if (e.LCC_RTF.is(":checked") == true && t != "rdbRoundTrip") {
            e.LCC_RTF.attr("checked", false);
            alert("Please select round trip.");
            return false
        }
        if (e.GDS_RTF.is(":checked") == true && t != "rdbRoundTrip") {
            e.GDS_RTF.attr("checked", false);
            alert("Please select round trip.");
            return false
        }
    } else {
        return false
    }
    var i = parseInt(e.Adult.val());
    var s = parseInt(e.Child.val());
    var o = parseInt(e.Infant.val());
    var u = i + s;
    if (u > 9) {
        alert("Total Number Of Passenger Should Be Less Than 9.");
        return false
    }
    if (o > i) {
        alert("Number Of Infant Should Be Less Than Or Equal To Number Of Adult");
        return false
    }
    if (e.txtAirline.val() != "") {
        if (e.hidtxtAirline.val() == "") {
            alert("Invalid Airline");
            e.txtAirline.val("").focus();
            return false
        }
    }
    e.StartSearch()
};
SearchHelper.prototype.StartSearch = function () {

    var e = this;
    var t = $("input[name='TripType']:checked").val();
    var n;
    var r;
    var i;
    if (e.LCC_RTF.is(":checked") == true) {
        n = "TRUE"
    } else {
        n = "FALSE"
    } if (e.GDS_RTF.is(":checked") == true) {
        r = "TRUE"
    } else {
        r = "FALSE"
    } if (e.chkNonstop.is(":checked") == true) {
        i = "TRUE"
    } else {
        i = "FALSE"
    }

    var s = "TripType=" + t + "&txtDepCity1=" + e.txtDepCity1.val() + "&txtArrCity1=" + e.txtArrCity1.val() + "&hidtxtDepCity1=" + e.hidtxtDepCity1.val() + "&hidtxtArrCity1=" + e.hidtxtArrCity1.val() + "&Adult=" + e.Adult.val();
    s += "&Child=" + e.Child.val() + "&Infant=" + e.Infant.val() + "&Cabin=" + e.Cabin.val() + "&txtAirline=" + e.txtAirline.val() + "&hidtxtAirline=" + e.hidtxtAirline.val() + "&txtDepDate=" + e.txtDepDate.val() + "&txtRetDate=" + e.txtRetDate.val() + "&RTF=" + n + "&NStop=" + i + "&RTF=" + n + "&Trip=" + Trip + "&GRTF=" + r;

    //s += "&txtDepDate1=" + e.txtDepDate1.val() + "&hidtxtDepDate1=" + e.hidtxtDepDate1.val();
    //s += "&txtDepCity2=" + e.txtDepCity2.val() + "&hidtxtDepCity2=" + e.hidtxtDepCity2.val() + "&txtArrCity2=" + e.txtArrCity2.val() + "&hidtxtArrCity2=" + e.hidtxtArrCity2.val() + "&txtDepDate2=" + e.txtDepDate2.val() + "&hidtxtDepDate2=" + e.hidtxtDepDate2.val();
    //s += "&txtDepCity3=" + e.txtDepCity3.val() + "&hidtxtDepCity3=" + e.hidtxtDepCity3.val() + "&txtArrCity3=" + e.txtArrCity3.val() + "&hidtxtArrCity3=" + e.hidtxtArrCity3.val() + "&txtDepDate3=" + e.txtDepDate3.val() + "&hidtxtDepDate3=" + e.hidtxtDepDate3.val();
    //s += "&txtDepCity4=" + e.txtDepCity4.val() + "&hidtxtDepCity4=" + e.hidtxtDepCity4.val() + "&txtArrCity4=" + e.txtArrCity4.val() + "&hidtxtArrCity4=" + e.hidtxtArrCity4.val() + "&txtDepDate4=" + e.txtDepDate4.val() + "&hidtxtDepDate4=" + e.hidtxtDepDate4.val();
    //s += "&txtDepCity5=" + e.txtDepCity5.val() + "&hidtxtDepCity5=" + e.hidtxtDepCity5.val() + "&txtArrCity5=" + e.txtArrCity5.val() + "&hidtxtArrCity5=" + e.hidtxtArrCity5.val() + "&txtDepDate5=" + e.txtDepDate5.val() + "&hidtxtDepDate5=" + e.hidtxtDepDate5.val();
    //s += "&txtDepCity6=" + e.txtDepCity6.val() + "&hidtxtDepCity6=" + e.hidtxtDepCity6.val() + "&txtArrCity6=" + e.txtArrCity6.val() + "&hidtxtArrCity6=" + e.hidtxtArrCity6.val() + "&txtDepDate6=" + e.txtDepDate6.val() + "&hidtxtDepDate6=" + e.hidtxtDepDate6.val();

    s += "&txtDepCity2=" + e.txtDepCity2.val() + "&hidtxtDepCity2=" + e.hidtxtDepCity2.val() + "&txtArrCity2=" + e.txtArrCity2.val() + "&hidtxtArrCity2=" + e.hidtxtArrCity2.val() + "&txtDepDate2=" + e.txtDepDate2.val();
    s += "&txtDepCity3=" + e.txtDepCity3.val() + "&hidtxtDepCity3=" + e.hidtxtDepCity3.val() + "&txtArrCity3=" + e.txtArrCity3.val() + "&hidtxtArrCity3=" + e.hidtxtArrCity3.val() + "&txtDepDate3=" + e.txtDepDate3.val();
    s += "&txtDepCity4=" + e.txtDepCity4.val() + "&hidtxtDepCity4=" + e.hidtxtDepCity4.val() + "&txtArrCity4=" + e.txtArrCity4.val() + "&hidtxtArrCity4=" + e.hidtxtArrCity4.val() + "&txtDepDate4=" + e.txtDepDate4.val();
    s += "&txtDepCity5=" + e.txtDepCity5.val() + "&hidtxtDepCity5=" + e.hidtxtDepCity5.val() + "&txtArrCity5=" + e.txtArrCity5.val() + "&hidtxtArrCity5=" + e.hidtxtArrCity5.val() + "&txtDepDate5=" + e.txtDepDate5.val();
    s += "&txtDepCity6=" + e.txtDepCity6.val() + "&hidtxtDepCity6=" + e.hidtxtDepCity6.val() + "&txtArrCity6=" + e.txtArrCity6.val() + "&hidtxtArrCity6=" + e.hidtxtArrCity6.val() + "&txtDepDate6=" + e.txtDepDate6.val();


    document.getElementById("__VIEWSTATE").name = "NOVIEWSTATE";
    if (Trip == "D") {

        ////window.location.href = UrlBase + "Domestic/Result.aspx?" + s;
        try {
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                data: "{ 'name': 'Domestic/Result.aspx'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var resUm = data.d;
                    if (resUm != null && resUm != "") {
                        window.location.href = UrlBase + resUm + "?" + s
                    }
                    else {
                        window.location.href = UrlBase + "Domestic/Result.aspx?" + s;
                    }
                },
                error: function (e, t, n) {
                    alert(t)
                    window.location.href = UrlBase + "Domestic/Result.aspx?" + s;
                }
            });
        }
        catch (err) {
            window.location.href = UrlBase + "Domestic/Result.aspx?" + s;
        }
    }
    else if (Trip == "I") {
        ////window.location.href = UrlBase + "International/FltResIntl.aspx?" + s;
        try {
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                data: "{ 'name': 'International/FltResIntl.aspx'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var resUm = data.d;
                    if (resUm != null && resUm != "") {
                        window.location.href = UrlBase + resUm + "?" + s
                    }
                    else {
                        window.location.href = UrlBase + "International/FltResIntl.aspx?" + s;
                    }
                },
                error: function (e, t, n) {
                    alert(t)
                    window.location.href = UrlBase + "International/FltResIntl.aspx?" + s;
                }
            });
        }
        catch (err) {
            window.location.href = UrlBase + "International/FltResIntl.aspx?" + s;
        }
    }
}

function MultiCityShowHide() {

    var trip = $("input[name='TripType']:checked").val();
    if (trip == "rdbMultiCity") {

        
        $("#two").show();
        $("#three").show();
        $("#add").show();
        $("#minus").hide();
    }
    else {
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();
        $("#five").hide();
        $("#six").hide();
        //$("#minus").hide();
        //$("#plus").hide();
        $("#add").hide();


    }

}
