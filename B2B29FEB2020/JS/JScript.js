function Proxy() {
    //    var from = document.getElementById("ctl00_ContentPlaceHolder1_ddl_From").value
    //    var myControlId = document.getElementById('<%=txt_Remark.ClientID%>').value;

    //if (document.getElementById("ctl00_lblCamt").innerHTML.replace("INR", "") < 5000) {

    //    alert('your minimum account balance should be greater than or equal to INR. 5000 to send proxy request.');
    //    return false;


    //}

    if (document.getElementById("txtDepCity1").value == "") {

        alert('Please Fill Departure City');
        document.getElementById("txtDepCity1").focus();
        return false;


    }
    if (document.getElementById("hidtxtDepCity1").value == "") {

        alert('Incorrect Departure City');
        document.getElementById("txtDepCity1").focus();
        return false;


    }
    if (document.getElementById("txtArrCity1").value == "") {

        alert('Please Fill Arrival City');
        document.getElementById("txtArrCity1").focus();
        return false;


    }
    if (document.getElementById("hidtxtArrCity1").value == "") {

        alert('Incorrect Arrival City');
        document.getElementById("txtArrCity1").focus();
        return false;


    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_Infrant").value > document.getElementById("ctl00_ContentPlaceHolder1_ddl_Adult").value) {

        alert('No of infant should not greater than Adults');
        document.getElementById("ctl00_ContentPlaceHolder1_ddl_Infrant").focus();
        return false;


    }

    if (document.getElementById("txtAirline").value == "") {

        alert('Please Fill Airline');
        document.getElementById("txtAirline").focus();
        return false;


    }
    if (document.getElementById("hidtxtAirline").value == "") {

        alert('Invalid Airline');
        document.getElementById("txtAirline").focus();
        return false;


    }

   // if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Remark").value == "") {

     //   alert('Please fill remark');
        //document.getElementById("ctl00_ContentPlaceHolder1_txt_Remark").focus();
     //   return false;


   // }

}
function InsertProxy() {

    if (document.getElementById("ctl00_ContentPlaceHolder1_td_TravelType").innerHTML == "One Way") {

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_DeptTime").value == "") {

            alert('Please Fill Departure Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_DeptTime").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalDate").value == "") {

            alert('Please Fill Arival Date');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalDate").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalTime").value == "") {

            alert('Please Fill Arrival Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalTime").focus();
            return false;
        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_TktingAirline").value == "") {

            alert('Please Fill Ticketing Airline');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_TktingAirline").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_GDSPNR").value == "") {

            alert('Please Fill GDSPNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_GDSPNR").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_AirlinePNR").value == "") {

            alert('Please Fill Airline PNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_AirlinePNR").focus();
            return false;

        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Flight").value == "") {

            alert('Please Fill FlightNo');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_Flight").focus();
            return false;
        }
        //        if (confirm("Are you sure you want to Update Proxy!"))
        //            return true;
        //        return false;

    }



    if (document.getElementById("ctl00_ContentPlaceHolder1_td_TravelType").innerHTML == "Round Trip") {

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_DeptTime").value == "") {

            alert('Please Fill Departure Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_DeptTime").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalDate").value == "") {

            alert('Please Fill Arival Date');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalDate").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalTime").value == "") {

            alert('Please Fill Arrival Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ArivalTime").focus();
            return false;
        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_TktingAirline").value == "") {

            alert('Please Fill Ticketing Airline');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_TktingAirline").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_RetTime").value == "") {

            alert('Please Fill Returning Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_RetTime").focus();
            return false;
        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReADate").value == "") {

            alert('Please Fill Returning Arrival Date');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReADate").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReATime").value == "") {

            alert('Please Fill Returning Arrival Time');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReATime").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReTktingAirline").value == "") {

            alert('Please Fill Returning Returning Ticketing Airline');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReTktingAirline").focus();
            return false;
        }


        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_GDSPNR").value == "") {

            alert('Please Fill GDSPNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_GDSPNR").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_AirlinePNR").value == "") {

            alert('Please Fill Airline PNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_AirlinePNR").focus();
            return false;

        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Flight").value == "") {

            alert('Please Fill FlightNo');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_Flight").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReGDSPNR").value == "") {

            alert('Please Fill Returning GDS PNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReGDSPNR").focus();
            return false;
        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_GDSPNR").value == document.getElementById("ctl00_ContentPlaceHolder1_txt_ReGDSPNR").value) {

            alert('Returning GDS PNR and GDS PNR can not be same');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReGDSPNR").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReAirlinePNR").value == "") {

            alert('Please Fill Returning Airline PNR');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReAirlinePNR").focus();
            return false;
        }
        if (document.getElementById("ctl00_ContentPlaceHolder1_txt_ReFlight").value == "") {

            alert('Please Fill Returning FlightNo');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_ReFlight").focus();
            return false;
        }

        //        if (confirm("Are you sure you want to Update Proxy!"))
        //            return true;
        //        return false;

    }
    if (confirm("Are you sure you want to Update Proxy!")) {
        document.getElementById("div_Submit").style.display = "none";
        document.getElementById("div_Progress").style.display = "block";
        return true;

    }
    else {
        return false;
    }

}


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
        return true;
    }
    else {

        return false;
    }
}
function ConfirmFlightBooking() {


    if (confirm("Are you sure!"))
        return true;
    return false;


}
function validate() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_cancel").value == "") {
        alert('Regarding cancellation Details Should not be blank');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_cancel").focus();
        return false;
    }
    if (confirm('Are You Sure'))

        return true;
    return false;
}

