
var SHandler;
$(document).ready(function () {
    SHandler = new SearchHelper;
    SHandler.BindEvents()
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


    this.txtAgencyName = $("#txtAgencyName");
    this.hidtxtAgencyName = $("#hidtxtAgencyName");


    this.rdbMultiCity = $("#rdbMultiCity");
    this.txtDepCity1 = $("#txtDepCity1");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    //this.txtDepDate1 = $("#txtDepDate1");
    //this.hidtxtDepDate1 = $("#hidtxtDepDate1");

};
SearchHelper.prototype.BindEvents = function () {
    var e = this;
    var t = e.hidtxtDepDate.val();
    var n = e.hidtxtRetDate.val();
    var r = {
        numberOfMonths: 2,
        dateFormat: "dd/mm/yy",
        maxDate: "+1y",
        minDate: 0,
        showOtherMonths: true,
        selectOtherMonths: false
    };
    
    //For Multicity



    //var t1 = e.hidtxtDepDate1.val();
    //e.txtDepDate1.datepicker(r).datepicker("option", {
    //    onSelect: e.UpdateMultiCityMininumDate1
    //}).datepicker("setDate", t1.substring(0, 10));
    if (e.txtAgencyName.length != 0) {
        var autoCity = UrlBase + "AgencySearch.asmx/FetchAgencyList";
        e.txtAgencyName.autocomplete({
            source: function (request, response) {
                debugger;
                $.ajax({
                    url: autoCity,
                    data: "{ 'city': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {
                        debugger;
                        response($.map(data.d, function (item) {
                            var result = item.Agency_Name + "(" + item.User_Id + ")";
                            var hidresult = item.User_Id;
                            return { label: result, value: result, id: hidresult }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                        alert(textStatus);
                    }
                })
            },

            autoFocus: true,
            minLength: 3,
            select: function (event, ui) {
                e.hidtxtAgencyName.val(ui.item.id);
                //If the No match found" item is selected, clear the TextBox.
            }
        
        });
    }







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
            //If the No match found" item is selected, clear the TextBox.
            if (n.item.val == -1) {
                //Clear the AutoComplete TextBox.
                $(this).val("");
            }
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
            //If the No match found" item is selected, clear the TextBox.
            if (n.item.val == -1) {
                //Clear the AutoComplete TextBox.
                $(this).val("");
            }
            //e.txtDepCity2.val("")            
            //e.hidtxtDepCity2.val("")
            //if (e.rdbMultiCity[0].checked)
            //{
            //    e.txtDepCity2.val(n.item.label)
            //    e.hidtxtDepCity2.val(n.item.id)
            //}

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
            //If the No match found" item is selected, clear the TextBox.
        }
    });

};



function focusObj(obj) {
    if (obj.value == "Agency Name or ID") obj.value = "";

}

function blurObj(obj) {
    if (obj.value == "") {
        obj.value = "Agency Name or ID";
        $('#txtAgencyName').val("");
    }

}
function focusObjAIR(obj) {
    if (obj.value == "Search By Airlines") obj.value = "";

}

function blurObjAIR(obj) {
    if (obj.value == "") {
        obj.value = "Search By Airlines";
        $('#hidtxtAirline').val("");
    }

}
function focusObjAIRS(obj) {
    if (obj.value == "Enter Your Departure City") obj.value = "";

}

function blurObjAIRS(obj) {
    if (obj.value == "") {
        obj.value = "Enter Your Departure City";
        $('#hidtxtDepCity1').val("");
    }
}


function focusObjAIRD(obj) {
    if (obj.value == "Enter Your Destination City") obj.value = "";


}

function blurObjAIRD(obj) {
    if (obj.value == "") {
        obj.value = "Enter Your Destination City";
        $('#hidtxtArrCity1').val("");
    }
}






//SearchHelper.prototype.UpdateMultiCityMininumDate1 = function (e, t) {
//    SHandler.txtDepDate2.datepicker("option", { minDate: e });
//    SHandler.txtDepDate3.datepicker("option", { minDate: e });
//};

