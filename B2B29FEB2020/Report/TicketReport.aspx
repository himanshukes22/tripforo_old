<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketReport.aspx.vb" Inherits="SprReports_TicketReport" MasterPageFile="~/MasterPageForDash.master" Title="" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/CSS/PopupStyle.css?V=1")%>" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/Scripts/ReissueRefund.js?v=1")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PopupScript.js?V=1")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScriptsPNRCancellation/CancelPNR.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Flight/jquery.blockUI.js")%>"></script>

    <link type="text/css" href="<%=ResolveUrl("~/Content/Popup.css")%>" rel="stylesheet" />
    <link type="text/css" href="<%=ResolveUrl("~/Content/CancelPNR.css")%>" rel="stylesheet" />


    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }
    </style>


    <style type="text/css">
        a, p, li, td, span {
            font-size: 12.5px !important;
        }
    </style>







    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">My Bookings</a></li>

        <div style="float: right; display: none;">

            <li style="font-weight: 600; color: #fff;">
                <span>Total Ticket Sale :</span>
                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
            </li>
            <li style="font-weight: 600; color: #fff;">
                <span>Total Ticket Issued :</span>
                <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
            </li>
        </div>
    </ol>



    <div class="card-main">

        <div class="card-body report">
            <div class="inner-box">
                <div class="row">
                    <div class="col-md-3">
                        <%--                     <label>From</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="From" id="From" placeholder="From Date" class="theme-search-area-section-input" readonly="readonly" />
								<asp:HiddenField ID="hdnFromDate" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="To" placeholder="To Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
								 <asp:HiddenField ID="hdnToDate" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3" id="tdTripNonExec2" runat="server">
                        <%--<label>Trip Type</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <asp:DropDownList class="theme-search-area-section-input" ID="ddlTripDomIntl" runat="server">
                                    <asp:ListItem Value="">Select Trip Type</asp:ListItem>
                                    <asp:ListItem Value="D">Domestic</asp:ListItem>
                                    <asp:ListItem Value="I">International</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <%--<label>Order ID</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                                <asp:TextBox ID="txt_OrderId" placeholder="Enter Order Id" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">

                    <div class="col-md-3">
                        <%--<label>PNR</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <%--<label>Ticket No</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:TextBox ID="txt_TktNo" placeholder="Enter TicketNo" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <%--<label>Pax Name</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                <asp:TextBox ID="txt_PaxName" placeholder="Enter Pax Name" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <%--<label>Airline</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                                <asp:TextBox ID="txt_AirPNR" placeholder="Enter Airline" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-3" id="td_Agency" runat="server">
                        <%-- <label>Agency Name/ID</label>--%>
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" class="theme-search-area-section-input" id="txtAgencyName" placeholder="Enter Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                                    onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="btn-search">
                            <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>

                            <%-- <button id="btn_result" runat="server" class="btn cmn-btn"><i class="theme-search-area-section-icon lin lin-magnifier"></i> Search</button>--%>

                            <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>
                        </div>


                    </div>



                </div>

                <%--   <hr />--%>
            </div>
        </div>

    </div>

    <div id="toPopupReport" class="tbltbl large-12 medium-12 small-12">
        <div class="close">
        </div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content">

            <table border="0" cellpadding="10" cellspacing="5" style="font-family: arial, Helvetica, sans-serif; width: 100%; font-size: 12px; font-weight: normal; font-style: normal; color: #000000">
                <tr>
                    <td>
                        <b>PNR :</b> <span id="PNR"></span>
                        <input id="txtPNRNO" name="txtPNRNO" type="hidden" />
                    </td>
                    <td id="TktNoInfo" style="display: none;">
                        <b>Ticket No:</b> <span id="TktNo"></span>
                    </td>
                </tr>
                <tr>
                    <td style="display: none;" id="PaxnameInfoResu">
                        <b>PAX NAME :</b> <span id="Paxname"></span>
                    </td>
                    <td style="display: none;" id="PaxnameInfoRefnd">
                        <div id="Refunddtldata" class="large-12 medium-12 small-12"></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b>
                            <span id="RemarksTypetext"></span>Remark 
                        </b>
                        <input id="RemarksType" name="RemarksType" type="hidden" />

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtRemark" name="txtRemark" class="form-control" cols="56" rows="2" style="border: thin solid #eee; width: 100%;"></textarea>
                    </td>
                </tr>
                <tr id="trCancelledBy" visible="false">
                    <td>
                        <b>Cancelled By:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpCancelledBy" CssClass="drop" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="width: 20%; padding-top: 10px; margin-left: 40%; text-align: center;">
                            <asp:Button ID="btnRemark" runat="server" Text="Submit" CssClass="btn btn-danger" />
                            <input id="txtPaxid" name="txtPaxid" type="hidden" />
                            <input id="txtPaxType" name="txtPaxType" type="hidden" />
                            <input id="txtSectorid" name="txtSectorid" type="hidden" />
                            <input id="txtOrderid" name="txtOrderid" type="hidden" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

    </div>





    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>

    <div id="divReport" runat="server" visible="false">
        <%--<div class="card-header">
        </div>--%>
        <div class="table-responsive">
            <%-- style="height:200px;overflow:scroll;"--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True" AllowSorting="True" OnRowDataBound="ticket_grdview_RowDataBound"
                        AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="30">
                        <Columns>
                            <asp:TemplateField HeaderText="P Type" FooterStyle-Wrap="false" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P ID">
                                <ItemTemplate>
                                    <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                        rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                        style="font-size: 12px; font-weight: bold; color: #000" title="view ticket details">
                                        <%--<asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)--%>
                                        <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order ID">
                                <ItemTemplate>
                                    <%--                                    <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"--%>
                                    <a href='OrderDeatils.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"
                                        rev="width: 900px; height: 500px; overflow:hidden;" style="font-size: 12px; font-weight: bold; color: #000">
                                        <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cancel">
                                <ItemTemplate>                                    
                                    <asp:LinkButton ID="lnkrefund" runat="server" Font-Strikeout="False" Font-Overline="False"
                                        CommandArgument='<%#Eval("PaxId") %>' CommandName='REFUND' OnClick="lnkrefund_Click"
                                        ToolTip="Refund Request" Style="font-weight: bold;">  
                                        <%# IIf(Eval("PaxStatus") = "Cancelled", "- - - - -", "Cancel")%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                        <%# IIf(Eval("PaxStatus") = "Cancelled", "<p style='color:red;font-weight:bold;'>Cancelled</p>", "<p style='color:green;font-weight:bold;'>Ticketed</p>")%>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PNR">
                                <ItemTemplate>
                                    <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket No">
                                <ItemTemplate>
                                    <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provider" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProvider" runat="server" Text='<%#Eval("Provider")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger Name">
                                <ItemTemplate>
                                    <asp:Label ID="PaxFNAme" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                    <asp:Label ID="PaxLName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--  <asp:TemplateField HeaderText="Exec ID">
                                <ItemTemplate>
                                    <asp:Label ID="ExcutiveID" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Airline">
                                <ItemTemplate>
                                    <asp:Label ID="Airline" runat="server" Text='<%#Eval("VC")%>'></asp:Label>
                                    <asp:Label ID="countFound" Visible="false" runat="server" Text='<%#Eval("countFound")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Sector" DataField="sector"></asp:BoundField>
                            <asp:BoundField HeaderText="Net Fare(₹)" DataField="TotalAfterDis">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>--%>
                            <asp:BoundField HeaderText="Booking Date" DataField="CreateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Journey Date" DataField="JourneyDate"></asp:BoundField>



                            <asp:TemplateField HeaderText="CancelOnline" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnOnlineCanc" runat="server" Font-Strikeout="False" Font-Overline="False"
                                        orderid='<%#Eval("OrderId") %>'
                                        CssClass="topbutton topopup edit btnContinue">Cancel Online                           
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fare Type">
                                <ItemTemplate>

                                    <%-- <span class="fareRuleToolTip">--%>
                                    <%--<img src='<%#ResolveClientUrl("~/images/fare-rules.png")%>' class="cursorpointer " alt="Click to View Full Details" title="Click to View Full Details" style="height: 20px; cursor: pointer;" /></span>--%>
                                    <%-- <div><%#Eval("FareRule")%> </div>--%>
                                    <asp:Label ID="FareType" runat="server" Text='<%#Eval("FareType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Partner Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="PartnerName" runat="server" Text='<%#Eval("PartnerName")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Mode">
                                <ItemTemplate>
                                    <asp:Label ID="PaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PGCharges" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPGCharges" runat="server" Text='<%#Eval("PGCharges")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Reissue" Visible="false">
                                <ItemTemplate>


                                    <asp:LinkButton ID="lnkreissue" runat="server" Font-Strikeout="False" Font-Overline="False"
                                        CommandArgument='<%#Eval("PaxId") %>' CommandName='REISSUE' OnClick="lnkreissue_Click"
                                        ToolTip="Reissue Request" Style="font-weight: bold;">
                                        <%# IIf(Eval("Provider") = "FDD", "", "Reissue")%>  
                                        
                                               
                                    </asp:LinkButton>


                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>



                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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


    <div class="fade" title="1apiUKNRMLITZNRML_O" style="display: none;">
        <div class="depcity">
            <div style="width: 98%; margin: 1%; padding: 2%; position: fixed; top: 20%; background: #f9f9f9; box-shadow: 0px 0px 4px #333;">
                <div class="close1" style="cursor: pointer; float: right; position: relative; top: 2px; right: 3px; font-size: 20px">
                    X
                </div>
                <div class="large-12 medium-12 small-12 bld">
                    Fare Rule
                </div>
                <div id="FruleTExt" style="overflow-y: scroll; height: 250px;">
                    fare rule not available.
                </div>
            </div>
        </div>
    </div>

    <div id="HourDeparturePopup">
        <div class="close11">
        </div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div class="HourDeparturepopup_content">

            <div class="col-md-12">
                <div class="clear"></div>
                <div class="col-md-12">Click “OK” to proceed for offline request.</div>
                <div class="clear"></div>
                <div class="col-md-4"></div>
                <div class="col-md-2 button  buttonfltbkss btnokReport">OK</div>
                <input id="txtPaxid_4HourDeparture" name="txtPaxid_4HourDeparture" type="hidden" />
            </div>

        </div>
    </div>



    <%-- <div id="SourceView"></div>--%>
    <div id="waitMessagefc" style="display: none">
        <div class="wait_New w40 center">
            <div class="clear1"></div>
            <h3>We are processing your request. Please wait....</h3>
            <div class="clear"></div>
            <img alt="loading" width="50" height="50" src="../images/loadingAnim.gif">
            <div class="clear"></div>
            <div id="searchquery" style="padding-top: 15px">
            </div>
            <div class="clear"></div>
            <span class="f10">Please do not close or refresh this window.</span>
            <div class="clear1"></div>
        </div>
    </div>

    <div id="toPopup" style="background: none repeat scroll 0 0 #FFFFFF; border: 0px solid #ccc!important; border-radius: 5px 5px 5px 5px; color: #333333; display: none; font-size: 14px; left: 55%; margin-left: -502px; position: fixed; top: 20%; width: 950px; height: 450px; overflow: auto; z-index: 2;">
        <div class="close"></div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content w100">

            <div id="SourceView"></div>
        </div>
    </div>
    <input type="hidden" id="objModel" name="objModel" />
    <br />
    <%--  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>--%>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
	<script type="text/javascript">
         var hdnFromDate = '<%=hdnFromDate.ClientID%>';
         $("#From").val($("#" + hdnFromDate).val());
         var hdnToDate = '<%=hdnToDate.ClientID%>';
        $("#To").val($("#" + hdnToDate).val());        
     </script>
</asp:Content>