function validateComment() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_comment").value == "") {
        alert('Rejection Comment Should not be blank');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_comment").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_reissuecomment").value == "") {
        alert('Rejection Comment Details Should not be blank');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_reissuecomment").focus();
        return false;
    }
    if (confirm('Are You Sure'))

        return true;
    return false;
}
function validateReIssue() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_pnr").value == "") {
        alert('Please Enter PNR Here.');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_pnr").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_reissue").value == "") {
        alert('Regarding Reissue Details Should not be blank');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_reissue").focus();
        return false;
    }
    if (confirm('Are You Sure'))

        return true;
    return false;
}
function Validation() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_from").value == "") {

        alert('Please Fill from Date');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_from").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_to").value == "") {

        alert('Please Fill To Date');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_to").focus();
        return false;
    }
}

function ValidateDeposit() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_amount").value == "") {

        alert('Please Fill Amount');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_amount").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "") {

        alert('Please Mention Mode Of Payment');
        document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_bankname").value == "") {

        if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value != "Cash") {
            alert('Please Mention Bank Name');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_bankname").focus();
            return false;
        }
        else { }
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_areacode").value == "") {

        alert('Please Mention Bank Area Code');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_areacode").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_city").value == "") {
        if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value != "Cash") {
            alert('Please Mention City');
            document.getElementById("ctl00_ContentPlaceHolder1_txt_city").focus();
            return false;
        }
        else { }
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {

        alert('Please Mention Remark');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
        return false;
    }

}
function rejcomment() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_comment").value == "") {

        alert('Please Mention Rejection Remark');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_comment").focus();
        return false;
    }
}
function UpdateParameters() {

    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_date").value == "") {
        alert('Please Mention Departure Date');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_date").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txtDepTime").value == "") {
        alert('Please Mention Departure Time');
        document.getElementById("ctl00_ContentPlaceHolder1_txtDepTime").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_pnr").value == "") {
        alert('Please Mention GDS PNR');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_pnr").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txtAirPnr").value == "") {
        alert('Please Mention Airline PNR');
        document.getElementById("ctl00_ContentPlaceHolder1_txtAirPnr").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txtTktNo").value == "") {
        alert('Please Mention Ticket No.');
        document.getElementById("ctl00_ContentPlaceHolder1_txtTktNo").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_charge").value == "") {
        alert('Please Mention ReIssue Charge');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_charge").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_servicecharge").value == "") {
        alert('Please Mention Service Charge');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_servicecharge").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_farediff").value == "") {
        alert('Please Mention Fare Diff Charge');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_farediff").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {
        alert('Please Mention Reamrk');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
        return false;
    }
}
function fltrclick(id) {
     
    $("#" + id).toggleClass("closeopen1");
    $("#" + id + 1).slideToggle();
}

function fltrclick1(id) {
    $(".collapse").toggleClass("hide");
    $(".fltbox2").toggleClass("w100");
    $("#" + id + 1).toggle();
}

function mtrxx(id) {
    $("*").removeClass("mtrx1");
    $("#" + id).addClass("mtrx1");
}

function myfunction(obj) {    
    var clsraw = $(obj).attr("class").split(" ")[2];
    $(obj).toggleClass("srtarw1");
}