function Validate() {
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_FName").value == "")
     {
        alert('Please Enter FirstName');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_FName").focus();
        return false;
 }
 if (document.getElementById("ctl00_ContentPlaceHolder1_txt_LName").value == "") {

        alert('Please Enter LastName');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_LName").focus();
        return false;


    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Loc").value == "") {

        alert('Please Enter Location');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_Loc").focus();
        return false;


    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Mno").value == "") {

        alert('Please Enter MobileNo');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_Mno").focus();
        return false;


    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_EmailID").value == "") {
        alert("Email id can not be blank");
        document.getElementById("ctl00_ContentPlaceHolder1_txt_EmailID").focus();
        return false;
    }


    var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    var emailid = document.getElementById("ctl00_ContentPlaceHolder1_txt_EmailID").value;
    var matchArray = emailid.match(emailPat);
    if (matchArray == null) {
        alert("Your email address seems incorrect. Please try again.");
        document.getElementById("ctl00_ContentPlaceHolder1_txt_EmailID").focus();
        return false;
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Pwd").value == "") {

        alert('Please Enter Password');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_Pwd").focus();
        return false;


    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Pwd").value != document.getElementById("ctl00_ContentPlaceHolder1_txt_CPwd").value) 
    {

        alert('Password is not matching');
        document.getElementById("ctl00_ContentPlaceHolder1_txt_CPwd").focus();
        return false;
    }

    if (confirm("Are you sure !!!"))

        return true;
    return false;

}

function Confirm() {


    if (confirm("Are you sure !!!"))

        return true;
        return false;

    
}
