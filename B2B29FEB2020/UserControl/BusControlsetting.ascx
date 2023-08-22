<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusControlsetting.ascx.cs" Inherits="UserControl_BusControlsetting" %>
 

<div>
<% if (Session["User_Type"].ToString().Trim().ToUpper().Equals("AGENT"))
   {%>
                                           <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BusMarkup.aspx")%>">BUS Markup</a>
        </div>

   
<%}
   else if (Session["User_Type"].ToString().Trim().ToUpper().Equals("ADMIN"))
   {%>

                                              <div class="fltnewmenu1">
                                                   <a href="<%=ResolveUrl("~/BS/BusAdminMarkup.aspx")%>">BUS Markup</a>
                                                  </div>
                                                   <div class="fltnewmenu1" style="color:aqua">
            <a href="<%=ResolveUrl("~/BS/BusCommsion.aspx")%>">BUS Commission</a>
        </div>
           
        
    
  <%}%>
    </div>