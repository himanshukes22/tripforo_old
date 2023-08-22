<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HotelDashboard.ascx.cs" Inherits="UserControl_HotelDashboard" %>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/Dashboard_Flight.js") %>"></script>

<div class="col-xs-12 leftp leftr">
    <!-- required for floating -->
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" style="margin-top: 0px;">
        <li id="HR" class="active ticket"><a href="#H_homess" data-toggle="tab" aria-expanded="true">Todays Bookings<div class="text-center">(0)</div>
        </a></li>
        <li id="HU" class="ticket"><a href="#H_profiless" data-toggle="tab" aria-expanded="false">Upcoming Trips
            <div class="text-center">(0)</div>
        </a></li>
        <li id="HP" class="ticket"><a href="#H_messagesss" data-toggle="tab" aria-expanded="false">Past Trips
            <div class="text-center">(0)</div>
        </a></li>
        <li id="H_Hold" class="ticket"><a href="#H_HoldPnr" data-toggle="tab" aria-expanded="false">Hold PNR<div class="text-center">(0)</div>
        </a></li>
        <li id="H_Refund" class="ticket"><a href="#H_RefundTicket" data-toggle="tab" aria-expanded="false">Cancel<div class="text-center">(0)</div>
        </a></li>
    </ul>
</div>
<div class="col-xs-12">
    <!-- Tab panes -->
    <div class="tab-content" style="background-color: #fff; padding-top: 5px;">
        <div class="tab-pane active" id="H_homess">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                <thead>
                    <tr id="7">
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <%--<th class="text-center" style="display:none;">Action(s)</th>--%>
                    </tr>
                </thead>
                <tbody id="Data_Bind_HR">
                    <tr>
                        <%--<td>
                            <a href="/Report/TicketReport.aspx">For More</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="H_profiless">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                <thead>
                    <tr id="8">
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <%--<th class="text-center">Action(s)</th>--%>
                    </tr>
                </thead>
                <tbody id="Data_Bind_HU">
                    <tr>
                        <%-- <td>
                            <a href="/Report/UpcomingPastTrip.aspx?ReqType=U">For More</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="H_messagesss">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                <thead>
                    <tr id="9">
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <th id="htlpast" class="text-center" style="display:none;">Action(s)</th>
                    </tr>
                </thead>
                <tbody id="Data_Bind_HP">
                    <tr>
                        <%--                        <td>
                            <a href="/Report/UpcomingPastTrip.aspx?ReqType=P">For More</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="H_HoldPnr">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                <thead>
                    <tr id="10">
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <th id="htlholdaction" class="text-center" style="display:none;">Action(s)</th>
                    </tr>
                </thead>
                <tbody id="Data_Bind_H_Hold">
                    <tr>
                        <%--   <td>
                            <a href="/Report/HoldPNR/HoldPNRReport.aspx">For More</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="H_RefundTicket">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; margin-bottom: 5px; cursor: pointer;">
                <thead>
                    <tr id="11">
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <th id="htlrefundaction" class="text-center" style="display:none;">Action(s)</th>
                    </tr>
                </thead>
                <tbody id="Data_Bind_H_RefundTicket">
                    <tr>
                        <%-- <td>
                            <a href="/Report/Refund/CancellationReport.aspx">For More</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
        <%--<div class="tab-pane" id="H_ReissueTicket">
            <table class="table table-hover abc" style="background-color: #fff; border: 1px solid #eeeeee; cursor: pointer;">
                <thead>
                    <tr>
                        <th class="text-center">OrderId </th>
                        <th>Name</th>
                        <th>Hotel Name</th>
                        <th>CheckIN On</th>
                        <th class="text-center">Action(s)</th>
                    </tr>
                </thead>
                <tbody id="Data_Bind_H_Reissue">
                    <tr>
                        <td>
                            <a href="/Report/Reissue/ReissueReport.aspx">For More</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>--%>
    </div>
</div>
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div style="width: 802px; margin: 28px auto;" class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">Hotel Booking Copy</h4>
            </div>
            <div class="modal-body">
                <iframe id="ifrmHTL" style="overflow-y: scroll;" width="800" height="500"></iframe>
            </div>
        </div>
    </div>
</div>
<div style="clear: both;"></div>

