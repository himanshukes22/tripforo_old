<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_senderdetail_model.ascx.cs" Inherits="DMT_Manager_User_Control_uc_senderdetail_model" %>

<button type="button" class="btn btn-info btn-lg hidden addbenificiarydetail" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#FormBeneficiary"></button>
<div class="modal fade" id="FormBeneficiary" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding-left: 20px!important;">
                <div class="container" id="BeneficiarySectionForm">
                    <div class="login-signup-page mx-auto">
                        <h3 class="font-weight-400 text-center">New Beneficiary Detail
                                <button type="button" id="btnbenformclose" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                        </h3>
                        <div class="bg-light shadow-md rounded p-4 mx-2">
                            <div class="form-group form-validation">
                                <input id="txtBenAccountNo" type="text" class="form-control" placeholder="Enter Bank Account Number" onkeypress="return isNumberValidationPrevent(event);" />
                            </div>
                            <div class="form-group form-validation">
                                <select id="ddlBindBankDrop" class="form-control">
                                    <option value="">Select Bank</option>
                                </select>
                                <span id="ProcessBindingBank" class="hidden" style="position: absolute; right: 83px; top: 160px; font-size: 25px; color: #ff434f;"><i class='fa fa-pulse fa-spinner'></i></span>
                                <p id="typeofifsc" class="text-warning text-right"></p>
                            </div>
                            <div class="form-group form-validation">
                                <input id="txtBenIFSCNo" type="text" class="form-control" placeholder="Enter IFSC Number" />
                            </div>
                            <div class="form-group form-validation text-right">
                                <input id="txtBenPerName" type="text" class="form-control" placeholder="Enter Beneficiary Name" />
                                <span id="CheckAccountDetailPayout" class="text-primary btn pull-right btn-sm" style="cursor: pointer; padding: 0px!important;" onclick="return GetBeneficiaryNamePayout();">Get Beneficiary Name</span>
                            </div>
                            <%-- <div class="form-group form-validation">
                                <input id="txtBenPerMobile" type="text" class="form-control" placeholder="Enter Beneficiary Mobile Number" maxlength="10" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
                            </div>--%>
                            <span id="btnBenRegistration" style="cursor: pointer;" class="btn btn-primary btn-block my-4" onclick="javascript: return SubmitBeneficiaryDetail();">Submit</span>
                            <p id="benerrormessage" class="text-danger"></p>
                            <h6 id="bensuccessmessage" class="text-success"></h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<button type="button" class="btn btn-info btn-lg popupopenclass hidden" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#PopupMsg"></button>
<div class="modal fade" id="PopupMsg" role="dialog">
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

<button type="button" class="btn btn-info btn-lg hidden benificierypopupclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#BenificieryDetail"></button>
<div class="modal fade" id="BenificieryDetail" role="dialog">
    <div class="modal-dialog modal-md" style="margin: 10% auto!important;">
        <div class="modal-content" style="border-radius: 5px !important;">
            <div class="modal-header" style="padding: 12px;">
                <h5 class="modal-title popupheading text-success"><i class="fa fa-file" aria-hidden="true"></i>&nbsp;Benificiery Detail</h5>
            </div>
            <div class="modal-body" style="padding-left: 20px!important;" id="BenBodyContent"></div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" style="padding: 0.5rem 1rem!important;" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

<button type="button" class="btn btn-info btn-lg hidden delbendetail" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#DeleteBenDetail"></button>
<div class="modal fade" id="DeleteBenDetail" role="dialog">
    <div class="modal-dialog modal-md" style="margin: 10% auto!important;">
        <div id="ConfirmBenDelete" class="modal-content" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding: 30px!important;">
                <h6>Are you sure want to delete this benificiary detail?</h6>
            </div>
            <div class="modal-footer">
                <button type="button" id="btnDelConfirmed" class="btn btn-sm btn-success" style="padding: 0.5rem 1rem!important;" onclick="ConfirmDeleteDen();">YES</button>
                <button type="button" class="btn btn-sm btn-danger" style="padding: 0.5rem 1rem!important;" data-dismiss="modal">NO</button>
            </div>
        </div>
        <div id="BenDeleteOtpSent" class="modal-content hidden" style="text-align: center; border-radius: 5px !important;">
            <div class="container">
                <div class="login-signup-page mx-auto">
                    <h3 class="font-weight-400 text-center text-success">
                        <button type="button" id="BenDelCloseClick" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </h3>
                    <p class="lead text-center text-success">Otp sent successfully to your mobile number.</p>
                    <div class="bg-light shadow-md rounded p-4 mx-2">
                        <div class="form-group form-validation">
                            <label style="float: left; font-size: 1rem;">Enter OTP Number</label>
                            <input type="text" id="txtBenDeleteOtp" class="form-control" placeholder="Enter OTP Number." onkeypress="return isNumberValidationPrevent(event);" />
                        </div>
                        <span id="btnBenDelVarification" style="cursor: pointer;" class="btn btn-primary btn-block my-4" onclick="javascript: return BenDelVarification();">Verify</span>
                        <p id="bendelvariyerror" class="text-danger"></p>
                    </div>
                </div>
            </div>
        </div>
        <div id="DeleteSuccessfully" class="modal-content hidden" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding: 30px!important;">
                <h6 id="bendeletemsg"></h6>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-danger" style="padding: 0.5rem 1rem!important;" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

