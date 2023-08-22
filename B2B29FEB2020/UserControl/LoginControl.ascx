<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoginControl.ascx.vb"
    Inherits="UserControl_LoginControl" %>
<link href="../icofont/icofont.css" rel="stylesheet" />
<link href="../icofont/icofont.min.css" rel="stylesheet" />


<style type="text/css">
    .service__icon--heading {
        font-size: 25px;
        text-align: center;
        line-height: 1.3;
        padding: 25px 0;
        color: #808080;
        font-weight: 200;
        /*  width: 50%;
    float:right;*/
    }

    .float-md-left {
        float: left !important;
    }



    .service__icon--list {
        display: inline-block;
        width: 49%;
        text-align: center;
    }

    .service__icon--label {
        font-size: 14px;
        color: #324253;
        padding-top: 10px;
    }

    .round-ico {
        border: 2px solid #828282;
        border-radius: 50%;
        padding: 7px;
    }

    .semiround {
        background: #24547d;
        padding: 10px;
        border-radius: 10px;
        color: #fff;
    }

    .business__image {
        height: 300px;
    }

    .pt-5, .py-5 {
        padding-top: 3rem !important;
    }

    .main__heading {
        font-size: 32px;
        color: #333;
    }

    .mb-3, .my-3 {
        margin-bottom: 1rem !important;
    }

    @media (min-width: 768px) {
        .col-md-6 {
            -webkit-box-flex: 0;
            -ms-flex: 0 0 50%;
            flex: 0 0 50%;
            max-width: 50%;
        }
    }

    .business__points--text {
        font-size: 14px;
        line-height: 1.5;
        color: #666;
        padding: 10px 0;
    }

    .business__content {
        display: flex;
        align-items: center;
        padding: 10px 0;
    }

    .pl-2, .px-2 {
        padding-left: .5rem !important;
    }

    .business__details--heading {
        font-size: 20px;
        color: #324253;
        font-weight: 500;
        margin-bottom: 0px;
    }

    .business__details--text {
        font-size: 12px;
        color: #999999;
        font-weight: 300;
        padding-top: 5px;
    }


    .business {
        font-size: 14px;
        line-height: 1.5;
        color: #666;
        padding: 10px 0;
    }

    .loginas__tab {
        padding: 25px 0 20px;
    }

    .loginas__tab--list.active, .loginas__tab--list:hover {
        color: #ff1228;
        border-bottom: 1px solid #ff1228;
    }

    .loginas__tab--list {
        display: inline-block;
        margin-right: 15px;
        font-size: 12px;
        font-family: "TJ_font-700";
        font-weight: 500;
        cursor: pointer;
        padding-bottom: 8px;
        color: #999;
        font-family: 'Quicksand', sans-serif !important;
    }

    .form__field--text {
        font-size: 14px;
        padding: 10px 0;
        color: #999;
    }

    .login__form--reset--link {
        color: #ff1228;
    }


    .trav-b2b {
        font-size: 20px;
        color: #000000;
        width: 800px;
        margin: 0 auto;
        text-align: center;
        font-weight: 600;
        padding: 0px 0 60px 0;
    }

    .b2b-product-tab {
        display: flex;
        justify-content: center;
        align-items: center;
        padding-bottom: 150px;
        /* max-width: 500px; */
        margin: 0 auto;
        margin-right: 50%;
        margin-left: 55%;
    }

    .product-type {
        height: 100px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: flex-end;
        padding-right: 93px;
    }

    .b2b-static-cont .b2b-product-tab .product-type img {
        display: block;
    }

    .product-type span {
        display: block;
        font-size: 22px;
        color: #ee3157;
        padding-top: 17px;
        font-weight: 600;
    }

    .easier-management-types-cont {
        background: #fafafa;
        margin-bottom: 90px;
    }

        .easier-management-types-cont .perfect-heading {
            padding-top: 100px;
            padding-bottom: 60px;
        }

    .aertrip-color {
        color: #ffbe07;
    }

    .easier-management-types {
        display: flex;
        justify-content: center;
        text-align: center;
        /*margin-left: 50%;
    margin-right: 46%;*/
    }

        .easier-management-types .management-lists {
            margin-right: 50px;
            width: 162px;
            color: #000;
            font-size: 16px;
            font-weight: 600;
        }

            .easier-management-types .management-lists .image {
                padding: 0;
                width: 93px;
                margin: 0 auto;
            }

            .easier-management-types .management-lists .name {
                padding-top: 16px;
                color: #000;
            }


    element.style {
    }

    .easier-management-para {
        font-size: 24px;
        padding: 85px 0 100px 0;
        width: 916px;
        margin: 0 auto;
        text-align: center;
        color: #000000;
    }

    .perfect-heading {
        font-size: 55px;
        text-align: center;
        margin: 0;
        padding: 0 0 50px 0;
        font-weight: bold;
        color: #2c3a4e;
    }

    @media only screen and (max-width: 500px) {
        .banner {
            display: none;
        }

        .banner, .business, .deal, .theme-copyright {
            display: none;
        }

        .navbar-theme.navbar-theme-abs {
            display: none !important;
        }

        .theme-login-header {
            text-align: center;
        }
    }

    
