<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CommissionMaster.aspx.cs" Inherits="SprReports_Admin_CommissionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../chosen/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/css/lytebox.css")%>" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <style type="text/css">
        .page-wrapperss {
            background-color: #fff;
            margin-left: 15px;


        }
        .form-group > label {
            color: black;
        }

    </style>
    <script language="javascript" type="text/javascript">
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

        
        function requiredfiled(id) {
            debugger
            // var Basic = document.getElementById("ctl00_ContentPlaceHolder1_grd_P_IntlDiscount_ctl11_txt_CBasic").Value;
            //ctl00_ContentPlaceHolder1_grd_P_IntlDiscount_ctl29_txt_CBasic
            var id1 = id.substring(0, id.lastIndexOf('_')).trim();
            var Basic = $("#" + id1 + "_txt_CBasic").val()
            var YQ = $("#" + id1 + "_txt_CYQ").val()
            var CBasicYQ = $("#" + id1 + "_txt_CBasicYQ").val()
            var PBasic = $("#" + id1 + "_txt_PBasic").val()
            var PBasicYQ = $("#" + id1 + "_txt_PBasicYQ").val()
            var CashBackAmt = $("#" + id1 + "_txt_CashBackAmt").val()

            var HBasic = $("#" + id1 + "_txt_CBasich").text()
            var HYQ = $("#" + id1 + "_txt_CYQh").text()
            var HCBasicYQ = $("#" + id1 + "_txt_CBasicYQh").text()
            var HPBasic = $("#" + id1 + "_txt_PBasich").text()
            var HPBasicYQ = $("#" + id1 + "_txt_PBasicYQh").text()
            var HCashBackAmt = $("#" + id1 + "_txt_CashBackAmth").text()

         
            if (parseFloat(Basic) > parseFloat(HBasic)) {
                alert("Basic fare Not Greater than Exists Value")
                return false;
            }

            if (parseFloat(YQ) > parseFloat(HYQ)) {
                alert("YQ fare Not Greater than Exists Value")
                return false;
            }


            if (parseFloat(CBasicYQ) > parseFloat(HCBasicYQ)) {
                alert("BasicYQ fare Not Greater than Exists Value")
                return false;
            }

            if (parseFloat(PBasic) > parseFloat(HPBasic)) {
                alert("PBasic fare Not Greater than Exists Value")
                return false;
            }

            if (parseFloat(PBasicYQ) > parseFloat(HPBasicYQ)) {
                alert("PBasicYQ fare Not Greater than Exists Value")
                return false;
            }

            if (parseFloat(CashBackAmt) > parseFloat(HCashBackAmt)) {
                alert("CashBack fare Not Greater than Exists Value")
                return false;

            }
        }

        </script>
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-10"  style="margin-left:50px;">
            <div class="page-wrapperss">

                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Flight Setting > Commission Master</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">GroupType :   </label>
                                    <input type="checkbox" id="CheckBoxAllType" name="All Type" value="All Type" onclick="javascript: allType();" class="checked hide" />
                                    <%--  All Group Type--%>
                                    <input type="hidden" id="HiddenAlltype" runat="server" value="" />
                                    <asp:DropDownList ID="ddl_ptype" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Airline :</label>
                                    <asp:DropDownList ID="ddl_Pairline" CssClass="form-control" runat="server" data-placeholder="Choose a Airline..."
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3" style="display:none">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Fare Type</label>
                                    <asp:DropDownList ID="DdlFareType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="ALL" Text="ALL" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="NRM" Text="Normal Fare"></asp:ListItem>
                                        <asp:ListItem Value="CRP" Text="Corporate Fare"></asp:ListItem>
                                        <asp:ListItem Value="CPN" Text="Coupon Fare"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3" style="display:none">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Booking Channel:</label>
                                    <asp:DropDownList ID="DdlBookingChannel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="ALL" Text="ALL" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="row" style="display:none">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Trip Type</label>
                                    <asp:DropDownList ID="DdlTripType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="D" Text="Domestic" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="I" Text="International"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Commission Basic :</label>
                                    <asp:TextBox ID="txt_basic" CssClass="form-control" runat="server" onKeyPress="return keyRestrict(event,'.0123456789');"
                                        MaxLength="5" Text="0"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3" id="tr_PLB" runat="server">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Commission YQ:</label>
                                    <asp:TextBox ID="txt_CYQ" CssClass="form-control" runat="server" onKeyPress="return keyRestrict(event,'.0123456789');" MaxLength="5"
                                        Text="0"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Commission(BASIC+YQ) :</label>
                                    <asp:TextBox ID="txt_CYB" runat="server" CssClass="form-control" onKeyPress="return keyRestrict(event,'.0123456789');" MaxLength="5"
                                        Text="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row" style="display:none">


                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">PLB Basic:</label>
                                    <asp:TextBox ID="txt_Pbasic" CssClass="form-control" runat="server" onKeyPress="return keyRestrict(event,'.0123456789');"
                                        MaxLength="5" Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">PLB (BASIC+YQ) :</label>
                                    <asp:TextBox ID="txt_Pyqb" CssClass="form-control" runat="server" onKeyPress="return keyRestrict(event,'.0123456789');"
                                        MaxLength="5" Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Booking From Date:</label>
                                    <input type="text" name="From" id="From" class="form-control" readonly="readonly" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Booking To Date:</label>
                                    <input type="text" name="To" id="To" class="form-control" readonly="readonly" />

                                </div>
                            </div>

                        </div>



                        <div class="row" style="display:none">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Onward Travel From Date:</label>
                                    <input type="text" name="OTFromDate" id="OTFromDate" class="form-control" readonly="readonly" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Onward Travel To Date:</label>
                                    <input type="text" name="OTToDate" id="OTToDate" class="form-control" readonly="readonly" />

                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Return Travel From Date:</label>
                                    <input type="text" name="RTFromDate" id="RTFromDate" class="form-control" readonly="readonly" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Return Travel To Date:</label>
                                    <input type="text" name="RTToDate" id="RTToDate" class="form-control" readonly="readonly" />

                                </div>
                            </div>
                        </div>

                        <div class="row" style="display:none">
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Cabin Class</label>
                                    <asp:ListBox ID="ListCabinClassIn" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                        <asp:ListItem Value="All" Selected="True">--All--</asp:ListItem>
                                        <asp:ListItem Value="C">Business</asp:ListItem>
                                        <asp:ListItem Value="Y">Economy</asp:ListItem>
                                        <asp:ListItem Value="F">First</asp:ListItem>
                                        <asp:ListItem Value="W">Premium Economy</asp:ListItem>
                                        <asp:ListItem Value="P">Premium First</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Cashback Amount :</label>
                                    <asp:TextBox ID="TxtCashBack" CssClass="form-control" runat="server" onKeyPress="return keyRestrict(event,'.0123456789');"
                                        MaxLength="5" Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Restriction ON</label>
                                    <asp:DropDownList ID="DdlRestriction" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="BOTH" Text="BOTH" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="COMM" Text="COMMISSION"></asp:ListItem>
                                        <asp:ListItem Value="PLB" Text="PLB"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>                           
                            <div class="col-md-3">
                                 <div class="form-group">
                                    <label for="exampleInputPassword1">PPP Segment / Sector</label>
                                    <asp:DropDownList ID="DdlPPPType" runat="server" CssClass="form-control">
                                        <%--<asp:ListItem Value="BOTH" Text="BOTH" Selected="True"></asp:ListItem>--%>
                                        <asp:ListItem Value="PPPSEGMENT" Text="PPP Segment"></asp:ListItem>
                                        <asp:ListItem Value="PPPSECTOR" Text="PPP Sector"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="clear"></div>
                        <div id="divMoreFilter" style="display: none;">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-2"></div>
                                    <div class="col-sm-3"><b>Include</b></div>
                                    <div class="col-sm-3"><b>Exclude</b></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Booking Class:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtBookingClassIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => A,B,C etc)</span>

                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TxtBookingClassEx" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span style="color: #38b4ee; font-size: 10px;">(Ex => A,B,C etc)</span>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Fare Basis:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtFareBasisIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => AAA,BBB etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtFareBasisEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => AAA,BBB etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Orgin Airport:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOrginAirportIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => DEL,BOM etc)</span>

                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOrginAirportEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => BLR,PAT etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Destination Airport:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtDestAirportIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => DEL,BOM etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtDestAirportEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => DEL,BOM etc)</span>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Origin Country:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOriginCountryIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => IN,US etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOriginCountryEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => IN,US etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Destination Country:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtDestCountryIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => IN,US etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtDestCountryEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex => IN,US etc)</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Flight No:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtFlightNoIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  304,384 etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">

                                            <asp:TextBox ID="TxtFlightNoEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  304,384 etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Operating Carrier:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOperatingCarrierIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  UK,9W etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtOperatingCarrierEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  6E,9W etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Marketing Carrier:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtMarketingCarrierIn" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  UK,9W etc)</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:TextBox ID="TxtMarketingCarrierEx" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span style="color: #38b4ee; font-size: 10px;">(Ex =>  AI,9W etc)</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="display: none;">
                                    <div class="col-sm-2">
                                        <label for="exampleInputPassword1">Cabin Class</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                                <asp:ListItem Value="All">--All--</asp:ListItem>
                                                <asp:ListItem Value="C">Business</asp:ListItem>
                                                <asp:ListItem Value="Y">Economy</asp:ListItem>
                                                <asp:ListItem Value="F">First</asp:ListItem>
                                                <asp:ListItem Value="W">Premium Economy</asp:ListItem>
                                                <asp:ListItem Value="P">Premium First</asp:ListItem>
                                            </asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:ListBox ID="ListCabinClassEx" runat="server" SelectionMode="Multiple">
                                                <asp:ListItem Value="All">--All--</asp:ListItem>
                                                <asp:ListItem Value="C">Business</asp:ListItem>
                                                <asp:ListItem Value="Y">Economy</asp:ListItem>
                                                <asp:ListItem Value="F">First</asp:ListItem>
                                                <asp:ListItem Value="W">Premium Economy</asp:ListItem>
                                                <asp:ListItem Value="P">Premium First</asp:ListItem>
                                            </asp:ListBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="row">


                            <div class="col-md-3">
                                <div class="form-group" style="display: none; color: #d9534f;" id="hide">
                                    <span style="cursor: pointer"><b><u>Click here for more filter show/hide</u></b></span>

                                </div>
                                <div class="form-group" id="show" style="color: #d9534f;display:none">
                                    <span style="cursor: pointer"><b><u>Click here for more filter show/hide</u></b></span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="BtnExport" runat="server" Text="Export" CssClass="button buttonBlue" OnClick="BtnExport_Click" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="button buttonBlue" OnClick="BtnSearch_Click" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" Visible="false" CssClass="button buttonBlue" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12" style="width: 1200px; overflow-y: scroll;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="background-color: #fff; overflow-y: scroll; overflow-x: scroll; max-height: 500px;">
                                    <ContentTemplate>
                                        <asp:GridView ID="grd_P_IntlDiscount" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped" GridLines="None" Width="100%" PageSize="30" OnRowCancelingEdit="grd_P_IntlDiscount_RowCancelingEdit"
                                            OnRowEditing="grd_P_IntlDiscount_RowEditing" OnRowUpdating="grd_P_IntlDiscount_RowUpdating" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                        <asp:Label ID="lbl_group" runat="server" Text='<%#Eval("GroupType") %>'></asp:Label>
                                                      <%--  <a href='FilterCommissionUpdate.aspx?ID=<%#Eval("Id")%>' rel="lyteframe" rev="width: 900px; height: 400px; overflow:hidden;"
                                                            target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                            <asp:Label ID="lbl_group" runat="server" Text='<%#Eval("GroupType") %>'></asp:Label>
                                                        </a>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Airline">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAirline" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblAirline" runat="server" Text='<%#Eval("AirlineCode") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="Trip_Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripTypeName") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>--%>
                                                    </ItemTemplate>

                                                    <%--<EditItemTemplate>
                                                     <asp:DropDownList ID="Ddl_TripType" runat="server" Width="150px" DataTextField='<%#Eval("TripType")%>'>
                                                        <asp:ListItem Value="D" Text="Domestic" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="I" Text="International"></asp:ListItem>                                       
                                                    </asp:DropDownList>                                                      
                                                    </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fare_Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFareType" runat="server" Text='<%#Eval("FareTypeName") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblFareType" runat="server" Text='<%#Eval("FareType") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <%-- <EditItemTemplate>
                                                    <asp:DropDownList ID="Ddl_FareType" runat="server" Width="150px" DataTextField='<%#Eval("FareType")%>' >
                                                            <asp:ListItem Value="ALL" Text="ALL" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NRM" Text="Normal Fare"></asp:ListItem>
                                                            <asp:ListItem Value="CRP" Text="Corporate Fare"></asp:ListItem>
                                                            <asp:ListItem Value="CPN" Text="Coupon Fare"></asp:ListItem>
                                                    </asp:DropDownList>                                                     
                                                    </EditItemTemplate>--%>                                                  
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Comm_Basic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommisionOnBasic" runat="server" Text='<%#Eval("CommisionOnBasic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_CBasic" runat="server" Text='<%#Eval("CommisionOnBasic") %>'
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" Width="60px" MaxLength="5" BackColor="#ffff66"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="txt_CBasic1" ControlToValidate="txt_CBasic" runat="server" ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                           </EditItemTemplate>
                                                </asp:TemplateField>


                                                  <asp:TemplateField HeaderText="Comm_Basic_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommisionOnBasicstk" runat="server" Text='<%#Eval("CommisionOnBasic_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                         <asp:Label ID="txt_CBasich" runat="server" Text='<%#Eval("CommisionOnBasic_Stk") %>' 
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" Width="60px" MaxLength="5" BackColor="#ffff66"></asp:Label>
                                                           </EditItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Comm_YQ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommissionOnYq" runat="server" Text='<%#Eval("CommissionOnYq") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_CYQ" runat="server" Text='<%#Eval("CommissionOnYq") %>' Width="60px"
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" MaxLength="5" BackColor="#ffff66"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="txt_CYQ1" ControlToValidate="txt_CYQ" runat="server" ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                                          </EditItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Comm_YQ_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommissionOnYqstk" runat="server" Text='<%#Eval("CommissionOnYq_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                     
                                                         <asp:Label ID="txt_CYQh" runat="server" Text='<%#Eval("CommissionOnYq_Stk") %>' 
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" Width="60px" MaxLength="5" BackColor="#ffff66"></asp:Label>                          
                                                          </EditItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Comm(Basic+YQ)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommisionOnBasicYq" runat="server" Text='<%#Eval("CommisionOnBasicYq") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_CBasicYQ" runat="server" Text='<%#Eval("CommisionOnBasicYQ") %>'
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" Width="60px" MaxLength="5" BackColor="#ffff66"></asp:TextBox>

                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                   <asp:TemplateField HeaderText="Comm(Basic+YQ)_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommisionOnBasicYqstk" runat="server" Text='<%#Eval("CommisionOnBasicYq_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txt_CBasicYQh" runat="server" Text='<%#Eval("CommisionOnBasicYq_Stk") %>'
                                                            onKeyPress="return keyRestrict(event,'.0123456789');" Width="60px" MaxLength="5" BackColor="#ffff66"></asp:Label>
                                                        <%--<asp:RequiredFieldValidator ID="txt_CBasicYQ1" ControlToValidate="txt_CBasicYQ" runat="server" ValidationGroup="upd" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PLB_Basic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pbasic" runat="server" Text='<%#Eval("PlbOnBasic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_PBasic" runat="server" Text='<%#Eval("PlbOnBasic") %>' onKeyPress="return keyRestrict(event,'.0123456789');"
                                                            MaxLength="5" Width="60px" BackColor="#ffff66"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="txt_PBasic1" ControlToValidate="txt_PBasic" runat="server"
                                                            ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                 
                                                         </EditItemTemplate>
                                                </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="PLB_Basic_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pbasicstk" runat="server" Text='<%#Eval("PlbOnBasic_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        
                                                         
                                                         <asp:Label ID="txt_PBasich" runat="server" Text='<%#Eval("PlbOnBasic_Stk") %>' onKeyPress="return keyRestrict(event,'.0123456789');"  
                                                            MaxLength="5" Width="60px" BackColor="#ffff66"></asp:Label>
                                                         </EditItemTemplate>
                                                </asp:TemplateField>









                                                <asp:TemplateField HeaderText="PLB(Basic+YQ)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pbasicyq" runat="server" Text='<%#Eval("PlbOnBasicYq") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_PBasicYQ" runat="server" Text='<%#Eval("PlbOnBasicYq") %>' MaxLength="5" onKeyPress="return keyRestrict(event,'.0123456789');"
                                                            Width="60px" BackColor="#ffff66"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="txt_PBasicYQ1" ControlToValidate="txt_PBasicYQ" runat="server"
                                                            ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                                 
                                                         </EditItemTemplate>
                                                </asp:TemplateField>


                                                   <asp:TemplateField HeaderText="PLB(Basic+YQ)_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pbasicyqstk" runat="server" Text='<%#Eval("PlbOnBasicYq_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                       
                                                         <asp:Label ID="txt_PBasicYQh" runat="server" Text='<%#Eval("PlbOnBasicYq_Stk") %>' MaxLength="5" onKeyPress="return keyRestrict(event,'.0123456789');"  
                                                            Width="60px" BackColor="#ffff66"></asp:Label>
                                                        
                                                         </EditItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="CashbackAmount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CashBackAmount" runat="server" Text='<%#Eval("CashBackAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_CashBackAmt" runat="server" Text='<%#Eval("CashBackAmt") %>' MaxLength="5" onKeyPress="return keyRestrict(event,'.0123456789');"
                                                            Width="60px" BackColor="#ffff66"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="txt_CashBackAmt1" ControlToValidate="txt_CashBackAmt" runat="server"
                                                            ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                              
                                                         </EditItemTemplate>
                                                </asp:TemplateField>


                                                   <asp:TemplateField HeaderText="CashbackAmount_Stockist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CashBackAmountstk" runat="server" Text='<%#Eval("CashBackAmt_Stk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                      
                                                     <asp:Label ID="txt_CashBackAmth" runat="server" Text='<%#Eval("CashBackAmt_Stk") %>' MaxLength="5" onKeyPress="return keyRestrict(event,'.0123456789');" 
                                                            Width="60px" BackColor="#ffff66"></asp:Label>
                                                         </EditItemTemplate>
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="Booking_From_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingFromDate" runat="server" Text='<%#Eval("BookingFromDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_BookingFromDate" runat="server" ReadOnly="true" Text='<%#Bind("BookingFromDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking_To_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingToDate" runat="server" Text='<%#Eval("BookingToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_BookingToDate" ReadOnly="true" runat="server" Text='<%#Bind("BookingToDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Onward_From_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOnwardTravelFromDate" runat="server" Text='<%#Eval("OnwardTravelFromDate") %>' Style="text-wrap: inherit;"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_OnwardTravelFromDate" ReadOnly="true" runat="server" Text='<%#Bind("OnwardTravelFromDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Onward_To_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOnwardTravelToDate" runat="server" Text='<%#Eval("OnwardTravelToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_OnwardTravelToDate" ReadOnly="true" runat="server" Text='<%#Bind("OnwardTravelToDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return_From_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReturnTravelFromDate" runat="server" Text='<%#Eval("ReturnTravelFromDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_ReturnTravelFromDate" ReadOnly="true" runat="server" Text='<%#Bind("ReturnTravelFromDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return_To_Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReturnTravelToDate" runat="server" Text='<%#Eval("ReturnTravelToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_ReturnTravelToDate" ReadOnly="true" runat="server" Text='<%#Bind("ReturnTravelToDate") %>' Width="70px" MaxLength="14"
                                                            CssClass="date" BackColor="#ffff66"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="txt_StartDate1" ControlToValidate="txt_StartDate" runat="server"
                                                        ErrorMessage="Required" ValidationGroup="upd" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                                            ErrorMessage="Please enter validate Date"
                                                            ControlToValidate="txt_StartDate"
                                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />    --%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Restriction_On">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRestrictionOn" runat="server" Text='<%#Eval("RestrictionOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <EditItemTemplate>
                                                        <asp:DropDownList Enabled="False" ID="ddl_RestrictionOn" runat="server" Width="150px" DataValueField='<%#Eval("RestrictionOn")%>' SelectedValue='<%#Eval("RestrictionOn")%>'>
                                                            <asp:ListItem Value="BOTH" Text="BOTH"></asp:ListItem>
                                                            <asp:ListItem Value="COMM" Text="COMMISSION"></asp:ListItem>
                                                            <asp:ListItem Value="PLB" Text="PLB"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>  
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="PPP_Segement/Sector">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PPPType" runat="server" Text='<%#Eval("PPPType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <EditItemTemplate>                                                                                                                
                                                                <asp:DropDownList Enabled="False" ID="Ddl_PPPType" runat="server" Width="150px" DataValueField='<%#Eval("PPPType")%>' SelectedValue='<%#Eval("PPPType")%>'>                                       
                                                                 <asp:ListItem Value="PPPSEGMENT" Text="PPP Segment"></asp:ListItem>
                                                                 <asp:ListItem Value="PPPSECTOR" Text="PPP Sector"></asp:ListItem>
                                                                </asp:DropDownList>
                                                    </EditItemTemplate>  
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Cabin_Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCabinClassInclude" runat="server" Text='<%#Eval("CabinClassInclude")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_PLBClass" runat="server" Width="150px" DataTextField='<%#Eval("CabinClassInclude")%>'>
                                                            <asp:ListItem Value="All">--All--</asp:ListItem>
                                                           <asp:ListItem Value="C">Business</asp:ListItem>
                                                            <asp:ListItem Value="Y">Economy</asp:ListItem>
                                                            <asp:ListItem Value="F">First</asp:ListItem>
                                                            <asp:ListItem Value="W">Premium Economy</asp:ListItem>
                                                            <asp:ListItem Value="P">Premium First</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>  --%>                                            
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="Booking_Channel">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingChannel" runat="server" Text='<%#Eval("BookingChannel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <%-- <asp:TemplateField HeaderText="BookingClassInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingClassInclude" runat="server" Text='<%#Eval("BookingClassInclude") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BookingClassExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingClassExclude" runat="server" Text='<%#Eval("BookingClassExclude")%>'></asp:Label>
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FareBasisInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFareBasisInclude" runat="server" Text='<%#Eval("FareBasisInclude")%>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="FareBasisExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFareBasisExclude" runat="server" Text='<%#Eval("FareBasisExclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrginAirportInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrginAirportInclude" runat="server" Text='<%#Eval("OrginAirportInclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="OrginAirportExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrginAirportExclude" runat="server" Text='<%#Eval("OrginAirportExclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DestinationAirportInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDestinationAirportInclude" runat="server" Text='<%#Eval("DestinationAirportInclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DestinationAirportExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDestinationAirportExclude" runat="server" Text='<%#Eval("DestinationAirportExclude")%>'></asp:Label>
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FlightNoInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlightNoInclude" runat="server" Text='<%#Eval("FlightNoInclude")%>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="FlightNoExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlightNoExclude" runat="server" Text='<%#Eval("FlightNoExclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="OperatingCarrierInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOperatingCarrierInclude" runat="server" Text='<%#Eval("OperatingCarrierInclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OperatingCarrierExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOperatingCarrierExclude" runat="server" Text='<%#Eval("OperatingCarrierExclude") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MarketingCarrierInclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMarketingCarrierInclude" runat="server" Text='<%#Eval("MarketingCarrierInclude")%>'></asp:Label>
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>
                                                                                           
                                                <asp:TemplateField HeaderText="CabinClassExclude">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCabinClassExclude" runat="server" Text='<%#Eval("CabinClassExclude") %>'></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created_By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField> --%>
                                                <asp:TemplateField HeaderText="Created_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EDIT">
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnledit" runat="server" Text="Edit" CommandName="Edit" Font-Bold="true"
                                                            CssClass="newbutton_2" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%-- <asp:Button ID="lnlupdate" runat="server" Text="Update" CommandName="Update" Font-Bold="true" ValidationGroup="upd"
                                                            CssClass="newbutton_2" />--%>
                                                        <asp:Button ID="lnlupdate" runat="server" Text="Update" CommandName="Update" Font-Bold="true" OnClientClick="javascript:return requiredfiled(this.id);" CssClass="newbutton_2" />
                                                        <asp:Button ID="lnlcancel" runat="server" Text="Cancel" CommandName="Cancel" Font-Bold="true"
                                                            CssClass="newbutton_2" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="btn_delete" CssClass="newbutton_2" runat="server" Text="Delete" CommandName="Delete"
                                                            OnClientClick="return ConfirmDelete()" Font-Bold="true" />  --%>
                                                        <asp:Button ID="btn_delete" CssClass="newbutton_2" runat="server" Text="Delete" CommandName="Delete" Font-Bold="true" />
                                                    </ItemTemplate>
                                                    <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <EmptyDataTemplate>No records Found</EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                                        </div>
                                        <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                            Please Wait....<br />
                                            <br />
                                            <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                            <br />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>

    <script type="text/javascript">
        $(function () {
            $("#From").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true, onSelect: UpdateToDate
                   }
              )

        });
        $(function () {
            $("#To").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                   }
              )

        });
        function UpdateToDate(dateText, inst) {
            $("#To").datepicker("option", { minDate: dateText });
        }


        $(function () {
            $("#OTFromDate").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true, onSelect: UpdateOTToDate
                   }
              )

        });
        $(function () {
            $("#OTToDate").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                   }
              )

        });
        function UpdateOTToDate(dateText, inst) {
            $("#OTToDate").datepicker("option", { minDate: dateText });
        }


        $(function () {
            $("#RTFromDate").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true, onSelect: UpdateRTToDate
                   }
              )

        });
        $(function () {
            $("#RTToDate").datepicker(
                   {
                       numberOfMonths: 1,
                       autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                       changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                       selectOtherMonths: true, showoff: "button", buttonImageOnly: true, onSelect: UpdateRTToDate
                   }
              )

        });
        function UpdateRTToDate(dateText, inst) {
            $("#RTToDate").datepicker("option", { minDate: dateText });
        }
    </script>

    <script type="text/javascript">


        function allType() {
            $('#<%=HiddenAlltype.ClientID %>').val("");
            if ($("#CheckBoxAllType").is(":checked")) {

                $('#<%=ddl_ptype.ClientID %>').hide();
                $('#<%=HiddenAlltype.ClientID %>').val("0");

            }
            else {
                $('#<%=ddl_ptype.ClientID %>').show();
                $('#<%=HiddenAlltype.ClientID %>').val("");
            }
        }
    </script>

    <script type="text/javascript">
        $("#hide").click(function () {
            $("#divMoreFilter").hide();
            $("#hide").hide();
            $("#show").show();
        });
        $("#show").click(function () {
            $("#divMoreFilter").show();
            $("#hide").show();
            $("#show").hide();
        });

    </script>
    <script type="text/javascript">
        function pageLoad() {
            $('.date').unbind();
            $('.date').datepicker(
                 {
                     numberOfMonths: 1,
                     autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                     changeYear: false, hideIfNoPrevNext: false, maxDate: '1y', minDate: 0, navigationAsDateFormat: true, defaultDate: +0, showAnim: 'toggle', showOtherMonths: true,
                     selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                 }
                );
        }


    </script>
</asp:Content>

