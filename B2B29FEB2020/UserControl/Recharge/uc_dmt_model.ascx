<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_dmt_model.ascx.cs" Inherits="DMT_Manager_User_Control_uc_dmt_model" %>

<button type="button" class="btn btn-info btn-lg popupopenclass hidden" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#PopupOpenId"></button>
<div class="modal fade" id="PopupOpenId" role="dialog">
    <div class="modal-dialog modal-md" style="margin: 15% auto!important;">
        <div class="modal-content" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-header" style="padding: 12px;">
                <h5 class="modal-title popupheading"></h5>
            </div>
            <div class="modal-body" style="padding-left: 20px!important;">
                <h6 class="popupcontent"></h6>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" style="padding: 0.5rem 1rem!important;" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

<button type='button' class='btn btn-success btn-sm hidden remitterreg' data-backdrop='static' data-keyboard='false' data-toggle='modal' data-target='#FormRegistration'>Registration</button>
<div class="modal fade" id="FormRegistration" role="dialog">
    <div class="modal-dialog modal-md" id="RegistMargin" style="margin: 3% auto!important;">
        <div class="modal-content" id="RegistSectionForm" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding-left: 20px!important;">
                <div class="container">
                    <div class="login-signup-page mx-auto">
                        <h3 class="font-weight-400 text-center">Sign Up for DMT
                         <button type="button" id="btnregclose" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true" style="color: #ff414d;">×</span></button>
                        </h3>
                        <p class="lead text-center">Your Sign Up information is safe with us.</p>
                        <div class="bg-light shadow-md rounded p-4 mx-2">
                            <div class="form-group form-validation">
                                <input id="txtRegMobileNo" class="form-control" placeholder="Enter Mobile No." maxlength="10" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
                                <%--<p id="moberrormsg" class="text-danger text-right"></p>--%>
                            </div>
                            <%--onkeypress="return isNumberValidationPrevent(event);"--%>
                            <div class="form-group form-validation">
                                <input id="txtRegFirstName" class="form-control" placeholder="Enter First Name" />
                            </div>
                            <div class="form-group form-validation">
                                <input id="txtRegLastName" class="form-control" placeholder="Enter Last Name" />
                            </div>
                            <div class="form-group form-validation">
                                <input id="txtRegPinCode" class="form-control" placeholder="Enter Area Pin Code" maxlength="6" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
                            </div>
                            <div class="form-group form-validation">
                                <textarea id="txtCurrLocalAddress" class="form-control" placeholder="Current / Local Address"></textarea>
                            </div>
                            <span id="btnRemtrRegistration" style="cursor: pointer;" class="btn btn-primary btn-block my-4" onclick="javascript: return SubmitRegRemitter();">Register</span>
                            <p id="perrormessage" class="text-danger"></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal-content hidden" style="text-align: center; border-radius: 5px !important;" id="OTPSectionForm">
            <div class="container">
                <div class="login-signup-page mx-auto">
                    <h3 class="font-weight-400 text-center text-success">
                        <button type="button" id="btnregformclose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="color: #ff414d;">×</span>
                        </button>
                    </h3>
                    <p class="lead text-center text-success" id="RegOtpHeading">Otp sent successfully to your mobile number.</p>
                    <div class="bg-light shadow-md rounded p-4 mx-2">
                        <div class="form-group form-validation">
                            <label style="float: left; font-size: 1rem;">Enter OTP Number</label>
                            <input type="text" id="txtRegOtp" class="form-control" placeholder="Enter OTP Number." onkeypress="return isNumberValidationPrevent(event);" />
                        </div>
                        <div class="row form-group form-validation">
                            <div class="col-sm-6"><span id="btnRegOtpVarification" style="cursor: pointer;font-size: 15px;" class="btn btn-primary btn-block my-4" onclick="javascript: return SubmitRegOtpVerification();">Verify</span></div>
                            <div class="col-sm-6">
                                <span id="btnResendOTP" style="cursor: pointer;font-size: 15px;" class="btn btn-primary btn-block my-4 hidden" onclick="ResendOTP();">Resend OTP</span>
                                <span id="timersection" class="btn btn-primary btn-block my-4 hidden"><span id="minRemaing"></span>:<span id="secRemaing"></span></span>
                            </div>
                        </div>                        
                        <p id="otperrormessage" class="text-danger"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<input id="hdnRegRemtId" type="hidden" />