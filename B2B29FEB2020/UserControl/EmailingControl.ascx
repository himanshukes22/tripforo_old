<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmailingControl.ascx.vb"
    Inherits="UserControl_EmailingControl" %>
<div id="right_sidebar">
    <div class="joinus">
        <span>Join Us:</span> <a href="http://twitter.com/#!/Springtravels" target="_blank">
            <img src="images/twitter.png" /></a><a href="http://www.flickr.com/photos/springtravels/"
                target="_blank"><img src="images/flickr.png" /></a><a href="http://in.linkedin.com/pub/spriing-travels/3b/13b/6b7"
                    target="_blank"><img src="images/linkedin.png" /></a><a href="http://www.facebook.com/pages/Spring-Travels-Pvt-Ltd/211208768941516?created&sk=page_getting_started#!/pages/Spring-Travels-Pvt-Ltd/211208768941516?sk=wall"
                        target="_blank"><img src="images/facebook.png" /></a></div>
    <div class="right_sidebar_section">
        <h2>
            Get Email Updates</h2>
        <label>
            Please enter your email address.</label>
        <asp:TextBox ID="subscribenow" CssClass="txtbx" runat="server"></asp:TextBox><asp:Label
            ID="Label1" runat="server" Text="" Visible="false"></asp:Label><br />
        <input type="button" value="Go" class="submit_btn" id="btn_go" />
        <%--  <asp:Button ID="btn_email" runat="server"  Text="Go"  CssClass="submit_btn"
                    />--%>
        <%-- --%>
        <%-- <asp:Button ID="reset" runat="server" Text="Reset" CssClass="submit_btn"  />--%>
        <%----%>
        <input type="button" value="Reset" class="submit_btn" id="btn_reset" />
        <div style="clear: both;">
        </div>
    </div>
    <div class="right_sidebar_section">
        <h2>
            Get SMS Updates</h2>
        <label>
            Please enter your Name & Number.</label>
        Name: &nbsp; &nbsp;
        <asp:TextBox ID="namecnumber" CssClass="txtbx" Width="152px" runat="server"></asp:TextBox><br />
        Number:
        <asp:TextBox ID="cnumber" runat="server" Width="153px" CssClass="txtbx"></asp:TextBox><br />
        <%--  <asp:Button ID="btn_sms" runat="server" Text="Add Now" CssClass="submit_btn" />
                <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="submit_btn"  />--%>
        <input type="button" value="Add Now" class="submit_btn" id="btn_sms" />
        <input type="button" value="Reset" class="submit_btn" id="btn_reset2" />
        <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
        <div style="clear: both;">
        </div>
    </div>
</div>
