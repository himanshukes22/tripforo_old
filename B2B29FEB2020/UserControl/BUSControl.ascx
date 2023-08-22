<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BUSControl.ascx.cs" Inherits="UserControl_BUSControl" %>
<div class="w100">       
    <div class="fltnewmenu1">
            <a href="<%=ResolveUrl("~/BS/BusReport.aspx")%>">BUS Ticket Report</a>
        </div>
    <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BUSRefundReport.aspx")%>">BUS Refund Report</a>
        </div>

    <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BUSRejectReport.aspx")%>">BUS Reject Report</a>
        </div>

   <%-- <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BUSReissueReport.aspx")%>">BUS Reissue Report</a>
        </div>--%>

        <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BUSHoldPnrReport.aspx")%>">BUS HoldPNR Report</a>
        </div>

</div>