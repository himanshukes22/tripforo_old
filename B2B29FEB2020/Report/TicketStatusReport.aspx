<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="TicketStatusReport.aspx.vb" Inherits="SprReports_TicketStatusReport" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/CSS/PopupStyle.css?V=1")%>" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/jAlertCss.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/Scripts/ReissueRefund.js?v=1")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PopupScript.js?V=1")%>"></script>
    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }
    </style>


        <style>
    a, p, li, td, span {
    font-size: 11.5px;
    /* line-height: 24px; */
    font-family: "Amazon Ember",Arial,sans-serif;
    font-weight: 400;
}
        </style>

      
    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">Booking Details</a></li>
        
    </ol>

    <div class="card-main">
      
        <div class="card-body">

            <div class="inner-box">
                <div class="row">
                    <div class="col-md-3">
                        <label>From</label>
                        <input type="text" name="From" id="From" placeholder="Select Date" class="form-control" readonly="readonly" />
                    </div>
                    <div class="col-md-3">
                        <label>To</label>
                        <input type="text" name="To" placeholder="Select Date" id="To" class="form-control" readonly="readonly" />
                    </div>
                    <div class="col-md-3">
                        <label>PNR</label>
                        <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Order ID</label>
                        <asp:TextBox ID="txt_OrderId" placeholder="Enter OrderId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <label>Pax Name</label>
                        <asp:TextBox ID="txt_PaxName" placeholder="Enter Pax Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Ticket No.</label>
                        <asp:TextBox ID="txt_TktNo" placeholder="Enter TicketNo" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Airline</label>
                        <asp:TextBox ID="txt_AirPNR" placeholder="Enter Airline" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3" id="tdTripNonExec2" runat="server">
                        <label>Airline Type</label>
                        <asp:DropDownList class="form-control" ID="ddlTripDomIntl" runat="server">
                            <asp:ListItem Value="">-----Select-----</asp:ListItem>
                            <asp:ListItem Value="D">Domestic</asp:ListItem>
                            <asp:ListItem Value="I">International</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <label>Status</label>
                        <asp:DropDownList CssClass="form-control" ID="dd_status" runat="server">
                            <asp:ListItem Text="ALL" Value="ALL">Select Status</asp:ListItem>
                            <asp:ListItem Text="Pending" Value="Request"></asp:ListItem>
                            <asp:ListItem Text="Hold" Value="Confirm"></asp:ListItem>
                            <asp:ListItem Text="HoldByAgent" Value="ConfirmByAgent"></asp:ListItem>
                            <asp:ListItem Text="PreHoldByAgent" Value="PreConfirmByAgent"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                            <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                            <asp:ListItem Text="Ticketed" Value="Ticketed"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-3" id="td_Agency" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>
                     <div class="btn-search col-md-3">
                            <asp:Button ID="btn_result" runat="server" class="btn cmn-btn" Text="Search Result" />
                        </div>   
                    </div>

                
                <hr />

                <div class="row ticket-bal">
                    <div class="col-md-12">
                        Total Ticket Count :
                         <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Total" Visible="false" runat="server"></asp:Label>
                    </div>                    
                 </div>
            </div>
        </div>


        <%--  <div class="row" style="padding: 10px 10px 10px 10px;">
                    <div class="col-md-10">
                        <div style="color: #FF0000">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                        </div>
                    </div>

                </div>--%>


        <div class="clear1"></div>


        <div class="clear1"></div>
    </div>
    <br />

    <div id="toPopupReport" class="tbltbl large-12 medium-12 small-12">
        <div class="close">
        </div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content">
            <!--your content start-->
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
                        <textarea id="txtRemark" name="txtRemark" cols="56" rows="2" style="border: thin solid #eee; width: 100%;"></textarea>
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
                            <asp:Button ID="btnRemark" runat="server" Text="Submit" CssClass="buttonfltbk rgt w20" />
                            <input id="txtPaxid" name="txtPaxid" type="hidden" />
                            <input id="txtPaxType" name="txtPaxType" type="hidden" />
                            <input id="txtSectorid" name="txtSectorid" type="hidden" />
                            <input id="txtOrderid" name="txtOrderid" type="hidden" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--your content end-->
    </div>

    <div class="clear1"></div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>

   
     <div id="divReport" class="card-main" runat="server" visible="false">
        <div class="card-header">
            <h3 class="main-heading"><asp:Button ID="btn_export" runat="server" CssClass="btn-export" Text="Download"/><i class="icofont-download-alt"></i></h3>         
        </div>
    <div class="table-responsive">
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="P Type" FooterStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P ID">
                            <ItemTemplate>
                                <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                    rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91" title="view ticket details">
                                    <%--<asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)--%>
                                    <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order ID">
                            <ItemTemplate>
                                <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"
                                    rev="width: 900px; height: 500px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                    <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pnr">
                            <ItemTemplate>
                                <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ticket No">
                            <ItemTemplate>
                                <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="LblStatus" runat="server" Text='<%#Eval("STATUS")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Provider" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProvider" runat="server" Text='<%#Eval("Provider")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Name">
                            <ItemTemplate>
                                <asp:Label ID="PaxFNAme" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                <asp:Label ID="PaxLName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Exec ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ExcutiveID" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Airline">
                            <ItemTemplate>
                                <asp:Label ID="Airline" runat="server" Text='<%#Eval("VC")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Sector" DataField="sector"></asp:BoundField>
                        <asp:BoundField HeaderText="N Fare" DataField="TotalAfterDis">
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                        <%-- <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>--%>
                        <asp:BoundField HeaderText="Booking Date" DataField="CreateDate"></asp:BoundField>
                        <asp:BoundField HeaderText="Journey Date" DataField="JourneyDate"></asp:BoundField>

                        <asp:TemplateField HeaderText="Reissue" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkreissue" runat="server" Font-Strikeout="False" Font-Overline="False"
                                    CommandArgument='<%#Eval("PaxId") %>' CommandName='REISSUE' OnClick="lnkreissue_Click"
                                    ToolTip="Reissue Request">Reissue
                                               
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancel" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkrefund" runat="server" Font-Strikeout="False" Font-Overline="False"
                                    CommandArgument='<%#Eval("PaxId") %>' CommandName='REFUND' OnClick="lnkrefund_Click"
                                    ToolTip="Refund Request">Cancel
                                              
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FareRule" Visible="false">
                            <ItemTemplate>

                                <span class="fareRuleToolTip">
                                    <img src='<%#ResolveClientUrl("~/images/fare-rules.png")%>' class="cursorpointer " alt="Click to View Full Details" title="Click to View Full Details" style="height: 20px; cursor: pointer;" /></span>
                                <div class="hide"><%#Eval("FareRule")%> </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Partner Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PartnerName" runat="server" Text='<%#Eval("PartnerName")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Mode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PGCharges" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPGCharges" runat="server" Text='<%#Eval("PGCharges")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Pax Count">
                            <ItemTemplate>
                                <asp:Label ID="TotalPax" runat="server" Text='<%#Eval("TotalPax")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderText="Hand_Bag_Fare">
                            <ItemTemplate>
                                <asp:Label ID="lblIsBagFares" runat="server" Text='<%#Eval("IsBagFares")%>'></asp:Label>
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

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
</asp:Content>

