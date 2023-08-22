<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage_Test.master" AutoEventWireup="false" CodeFile="PasswordRedirect.aspx.vb" Inherits="PasswordRedirect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title>RWT</title>
    <%--<script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7,IE=EmulateIE8,IE=EmulateIE9,IE=EmulateIE10,IE=EmulateIE11" />
    <meta http-eqiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <%--Like this also as below--%>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7,IE8,IE9,IE10,IE11" />--%>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/")%>';
        //var ApplUrl = 'http://b2b.//.com/';
    </script>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i,800,800i" rel="stylesheet" />

    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet">


    <%-- <script type="text/javascript" src="JS/newjs/bootstrap.js"></script>
    <script type="text/javascript" src="JS/newjs/bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="https://use.fontawesome.com/20e527aa02.js"></script>

    <script src="<%= ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/js/lytebox.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/JS/JScript.js") %>" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        function PWSMATCH() {
            debugger;
            var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,16}$/;
            if (!regex.test(document.getElementById("ctl00_ContentPlaceHolder1_txt_password").value)) {
                alert("Password must contain:8-16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character'");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_password").focus();
                return false;
            }

            if (!regex.test(document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").value)) {
                alert("Password must contain:8-16 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character'");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_password").value != document.getElementById("ctl00_ContentPlaceHolder1_txt_cpassword").value) {
                alert('Please Enter Same Password');
                return false;
            }
        }


    </script>



    <div class="theme-hero-area" style="margin-top: 60px;">
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
                                <p class="theme-login-subtitle">Update your expired password</p>
                            </div>

                            <p class="theme-login-pwd-reset-text">Agency Name : <asp:Label ID="txtAgencyName" runat="server" /></p>
                            
                            <div id="td_login1" runat="server">

                                <div class="">
                                    <div class="theme-search-area-section theme-search-area-section-line">
                                        <div class="theme-search-area-section-inner">
                                            <i class="theme-search-area-section-icon lin lin-lock-open"></i>
                                            <asp:TextBox ID="txt_oldpassword" placeholder="Old Password" CssClass="theme-search-area-section-input" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <br />


                                <div class="">
                                    <div class="theme-search-area-section theme-search-area-section-line">
                                        <div class="theme-search-area-section-inner">
                                            <i class="theme-search-area-section-icon lin lin-lock"></i>
                                            <asp:TextBox ID="txt_password" placeholder="New Password" CssClass="theme-search-area-section-input" MaxLength="16" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <br />


                                <div class="">
                                    <div class="theme-search-area-section theme-search-area-section-line">
                                        <div class="theme-search-area-section-inner">
                                            <i class="theme-search-area-section-icon lin lin-lock"></i>
                                            <asp:TextBox ID="txt_cpassword" placeholder="Confirm Password" CssClass="theme-search-area-section-input" MaxLength="16" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                              <br />
                               
                                <div class="row">
                                    <div class="col-sm-6">
                                    <asp:Button ID="btn_Save" runat="server" OnClientClick="return PWSMATCH()" Text="Save" CssClass="btn btn-uc btn-dark btn-block btn-lg" Style="padding: 10px;"/>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn btn-uc btn-dark btn-block btn-lg" Style="padding: 10px;"/>
                                        </div>
                                </div>
                                <br />
                                <div class="row">
                                 <div id="logIDDiv" runat="server" style="font-size:10px;font-weight: 500;font-size: 13px;color: rgb(0 0 0);background-color: rgb(153 214 90 / 49%);padding: 8px;margin-bottom: 6px;border-radius: 6px;display: block;border: 1px solid #97d558;">Password Changed Successfully <a style="color: red" href="Login.aspx" >Login</a> Here with New Password </div>
                                    <div id="divErrorMsg" visible="false" runat="server" style="text-align: center;font-size:10px;font-weight: 500;font-size: 13px;color: rgb(0 0 0);background-color: rgb(255 0 0 / 49%);padding: 8px;margin-bottom: 6px;border-radius: 6px;display: block;border: 1px solid #d55858;"></div>

                                </div>


                                <asp:HiddenField ID="oldpasshndfld" runat="server" />
                            </div>
                         </div>
                      </div>
                                 </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

