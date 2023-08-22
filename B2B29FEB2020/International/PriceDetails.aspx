<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PriceDetails.aspx.vb" MasterPageFile="~/MasterAfterLogin.master"
    Inherits="FlightIntl_PriceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

    <link href="../Custom_Design/css/skunal.css" rel="stylesheet" />

    <style type="text/css">
          .table {
    display: table;
    text-align: center;
    width: 90%;
    margin: 2% auto 20px;
    border-collapse: separate;
    font-family: 'Roboto', sans-serif;
    font-weight: 400;
  }
  
  .table_row {
    display: table-row;
  }
  
  .theader {
    display: table-row;
  }
  
  .table_header {
display: table-cell;
    border-bottom: #040404 1px solid;
    border-top: #000 1px solid;
    background: #ffffff;
    color: #909090;
    padding-top: 10px;
    padding-bottom: 10px;
    font-weight: 700;
    border-right: 1px solid #000;
  }
  
  .table_header:first-child {
   border-right: #000000 1px solid;
    border-top-left-radius: 5px;
    border-left: #000000 1px solid;
  }
  
  .table_header:last-child {
    border-right: #000 1px solid;
    border-top-right-radius: 5px;
  }
  
  .table_small {
    display: table-cell;
  }
  
  .table_row > .table_small > .table_cell:nth-child(odd) {
    display: none;
    background: #bdbdbd;
    color: #e5e5e5;
    padding-top: 10px;
    padding-bottom: 10px;
  }
  
  .table_row > .table_small > .table_cell {
    padding-top: 3px;
    padding-bottom: 3px;
    color: #5b5b5b;
    border-bottom: #000 1px solid;
    border-right: 1px solid #000;
  }
  
  .table_row > .table_small:first-child > .table_cell {
    border-left: #000 1px solid;
    border-right: #000 1px solid;
  }
  
  .table_row > .table_small:last-child > .table_cell {
    border-right: #000 1px solid;
  }
  
  .table_row:last-child > .table_small:last-child > .table_cell:last-child {
    border-bottom-right-radius: 5px;
  }
  
  .table_row:last-child > .table_small:first-child > .table_cell:last-child {
    border-bottom-left-radius: 0px;
  }
  
  .table_row:nth-child(2n+3) {
    background: #e9e9e9;
  }
  
  @media screen and (max-width: 900px) {
    .table {
      width: 90%
    }
  }
  
  @media screen and (max-width: 650px) {
    .table {
      display: block;
    }
    .table_row:nth-child(2n+3) {
      background: none;
    }
    .theader {
      display: none;
    }
    .table_row > .table_small > .table_cell:nth-child(odd) {
      display: table-cell;
      width: 50%;
    }
    .table_cell {
      display: table-cell;
      width: 50%;
    }
    .table_row {
      display: table;
      width: 100%;
      border-collapse: separate;
      padding-bottom: 20px;
      margin: 5% auto 0;
      text-align: center;
    }
    .table_small {
      display: table-row;
    }
    .table_row > .table_small:first-child > .table_cell:last-child {
      border-left: none;
    }
    .table_row > .table_small > .table_cell:first-child {
      border-left: #ccc 1px solid;
    }
    .table_row > .table_small:first-child > .table_cell:first-child {
      border-top-left-radius: 5px;
      border-top: #ccc 1px solid;
    }
    .table_row > .table_small:first-child > .table_cell:last-child {
      border-top-right-radius: 5px;
      border-top: #ccc 1px solid;
    }
    .table_row > .table_small:last-child > .table_cell:first-child {
      border-right: none;
    }
    .table_row > .table_small > .table_cell:last-child {
      border-right: #ccc 1px solid;
    }
    .table_row > .table_small:last-child > .table_cell:first-child {
      border-bottom-left-radius: 5px;
    }
    .table_row > .table_small:last-child > .table_cell:last-child {
      border-bottom-right-radius: 5px;
    }
  }
    </style>

  


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var rslt = false;
            $("#ctl00_ContentPlaceHolder1_Submit").click(function (e) {
                //e.preventDefault();

                jConfirm('Are you sure!', 'Confirmation', function (r) {

                    if (r) {

                        document.getElementById("div_Submit").style.display = "none";
                        document.getElementById("div_Progress").style.display = "block";
                        rslt = true;

                        $('#ctl00_ContentPlaceHolder1_Submit').unbind('click').click();
                    }
                    else {
                        rslt = false;
                    }

                });

                return rslt;
                //if (confirm("Are you sure!")) {

                //    return true;
                //}
                //else {
                //    return false;
                //}
            });
        });
    </script>

    <%--<div class="w66 mauto boxshadow1">
        <div class="f16 bgf1 bld padding1">
            Itinerary Details</div>
        <hr />
        <div class="padding1 w95 mauto">--%>
    <%--<div id="divFltDtls" runat="server" class="f13">
            </div>
            <div class="clear1">
            </div>
            <div id="divPaxdetails" runat="server" class="f13">
            </div>
            <div class="clear1">
            </div>
            <div class="clear1">
            </div>
            <div id="divFareDtls" runat="server" class="lft w48">
            </div>
            <div id="divFareDtlsR" runat="server" class="rgt w48">
            </div>
            <div class="clear1">
            </div>
            <div class="clear1">
            </div>
            <div class="w10 mauto">
                <asp:Button ID="Submit" runat="server" Text="Book" CssClass="button" OnClientClick="return Validate()" />
            </div>--%>

    <div style="background-color: #eee;">
        <div class="large-10 medium-10 small-12 large-push-1 medium-push-1">
            <div class="large-12 medium-12 small-12">
                <div class="clear">
                </div>
                <div class="clear">
                </div>

                <div class="large-12 medium-12 small-12 bor" id="divFltDtls" runat="server"></div>

                <div class="large-12 medium-12 small-12 bor" id="divPaxdetails" runat="server" style="background-color: #fff; margin-top: 10px;">
                </div>
                 <div class="large-12 medium-12 small-12" id="SeatInformation" runat="server" style="background-color:#fff; margin-top:10px; " >
            
        </div>
                <div class="large-12 medium-12 small-12 bor" id="divFareDtls" runat="server" style="background-color: #fff; margin-top: 10px;"></div>

                <div class="large-12 medium-12 small-12 bor" id="divFareDtlsR" runat="server" style="background-color: #fff; margin-top: 10px;">
                </div>


               



                <div class="large-12 medium-12 small-12 bor" style="background-color: #fff; margin-top: 10px;">
                     <div class="large-12 medium-12 small-12  headbgs fd-h" style="line-height: 28px !important;"><i class="fa fa-inr" aria-hidden="true"></i> Payment Mode:</div>
                 
             <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                 <asp:ListItem Text="Wallet" Selected="True" Value="WL"></asp:ListItem>                                        
                 <%--<asp:ListItem Text="Payment Gateway" Value="PG"></asp:ListItem>
		     <asp:ListItem Text="Cash Card" Value="cashcard"></asp:ListItem>--%>
             <%--    <asp:ListItem Text="Credit Card" Value="creditcard"></asp:ListItem>                                        
                 <asp:ListItem Text="Debit Card" Value="debitcard"></asp:ListItem>
                 <asp:ListItem Text="Net Banking"  Value="netbanking"></asp:ListItem>     
                 <asp:ListItem Text="Paytm" Value="Paytm"></asp:ListItem>
		 <asp:ListItem Text="Amex" Value="AMEX"></asp:ListItem>
		<asp:ListItem Text="Diners" Value="DINR"></asp:ListItem>
                 <asp:ListItem Text="UPI" Value="upi"></asp:ListItem>                           
                 <asp:ListItem Text="Amazon Pay" Value="AMZPAY"></asp:ListItem>
                 <asp:ListItem Text="PhonePe" Value="PHONEPE"></asp:ListItem>--%>
             </asp:RadioButtonList>
                </div>



                <div id="div_Progress" style="display: none">
                    <b>Booking In Progress.</b> Please do not 'refresh' or 'back' button
                <img alt="Processing.." src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                </div>
            
         
        </div>

            
            
        
    <div class="clear"></div>
    <div class="clear"></div>
    <div class="row">
                <div id="div_Submit" class="col-md-12">
                      <div class="col-md-1">
                <asp:Button ID="ButtonHold" class="btn btn-danger" runat="server" Text="Hold" Visible="false" style="border-radius:4px;"/>
                </div>

                    <div class="col-md-8">
                      <asp:Label ID="lblHoldBookingCharge" runat="server" Text="" style="font-weight:600;"></asp:Label>
                        </div>

                     <div class="col-md-2" style="float:right;">
                    <asp:Button ID="Submit" runat="server" Text="Book" CssClass="btn btn-danger" style="border-radius:4px;width: 104px;"/>
                         </div>
                    
    
        <asp:HiddenField ID="HdnTripType" runat="server" />       

    </div>
            </div>
    </div>
    </div>

    <div class="clear"></div>
    <div class="clear"></div>
    <%--<div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>--%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>

    <script type="text/javascript">
        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg

        }
    </script>

    <script type="text/javascript">
        $("#ctl00_ContentPlaceHolder1_rblPaymentMode").click(function () {
            GetPgTransCharge();
        });
        function GetPgTransCharge() {
            var checked_radio = $("[id*=ctl00_ContentPlaceHolder1_rblPaymentMode] input:checked");
            var PaymentMode = checked_radio.val();
            var tripType = $("#ctl00_ContentPlaceHolder1_HdnTripType").val();
            var NetFareOutBound = 0;
            var NetFareInBound = 0;
            var TotalNetFareOutBound = 0;
            var TotalNetFareInBound = 0;


            var FareOutBound = 0;
            var FareInBound = 0;
            var TotalFareOutBound = 0;
            var TotalFareInBound = 0;
            var PgChargOb = 0;
            var PgChargIb = 0;

            var TotalPgChargOb = 0.00;
            var TotalPgChargIb = 0.00;
            // 
            if (tripType == "InBound") {
                NetFareOutBound = $("#hdnNetFareOutBound").val();
                NetFareInBound = $("#hdnNetFareInBound").val();

                FareOutBound = $("#hdnTotalFareOutBound").val();
                FareInBound = $("#hdnTotalFareInBound").val();

            }
            else {
                NetFareOutBound = $("#hdnNetFareOutBound").val();
                FareOutBound = $("#hdnTotalFareOutBound").val();
            }

            if (PaymentMode != "WL") {
                // var PaymentMode = $("#ctl00_ContentPlaceHolder1_rblPaymentMode").val();            
                $.ajax({
                    type: "POST",
                    url: "PriceDetails.aspx/GetPgChargeByMode",
                    data: '{paymode: "' + PaymentMode + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "") {
                            // 
                            if (data.d.indexOf("~") > 0) {
                                //var res = data.d.split('~');
                                var pgCharge = data.d.split('~')[0]
                                var chargeType = data.d.split('~')[1]

                                if (tripType == "InBound") {
                                    if (chargeType == "F") {
                                        //calculate pg charge Fixed  of InBound
                                        PgChargOb = (parseFloat(pgCharge) / 2).toFixed(2);
                                        TotalNetFareOutBound = (parseFloat(NetFareOutBound) + parseFloat(PgChargOb)).toFixed(2);
                                        TotalFareOutBound = (parseFloat(FareOutBound) + parseFloat(PgChargOb)).toFixed(2);

                                        PgChargIb = (parseFloat(pgCharge) / 2).toFixed(2);
                                        TotalNetFareInBound = (parseFloat(NetFareInBound) + parseFloat(PgChargIb)).toFixed(2);
                                        TotalFareInBound = (parseFloat(FareInBound) + parseFloat(PgChargIb)).toFixed(2);


                                        $('#PgChargeOutBound').html(PgChargOb);
                                        $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                                        $('#divTotalFareOutBound').html(TotalFareOutBound);

                                        $('#PgChargeInBound').html(PgChargIb);
                                        $('#lblNetFareInBound').html(TotalNetFareInBound);
                                        $('#divTotalFareInBound').html(TotalFareInBound);

                                    }
                                    else {
                                        //calculate pg charge Percentage of InBound
                                        PgChargOb = ((parseFloat(NetFareOutBound) * parseFloat(pgCharge)) / 100).toFixed(2);
                                        TotalNetFareOutBound = (parseFloat(NetFareOutBound) + parseFloat(PgChargOb)).toFixed(2);
                                        TotalFareOutBound = (parseFloat(FareOutBound) + parseFloat(PgChargOb)).toFixed(2);

                                        PgChargIb = ((parseFloat(NetFareInBound) * parseFloat(pgCharge)) / 100).toFixed(2);
                                        TotalNetFareInBound = (parseFloat(NetFareInBound) + parseFloat(PgChargIb)).toFixed(2);
                                        TotalFareInBound = (parseFloat(FareInBound) + parseFloat(PgChargIb)).toFixed(2);

                                        $('#PgChargeOutBound').html(PgChargOb);
                                        $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                                        $('#divTotalFareOutBound').html(TotalFareOutBound);

                                        $('#PgChargeInBound').html(PgChargIb);
                                        $('#lblNetFareInBound').html(TotalNetFareInBound);
                                        $('#divTotalFareInBound').html(TotalFareInBound);

                                    }
                                }
                                else {
                                    //use for Outbound
                                    if (chargeType == "F") {
                                        //calculate pg charge Fixed of OutBound

                                        PgChargOb = (parseFloat(pgCharge)).toFixed(2);
                                        TotalNetFareOutBound = (parseFloat(NetFareOutBound) + parseFloat(PgChargOb)).toFixed(2);
                                        TotalFareOutBound = (parseFloat(FareOutBound) + parseFloat(PgChargOb)).toFixed(2);

                                        $('#PgChargeOutBound').html(PgChargOb);
                                        $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                                        $('#divTotalFareOutBound').html(TotalFareOutBound);

                                    }
                                    else {

                                        //calculate pg charge Percentage of OutBound

                                        PgChargOb = ((parseFloat(NetFareOutBound) * parseFloat(pgCharge)) / 100).toFixed(2);
                                        TotalNetFareOutBound = (parseFloat(NetFareOutBound) + parseFloat(PgChargOb)).toFixed(2);
                                        TotalFareOutBound = (parseFloat(FareOutBound) + parseFloat(PgChargOb)).toFixed(2);

                                        $('#PgChargeOutBound').html(PgChargOb);
                                        $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                                        $('#divTotalFareOutBound').html(TotalFareOutBound);
                                    }

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
                if (tripType == "InBound") {
                    // 
                    //calculate pg charge Fixed  of InBound
                    PgChargOb = "0.00";
                    TotalNetFareOutBound = (parseFloat(NetFareOutBound)).toFixed(2);
                    TotalFareOutBound = parseFloat(FareOutBound);

                    PgChargIb = "0.00";
                    TotalNetFareInBound = (parseFloat(NetFareInBound)).toFixed(2);
                    TotalFareInBound = parseFloat(FareInBound);

                    $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                    $('#divTotalFareOutBound').html(TotalFareOutBound);
                    $('#PgChargeOutBound').html(PgChargOb);

                    $('#lblNetFareInBound').html(TotalNetFareInBound);
                    $('#divTotalFareInBound').html(TotalFareInBound);
                    $('#PgChargeInBound').html(PgChargIb);

                }
                else {
                    //calculate pg charge Percentage of OutBound
                    PgChargOb = "0.00";
                    TotalNetFareOutBound = (parseFloat(NetFareOutBound)).toFixed(2);
                    TotalFareOutBound = parseFloat(FareOutBound);

                    $('#lblNetFareOutBound').html(TotalNetFareOutBound);
                    $('#divTotalFareOutBound').html(TotalFareOutBound);
                    $('#PgChargeOutBound').html(PgChargOb);
                }

            }

        }
    </script>
</asp:Content>
