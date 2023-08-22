<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Settings.ascx.vb" Inherits="UserControl_Settings" %>
<% Dim obj As New FltSearch1()%>
<% Dim um As String%>
<% Dim rawurlS As String%>
<div>
    <%If Session("User_Type").ToString().Trim().ToUpper().Equals("ADMIN") Then%>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Admin/DomAirlineMarkup.aspx")%>
            <% rawurlS = ""%>
            <% rawurlS = Request.RawUrl%>
            <%rawurlS = "/" & rawurlS.Replace(rawurlS, um)%>
            <a href="<%= rawurlS%>">Dom. Airline Markup</a>--%>
            <a href="<%= ResolveUrl("~/Report/Admin/DomAirlineMarkup.aspx")%>">Dom. Airline Markup</a>
        </div>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Admin/IntlAirlineMarkup.aspx")%>
            <% rawurlS = ""%>
            <% rawurlS = Request.RawUrl%>
            <%rawurlS = "/" & rawurlS.Replace(rawurlS, um)%>
            <a href="<%= rawurlS%>">Intl. Airline Markup</a>--%>
            <a href="<%= ResolveUrl("~/Report/Admin/IntlAirlineMarkup.aspx")%>">Intl. Airline Markup</a>
        </div>
   
        <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/AirlineFee.aspx")%>">Airline Fee</a>
        </div>
    
        <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/DomDiscountMaster.aspx")%>">Dom. Discount Master</a>
        </div>
    
        <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/IntlDiscountMaster.aspx")%>">Intl. Discount Master</a>
        </div>
   
        <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/MISCSRVCHARGE.aspx")%>">Misc Markup Charges</a>
        </div>
     <div class="fltnewmenu1">
                     <a href="<%= ResolveUrl("~/Report/Admin/airproviderswitch.aspx")%>">Air Provider Switch</a>
        </div>
    <div class="fltnewmenu1">
                     <a href="<%= ResolveUrl("~/Report/Admin/Galtktswitch.aspx")%>">Gal Ticketing  Switch</a>
        </div>

    
      <%--  <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/ModuleAccess.aspx")%>">Module Access Authorisation</a>
        </div>--%>
     
    <%--<div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/AdminMarkupset.aspx")%>">Transfer Markup</a>
        </div>--%>
    
    <% ElseIf Session("User_Type").ToString().Trim().ToUpper().Equals("AGENT") Then%>
    
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Agent/Agent_markup.aspx")%>
            <% rawurlS = ""%>
            <% rawurlS = Request.RawUrl%>
            <%rawurlS = "/" & rawurlS.Replace(rawurlS, um)%>
            <a href="<%= rawurlS %>">Dom. Airline Markup</a>--%>
            <a href="<%= ResolveUrl("~/Report/Agent/Agent_markup.aspx")%>">Dom. Airline Markup</a>
        </div>
   
        <div class="fltnewmenu1">
            <%--<% um = ""%>
            <% um = obj.GetMUForPage("Report/Agent/AgentMarkupIntl.aspx")%>
            <% rawurlS = ""%>
            <% rawurlS = Request.RawUrl%>
            <%rawurlS = "/" & rawurlS.Replace(rawurlS, um)%>
            <a href="<%= rawurlS %>">Intl. Airline Markup</a>--%>
            <a href="<%= ResolveUrl("~/Report/Agent/AgentMarkupIntl.aspx")%>">Intl. Airline Markup</a>
        </div>
   

     



      <%--  <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Agent/AgentMarkupset.aspx")%>">Transfer Markup</a>
        </div>--%>
    
    <%End If%>
</div>