input[type="radio"] {
  visibility: hidden; /* 1 */
  height: 0; /* 2 */
  width: 0; /* 2 */
}

label {
  display: flex;
  
  vertical-align: middle;
  align-items: center;
  justify-content: center;
  text-align: center;
  cursor: pointer;
  background-color: var(--color-gray);
  color: #828282;
  padding: 5px 10px;
  border-radius: 6px;
  transition: color --transition-fast ease-out, 
              background-color --transition-fast ease-in;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
 margin-right: 8px;
}

label:last-of-type {
 margin-right: 0;
}

input[type="radio"]:checked + label {
  color: #6dbb3f;
    border-bottom: 2px solid #6dbb3f;
}

input[type="radio"]:hover:not(:checked) + label {
 background-color: none;
 color: #000;
}

.InputGroup {
 display: flex;

}
</style>

<script>
    function supplier() {
        //location.replace("fixeddeparture.tripforo.com")
        window.open("http://fixeddeparture.tripforo.com/");
    }
</script>




<div class="theme-hero-area" style="margin-top: 62px;">
    <div class="theme-hero-area-bg-wrap">
        <div class="theme-hero-area-bg"></div>
        <div class="theme-hero-area-mask theme-hero-area-mask-strong" style="background: #fff !important;"></div>
    </div>
    <div class="theme-hero-area-body">
        <div class="theme-page-section _pt-100 theme-page-section-xl" style="padding-top: 6px !important;">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-8 banner" style="padding: 0; height: 420px;">
                        <img src="../Advance_CSS/banner-pln.jpg" style="width: 100%;margin-top: -55px;" />
                        <%--<h4 class="service__icon--heading float-md-right">Tripforo is the world's<span class="block">A
                        Smartest B2B Travel Platform
                    </span></h4>
                                <div class="row float-md-left col-md-12">
                                    <div class="col-md-4 banner__service--img" style="z-index:9;">
                                       
                        <img src="../Advance_CSS/new_icons/business-woman.png" class="banner__img" alt="Best B2B Travel Portal in India | White Label Solution" style="width:205px;">
                    </div>
                    <div class="col-md-8 banner__service--icon"  style="background: #f3f3f3;padding: 78px;margin-left: -93px;width: 79%;height: 313px;border-radius: 10px;-webkit-box-shadow: 0 5px 8px 0 rgb(133 133 133 / 50%);box-shadow: 0 5px 8px 0 rgb(133 133 133 / 50%);">
                        <ul class="service__icon">
                            <div class="service__icon--list">
                                <i class="icofont-airplane icofont-3x semiround"></i>
                                <p class="service__icon--label">Flights</p>
                            </div>
                            <div class="service__icon--list">
                                <i class="icofont-hotel icofont-3x semiround"></i>
                                <p class="service__icon--label">Hotels</p>
                            </div>
                            </ul>
                            <ul class="service__icon">
                            <div class="service__icon--list">
                                <i class="icofont-beach icofont-3x semiround"></i>
                                <p class="service__icon--label">Holidays</p>
                            </div>

                               <div class="service__icon--list">
                                <i class="icofont-visa-alt icofont-3x semiround"></i>
                                <p class="service__icon--label">Visa</p>
                            </div>
                        </ul>
                    </div>
           
                </div>--%>
                    </div>
                    <div class="col-md-4" style="padding: 40px 15px 0 30px;">
                        <div class="theme-login theme-login-white">
                            <div class="theme-login-header">
                                <h4 class="theme-login-title">Welcome to Tripforo</h4>
                                <p style="font-size: 14px; color: #999;">Login here to your account as</p>

                                
        <asp:Label ID="lblerror" Visible="false" Font-Size="10px" runat="server" style="font-weight: 500;font-size: 13px;color: rgb(229 0 0); background-color: rgb(238 207 207); padding: 8px; margin-bottom: 6px; border-radius: 6px; display: block;"></asp:Label>
    

                                <div class="loginas__tab">

                                    <div class="InputGroup">

                                        <%--<input type="radio" name="size" id="size_1" value="small" checked />--%>
                                        <asp:RadioButton ID="rdoAgent" runat="server" Text="AGENT" GroupName="LoginType" Checked="true"/>

                                       


                                        <asp:RadioButton ID="rdoSupplier" runat="server" Text="SUPPLIER" GroupName="LoginType" />
                                        <%--<input type="radio" name="size" id="size_2" value="small" />--%>
                                        


                                    </div>

                                </div>

                            </div>
                            <div class="">
                                <div class="theme-login-box-inner">
                                    <form class="theme-login-form">
                                        <asp:Login ID="UserLogin" runat="server">


                                            <TextBoxStyle />
                                            <LoginButtonStyle />
                                            <LayoutTemplate>

                                                <div class="form-group theme-login-form-group">
                                                    <div class="theme-search-area-section first theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">

                                                        <div class="">
                                                            <i class="theme-search-area-section-icon lin lin-user" style="line-height: 44px !important;"></i>
                                                            <asp:TextBox runat="server" class="theme-search-area-section-input" placeholder="User Id" ID="UserName" Style="height: 45px !important; border-radius: 5px; border: 1px solid #949494 !important;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                ErrorMessage="User Name is required." ToolTip="User Id is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="form-group theme-login-form-group">
                                                    <div class="theme-search-area-section first theme-search-area-section-curved theme-search-area-section-bg-white theme-search-area-section-no-border theme-search-area-section-mr">

                                                        <div class="">
                                                            <i class="theme-search-area-section-icon lin lin-lock" style="line-height: 44px !important;"></i>
                                                            <asp:TextBox runat="server" class="theme-search-area-section-input" TextMode="Password" placeholder="Password" ID="Password" Style="height: 45px !important; border-radius: 5px; border: 1px solid #949494 !important;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Button runat="server" ID="LoginButton" OnClick="LoginButton_Click" Text="Sign In" class="btn btn-uc btn-dark btn-block btn-lg-custom" Style="height: 50px;" />
                                                    </div>


                                                </div>

                                                <div>


                                                    <p class="form__field--text" style="margin-bottom: -5px;">
                                                        Forgot your password? <a href="../ForgotPassword.aspx" class="login__form--reset--link">Reset
                        Here
                                                        </a>
                                                    </p>

                                                    <p class="form__field--text">
                                                        Don’t have any account? <a href="regs_new.aspx" class="login__form--reset--link">Sign
                        up here</a>
                                                    </p>
                                                </div>
                                            </LayoutTemplate>
                                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                            <TitleTextStyle />
                                        </asp:Login>
                                    </form>


                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



