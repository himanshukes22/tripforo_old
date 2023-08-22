<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="FeedSeriesDetails.aspx.vb" Inherits="SprReports_OLSeries_FeedSeriesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script src="JS/JScript.js" type="text/javascript"></script>

    <style>
        input[type="text"], input[type="password"], select, textarea
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>
    <%--<link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>

    <script src="JS/JScript.js" type="text/javascript"></script>

    <div>
        <table cellpadding="0" cellspacing="0" align="center" style="margin: auto" width="100%">
            <tr>
                <td id="seriesDetailsTD" runat="server">
                    <%--<fieldset style="border: thin solid #004b91; font-family: arial, Helvetica, sans-serif; ">
                        <legend style="font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91" >
                            Insert Series Details</legend>--%>
                    <div id="mail" runat="server">
                        <table cellpadding="0" cellspacing="20" align="center" class="tbltbl">
                            <tr>
                                <td class="Heading" colspan="4" height="25" align="center">
                                    Feed Series Departure
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Airline Name :
                                </td>
                                <td>
                                    <input id="txtairline" type="text" name="txtairline" value="" class="txt" />
                                </td>
                                <td>
                                    Sector :
                                </td>
                                <td>
                                    &nbsp;<input id="txtsector" type="text" class="txt" name="txtsector" value="" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Available Seat:
                                </td>
                                <td>
                                    &nbsp;<input id="txtseat" onkeypress="return ValidateNoPax(event);" type="text" class="txt"
                                        name="txtseat" value="" />
                                </td>
                                <td>
                                    Amount:
                                </td>
                                <td>
                                    <input id="txtamount" onkeypress="return ValidateNoPax(event);" type="text" class="txt"
                                        name="txtamount" value="" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;Departure Date:
                                </td>
                                <td>
                                    &nbsp;<input type="text" name="From" id="From" readonly="readonly" style="width: 100px" />
                                </td>
                                <td>
                                    Return Date:
                                </td>
                                <td>
                                    &nbsp;<input type="text" name="To" id="To" readonly="readonly" style="width: 100px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Itinerary/Inclusions :
                                </td>
                                <td>
                                    &nbsp;<textarea id="txtaircode" type="text" class="txt" name="txtaircode" value=""
                                        style="width: 300px; height: 80px"></textarea>
                                </td>
                                <td>
                                    Trip:
                                </td>
                                <td style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;
                                    color: #FFFFFF; font-style: normal;" valign="middle">
                                    <asp:DropDownList ID="DdlTrip" runat="server" Width="100px">
                                        <asp:ListItem Value="D">Domestic</asp:ListItem>
                                        <asp:ListItem Value="I">International</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="button" OnClientClick="return Validate()" />
                                    &nbsp;<asp:Button ID="btn_modify" runat="server" CssClass="button" Text="Modify Details " />
                                    &nbsp;<asp:Button ID="btn_Offline" runat="server" CssClass="button" Text="Offline Request" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--</fieldset>--%>
                </td>
            </tr>
            <tr>
                <td headers="30px" style="padding-left: 10px">
                    &nbsp;
                </td>
            </tr>
            <tr align="center" id="grdTD" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="10" width="80%" align="center" class="tbltbl">
                        <tr>
                            <td>
                                <table cellspacing="3" cellpadding="3" align="center" class="tbltbl">
                                    <tr>
                                        <td align="left" style="color: #004b91; font-size: 13px; font-weight: bold">
                                            Search Series Departure
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="90" style="font-weight: bold" height="25">
                                                        From Date
                                                    </td>
                                                    <td width="150" align="left">
                                                        <input type="text" name="Depart" id="Depart" readonly="readonly" style="width: 100px" />
                                                    </td>
                                                    <td width="90" style="font-weight: bold">
                                                        To Date
                                                    </td>
                                                    <td width="150px" align="left">
                                                        <input type="text" name="Dest" id="Dest" readonly="readonly" style="width: 100px" />
                                                    </td>
                                                    <td style="font-weight: bold">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td id="Td1" width="90" style="font-weight: bold" height="25" runat="server">
                                                        Airline
                                                    </td>
                                                    <td id="Td2" width="130" runat="server">
                                                        <asp:DropDownList ID="ddl_Airline" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td id="agencyrow" runat="server" colspan="1" width="380">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td width="90" style="font-weight: bold">
                                                                    Sector
                                                                </td>
                                                                <td align="left">
                                                                    <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="ddl_sector" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button" />&nbsp;<asp:Button
                                                            ID="btn_close" runat="server" Text="Cancel" CssClass="button" Visible="false" />
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="Heading" colspan="4" height="25" align="center">
                                Modify Series Departure
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grd_Seriesflight" runat="server" AutoGenerateColumns="false" DataKeyNames="Counter"
                                            CssClass="GridViewStyle" Width="100%" ForeColor="#333333">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Airline">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblairline" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sector">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_sector" runat="server" Text='<%#Eval("Sector") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_amount" onkeypress="return ValidateNoPax(event);" runat="server"
                                                            Text='<%#Eval("Amount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Available Seats">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblavlseats" runat="server" Text='<%#Eval("Available_Seat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtavlseats" onkeypress="return ValidateNoPax(event);" runat="server"
                                                            Text='<%#Eval("Available_Seat") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hold Seats">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblholdseats" runat="server" Text='<%#Eval("Hold_Seat")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sold Seats">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsoldseats" runat="server" Text='<%#Eval("Confirm_Seat")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Itinerary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblaircode" runat="server" Text='<%#Eval("Airline_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_aircode" runat="server" Text='<%#Eval("Airline_Code") %>' TextMode="MultiLine"
                                                            Width="300px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Departure Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_deptdate" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_retdate" runat="server" Text='<%#Eval("Ret_date") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Requested Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblreqdate" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modify">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_book" runat="server" CommandName="Edit" Text="Edit" CssClass="newbutton_2" />
                                                        <asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirmdelete();"
                                                            CssClass="newbutton_2" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_update" runat="server" CommandName="Update" Text="Update" CssClass="newbutton_2" />
                                                        <asp:Button ID="btn_cancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="newbutton_2" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <HeaderStyle CssClass="HeaderStyle" Height="35px" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                            padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                            z-index: 1000;">
                                        </div>
                                        <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                            z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                            font-weight: bold; color: #000000">
                                            Please Wait....<br />
                                            <br />
                                            <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                            <br />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 10px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="Offline_ReqTD" runat="server" align="center" visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" class="Heading" style="padding-bottom: 5px">
                                Offline Request
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 5px">
                                <asp:Button ID="cancelhold" runat="server" Text="Cancel" CssClass="button" />
                            </td>
                        </tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd_OfflineReq" runat="server" AutoGenerateColumns="false" DataKeyNames="Counter"
                                        CssClass="GridViewStyle" Width="90%" ForeColor="#333333">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcounter" runat="server" Text='<%#Eval("Counter")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("ExecId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Airline">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAirline" runat="server" Text='<%#Eval("AirlineName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Itinerary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAirlinecode" runat="server" Text='<%#Eval("Airline_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sector">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSector" runat="server" Text='<%#Eval("Sector")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Avaliable Seat">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAvlSeats" runat="server" Text='<%#Eval("Available_Seat")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepdate" runat="server" Text='<%#Eval("Depart_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ret Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHold" runat="server" onkeypress="return ValidateNoPax(event);"
                                                        Width="60px" Visible="false"></asp:TextBox>
                                                    <%-- <asp:Label ID="lblDepdate" runat="server" Text='<%#Eval("Hold_Seat")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Confirm">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtConfirm" onkeypress="return ValidateNoPax(event);" runat="server"
                                                        Width="60px" Visible="false"></asp:TextBox>
                                                    <%-- <asp:Label ID="lblConfirm" runat="server" Text='<%#Eval("Confirm_Seat")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="300px" Visible="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqDate" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold/Confirm">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnHold" runat="server" Text="Hold" CommandName="Hold" CssClass="newbutton_2" />
                                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CommandName="Confirm" CssClass="newbutton_2" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CommandName="Offlineupdate"
                                                        CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" CommandName="Cancel"
                                                        CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" />
                                                    <asp:Button ID="btnConfirmUpdate" runat="server" Text="Update" Visible="false" CommandName="ConfirmUpdate"
                                                        CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" />
                                                    <asp:Button ID="btnConfirmCancel" runat="server" Text="Cancel" Visible="false" CommandName="ConfirmCancel"
                                                        CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" Height="35px" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                        padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                        z-index: 1000;">
                                    </div>
                                    <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                        z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                        font-weight: bold; color: #000000">
                                        Please Wait....<br />
                                        <br />
                                        <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                        <br />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
            </tr>
        </table>
        </td> </tr> </table>
    </div>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <%--<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>--%>
    <%--<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/JCalender.js") %>"></script>--%>

    <script type="text/javascript">

        $(function() {
            $("#From").datepicker(
                 { numberOfMonths: 1,

                     showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                     changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                     selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                 }
            ).datepicker("setDate", new Date().getDate - 1);

        });
        $(function() {
            $("#To").datepicker(
                 { numberOfMonths: 1,

                     showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                     changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                     selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                 }
            ).datepicker("setDate", new Date().getDate - 1);

        });
        $(function() {
            $("#Depart").datepicker(
                 { numberOfMonths: 1,

                     showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                     changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                     selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                 }
            ).datepicker("setDate", new Date().getDate - 1);

        });
        $(function() {
            $("#Dest").datepicker(
                 { numberOfMonths: 1,

                     showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                     changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                     selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                 }
            ).datepicker("setDate", new Date().getDate - 1);

        });
    </script>

</asp:Content>
