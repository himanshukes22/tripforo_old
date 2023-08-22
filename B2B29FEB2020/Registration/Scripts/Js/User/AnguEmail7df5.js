var app = angular.module('myApp', []);
app.controller('appCtrl', function ($scope, $http) {
    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1]);// decodeURIComponent(results[1].replace(/\+/g, " "));
    }
    var searchId = "?searchid=" + getParameterByName("searchid");
    $scope.CheckSessionExp = function (value) {
        if (value == "SEXP") {
            location.href = "/SessionExpired";
        }
    }
    $scope.SendAgentEmail = function (cnfrmID, frmName) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        var urlNme_ = null;
        if (frmName == "AirAgentInvoice") {
            urlNme_ = "/AgentInvoice/SendEmail";
        }
        else if (frmName == "HotelAgentInvoice") {
            urlNme_ = "/HotelAgentInvoice/SendEmail";
        }
        else if (frmName == "CarAgentInvoice") {
            urlNme_ = "/CarAgentInvoice/SendEmail";
        }
        else if (frmName == "BusAgentInvoice") {
            urlNme_ = "/BusAgentInvoice/SendEmail";
        }
        else if (frmName == "HolidayDealAgentInvoice") {
            urlNme_ = "/HolidayDealInvoice/AgentInvoiceSendEmail";
        }
        var ticketNumbers_ = [];
        var CheckBoxLength = document.getElementsByName("chckOutPass");
        for (var cntPassInv = 0; cntPassInv < CheckBoxLength.length; cntPassInv++) {
            if (CheckBoxLength[cntPassInv].checked) {
                $scope.ticketNumbers_.push(CheckBoxLength[cntPassInv].value);
            }
        }
        $http({
            method: "POST",
            url: urlNme_ + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailAgent, frmName_: frmName, ticketNumbers_: ticketNumbers_ }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.SendCustomerEmail = function (cnfrmID, frmName) {
        if (!$scope.ValidateEmail('txtEmailCstmr')) {
            return false;
        }
        var urlNme_ = null;
        if (frmName == "AirCustomerInvoice") {
            urlNme_ = "/CustomerInvoice/SendEmail";
        }
        else if (frmName == "HotelCustomerInvoice") {
            urlNme_ = "/HotelCustomerInvoice/SendEmail";
        }
        else if (frmName == "CarCustomerInvoice") {
            urlNme_ = "/CarCustomerInvoice/SendEmail";
        }
        else if (frmName == "BusCustomerInvoice") {
            urlNme_ = "/BusCustomerInvoice/SendEmail";
        }
        else if (frmName == "HolidayDealCustomerInvoice") {
            urlNme_ = "/HolidayDealInvoice/CustomerInvoiceSendEmail";
        }
        $http({
            method: "POST",
            url: urlNme_ + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailCstmr, frmName_: frmName }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.ValidateEmail = function (cntrlID) {
        if (document.getElementById(cntrlID).value == '') {
            alert("Please enter email address");
            return false;
        }
        var pattern = new RegExp(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$/);
        if (!pattern.test(document.getElementById(cntrlID).value)) {
            alert("Please enter valid email address");
            return false;
        }
        else
            document.getElementById(cntrlID).value = "";
        return true;
    }

    // -----------------Bind Country List for Registration Page--------------
    $scope.CountryListRegis = function () {
        $http({
            method: "post",
            url: "/UserRegistration/GetCountryDrpDwnList" + searchId,
            data: "",
            headers: {
                'Content-Type': "application/json"
            }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            if (httpData != null) {
                $("#DrpCountry").css("display", "block");
                $("#DrpCountry").css("display", "block");
                $("#DrpCountry").html(httpData);

                $("#DrpResCountry").css("display", "block");
                $("#DrpResCountry").css("display", "block");
                $("#DrpResCountry").html(httpData);
            }
        })
        .error(function (http, status, fnc, httpObj) {
            alert("No Data Found" + httpObj + " " + status);
        });
    }
    // -----------------Bind State List for Registration Page--------------
    $scope.StateListRegis = function () {
        var cntryId = $scope.cntryId.split('|')[0];
        $http({
            method: "post",
            url: "/UserRegistration/GetStateDrpDwnList" + searchId,
            data: "{'cntryId_':'" + cntryId + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
       .success(function (httpData) {
           $scope.CheckSessionExp(httpData);
           if (httpData != null) {
               $("#DrpState").css("display", "block");
               $("#DrpState").css("display", "block");
               $("#DrpState").html(httpData);
               $("#DrpCity").empty();
           }
       })
       .error(function (http, status, fnc, httpObj) {
           alert("No Data Found" + httpObj + " " + status);
       });
    }
    // -----------------Bind City List for Registration Page--------------
    $scope.CityListRegis = function () {
        var stateId = $scope.stateId.split('|')[0];
        $http({
            method: "post",
            url: "/UserRegistration/GetCityDrpDwnList" + searchId,
            data: "{'stateId_':'" + stateId + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
       .success(function (httpData) {
           $scope.CheckSessionExp(httpData);
           if (httpData != null) {
               $("#DrpCity").css("display", "block");
               $("#DrpCity").css("display", "block");
               $("#DrpCity").html(httpData);
           }
       })
       .error(function (http, status, fnc, httpObj) {
           alert("No Data Found" + httpObj + " " + status);
       });
    }

    $scope.msg = null;
    $scope.SendForgetLink = function () {
        if (!ValidateEmail()) {
            return false;
        }
        document.getElementById("divForgetPass").innerHTML = "Please wait while we are processing your request...";
        $http({
            method: "post",
            url: "/ForgetPassword/SendLink" + searchId,
            data: "{'mailId_':'" + document.getElementById('txtEmail').value + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
      .success(function (httpData) {
          $scope.CheckSessionExp(httpData);

          if (httpData != null) {
              if (httpData == 1) {
                  $scope.msg = "Unsuccessful email sent";
              }
              else {
                  $scope.msg = httpData;
                  document.getElementById('txtEmail').value = "";
              }
              document.getElementById("divForgetPass").innerHTML = "";
          }
      })
      .error(function (http, status, fnc, httpObj) {
          alert("No Data Found" + httpObj + " " + status);
      });
    }
    $scope.ForgetPwd = function () {
        if (!ValidateForget()) {
            return false;
        }
        var encrpMail = getParameterByName("id");
        $http({
            method: "post",
            url: "/ForgetPassword/ForgetPwd",
            data: "{'encrpMail_':'" + encrpMail + "','pwd_':'" + document.getElementById('txtPassword').value + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
      .success(function (httpData) {
          $scope.CheckSessionExp(httpData);
          if (httpData != null) {
              if (httpData == 1) {
                  $scope.msg = "Update Unsuccessful !! Please Try Again";
              }
              else {
                  alert("Password Successfully Updated");
                  $scope.msg = "";
                  location.href = "/Login";
              }
              document.getElementById('txtPassword').value = "";
              document.getElementById('txtRetypePassword').value = "";
          }
      })
      .error(function (http, status, fnc, httpObj) {
          alert("No Data Found" + httpObj + " " + status);
      });
    }

    $scope.DownLoadPdf = function () {
        var ticketNumbers_ = [];
        var CheckBoxLength = document.getElementsByName("chckOutPass");
        for (var cntPassInv = 0; cntPassInv < CheckBoxLength.length; cntPassInv++) {
            if (CheckBoxLength[cntPassInv].checked) {
               ticketNumbers_.push(CheckBoxLength[cntPassInv].value);
            }
        }
        if (ticketNumbers_.length > 0) {
            document.getElementById("frmAgentInvPdfDown").hddPassenger.value = ticketNumbers_;
            document.getElementById("frmAgentInvPdfDown").action = "/AgentInvoice/AgentInvoicePaxWise";
            document.getElementById("frmAgentInvPdfDown").method = "post";
            document.getElementById("frmAgentInvPdfDown").submit();
        }
        else {
            document.getElementById("frmAgentInvPdfDown").hddPassenger.value = ticketNumbers_;
            document.getElementById("frmAgentInvPdfDown").action = "/AgentInvoice/AgentInvoice_N";
            document.getElementById("frmAgentInvPdfDown").method = "post";
            document.getElementById("frmAgentInvPdfDown").submit();
        }
    }

    $scope.AddApprovalMgrComment = function () {
        if (!ValidateMailComment()) {
            return false;
        }
        var encrpData = getParameterByName("id");
        $http({
            method: "post",
            url: "/TravelRequisition/AddApprovalMgrComment",
            data: "{'encrpData_':'" + encrpData + "','txtComment_':'" + document.getElementById('txtComment').value + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
      .success(function (httpData) {
          $scope.CheckSessionExp(httpData);
          if (httpData != null) {
              alert(httpData.Message);
              document.getElementById('txtComment').value = "";

              if (httpData.qryNo != "" && httpData.qryNo.includes('|')) {
                  var redirecturl = httpData.qryNo.split('|');
                  if(redirecturl[0]!="" && redirecturl[0].toLowerCase()=="true")
                  {
                      if (redirecturl[1] != "") {
                          window.location.href = redirecturl[1];
                      }
                      else
                      {
                          window.location.href = "/Login";
                      }
                  }
                  else {
                      window.location.href = "/Login";
                  }
              }
              else
              {
                  window.location.href = "/Login";
              }
          }
          else
          {
              window.location.href = "/Login";
          }
      })
      .error(function (http, status, fnc, httpObj) {
          alert("Something went wrong !! Please Try Again" + httpObj + " " + status);
      });
    }

    // -----------------Bind State List for Resdiential Details on Registration Page--------------
    $scope.StateListResdential = function (cntryId) {
        //var cntryId = null;
        //if ($scope.rescntryId != null) {
        //    cntryId = $scope.rescntryId.split('|')[0];
        //}
        //if (cntryId == null)
        //{
        //    cntryId = document.getElementById('DrpResCountry').value;
        //}

        cntryId = cntryId.split('|')[0];
        $http({
            method: "post",
            url: "/UserRegistration/GetStateDrpDwnList" + searchId,
            data: "{'cntryId_':'" + cntryId + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
       .success(function (httpData) {
           $scope.CheckSessionExp(httpData);
           if (httpData != null) {
               $("#DrpResState").css("display", "block");
               $("#DrpResState").css("display", "block");
               $("#DrpResState").html(httpData);
               $("#DrpResCity").empty();
               if (document.getElementById('chkSameAgncyAdd').checked) {
                   if (document.getElementById('DrpState').value != "" && document.getElementById('DrpState').value != "select") {
                       document.querySelector('#DrpResState [value="' + document.getElementById('DrpState').value + '"]').selected = true;
                   }
               }
           }
       })
       .error(function (http, status, fnc, httpObj) {
           alert("No Data Found" + httpObj + " " + status);
       });
    }
    // -----------------Bind City List for Resdiential Details on Registration Page--------------
    $scope.CityListResdential = function (stateId) {
        //var stateId = null;
        //if ($scope.resstateId != null) {
        //    stateId = $scope.resstateId.split('|')[0];
        //}
        //if (stateId == null) {
        //    stateId = document.getElementById('DrpResState').value;
        //}
        
        stateId = stateId.split('|')[0];
        $http({
            method: "post",
            url: "/UserRegistration/GetCityDrpDwnList" + searchId,
            data: "{'stateId_':'" + stateId + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
       .success(function (httpData) {
           $scope.CheckSessionExp(httpData);
           if (httpData != null) {
               $("#DrpResCity").css("display", "block");
               $("#DrpResCity").css("display", "block");
               $("#DrpResCity").html(httpData);
               if (document.getElementById('chkSameAgncyAdd').checked) {
                   if (document.getElementById('DrpCity').value != "" && document.getElementById('DrpCity').value != "select") {
                       document.querySelector('#DrpResCity [value="' + document.getElementById('DrpCity').value + '"]').selected = true;
                   }
               }
           }
       })
       .error(function (http, status, fnc, httpObj) {
           alert("No Data Found" + httpObj + " " + status);
       });
    }

    $scope.AutoFillAdd = function () {
        if (document.getElementById('chkSameAgncyAdd').checked) {
            if (document.getElementById('txtAddress').value != "") {
                document.getElementById("resAdd").style.display = "none";
            }
            if (document.getElementById('txtpincode').value != "") {
                document.getElementById("resPincode").style.display = "none";
            }
            if (document.getElementById('txtPostOfcName').value != "") {
                document.getElementById("resPostOfc").style.display = "none";
            }
            if (document.getElementById('txtOfcPh').value != "") {
                document.getElementById("resPhNo").style.display = "none";
            }

            document.getElementById('txtResAddress').value = document.getElementById('txtAddress').value;
            document.getElementById('txtRespincode').value = document.getElementById('txtpincode').value;
            document.getElementById('txtResOfcName').value = document.getElementById('txtPostOfcName').value;
            document.getElementById('txtResPhNo').value = document.getElementById('txtOfcPh').value;
            if (document.getElementById('DrpCountry').value != "select" && document.getElementById('DrpCountry').value != "") {
                document.querySelector('#DrpResCountry [value="' + document.getElementById('DrpCountry').value + '"]').selected = true;
                $scope.StateListResdential(document.getElementById('DrpCountry').value);
            }
            if (document.getElementById('DrpState').value != "" && document.getElementById('DrpState').value != "select") {
                $scope.CityListResdential(document.getElementById('DrpState').value);
            }
        }
        else
        {
            document.getElementById("resAdd").style.display = "block";
            document.getElementById("resPincode").style.display = "block";
            document.getElementById("resPostOfc").style.display = "block";
            document.getElementById("resPhNo").style.display = "block";
            document.getElementById('txtResAddress').value = "";
            document.getElementById('txtRespincode').value = "";
            if (document.getElementById('DrpCountry').value != "select" && document.getElementById('DrpCountry').value != "") {
                document.querySelector('#DrpResCountry [value="' + document.getElementById('DrpCountry').value + '"]').selected = false;
            }
            if (document.getElementById('DrpState').value != "select" && document.getElementById('DrpState').value != "") {
                document.querySelector('#DrpResState [value="' + document.getElementById('DrpState').value + '"]').selected = false;
            }
            if (document.getElementById('DrpCity').value != "select" && document.getElementById('DrpCity').value != "") {
                document.querySelector('#DrpResCity [value="' + document.getElementById('DrpCity').value + '"]').selected = false;
            }
            document.getElementById('txtResOfcName').value = "";
            document.getElementById('txtResPhNo').value = "";
        }
    }
    $scope.SendAgentCreditNoteEmail = function (cnfrmID) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        var urlNme_ = null;
        var ticketNumbers_ = [];
        var CheckBoxLength = document.getElementsByName("chckOutPass");
        for (var cntPassInv = 0; cntPassInv < CheckBoxLength.length; cntPassInv++) {
            if (CheckBoxLength[cntPassInv].checked) {
                $scope.ticketNumbers_.push(CheckBoxLength[cntPassInv].value);
            }
        }
        $http({
            method: "POST",
            url: "/CreditNote/SendEmail" + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailAgent,ticketNumbers_: ticketNumbers_,CreditNoteType_:"PaxWise" }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.DownLoadCreditNotePdf = function () {
        var ticketNumbers_ = [];
        var CheckBoxLength = document.getElementsByName("chckOutPass");
        for (var cntPassInv = 0; cntPassInv < CheckBoxLength.length; cntPassInv++) {
            if (CheckBoxLength[cntPassInv].checked) {
                ticketNumbers_.push(CheckBoxLength[cntPassInv].value);
            }
        }

        document.getElementById("frmAgentInvPdfDown").hddPassenger.value = ticketNumbers_;
        document.getElementById("frmAgentInvPdfDown").action = "/CreditNote/CreditNotePaxWise";
        document.getElementById("frmAgentInvPdfDown").method = "post";
        document.getElementById("frmAgentInvPdfDown").submit();
    }
    $scope.SendAgentSctr_TranWiseCreditNoteEmail = function (cnfrmID, CreditNoteType) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        var urlNme_ = null;
        $http({
            method: "POST",
            url: "/CreditNote/SendEmail" + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailAgent, CreditNoteType_: CreditNoteType }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.SendAgentSctr_TranWiseAgentInvoiceEmail = function (cnfrmID, AgentInviceType) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        var urlNme_ = null;
        $http({
            method: "POST",
            url: "/AgentInvoice/SendEmailInvoiceType" + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailAgent, AgentInviceType_: AgentInviceType }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.SendCstmrSctr_TranWiseAgentInvoiceEmail = function (cnfrmID, CstmrInviceType) {
        if (!$scope.ValidateEmail('txtEmailCstmr')) {
            return false;
        }
        var urlNme_ = null;
        $http({
            method: "POST",
            url: "/CustomerInvoice/SendEmailInvoiceType" + searchId,
            data: { cnfrmID_: cnfrmID, email_: $scope.EmailAgent, CstmrInviceType_: CstmrInviceType }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.SendAgentInvoiceEmail = function (InvoiceNo, frmName) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        var urlNme_ = null;
        if (frmName == "AirAgentInvoice") {
            urlNme_ = "/AgentInvoice/SendInvoiceEmail";
        }
        else if (frmName == "SecwiseAirAgentInvoice") {
            urlNme_ = "/AgentInvoice/SendInvoiceEmail";
        }
        else if (frmName == "TranwiseAirAgentInvoice") {
            urlNme_ = "/AgentInvoice/SendInvoiceEmail";
        }
        $http({
            method: "POST",
            url: "/AgentInvoice/SendInvoiceEmail" + searchId,
            data: {InvoiceNo_:InvoiceNo, email_: $scope.EmailAgent, frmName_: frmName}
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            if (httpData == 1)
            {
                alert("Email sent Failed");
            }
            else
            alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.SendCreditNoteEmail = function (InvoiceNo, frmName) {
        if (!$scope.ValidateEmail('txtEmailAgent')) {
            return false;
        }
        $http({
            method: "POST",
            url: "/CreditNote/SendCreditNoteEmail" + searchId,
            data: { InvoiceNo_: InvoiceNo, email_: $scope.EmailAgent, frmName_: frmName }
        })
        .success(function (httpData) {
            $scope.CheckSessionExp(httpData);
            if (httpData == 1) {
                alert("Email sent Failed");
            }
            else
                alert("Email sent successfully.");
            if (document.getElementById('signup').style.display == "block") {
                document.getElementById('signup').style.display = "none";
            }
        })
        .error(function (http, status, fnc, httpObj) {
        });
    }
    $scope.GetFlightDtlForBoking = function (TravelReqId, ApproveeId, BookingType, IsSpeclAppr) {
        document.getElementById('dvloader').style.display = "block";
        $http({
            method: "post",
            url: "/Confirmation/GetFlightDtlForBoking",
            data: "{'TravelReqId_':'" + TravelReqId + "','ApproveeId_':'" + ApproveeId + "','BkType_':'" + BookingType + "','IsSpeclAppr_':'" + IsSpeclAppr + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
      .success(function (httpData) {
          document.getElementById('dvloader').style.display = "none";
          if (httpData != null && httpData != '')
          {
              document.getElementById("crm").innerHTML = httpData;
          }
          else {
              alert("Booking Failed");
          }
      })
      .error(function (http, status, fnc, httpObj) {
          document.getElementById('dvloader').style.display = "none";
          alert("Something went wrong !! Please Try Again" + httpObj + " " + status);
      });
    }
    $scope.GetHotelDtlForBoking = function (TravelReqId, ApproveeId, BookingType, IsSpeclAppr) {
        document.getElementById('dvloader').style.display = "block";
        $http({
            method: "post",
            url: "/HotelConfirmTicket/GetHotelDtlForBoking",
            data: "{'TravelReqId_':'" + TravelReqId + "','ApproveeId_':'" + ApproveeId + "','BkType_':'" + BookingType + "','IsSpeclAppr_':'" + IsSpeclAppr + "'}",
            headers: {
                'Content-Type': "application/json"
            }
        })
      .success(function (httpData) {
          document.getElementById('dvloader').style.display = "none";
          if (httpData != null && httpData != '') {
              document.getElementById("crm").innerHTML = httpData;
          }
          else {
              alert("Booking Failed");
          }
      })
      .error(function (http, status, fnc, httpObj) {
          document.getElementById('dvloader').style.display = "none";
          alert("Something went wrong !! Please Try Again" + httpObj + " " + status);
      });
    }
   
});
function ValidateEmail() {
    if (document.getElementById('txtEmail').value == '') {
        Errormsg("txtEmail");
        $("#errorEmail").html("Enter email");
        return false;
    }
    else {
        document.getElementById('txtEmail').style.borderColor = "";
        $("#errorEmail").html("");
    }
    var pattern = new RegExp(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$/);
    if (!pattern.test(document.getElementById('txtEmail').value)) {
        Errormsg("txtEmail");
        $("#errorEmail").html("Enter valid email");
        return false;
    }
    else {
        document.getElementById('txtEmail').style.borderColor = "";
        $("#errorEmail").html("");
    }
    return true;
}
function ValidateForget() {
    if (document.getElementById('txtPassword').value == '') {
        Errormsg("txtPassword");
        $("#errorPassword").html("Enter new password");
        return false;
    }
    else {
        document.getElementById('txtPassword').style.borderColor = "";
        $("#errorPassword").html("");
    }

    if (document.getElementById('txtRetypePassword').value == '') {
        Errormsg("txtRetypePassword");
        $("#errorRetypePassword").html("Retype new password");
        return false;
    }
    else {
        document.getElementById('txtRetypePassword').style.borderColor = "";
        $("#errorRetypePassword").html("");
    }

    var pwd = document.getElementById("txtPassword").value;
    var cnfrmPwd = document.getElementById("txtRetypePassword").value;
    if (pwd != cnfrmPwd) {
        Errormsg("txtRetypePassword");
        $("#errorRetypePassword").html("Password do not match");
        return false;
    }
    else {
        document.getElementById('txtRetypePassword').style.borderColor = "";
        $("#errorRetypePassword").html("");
    }
    return true;
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1]);
}
function ValidateMailComment() {
    if (document.getElementById('txtComment').value == '') {
        Errormsg("txtComment");
        $("#errorComment").html("Kindly Fill Comment");
        return false;
    }
    else {
        document.getElementById('txtComment').style.borderColor = "";
        $("#errorComment").html("");
    }
    return true;
}
function ExportPdfInvoice() {
    html2canvas(document.getElementById('tblInvoice'), {
        onrendered: function (canvas) {
            var data = canvas.toDataURL();
            var docDefinition = {
                content: [{
                    image: data,
                    width: 500
                }]
            };
            pdfMake.createPdf(docDefinition).download("AgentInvoice.pdf");
        }
    });
}
