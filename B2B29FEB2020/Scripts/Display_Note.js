
//var usr = document.getElementById("widget_agent_id").value;
//var ust = document.getElementById("widget_agent_type").value;
function getNotificationFromLocalStorage() {


    if (typeof (Storage) !== "undefined") {
        var retrievedObject = localStorage.getItem('added-items');
        // CONVERT STRING TO REGULAR JS OBJECT
        var adddivAll = document.getElementById("divResults");
        var parsedObject = JSON.parse(retrievedObject);
        var url = document.URL
        var pageurlname = (url.substr(url.lastIndexOf('/') + 1)).split(".")[0];
        var htmlsdr = '<div class="slidercontainer">'
        var htmlmaq = '<marquee><b>'
        htmlsdr += '<div id="closent" style="float:right;background-color:black;color:white;width: 26px;height: 30px;padding: 7px;"  onclick="return Closexx()"> X </div>'
        htmlsdr += '<div id="hrdr">Notification</div>'

        var checkidexistvv = localStorage.getItem('compr');
        var checkidexist = JSON.parse(checkidexistvv);
        var fill = 0;
        for (i = 0; i < parsedObject.length; i++) {

            var status = 0;

            if (checkidexist != null) {

                for (var xx = 0; checkidexist.length >= xx; xx++) {
                    if (parsedObject[i].ID == checkidexist[xx]) {
                        status = 1
                        break;
                    }
                }
            }

            if (status == 0) {


                if (parsedObject[i].TypeOfmsg == "Password") {

                    if (pageurlname.toLowerCase() == parsedObject[i].PageName.toLowerCase() || parsedObject[i].PageName == '') {
                        document.getElementById('divResults').style.display = "block";
                        fill = 1;
                        htmlsdr += '<div class="showSlide fadde">';
                        htmlsdr += '<div style="color:red;font-size:12px;height:22px"></div>';


                        htmlsdr += '<div style="clear:both"></div>'
                        htmlsdr += '<div class="leftt"  onclick="return nextSlide(-1)">Prev</div>';
                        htmlsdr += '<div class="rightt" onclick="return nextSlide(1)">Next</div>';
                        htmlsdr += '<div class="content"><span style="font-family:"Times New Roman", Times, serif;"><span style="padding-left:4px; font-weight:bold; text-decoration:underline;color:red;font-size:15px;">' + parsedObject[i].Title.trim() + ' </span><br />    Notice: ' + parsedObject[i].Message.trim() + '</span></div>';
                        htmlsdr += '</div>';
                    }
                }
             
                if (parsedObject[i].TypeOfmsg == "PopUP") {
                   
                    if (pageurlname.toLowerCase() == parsedObject[i].PageName.toLowerCase() || parsedObject[i].PageName == '') {
                        document.getElementById('divResults').style.display = "block";
                        fill = 1;
                        htmlsdr += '<div class="showSlide fadde">';
                        htmlsdr += '<div style="color:red;font-size:12px;"><b>DO NOT SHOW ME</b><input type="checkbox" onclick="onClickHandler(' + parsedObject[i].ID + ')" idck=' + parsedObject[i].ID + ' /></div>';
                        
                        htmlsdr += '<div style="clear:both"></div>'
                        htmlsdr += '<div class="leftt"  onclick="return nextSlide(-1)">Prev</div>';
                        htmlsdr += '<div class="rightt" onclick="return nextSlide(1)">Next</div>';
                        htmlsdr += '<div class="content"><span style="font-family:"Times New Roman", Times, serif;"><span style="padding-left:4px; font-weight:bold; text-decoration:underline;color:red;font-size:15px;">' + parsedObject[i].Title.trim() + ' </span><br />    Notice: ' + parsedObject[i].Message.trim() + '</span></div>';
                        htmlsdr += '</div>';
                    }
                }
                if (parsedObject[i].TypeOfmsg == "marquee") {
                    htmlmaq += '' + parsedObject[i].Title.trim() + ' - ' + parsedObject[i].Message.trim() + ",  ";
                }
            }


        }
       
        htmlsdr += '</div>';
        adddivAll.innerHTML = htmlsdr;
        htmlmaq += '</b></marquee>'
        Marqueediv.innerHTML = htmlmaq;
        htmlmaq = "";

        htmlsdr = "";
        if (fill == 1) {
            var slide_index = 1;
            displaySlides(slide_index);
        }
    }
}

function displaySlides(n) {
    var i;
    var slides = document.getElementsByClassName("showSlide");
    if (n > slides.length) { slide_index = 1 }
    if (n < 1) { slide_index = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slide_index - 1].style.display = "block";
}    


function getNotification(userid, usertype) {
  // var i = setInterval(function () {
    //Call ajax here
    $.ajax({
        type: "POST",
        url: UrlBase + "NotificationDisplay.asmx/GetData",
        data: "{ 'userid': '" + userid + "','usertype': '" + usertype + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var myObj = JSON.parse(response.d);
            localStorage.setItem('added-items', JSON.stringify(myObj));
            getNotificationFromLocalStorage();      
        },   
        failure: function (response) {
            alert(response.d);
        }
    });
   //}, 5000, true)
}


function getNotificationTimer(userid, usertype) {

    var i = setInterval(function () {
    //Call ajax here
    $.ajax({
        type: "POST",
        url: UrlBase + "NotificationDisplay.asmx/GetData",
        data: "{ 'userid': '" + userid + "','usertype': '" + usertype + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var myObj = JSON.parse(response.d);
            localStorage.setItem('added-items', JSON.stringify(myObj));
            getNotificationFromLocalStorage();
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    }, 1200000, true)   //call after 10 min 
}

//var myVar = setInterval(getNotification(userid, usertype), 5000);
var slide_index = 1;
function nextSlide(n) {
    displaySlides(slide_index += n);
}

function currentSlide(n) {
    displaySlides(slide_index = n);
}
function displaySlides(n) {
    var i;
    var slides = document.getElementsByClassName("showSlide");
    if (n > slides.length) { slide_index = 1 }
    if (n < 1) { slide_index = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slide_index - 1].style.display = "block";
}
function Closexx() {
    $("#divResults").hide();

}


function onClickHandler(chkvalue) {
    var Arry = [];
    Arry.push(chkvalue);
    //$('#' + chkvalue).hide();
    var checkidexistvv = localStorage.getItem('compr');

    if (checkidexistvv != null)
    {      
        var checkidexist = JSON.parse(checkidexistvv);
        var checkidexistvv
        checkidexist.push(chkvalue);

        localStorage.setItem('compr', JSON.stringify(checkidexist));
    }
    else
    {
        localStorage.setItem('compr', JSON.stringify(Arry));
    }
}
