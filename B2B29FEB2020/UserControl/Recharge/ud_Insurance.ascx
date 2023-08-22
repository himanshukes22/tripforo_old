<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_Insurance.ascx.cs" Inherits="DMT_Manager_User_Control_ud_Insurance" %>



<br />
<div class="row" id="InsuranceSection">
    <div class="col-md-3 form-validation" id="BindInsurance">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
        <select id="ddlInsurance" class="form-control"></select>
            </div>
    </div>     
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnInsurancePremium" onclick="InsurancePremiumSubmit();">Get Premium</span>
    </div>
</div>


<br />
<div class="row hidden" id="insurancenotesmsg"></div>

<button type="button" class="btn btn-info btn-lg hidden insurancesection" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#InsuranceModelSection">OPEN</button>
<div class="modal fade" id="InsuranceModelSection" role="dialog">
    <div class="modal-dialog" style="margin: 7% auto!important;">
        <div class="modal-content" id="modelinsuranceheadbody"></div>
    </div>
</div>