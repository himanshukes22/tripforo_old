<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PnrSummaryIntl.aspx.vb"
    Inherits="FlightInt_PnrSummaryIntl" EnableViewStateMac="false" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="~/Utilities/User_Control/PnrSummaryIntl.ascx" TagPrefix="uc1" TagName="PnrSummaryIntl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Tripforo : Ticket Detail</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="css/transtour.css" rel="stylesheet" type="text/css" />
    <link href="css/core_style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/itz.css" rel="stylesheet" />
    <link href="../CSS/newcss/main.css" rel="stylesheet" />
    <link href="../CSS/foundation.css" rel="stylesheet" />

        <link rel="stylesheet" href="../Advance_CSS/css/bootstrap.css" />
    <link rel="stylesheet" href="../Advance_CSS/css/styles.css" />

    <script src='../Hotel/JS/jquery-1.3.2.min.js' type='text/javascript'></script>
<%--    <script src='../Hotel/JS/jquery-barcode.js' type='text/javascript'></script>--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
<%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <link href="../icofont/icofont.css" rel="stylesheet" />
    <link href="../icofont/icofont.min.css" rel="stylesheet" />
    <script type="text/javascript">        
        function myPrint() {
            window.print();
        }
    </script>

    <script type="text/javascript" language='javascript'>
        function callprint(strid) {

            //var prtContent11 = document.getElementById("test");

            //var prtContent = document.getElementById(strid);

            var prtContent = $('#' + strid);
            var sst = '<html><head><title>Ticket Details</title><link rel="stylesheet" href="http://RWT.co/CSS/itz.css" type="text/css" media="print"></style></head><body>';

            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');

            WinPrint.document.write('<html><head><title>Ticket Details</title>');

            WinPrint.document.write('</head><body>' + prtContent.html() + '</body></html>');


            ////prtContent11.innerHTML = sst + prtContent.innerHTML + "</body></html>";
            //WinPrint.document.write(prtContent11.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    </script>

    <style type="text/css">
       

        .menu-container {
            visibility: hidden;
            opacity: 0;
        }

            .menu-container.active {
                visibility: visible;
                opacity: 1;
                -webkit-transition: all .2s ease-in-out;
                transition: all .2s ease-in-out;
            }

        .user-menu {
            position: absolute;
            right: -22px;
            background-color: #FFFFFF;
            width: 256px;
            border-radius: 2px;
            border: 1px solid rgba(0, 0, 0, 0.15);
            padding-top: 5px;
            padding-bottom: 5px;
            margin-top: 20px;
        }

            .user-menu .profile-highlight {
                display: -webkit-box;
                display: flex;
                border-bottom: 1px solid #E0E0E0;
                padding: 12px 16px;
                margin-bottom: 6px;
            }

                .user-menu .profile-highlight img {
                    width: 48px;
                    height: 48px;
                    border-radius: 25px;
                    -o-object-fit: cover;
                    object-fit: cover;
                }

                .user-menu .profile-highlight .details {
                    display: -webkit-box;
                    display: flex;
                    -webkit-box-orient: vertical;
                    -webkit-box-direction: normal;
                    flex-direction: column;
                    margin: auto 12px;
                }

                    .user-menu .profile-highlight .details #profile-name {
                        font-weight: 600;
                        font-size: 16px;
                    }

                    .user-menu .profile-highlight .details #profile-footer {
                        font-weight: 300;
                        font-size: 14px;
                        margin-top: 4px;
                    }

            .user-menu .footer {
                border-top: 1px solid #E0E0E0;
                padding-top: 6px;
                margin-top: 6px;
            }

                .user-menu .footer .user-menu-link {
                    font-size: 13px;
                }

            .user-menu .user-menu-link {
                display: -webkit-box;
                display: flex;
                text-decoration: none;
                color: #333333;
                font-weight: 400;
                font-size: 14px;
                padding: 12px 16px;
            }

                .user-menu .user-menu-link div {
                    margin: auto 10px;
                }

                .user-menu .user-menu-link:hover {
                    background-color: #F5F5F5;
                    color: #333333;
                }

            .user-menu:before {
                position: absolute;
                top: -16px;
                left: 120px;
                display: inline-block;
                content: "";
                border: 8px solid transparent;
                border-bottom-color: #E0E0E0;
            }

            .user-menu:after {
                position: absolute;
                top: -14px;
                left: 121px;
                display: inline-block;
                content: "";
                border: 7px solid transparent;
                border-bottom-color: #FFFFFF;
            }
    </style>

    <style type="text/css">

        .nav>li>a:focus, .nav>li>a:hover {
    text-decoration: none;
    background-color: none !important;
}
        .nav>li>a:focus, .nav>li>a:hover {
    text-decoration: none;
    background-color: none !important;
}

        p {
            margin: 0px 0 20px !important;
        }

        @media print {

            .pri {
                background-color: <%=BackClor%> !important;
                -webkit-print-color-adjust: exact;
            }

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

   /*     @media mail {
            table {
    border-spacing: 0;
    border-collapse: collapse;
}
        }*/

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



</head>
<body>
    <form id="form1" runat="server">
     <nav class="navbar navbar-default navbar-inverse navbar-theme navbar-theme-abs navbar-theme-transparent navbar-theme-border" id="main-nav" style="display:none !important;">
        <div class="container">
            <div class="navbar-inner nav">




                <div class="navbar-header">
                    <button class="navbar-toggle collapsed" data-target="#navbar-main" data-toggle="collapse" type="button" area-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                
                         <i class="icofont-ui-user icofont-2x"></i>
                    </button>

                     <a id="show-sidebar" class="navbar-brand" href="#" style="z-index: 1011;display:none;">
            <i class="icofont-navigation-menu icofont-2x"></i>
        </a>

                    <a class="navbar-brand" href="/Search.aspx">
                        <img src="../Advance_CSS/Icons/logo(ft).png" alt="Image Alternative text" title="Image Title" />
                    </a>
                </div>
                <div class="collapse navbar-collapse" id="navbar-main">
                    <ul class="nav navbar-nav" style="margin-top:-5px;">

                       

                    

                         <li class="user-menu__item">
        <a class="user-menu-link" href="#">
         Balance: ₹<span id="blc"></span>
        </a>
      </li>

                         <li class="user-menu__item">
        <a class="user-menu-link" href="#">
          Credit Limit:  ₹<span id="CrdLimit"></span>
        </a>
      </li>
      <li class="user-menu__item">
        <a class="user-menu-link" href="#">
          Due: ₹<span id="DueAmt"></span>
        </a>
      </li>
                <li>
                            <asp:LinkButton class="user-menu-link"  Text="Logout" Font-Italic="False" ID="LinkButton1" runat="server" onclick="lnklogout_Click1" causesvalidation="False"
                                                  style="color: red;font-weight:600;"></asp:LinkButton>
                        </li>


                   

                    </ul>

                      <ul class="nav navbar-nav">
                          </ul>
                    
                            <ul class="dropdown-menu" >

                                <div class="pd" runat="server" id="divAgentBalance">
                                <li>
                                    <a href="account.html"></a>
                                </li>
                                <li>
                                    <a href="account-notifications.html"></a>
                                </li>
                                <li>
                                    <a href="account-cards.html"></a>
                                </li>
                                    </div>
                           
                                <li>
                                    
                                </li>

                            </ul>
                    
                </div>
            </div>
        </div>
    </nav>

        <div class="w3-card-2 topnav notranslate" id="topnav" style="display:none;">
            <div style="overflow: auto;">
                <div class="w3-bar w3-left" style="width: 100%; overflow: hidden; height: 30px">
                    <a href="/Search.aspx" class="topnav-icons fa fa-home w3-left w3-bar-item w3-button" style="border-left: none;"></a>

                    <a class="w3-bar-item w3-button" href="/Report/TicketReport.aspx">My Booking</a>
                    <%--<a class="w3-bar-item w3-button" href="/Report/TicketReport.aspx">Dashboard</a>--%>
                    <a class="w3-bar-item w3-button" href="#">About</a>
                    <a class="w3-bar-item w3-button" href="#">Contact</a>
                    
                    <a class="w3-bar-item w3-button" style="float: right;">Welcome : <%=AgencyName%> (<%=AgencyId%>)</a>
                </div>


            </div>
        </div>

    
       

        


        <div style="display:none;">
            <asp:HiddenField id="hdnReferenceNo" runat="server" />
            <ul class="share">
                <!--Facebook-->
                <li>
                    <div class="social-link">
                        <div class="nav-label" style="padding: 14px; width: 500%; height: auto;">
                            <span>
                                <asp:Button ID="btn_exporttoword" runat="server" Text="ExportToWord" CssClass="btn btn-danger"
                                    CausesValidation="False" OnClientClick="return hidecharge();" /></span>
                        </div>
                    </div>
                </li>
                <!--Twitter-->
                <li>
                    <div class="social-link">
                  
                    </div>
                </li>
                <li>
                    <div class="social-link">
                        <div class="nav-label">
                            <div id="div1" runat="server">
                                <div class="row">
                                    <input type="button" class="btn btn-danger" id="BtnShowFare" name="btn_edit" value="Fare Show" onclick="ShowFare();" style="display: none;" />
                                    <input type="button" class="btn btn-danger" id="BtnHideFare" name="btn_edit" value="Fare Hide" onclick="HideFare();" />


                                    <input type="button" class="btn btn-danger" id="btnShowAgency" name="btn_edit" value="Agency Show" onclick="ShowAgency();" style="display: none;" />
                                    <input type="button" class="btn btn-danger" id="btnHideAgency" name="btn_edit" value="Agency Hide" onclick="HideAgency();" />
                                    <asp:Button ID="btn_exporttoecel" runat="server" Text="ExportToExcel" BackColor="#004b91"
                                        Font-Bold="False" ForeColor="White" CausesValidation="False" OnClientClick="return hidecharge();"
                                        Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
            
                <li style="display: none;">
                    <div class="social-link">
                        <div class="nav-label" style="padding: 14px;">
                            <div id="div5" runat="server">
                                <span>

                                    <asp:Button ID="Button1" runat="server" Text="ExportToExcel" BackColor="#004b91"
                                        Font-Bold="False" ForeColor="White" CausesValidation="False" OnClientClick="return hideagencydetail();"
                                        Visible="false" />
                                </span>
                            </div>
                        </div>
                    </div>
                </li>
               <%-- <li>
                    <div class="social-link" id="ExportToPdfFun" title="Download Ticket"></div>
                </li>--%>

                <li>
                    <div class="social-link">
                        <div class="nav-label" style="padding: 14px; width: 500%; height: auto;">
                            <div style="width: 150px;">

                                <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Markup+</button>
                            </div>

                        </div>
                    </div>
                </li>
            </ul>

        </div>

        <div  style="margin: 5px auto; width: 97%; background-color: #FFFFFF; padding: 5px;margin-top: 50px;">
            <div class="large-12 medium-12 small-12">
                 <div style="margin: 5px auto; width: 80%;padding:10px;">

                    <%-- <ul class="nav navbar-nav">
                         <li class="user-menu__item"><a href="../../Search.aspx"><i class="icofont-home"></i> Home</a></li>
                         <li class="user-menu__item"><a href="#" onclick="window.history.go(-1)"><i class="icofont-caret-left"></i> Previous Page</a></li>
                     </ul>--%>

                     <ul class="nav navbar-nav" style="float: right !important;font-size:16px;display:none">

                    <li class="user-menu__item"></li>
                    <li class="user-menu__item">
                        
                    </li>

                    <li class="user-menu__item">
                        <a class="user-menu-link" href="Accounts/IntInvoiceDetails.aspx?OrderId=<%=OrderId %>" target="_blank"><i class="icofont-copy-invert"></i> Invoice </a>

                    </li>




                    <li class="user-menu__item">
                        <a class="user-menu-link dropdown show" style="margin-top: -30px;">
                            </a><a class="dropright user-menu-link" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">More Options<i class="icofont-caret-down"></i>
                            </a>

                            <div class="dropdown-menu" aria-labelledby="dropdownMenuLink" style="padding: 9px; position: absolute; left: -46px; top: 48px;">
                                <div class="sidebar-submenu" style="overflow: hidden; display: block;">
                                    <ul>
                                        <li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#markup"><i class="icofont-file-word"></i>Markup+</a></li>
                                        <li><span class="dropdown-item" id="ExportToPdfFun" style="cursor:pointer;"><i class="icofont-file-pdf"></i> Export To Pdf</span></li>
                                        <li><a class="dropdown-item" data-toggle="modal" data-target="#myModal" href="#"><i class="icofont-send-mail"></i> Send Mail</a></li>
                                        <li style="display:none;"><a class="dropdown-item" id="SendSms" onclick="SendSMS()" data-toggle="modal" data-target="#smsModal" href="#"><i class="icofont-ui-messaging"></i> Send SMS</a></li>

                                        <li style="display:none;"><a class="dropdown-item" href="#"><i class="icofont-brand-whatsapp" ></i> Whatsapp</a></li>
                                    </ul>
                                </div>
                            </div>



                            <!-- Modal email -->

                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">×</button>
                                        </div>
                                        <div class="modal-body">

                                            <h4 style="text-align: center">Send Ticket Copy to Email Address</h4>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="font-size: small">Email address</label>
                                                
                                               <asp:TextBox ID="txt_email" runat="server" CssClass="textboxflight" Style="width: 300px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                                        ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                <small id="emailHelp" class="form-text text-muted">Invoice will be send provided email address.</small>
                                                <asp:Label ID="mailmsg" runat="server"></asp:Label>
                                <div style="text-align: center; color: #EC2F2F">
                                    <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                                        ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                                </div>
                                            </div>

                                             <asp:Button ID="btn" runat="server" OnClientClick="return emailvalidate();" CssClass="btn btn-danger" Text="Send"></asp:Button>
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
                                            <button type="button" class="close" data-dismiss="modal">×</button>
                                        </div>
                                        <div class="modal-body">
                                            <h4 style="text-align: center">Send Invoice Details By SMS</h4>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="font-size: small">Mobile Number</label>
                                                <input type="tel" class="form-control" id="exampleInputSMS" aria-describedby="smsHelp" placeholder="Enter Mobile Number">
                                                <small id="smsHelp" class="form-text text-muted">Invoice details will be send to provided Mobile number</small>
                                            </div>

                                            <button class="emlbtn" type="submit">Send</button>
                                            
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <!-- Modal sms end -->
                        
                    </li>


                </ul>
                 </div>
				 <div id="divtkt" runat="server" style="margin: 5px auto; width: 97%; background-color: #FFFFFF; padding: 5px;margin-top:-44px;">
				 <a style="float:right;margin-right:106px;margin-top:-20px;color:#fff;padding:4px 12px;background:#25b73e;radius:4px;text-decoration:auto;" class="user-menu-link" href='javascript:;' onclick="myPrint()">
                            <i class="icofont-print"></i> Print Ticket </a>
				 </div>
                <div id="divprint" runat="server" style="margin: 5px auto; width: 80%;">
                    <div id="div_mail" runat="server">
                        <div style="clear: both;"></div>

                        <asp:Label ID="LabelTkt" runat="server"></asp:Label>

                        <div style="clear: both;"></div>

                    </div>
                </div>

                <%--<div id="divprint1" runat="server" style="margin: 5px auto; border: 1px #20313f solid; width: 90%; background-color: #FFFFFF; padding: 5px; display: none;">
                    <div id="div_mail">
                        <asp:Label ID="LabelTkt" runat="server"></asp:Label>
                    </div>
                </div>--%>
            </div>
        </div>



        <div class="modal fade" id="markup" role="dialog" style="display:none;">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add Service Charge</h4>
                    </div>
                    <div class="modal-body">
                        <div id="Div_Main" runat="server">
                            <div style="margin: 5px auto; width: 96%; background-color: #FFFFFF; padding: 10px;">
                                <div class="large-12 medium-12 small-12" style="padding: 10px; background-color: #eeeeee;">
                                    <div style="font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; color: #004b91; padding-left: 10px; padding-top: 5px; padding-bottom: 5px;"
                                        id="td_showaddchage">
                                        <input type="button" class="buttonfltbk" id="btn_addcharge" name="btn_addcharge" value="ServiceCharge(+)" style="width: 135px; height: 30px"
                                            onclick="showcharge();" />&nbsp;&nbsp;<br />
                                        <div class="clear"></div>
                                        <br />
                                        <asp:Label ID="TaxNew" runat="server" Text=""></asp:Label>

                                        &nbsp; <%--<span style="font-family: arial, Helvetica, sans-serif; padding: 8px; font-size: 12px; color: #FF3300">
                            <b style="font-family: arial, Helvetica, sans-serif; font-size: 13px; color: #004b91;">Note:</b>&nbsp; We are not
                                        storing any data regarding additional charge into our database.</span>--%>
                                    </div>

                                    <div id="td_servicecharge" style="display: none" class="row">
                                        <div class="large-2 medium-2 small-4 col-md-4">
                                            <label>Select Charge Type</label>
                                            <asp:DropDownList ID="ddl_srvtype" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="TC">Transaction Charge</asp:ListItem>
                                                <asp:ListItem Value="TAX">Tax</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="large-2 medium-2 small-4 col-md-4">
                                            <label>Charge Amount</label>
                                            <a onclick="hidecharge();" href="#">
                                                <div style="font-size: 14px; float: right; color: #424242;">×</div>
                                            </a>
                                            <input type="text" id="txt_srvcharge" name="txt_srvcharge" class="form-control" runat="server" onkeypress="return NumericOnly(event);" />
                                        </div>
                                        <div class="large-2 medium-2 small-12">
                                            <%--<input type="button" class="buttonfltbk" id="btn_edit" name="btn_edit" value="Add Charge" onclick="AdditionalCharge();" style="width: 135px; height: 30px" />--%>
                                            <span class="buttonfltbk" id="btn_edit" onclick="AdditionalCharge();" style="width: 135px; height: 30px">Add Charge</span>

                                        </div>

                                        <div class="col-md-6">
                                            <b>NOTE:</b>&nbsp; Charge amount should be per pax
                                        </div>

                                        <div>
                                            <%--For TC --%>
                                            <input type="hidden" id="hidtcadt" name="hidtcadt" />
                                            <input type="hidden" id="hidtcchd" name="hidtcchd" />

                                            <input type="hidden" id="hidtotadt" name="hidtotadt" />
                                            <input type="hidden" id="hidtotchd" name="hidtotchd" />

                                            <input type="hidden" id="hidgrandtot" name="hidgrandtot" />
                                            <input type="hidden" id="hidfinaltot" name="hidfinaltot" />

                                            <%--For Tax --%>
                                            <input type="hidden" id="hidtaxadt" name="hidtaxadt" />
                                            <input type="hidden" id="hidtaxchd" name="hidtaxchd" />

                                            <input type="hidden" id="hidtaxtotadt" name="hidtaxtotadt" />
                                            <input type="hidden" id="hidtaxtotchd" name="hidtaxtotchd" />

                                            <input type="hidden" id="hidtaxgrandtot" name="hidtaxgrandtot" />
                                            <input type="hidden" id="hidtaxfinaltot" name="hidtaxfinaltot" />

                                            <input type="hidden" id="hedtotInfant" name="hedtotInfant" />
                                            <input type="hidden" id="hedFinalTotal" name="hedFinalTotal" />
                                            <input type="hidden" id="hedFinalTotaltax" name="hedFinalTotaltax" />

                                            <%--Pax Wise--%>

                                            <input type="hidden" id="hidperpaxtc" name="hidperpaxtc" />
                                            <input type="hidden" id="hidperpaxTCtot" name="hidperpaxTCtot" />
                                            <input type="hidden" id="hidperpaxgrandTCtot" name="hidperpaxgrandtot" />

                                            <input type="hidden" id="hidperpaxtax" name="hidperpaxtax" />
                                            <input type="hidden" id="hidperpaxTaxtot" name="hidperpaxTaxtot" />
                                            <input type="hidden" id="hidperpaxgrandTaxtot" name="hidperpaxgrandtot" />


                                        </div>
                                    </div>
                                </div>

                                <div>
                                    <input type="hidden" id="Hidden1" runat="server" name="Hidden1" />
                                </div>
                            </div>

                            <div style="margin: 10px auto; border: 1px #20313f solid; width: 90%; background-color: #FFFFFF; padding: 0px;" id="div2" runat="server">
                                <%--<table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#20313f" style="height: 80px"
                    align="center">
                    <tr>
                        <td colspan="2" style="color: #ffffff; font-size: 12px;">
                            <strong style="padding-left: 10px">Send E-Mail:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #ffffff; font-size: 12px; padding-left: 15px;" valign="top">Email-ID :
                    <asp:TextBox ID="txt_email" Width="350" Height="35" runat="server" CssClass="textboxflight"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                                ErrorMessage="*" ForeColor="#990000" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <br />
                            <div style="text-align: center; color: #EC2F2F">
                                <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                                    ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                            </div>
                        </td>
                        <td style="text-align: left; padding-top: 18px;" width="40%" valign="middle">
                            <asp:Button ID="btn" runat="server" OnClientClick="return emailvalidate();" CssClass="buttonfltbk" Text="Send" Style="width: 135px; height: 30px"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="color: red; font-size: 12px; padding-left: 15px;">
                            <asp:Label ID="mailmsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>--%>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>



        <asp:HiddenField ID="HdnOrderId" runat="server" />
        <asp:HiddenField ID="HdnTrnsId" runat="server" />
        <asp:HiddenField ID="HidDivFare" runat="server" />
        <input type="hidden" id="taxfarenrm" value="10" />
        <input type="hidden" id="tranfarenrm" value="0" />
        <asp:HiddenField ID="basetaxfarenrm" runat="server" Value="0" />


        <%-- <div style="position: fixed;display:none; top: 0%;  width: 100%; height: 100%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000;">
                    Proccessing......<br />
                    <br />
                    <img alt="loading" style="margin-top:200px" src="<%= ResolveUrl("~/images/wait.gif")%>" />
                    <br />
                </div>--%>

        <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
            function emailvalidate() {
                if ($("#txt_email").val() == "" || $("#txt_email").val() == " ") {
                    alert("Please Provide valid emailID.")
                    return false;
                }
                var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                var emailid = document.getElementById("txt_email").value;
                var matchArray = emailid.match(emailPat);
                if (matchArray == null) {
                    alert("Your email address seems incorrect. Please try again.");
                    document.getElementById("txt_email").focus();
                    return false;
                }
            }
        </script>

        <script language="javascript" type="text/javascript">
            function NumericOnly(event) {

                if (event.which != 46 && event.which != 45 && event.which != 46 &&
                    !(event.which >= 48 && event.which <= 57)) {
                    return false;
                }

                //var charCode = (event.keyCode ? event.keyCode : event.which);
                //if (charCode > 31 && (charCode < 48 || charCode > 57))
                //    return false;
                //return true;

            }
            function showcharge() {
                $("#td_servicecharge").show();
                $("#td_showaddchage").hide();
            }
            function hidecharge() {
                debugger;
                $("#td_servicecharge").hide();
                $("#td_showaddchage").show();
                if ($("#HidDivFare").val() == "hide") {
                    HideFare();
                    //ShowFare()
                    //$("#HidDivFare").val("hide");
                }
                else {
                    ShowFare();
                }

            }
            function UpdateCharges() {
                $.ajax({
                    type: "POST",
                    //url: "PnrSummaryIntl.aspx/GetCurrentTime",
                    //data: '{name: "' + $("#txt_srvcharge").val() + '" }',
                    url: "PnrSummaryIntl.aspx/UpdateCharges",
                    data: '{OrderId: "' + $("#HdnOrderId").val() + '",Amount: "' + $("#txt_srvcharge").val() + '",ChargeType: "' + $("#ddl_srvtype").val() + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        //alert(response.d);
                    }
                });
            }
            function OnSuccess(response) {
                location.reload();

                //alert(response.d);        
            }

            function ShowFare() {
                $("#disfareinfoheader").show();
                $("#disfareinfo").show();
                $("#hdpaxinfo").hide();
                $("#TR_FareInformation1").show();
                $("#TR_FareInformation2").show();
                $("#TR_FareInformation3").show();
                $("#TR_FareInformation4").show();
                $("#TR_FareInformation5").show();
                $("#TR_FareInformation6").show();
                $("#TR_FareInformation7").show();
                $("#TR_FareInformation8").show();
                $("#TR_FareInformation9").show();
                $("#TR_FareInformation10").show();
                $("#TR_FareInformation11").show();
                //$("#LabelTkt").show();
                $("#BtnHideFare").show();
                $("#BtnShowFare").hide();

                $("#HidDivFare").val("");

            }
            function HideFare() {
                $("#BtnHideFare").hide();
                $("#BtnShowFare").show();
                $("#disfareinfoheader").hide();
                $("#disfareinfo").hide();
                $("#hdpaxinfo").show();
                $("#HidDivFare").val("hide");
                $("#TR_FareInformation1").hide();
                $("#TR_FareInformation2").hide();
                $("#TR_FareInformation3").hide();
                $("#TR_FareInformation4").hide();
                $("#TR_FareInformation5").hide();
                $("#TR_FareInformation6").hide();
                $("#TR_FareInformation7").hide();
                $("#TR_FareInformation8").hide();
                $("#TR_FareInformation9").hide();
                $("#TR_FareInformation10").hide();
                $("#TR_FareInformation11").hide();
                //$("#	GRAND TOTAL").hide();
                //$("#td_showaddchage").show();
            }
        </script>
        <uc1:PnrSummaryIntl runat="server" ID="PnrSummaryIntl" />
    </form>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/")%>';
        try {
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/GetAgencyBal",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    try {
                        var resBal = 0;
                        var dueAmt = 0;
                        var crdLimit = 0;
                        var cmbBal = data.d

                        if (cmbBal.split('~').length > 0) {
                            resBal = cmbBal.split('~')[0];
                            if (cmbBal.split('~').length > 1) {
                                crdLimit = cmbBal.split('~')[1];
                            }
                            if (cmbBal.split('~').length > 2) {
                                dueAmt = cmbBal.split('~')[2];
                            }
                        }
                        //var resBal = data.d;
                        //<span id="CrdLimit">2000/-</span> Due: <span id="DueAmt">1000/-</span>
                        if (resBal != null && resBal != "") {
                            $("#blc").html(" " + $.trim(resBal.split('.')[0]) + "/-");
                        }
                        if (crdLimit != null && crdLimit != "") {
                            $("#CrdLimit").html(" " + $.trim(crdLimit.split('.')[0]) + "/-");
                        }
                        if (dueAmt != null && dueAmt != "") {
                            $("#DueAmt").html(" " + $.trim(dueAmt.split('.')[0]) + "/-");
                        }
                        // $("#imgLoadLoginVals").hide();
                        $("#blc").show();
                        $("#CrdLimit").show();
                        $("#DueAmt").show();
                        ////$("#DivLoadPBal").hide();                            
                    }
                    catch (err) {
                        alert(err);
                        ////$("#DivLoadPBal").hide();
                        //$("#imgLoadLoginVals").hide();
                        $("#blc").show();
                        $("#CrdLimit").show();
                        $("#DueAmt").show();
                    }
                },
                error: function (e, t, n) {
                    alert(t);
                    ////$("#DivLoadPBal").hide();                                                
                    //$("#imgLoadLoginVals").hide();
                    $("#blc").show();
                    $("#CrdLimit").show();
                    $("#DueAmt").show();
                }
            });
        }
        catch (err) {
            alert(err);
            ////$("#DivLoadPBal").hide();
            // $("#imgLoadLoginVals").hide();
            $("#blc").show();
            $("#CrdLimit").show();
            $("#DueAmt").show();
        }
    </script>
</body>
</html>
