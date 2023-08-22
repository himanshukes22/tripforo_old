<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="DashboardForOffline.aspx.cs" Inherits="Report_Proxy_DashboardForOffline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        a {
            color: #fff;
        }
       /* .page-title {
    color: #343a40;
    font-size: 1.125rem;
    margin-bottom: 0;
}
        .page-title .page-title-icon {
    display: inline-block;
    width: 36px;
    height: 36px;
    border-radius: 4px;
    text-align: center;
    -webkit-box-shadow: 0px 3px 8.3px 0.7px rgb(163 93 255 / 35%);
    box-shadow: 0px 3px 8.3px 0.7px rgb(163 93 255 / 35%);
}
        .bg-gradient-primary {
    background: -webkit-gradient(linear, left top, right top, from(#475c7d), to(#223045)) !important;
    background: linear-gradient(to right, #475c7d, #223045) !important;
}*/
    </style>
	<style type="text/css">
	@media (min-width: 1200px) {
    .container-scroller .content-wrapper {
        max-width: 1140px;
    }
}

@media (min-width: 992px) {
    .container-scroller .content-wrapper {
        max-width: 960px;
    }
}

@media (min-width: 768px) {
    .container-scroller .content-wrapper {
        max-width: 720px;
    }
}

@media (min-width: 576px) {
    .container-scroller .content-wrapper {
        max-width: 540px;
    }
}

.container-scroller .content-wrapper {
    width: 100%;
    padding-right: 20px;
    padding-left: 20px;
    margin-right: auto;
    margin-left: auto;
    margin: auto;
    padding: 2rem 0.937rem;
}


.page-header {
    margin: 0 0 1.5rem 0;
}

.align-items-center, .page-header, .loader-demo-box, .list-wrapper ul li, .email-wrapper .message-body .attachments-sections ul li .thumb, .email-wrapper .message-body .attachments-sections ul li .details .buttons, .horizontal-menu .top-navbar .navbar-menu-wrapper .navbar-nav, .horizontal-menu .top-navbar .navbar-menu-wrapper .navbar-nav .nav-item.nav-profile, .horizontal-menu .top-navbar .navbar-menu-wrapper .navbar-nav .nav-item.dropdown .navbar-dropdown .dropdown-item, .horizontal-menu .bottom-navbar .page-navigation > .nav-item > .nav-link {
    -webkit-box-align: center !important;
    -ms-flex-align: center !important;
    align-items: center !important;
}

.justify-content-between, .page-header {
    -webkit-box-pack: justify !important;
    -ms-flex-pack: justify !important;
    justify-content: space-between !important;
}

.card {
    border: 0;
    background: #fff;
}

.card {
    position: relative;
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    -ms-flex-direction: column;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 1px solid rgba(0, 0, 0, 0.125);
    border-radius: 0.3125rem;
}

.card-body {
    -webkit-box-flex: 1;
    -ms-flex: 1 1 auto;
    flex: 1 1 auto;
    min-height: 1px;
    padding: 1.25rem;
}

.card-main {
    background: #fff;
    box-shadow: 1px 1px 15px rgb(0 0 0 / 10%);
    margin-bottom: 20px;
    position: relative;
    clear: both;
    border-radius: 6px;
    padding: 0px;
}

.breadcrumb-arrow {
    height: 36px;
    padding: 0;
    line-height: 36px;
    list-style: none;
    
    margin-bottom: 15px;
    border-radius: 6px;
}

    .breadcrumb-arrow li:first-child {
        border-radius: 6px 0 0 6px;
        background: var(--breadcrumbFirstChild-bg);
    }


    .breadcrumb-arrow li {
        display: inline-block;
        vertical-align: top;
        background: #1c2d43;
        margin-right: 23px;
        position: relative;
    }

.breadcrumb-arrow li:first-child:before {
    border: none;
}


    .breadcrumb-arrow li:before {
        content: "";
        position: absolute;
        top: 0;
        border: 0 solid #1c2d43;
        border-width: 18px 10px;
        width: 0;
        height: 0;
        left: -20px;
        border-color: #1c2d43;
        border-left-color: transparent;
    }