<div class="theme-page-section theme-page-section-xxl">
    <div class="container">
        <div class="theme-page-section-header">
            <h5 class="theme-page-section-title">Today's Top Deals</h5>
            <p class="theme-page-section-subtitle">Save your money with our best offers</p>
        </div>
        <div class="row row-col-mob-gap" data-gutter="10">
            <div class="col-md-3 ">
                <div class="banner _br-5 banner-animate banner-animate-mask-in banner-animate-zoom-in">
                    <img class="banner-img" src="https://images.unsplash.com/photo-1505164294036-5fad98506d20?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1050&q=80" alt="Image Alternative text" title="Image Title">
                    <div class="banner-mask"></div>
                    <a class="banner-link" href="#"></a>
                    <div class="banner-caption _ta-c _pb-20 _pt-60 banner-caption-bottom banner-caption-grad">
                        <h5 class="banner-title _fs">Flights to Delhi</h5>
                        <p class="banner-subtitle _fw-n _mt-5">Rountrips from ₹ 1299</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 ">
                <div class="banner _br-5 banner-animate banner-animate-mask-in banner-animate-zoom-in">
                    <img class="banner-img" src="https://thecollegepost.com/wp-content/uploads/2020/11/pexels-anna-shvets-3943882-min-scaled.jpg" alt="Image Alternative text" title="Image Title">
                    <div class="banner-mask"></div>
                    <a class="banner-link" href="#"></a>
                    <div class="banner-caption _ta-c _pb-20 _pt-60 banner-caption-bottom banner-caption-grad">
                        <h5 class="banner-title _fs">Fly for Free</h5>
                        <p class="banner-subtitle _fw-n _mt-5">Earn free miles with rewards system</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 ">
                <div class="banner _br-5 banner-animate banner-animate-mask-in banner-animate-zoom-in">
                    <img class="banner-img" src="https://images.unsplash.com/photo-1618765138214-465c0399fcf2?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1050&q=80" alt="Image Alternative text" title="Image Title">
                    <div class="banner-mask"></div>
                    <a class="banner-link" href="#"></a>
                    <div class="banner-caption _ta-c _pb-20 _pt-60 banner-caption-bottom banner-caption-grad">
                        <h5 class="banner-title _fs">First Class Flights</h5>
                        <p class="banner-subtitle _fw-n _mt-5">Save up to 30%</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 ">
                <div class="banner _br-5 banner-animate banner-animate-mask-in banner-animate-zoom-in">
                    <img class="banner-img" src="https://images.unsplash.com/photo-1517400508447-f8dd518b86db?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1050&q=80" alt="Image Alternative text" title="Image Title">
                    <div class="banner-mask"></div>
                    <a class="banner-link" href="#"></a>
                    <div class="banner-caption _ta-c _pb-20 _pt-60 banner-caption-bottom banner-caption-grad">
                        <h5 class="banner-title _fs">Last Minute Deals</h5>
                        <p class="banner-subtitle _fw-n _mt-5">Travel smart, travel cheap</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<section class="business pt-5">
    <div class="container">
        <h1 class="main__heading mb-3" style="font-weight: 500;">Easier Management
        </h1>
        <div class="row">


            <div class="col-md-6 p-0 " style="margin-top: 16px;">

                <img src="../Advance_CSS/new_icons/Virtual-Office.png" class="business__image" alt="Tripforo B2B">
            </div>

            <div class="col-md-6 business__points">
                <p class="business__points--text">And that’s not all! A lot more features like Timeline view, Filter Graphs, Group Booking tools are available online. This with unparalleled Security &amp; Privacy and 24/7 Online &amp; Offline Support makes Tripforo the best B2B travel platform out there.</p>
                <div class="row">
                    <div class="col-md-6">
                        <div class="business__content">

                            <i class="icofont-dashboard-web icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">Your very own CRM</h4>
                                <p class="business__details--text">Years of business experience</p>
                            </div>
                        </div>
                        <div class="business__content">
                            <i class="icofont-pay icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">Payment options</h4>
                                <p class="business__details--text">Get awesome opportunities</p>
                            </div>
                        </div>
                        <div class="business__content">
                            <i class="icofont-air-ticket icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">Booking management</h4>
                                <p class="business__details--text">Get Interactive exclusive portal</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="business__content">
                            <i class="icofont-live-support icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">24/7</h4>
                                <p class="business__details--text">Online after sales</p>
                            </div>
                        </div>
                        <div class="business__content">
                            <i class="icofont-list icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">Online Accounting</h4>
                                <p class="business__details--text">Earn good deals and commissions</p>
                            </div>
                        </div>
                        <div class="business__content">
                            <i class="icofont-sale-discount icofont-2x round-ico"></i>
                            <div class="business__details pl-2">
                                <h4 class="business__details--heading">Latest Deals</h4>
                                <p class="business__details--text">Access to the great deals</p>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </div>
    </div>
