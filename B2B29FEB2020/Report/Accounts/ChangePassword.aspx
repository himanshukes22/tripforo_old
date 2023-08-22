<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb"  MasterPageFile="~/MasterPageForDash.master" Inherits="SprReports_Accounts_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">

        

        </div>

       <div class="large-8 medium-8 small-12 columns heading">
            <div class="large-12 medium-12 small-12 heading1">
                Change Password
            </div>
            <div class="clear1"></div>
           <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
             <div class="clear1"></div>
            <div class="large-12 medium-12 small-12">
   
    <table>
<%--        <tr><td>Old Password</td><td><asp:TextBox ID="TextBoxOldPass" runat="server" TextMode="Password"></asp:TextBox> </td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></td></tr>--%>
         <tr><td>New Password</td><td><asp:TextBox ID="TextBoxNewPass" runat="server" TextMode="Password"></asp:TextBox> </td>
             <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter new password." ControlToValidate="TextBoxNewPass" ValidationGroup="cp"></asp:RequiredFieldValidator></td></tr>
         <tr><td>Confirm Password</td><td><asp:TextBox ID="TextBoxConfirmPass" runat="server" TextMode="Password"></asp:TextBox> </td>
             <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter confirm password." ValidationGroup="cp" ControlToValidate="TextBoxConfirmPass"></asp:RequiredFieldValidator>
               <br />  <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New password and corfirm password did not match." ValidationGroup="cp" ControlToValidate="TextBoxConfirmPass" ControlToCompare="TextBoxNewPass"></asp:CompareValidator>
             </td></tr>
         <tr><td></td><td><asp:Button runat="server" ID="ButtonSubmit" CssClass="buttonfltbk" Text="Submit" ValidationGroup="cp" /> </td><td></td></tr>

    </table>


   </div>
           </div>
         </div>
 </asp:Content>
   
