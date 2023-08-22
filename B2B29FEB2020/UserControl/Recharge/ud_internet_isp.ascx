<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ud_internet_isp.ascx.cs" Inherits="DMT_Manager_User_Control_ud_internet_isp" %>



<br />

<div class="row">
    <div class="col-md-3" id="BindInternetSec">
        <div class="theme-payment-page-form-item form-group form-validation">
            <i class="fa fa-angle-down"></i>
        <select id="ddlInternetService" class="form-control"></select>
            </div>
    </div>   
    <div class="col-md-2">
        <span class="btn btn-primary btn-block" id="btnBroadband" onclick="BroadbandBillPaySubmit();">Get Bill</span>
    </div>
</div>

<br />
<div class="row hidden" id="internetnotesmsg"></div>

<button type="button" class="btn btn-info btn-lg hidden internetmodelclickclass" data-backdrop="static" data-keyboard="false" data-toggle="modal" data-target="#InternetModelSection">OPEN</button>
<div class="modal fade" id="InternetModelSection" role="dialog"><div class="modal-dialog" style="margin: 7% auto!important;"><div class="modal-content" id="modelinternetheadbody"></div></div></div>