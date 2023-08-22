<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HotelSettings.ascx.cs" Inherits="UserControl_HotelSettings" %>

<div>
    <%if (Session["User_Type"].ToString().Trim().ToUpper().Equals("ADMIN") ){%>
   
    <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Admin/AdminHtlMarkup.aspx")%>">Hotel Markup</a>
        </div>
    
    <% } else if( Session["User_Type"].ToString().Trim().ToUpper().Equals("AGENT") ){%>
    
    <div class="fltnewmenu1">
            <a href="<%= ResolveUrl("~/Report/Agent/AgentHtlMarkup.aspx")%>">Hotel Markup</a>
        </div>
    <%}%>
</div>