.breadcrumb-arrow a {
    display: inline-block;
    vertical-align: top;
    position: relative;
    color: #fff;
    text-decoration: none;
    padding: 0 10px;
}

    .breadcrumb-arrow li:first-child {
        border-radius: 6px 0 0 6px;
        background: #96d558;
    }

        .breadcrumb-arrow li:first-child:after {
            border-left-color: #96d558;
        }


    .breadcrumb-arrow li:after {
        content: "";
        position: absolute;
        top: 0;
        border: 0 solid #1c2d43;
        border-width: 18px 10px;
        width: 0;
        height: 0;
        left: 100%;
        border-color: transparent;
        border-left-color: #1c2d43;
    }


.btn-export {
    background: #96d558;
    color: #fff;
    padding: 6px 10px;
    border-radius: 2px;
    text-transform: uppercase;
    font-size: 10px;
    margin-top: 0;
    margin-right: 15px;
    transition: all 0.3s ease-out;
    border: none;
}


.btn, .cmn-btn {
    background: #1c2d43 !important;
    color: #fff;
    position: relative;
    /*top: 20px;*/
}

    .btn:hover, .btn:focus, .btn.focus {
        color: #fff !important;
        text-decoration: none;
    }

.btn, .cmn-btn:hover {
    background: #ccc;
   
}


.card.card-img-holder .card-img-absolute {
    position: absolute;
    top: 0;
    right: 0;
    height: 100%;
}

img {
    vertical-align: middle;
    border-style: none;
}

.font-weight-normal {
    font-family: "ubuntu-regular", sans-serif;
}

.font-weight-normal {
    font-weight: 400 !important;
}

.mb-3, .template-demo .circle-progress-block, .lock-screen .card .card-body img, .lock-screen .card .card-body p, .my-3 {
    margin-bottom: 1rem !important;
}

.float-right {
    float: right !important;
}

h2, .h2 {
    font-size: 1.88rem;
}

.card.card-img-holder {
    position: relative;
}

.stretch-card > .card {
    width: 100%;
    min-width: 100%;
}

.card {
    border: 0;
    background: #fff;
}

