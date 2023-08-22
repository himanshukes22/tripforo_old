<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_landline.ascx.cs" Inherits="DMT_Manager_User_Control_ud_landline" %>


<br />
<div class="row">
    <div class="col-md-3" id="BindLandlineSection">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
            <select id="ddlLandlineOperator" class="form-control"></select>
        </div>
    </div>
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnLandline" onclick="FeatchLandlineBill();">Get Bill</span>
    </div>
</div>

<br />
<div class="row hidden" id="landlinenotesmsg"></div>


<button type="button" class="btn btn-info btn-lg hidden landlinemodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#LandlineModelSection"></button>
<div class="modal fade" id="LandlineModelSection" role="dialog">
    <div class="modal-dialog" style="margin: 7% auto!important;">
        <div class="modal-content" id="modellandlineheadbody"></div>
    </div>
</div>
