function Validate() {
    if (document.getElementById("txtairline").value == "") {
        alert('Please provide Airline');
        document.getElementById("txtairline").focus();
        return false;
    }
    if (document.getElementById("txtaircode").value == "") {
        alert('Please provide Itinerary');
        document.getElementById("txtaircode").focus();
        return false;
    }
    if (document.getElementById("txtsector").value == "") {
        alert('Please provide Sector');
        document.getElementById("txtsector").focus();
        return false;
    }
    if (document.getElementById("txtamount").value == "") {
        alert('Please provide Amount');
        document.getElementById("txtamount").focus();
        return false;
    }
    if (document.getElementById("txtseat").value == "") {
        alert('Please provide Available Seat');
        document.getElementById("txtseat").focus();
        return false;
    }
    if (document.getElementById("txtdeptdate").value == "") {
        alert('Please provide Departure date');
        document.getElementById("txtdeptdate").focus();
        return false;
    }
    if (document.getElementById("txtretdate").value == "") {
        alert('Please provide Return Date');
        document.getElementById("txtretdate").focus();
        return false;
    }


}
function ValidateACC() {
    if (document.getElementById("txtagency").value == "") {
        alert('Please provide Airline');
        document.getElementById("txtagency").focus();
        return false;
    }
    if (document.getElementById("txtamount").value == "") {
        alert('Please provide Itinerary');
        document.getElementById("txtamount").focus();
        return false;
    }

    if (document.getElementById("txtremark").value == "") {
        alert('Please provide Airline');
        document.getElementById("txtremark").focus();
        return false;
    }



}
function ValidateSeries() {
    if (document.getElementById('txtadult').value == "") {
        alert('Please enter atleast one Adult')
        document.getElementById('txtadult').focus()
        return false;
    }
    if (document.getElementById('txtCntctPerson').value == "") {
        alert('Please enter contact person name')
        document.getElementById('txtCntctPerson').focus()
        return false;
    }
    
    var phone = document.getElementById('txtCntctPersonNo').value;
    if (phone.length != 10) {
        alert('Please Enter 10 Digit Mobile No')
        document.getElementById('txtCntctPersonNo').focus();
        return false;
    }
    if (document.getElementById('txtCntctEmailid').value == "") {
        alert('Please enter EmailID')
        document.getElementById('txtCntctEmailid').focus()
        return false;
    }
    var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    var emailid = document.getElementById("txtCntctEmailid").value;
    var matchArray = emailid.match(emailPat);
    if (matchArray == null) {
        alert("Your email address seems incorrect. Please try again.");
        document.getElementById("txtCntctEmailid").focus();
        return false;
    }
    if (document.getElementById('txtreamrk').value == "") {
        alert('Please fill Remark')
        document.getElementById('txtreamrk').focus()
        return false;
    }
    

}
function ValidateNoPax(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}
function confirmdelete() {

    if (confirm('Are You Sure?'))
        return true;
    else
        return false;

}