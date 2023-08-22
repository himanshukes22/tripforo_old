<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OTPValidateUser.aspx.cs" Inherits="OTPValidate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="container">
        <div class="row">
            <div class="col-md-12 aboutus">

                <div class="sectionHeader">
                    <span class="header ver2">
                        <em>Login</em>

                    </span>

                </div>
                 <span>Enter OTP</span>
                <asp:TextBox ID="TxtOTP"  runat="server" MaxLength="6" TextMode="Password" ></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:Button ID="BtnSubmit" runat="server"  OnClick="BtnSubmit_Click" Text="Validate"  CssClass="btnsss" style="width: 136px;"/>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnAgencyId" runat="server" />
    <asp:HiddenField ID="hdnOTPID" runat="server" />
    <asp:HiddenField ID="hdnCreatedBy" runat="server" />
</asp:Content>

