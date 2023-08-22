<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDeatils.aspx.cs" Inherits="Report_OrderDeatils" MasterPageFile="~/MasterPageForDash.master" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
<%@ Register Src="~/Utilities/User_Control/PnrSummaryIntl.ascx" TagPrefix="uc1" TagName="PnrSummaryIntl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/CSS/PopupStyle.css?V=1")%>" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/Scripts/ReissueRefund.js?v=1")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PopupScript.js?V=1")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScriptsPNRCancellation/CancelPNR.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>

    <link type="text/css" href="<%=ResolveUrl("~/Content/Popup.css")%>" rel="stylesheet" />
    <link type="text/css" href="<%=ResolveUrl("~/Content/CancelPNR.css")%>" rel="stylesheet" />
    <link href="Advance_CSS/css/Dash.css" rel="stylesheet" />
    <style type="text/css">
        .nav, .navbar-nav {
            margin-top:5px !important;
        }
        .greyLter {
    color: #30a500 !important;
}

        .nav > li > a:hover, .nav > li > a:focus {
    text-decoration: none;
    background-color: none !important;
    border-radius: 4px;
}


        .user-menu-link {
            color: #000 !important;
        }

         .user-menu-link:hover {
             background-color:none !important;
            color: #d6d6d6 !important;
        }
  
  
 
        .fb {
    font-weight: normal;
    font-style: normal;
}
        .ico18 {
    font-size: 14px !important;
}
        .posRel {
            position: relative;
        }

        .brRadius5 {
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }

        .fl {
            float: left;
        }

        .whiteBg {
            background: #fff;
        }

        .borderAll {
            border: 1px solid #e6e6e6;
        }

        .width100 {
            width: 100%;
        }

        .padTB10 {
            padding-top: 10px;
            padding-bottom: 10px;
        }

        .posAbs {
            position: absolute;
        }

        .bkHalfCircleTop {
            width: 20px;
            height: 10px;
            border-bottom-left-radius: 100px;
            border-bottom-right-radius: 100px;
            border: 1px solid rgba(0,0,0,0.125);
            border-top-color: #fff;
        }

        .bkHalfCirctop {
            top: -1px;
            right: 127px;
        }

        .bkHalfCircle {
            width: 20px;
            height: 10px;
            border-top-left-radius: 100px;
            border-top-right-radius: 100px;
            border: 1px solid rgba(0,0,0,0.125);
            border-bottom-color: #fff;
        }

        .bkHalfCircbot {
            bottom: -1px;
            right: 127px;
        }

        .posAbs {
            position: absolute;
        }

        .padL20 {
    padding-left: 20px;
}

        .grey {
    color: #666;
}
.db {
    display: block;
}

.ico20 {
    font-size: 20px;
}
.padB10 {
    padding-bottom: 10px;
}
.padT5 {
    padding-top: 5px;
}

.padT20 {
    padding-top: 20px;
}
.padLR20 {
    padding-left: 20px;
    padding-right: 20px;
}

.backgroundLn:before {
    border-top: 2px dashed #dfdfdf;
    content: "";
    margin: 0 auto;
    position: absolute;
    top: 3px;
    left: 0;
    right: 0;
    bottom: 0;
    width: 98%;
    z-index: -1;
}

.fl {
    float: left;
}
.oval-2 {
    width: 7px;
    height: 7px;
    background-color: #366db0;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
}

.posRel {
    position: relative;
}
.marginT10 {
    margin-bottom: 10px;
}
.crdShdw {
    -webkit-box-shadow: 0 1px 10px rgb(0 0 0 / 24%);
    -moz-box-shadow: 0 1px 10px rgba(0,0,0,0.24);
    box-shadow: 0 1px 10px rgb(0 0 0 / 24%);
}
.brRadius5 {
    border-radius: 5px;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
}

.width100 {
    width: 100%;
}