</section>




<div class="theme-hero-area">
    <div class="theme-hero-area-bg-wrap">
        <div class="theme-hero-area-bg" style="background-image: url(https://img.emg-services.net/educations/education341119/bachelor-s-degree-in-hospitality-and-tourism-management.jpg);"></div>
        <div class="theme-hero-area-mask theme-hero-area-mask-half" style="background: #00000047 !important;"></div>
    </div>
    <div class="theme-hero-area-body">
        <div class="theme-page-section _pv-100">
            <div class="container">
                <div class="row">
                    <div class="col-md-8 col-md-offset-2">
                        <div class="theme-hero-text theme-hero-text-white theme-hero-text-center">
                            <div class="theme-hero-text-header">
                                <h2 class="theme-hero-text-title">Pay less travel more</h2>
                                <p class="theme-hero-text-subtitle">Subscribe now and unlock our secret deals. Save up to 70% by getting access to our special offers for hotels, flights, cars, vacation rentals and travel experiences.</p>
                            </div>
                            <a class="btn _tt-uc _mt-20 btn-white btn-ghost btn-lg" href="#">Sign Up Now</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<div class="large-12 medium-12 small-12" style="display: none;">
    <div class="col-md-12  userway "><i class="fa fa-user-circle" aria-hidden="true"></i></div>
    <div class="lft f16" style="display: none;">
        Login Here As
                <asp:DropDownList ID="ddlLogType" runat="server">
                    <asp:ListItem Value="C">Customer</asp:ListItem>
                    <asp:ListItem Value="M">Management</asp:ListItem>
                </asp:DropDownList>
    </div>
    <%--<div class="rgt">
                <a href="ForgotPassword.aspx" rel="lyteframe" class="forgot">Forgot Your password (?)</a>
                </div>--%>
    <div class="clear1">
    </div>

    <div class="form-group has-success has-feedback">

        <div class="input-group">
            <span class="input-group-addon"><i class="fa fa-user-o" aria-hidden="true"></i></span>
            <%--<asp:TextBox ID="UserName" class="form-controlsl " BackColor="White" placeholder="Enter Your User Id" runat="server"></asp:TextBox>--%>
        </div>
        <%--<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ErrorMessage="User Name is required." ToolTip="User Id is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>--%>
    </div>
    <div class="form-group has-success has-feedback">

        <div class="input-group">
            <span class="input-group-addon"><i class="fa fa-lock" aria-hidden="true" style="font-size: 23px;"></i></span>
            <%-- <asp:TextBox ID="Password" class="form-controlsl" BackColor="White" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>--%>
        </div>
        <%-- <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>--%>
    </div>



    <div class="large-4 medium-4 small-12 columns">
        <div class="clear1">
        </div>
        <%--<asp:Button ID="LoginButton" runat="server" ValidationGroup="UserLogin" OnClick="LoginButton_Click" CssClass="btnsss"
                    Text="LOGIN" />--%>
        <br />

        <a href="../ForgotPassword.aspx" style="color: #ff0000">Forgot Password</a>
    </div>
    <div class="clear">
    </div>
    
    <div class="clear">
    </div>
