<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="GroupBookingLogin.aspx.vb" Inherits="Login" %>

<%@ Register Src="~/UserControl/GroupUserControl/LoginControl.ascx" TagPrefix="UC1" TagName="Login" %>
<%@ Register Src="~/UserControl/IssueTrack.ascx" TagName="Holiday" TagPrefix="ucholiday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table { width:100%;
        }

    </style>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
<%--<script language="JavaScript" type="text/javascript">
    $(document).ready(function() {
        $(".totoPopup1").click(function() {
            $(".toPopup1").fadeIn();
        });
        $("#CANCEL1").click(function() {
            $(".toPopup1").fadeOut();
        });
    });

    </script>--%>
    <div class="large-8 medium-8 small-12 large-push-2 medium-push-2">
        <div class="w100"><UC1:Login ID="uc" runat="server"  CssClass="w100" /></div>

        
        <div class="clear">
        </div>
        <%--<div>
            <img src="images/new-banner.jpg" style="width: 606px;" />
        </div>--%>
        <div class="clear">
        </div>
        <div class="content_section_left" style="display:none">
            <div class="product_box">
                <div class="totoPopup">
                    <a href="Adds/Offers/ViewOffers.aspx?imgname=homeb1" target="_blank" id="ancHomeBott1">
                        <img src="" id="imgHomeb1" />
                    </a>
                </div>
                <div class="clear1">
                </div>
                <%--<div class="protxt">
                    <span id="spnHomeb1">Tashkent, the pearl of East</span>
                    <div id="divhomebdur1">
                        03 Nights 04 Days@36,499/- PP</div>
                </div>--%>
            </div>
            <%--<div class="product_box">
                <div>
                    <a target="_blank" href="Adds/Offers/ViewOffers.aspx?imgname=homeb2" id="ancHomeBott2">
                        <img src="" alt="" id="imgHomeb2" />
                    </a>
                </div>
                <div class="clear1">
                </div>
                <div class="protxt">
                    <span id="spnHomeb2">Enchanting Kerala</span>
                    <div id="divhomebdur2">
                        05 Nights 06 Days@15,499/-</div>
                </div>
            </div>--%>
            <div class="product_box">
                <%--<div>
                    <a target="_blank" href="Adds/Offers/ViewOffers.aspx?imgname=homeb3" id="ancHomeBott3">
                        <img src="" id="imgHomeb3" />
                    </a>
                </div>--%>
                <div class="clear1">
                </div>
                <%--<div class="protxt">
                    <span id="spnHomeb3">Singapore With Cruise</span>
                    <div id="divhomebdur3">
                        05 Nights 06 Days@63,999/-</div>
                </div>--%>
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
  <%--  <div id="banner">
        <a target="_blank" href="Adds/Offers/ViewOffers.aspx?imgname=homer" id="ancHomer">
            <img src="" id="imgHomeR1" />
        </a>
    </div>

    <script src="Scripts/SetBanners.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#slds").delay(1000).slideDown(1000);
            $("#updo").click(function() {
                $("#updo").hide();
                $("#downd").css("display", "block");
                $("#slds").slideUp(1000);
            });
            $("#downd").click(function() {
                $("#downd").hide();
                $("#updo").css("display", "block");
                $("#slds").slideDown(1000);
            });
        });
    </script>--%>

   <%-- <div style="position: fixed; top: 0; left: 0; z-index: 999; width: 100%;">
        <div id="slds" style="width: 100%; padding: 20px 0; position: relative; box-shadow: 0px 0px 15px #272727;
            background: #f9f9f9; border-bottom: 2px solid #333333; display: none;">
            <div style="float: left; width: 80px; margin-left: 100px;">
                <img src="Images/irctclogomain.png" style="width: 80px;" />
            </div>
            <div style="margin-left: 80px; float: left;">
                <p style="font-weight: bold;">
                    Dear Partners, Look N Book is the first company to have successfully processed New
                    Rail Id’s as per IRCTC New Policies.</p>
                <ul>
                    <span style="color: #2d5a85; font-weight: bold; position: relative; left: -20px;">For
                        new rail id please write to:</span>
                    <li style='clear: both; list-style-type: none; height: 1px;'></li>
                    <li style='float: left; width: 50%;'>rail1@RWT.com : Jasmine Pathania</li>
                    <li style='float: left; width: 50%;'>rail2@RWT.com : Sheetal Chauhan</li>
                    <li style='clear: both; list-style-type: none; height: 1px;'></li>
                    <li style='float: left; width: 50%;'>rail3@RWT.com : Neha Rawat</li>
                    <li style='float: left; width: 50%;'>rail7@RWT.com : Neha Prasad</li>
                </ul>
                <div style='clear: both;'>
                </div>
                <h3 style="color: #2d5a85;">
                    LooknBook Rail helpline number for new rail id registration : 011-43044304
                </h3>
            </div>
            <div class="clear">
            </div>
        </div>
        <div style="width: 84px; margin: auto; cursor: pointer;">
            <img src="images/up.png" id="updo" title="Click to Hide" alt="Click to Hide" />
            <img src="images/down.png" id="downd" title="Click to show Looknbook Rail Helpline Details"
                alt="Click to show Looknbook Rail Helpline Details" style="display: none;" />
            <div class="clear">
            </div>
        </div>
    </div>--%>
     <div class="toPopup1" style="display:none;">
        <div style="width: 32%; padding:1%; margin: 50px auto 0; background:#f9f9f9;">
            <div style="font-weight:bold;">
                Timings 10 AM to 6 PM, Monday to Saturday. Issues Reported after 6 PM and on Sunday will be addressed on the following day.
            </div>
            <div>
                <ucholiday:Holiday ID="uc_holiday" runat="server" />
            </div>
            </div>
    </div>
    <%--<div class="totoPopup" style="position: fixed; top: 180px; background:#fff; padding:10px; cursor:pointer; left: 0px; width:108px; font-weight:bold; box-shadow:1px 1px 5px #333;">
        <div style="width:100%;"><img src="images/issuei.jpg" style="width:100%;" /></div>
        <div style="width:94%; padding:3%; font-size:18px; line-height:30px; color:#555;">Report an Issue and Get a Quick Response</div>
    </div>--%>
</asp:Content>
