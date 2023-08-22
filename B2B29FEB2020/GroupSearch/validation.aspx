<%@ Page Language="C#" AutoEventWireup="true" CodeFile="validation.aspx.cs" Inherits="validation" MasterPageFile="~/MasterAfterLogin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <style type="text/css">
        input[type="text"], input[type="password"], select, textarea {
               background: #ffffff !important;
    color: #525865;
    border-radius: 4px;
    border: 1px solid #d1d1d1;
    box-shadow: inset 1px 2px 8px rgba(0, 0, 0, 0.10);
    font-family: Tahoma !important;
    font-size: 1em;
    height: 40px;
    width: 100%;
    line-height: 1.45;
    outline: none;
    padding: 0.1em 1.00em 0.2em;
    -webkit-transition: .18s ease-out;
    -moz-transition: .18s ease-out;
    -o-transition: .18s ease-out;
    transition: .18s ease-out;
        }

        .txtBox {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }

        .txtCalander {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }

        .main {
            /*background-color: #18184b;*/
            padding: 3px;
            width: 994px;
            margin-right: auto;
            margin-left: auto;
            color: #000;
            line-height: 150%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        /*.main td
        {
            border: thin solid #cce8fe;
            border-collapse: collapse;
            padding: 5px;
        }*/ .txtCalander {
            width: 100px;
            background-image: url(images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }

        .link {
            padding: 10px 25px;
            color: #fff;
            background-color: #e0136f;
            text-decoration: none;
            margin-right: 2px;
            border-radius: 5px;
            border-bottom-radius: none;
            border: thin solid #fff;
            border-bottom: thin solid #cce8fe;
        }

            .link:hover {
                background-color: #18184b;
            }

        .heading {
            color: #fff;
            font-size: 20px;
            font-weight: bold;
            font-family: "Times New Roman", Times, serif;
            background-image: url(bg-tab.gif);
            background-repeat: repeat-x;
            padding-left: 35px;
        }

        .style3 {
            width: 149px;
        }

        .style4 {
            height: 39px;
        }

        .main table th {
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            text-align: left;
        }

        .main table td {
            padding: 5px;
        }
    </style>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
     
    </script>
    <script type="text/javascript">
        $(".link").click(function (e) {
            $("#div_Progress").show();
        });
    </script>



    <div style="background-color: #eee;">
        <div style="margin-top: 30px;"></div>
        <div class="">
            <div class="large-12 medium-12 small-12" style="border: 1px solid #ccc;">
                <div>
                    <div class="large-12 medium-1 small-1 headbgs">
                        <span id="ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_pttextADT"><i class="fa fa-plane" aria-hidden="true"></i>Flight Details</span>
                    </div>
                    <div style="float: right; margin-right: 100px; font-weight: bold; color: Red; font-size: 14px;" id="divUname" runat="server">
                    </div>
                    <div style="clear: both;">
                    </div>

                    <input type="hidden" id="hidtxtDepCity1" name="nmofhidtxtDepCity1" />
                    <input type="hidden" id="hidtxtArrCity1" name="nmofhidtxtArrCity1" />
                    <input type="hidden" id="hdnUserId" name="hdnUserId" value='<%=Session["UID"] %>' />
                    <input type="hidden" id="hdnUserType" name="nmOfhdnUserType" value='<%=Session["User_Type"] %>' />
                    <input type="hidden" id="hdnAilines" name="hdnAilines" />

                    <table id="tblMainGroupFrame" class="table">

                        <tr>
                            <td bgcolor="#FFFFFF" style="height: 27px;">
                                <asp:RadioButton ID="rbtnDomestic" Enabled="false" runat="server" Text="Domestic" GroupName="grpFlight"
                                    ToolTip="Domestic Flight" Style="cursor: pointer; font-size: 12px;" CssClass="ClsTripTravelType" />&nbsp;
                    <asp:RadioButton ID="rbtnInternational" Enabled="false" runat="server" Text="International" GroupName="grpFlight" CssClass="ClsTripTravelType"
                        ToolTip="International Flight" Style="cursor: pointer; font-size: 12px;" />
                            </td>
                            <td colspan="2" bgcolor="#FFFFFF">
                                <asp:RadioButton ID="rbtnOneWay" Enabled="false" CssClass="ClsOneWayOrMulty" runat="server" Text="One Way" GroupName="GrpTpType" Style="cursor: pointer; font-size: 12px;" ToolTip="One Way" />&nbsp;
                    <asp:RadioButton ID="rbtnRoundTrip" Enabled="false" runat="server" Text="Round Trip" GroupName="GrpTpType" Style="cursor: pointer; font-size: 12px;" ToolTip="Round Trip" />&nbsp;
                    <asp:RadioButton ID="rbtnMultiCity" runat="server" GroupName="GrpTpType" ToolTip="Multi City Flight"
                        Text="Multi City" Style="cursor: pointer; font-size: 12px;" Visible="False" />
                            </td>
                            <td colspan="4" bgcolor="#FFFFFF">Booking Name*<asp:TextBox ID="txt_Bookingname" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="OneWayTrip" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                            PageSize="100" OnRowDataBound="OneWayTrip_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Request">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lbl_requestid" runat="server" ReadOnly="true" Text='<%#Eval("Track_ID")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Trip From*" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OFrom" ReadOnly="true" runat="server" CssClass="CityName" Width="150px" Text='<%#Eval("DepartureCityName").ToString() +"( "+ Eval("DepartureLocation").ToString() +")" %>'></asp:TextBox>
                                                        <asp:Label ID="lbl_ODepAirportName" runat="server" Text='<%#Eval("DepAirportCode")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_odeploction" runat="server" Text='<%#Eval("DepartureLocation")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Trip To*">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OTo" ReadOnly="true" runat="server" CssClass="CityName" Width="150px" Text='<%#Eval("ArrivalCityName").ToString() +"( "+ Eval("ArrivalLocation").ToString() +")" %>'></asp:TextBox>
                                                        <asp:Label ID="lbl_OArvlAirportName" runat="server" Text='<%#Eval("ArrAirportCode")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_ArrivalLocation" runat="server" Text='<%#Eval("ArrivalLocation")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Departure Date*">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_ODepDate" ReadOnly="true" Enabled="false" runat="server" Text='<%#Eval("DepartureDate")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_ODepTime" ReadOnly="true" onkeypress="return NumericOnly(event)" runat="server" Text='<%#Eval("DepartureTime")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Arrival Date*">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OArvlDate" ReadOnly="true" Enabled="false" runat="server" Text='<%#Eval("ArrivalDate")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OArvlTime" ReadOnly="true" onkeypress="return NumericOnly(event)" runat="server" Text='<%#Eval("ArrivalTime")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Airline">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OAirline" ReadOnly="true" runat="server" CssClass="AirlineName" Text='<%#Eval("AirlineName")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Flight No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_OFlightNo" ReadOnly="true" runat="server" Text='<%#Eval("FlightIdentification")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_OStops" runat="server" Text='<%#Eval("Stops")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_OTripType" runat="server" Text='<%#Eval("Trip")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_OSector" runat="server" Text='<%#Eval("Sector")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_Oflight" runat="server" Text='<%#Eval("FlightStatus")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_Oflt" runat="server" Text='<%#Eval("Flight")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_ORequestID" runat="server" Text='<%#Eval("Track_id")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_Provider" runat="server" Text='<%#Eval("Provider")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_sno" runat="server" Text='<%#Eval("sno")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_vc" runat="server" Text='<%#Eval("ValiDatingCarrier")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="RoundTrip" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                PageSize="30" OnRowDataBound="RoundTrip_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Inbond Trip From*" FooterStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RFrom" runat="server" CssClass="CityName" Width="300px" Text='<%#Eval("DepartureCityName").ToString() +"( "+ Eval("DepartureLocation").ToString() +")" %>'></asp:TextBox>
                                            <asp:Label ID="lbl_RDepAirportName" runat="server" Text='<%#Eval("DepAirportCode")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inbond Trip To*">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RTo" runat="server" CssClass="CityName" Width="300px" Text='<%#Eval("ArrivalCityName").ToString() +"( "+ Eval("ArrivalLocation").ToString() +")" %>'></asp:TextBox>
                                            <asp:Label ID="lbl_RArvlAirportName" runat="server" Text='<%#Eval("ArrAirportCode")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Date*">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RDepDate" runat="server" CssClass="date" Text='<%#Eval("DepartureDate")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RDepTime" CssClass="my_time" runat="server" Text='<%#Eval("DepartureTime")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arrival Date*">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RArvlDate" runat="server" CssClass="date" Text='<%#Eval("ArrivalDate")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RArvlTime" CssClass="my_time" runat="server" Text='<%#Eval("ArrivalTime")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Airline">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RAirline" runat="server" CssClass="AirlineName" Text='<%#Eval("AirlineName")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flight No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_RFlightNo" runat="server" Text='<%#Eval("FlightIdentification")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_RStops" runat="server" Text='<%#Eval("Stops")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_RTripType" runat="server" Text='<%#Eval("Trip")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_RSector" runat="server" Text='<%#Eval("Sector")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_Rflight" runat="server" Text='<%#Eval("FlightStatus")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_Rflt" runat="server" Text='<%#Eval("Flight")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_RRequestID" runat="server" Text='<%#Eval("Track_id")%>' Visible="false"></asp:Label>
                                            <%--<asp:Label ID="lbl_Provider" runat="server" Text='<%#Eval("Provider")%>' Visible="false"></asp:Label>--%>
                                <%--</ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr>
                            <%-- <th>Probable Days
                </th>--%>
                            <th>No Of Adult*
                            </th>
                            <th>No Of Child
                            </th>
                            <th>No Of Infant
                            </th>
                            <%-- <th>Date Of Confirmation
                </th>--%>
                            <th>Expected Fare*
                            </th>
                            <th colspan="1">Remarks
                            </th>
                        </tr>
                        <tr>
                            <%-- <td>
                    <select name="nmOfsltProbableDays" id="sltProbableDays" runat="server">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                    </select>
                </td>--%>
                            <td>
                                <input name="nmOftxtNoOfPsgr" type="text" id="txtNoOfAdult" size="4" maxlength="2" onkeypress="return NumericOnly(event);" runat="server" />
                            </td>
                            <td>
                                <input name="nmOftxtNoOfPsgr" type="text" id="txtNoOfChild" size="4" maxlength="2" onkeypress="return NumericOnly(event);" runat="server" />
                            </td>
                            <td>
                                <input name="nmOftxtNoOfPsgr" type="text" id="txtNoOfInfant" size="4" maxlength="2" onkeypress="return NumericOnly(event);" runat="server" />
                            </td>
                            <%-- <td>
                    <input type="text" id="txtCfmDate" name="nmOFtxtCfmDate" maxlength="25" size="25" runat="server" readonly="readonly" />
                </td>--%>
                            <td>
                                <input type="text" id="txtExpectedFair" name="nmOftxtExpectedFair" onkeypress="return NumericOnly(event);" maxlength="12" runat="server" />
                            </td>
                            <td colspan="1">
                                <textarea name="textarea" id="textarea1" cols="60" rows="5" style="width: 200px; height: 50px" runat="server"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">
                                <%--                    <input type="button" name="nmOFbtnSmtDet" id="btnSmtDet" value="Submit" class="link" />--%>
                     &nbsp;<asp:Button ID="bnt_Submit" runat="server" Text="Submit" class="buttonfltbk" OnClick="bnt_Submit_Click" OnClientClick="return Validate();" Width="100px" />
                            </td>
                        </tr>
                    </table>


                    <div id="waitMessage" style="display: none;">
                        <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; /* border: 5px solid #d1d1d1; */ /* border-radius: 10px; */">
                            <div style="position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
                                Please wait....<br />
                                <br />
                                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
        <div class="clear1"></div>
    </div>






    <br />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script src="../scripts/jquery.js" type="text/javascript"></script>
    <script src="../scripts/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../scripts/json2.js" type="text/javascript"></script>
    <script src="../Scripts/JS-Book-A-Flight.js" type="text/javascript"></script>

    <script type="text/javascript">

        $('form input').each(function () {
            this.setAttribute('value', this.value);
            if (this.checked)
                this.setAttribute('checked', 'checked');
            else
                this.removeAttribute('checked');
        });

        $('form select').each(function () {
            var index = this.selectedIndex;
            var i = 0;
            $(this).children('option').each(function () {
                if (i++ != index)
                    this.removeAttribute('selected');
                else
                    this.setAttribute('selected', 'selected');
            });
        });

        $('form textarea').each(function () {
            $(this).html($(this).val());
        });

        var aircode = $('.CityName').each(function () {
            $(this).autocomplete({
                source: function (e, t) {
                    $.ajax({
                        url: UrlBase + "CitySearch.asmx/FetchCityList",
                        data: "{ 'city': '" + e.term + "', maxResults: 10 }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (e) {
                            t($.map(e.d, function (e) {
                                var t = e.CityName + "(" + e.AirportCode + ")";
                                var n = e.AirportCode + "," + e.CountryCode;
                                return {
                                    label: t,
                                    value: t,
                                    id: n
                                }
                            }))
                        },
                        error: function (e, t, n) {
                            alert(t)
                        }
                    })
                },
                autoFocus: true,
                minLength: 3,
                select: function (t, n) {
                    $(this).next().val(n.item.id)
                }
            });
        });
        var aircode = $('.AirlineName').each(function () {
            $(this).autocomplete({
                source: function (e, t) {
                    $.ajax({
                        url: UrlBase + "CitySearch.asmx/FetchAirlineList",
                        data: "{ 'airline': '" + e.term + "', maxResults: 10 }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (e) {
                            t($.map(e.d, function (e) {
                                var t = e.ALName + "(" + e.ALCode + ")";
                                var n = e.ALCode;
                                return {
                                    label: t,
                                    value: t,
                                    id: n
                                }
                            }))
                        },
                        error: function (e, t, n) {
                            alert(t)
                        }
                    })
                },
                autoFocus: true,
                minLength: 3,
                select: function (t, n) {
                    $(this).next().val(n.item.id)
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({
                dateFormat: 'dd/mm/yy',
                minDate: 0,
            });
        });
    </script>

    <script language="javascript" type="text/javascript">
        function Validate() {
            //  
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Bookingname").value == "") {
                alert('Booking name can not be blank,Please fill the booking name');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Bookingname").focus();
                return false;
            }
            if ((document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfAdult").value == "") || (document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfAdult").value == 0)) {
                alert('Adult can not be blank or 0,Please fill right no of pax');
                document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfAdult").focus();
                return false;
            }
            if ((document.getElementById("ctl00_ContentPlaceHolder1_txtExpectedFair").value == "") || (document.getElementById("ctl00_ContentPlaceHolder1_txtExpectedFair").value == 0)) {
                alert('Fare can not be blank or 0,Please fill correct fare');
                document.getElementById("ctl00_ContentPlaceHolder1_txtExpectedFair").focus();
                return false;
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_textarea1").value == "") {
                alert('Remarks can not be blank,Please fill remarks');
                document.getElementById("ctl00_ContentPlaceHolder1_textarea1").focus();
                return false;
            }
            $("#waitMessage").show();
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //  
            var btnclk = 0;
            $("#tblMainGroupFrame1").click(function (e) {
                var elem = document.getElementById('tblMainGroupFrame').getElementsByTagName("input");
                var Outbound = parseInt(0);

                for (var i = 0; i < elem.length; i++) {


                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OFrom") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RFrom") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Outbond Trip From,can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 3 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OTo") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RTo") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Outbond Trip To,can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 3 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ODepDate") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RDepDate") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Departure date can not be blank ', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ODepTime") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RDepTime") > 0) {
                        Outbound++;
                        if (elem[i].value == "" || elem[i].value.length > 5) {
                            alert('Please fill the Departure time or Departure time is Invalid format', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OArvlDate") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RArvlDate") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Arrival date can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OArvlTime") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RArvlTime") > 0) {
                        Outbound++;
                        if (elem[i].value == "" || elem[i].value.length > 5) {
                            alert('Please fill the arrival time or arrival time is Invalid format', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OAirline") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RAirline") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('airline can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 2 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_OFlightNo") > 0 || elem[i].type == "text" && elem[i].id.indexOf("txt_RFlightNo") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Flight no can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }

                }


            });
        });
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            try {
                var charCode = (event.keyCode ? event.keyCode : event.which);
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            catch (exception) {
            }
        }
        function NumericOnly(event) {
            try {
                var charCode = (event.keyCode ? event.keyCode : event.which);
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            catch (exception) {
            }
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Your Request has been submitted!!");
                    window.opener.location.reload('GroupDetails.aspx')
                    window.close();
                }
                    break;
            }
        }
    </script>
</asp:Content>
