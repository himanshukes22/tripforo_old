
var SHandler;
$(document).ready(function () {
    SHandler = new SearchHelper();
    SHandler.BindEvents();
});
var SearchHelper = function () {


    this.txtAgencyName = $("#txtAgencyName");
    this.hidtxtAgencyName = $("#hidtxtAgencyName");

    this.Form = $("#From");
    this.To = $("#To");
    //this.hidForm = $("#hidForm");

}

SearchHelper.prototype.BindEvents = function () {
    // 
    var h = this;



    // h.txtRetDate.datepicker(dtPickerOptions).datepicker("setDate", returnDate.substr(0, 10));




    //Origin AutoComplete

    //Url name should be relative
    //var autoCity = UrlBase + "AgencySearch.asmx/FetchAgencyList";
  //  var autoCity =  "AgencySearch.asmx/FetchAgencyList";
    // 
    if (h.txtAgencyName.length != 0) {
        var autoCity = UrlBase + "AgencySearch.asmx/FetchAgencyList";
        h.txtAgencyName.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: autoCity,
                    data: "{ 'city': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {
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
                h.hidtxtAgencyName.val(ui.item.id);
            }
        });
    }
    var FromDate = h.Form.val();
    var ToDate = h.To.val();

    //var returnDate = h.hidtxtRetDate.val();
    //Date Picker Bind
    
    var dtPickerOptions = {
        numberOfMonths: 1, dateFormat: "dd-mm-yy", maxDate: "+1y", minDate: "-2y", showOtherMonths: true, selectOtherMonths: false
    };
    if (h.Form.length != 0) {
        h.Form.datepicker(dtPickerOptions).datepicker("option", { onSelect: h.UpdateRoundTripMininumDate }).datepicker("setDate", FromDate.substr(0, 10));
    }
    if (h.To.length != 0) {
        h.To.datepicker(dtPickerOptions).datepicker("option", { onSelect: h.UpdateRoundTripMininumDate }).datepicker("setDate", ToDate.substr(0, 10));
    }
    //Destination AutoComplete

    h.txtAgencyName.autocomplete({
        source: function (request, response) {
            $.ajax({
                url: autoCity,
                data: "{ 'city': '" + request.term + "', maxResults: 10 }",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
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
            h.hidtxtAgencyName.val(ui.item.id);
        }
    });
}


function focusObj(obj) {
    if (obj.value == "Agency Name or ID") obj.value = "";

}

function blurObj(obj) {
    if (obj.value == "") obj.value = "Agency Name or ID";


}