<button type="button" class="btn btn-info btn-lg hidden updateaddresspopup" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#UpdateAddressPopup"></button>
<div class="modal fade" id="UpdateAddressPopup" role="dialog">
    <div class="modal-dialog modal-md" style="margin: 5% auto!important;">
        <div class="modal-content" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding-left: 20px!important;">
                <div class="container">
                    <div class="login-signup-page mx-auto">
                        <h4 class="font-weight-400 text-center">Update Current / Local Address
                                <button type="button" id="UpdateAddressClose" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                        </h4>
                        <div class="bg-light shadow-md rounded p-4 mx-2">
                            <div class="form-group form-validation">
                                <textarea id="txtUpdateCurrLocalAddress" class="form-control" placeholder="Current / Local Address"></textarea>
                            </div>
                            <span id="btnUpdateCurrLocalAddress" style="cursor: pointer;" class="btn btn-primary btn-block my-4" onclick="return UpdateCurrLocalAddress();">Update Address</span>
                            <p id="updatemsg"></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<button type="button" class="btn btn-primary hidden VerifyTransferDetails" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#VerifyTransferDetails">rtuy</button>
<div class="modal fade" id="VerifyTransferDetails">
    <div class="modal-dialog modal-lg" id="VerifyTransferModalDialog" style="margin: 6% auto!important;">
        <div class="modal-content" id="VerifyTransSummaryDetail">
            <div class="modal-header">
                <h5 class="modal-title" id="transveryheading">Transaction Summary</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ff414d;">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body" id="FundTransDetailBody"></div>
            <div class="modal-footer" id="FundTransDetailFooter"></div>
            <div class="col-sm-12 text-center">
                <p id="paratranserror"></p>
            </div>
        </div>
    </div>
</div>


<button type="button" class="btn btn-info btn-lg hidden transferpayoutdirect" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#FormPayoutDirect"></button>
<div class="modal fade" id="FormPayoutDirect" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content" style="text-align: center; border-radius: 5px !important;">
            <div class="modal-body" style="padding-left: 20px!important;">
                <div class="container">                    
                    <div class="mx-auto">
                        <h3 class="font-weight-400 text-center">Payout Direct - Fund Transfer
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                        </h3>
                        <div class="bg-light shadow-md rounded p-4 mx-2" id="PayoutTransferSection">
                            <div class="form-group">
                                <select id="ddlPayoutSp_Key" class="form-control">                                    
                                    <option value="DPN">IMPS</option>
                                    <option value="BPN">NEFT</option>
                                    <option value="CPN">RTGS</option>                                   
                                </select>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6 form-validation">
                                        <input id="txtPayoutAccountNumber" type="text" class="form-control" placeholder="Enter Account Number" onkeypress="return isNumberValidationPrevent(event);"  />
                                    </div>
                                    <div class="col-sm-6 form-validation">
                                        <input id="txtPayoutIfscCode" type="text" class="form-control" placeholder="Enter IFSC Code" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6 form-validation text-right">
                                        <input id="txtPayoutBeneficiary" type="text" class="form-control" placeholder="Enter Beneficiary Name" />
                                        <span id="CheckPayoutBenName" class="text-primary btn btn-sm" style="cursor: pointer; padding: 0px!important;" onclick="return GetPayoutBeneficiaryNamePayout();">Get Beneficiary Name</span>
                                    </div>
                                    <div class="col-sm-6 form-validation">
                                        <input id="txtPayoutTransferAmount" type="text" class="form-control" placeholder="Enter Transfer Amount" onkeypress="return isNumberValidationPrevent(event);"  />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6 form-validation">
                                        <input id="txtPayoutAlertMobile" type="text" class="form-control" placeholder="Enter Alert Mobile Number" />
                                        <i class="fa fa-question-circle" aria-hidden="true" style="position: absolute; right: 0px; bottom: 20px; color: #ff414d;" data-placement="left" data-toggle="tooltip" title="In this 10 digit Mobile Number on which alert would be sent when beneficiary receives the funds.Multiple numbers can be sent for alert by using comma separated list. 95xxxxxxx,95xxxxxxx... etc."></i>
                                    </div>
                                    <div class="col-sm-6 form-validation">
                                        <input id="txtPayoutAlertEmail" type="text" class="form-control" placeholder="Enter Alert Email Id" />
                                        <i class="fa fa-question-circle" aria-hidden="true" style="position: absolute; right: 0px; bottom: 20px; color: #ff414d;" data-placement="left" data-toggle="tooltip" title="Email ID on which alert would be sent when beneficiary receives the funds. Multiple emails can be sent for alert by using comma separated list. xxxxx@gmail.com,xxxxx@gmail.com etc."></i>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group form-validation">
                                <textarea id="txtPayoutRemark" class="form-control" placeholder="Remark"></textarea>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <p id="payoutbenerrormessage" class="text-danger"></p>
                                    </div>
                                    <div class="col-sm-6"><span id="btnPayoutTransferFund" style="cursor: pointer;" class="btn btn-primary btn-block my-4" onclick="return PayoutFundTransfer();">Transfer</span></div>
                                </div>                                
                            </div>
                        </div>
                        <div class="bg-light shadow-md rounded p-4 mx-2 hidden" id="PayoutSuccessTransferSection"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .tooltip-inner {
        max-width: 350px !important;
    }
</style>
<script>
    $('[data-toggle="tooltip"]').tooltip();
</script>
