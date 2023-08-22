<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoginControl.ascx.vb"
    Inherits="UserControl_LoginControl" %>

<asp:Login ID="UserLogin" runat="server">
    <TextBoxStyle />
    <LoginButtonStyle />
    <LayoutTemplate>
        <div class="large-12 medium-12 small-12">
            <div class="lft f16" style="display:none;">
                Login Here As <asp:DropDownList ID="ddlLogType" runat="server">
                    <asp:ListItem Value="C">Customer</asp:ListItem>
                    <asp:ListItem Value="M">Management</asp:ListItem>
                              </asp:DropDownList>
            </div>
<%--<div class="rgt">
                <a href="ForgotPassword.aspx" rel="lyteframe" class="forgot">Forgot Your password (?)</a>
            </div>--%>
            
            <div class="clear1">
            </div>
            
            <div class="large-3 medium-3 small-11 columns">
               <%-- <img src="images/id_card.png" />--%>
               UserId:
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
               
            </div>
            <div class="large-1 medium-1 small-1 columns">
                 <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>
            </div>
            
            <div class="large-3 medium-3 small-11 columns">
                <%--<img src="images/login.png" />--%>
              Password:
                  <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                
            </div>
            <div class="large-1 medium-1 small-1 columns"><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator></div>
            
            <div class="large-4 medium-4 small-12 columns">
                <div class="clear1">
            </div>
                <asp:Button ID="LoginButton" runat="server" ValidationGroup="UserLogin" OnClick="LoginButton_Click" CssClass="btn"
                    Text="Log In" />
            </div>
            <div class="clear">
            </div>
            <div>
                <asp:Label ID="lblerror" Font-Size="10px" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="clear">
            </div>
        </div>
    </LayoutTemplate>
    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <TitleTextStyle />
</asp:Login>
