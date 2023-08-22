<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="UserControl_DashBoard" %>
<%@ Register Src="~/UserControl/HotelDashboard.ascx" TagPrefix="uc1" TagName="HotelDashboard" %>
<%@ Register Src="~/UserControl/BusDashBoard.ascx" TagPrefix="uc1" TagName="BusDashBoard" %>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/Dashboard_Flight.js") %>"></script>
<div style="margin-top: 10px;">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#homes" aria-controls="homes" role="tab" data-toggle="tab"><i class="fa fa-plane" aria-hidden="true"></i> Flight</a></li>
        <li role="presentation" onclick="return TripType('HR');"><a href="#profiles" aria-controls="profiles" role="tab" data-toggle="tab"><i class="fa fa-bed" aria-hidden="true"></i> Hotel</a></li>
        <li role="presentation" onclick="return TripType('BR');"><a href="#busss" aria-controls="busss" role="tab" data-toggle="tab"><i class="fa fa-bus" aria-hidden="true"></i> Bus</a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content" style="background-color: #fff; padding-top: 5px;">
        <div role="tabpanel" class="tab-pane active" id="homes">
            <div class="col-xs-12 leftp leftr">
                <!-- required for floating -->
                <!-- Nav tabs -->
                <ul class="nav nav-tabs " style="margin-top: 0px;">
                    <li id="R" class="active ticket"><a href="#homess" data-toggle="tab" aria-expanded="true">Todays Bookings
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="U" class="ticket"><a href="#profiless" data-toggle="tab" aria-expanded="false">Upcoming Trips
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="P" class="ticket"><a href="#messagesss" data-toggle="tab" aria-expanded="false">Past Trips
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="Hold" class="ticket"><a href="#HoldPnr" data-toggle="tab" aria-expanded="false">Hold PNR
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="Refund" class="ticket"><a href="#RefundTicket" data-toggle="tab" aria-expanded="false">Cancel
                        <div class="text-center">(0)</div>
                    </a></li>
                    <li id="Reissue" class="ticket"><a href="#ReissueTicket" data-toggle="tab" aria-expanded="false">Reissue
                        <div class="text-center">(0)</div>
                    </a></li>
                </ul>
            </div>
            <div class="col-xs-12">
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-pane active" id="homess">
                        <table class="table table-hover abc" style="background-color: #fff; margin-top: 5px; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                            <thead>
                                <tr id="1">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                    <%--<th style="text-align:center!important;" >Refund  and  Reissue </th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_R">
                                <tr>
                                    <%-- <td>
                                        <a href="/Report/TicketReport.aspx">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="profiless">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                            <thead>
                                <tr id="2">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                    <%--<th style="text-align:center!important;" >Refund  and  Reissue </th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_U">
                                <tr>
                                    <%-- <td>
                                        <a href="/Report/TicketReport.aspx">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="messagesss">
                        <table class="table table-hover abc" style="background-color: #fff; margin-bottom: 5px; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="3">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>

                                </tr>
                            </thead>
                            <tbody id="Data_Bind_P">
                                <tr>
                                    <%--<td>
                                        <a href="/Report/UpcomingPastTrip.aspx?ReqType=P">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="HoldPnr">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="4">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                   <%--  <th style="text-align:center!important;" >Refund  and  Reissue </th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_Hold">
                                <tr>
                                    <%--<td>
                                        <a href="/Report/HoldPNR/HoldPNRReport.aspx">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="RefundTicket">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="5">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                     <%--<th style="text-align:center!important;" >Refund  and  Reissue </th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_RefundTicket">
                                <tr>
                                    <%--<td>
                                        <a href="/Report/Refund/CancellationReport.aspx">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="ReissueTicket">
                        <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                            <thead>
                                <tr id="6">
                                    <th class="text-center">Order Id </th>
                                    <th>Passenger Name</th>
                                    <th>Sector </th>
                                    <th>Trip Date</th>
                                     <%--<th style="text-align:center!important;" >Refund  and  Reissue </th>--%>
                                </tr>
                            </thead>
                            <tbody id="Data_Bind_Reissue">
                                <tr>
                                    <%--<td>
                                        <a href="/Report/Reissue/ReissueReport.aspx">For More</a>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div style="width: 802px; margin: 28px auto;" class="modal-dialog" role="document">
                <div class="modal-content">
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
            </div>
        </div>
        <div role="tabpanel" class="tab-pane" id="profiles">
            <uc1:HotelDashboard runat="server" ID="HotelDashboard" />
        </div>
         <div role="tabpanel" class="tab-pane" id="busss">
             
             <uc1:BusDashBoard runat="server" ID="BusDashBoard" />
        </div>
    </div>
</div>
<style type="text/css">
    .holds-the-iframe {
        background: url(../images/loader.gif) center center no-repeat;
    }
</style>