.bg-gradient-danger {
    background: -webkit-gradient(linear, left top, right top, from(#ffbf96), to(#fe7096)) !important;
    background: linear-gradient(to right, #ffbf96, #fe7096) !important;
}

.text-white {
    color: #ffffff !important;
}

.card {
    position: relative;
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    -ms-flex-direction: column;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 1px solid rgba(0, 0, 0, 0.125);
    border-radius: 0.3125rem;
    box-shadow: 0 1px 10px rgb(0 0 0 / 24%);
}

a {
    color: #fff;
}

/*.stretch-card {
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-align: stretch;
    -ms-flex-align: stretch;
    align-items: stretch;
    -webkit-box-pack: stretch;
    -ms-flex-pack: stretch;
    justify-content: stretch;
}*/

.grid-margin {
    margin-bottom: 2.5rem;
}

@media (min-width: 768px) {
    .col-md-3, .lightGallery .image-tile {
        -webkit-box-flex: 0;
        -ms-flex: 0 0 25%;
        flex: 0 0 25%;
        max-width: 25%;
    }
}

.stretch-card > .card {
    width: 100%;
    min-width: 100%;
}

.card {
    border: 0;
    background: #fff;
}


.bg-gradient-info {
    background: -webkit-gradient(linear, left top, right top, from(#90caf9), color-stop(99%, #047edf)) !important;
    background: linear-gradient(to right, #90caf9, #047edf 99%) !important;
}

.bg-gradient-success, .datepicker-custom .datepicker.datepicker-inline .datepicker-days .table-condensed tbody tr td.day.today:before {
    background: -webkit-gradient(linear, left top, right top, from(#84d9d2), to(#07cdae)) !important;
    background: linear-gradient(to right, #84d9d2, #07cdae) !important;
}



	</style>



    <script type="text/javascript">
        $(document).ready(function () {
            var setinterval = setInterval("reloadpage()", 120000);
        })

        function reloadpage() {
            location.reload();
        }

    </script>
     <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="DashboardForOffline.aspx">Offline Request</a></li>
                <%-- <li><a href="Proxy.aspx">Genrate Request</a></li>

        <li><a href="ProxyPaxUpdate.aspx">Update Request</a></li>--%>
       <%--<li class="dropdown">
                <a href="#lala" class="dropdown-toggle" data-toggle="dropdown" >
                   Manage Booking
                   <b class="caret"></b>
                </a>
                <ul class="dropdown-menu" role="listbox">
                   <li class="divider"></li>
                   <li><a href="Proxy.aspx" role="option">View Request</a></li>
                   <li class="divider"></li>
                   <li><a href="ProxyPaxUpdate.aspx" role="option">Update Request</a></li>
                </ul>
             </li>--%>

    </ol>
    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
                <div class="page-header" style="background: none; box-shadow: none;">
                    <h2 class="page-title">
                        <span class="page-title-icon bg-gradient-primary text-white mr-2">
                             <li class="dropdown">
                <a href="#lala" class="dropdown-toggle" style="color:#23527c;" data-toggle="dropdown" >
                   Manage Booking
                   <b class="caret"></b>
                </a>
                <ul class="dropdown-menu" role="listbox">
                   <li><a href="Proxy.aspx" role="option">Genrate Request</a></li>
                   <li class="divider"></li>
                   <li><a href="ProxyPaxUpdate.aspx" role="option">Update Request</a></li>
                </ul>
             </li>
                        </span> 
                       

                    </h2>
                   
                    

                </div>
                <div class="row">

                    <div class="col-md-3 stretch-card grid-margin">
                        <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=open">
                            <div class="card bg-gradient-danger card-img-holder text-white">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Open<i class="mdi mdi-chart-line mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="open" runat="server" Text=""></asp:Label></h2>
                                </div>

                            </div>
                        </a>
                    </div>

                    <div class="col-md-3 stretch-card grid-margin">
                        <div class="card bg-gradient-info card-img-holder text-white">
                            <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=quote">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Quote<i class="mdi mdi-bookmark-outline mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="quote" runat="server" Text=""></asp:Label></h2>

                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-md-3 stretch-card grid-margin">
                        <div class="card bg-gradient-success card-img-holder text-white">
                            <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=quoteAccepted">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Quote Accepted<i class="mdi mdi-diamond mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="quoteAccepted" runat="server" Text=""></asp:Label></h2>
                                    <%--<h6 class="card-text">Increased by 5%</h6>--%>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-md-3 stretch-card grid-margin">
                        <div class="card bg-gradient-success card-img-holder text-white">
                            <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=quoteRejected">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Quote Rejected<i class="mdi mdi-diamond mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="quoteRejected" runat="server" Text=""></asp:Label></h2>
                                    <%-- <h6 class="card-text">Increased by 5%</h6>--%>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-md-3 stretch-card grid-margin">
                        <div class="card bg-gradient-success card-img-holder text-white">
                            <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=namegiven">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Names Given<i class="mdi mdi-diamond mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="NameGiven" runat="server" Text=""></asp:Label></h2>
                                    <%-- <h6 class="card-text">Increased by 5%</h6>--%>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-md-3 stretch-card grid-margin">
                        <div class="card bg-gradient-success card-img-holder text-white">
                            <a href="/Report/Proxy/ProxyPaxUpdate.aspx?status=cancelled">
                                <div class="card-body">
                                    <img src="https://www.bootstrapdash.com/demo/purple/jquery/template/assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                                    <h4 class="font-weight-normal mb-3">Request Cancelled<i class="mdi mdi-diamond mdi-24px float-right"></i>
                                    </h4>
                                    <h2>
                                        <asp:Label ID="RquestCancelled" runat="server" Text=""></asp:Label></h2>
                                    <%-- <h6 class="card-text">Increased by 5%</h6>--%>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>

