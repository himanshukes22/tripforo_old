function Show(obj) {

    if (obj.checked) {
//        document.getElementById("txtRetDate").style.display = "block";
//        document.getElementById("td_ret").style.display = "block";
        //document.getElementById("td_ag").style.display = "block";
        document.getElementById("td_ag").style.display = "block";
    }
}
function Hide(obj) {
    if (obj.checked) {
//        document.getElementById("txtRetDate").style.display = "none";
//        document.getElementById("td_ret").style.display = "none";
//        document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "none";
        document.getElementById("td_ag").style.display = "none";
    }


}
function Show1(obj) {

    if (obj.checked) {
        //        document.getElementById("txtRetDate").style.display = "block";
        //        document.getElementById("td_ret").style.display = "block";
        //document.getElementById("td_ag").style.display = "block";
        document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "block";
        document.getElementById("ctl00_ContentPlaceHolder1_tr_Agency").style.display = "block";

    }
    
}
function Hide1(obj) {
    if (obj.checked) {
        //        document.getElementById("txtRetDate").style.display = "none";
        //        document.getElementById("td_ret").style.display = "none";
        //        document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "none";
        document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "none";
        document.getElementById("ctl00_ContentPlaceHolder1_tr_Agency").style.display = "none";

    }


}

function ShowLed(obj) {
    if (obj.checked) {
    
        document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "block";
    }
}
function HideLed(obj) {
    if (obj.checked) {        
         document.getElementById("ctl00_ContentPlaceHolder1_tr_AgencyName").style.display = "none";        
    }
}