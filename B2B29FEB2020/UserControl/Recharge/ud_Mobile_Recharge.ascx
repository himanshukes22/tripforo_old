<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_Mobile_Recharge.ascx.cs" Inherits="DMT_Manager_User_Control_ud_Mobile_Recharge" %>

<style>
    .text-warning {
        color: #ff9900;
    }
</style>


<br />
<div class="row">
    <div class="col-sm-6">
        <div class="theme-search-area-options theme-search-area-options-white theme-search-area-options-dot-primary-inverse clearfix" id="Div1" style="">
            <span class="custom-control custom-radio custom-control-inline">
                <label class="mail active" for="rdoPrepaid" style="color:#fff;">
                    <input type="radio" id="rdoPrepaid" name="mobiletype" class="custom-control-input" value="Prepaid" checked="" />
                    Prepaid</label>
            </span>
            <span class="custom-control custom-radio custom-control-inline">
                <label class="mail" for="rdoPostpaid" style="color:#fff;">
                    <input type="radio" id="rdoPostpaid" name="mobiletype" class="custom-control-input" value="Postpaid" />
                    Postpaid</label>
            </span>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <div class="theme-payment-page-form-item form-group form-validation">
            <input type="text" class="form-control" id="txtMobileNumber" placeholder="Mobile Number" maxlength="10" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
        </div>
    </div>
    <div class="col-md-2">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
            <select id="ddlMobileOprator" class="form-control"></select>
        </div>
    </div>
    <div class="col-md-2" id="MobileCircleSection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
            <select id="ddlMobileCircle" class="form-control">
                <option value="0">--Change Circle--</option>
                <option value="1">Andhra Pradesh & Telangana</option>
                <option value="2">Assam</option>
                <option value="3">Bihar & Jharkand</option>
                <option value="4">Chennai</option>
                <option value="5">Delhi NCR</option>
                <option value="6">Gujarat & Damam Diu</option>
                <option value="8">Harayana</option>
                <option value="7">Himachal Pradesh</option>
                <option value="9">Jammu & Kashmir</option>
                <option value="11">Karnataka</option>
                <option value="10">Kerela & Laskhadweep</option>
                <option value="12">Kolkata</option>
                <option value="14">Madhya Pradhesh & Chhattisgarh</option>
                <option value="13">Maharashtra & Goa</option>
                <option value="15">Mumbai</option>
                <option value="16">North East</option>
                <option value="17">Odisha</option>
                <option value="18">Punjab</option>
                <option value="19">Rajasthan</option>
                <option value="20">Tamil Nadu</option>
                <option value="21">Uttar Pradesh East</option>
                <option value="22">Uttar Pradesh West & Uttrakhand</option>
                <option value="23">West Bengal & AN Islands & Sikkim</option>
            </select>
        </div>
    </div>
    <div class="col-md-2" id="MobileAmountSection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <input type="text" class="form-control" id="txtMobileAmount" placeholder="Amount" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
        </div>
    </div>
    <div class="col-md-2">
        <span class="btn btn-primary" id="btnMobileRecharge" onclick="RechargeEvent();">Recharge</span>
    </div>
</div>

<br />
<div class="row hidden" id="mobilenotesmsg"></div>
<br />
<br />


<button type="button" class="btn btn-info btn-lg hidden mobilemodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#MobileModelSection"></button>
<div class="modal fade" id="MobileModelSection" role="dialog">
    <div class="modal-dialog modal-lg" style="margin: 7% auto!important;">
        <div class="modal-content" id="modelmobileheadbody"></div>
    </div>
</div>


<%--<button type="button" class="btn btn-info btn-lg demo" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#Demo">OPEN</button>
<div class="modal fade" id="Demo" role="dialog">
    <div class="modal-dialog" style="margin: 10% auto!important;">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title text-danger"><i class='fa fa-exclamation-triangle'></i>&nbsp;Failed</h5>
                <button type="button" class="close text-danger" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-danger text-center" style="padding: 40px!important;">
                <h4>Error occurred, Please try again after sometime.</h4>
            </div>            
        </div>
    </div>
</div>--%>