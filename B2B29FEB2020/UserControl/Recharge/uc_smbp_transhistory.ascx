<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_smbp_transhistory.ascx.cs" Inherits="DMT_Manager_User_Control_uc_smbp_transhistory" %>
<link href="custom/css/validationcss.css" rel="stylesheet" />
<link href="custom/css/datetime.css" rel="stylesheet" />
<style>
    .scrolltable {       
        max-height: 600px;
    }

    .checkstatus {
        padding: 3px;
    color: #ff414d;
    box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.15);
    border-radius: 4px;
    border: 1px solid #ff414d;
    }

    .table thead th {
        position: sticky;
        top: 0;
        padding: 10px;
        vertical-align: bottom;
        border-bottom: 1px solid #000000;
        background: #f1f5f6;
    }
</style>

<div class="row col-sm-12" style="margin-bottom: 10px;">
    <div class="row">
        <div class="col-sm-2">
            <label style="margin-top: 5px;">From Date</label>
            <div class="theme-payment-page-form-item form-group">

                <input type="text" class="form-control commonDate" placeholder="dd/mm/yyyy" id="txtBillFromDate" name="txtBillFromDate" style="padding: 4px;" size="10">
            </div>
        </div>
        <div class="col-sm-2">
            <label style="margin-top: 5px;">To Date</label>
            <div class="theme-payment-page-form-item form-group">
                <input type="text" class="form-control commonDate" placeholder="dd/mm/yyyy" id="txtBillToDate" name="txtBillToDate" style="padding: 4px;" size="10">
            </div>
        </div>
        <div class="col-sm-2">
            <label style="margin-top: 5px;">Client Ref. Id</label>
            <div class="theme-payment-page-form-item form-group">
                <input type="text" class="form-control" placeholder="Client Ref. ID" id="txtClientRefID" name="txtClientRefID" style="padding: 4px;" size="10">
            </div>
        </div>
        <div class="col-sm-2">
            <label style="margin-top: 5px;">Transaction Type</label>
            <div class="theme-payment-page-form-item form-group">
                <i class="fa fa-angle-down"></i>
                <select id="ddlBillTransType" class="form-control">
                    <option value="">All</option>
                    <option value="mobile">Mobile</option>
                    <option value="dth">DTH</option>
                    <option value="electricity">Electricity</option>
                    <option value="landline">Landline</option>
                    <option value="insurance">Insurance</option>
                    <option value="gas">Gas</option>
                    <option value="broadband">Broadband</option>
                    <option value="water">Water</option>
                </select>
            </div>
        </div>
        <div class="col-sm-2">
            <label style="margin-top: 5px;">Status</label>
            <div class="theme-payment-page-form-item form-group">
                <i class="fa fa-angle-down"></i>
                <select id="ddlBillTransStatus" class="form-control">
                    <option value="">All</option>
                    <option value="successful">Success</option>
                    <option value="under process">Under Process</option>
                    <option value="failed">Failed</option>
                </select>
            </div>
        </div>
        <div class="col-sm-2" style="margin-top: 30px;">
            <span id="btnBillSearchSubmit" style="cursor: pointer;" class="btn btn-sm btn-primary" onclick="BillSearchSubmit();">Search</span>
            <span id="btnBillSearchClear" style="cursor: pointer;" class="btn btn-sm btn-primary" onclick="BillSearchClear();">Clear</span>
        </div>
    </div>
</div>
<br />
<div class="scrolltable">
    <table class="table table-sm table-bordered">
        <thead>
            <tr>
                <th>Txn Date</th>
                <th>Order ID</th>
                <th>Client Ref. ID</th>
                <th>Number</th>
                <th>Amount</th>
                <th>Agent ID</th>
                <th>Service</th>
                <th>Status</th>
                <th></th>
                <th class="text-center">Refund</th>
                <th class="text-center">Recipt</th>
            </tr>
        </thead>
        <tbody id="BillTransRowDetails"></tbody>
    </table>
</div>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<script>
    $('.commonDate').datepicker({
        dateFormat: "dd/mm/yy",
        showStatus: true,
        showWeeks: true,
        currentText: 'Now',
        autoSize: true,
        maxDate: -0,
        gotoCurrent: true,
        showAnim: 'blind',
        highlightWeek: true
    });
</script>
