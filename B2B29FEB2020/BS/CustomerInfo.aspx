<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true"
    CodeFile="CustomerInfo.aspx.cs" Inherits="BS_CustomerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <div class="ssssss" runat="server" id="ddddd"></div>

    <link href="<%=ResolveUrl("~/BS/CSS/CommonCss.css")%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .totalfare {
            text-align: left;
            position: absolute;
            z-index: 1000;
            padding: 10px;
            margin-left: -300px;
            max-width: 400px;
            right: 100px;
            width: 300px;
            margin-top: 250px;
        }

        .col-md-3 {
            width: 25%;
        }

        .col-md-8 {
            width: 66.66666667%;
        }

        /*.payamount {
    text-align: left;
    position: absolute;
    display: none;
    box-shadow: 1px 0px 3px #333;
    z-index: 1000;
    padding: 10px;
    margin-left: -300px;
    max-width: 400px;
    right: 200px;
    min-width: 200px;
    margin-top: 130px;

}*/
    </style>
    <div style="background-color: #eee!important; width:100%; height:auto; ">
        <div class="large-10 medium-10 small-12 large-push-1 medium-push-1">
            <div class="large-12 medium-12 small-12">
                <div class="large-9 medium-9 small-12 columns " style="border: 1px solid #ccc; margin-top: 10px;">
                    <div class="large-9 medium-1 small-1 headbgs" id="abcd">
                        <span><i class="fa fa-bus" aria-hidden="true"></i><span id="">Bus Details</span></span>

                    </div>
                    <div class="w100 mauto padding1" id="tblhidefalse" runat="server" style="padding: 10px;">
                        <div id="divUpper" class="w100" runat="server">
                            <div class="clear"></div>
                        </div>

                    </div>
                    <div class="clear"></div>
                    <div class="w100">
                        <div>
                            <asp:Repeater ID="rep_Pax" runat="server">
                                <ItemTemplate>
                                    <div class="large-9 medium-1 small-1 headbgs" id="lblpax" runat="server">
                                        <span><i class="fa fa-user-o" aria-hidden="true"></i><span id="">'<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'</span></span>

                                    </div>
                                    <table width="100%" cellpadding="10" cellspacing="5" border="0" style="line-height: 20px; margin-bottom: 10px;">

                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="15" border="0" align="center" style="width: 100%;">
                                                    <tr>
                                                        <td class="w12">
                                                            <div>
                                                                Title:
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList ID="dptitle" CssClass="drpBox dptitle" runat="server">
                                                                    <asp:ListItem Value="select">Title</asp:ListItem>
                                                                    <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                                                    <asp:ListItem Value="Ms">Ms</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td class="w25">
                                                            <div>
                                                                Full Name:
                                                            </div>
                                                            <div class="lft">
                                                                <asp:TextBox ID="txtpaxname" runat="server" MaxLength="70" CssClass="txtBox txtpaxname"></asp:TextBox>
                                                            </div>
                                                            <div class="lft">
                                                                <div class="divERR" id='divtxtpaxname' runat="server"></div>
                                                            </div>
                                                        </td>
                                                        <td class="w20">
                                                            <div>
                                                                Age:
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtpaxage" runat="server" CssClass="txtBox w100 txtpaxage" onblur="agevalidate(this.value)" MaxLength="3" onkeypress="validatenumber(event,this.id)"></asp:TextBox>
                                                            </div>
                                                            <div>
                                                                <div class="divERR" id="divtxtpaxage" runat="server">
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td class="w15">
                                                            <div>
                                                                Gender:
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList ID="dpgender" CssClass="drpBox dpgender" runat="server">
                                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td class='w8'>
                                                            <div>Seat:</div>
                                                            <div>
                                                                <img src='<%#DataBinder.Eval(Container.DataItem, "ladiesSeat")%>' />
                                                                <asp:Label ID="lblseat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "seat")%>'></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td class='w12'>
                                                            <div>Fare:</div>
                                                            <div class="f16">
                                                                <img src='../images/rs.png' /><asp:Label ID="lblfare" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "fare")%>'></asp:Label><asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Originalfare")%>'
                                                                    Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td class="tdSeatFare w8">
                                                            <div>Seat:</div>
                                                            <div>
                                                                <img src='<%#DataBinder.Eval(Container.DataItem, "ladiesSeat1")%>' /><asp:Label ID="lblseat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "seat1")%>'></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td class="tdSeatFare w12">
                                                            <div>Fare:</div>
                                                            <div class="f16">
                                                                <img src='../images/rs.png' /><asp:Label ID="lblfare1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "fare1")%>'></asp:Label><asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Originalfare1")%>'
                                                                    Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div>
                            <table cellpadding="0" cellspacing="0" border="0" class="brdr w100">
                                <tr>
                                    <td class="pcls" colspan="2">Primary Passenger Contact Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="clear1"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="pPaxId" runat="server" class="w45 padding2s">
                                        <div>
                                            Full Name:
                                        </div>
                                        <div>
                                            <input type="hidden" id="hidprovider" runat="server" />
                                        </div>
                                        <div>
                                            <input type="text" id="txtprimarypax" name="txtprimarypax" class="txtBox w100"  />
                                        </div>
                                    </td>
                                    <td class="w45 padding2s">
                                        <div>
                                            Mobile:
                                        </div>
                                        <div>
                                            <input type="text" id="txtmob" name="txtmob" class="txtBox w100" placeholder="Mobile Number" maxlength="10" onkeypress="validatenumber(event,this.id)" />
                                        </div>
                                        <div>
                                            <div class="divERR" id="divtxtmob"></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="w45 padding2s">
                                        <div>
                                            Email:
                                        </div>
                                        <div>
                                            <input type="text" id="txtemail" name="txtemail" class="txtBox w100" placeholder="Email Address" onblur="validateEmail(this.id)" />
                                        </div>
                                        <div>
                                            <div class="divERR" id="divtxtemail">
                                            </div>
                                        </div>
                                    </td>

                                    <td class="w45 padding2s">
                                        <div>
                                            Address:
                                        </div>
                                        <div>
                                            <input type="text" id="txtaddress" name="txtaddress" class="txtBox w100"
                                                placeholder="Address" />
                                        </div>
                                        <div>
                                            <div class="divERR" id="divtxtaddress">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="tdid1" runat="server" visible="false" class="w45 padding2s">
                                        <div>ID Proof:</div>
                                        <div>
                                            <select id='idproof' name='idproof' class='drpBox w100'>
                                                <option value='PAN_CARD'>Pan Card</option>
                                                <option value='VOTER_CARD'>Voter Id</option>
                                                <option value='DRIVING_LICENCE'>Driving Licence</option>
                                                <option value='RATION_CARD'>Ration Card</option>
                                                <option value='AADHAR'>Aadhar Card</option>
                                            </select>
                                        </div>
                                    </td>
                                    <td id="tdid2" runat="server" visible="false" class="w45 padding2s">
                                        <div>ID Number: </div>
                                        <div>
                                            <input type="text" id="txtcard" name="txtcard" class="txtBox w100" placeholder="ID Number" />
                                        </div>
                                        <div>
                                            <div class="divERR" id="divtxtcard">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                 <%--<tr class="hide">--%>
                                <tr class="hi de">
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="WL">Wallet</asp:ListItem>
                                                    <%--<asp:ListItem Value="PG">Payment Gateway</asp:ListItem>--%>
                                                    <asp:ListItem Text="Credit Card" Value="creditcard"></asp:ListItem>
                                                    <asp:ListItem Text="Debit Card" Value="debitcard"></asp:ListItem>
                                                    <asp:ListItem Text="Net Banking" Value="netbanking"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Cash Card" Value="cashcard"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnbook" runat="server" Text="Book" CssClass="buttonfltbk" OnClick="btnbook_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="large-3 medium-3 small-12 columns">
                    <div class="row" style="border: 1px solid #ccc; margin: 10px; padding: 0px;">
                        <div class="large-12 medium-12 small-1 headbgs">
                            <span><i class="fa fa-inr" aria-hidden="true"></i><span id="">Fare Breakup </span></span>

                        </div>
                        <div id="tbl_pax">
                            <div class="w100 rgt brdr boxshadow bgf1 padding1" style="padding: 10px;">
                                <div id="divfarebrk" class="" runat="server">
                                </div>
                                <div id="divComm">
                                </div>
                            </div>

                            <div class="clear"></div>
                        </div>

                    </div>
                </div>

            </div>
            <asp:HiddenField ID="HdnPgCharge" runat="server" Value="0" />            
            <asp:HiddenField ID="HdnOrgTotalFare" Value="0" runat="server" />
            <asp:HiddenField ID="HdnOrgPayAmount" runat="server" Value="0" />
            <asp:HiddenField ID="HdnOrgNetFare" Value="0" runat="server" />
            <asp:HiddenField ID="HdnOrgNetFare2" Value="0" runat="server" />


            <input type="hidden" name="HdnPaymentMode" value="WL" id="HdnPaymentMode" />
            <input type="hidden" name="HdnNetFare" value="0" id="HdnNetFare" />
            <input type="hidden" name="HdnNetFarePayAmt" value="0" id="HdnNetFarePayAmt" />            
            
        </div>
        <div class="w60 mauto padding1 brdr bgf1 boxshadow" id="idehide" runat="server">
            <div id="ghxsgh" runat="server">
            </div>
            <div id="hideGs" align="right" runat="server">
                <div id="ordrId" runat="server" class="hide">xgfdx</div>
                <div id="ordrId1" runat="server" class="hide">fgdfgd</div>
                <asp:Button ID="btnbookGs" class="button33 w20" runat="server" Text="OK" OnClick="btnbookGs_Click" />
            </div>
            <div class="clear"></div>
        </div>



        <div id="wait" style="display: none; margin-top: 100px;" class="wait">
            <div style="padding: 2% 2%; width: 46%; margin: 100px auto 0; border: 5px solid #ccc; background: #fff; text-align: center; font-family: Arial;">
                <strong>Booking is on Process..Please Wait</strong><br />
                <img src="Images/loaderB64.gif" /><br />
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <div class="clear"></div>
    </div>
    </div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_rep_Pax_ctl00_txtpaxname").change(function () {
                $("#txtprimarypax").val($("#ctl00_ContentPlaceHolder1_rep_Pax_ctl00_txtpaxname").val());
            });
        });
    </script>
    <script src="<%= ResolveUrl("~/BS/JS/jquery-1.9.1.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/BS/JS/Common.js")%>" type="text/javascript"></script>
     <script type="text/javascript">
         function agevalidate(strval) {

             var age = strval;
             if (!(age > 1 && age < 100)) {
                 alert("The age must be a number between 1 and 100");
                 return false;
             }
             return true;
         }
    </script>

     <script type="text/javascript">
         $("#ctl00_ContentPlaceHolder1_rblPaymentMode").click(function () {
             GetPgTransCharge();
         });
         function GetPgTransCharge() {            
             var checked_radio = $("[id*=ctl00_ContentPlaceHolder1_rblPaymentMode] input:checked");
             var PaymentMode = checked_radio.val();           
             var TotalPgCharges = 0;
             var TotalFare = 0;
             var PayAmount = 0;
             var NetFare = 0;
            // $('#ctl00_ContentPlaceHolder1_HdnPaymentMode').val(PaymentMode);
             $('#HdnPaymentMode').val(PaymentMode);
             var HdnOrgTotalFare = $("#ctl00_ContentPlaceHolder1_HdnOrgTotalFare").val();
             var HdnOrgPayAmount = $("#ctl00_ContentPlaceHolder1_HdnOrgPayAmount").val();
             var HdnOrgNetFare = $("#ctl00_ContentPlaceHolder1_HdnOrgNetFare").val();

             if (PaymentMode != "WL") {
                 $.ajax({
                     type: "POST",
                     url: "CustomerInfo.aspx/GetPgChargeByMode",
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
                                     TotalFare = (parseFloat(HdnOrgTotalFare) + parseFloat(TotalPgCharges)).toFixed(2);
                                     PayAmount = (parseFloat(HdnOrgPayAmount) + parseFloat(TotalPgCharges)).toFixed(2);
                                     NetFare = (parseFloat(HdnOrgNetFare) + parseFloat(TotalPgCharges)).toFixed(2);                                     
                                     $('#PgCharge').html(TotalPgCharges);
                                     $('#divTotalFare').html(TotalFare);
                                     $('#SpanPayAmount').html(PayAmount);

                                     $('#NetFare').val(NetFare);
                                     $('#NetFarePayAmt').val(NetFare);

                                     $('#HdnNetFare').val(NetFare);
                                     $('#HdnNetFarePayAmt').val(NetFare);

                                     //$('#ctl00_ContentPlaceHolder1_HdnNetFare').val(NetFare);
                                     //$('#ctl00_ContentPlaceHolder1_HdnNetFarePayAmt').val(NetFare);

                                 }
                                 else {
                                     //calculate percentage pg charge  
                                     TotalPgCharges = ((parseFloat(HdnOrgNetFare) * parseFloat(PgCharge)) / 100).toFixed(2);

                                     TotalFare = (parseFloat(HdnOrgTotalFare) + parseFloat(TotalPgCharges)).toFixed(2);
                                     PayAmount = (parseFloat(HdnOrgPayAmount) + parseFloat(TotalPgCharges)).toFixed(2);
                                     NetFare = (parseFloat(HdnOrgNetFare) + parseFloat(TotalPgCharges)).toFixed(2);
                                     $('#PgCharge').html(TotalPgCharges);
                                     $('#divTotalFare').html(TotalFare);
                                     $('#SpanPayAmount').html(PayAmount);
                                     // change value
                                     $('#NetFare').val(NetFare);
                                     $('#NetFarePayAmt').val(NetFare);

                                     $('#HdnNetFare').val(NetFare);
                                     $('#HdnNetFarePayAmt').val(NetFare);

                                     


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
                 TotalPgCharges = "0.00";
                 $('#PgCharge').html(TotalPgCharges);
                 $('#divTotalFare').html(HdnOrgTotalFare);
                 $('#SpanPayAmount').html(HdnOrgPayAmount);
                 // change value
                 $('#NetFare').val(HdnOrgNetFare);
                 $('#NetFarePayAmt').val(HdnOrgNetFare);

                 $('#HdnNetFare').val(HdnOrgNetFare);
                 $('#HdnNetFarePayAmt').val(HdnOrgNetFare);

                 //$('#ctl00_ContentPlaceHolder1_HdnNetFare').val(HdnOrgNetFare);
                 //$('#ctl00_ContentPlaceHolder1_HdnNetFarePayAmt').val(HdnOrgNetFare);

             }
         }

        </script>
</asp:Content>
