function CheckTripF(e, t) {
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
var SHandlerF;
$(document).ready(function () {
    SHandlerF = new SearchHelperF;
    SHandlerF.BindEvents()
});
$(document).ready(function () {
    var e = $("input[name='TripTypeF']:checked").val();
    if (e == "rdbOneWayF") {
        $("#trRetDateRowF").hide()
       // MultiCityShowHide();
    }
    else if (e == "rdbRoundTripF") {
        $("#trRetDateRowF").show()
       // MultiCityShowHide();
    }
    else {
        $("#trRetDateRowF").hide()
        $("#trRetDateRowF").hide()
        //MultiCityShowHide();
    }


    $("#txtDepCity2F").click(function () {
        $("#txtDepCity2F").val($("#txtArrCity1F").val());
        $("#hidtxtDepCity2F").val($("#hidtxtArrCity1F").val());
    });


    $("#txtDepCity3F").click(function () {

        $("#txtDepCity3F").val($("#txtArrCity2F").val());
        $("#hidtxtDepCity3F").val($("#hidtxtArrCity2F").val());
    });

    $("#txtDepCity4F").click(function () {
        $("#txtDepCity4F").val($("#txtArrCity3F").val());
        $("#hidtxtDepCity4F").val($("#hidtxtArrCity3F").val());
    });


    $("#txtDepCity5F").click(function () {

        $("#txtDepCity5F").val($("#txtArrCity4F").val());
        $("#hidtxtDepCity5F").val($("#hidtxtArrCity4F").val());
    });

    $("#txtDepCity6F").click(function () {

        $("#txtDepCity6F").val($("#txtArrCity5F").val());
        $("#hidtxtDepCity6F").val($("#hidtxtArrCity5F").val());
    });



    var i = 0;
    $("#plusF").click(function () {
        if (i <= 2) {
            if (i == 0)
                $("#fourF").show();
            if (i == 1)
                $("#fiveF").show();
            if (i == 2) {
                $("#sixF").show();
                $("#plusF").hide();

            }
            $("#minusF").show();
            i = i + 1;
        }
    });
    $("#minusF").click(function () {
        if (i <= 3 && i != 0) {
            if (i == 1) {
                $("#fourF").hide();
                $("#minusF").hide();

                $("#txtDepCity4F").val("");
                $("#hidtxtDepCity4F").val("");
                $("#txtArrCity4F").val("");
                $("#hidtxtArrCity4F").val("");
                $("#txtDepDate4F").val("");
                $("#hidtxtDepDate4F").val("");
            }

            if (i == 2) {
                $("#fiveF").hide();
                $("#txtDepCity5F").val("");
                $("#hidtxtDepCity5F").val("");
                $("#txtArrCity5F").val("");
                $("#hidtxtArrCity5F").val("");
                $("#txtDepDate5F").val("");
                $("#hidtxtDepDate5F").val("");
            }
            if (i == 3) {
                $("#sixF").hide();
                $("#plusF").show();

                $("#txtDepCity6F").val("");
                $("#hidtxtDepCity6F").val("");
                $("#txtArrCity6F").val("");
                $("#hidtxtArrCity6F").val("");
                $("#txtDepDate6F").val("");
                $("#hidtxtDepDate6F").val("");
            }
            i = i - 1;
        }
    });
});
var Trip;

var SearchHelperF = function () {
    this.flight = $("flight");
    this.txtDepCity1 = $("#txtDepCity1F");
    this.txtDepCityFD = $("#txtDepCityFD");
    this.txtArrCity1 = $("#txtArrCity1F");
    this.btnSearch = $("#btnSearchF");
    this.hidtxtDepCityFD = $("#hidtxtDepCityFD");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1F");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1F");
    this.hidAirline = $("#hidAirLineF");
    this.rdbOneWay = $("#rdbOneWayF");
    this.rdbRoundTrip = $("#rdbRoundTripF");
    this.txtDepDate = $("#txtDepDateF");
    this.txtRetDate = $("#txtRetDateF");
    this.hidtxtDepDate = $("#hidtxtDepDateF");
    this.hidtxtRetDate = $("#hidtxtRetDateF");
    this.trRetDateRow = $("#trRetDateRowF");
    this.TripType = $("input[name=TripTypeF]");
    this.Adult = $("#AdultF");
    this.Child = $("#ChildF");
    this.Infant = $("#InfantF");
    this.Cabin = $("#CabinF");
    this.txtAirline = $("#txtAirlineF");
    this.hidtxtAirline = $("#hidtxtAirlineF");
    this.chkNonstop = $("#chkNonstop");
    this.chkAdvSearch = $("#chkAdvSearchF");
    this.trAdvSearchRow = $("#trAdvSearchRowF");
    this.LCC_RTF = $("#LCC_RTFF");
    this.GDS_RTF = $("#GDS_RTFF");
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
    this.txtDepCity1 = $("#txtDepCity1F");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1F");
    this.txtArrCity1 = $("#txtArrCity1F");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1F");
    //this.txtDepDate1 = $("#txtDepDate1");
    //this.hidtxtDepDate1 = $("#hidtxtDepDate1");

    this.txtDepCity2 = $("#txtDepCity2F");
    this.hidtxtDepCity2 = $("#hidtxtDepCity2F");
    this.txtArrCity2 = $("#txtArrCity2F");
    this.hidtxtArrCity2 = $("#hidtxtArrCity2F");
    this.txtDepDate2 = $("#txtDepDate2F");
    this.hidtxtDepDate2 = $("#hidtxtDepDate2F");

    this.txtDepCity3 = $("#txtDepCity3F");
    this.hidtxtDepCity3 = $("#hidtxtDepCity3F");
    this.txtArrCity3 = $("#txtArrCity3F");
    this.hidtxtArrCity3 = $("#hidtxtArrCity3F");
    this.txtDepDate3 = $("#txtDepDate3F");
    this.hidtxtDepDate3 = $("#hidtxtDepDate3F");

    //
    this.txtDepCity4 = $("#txtDepCity4F");
    this.hidtxtDepCity4 = $("#hidtxtDepCity4F");
    this.txtArrCity4 = $("#txtArrCity4F");
    this.hidtxtArrCity4 = $("#hidtxtArrCity4F");
    this.txtDepDate4 = $("#txtDepDate4F");
    this.hidtxtDepDate4 = $("#hidtxtDepDate4F");

    this.txtDepCity5 = $("#txtDepCity5F");
    this.hidtxtDepCity5 = $("#hidtxtDepCity5F");
    this.txtArrCity5 = $("#txtArrCity5F");
    this.hidtxtArrCity5 = $("#hidtxtArrCity5F");
    this.txtDepDate5 = $("#txtDepDate5F");
    this.hidtxtDepDate5 = $("#hidtxtDepDate5F");

    this.txtDepCity6 = $("#txtDepCity6F");
    this.hidtxtDepCity6 = $("#hidtxtDepCity6F");
    this.txtArrCity6 = $("#txtArrCity6F");
    this.hidtxtArrCity6 = $("#hidtxtArrCity6F");
    this.txtDepDate6 = $("#txtDepDate6F");
    this.hidtxtDepDate6 = $("#hidtxtDepDate6F");





};
SearchHelperF.prototype.BindEvents = function () {
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
            SHandlerF.txtRetDate.datepicker("option", {
                minDate: e

            });
        },

        onClose: function (e) {


            var t = $("input[name='TripTypeF']:checked").val();
            if (t == "rdbRoundTripF") {
                $("#txtRetDateF").focus();
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

    var t2 = e.hidtxtDepDate2.val();
    e.txtDepDate2.datepicker(r).datepicker("option", {
        onSelect: e.UpdateMultiCityMininumDate2
    }).datepicker("setDate", t2.substring(0, 10));

    var t3 = e.hidtxtDepDate3.val();
    e.txtDepDate3.datepicker(r).datepicker("option", {
        onSelect: e.UpdateMultiCityMininumDate3
    }).datepicker("setDate", t3.substring(0, 10));


    var t4 = e.hidtxtDepDate4.val();
    e.txtDepDate4.datepicker(r).datepicker("option", {
        onSelect: e.UpdateMultiCityMininumDate4
    }).datepicker("setDate", t4.substring(0, 10));


    var t5 = e.hidtxtDepDate5.val();
    e.txtDepDate5.datepicker(r).datepicker("option", {
        onSelect: e.UpdateMultiCityMininumDate5
    }).datepicker("setDate", t5.substring(0, 10));

    var t6 = e.hidtxtDepDate6.val();
    //e.txtDepDate6.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateRoundTripMininumDate
    //}).datepicker("setDate", t6.substring(0, 10));

    e.txtDepDate6.datepicker(r).datepicker("setDate", t6.substr(0, 10));


    //


    var i = UrlBase + "CitySearch.asmx/FetchCityList";
    //var TP = UrlBase + "CitySearch.asmx/FetchCityListFD";
    e.txtDepCityFD.autocomplete({
        source: function (e, t) {
            $.ajax({
                url: TP,
                data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    t($.map(e.d, function (e) {
                        var t = e.CityName;
                        var n = e.CountryCode;
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

            e.hidtxtDepCityFD.val(n.item.id)
            $('#txtDepCityFD').focus();
        }
    });
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
            $('#txtArrCity1F').focus();
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
            $('#txtDepDateF').focus();
            
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
            $('#txtDepDateF').focus();
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
            $('#txtDepDate2F').focus();

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
            $('#txtDepDate3F').focus();
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
            $('#txtDepDate4F').focus();

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
            $('#txtDepDate5F').focus();

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
            $('#txtDepDate6F').focus();
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



        if ($(this).val() == "rdbOneWayF" || $(this).val() == "rdbRoundTripF" || $(this).val() == "rdbMultiCityF") {
           
            if ($(this).val() == "rdbRoundTripF") {
                e.UpdateRoundTripMininumDate(e.txtDepDate.val());
                e.trRetDateRow.show()
                e.returnDateSearch.show();
               // MultiCityShowHide();

                
                //$(".mltcs").removeClass("col-md-4 col-xs-4 text-search mltcs").addClass("col-md-12 col-xs-12 text-search mltcs");
                //$(".mltcrt").removeClass("col-md-12 col-xs-12 text-search mltcrt").addClass("col-md-12 col-xs-12 text-search mltcrt");
                $("#trRetDateRowF").show();
               // $(".mltcrtF").show();
               // $("#ReturnF").show();
                
            }
            else if ($(this).val() == "rdbOneWayF") {
                
              //  $(".mltcs").removeClass("col-md-4 col-xs-4 text-search mltcs").addClass("col-md-12 col-xs-12 text-search mltcs");
                $("#trRetDateRowF").hide();
                $(".mltcOTF").show();
                $("#addF,#twoF,#threeF").hide();
            }            
            else if ($(this).val() == "rdbMultiCityF") {
                e.trRetDateRow.hide()
                e.returnDateSearch.hide();
               // MultiCityShowHide();
               // $(".mltcs").removeClass("col-md-12 col-xs-12 text-search mltcs").addClass("col-md-4 col-xs-4 text-search mltcs");
                //$(".mltcs").removeClass("col-md-6 col-xs-6 text-search mltcs").addClass("col-md-4 col-xs-4 text-search mltcs");
                //$(".mltcrt").removeClass("col-md-6 col-xs-6 text-search mltcrt").addClass("col-md-4 col-xs-4 text-search mltcs");
               // $("#ReturnF").hide();
              //  $(".mltcOTF").show();
               // $(".mltcrtF").hide();
            }
            else {
                e.trRetDateRow.hide()
                e.returnDateSearch.hide();
                //MultiCityShowHide();
                $(".mltcrtF").show();

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
SearchHelperF.prototype.UpdateRoundTripMininumDate = function (e, t) {
    SHandlerF.txtRetDate.datepicker("option", {
        minDate: e
    });
    SHandlerF.txtDepDate2.datepicker("option", { minDate: e });
};



//SearchHelper.prototype.UpdateMultiCityMininumDate1 = function (e, t) {
//    SHandler.txtDepDate2.datepicker("option", { minDate: e });
//    SHandler.txtDepDate3.datepicker("option", { minDate: e });
//};

SearchHelperF.prototype.UpdateMultiCityMininumDate2 = function (e, t) {

    //SHandler.txtDepCity3.datepicker("option", {
    //    minDate: e
    //})
    SHandlerF.txtDepDate3.datepicker("option", { minDate: e });
    SHandlerF.txtDepDate4.datepicker("option", { minDate: e });
};

SearchHelperF.prototype.UpdateMultiCityMininumDate3 = function (e, t) {
    //SHandler.txtDepCity4.datepicker("option", {
    //    minDate: e
    //})
    SHandlerF.txtDepDate4.datepicker("option", { minDate: e });
    SHandlerF.txtDepDate5.datepicker("option", { minDate: e });
};

SearchHelperF.prototype.UpdateMultiCityMininumDate4 = function (e, t) {
    SHandlerF.txtDepDate5.datepicker("option", {
        minDate: e
    })
};

SearchHelperF.prototype.UpdateMultiCityMininumDate5 = function (e, t) {
    SHandlerF.txtDepDate6.datepicker("option", {
        minDate: e
    })
};


SearchHelperF.prototype.validate = function () {
    var e = this;

    if (document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector").value == "0")
    {
        alert("Select Sector")
        return false
    }

    var ee = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
    var HidSector = ee.options[ee.selectedIndex].value;
    var array = HidSector.split("-")
  
    var t = $("input[name='TripTypeF']:checked").val();
    if (array.length == 3) {
        t = "rdbRoundTripF";
    }
    else {
        t == "rdbOneWayF"
    }

    $("#chkNonstop").attr("checked", true); e.chkNonstop.val("TRUE")


    
    CheckTripF(e.hidtxtDepCity1.val(), e.hidtxtArrCity1.val());
    if (t == "rdbOneWayF" || t == "rdbRoundTripF" || t == "rdbMultiCityF") {
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

                if ($('#hidtxtDepCityF' + i).val() == "" && $('#hidtxtArrCityF' + i).val() != "") {

                    alert("Origin city is required");
                    $('#txtDepCityF' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCityF' + i).val() != "" && $('#hidtxtArrCityF' + i).val() == "") {

                    alert("Destination city is required.");
                    $('#txtArrCityF' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCityF' + i).val() != "" && $('#hidtxtArrCityF' + i).val() != "" && $('#txtDepDateF' + i).val() == "") {

                    alert("Departure Date is required.");
                    $('#txtDepDateF' + i).focus();
                    return false;
                    break;

                }

                if ($('#hidtxtDepCityF' + i).val() == "" && $('#hidtxtArrCityF' + i).val() != "" && $('#txtDepDateF' + i).val() != "") {

                    alert("Origin city is required.");
                    $('#txtDepCityF' + i).focus();
                    return false;
                    break;

                }


                if ($('#hidtxtDepCity' + i).val() != "" && $('#hidtxtArrCityF' + i).val() == "" && $('#txtDepDateF' + i).val() != "") {
                    alert("Destination city is required.");
                    $('#txtArrCityF' + i).focus();
                    return false;
                    break;

                }


                if ($.trim($('#hidtxtDepCityF' + i).val()) != "" && $.trim($('#hidtxtArrCityF' + i).val()) != "") {
                    if ($.trim($('#hidtxtDepCityF' + i).val()) == $.trim($('#hidtxtArrCityF' + i).val())) {
                        alert("Origin and destination must not be the same.");
                        $('#txtArrCityF' + i).focus();
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
        if (t == "rdbRoundTripF") {
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
        if (e.LCC_RTF.is(":checked") == true && t != "rdbRoundTripF") {
            e.LCC_RTF.attr("checked", false);
            alert("Please select round trip.");
            return false
        }
        if (e.GDS_RTF.is(":checked") == true && t != "rdbRoundTripF") {
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
SearchHelperF.prototype.StartSearch = function () {

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


    var t = $("input[name='TripTypeF']:checked").val();


    var eee = document.getElementById("ctl00_ContentPlaceHolder1_FixDep_Sector");
    var HidSector1 = eee.options[eee.selectedIndex].value;
    var array = HidSector1.split("-")

    var t = $("input[name='TripTypeF']:checked").val();
    if (array.length == 3) {
        t = "rdbRoundTripF";
    }
    else {
        t == "rdbOneWayF"
    }


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

    var s = "TripType=" + t + "&txtDepCity1=" + txtDepCity1F + "&txtArrCity1=" + txtArrCity1F + "&hidtxtDepCity1=" + hidtxtDepCity1 + "&hidtxtArrCity1=" + hidtxtArrCity1 + "&Adult=" + e.Adult.val();
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

//function MultiCityShowHide() {

//    var trip = $("input[name='TripTypeF']:checked").val();
//    if (trip == "rdbMultiCityF") {

        
//        $("#twoF").show();
//        $("#threeF").show();
//        $("#addF").show();
//        $("#minusF").hide();
//    }
//    else {
//        $("#twoF").hide();
//        $("#threeF").hide();
//        $("#fourF").hide();
//        $("#fiveF").hide();
//        $("#sixF").hide();
//        //$("#minus").hide();
//        //$("#plus").hide();
//        $("#addF").hide();


//    }

//}
