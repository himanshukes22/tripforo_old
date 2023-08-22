<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="UploadAmount.aspx.cs" Inherits="SprReports_Accounts_UploadAmount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />
    <style>
        .msi {
            width: 130% !important;
            max-width: 130% !important;
        }
        #ctl00_ContentPlaceHolder1_rblPaymentMode {
            margin:0 auto;
        }
    </style>
    <div class="mtop80"></div>
    <div class="row" style="padding-top:20px;" >
        <div class="col-md-12 text-center search-text  ">
            Wallet Top-Up By Payment Gateway
        </div>
    </div>
    <div class="row ">
        <div class="col-md-12  text-center ">
            <div class="form-inlines">
                <div class="col-md-4 col-xs-12  text-center ">
                </div>
                <div class="col-md-4 col-xs-12  text-center ">
                    <div class="" style="font-size: 14px;">
                        Enter Amount
                    <br />
                        <br />
                        <asp:TextBox ID="TxtAmout" runat="server" class="form-controlaa" onkeypress="return keyRestrict(event,'0123456789');" MaxLength="11" Text="0" AutoCompleteType="Disabled"></asp:TextBox>

                    </div>
                    <%-- <div class="form-groups">
                    Remarks<br />
                    <asp:TextBox ID="TxtRemark" runat="server" class="form-controlaa" MaxLength="50"></asp:TextBox>
                </div>--%>

                    <div class=" " style="  font-size: 14px;">
                        Payment Mode<br />
                        <br />
                        <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Credit Card" Value="creditcard" 
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                ed="True"></asp:ListItem>
                            <asp:ListItem Text="Debit Card" Value="debitcard"></asp:ListItem>
                            <asp:ListItem Text="Net Banking" Value="netbanking"></asp:ListItem>
                            <asp:ListItem Text="Mobikwik" Value="Mobikwik"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <br />
                    
                    <div class="form-inlines text-left">
                        <div class="col-md-6  text-center ">
                            <div class="form-groups" style="width: 230px; font-size: 14px;">
                                Transaction Charges: &nbsp; &nbsp;<asp:Label ID="lblTransCharges" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-6  text-center ">
                            <div class="form-groups" style="width: 240px; font-size: 14px;">
                                Total  Amount:&nbsp; &nbsp;<asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="form-groups text-center" style="margin-left:50%;" >
                            <asp:Button ID="BtnUpload" runat="server" CssClass="buttonfltbks" Text="Upload Amount" OnClick="BtnUpload_Click" OnClientClick="return ValidateAmount()" style="width:120%" />
                        </div>
                    </div>
                </div>
                <div class="col-md-4  text-center ">
                </div>



            </div>


        </div>
    </div>
    <input type="hidden" id="TransCharges" name="TransCharges" />
    <input type="hidden" id="TotalAmount" name="TotalAmount" />




    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/JS/Distributor.js") %>"></script>

    <%-- <script type="text/javascript">        
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_TxtAmout").keypress(function () {
                GetPgTransCharge();
            });
        });
</script>--%>

    <script type="text/javascript">
        //function preventBack() { window.history.forward(); }
        //setTimeout("preventBack()", 0);
        //window.onunload = function () { null };
        //window.onload = function () {
        //    document.getElementById("ctl00_ContentPlaceHolder1_TxtAmout").name = "txt" + Math.random();
        //}
    </script>

    <script type="text/javascript">
        function ValidateAmount() {
            if ($("#ctl00_ContentPlaceHolder1_TxtAmout").val().trim() == "") {
                alert("Plese enter upload amount.")
                return false;
            }
            if (parseFloat($("#ctl00_ContentPlaceHolder1_TxtAmout").val()) < 1) {
                alert("Plese enter amount greater than zero.")
                return false;
            }
            GetPgTransCharge();

            var bConfirm = confirm('Payment gateway transaction charges Rs. ' + $('#TransCharges').val() + ' and  Total Amount Rs. ' + $('#TotalAmount').val() + ' debit from your bank a/c, Are you sure upload amount?');
            if (bConfirm == true) {
                $("#ctl00_ContentPlaceHolder1_BtnUpload").hide();
                return true;
            }
            else {
                $("#ctl00_ContentPlaceHolder1_BtnUpload").show();
                return false;
            }
        }

        $("#ctl00_ContentPlaceHolder1_TxtAmout").keyup(function () {
            GetPgTransCharge();
            //if ($("#ctl00_ContentPlaceHolder1_TxtAmout").val().trim() == "" && $("#ctl00_ContentPlaceHolder1_TxtAmout").val() == null) {
            //    var str = 0;
            //    $("#ctl00_ContentPlaceHolder1_TxtAmout").val(str);
            //}            
        })

        $("#ctl00_ContentPlaceHolder1_rblPaymentMode").click(function () {
            GetPgTransCharge();
        });
        function GetPgTransCharge() {
            var checked_radio = $("[id*=ctl00_ContentPlaceHolder1_rblPaymentMode] input:checked");
            var PaymentMode = checked_radio.val();
            var TotalPgCharges = 0;
            var TotalAmount = 0;
            var Amount = 0;
            if ($("#ctl00_ContentPlaceHolder1_TxtAmout").val().trim() != "" && $("#ctl00_ContentPlaceHolder1_TxtAmout").val() != null) {
                Amount = $("#ctl00_ContentPlaceHolder1_TxtAmout").val();
            }
            else {
                Amount = 0
            }
            $.ajax({
                type: "POST",
                url: "UploadAmount.aspx/GetPgChargeByMode",
                data: '{paymode: "' + PaymentMode + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        if (data.d.indexOf("~") > 0) {
                            //var res = data.d.split('~');
                            var PgCharge = data.d.split('~')[0]
                            var chargeType = data.d.split('~')[1]
                            if (chargeType == "F") {
                                //calculate fixed pg charge 
                                TotalPgCharges = (parseFloat(PgCharge)).toFixed(2);
                                TotalAmount = (parseFloat(Amount) + parseFloat(TotalPgCharges)).toFixed(2);

                                $('#TransCharges').val(TotalPgCharges);
                                $('#TotalAmount').val(TotalAmount);
                                $('#<%=lblTransCharges.ClientID%>').html(TotalPgCharges);
                                     $('#<%=lblTotalAmount.ClientID%>').html(TotalAmount);
                                 }
                                 else {
                                     //calculate percentage pg charge                                     
                                     TotalPgCharges = ((parseFloat(Amount) * parseFloat(PgCharge)) / 100).toFixed(2);
                                     TotalAmount = (parseFloat(Amount) + parseFloat(TotalPgCharges)).toFixed(2);

                                     $('#TransCharges').val(TotalPgCharges);
                                     $('#TotalAmount').val(TotalAmount);
                                     $('#<%=lblTransCharges.ClientID%>').html(TotalPgCharges);
                                     $('#<%=lblTotalAmount.ClientID%>').html(TotalAmount);
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

             function getKeyCode(e) {
                 if (window.event)
                     return window.event.keyCode;
                 else if (e)
                     return e.which;
                 else
                     return null;
             }
             function keyRestrict(e, validchars) {
                 var key = '', keychar = '';
                 key = getKeyCode(e);
                 if (key == null) return true;
                 keychar = String.fromCharCode(key);
                 keychar = keychar.toLowerCase();
                 validchars = validchars.toLowerCase();
                 if (validchars.indexOf(keychar) != -1)
                     return true;
                 if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
                     return true;
                 return false;
             }
    </script>

</asp:Content>

