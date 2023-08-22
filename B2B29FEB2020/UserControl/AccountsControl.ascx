<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccountsControl.ascx.vb" Inherits="UserControl_AccountsControl" %>
<% Dim obj As New FltSearch1()%>
<% Dim um As String%>
<% Dim rawurlA As String%>
<div>

    <div class="fltnewmenu1">
        <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Accounts/DomSaleRegister.aspx")%>
            <% rawurlA = ""%>
            <% rawurlA = Request.RawUrl%>
            <%rawurlA = "/" & rawurlA.Replace(rawurlA, um)%>
            <a href="<%= rawurlA %>">Dom. Sale Register</a>--%>
        <a href="<%= ResolveUrl("~/Report/Accounts/DomSaleRegister.aspx")%>">Dom. Sale Register</a>
    </div>

    <div class="fltnewmenu1">
        <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Accounts/IntlSaleRegister.aspx")%>
            <% rawurlA = ""%>
            <% rawurlA = Request.RawUrl%>
            <%rawurlA = "/" & rawurlA.Replace(rawurlA, um)%>
            <a href="<%= rawurlA %>">Intl. Sale Register</a>--%>
        <a href="<%= ResolveUrl("~/Report/Accounts/IntlSaleRegister.aspx")%>">Intl. Sale Register</a>
    </div>

    <div class="fltnewmenu1">
        <a href="<%=ResolveUrl("~/Report/Admin/PGReports.aspx")%>">PG Report</a>
    </div>

     <% If Session("User_Type").ToString().Trim().ToUpper().Equals("ADMIN") Then%>
 
        <div class="fltnewmenu1">
        <a href="<%=ResolveUrl("~/Report/Admin/PGCharges.aspx")%>">PG Charges</a>
    </div>
    
    <% End If%>

    

</div>
