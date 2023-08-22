<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftMenu.ascx.vb" Inherits="UserControl_LeftMenu" %>
<% Dim obj As New FltSearch1()%>
<% Dim um As String%>
<% Dim rawurlLM As String%>
<div class="w100">
    
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/DomHoldPNRReport.aspx")%>
            <% rawurlLM = ""%>
            <% rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Hold Pnr Report</a>--%>
            <%--<a  href="<%= ResolveUrl("~/Report/HoldPNR/DomHoldPNRReport.aspx")%>">Dom. Hold Pnr Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/HoldPNR/HoldPNRReport.aspx")%>">Hold Pnr Report</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Refund/CancellationReportDom.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM  %>">Dom. Refund Ticket Report</a>--%>
            <%--<a href="<%= ResolveUrl("~/Report/Refund/CancellationReportDom.aspx")%>">Dom. Refund Ticket Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/Refund/CancellationReport.aspx")%>">Refund Report</a>
        </div>
    <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/ReIssueReportDom.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Reissue Ticket Report</a>--%>
            <%--<a href="<%= ResolveUrl("~/Report/Reissue/ReIssueReportDom.aspx")%>">Dom. Reissue Ticket Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/Reissue/ReissueReport.aspx")%>">Reissue Report</a>
        </div>
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/ReIssueReportDom.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Reissue Ticket Report</a>--%>
            <%--<a href="<%= ResolveUrl("~/Report/Reissue/ReIssueReportDom.aspx")%>">Dom. Reissue Ticket Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/RejectedTicket.aspx")%>">Rejected Report</a>
        </div>
     <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/DomHoldPNRReport.aspx")%>
            <% rawurlLM = ""%>
            <% rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Hold Pnr Report</a>--%>
            <%--<a  href="<%= ResolveUrl("~/Report/HoldPNR/DomHoldPNRReport.aspx")%>">Dom. Hold Pnr Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/HoldPNR/HoldPNRReport.aspx")%>">Hold Pnr Report</a>
        </div>
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/TicketReportDom.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Ticket Report</a>--%>
            <%--<a href="<%= ResolveUrl("~/Report/TicketReportDom.aspx")%>">Dom. Ticket Report</a>--%>
            <a href="<%= ResolveUrl("~/Report/TicketReport.aspx")%>">Ticket Report</a>
        </div>
    

    <% If Session("User_Type").ToString().Trim().ToUpper().Equals("EXEC") And Not Session("TypeID").ToString().Trim().ToUpper().Equals("ECR") Then%>
    <% If Session("TripExec").ToString().Trim().ToUpper().Equals("D") Then%>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/DomHoldPNRRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Hold PNR Request</a>--%>
            <a href="<%= ResolveUrl("~/Report/HoldPNR/DomHoldPNRRequest.aspx") %>">Dom. Hold PNR Request</a>
        </div>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/DomHoldPNRUpdate.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Hold PNR Update</a>--%>
            <a href="<%=ResolveUrl("~/Report/HoldPNR/DomHoldPNRUpdate.aspx")%>">Dom. Hold PNR Update</a>
        </div>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Refund/TktRptDom_RefundRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Refund Request</a>--%>
            <a href="<%=ResolveUrl("~/Report/Refund/TktRptDom_RefundRequest.aspx")%>">Dom. Refund Request</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Refund/TktRptDom_RefundInProcess.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Refund In Process</a>--%>
            <a href="<%=ResolveUrl("~/Report/Refund/TktRptDom_RefundInProcess.aspx")%>">Dom. Refund In Process</a>
        </div>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/TktRptDom_ReIssueRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Reissue Request</a>--%>
            <a href="<%=ResolveUrl("~/Report/Reissue/TktRptDom_ReIssueRequest.aspx")%>">Dom. Reissue Request</a>
        </div>
  
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/TktRptDom_ReIssueInProcess.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Dom. Reissue In Process</a>--%>
            <a href="<%=ResolveUrl("~/Report/Reissue/TktRptDom_ReIssueInProcess.aspx")%>">Dom. Reissue In Process</a>
        </div>
    

    <% ElseIf Session("TripExec").ToString().Trim().ToUpper().Equals("I") Then%>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/TktRptIntl_ReIssueInProcess.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Reissue In Process</a>--%>
            <a href="<%=ResolveUrl("~/Report/Reissue/TktRptIntl_ReIssueInProcess.aspx")%>">Intl. Reissue In Process</a>
        </div>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/IntlHoldPNRRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Hold PNR Request</a>--%>
            <a href="<%=ResolveUrl("~/Report/HoldPNR/IntlHoldPNRRequest.aspx")%>">Intl. Hold PNR Request</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/HoldPNR/IntlHoldPNRUpdate.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Hold PNR Update</a>--%>
            <a href="<%=ResolveUrl("~/Report/HoldPNR/IntlHoldPNRUpdate.aspx")%>">Intl. Hold PNR Update</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Refund/TktRptIntl_RefundRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Refund Request</a>--%>
            <a href="<%=ResolveUrl("~/Report/Refund/TktRptIntl_RefundRequest.aspx")%>">Intl. Refund Request</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Refund/TktRptIntl_RefundInProcess.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Refund In Process</a>--%>
            <a href="<%=ResolveUrl("~/Report/Refund/TktRptIntl_RefundInProcess.aspx")%>">Intl. Refund In Process</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Reissue/TktRptIntl_ReIssueRequest.aspx")%>
            <% rawurlLM = ""%>
            <%rawurlLM = Request.RawUrl%>
            <%rawurlLM = "/" & rawurlLM.Replace(rawurlLM, um)%>
            <a href="<%= rawurlLM %>">Intl. Reissue Request</a>--%>
            <a href="<%=ResolveUrl("~/Report/Reissue/TktRptIntl_ReIssueRequest.aspx")%>">Intl. Reissue Request</a>
        </div>
   
    <% End If%>
    <% End If%>
    <% If Session("User_Type").ToString().Trim().ToUpper().Equals("ADMIN") Then%>
 
        <div class="fltnewmenu1">
            <a href="<%=ResolveUrl("~/Report/ConfigureMails.aspx")%>">Configure Mails</a>
        </div>
    
    <% End If%>
</div>