.borderBtm {
    border-bottom: 1px solid #e6e6e6;
 
    color: #333;
    border-radius: 5px 5px 0px 0px;
}

b, strong {
    font-weight: 500;
    color: #000;
}

.padTB10 {
    padding-top: 10px;
    padding-bottom: 10px;
}

        .pad10 {
            padding:10px;
        }
    </style>

    <style type="text/css">
        .navbar-nav > li > a {
            padding-top: 0px !important;
            padding-bottom: 0px !important;
            padding: 0px !important;
            padding-top: 0px !important;
            padding-bottom: 0px !important;
            margin-right: 28px;
        }

        .nav > li > a:hover, .nav > li > a:focus {
            text-decoration: none;
            background-color: none !important;
            border-radius: 0px !important;
        }

        .dropdown-item {
            color: #6f6f6f;
            line-height: 32px;
        }

        label {
            font-size: 13px;
            /*text-transform: uppercase;*/
            font-weight: 400;

        }
    </style>

    <style type="text/css">
        p {
            margin: 0px 0 20px !important;
        }

        @media print {



            .pri font, .pri {
                color: #fff !important;
            }

            #divprint {
                width: 100% !important;
            }

            .pri2 {
                background-color: #f1f1f1 !important;
                -webkit-print-color-adjust: exact;
            }
        }

        .fade.in {
            background: #0000006b;
        }


        tbody > tr > th {
            padding: 3px;
            border-top: 1px solid #ddd;
        }

        label {
            display: inline-block !important;
            max-width: 100% !important;
            margin-bottom: 5px !important;
            color: #6f6f6f !important;
        }

        .card-main {
            padding: 7px !important;
        }

        .panel {
            margin-bottom: 12px !important;
        }

        h3 {
            line-height: 0px !important;
            color: #333 !important;
        }
    </style>
    <style type="text/css">
        @media print {
            body * {
                visibility: hidden;
            }

            #divprint, #divprint * {
                visibility: visible;
            }

            #divprint {
                position: absolute;
                left: 0;
                top: 0;
            }
        }

        .style1 {
            height: 14px;
        }

        .style2 {
            width: 40%;
        }

        * {
            box-sizing: border-box;
        }


        .share {
            position: fixed;
            left: 0;
            top: 50%;
            list-style-type: none;
            margin: 0;
            padding: 0;
            -moz-transform: translateY(-50%);
            -ms-transform: translateY(-50%);
            -webkit-transform: translateY(-50%);
            transform: translateY(-50%);
            border-radius: 0px 20px 20px 0px;
            box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.8);
            background: #eee;
        }

            .share li {
                position: relative;
            }

                .share li:nth-of-type(1) .social-link,
                .share li:nth-of-type(1) .social-link:hover {
                    background: #3e4b67 url("https://cdn3.iconfinder.com/data/icons/flat-design-spreadsheet-set-4/24/export-to-word-512.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-radius: 0px 20px 0px 0px;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(1) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(1) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #3B5998;
                }

                .share li:nth-of-type(2) .social-link,
                .share li:nth-of-type(2) .social-link:hover {
                    background: #3e4b67 url("https://cdn4.iconfinder.com/data/icons/web-ui-color/128/Mail-512.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(2) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #0065d9;
                }

                .share li:nth-of-type(2) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #4099FF;
                }

                .share li:nth-of-type(3) .social-link,
                .share li:nth-of-type(3) .social-link:hover {
                    background: #3e4b67 url("/Images/icons/Due.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(3) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(3) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #3B5998;
                }

                .share li:nth-of-type(4) .social-link,
                .share li:nth-of-type(4) .social-link:hover {
                    background: #3e4b67 url("/Images/printericon.jpg") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(4) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(4) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #3B5998;
                }

                /*======================*/
                .share li:nth-of-type(5) .social-link,
                .share li:nth-of-type(5) .social-link:hover {
                    background: #3e4b67 url("/Images/icons/Due.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(5) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(5) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #3B5998;
                }
                /*======================*/
                .share li:nth-of-type(6) .social-link,
                .share li:nth-of-type(6) .social-link:hover {
                    background: #3e4b67 url("/Images/pdfimage.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-bottom: 1px solid #7b7b7b;
                }

                .share li:nth-of-type(6) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(6) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #3B5998;
                }

                .share li:nth-of-type(7) .social-link,
                .share li:nth-of-type(7) .social-link:hover {
                    background: #3e4b67 url("https://cdn.sunlife.com/static/ca/tools2018/net_worth/common/images/results-icon.png") 50% 50% no-repeat;
                    background-size: 25px auto;
                    border-radius: 0px 0px 20px 0px;
                }

                .share li:nth-of-type(7) .nav-label {
                    -moz-transition: background 0.4s ease, -moz-transform 0.4s ease 0.1s;
                    -o-transition: background 0.4s ease, -o-transform 0.4s ease 0.1s;
                    -webkit-transition: background 0.4s ease, -webkit-transform 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: background 0.4s ease, transform 0.4s ease 0.1s;
                    background: #1e2e4f;
                }

                .share li:nth-of-type(7) .social-link:hover .nav-label {
                    -moz-transition: -moz-transform 0.4s ease, background 0.4s ease 0.1s;
                    -o-transition: -o-transform 0.4s ease, background 0.4s ease 0.1s;
                    -webkit-transition: -webkit-transform 0.4s ease, background 0.4s ease;
                    -webkit-transition-delay: 0s, 0.1s;
                    transition: transform 0.4s ease, background 0.4s ease 0.1s;
                    background: #eee;
                    box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.8);
                    border-radius: 5px;
                }

                .share li .social-link {
                    padding: 0;
                    display: block;
                    cursor: pointer;
                    width: 60px;
                    height: 60px;
                    padding: 15px 20px;
                }

                    .share li .social-link .nav-label {
                        font-family: sans-serif;
                        font-size: 14px;
                        color: white;
                        display: block;
                        height: 60px;
                        position: absolute;
                        top: 0px;
                        top: 0rem;
                        margin-left: 40px;
                        line-height: 64px;
                        padding: 0 20px;
                        white-space: nowrap;
                        z-index: 4;
                        -moz-transition: -moz-transform 0.4s ease;
                        -o-transition: -o-transform 0.4s ease;
                        -webkit-transition: -webkit-transform 0.4s ease;
                        transition: transform 0.4s ease;
                        -moz-transform-origin: left 50%;
                        -ms-transform-origin: left 50%;
                        -webkit-transform-origin: left 50%;
                        transform-origin: left 50%;
                        -moz-transform: rotateY(-90deg);
                        -webkit-transform: rotateY(-90deg);
                        transform: rotateY(-90deg);
                    }

                        .share li .social-link .nav-label span {
                            -moz-transform-origin: left 50%;
                            -ms-transform-origin: left 50%;
                            -webkit-transform-origin: left 50%;
                            transform-origin: left 50%;
                            -moz-transform: rotateY(-90deg);
                            -webkit-transform: rotateY(-90deg);
                            transform: rotateY(-90deg);
                        }

                    .share li .social-link:hover .nav-label,
                    .share li .social-link:hover .nav-label span {
                        -moz-transform: rotateY(0);
                        -webkit-transform: rotateY(0);
                        transform: rotateY(0);
                    }
    </style>

    <style type="text/css">
        th, td {
            white-space: nowrap;
            padding: 1px;
            text-align: center;
        }

        .emlbtn {
            color: #fff;
            background-color: #3bb733;
            border-color: #2e6da4;
            display: inline-block;
            margin-bottom: 0;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            background-image: none;
            border: 1px solid transparent;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            border-radius: 4px;
        }
    </style>


    <style type="text/css">
        a, p, li, span {
            font-size: 12.5px !important;
        }
    </style>

    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function SendMail() {
            if ($("[id$=emailtxtbox]").val() == "" || $("[id$=emailtxtbox]").val() == " ") {
                alert("Please Provide valid emailID.")
                return false;
            }
            var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            var emailid = document.getElementById("emailtxtbox").value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Your email address seems incorrect. Please try again.");
                document.getElementById("emailtxtbox").focus();
                return false;
            }
        }
    </script>


    <style type="text/css">
        .breadcrumb.bc2x {
            font-size: 1em;
            padding: 1px 2px;
        }

        .breadcrumb li {
            display: inline-block;
            margin-bottom: 0.2em;
        }

            .breadcrumb li a {
                background-color: #006a97;
                background: #1c2d43;
                box-sizing: border-box;
                color: #fff;
                display: block;
                max-height: 2em;
                padding: 0.5em 2em 0.5em 1.5em;
                position: relative;
                text-decoration: none;
                transition: 0.25s;
            }

                .breadcrumb li a:before {
                    border-top: 1em solid transparent;
                    border-bottom: 1em solid transparent;
                    border-left: 1em solid #fff;
                    content: "";
                    position: absolute;
                    top: 0;
                    right: -1.25em;
                    z-index: 1;
                }

                .breadcrumb li a:after {
                    border-top: 1em solid transparent;
                    border-bottom: 1em solid transparent;
                    border-left: 1em solid #1c2d43;
                    content: "";
                    position: absolute;
                    top: 0;
                    right: -1em;
                    transition: 0.25s;
                    z-index: 1;
                }

                .breadcrumb li a:hover:after {
                    border-left-color: #16a00d;
                }

            .breadcrumb li:first-child a {
                background-color: #16a00d;
                pointer-events: none;
            }

                .breadcrumb li:first-child a:after {
                    border-left-color: #16a00d;
                }
    </style>

    <%--    mobile and tablet css--%>
    <style type="text/css">
        @media only screen and (max-width:650px) {
            .card-main {
                width: 566px;
            }

            .sidebar-wrapper {
                width: 129px;
            }

            [class*="col-"] {
                width: 100%;
            }
        }
    </style>

    <%--<ol class="breadcrumb-arrow">

        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">My Bookings</a></li>

        <li><a href="#">Booking ID-<asp:PlaceHolder ID="BkId" runat="server"></asp:PlaceHolder>
        </a></li>


    </ol>--%>



       <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="/Report/TicketReport.aspx">My Bookings</a></li>
        <li><a href="#"><asp:PlaceHolder ID="BkId" runat="server"></asp:PlaceHolder></a></li>
           
    </ol>



              <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                
                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb"><i class="icofont-airplane icofont-rotate-90"></i>  Booking Information : <asp:PlaceHolder ID="CartInformation" runat="server"></asp:PlaceHolder></span>
                                        <div class="padLR10 padT5 ico18 quicks fb" style="cursor: pointer; float: right; margin-top: -4px;">
                                            <ul class="nav navbar-nav">

                    <li class="user-menu__item"></li>
                    <li class="user-menu__item">
                        <a class="user-menu-link" href="PnrSummaryIntl.aspx?OrderId=<%=passorderid%>">
                            <i class="icofont-eye"></i> View Ticket </a>
                    </li>

                    <li class="user-menu__item">
                        <a class="user-menu-link" href='Accounts/IntInvoiceDetails.aspx?OrderId=<%=passorderid%>' target="_blank"><i class="icofont-copy-invert"></i> Invoice </a>

                    </li>




                    <li class="user-menu__item" style="display:none;">
                        <a class="user-menu-link dropdown show">
                            <a class="dropright user-menu-link" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">More Options<i class="icofont-caret-down"></i>
                            </a>

                            <div class="dropdown-menu" aria-labelledby="dropdownMenuLink" style="padding: 9px; position: absolute; left: -83px; top: 26px;">
                                <div class="sidebar-submenu" style="overflow: hidden; display: block;">
                                    <ul>
                                        <li><a class="dropdown-item" href="PnrSummaryIntl.aspx?OrderId=<%=passorderid%>&exword=true"><i class="icofont-file-word"></i> Export To Word</a></li>
                                        <li><a class="dropdown-item" href='PnrSummaryIntl.aspx?OrderId=<%=passorderid%>&pdf=true'><i class="icofont-file-pdf"></i> Export To Pdf</a></li>
                                        <li><a class="dropdown-item" data-toggle="modal" data-target="#myModal" href="#"><i class="icofont-send-mail"></i> Send Mail</a></li>
                                        <li><a class="dropdown-item" id="SendSms" onclick="SendSMS()" data-toggle="modal" data-target="#smsModal" href="#"><i class="icofont-ui-messaging"></i> Send SMS</a></li>

                                        <li><a class="dropdown-item" href="#"><i class="icofont-brand-whatsapp"></i> Whatsapp</a></li>
                                    </ul>
                                </div>
                            </div>



                            <!-- Modal email -->

                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                        <div class="modal-body">
                                            <h4 style="text-align: center">Send Invoice to Email Address</h4>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="font-size: small">Email address</label>
                                                <%--                                        <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email" />--%>
                                                <asp:TextBox ID="emailtxtbox" runat="server" placeholder="Enter Email"></asp:TextBox>
                                                <small id="emailHelp" class="form-text text-muted">Invoice will be send provided email address.</small>
                                            </div>

                                            <asp:Button ID="sendmail" runat="server" Text="Send" CssClass="emlbtn" OnClientClick="return SendMail();" OnClick="SendEmail_Click" />
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <!-- Modal email end-->
                            <!-- Modal sms -->
                            <div class="modal fade" id="smsModal" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                        <div class="modal-body">
                                            <h4 style="text-align: center">Send Invoice Details By SMS</h4>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="font-size: small">Mobile Number</label>
                                                <input type="tel" class="form-control" id="exampleInputSMS" aria-describedby="smsHelp" placeholder="Enter Mobile Number" />
                                                <small id="smsHelp" class="form-text text-muted">Invoice details will be send to provided Mobile number</small>
                                            </div>

                                            <button class="emlbtn" type="submit">Send</button>
                                            <%--                                    <asp:Button ID="SendSMS" runat="server" Text="Send" CssClass="emlbtn" type="button" OnClick="SendSMS_Click" />--%>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <!-- Modal sms end -->
                        </a>
                    </li>


                </ul>

                                        </div>
                                    </div>
                                </div>
                  <div class="pad10">
      <div class="inner-box" style="border: 1px solid #ccc; padding: 1px; border-radius: 5px; margin-bottom: 0px;">

            <div class="card-body report">

                <div class="row">
                    <div class="col-md-4">
                        <asp:PlaceHolder ID="BookingId" runat="server"></asp:PlaceHolder>

                    </div>

                    <div class="col-md-4">
                        <asp:PlaceHolder ID="Amount" runat="server"></asp:PlaceHolder>

                    </div>

                    <div class="col-md-4">
                        <asp:PlaceHolder ID="Status" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <br />
                <div class="row">

                    <%--<div class="col-md-4">
                        <label>Order Type : Air</label>
                    </div>
                    <div class="col-md-4">
                        <label>Channel Type : Desktop</label>
                    </div>--%>
                    <div class="col-md-4">
                        <asp:PlaceHolder ID="CreateDate" runat="server"></asp:PlaceHolder>

                    </div>

                    <div class="col-md-4">
                        <asp:PlaceHolder ID="AgentName" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-md-4">
                        <label>Flow Type : <strong>Online</strong></label>
                    </div>

                </div>
                <br />
                <div class="row">

                    <%--<div class="col-md-4">
                        <asp:PlaceHolder ID="AgentID" runat="server"></asp:PlaceHolder>

                    </div>--%>

                    <div class="col-md-4">
                        <asp:PlaceHolder ID="BookingDate" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-md-4">
                        <asp:PlaceHolder ID="AgentEmail" runat="server"></asp:PlaceHolder>

                    </div>
                    <div class="col-md-4">
                        <asp:PlaceHolder ID="AgentMobile" runat="server"></asp:PlaceHolder>

                    </div>
                </div>
            </div>


        </div>
                      </div>
        </div>
        
 



  <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                
                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb"><i class="icofont-airplane icofont-rotate-90"></i>  Booking Details</span>
