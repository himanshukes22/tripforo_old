<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_Electricity_Recharge.ascx.cs" Inherits="DMT_Manager_User_Control_ud_Electricity_Recharge" %>



<br />
<div class="row">
    <div class="col-md-3" id="BindElectricitySection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
            <select id="ddlElectricityBoard" class="form-control"></select>

        </div>
    </div>
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnElectricity" onclick="return FeatchElectricityBill();">Get Bill</span>
    </div>
</div>

<br />
<div class="row hidden" id="electricitynotesmsg"></div>


<button type="button" class="btn btn-info btn-lg hidden electricitymodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#ElectricityModelSection"></button>
<div class="modal fade" id="ElectricityModelSection" role="dialog">
    <div class="modal-dialog modal-lg" style="margin: 7% auto!important;">
        <div class="modal-content" id="modelelectricityheadbody"></div>
    </div>
</div>
