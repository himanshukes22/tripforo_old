<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PriceDetails.aspx.vb" MasterPageFile="~/MasterAfterLogin.master" Inherits="FlightDom_PriceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../CSS/astyle.css" rel="stylesheet" />--%>
    <link href="../Advance_CSS/css/Price-Details.css" rel="stylesheet" />
    <link href="../Advance_CSS/css/paxdetails.css" rel="stylesheet" />
    <style type="type/css">

        .clean {
            clear: both;
        }

        .f18 {
            font-size: 18px;
        }

        .blds {
            color: #004b91 !important;
            font-weight: bold !important;
        }
    </style>



    <link href="../Custom_Design/css/skunal.css" rel="stylesheet" />


    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

    <style type="text/css">
        :root {
            --primary-color: white;
            --secondary-color: rgb(61, 68, 73);
            --highlight-color: #3282b8;
            --dt-status-available-color: greenyellow;
            --dt-status-away-color: lightsalmon;
            --dt-status-offline-color: lightgray;
            --dt-padding: 12px;
            --dt-padding-s: 6px;
            --dt-padding-xs: 2px;
            --dt-border-radius: 3px;
            --dt-background-color-container: #2a3338;
            --dt-border-color: var(--secondary-color);
            --dt-bg-color: var(--highlight-color);
            --dt-text-color: var(--primary-color);
            --dt-bg-active-button: var(--highlight-color);
            --dt-text-color-button: var(--primary-color);
            --dt-text-color-active-button: var(--primary-color);
            --dt-hover-cell-color: var(--highlight-color);
            --dt-even-row-color: var(--secondary-color);
            --dt-focus-color: var(--highlight-color);
            --dt-input-background-color: var(--secondary-color);
            --dt-input-color: var(--primary-color);
        }

        .material-icons {
            font-size: 16px;
        }

        .datatable-container {
            font-family: sans-serif;
            background-color: #ffffff;
            border-radius: var(--dt-border-radius);
            color: black;
            max-width: 100%;
            min-width: 100%;
            margin: 0 auto;
            font-size: 14px;
        }

        .pass-head {
            background: #000;
            color: #fff;
            font-size: 14px;
        }

        .datatable-container .header-tools {
            border-bottom: solid 1px var(--dt-border-color);
            padding: var(--dt-padding);
            padding-left: 0;
            display: flex;
            align-items: baseline;
        }

            .datatable-container .header-tools .search {
                width: 30%;
            }

                .datatable-container .header-tools .search .search-input {
                    width: 100%;
                    height: calc(1.5em + 0.75rem + 2px);
                    padding: 0.375rem 0.75rem;
                    background-color: var(--dt-input-background-color);
                    display: block;
                    box-sizing: border-box;
                    border-radius: var(--dt-border-radius);
                    border: solid 1px var(--dt-border-color);
                    color: var(--dt-input-color);
                }

            .datatable-container .header-tools .tools {
                width: 70%;
            }

                .datatable-container .header-tools .tools ul {
                    margin: 0;
                    padding: 0;
                    display: flex;
                    justify-content: start;
                    align-items: baseline;
                }

                    .datatable-container .header-tools .tools ul li {
                        display: inline-block;
                        margin: 0 var(--dt-padding-xs);
                        align-items: baseline;
                    }

        .datatable-container .footer-tools {
            padding: var(--dt-padding);
            display: flex;
            align-items: baseline;
        }

            .datatable-container .footer-tools .list-items {
                width: 50%;
            }

            .datatable-container .footer-tools .pages {
                margin-left: auto;
                margin-right: 0;
                width: 50%;
            }

                .datatable-container .footer-tools .pages ul {
                    margin: 0;
                    padding: 0;
                    display: flex;
                    align-items: baseline;
                    justify-content: flex-end;
                }

                    .datatable-container .footer-tools .pages ul li {
                        display: inline-block;
                        margin: 0 var(--dt-padding-xs);
                    }

                        .datatable-container .footer-tools .pages ul li button,
                        .datatable-container .header-tools .tools ul li button {
                            color: var(--dt-text-color-button);
                            width: 100%;
                            box-sizing: border-box;
                            border: 0;
                            border-radius: var(--dt-border-radius);
                            background: transparent;
                            cursor: pointer;
                        }

                            .datatable-container .footer-tools .pages ul li button:hover,
                            .datatable-container .header-tools .tools ul li button:hover {
                                background: var(--dt-bg-active-button);
                                color: var(--dt-text-color-active-button);
                            }

                        .datatable-container .footer-tools .pages ul li span.active {
                            background-color: var(--dt-bg-color);
                            border-radius: var(--dt-border-radius);
                        }

                        .datatable-container .footer-tools .pages ul li button,
                        .datatable-container .footer-tools .pages ul li span,
                        .datatable-container .header-tools .tools ul li button {
                            padding: var(--dt-padding-s) var(--dt-padding);
                        }

        .datatable-container .datatable {
            border-collapse: collapse;
            width: 100%;
        }

            .datatable-container .datatable,
            .datatable-container .datatable th,
            .datatable-container .datatable td {
                padding: var(--dt-padding) var(--dt-padding);
            }

                .datatable-container .datatable th {
                    font-weight: bolder;
                    text-align: left;
                    border-bottom: solid 1px var(--dt-border-color);
                }

                .datatable-container .datatable td {
                    border-bottom: solid 1px rgb(202 202 202);
                    text-transform: capitalize;
                }

                .datatable-container .datatable tbody tr:nth-child(even) {
                    
                }

                .datatable-container .datatable tbody tr:hover {
                    background-color: #99d65a33;
                }

                .datatable-container .datatable tbody tr .available::after,
                .datatable-container .datatable tbody tr .away::after,
                .datatable-container .datatable tbody tr .offline::after {
                    display: inline-block;
                    vertical-align: middle;
                }

                .datatable-container .datatable tbody tr .available::after {
                    content: "Online";
                    color: var(--dt-status-available-color);
                }

                .datatable-container .datatable tbody tr .away::after {
                    content: "Away";
                    color: var(--dt-status-away-color);
                }

                .datatable-container .datatable tbody tr .offline::after {
                    content: "Offline";
                    color: var(--dt-status-offline-color);
                }

                .datatable-container .datatable tbody tr .available::before,
                .datatable-container .datatable tbody tr .away::before,
                .datatable-container .datatable tbody tr .offline::before {
                    content: "";
                    display: inline-block;
                    width: 10px;
                    height: 10px;
                    margin-right: 10px;
                    border-radius: 50%;
                    vertical-align: middle;
                }

                .datatable-container .datatable tbody tr .available::before {
                    background-color: var(--dt-status-available-color);
                }

                .datatable-container .datatable tbody tr .away::before {
                    background-color: var(--dt-status-away-color);
                }

                .datatable-container .datatable tbody tr .offline::before {
                    background-color: var(--dt-status-offline-color);
                }
    </style>
    <%--<script language="javascript" type="text/javascript">

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
    </script>--%>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        .modal-header {
            /* border-radius: 5px; */
            /* color: #fff; */
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-align: start;
            -ms-flex-align: start;
            align-items: flex-start;
            -webkit-box-pack: justify;
            -ms-flex-pack: justify;
            /* justify-content: space-between; */
            padding: 1rem;
            border-bottom: 1px solid #e9ecef;
            border-top-left-radius: .3rem;
            border-top-right-radius: .3rem;
        }
    </style>
    <script>
        $(document).ready(function () {
            var rslt = false;
            $("#ctl00_ContentPlaceHolder1_Submit").click(function (e) {
                $("#btnConfirmPopup").click();
                return false;
            });

            $("#modal-btn-confirm-popup").click(function () {
                $("#modal-btn-confirm-popup").html("Processing...<i class='fa fa-spinner fa-pulse'></i>");
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PriceDetails.aspx/Submit_Button_Click",
                    dataType: "json",
                    success: function (data) {
                        if (data != "") {
                            //alert(data.d);
                            window.location.href = data.d;
                        }
                    },
                    error: function (e, t, n) {
                        debugger;
                        alert(e.response);
                    }
                });
            });
        });
    </script>

    <button type="button" class="btn btn-primary" style="display: none;" id="btnConfirmPopup" data-toggle="modal" data-target="#ConfirmPopup"></button>
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallConfirmPopup" aria-hidden="true" id="ConfirmPopup">
        <div class="modal-dialog modal-sm" style="margin-top: 10%; width: 30% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="color: forestgreen;"><i class='fa fa-check-circle'></i>&nbsp;Confirmation</h4>
                </div>
                <div class="modal-body">
                    <h5 class="text-center">Are you sure you want to continue with this flight?</h5>
                    <div id="div_FlightDel" runat="server"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="modal-btn-cancel-popup" data-dismiss='modal' style="float: left; background: #ff0000">Cancel</button>
                    <button type="button" class="btn btn-success" id="modal-btn-confirm-popup">Confirm</button>
                </div>
            </div>
        </div>
    </div>


    <div style="text-align: right; width: 20%; float: right; padding: 5px; display: none;">
        <input type="button" id="btnBookAnother" value="Book Other Flight" style="width: 200PX;" class="buttonfltbk" name="<%=Session("SearchCriteriaUser").ToString()%>" />
    </div>


    <div class="theme-page-section theme-page-section-lg">
        <div class="container">
            <div class="row row-col-static row-col-mob-gap" data-gutter="60">

                <div class="col-md-8">
                    <div class="theme-payment-page-sections">

                        <div class="row">
                            <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb">Ticket Details</span>
                                        <div class="padLR10 padT5 ico18 quicks fb" style="cursor: pointer; margin-top: 1px; color: #5d5d5d; padding: 5px; border-radius: 5px; float: right; margin-top: -4px; margin-right: 10px;"><i class="icofont-bubble-left"></i><span onclick="history.back(-1)" style="font-size: 15px;">Back to Previous Section</span></div>
                                    </div>
                                </div>
                                <div id="divFltDtls" runat="server">
                                </div>
                            </div>
                        </div>

                        <div class="clean"></div>
                        <div class="row">
                            <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                                <div class="width100 borderBtm padLR15 padTB10">
                                    
                                        <span class="padLR10 padT5 ico18 quicks fb">Passenger Details</span>
                                        <div class="padLR10 padT5 ico18 quicks fb hidden" style="cursor: pointer; margin-top: 1px; color: #5d5d5d; padding: 5px; border-radius: 5px; float: right; margin-top: -4px; margin-right: 10px;">
                                            <i class="icofont-edit"></i>
                                            <asp:LinkButton ID="LinkEdit" Text="Edit" runat="server" Font-Bold="True"></asp:LinkButton>
                                        </div>

                                    
                                </div>
                                <div id="divPaxdetails" runat="server">
                                </div>
                                <div class="large-12 medium-12 small-12  " id="SeatInformation" runat="server" style="background-color: #fff; margin-top: 10px;">
                                </div>




                                <div class='row' id="PaxGrd_div" runat="server" style='background-color: #fff; width: 98%;'>
                                    
                                    <div class='clear1'></div>
                                    <div class='large-12 medium-12 small-12'></div>

                                    <div class="datatable-container">
                                        <table class="datatable">
                                            <tr>

                                                <td>
                                                    <asp:Label runat="server"><b>Mobile:</b></asp:Label>
                                                    <asp:TextBox ID="Mob_txt" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="Mob_txt" runat="server"
                                                        ErrorMessage="Mobile Number Is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Mob_txt" ErrorMessage="Invalid Mobile" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                </td>


                                                <td>
                                                    <asp:Label runat="server"><b>Email:</b></asp:Label>
                                                    <asp:TextBox ID="Email_txt" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="Email_txt" runat="server"
                                                        ErrorMessage="EmailID Is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email_txt"></asp:RegularExpressionValidator>

                                                </td>

                                            </tr>
                                        </table>
                                    </div>

                                    <div class="datatable-container">
                                    <asp:GridView ID="PAXGRD" class="datatable" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDetails_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Title" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Tittle" Visible="false" runat="server" Text='<%#Eval("Title")%>' />
                                                    <asp:DropDownList ID="GGDD_DISTYPE" runat="server" Style="margin-top: -20px" class="form-control">
                                                        <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                                        <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                                                        <asp:ListItem Value="Ms">Ms</asp:ListItem>
                                                        <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                        <asp:ListItem Value="Mstr">Mstr</asp:ListItem>

                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_PaxID" Visible="false" runat="server" Text='<%#Eval("PaxId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FName" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_FName" runat="server" Text='<%#Eval("FName")%>' />
                                                    <asp:RequiredFieldValidator ID="Requiredlbl_FName" ControlToValidate="lbl_FName" runat="server"
                                                        ErrorMessage="First Name Is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="MName" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_MName" Style="margin-top: -20px" runat="server" Text='<%#Eval("MName")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="LName" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_LName" runat="server" Text='<%#Eval("LName")%>' />
                                                    <asp:RequiredFieldValidator ID="Requiredlbl_LName" ControlToValidate="lbl_LName" runat="server"
                                                        ErrorMessage="Last Name Is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_PaxType" runat="server" Text='<%#Eval("PaxType")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DOB" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Gtxt_DOB" CssClass='<%#Eval("PaxType")%>' runat="server" Text='<%#Eval("DOB")%>' />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Gtxt_DOB" runat="server" ErrorMessage="Required DOB"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>

                                    </asp:GridView>
                                        </div>

                                </div>
                            </div>
                        </div>
                        <div class="clean"></div>

                        <div class="row">
                            <div class="borderAll whiteBg posRel crdShdw brRadius5 fl width100 marginT10 padB20">
                                <div class="width100 borderBtm padLR15 padTB10">
                                    <div class="">
                                        <span class="padLR10 padT5 ico18 quicks fb">Payment</span>
                                    </div>
                                </div>

                                <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal" Style="margin: 23px; font-size: 15px;">
                                    <asp:ListItem Text="Wallet" Selected="True" Value="WL" style="background: #9ad75a61; padding: 6px; border-radius: 5px;">&nbsp; Wallet
                            
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                                <label style="position: relative; top: -21px; left: 24px; color: #ccc;">Avilable Balance: <span id="avlblc" style="font-size: 11.5px !important; color: #000;"></span></label>
                                <asp:HiddenField ID="hdnAvlBal" runat="server" />
                            </div>
                        </div>

                        <div class="clean"></div>
                        <div class="row">
                            <div id="div_Submit" class="posRel brRadius5 fl width100 marginT10 padB20">
                                <div style="float: right;">
                                    <asp:Button ID="Submit" class="btn btn-danger" runat="server" Text="Confirm Booking" Style="border-radius: 4px;" />
                                </div>


                                <div class="col-md-1 hidden" style="float: right;">
                                    <asp:Button ID="ButtonHold" class="btn btn-danger" runat="server" Text="Hold" Visible="false" Style="border-radius: 4px;" />
                                </div>


                            </div>



                        </div>

                    </div>
                </div>

                <div class="col-md-4">
                    <div class="" id="divFareDtls" runat="server"></div>


                    <div class="" id="divFareDtlsR" runat="server">
                    </div>
                </div>


                <asp:HiddenField ID="HdnTripType" runat="server" />

            </div>
        </div>
    </div>


    <div class="">
        <div class="hidden">
            <asp:Label ID="lblHoldBookingCharge" runat="server" Text="" Style="font-weight: 800; font-size: 14px; color: orange;"></asp:Label>
        </div>



    </div>




    <div id="div_Progress" style="display: none">
        <b>Booking In Progress.</b> Please do not 'refresh' or 'back' button
                <img alt="Processing.." src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
    </div>



    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            let avlbal = $("#<%=hdnAvlBal.ClientID%>").val();
            if (avlbal != "") {
                $("#avlblc").html(avlbal);
            }
        });

        function funcnetfare(arg, id) {
            document.getElementById(id).style.display = arg

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnBookAnother").click(function () {
                window.location.href = $.trim($(this).attr("name"))
            });
        });
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

    <script type="text/javascript">
        var d = new Date();

        $(function () { var d = new Date(); var dd = new Date(1952, 01, 01); $(".ADT").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: ('1920:' + (d.getFullYear() - 12) + ''), navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true, defaultDate: dd }) });
        $(function () { $(".CHD").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '-2y', minDate: '-12y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
        $(function () { $(".INF").datepicker({ numberOfMonths: 1, dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, maxDate: '+0y', minDate: '-2y', navigationAsDateFormat: true, showOtherMonths: true, selectOtherMonths: true }) });
    </script>
</asp:Content>
