﻿<%@ Page Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="HotelCheckOut.aspx.vb" Inherits="HotelCheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="<%=ResolveUrl("../Scripts/jquery-1.4.4.min.js") %>"></script>
      <script type="text/javascript" src="<%=ResolveUrl("../Scripts/jquery-ui-1.8.8.custom.min.js") %>"></script>
    <link href="css/B2Bhotelengine.css" rel="stylesheet" />
    <script src="<%=ResolveUrl("JS/HotelRefund.js") %>" type="text/javascript"></script>
    <div class="large-10 medium-10 small-10 large-push-1 medium-push-1 clear1">

        <div align="center">
            <asp:Label ID="lblError" runat="server" Width="634px" Font-Bold="True" Font-Size="13px"
                ForeColor="Red" Font-Names="Arial"></asp:Label>
        </div>


        <div class="large-12 medium-12 small-12  bgw padding1">
            <div class="large-9 medium-1 small-1 headbgs" id="abcd">
                <span><i class="fa fa-bed" aria-hidden="true"></i>
                    <asp:Label ID="HtlNameLbl" runat="server"></asp:Label><asp:Label ID="HtlStrImg" runat="server"></asp:Label></span>
                <a href="#abc" style="float: right;"><i class="fa fa-angle-double-down" aria-hidden="true"></i>cancellation policy</a>
            </div>
            <div class="brdr" style="padding: 10px;">

                <div class="large-2 medium-2 small-3 columns">
                    <asp:Image ID="HtlImg" runat="server" class="HtlThumbImage" />
                </div>

                <div class="large-10 medium-10 small-9 columns">

                    <div class="large-2 medium-2 small-2 columns bld">
                        Location:
                    </div>
                    <div class="large-10 medium-10 small-10 columns">
                        <asp:Label ID="HtlLoc" runat="server"></asp:Label>
                    </div>
                     <div class="clear1"></div>
                    <div class="large-2 medium-2 small-2 columns bld">
                        Room Type:
                    </div>
                    <div class="large-10 medium-10 small-10 columns">
                        <asp:Label ID="SuiteNm" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear1"></div>

                    <div class="large-2 medium-2 small-3 columns bld" id="RoomInclusionTDHead" runat="server">
                        Room Included:
                    </div>
                    <div class="large-10 medium-10 small-9 columns" id="RoomInclusionTD" runat="server"></div>
                    <div class="clear1"></div>
                    <div class="large-2 medium-2 small-3 columns bld">
                        Check In:
                    </div>
                    <div class="large-2 medium-2 small-3 columns">
                        <asp:Label ID="htlcheckinlbl" runat="server"></asp:Label>
                    </div>
                    <div class="large-2 medium-2 small-3 columns bld">
                        Check Out:
                    </div>
                    <div class="large-2 medium-2 small-3 columns">
                        <asp:Label ID="htlcheckoutlbl" runat="server"></asp:Label>
                    </div>
                    <div class="large-4 medium-4 small-3 columns">
                        <span class="bld">No of Night:</span><span id="lblNights" runat="server"></span>
                    </div>

                    <div class="clear1"></div>



                    <div class="large-2 medium-2 small-3 columns bld">ROOM:  <span id="htlrmslbl" runat="server"></span></div>
                    <div class="large-2 medium-2 small-3 columns bld">ADULT:  <span id="TotAdt" runat="server"></span></div>
                    <div class="large-2 medium-2 small-3 columns bld">CHILD: <span id="Totchd" runat="server"></span></div>

                    <div class="large-4 medium-4 small-3 columns bld">
                        TOTAL:  <span>INR
                            <asp:Label ID="lblTotalCharge" runat="server"></asp:Label>/- (Inclusive of all taxes)
                             <img src='images/icon.jpg' class="brekups" title='Price Break Up' />
                        </span>
                    </div>

                    <div class="clear"></div>
                    <div class="large-2 medium-2 small-3 columns" style="display: none;" id="pernight" runat="server"></div>


                </div>

                <div class="clear"></div>
            </div>
            <div class="large-9 medium-1 small-1 headbgs">
                <span><i class="fa fa-user-o" aria-hidden="true"></i> Guest Information</span>
            </div>
            <div class="brdr" style="padding: 10px;">

                <div class="large-1 medium-1 small-1 columns passenger">
                    Title:
                </div>
                <div class="large-2 medium-2 small-3 columns">
                    <asp:DropDownList ID="TitleDDL" runat="server">
                        <asp:ListItem Value="Mr">Mr</asp:ListItem>
                        <asp:ListItem Value="Ms">Ms</asp:ListItem>
                        <asp:ListItem Value="Mrs">Mrs</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="large-1 medium-1 small-3 columns large-push-1 medium-push-1 passenger">
                    First Name:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-1 medium-push-1">
                    <asp:TextBox ID="Fname" runat="server" MaxLength="20" onkeypress="return isCharKey(event)"></asp:TextBox>
                </div>
                <div class="large-1 medium-1 small-3 columns large-push-2 medium-push-2 passenger">
                    Last Name:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-2 medium-push-2">
                    <asp:TextBox ID="Lname" runat="server" MaxLength="20" onkeypress="return isCharKey(event)"></asp:TextBox>
                </div>
                <div class="clear"></div>
                 <div id='tblrpt' style="margin: 0 auto;" class="large-12 medium-12 small-12">
                    <asp:Repeater ID="rptItems" runat="server">
                        <ItemTemplate>
                            <div class="large-3 medium-3 small-3 columns" style='color: #083E68;'>
                            <span>Room <asp:Label ID="lblRNO" runat="server" Text='<%#Eval("RoomNO")%>'></asp:Label>&nbsp;&nbsp;</span>
                            <span style=" padding-left: 37px;" >
                                <asp:Label ID="lblPaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>  <asp:Label ID="CAge" runat="server" Text='<%#Eval("ChildAge")%>'></asp:Label>
                            </span>
                            </div>
                             <div class="clear"></div>
                            <div style="padding: 10px; display: block;">

                             <div class="large-1 medium-1 small-1 columns passenge">
                                Title: 
                            </div>
                            <div class="large-2 medium-2 small-3 columns">
                                <asp:DropDownList ID="ATitleDDL" runat="server" CssClass="CheckoutDDL">
                                    <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                    <asp:ListItem Value="Ms">Ms</asp:ListItem>
                                    <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                    <%--<asp:ListItem Value="Dr">Dr</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                                <div class="large-1 medium-1 small-3 columns large-push-1 medium-push-1 passenger">
                                    First Name
                                </div>
                                <div class="large-2 medium-2 small-3 columns large-push-1 medium-push-1">
                                    <asp:TextBox ID="txtFName" runat="server" MaxLength='20' Text="" onkeypress='return isCharKey(event)'
                                        onfocus="focusObjF(this);" onblur="blurObjF(this);" defvalue="First Name" CssClass="psb_dd"></asp:TextBox>
                                </div>
                                <div class="large-1 medium-1 small-3 columns large-push-2 medium-push-2 passenger">
                                    Last Name
                                </div>
                                <div class="large-2 medium-2 small-3 columns large-push-2 medium-push-2">
                                    <asp:TextBox ID="txtLName" runat="server" MaxLength='20' Text="" onkeypress='return isCharKey(event)'
                                        CssClass="psb_dd" onfocus="focusObjL(this);" onblur="blurObjL(this);" defvalue="Last Name"></asp:TextBox>
                                </div>