<%--                                        <div class="padLR10 padT5 ico18 quicks fb" style="cursor: pointer; margin-top: 1px; color: #5d5d5d; padding: 5px; border-radius: 5px; float: right; margin-top: -4px; margin-right: 10px;"><i class="icofont-bubble-left"></i><span onclick="history.back(-1)" style="font-size: 15px;">Back to Result</span></div>--%>
                                    </div>
                                </div>


      <div class="pad10">
                    <div class="borderAll posRel whiteBg brRadius5 fl width100 fl padTB10" style="padding-top: 25px;margin-bottom:10px;">
                        <div class="bkHalfCircleTop mobdn bkHalfCirctop posAbs"></div>

                        <div class="col-md-3">
                            <div class="row">
                                <div class="col-md-5">
                            <span class="db padB5">
                                <img alt='Logo Not Found' style='width: 35px; margin-left: 20px;' src='/AirLogo/sm<%=imgurlvc%>.gif' />
                            </span>
                                    </div>
                                <div class="col-md-7">
                            <span class="db greyLt ico12">
                                <asp:PlaceHolder ID="AirLineName" runat="server"></asp:PlaceHolder>
                            </span>
                            <span class="db greyLter padT2 ico11">
                                <asp:PlaceHolder ID="FltNo" runat="server"></asp:PlaceHolder>
                            </span>
                                    </div>
                                </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-xs-4 padL20">
                            <span class="ico20 db padB10">
                                <asp:PlaceHolder ID="DepAirportCode" runat="server"></asp:PlaceHolder>
                            </span>
                            <span class="ico13 db greyLter lh1-2 mobdn">
                                <asp:PlaceHolder ID="DepDateTime" runat="server"></asp:PlaceHolder>
                            </span>
                        </div>

                        <div class="col-md-3 col-sm-4 col-xs-4 mobdn txtCenter padLR20 padT20">
                            <span class="db ico20 backgroundLn">
                                <i class="oval-2 fl"></i>
                               
                                <i class="icofont-airplane icofont-2x icofont-rotate-90 fr ico22 blue icon-flight_line1" style="margin-top: -9px;"></i>

                            </span>
                            </div>

                        <%--<i class="icofont-airplane icofont-rotate-90" style="padding-bottom: 19px;"></i>--%>

                        <div class="col-md-3 col-sm-3 col-xs-4 padL20">
                            <span class="ico20 db padT5 padB10">
                                <asp:PlaceHolder ID="ArrivalDetailsAirport" runat="server"></asp:PlaceHolder>
                            </span>
                            <span class="ico13 db greyLter lh1-2 mobdn">
                                <asp:PlaceHolder ID="ArrivalDateTime" runat="server"></asp:PlaceHolder>
                            </span>
                        </div>


                        <div class="bkHalfCircle bkHalfCircbot mobdn posAbs"></div>
                    </div>
             
              
                    <div class="borderAll posRel whiteBg brRadius5 fl width100 fl padTB10" style="padding-top: 10px;margin-bottom:10px;">
                        <div class="bkHalfCircleTop mobdn bkHalfCirctop posAbs"></div>

                        <div class="tabl-responsive">
                            <table class="table" style="margin-bottom: -10px !important;font-size: 12px;">
                                <thead>
                                    <tr>
                                    <th>Sr No.</th>
                                        <th>Passenger Name</th>
                                        <th>Type</th>
                                        <th>Ticket Number</th>
                                        <th>Ticket Status</th>
                                        <th>Base Fare</th>
                                        </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td> <asp:PlaceHolder ID="Srno" runat="server"></asp:PlaceHolder></td>
                                        <td><asp:PlaceHolder ID="PassengerName" runat="server"></asp:PlaceHolder></td>
                                        <td><asp:PlaceHolder ID="Paxtype" runat="server"></asp:PlaceHolder></td>
                                        <td> <asp:PlaceHolder ID="TicketNo" runat="server"></asp:PlaceHolder></td>
                                        <td> <asp:PlaceHolder ID="PaxStatus" runat="server"></asp:PlaceHolder></td>
                                        <td><asp:PlaceHolder ID="BaseFare" runat="server"></asp:PlaceHolder></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        
                        <div class="bkHalfCircle bkHalfCircbot mobdn posAbs"></div>
                    </div>
                  
          
                   

  
       <div class="borderAll posRel whiteBg brRadius5 fl width100 fl padTB10" style="padding-top: 10px;margin-bottom:10px;">
                        <div class="bkHalfCircleTop mobdn bkHalfCirctop posAbs"></div>
                    <div class="row" style="padding-left: 22px">

                        <div class="col-md-3">
                            <asp:PlaceHolder ID="TotalTax" runat="server"></asp:PlaceHolder>


                        </div>

                        <div class="col-md-3">
                            <asp:PlaceHolder ID="TotalComm" runat="server"></asp:PlaceHolder>

                        </div>
                        <div class="col-md-3">

                            <asp:PlaceHolder ID="NetFare" runat="server"></asp:PlaceHolder>

                        </div>
                        <div class="col-md-3">
                            <asp:PlaceHolder ID="GrossFare" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="row" style="padding-left: 22px">
                        <div class="col-md-3">

                            <asp:PlaceHolder ID="AirLinePNR" runat="server"></asp:PlaceHolder>
                        </div>

                        <div class="col-md-3">
                            <asp:PlaceHolder ID="BookingClass" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-4">
                            <asp:PlaceHolder ID="Refundable" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
           <div class="bkHalfCircle bkHalfCircbot mobdn posAbs"></div>
                </div>
          </div>
          </div>
         
           
    


  
  <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                
                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb"><i class="icofont-airplane icofont-rotate-90"></i>  Payment Process</span>