</div>




<script>
    var overlay = document.getElementById("overlay");

    // Buttons to 'switch' the page
    var openSignUpButton = document.getElementById("slide-left-button");
    var openSignInButton = document.getElementById("slide-right-button");

    // The sidebars
    var leftText = document.getElementById("sign-in");
    var rightText = document.getElementById("sign-up");

    // The forms
    var accountForm = document.getElementById("sign-in-info")
    var signinForm = document.getElementById("sign-up-info");

    // Open the Sign Up page
    openSignUp = () => {
        // Remove classes so that animations can restart on the next 'switch'
        leftText.classList.remove("overlay-text-left-animation-out");
        overlay.classList.remove("open-sign-in");
        rightText.classList.remove("overlay-text-right-animation");
        // Add classes for animations
        accountForm.className += " form-left-slide-out"
        rightText.className += " overlay-text-right-animation-out";
        overlay.className += " open-sign-up";
        leftText.className += " overlay-text-left-animation";
        // hide the sign up form once it is out of view
        setTimeout(function () {
            accountForm.classList.remove("form-left-slide-in");
            accountForm.style.display = "none";
            accountForm.classList.remove("form-left-slide-out");
        }, 700);
        // display the sign in form once the overlay begins moving right
        setTimeout(function () {
            signinForm.style.display = "flex";
            signinForm.classList += " form-right-slide-in";
        }, 200);
    }

    // Open the Sign In page
    openSignIn = () => {
        // Remove classes so that animations can restart on the next 'switch'
        leftText.classList.remove("overlay-text-left-animation");
        overlay.classList.remove("open-sign-up");
        rightText.classList.remove("overlay-text-right-animation-out");
        // Add classes for animations
        signinForm.classList += " form-right-slide-out";
        leftText.className += " overlay-text-left-animation-out";
        overlay.className += " open-sign-in";
        rightText.className += " overlay-text-right-animation";
        // hide the sign in form once it is out of view
        setTimeout(function () {
            signinForm.classList.remove("form-right-slide-in")
            signinForm.style.display = "none";
            signinForm.classList.remove("form-right-slide-out")
        }, 700);
        // display the sign up form once the overlay begins moving left
        setTimeout(function () {
            accountForm.style.display = "flex";
            accountForm.classList += " form-left-slide-in";
        }, 200);
    }

    // When a 'switch' button is pressed, switch page
    openSignUpButton.addEventListener("click", openSignUp, false);
    openSignInButton.addEventListener("click", openSignIn, false);
</script>
