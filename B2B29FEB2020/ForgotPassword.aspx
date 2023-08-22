<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage_Test.master" CodeFile="ForgotPassword.aspx.vb" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         label {
            color: orange;
        }
    </style>
    <link href="Advance_CSS/css/bootstrap.css" rel="stylesheet" />
    <link href="Advance_CSS/css/styles.css" rel="stylesheet" />


            <div class="theme-hero-area" style="margin-top:60px;">
      <div class="theme-hero-area-bg-wrap">
        <div class="theme-hero-area-bg"></div>
        <div class="theme-hero-area-mask-strong"></div>
      </div>
      <div class="theme-hero-area-body">
        <div class="theme-page-section _pt-100 theme-page-section-xl">
          <div class="container">
            <div class="row">
              <div class="col-md-4 col-md-offset-4">

                  

                <div class="theme-login theme-login-white" style="margin-top: 20px;">
                  
                  <div class="theme-login-box">
                    <div class="theme-login-box-inner">
                        <div class="theme-login-header" style="margin-bottom: 10px !important;border-bottom: 2px solid #91e111;padding: 0px;padding-bottom: 7px;">
                    <h1 class="theme-login-title">Password Reset</h1>
                    <p class="theme-login-subtitle">Get your forgotten password</p>
                  </div>
                      <p class="theme-login-pwd-reset-text">Enter the email associated with your account in the field below and we'll email your password.</p>
              

                          <div class="">
                
                                            <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                           <asp:TextBox ID="txt_UserID" placeholder="Enter User ID" CssClass="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                                                </div>
                    </div>
                        <br />

                        <div class="">
                              <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-phone"></i>
                            <asp:TextBox ID="txt_MobileNo" class="theme-search-area-section-input" placeholder="Enter Registered Mobile No" runat="server"></asp:TextBox>
                                </div>
                                </div>
                            </div>
                        <br />
                        <div class="">
                              <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-Email">✉</i>
                            <asp:TextBox ID="txt_EmailID" CssClass="theme-search-area-section-input" placeholder="Enter Registered Email" runat="server"></asp:TextBox>
                        </div>
                                  </div>
                            </div>
                        <br />

                   <%--   <a class="btn btn-uc btn-dark btn-block btn-lg" href="#">Reset Password</a>--%>
                           <asp:Button ID="Button1" runat="server" Text="Get Password" CssClass="btn btn-uc btn-dark btn-block btn-lg"
                    Style="padding: 10px;"/>
                     
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>























    <div class="container card bg-primary text-white h-100" style="display:none;">
        <div class="card-header">
            <div class="col-md-12">
                <h3 style="text-align: center;">Forgot your password Or Expired?</h3>
                <hr />
            </div>
        </div>

        <div class="card-body">
            <div class="col-md-12">

                <div class="row">

                 <%--   <div class="col-md-3">
                <label>User ID</label>
    
        <asp:TextBox ID="txt_UserID" placeholder="Enter User ID" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>--%>

                    <div class="col-md-3">
               <label>Registered Email</label>
   
        <%--<asp:TextBox ID="txt_EmailID" CssClass="form-control" placeholder="Enter Registered Email" runat="server"></asp:TextBox>--%>
                    </div>

                    <div class="col-md-3">
                <label>Registered Mobile No</label>
  
        <%--<asp:TextBox ID="txt_MobileNo" class="form-control" placeholder="Enter Registered Mobile No" runat="server"></asp:TextBox>--%>
                    </div>

                    <div class="col-md-3">
                        
             <%--   <asp:Button ID="Button1" runat="server" Text="Get Password" CssClass="btn btn-danger"
                    BorderColor="#161946" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                    style="margin-top: 24px;"/>--%>

                        </div>

                </div>

                 <br />
        <br />
    

            </div>
        </div>
    </div>

</asp:Content>