<div class="clear"></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="clear"></div>
                <div class="large-1 medium-1 small-3 columns hide">
                    Address:
                </div>
                <div class="large-2 medium-2 small-3 columns hide">
                    <asp:TextBox ID="TB_AddLine" runat="server" Text="  " MaxLength="500" onkeypress="return AvoideAnd_singleQuote(event)" />
                </div>
               <div class="large-1 medium-1 small-3 columns large-push-1 medium-push-1 hide">
                    City:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-1 medium-push-1 hide">
                    <asp:TextBox ID="TB_City" runat="server" MaxLength="50" Text=" " onkeypress="return isCharKey(event)" />
                </div>
                <div class="large-1 medium-1 small-3 columns large-push-2 medium-push-2 hide">
                    State:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-2 medium-push-2 hide">
                    <asp:TextBox ID="DDL_State" runat="server" Text=" " MaxLength="20" onkeypress="return isCharKey(event)">                                    
                    </asp:TextBox>
                </div>
                <div class="clear"></div>

                <div class="large-1 medium-1 small-1 columns passenger">
                    Nationality:
                </div>
                <div class="arge-2 medium-2 small-3 columns">
                    <asp:TextBox ID="txtCountry" runat="server" MaxLength="40" Text="India" CssClass="txtBox" onkeypress="return isCharKey(event)" />
                    <input type="hidden" id="CountryCode" name="CountryCode" value="IN" />
                </div>
                <div class="large-1 medium-1 small-3 columns large-push-1 medium-push-1 hide">
                    Pin Code:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-1 medium-push-1 hide">
                    <asp:TextBox ID="TB_PinCode" runat="server" MaxLength="6" onkeypress="return isNumberKey(event)"
                        CssClass="txtBox" Text=" " />
                </div>
                <div class="large-1 medium-1 small-3 columns large-push-1 medium-push-1 passenger">
                    Email:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-1 medium-push-1">
                    <asp:TextBox ID="txt_email" runat="server" CssClass="txtBox" MaxLength="50"></asp:TextBox>
                </div>
                <%--<div class="clear"></div>--%>

                <div class="large-1 medium-1 small-3 columns large-push-2 medium-push-2 passenger">
                    Mobile No:
                </div>
                <div class="large-2 medium-2 small-3 columns large-push-2 medium-push-2">
                    <asp:TextBox ID="txtCIPhoneNo" MaxLength="10" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="clear"></div>

                <div class="clear"></div>
                <div class="large-12 medium-12 small-12">
                    <div class="large-2 medium-2 small-2">  Payment Mode:&nbsp; </div>
                     <div class="large-10 medium-10 small-10">
             <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                 <asp:ListItem Text="Wallet" Selected="True" Value="WL"></asp:ListItem>
                 <%-- <asp:ListItem Text="Payment Gateway" Value="PG"></asp:ListItem>--%>
                 <asp:ListItem Text="Credit Card" Value="creditcard"></asp:ListItem>
                 <asp:ListItem Text="Debit Card" Value="debitcard"></asp:ListItem>
                 <asp:ListItem Text="Net Banking" Value="netbanking"></asp:ListItem>
                   <asp:ListItem Text="Paytm" Value="Paytm"></asp:ListItem> 
               <%--  <asp:ListItem Text="Cash Card" Value="cashcard"></asp:ListItem>--%>
             </asp:RadioButtonList>
                         </div>
                </div>
                <div class="clear"></div>

            </div>

            <div class="large-9 medium-1 small-1 headbgs" id="abc">
                <span><i class="fa fa-plane" aria-hidden="true"></i>term & Conditions</span>
                <a href="#abcd" style="float: right;"><i class="fa fa-angle-double-up" aria-hidden="true"></i>Hotel Details</a>
            </div>
            <div class="brdr" style="padding: 10px;">

                <div class="large-12 medium-12 small-12">

                    <div class="large-9 medium-9 small-12 columns">
                        <asp:CheckBox ID="chkTC" runat="server" Text=" I Understand And Agree With The Hotel Booking Policy, Term and Condition Of RWT" />
                    </div>
                    <div class="large-2 medium-2 small-12 end columns">
                        <%--<asp:Button ID="btnPayment" runat="server" Text="" OnClientClick="return CheckoutValidate();" Class="finalbtn" />--%>
                        <asp:Button ID="btnPayment" runat="server" Text="Submit" Class="buttonfltbk" />
                    </div>
                    <div class="clear"></div>
                    <div id="Canpolicy">
                        <div style="color: #373839; font-style: italic; padding: 0 20px;">
                            <span style='font-size: 13px; font-weight: bold;'>CANCELLATION POLICIES</span>
                            <div class="clear"></div>
                            <asp:Label ID="lblRules" runat="server"></asp:Label>
                             <div class="clear1"></div>
                            
                                <div style='font-size:13px;font-weight: bold;'>HOTEL POLICIES</div>
                            <div>
                                <ul>
                     <li> As per Government regulations, it is mandatory for all guests above 18 years of age to carry a valid photo identity card & address proof at the time of check-in. Please note that failure to abide by this can result with the hotel denying a check-in. Hotels normally do not provide any refund for such cancellations.</li>
                     <li> The standard check-in and check-out times are 12 noon. Early check-in or late check-out is subject to hotel availability and may also be chargeable by the hotel. Any early check-in or late check-out request must be directed to and reconfirmed with the hotel directly.</li>
                     <li> Failure to check-in to the hotel, will attract the full cost of stay or penalty as per the hotel cancellation policy.</li>
                     <li> All additional charges other than the room charges and inclusions as mentioned in the booking voucher are to be borne and paid separately during check-out. Please make sure that you are aware of all such charges that may comes as extras. Some of them can be WiFi costs, Mini Bar, Laundry Expenses, Telephone calls, Room Service, Snacks etc.</li>
                     <li> Any changes or booking modifications are subject to availability and charges may apply as per the hotel policies.</li>
