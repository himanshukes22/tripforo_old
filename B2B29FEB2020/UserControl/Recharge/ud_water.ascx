<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_water.ascx.cs" Inherits="DMT_Manager_User_Control_ud_water" %>




<br />
<div class="row">
    <div class="col-md-3 form-validation" id="BindWaterSection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
        <select id="ddlWaterBoard" class="form-control"></select>
            </div>
    </div>    
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnWaterProceed" onclick="WaterProceedSubmit();">Get Bill</span>
    </div>
</div>

<br />
<div class="row hidden" id="waternotesmsg"></div>

<button type="button" class="btn btn-info btn-lg hidden watermodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#WaterModelSection">OPEN</button>
<div class="modal fade" id="WaterModelSection" role="dialog">
    <div class="modal-dialog" style="margin: 7% auto!important;">
        <div class="modal-content" id="modelwaterheadbody"></div>
    </div>
</div>