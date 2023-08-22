<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_DTH_Recharge.ascx.cs" Inherits="DMT_Manager_User_Control_ud_DTH_Recharge" %>


<br />

<div class="row">
    <div class="col-md-2">
        <div class="theme-payment-page-form-item form-group  form-validation">
            <i class="fa fa-angle-down"></i>
            <select id="ddlDTHOprator" class="form-control"></select>
        </div>
    </div>
    <div class="col-md-2  hidden dthsecation">
        <div class="theme-payment-page-form-item form-group form-validation">
            <input type="text" class="form-control" id="txtDTHNumber" placeholder="DTH Number" minlength="6" maxlength="18" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
        </div>
    </div>
    <div class="col-md-2  hidden dthsecation">
        <div class="theme-payment-page-form-item form-group form-validation">
            <input type="text" class="form-control" id="txtDTHAmount" placeholder="Enter Amount" autocomplete="off" onkeypress="return isNumberValidationPrevent(event);" />
        </div>
    </div>
    <div class="col-md-2">
        <div class="theme-payment-page-form-item form-group">
            <span class="btn btn-primary btn-block" id="btnDTHRecharge" onclick="DTHRechargeEvent();">Recharge</span>
        </div>
    </div>
</div>

<br />
<div class="row hidden" id="dthnotesmsg"></div>

<button type="button" class="btn btn-info btn-lg hidden dthmodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#DTHModelSection"></button>
<div class="modal fade" id="DTHModelSection" role="dialog">
    <div class="modal-dialog" style="margin: 7% auto!important;">
        <div class="modal-content" id="modeldthheadbody"></div>
    </div>
</div>