<%--                                        <div class="padLR10 padT5 ico18 quicks fb" style="cursor: pointer; margin-top: 1px; color: #5d5d5d; padding: 5px; border-radius: 5px; float: right; margin-top: -4px; margin-right: 10px;"><i class="icofont-bubble-left"></i><span onclick="history.back(-1)" style="font-size: 15px;">Back to Result</span></div>--%>
                                    </div>
                                </div>
        <div class="pad10">

            <div class="inner-box">
               
                    <div class="panel" style="overflow-x: auto;border:1px solid #ccc;">
                        <table class="panel-body table ">
                            <tr style="font-size: 12px;">
                                <th>
                                    Sr No.
                                </th>
                                <th>
                                    Date
                                </th>
                                <th>
                                    Booking ID
                                </th>
                                <th>
                                    Flight Type
                                </th>
                                <th style="text-align:right;">
                                    Credit
                                </th>
                                <th style="text-align:right;">
                                    Debit
                                </th>
                                <th style="text-align:right;">
                                    Balance
                                </th>
                                <th>
                                    Status
                                </th>

                            </tr>
                            <tr>
                                <asp:PlaceHolder ID="PaymentProcess" runat="server"></asp:PlaceHolder>
                            </tr>
                        </table>
                    </div>
               
            </div>
        </div>
    </div>

</asp:Content>
