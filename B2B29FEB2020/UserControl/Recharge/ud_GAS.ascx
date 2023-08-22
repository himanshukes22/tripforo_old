<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_GAS.ascx.cs" Inherits="DMT_Manager_User_Control_ud_GAS" %>


<br />
<div class="row">    
    <div class="col-md-3" id="BindGasSection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
       <select id="ddlGasProvider" class="form-control"></select>

    </div>
        </div>
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnGasSumit" onclick="GasBillPaySubmit();">Get Bill</span>
    </div>
</div>

<br />
<div class="row hidden" id="gasnotesmsg"></div>

<button type="button" class="btn btn-info btn-lg hidden gasmodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#GasModelSection"></button>
<div class="modal fade" id="GasModelSection" role="dialog"><div class="modal-dialog" style="margin: 7% auto!important;"><div class="modal-content" id="modelgasheadbody"></div></div></div>