</ul>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="FareBreakups">

            <%-- <tr><td colspan="2" runat="server" id="beforTaxAmt"></td></tr> 
     <tr><td colspan="2" runat="server" id="Taxses"></td></tr> 
     <tr><td colspan="2" runat="server" id="Extracharges"></td></tr>--%>
            <div class="large-6 medium-6 small-12 columns">Total Price : </div>
            <div runat="server" id="lblAmount" class="large-6 medium-6 small-12 columns"></div>
            <div class="large-6 medium-6 small-12 columns">Commision : </div>
            <div runat="server" id="AgtComm" class="large-6 medium-6 small-12 columns"></div>
            <div class="large-6 medium-6 small-12 columns">Markups : </div>
            <div runat="server" id="AgtMrk" class="large-6 medium-6 small-12 columns"></div>
            <div class="large-6 medium-6 small-12 columns">PG. Charge : </div>
            <div runat="server" id="DivPgCharge" class="large-6 medium-6 small-12 columns"></div>
            <div class="large-6 medium-6 small-12 columns">Price To Agent : </div>
            <div runat="server" id="TdNetCost" class="large-6 medium-6 small-12 columns"></div>

            <asp:HiddenField ID="HdnTotalPrice" runat="server" />
            <asp:HiddenField ID="HdnCommision" runat="server" />
            <asp:HiddenField ID="HdnMarkups" runat="server" />
            <asp:HiddenField ID="HdnPgCharge" runat="server" Value="0" />
            <asp:HiddenField ID="HdnPriceToAgent" runat="server" />
            <asp:HiddenField ID="HdnOrignalPriceAgent" runat="server" />
        </div>
        <script type="text/javascript">


            $("#ctl00_ContentPlaceHolder1_rblPaymentMode").click(function () {
                GetPgTransCharge();
            });
            function GetPgTransCharge() {
                //;
                var checked_radio = $("[id*=ctl00_ContentPlaceHolder1_rblPaymentMode] input:checked");
                var PaymentMode = checked_radio.val();
                //var TotalPrice = 0;
                //var Commision = 0;
                //var Markups = 0;
                var TotalPgCharges = 0;
                var PriceToAgent = 0;
                var OrignalPriceAgent = 0;

                //TotalPrice = $("#ctl00_ContentPlaceHolder1_HdnTotalPrice").val();
                //Commision = $("#ctl00_ContentPlaceHolder1_HdnCommision").val();
                //Markups = $("#ctl00_ContentPlaceHolder1_HdnMarkups").val();
                //TotalPgCharges = $("#ctl00_ContentPlaceHolder1_HdnPgCharge").val();
                //PriceToAgent = $("#ctl00_ContentPlaceHolder1_HdnPriceToAgent").val();
                OrignalPriceAgent = $("#ctl00_ContentPlaceHolder1_HdnOrignalPriceAgent").val();

                if (PaymentMode != "WL") {
                    $.ajax({
                        type: "POST",
                        url: "HotelCheckOut.aspx/GetPgChargeByMode",
                        data: '{paymode: "' + PaymentMode + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d != "") {
                                //;
                                if (data.d.indexOf("~") > 0) {
                                    //var res = data.d.split('~');
                                    var PgCharge = data.d.split('~')[0]
                                    var chargeType = data.d.split('~')[1]
                                    if (chargeType == "F") {
                                        //calculate fixed pg charge 
                                        TotalPgCharges = (parseFloat(PgCharge)).toFixed(2);
                                        PriceToAgent = (parseFloat(OrignalPriceAgent) + parseFloat(TotalPgCharges)).toFixed(2);
                                        $('#ctl00_ContentPlaceHolder1_DivPgCharge').html(TotalPgCharges);
                                        $('#ctl00_ContentPlaceHolder1_TdNetCost').html(PriceToAgent);
                                        $('#ctl00_ContentPlaceHolder1_lblTotalCharge').html(PriceToAgent);
                                        $("#ctl00_ContentPlaceHolder1_HdnPriceToAgent").val(PriceToAgent);
                                        $("#ctl00_ContentPlaceHolder1_HdnPgCharge").val(TotalPgCharges);
                                    }
                                    else {
                                        //calculate percentage pg charge  
                                        TotalPgCharges = ((parseFloat(OrignalPriceAgent) * parseFloat(PgCharge)) / 100).toFixed(2);
                                        PriceToAgent = (parseFloat(OrignalPriceAgent) + parseFloat(TotalPgCharges)).toFixed(2);
                                        $('#ctl00_ContentPlaceHolder1_DivPgCharge').html(TotalPgCharges);
                                        $('#ctl00_ContentPlaceHolder1_TdNetCost').html(PriceToAgent);
                                        $('#ctl00_ContentPlaceHolder1_lblTotalCharge').html(PriceToAgent);
                                        $("#ctl00_ContentPlaceHolder1_HdnPriceToAgent").val(PriceToAgent);
                                        $("#ctl00_ContentPlaceHolder1_HdnPgCharge").val(TotalPgCharges);
                                    }
                                }
                            }
                            else {
                                alert("try again");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });

                }
                else {
                    TotalPgCharges = "0";
                    PriceToAgent = (parseFloat(OrignalPriceAgent)).toFixed(2);
                    $('#ctl00_ContentPlaceHolder1_DivPgCharge').html(TotalPgCharges);
                    $('#ctl00_ContentPlaceHolder1_TdNetCost').html(PriceToAgent);
                    $('#ctl00_ContentPlaceHolder1_lblTotalCharge').html(PriceToAgent);
                    $("#ctl00_ContentPlaceHolder1_HdnPriceToAgent").val(PriceToAgent);
                    $("#ctl00_ContentPlaceHolder1_HdnPgCharge").val(TotalPgCharges);

                }
            }

            $(document).ready(function () {
                // Country Search Autocomplet Start
                $("#ctl00_ContentPlaceHolder1_txtCountry").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: UrlBase + "Hotel/HotelSearchs.asmx/HtlMrkupCityCountryList",
                            data: "{ 'city': '', 'Country': '" + request.term + "' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    var result = item.Country;
                                    var hidresult = item.CountryCode;
                                    // var countrycode = item.CountryCode;
                                    return { label: result, value: result, label1: hidresult }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    autoFocus: true,
                    minLength: 2,
                    select: function (event, ui) {
                        $("#ctl00_ContentPlaceHolder1_txtCountry").val(ui.item.label);
                        $("#CountryCode").val(ui.item.label1);
                    }
                });
            });
            // Country Search Autocomplet End
        </script>
    </div>
</asp:Content>
