<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusDashBoard.ascx.cs" Inherits="UserControl_BusDashBoard" %>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/Dashboard_Flight.js?v=0") %>"></script>
 
    <!-- Nav tabs -->
     
    <!-- Tab panes -->
    
              
            <div class="col-xs-12 leftp leftr">
                <!-- required for floating -->
                <!-- Nav tabs -->
                <ul class="nav nav-tabs " style="margin-top: 0px;">
                    <li id="BR" class="active ticket"><a href="#bhomess" data-toggle="tab" aria-expanded="true">Todays Bookings
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="BU" class="ticket"><a href="#bprofiless" data-toggle="tab" aria-expanded="false">Upcoming Trips
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="BP" class="ticket"><a href="#bmessagesss" data-toggle="tab" aria-expanded="false">Past Trips
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="B_Hold" class="ticket"><a href="#bHoldPnr" data-toggle="tab" aria-expanded="false">Hold PNR
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="B_Refund" class="ticket"><a href="#bRefundTicket" data-toggle="tab" aria-expanded="false">Cancel
                        <div class="text-center">(0)</div>
                    </a></li>
                </ul>
            </div>
            <div class="col-xs-12">
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-pane active" id="bhomess">
                        <table class="table table-hover abc" style="background-color: #fff; margin-top: 5px; border: 1px solid #eeeeee;     margin-bottom: 5px; cursor: pointer;">
                            <thead>
                                <tr id="12">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                   <%--<th style="text-align:center!important" >Refund</th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_BR">
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="bprofiless">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                            <thead>
                                <tr id="13">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                   <%-- <th style="text-align:center!important" >Refund</th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_BU">
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="bmessagesss">
                        <table class="table table-hover abc" style="background-color: #fff; margin-bottom:5px; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="14">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                      
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_BP">
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="bHoldPnr">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="15">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                     <%--<th style="text-align:center!important" >Refund</th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_B_Hold">
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="bRefundTicket">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="16">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                     <%--<th style="text-align:center!important" >Refund</th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_B_RefundTicket">
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
    
                <div class="modal-content" style="display:none;" >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel"></h4>
                    </div>
                    <div class="modal-body" id="hhh">
                        <div class="holds-the-iframe">
                            <iframe id="IfrmTKT" width="800" height="500" style="overflow-y: scroll; background: #ffffff url(http://mentalized.net/activity-indicators/indicators/simon-claret/progress_bar.gif) no-repeat 50% 5%;"></iframe>
                        </div>
                    </div>
                </div>
            
    

<style type="text/css">
    .holds-the-iframe {
        background: url(../images/loader.gif) center center no-repeat;
    }
</style>
