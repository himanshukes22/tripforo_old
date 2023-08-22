<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketCalender.aspx.vb" Inherits="SprReports_TicketCalender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 
          <link href="../CSS/PopupStyle.css?V=1" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../Styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" />
    <link href="../Styles/jAlertCss.css"rel="stylesheet" />
      <script type="text/javascript" src="../Scripts/PopupScriptnew.js?V=1"></script>
    <script  type="text/javascript"  src="../Scripts/ReissueRefundMulPax.js?v=1" type="text/javascript"></script>
  



    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }
        .msi {
         /*font-size: 18px;
            border-collapse: collapse;
            position: fixed;
            margin-top: -115px;
            background: white;
            z-index: 99999;*/
        }

       .flyclubhead {
           display:none;
        }

        #toPopupReport {
       width: 80% !important;
    margin-left: -320px;
    overflow-x: hidden;
    height: 440px;
    overflow-y: scroll;
   
   
}
        body{
                line-height: 20px;
        }
        .w80{width:80%}
        .auto{margin:auto}

        div.close {
   
    height: 32px;
    left: 11px;
    position: relative;
    width: 38px;
     top: -2px;
   
}
    </style>



      <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>

    <div class="mtop80"></div>
    <div class="row">
        <div class="col-md-12 text-center search-text  " style="display:none">
            Ticket Report
        </div>
    </div>
    <div class="large-12 medium-12 small-12">

        <%--<div class="large-8 medium-8 small-12 columns">--%>
        <div class="row"  style="display:none;">

            <div class="col-md-10" style="padding-left: 100px;">
                <div class="form-inlines">
                    <div class="form-groups">
                        <input type="text" name="From" id="From" placeholder="From Date" class="form-control" readonly="readonly" />
                    </div>
                    <div class="form-groups">
                        <input type="text" name="To" placeholder="To Date" id="To" class="form-control" readonly="readonly" />
                    </div>
                    <div class="form-groups">
                        <asp:TextBox ID="txt_PNR" placeholder="PNR" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups">
                        <asp:TextBox ID="txt_OrderId" placeholder="OrderId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups">
                        <asp:TextBox ID="txt_PaxName" placeholder="Pax Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups">
                        <asp:TextBox ID="txt_TktNo" placeholder="TicketNo" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups">
                        <asp:TextBox ID="txt_AirPNR" placeholder="Airline" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-groups" id="tdTripNonExec2" runat="server">
                        <asp:DropDownList class="form-control" ID="ddlTripDomIntl" runat="server">
                            <asp:ListItem Value="">-----Select-----</asp:ListItem>
                            <asp:ListItem Value="D">Domestic</asp:ListItem>
                            <asp:ListItem Value="I">International</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-groups" id="Div1" runat="server">
                        <asp:DropDownList class="form-control" ID="DropDownListDate" runat="server">
                           <asp:ListItem Value="B" Selected="True">Booking Date</asp:ListItem>
                           <asp:ListItem Value="J">Journey Date</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="color: #FF0000">
                        * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                    </div>
                    <div class="form-groups" id="td_Agency" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btn_result" runat="server" class="buttonfltbks" Text="Search Result" />
                <asp:Button ID="btn_export" runat="server" class="buttonfltbk" Text="Export" />
            </div>
            <%--  <div class="row" style="padding: 10px 10px 10px 10px;">
                    <div class="col-md-10">
                        <div style="color: #FF0000">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                        </div>
                    </div>

                </div>--%>
        </div>

        <div class="clear1"></div>

        <div class="row" style="display:none">
            <div class="col-md-4">
                </div>
            <div class="col-md-3">
                Total Ticket Sale :
                                    <asp:Label ID="lbl_Total" runat="server"></asp:Label>
            </div>
      <div class="col-md-3">
                Total Ticket Issued :
                                    <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div id="toPopupReport">
       
        <div class="close">
        </div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content">
            <!--your content start-->
            <table border="0" cellpadding="10" cellspacing="5" style="font-family: arial, Helvetica, sans-serif; width:100%; font-size: 12px; font-weight: normal; font-style: normal; color: #000000">
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
                    <td>
                         <b>OrderID :</b> <span id="Odno"></span>                     
                    </td>
                        <td id="nn" style="display: none;">
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
                            <span id="RemarksTypetext" style="padding:4px"></span>Remark
                        </b>
                        <input id="RemarksType" name="RemarksType" type="hidden" />

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtRemark" name="txtRemark" cols="56" rows="2" style="border: thin solid #eee; width:100%; "></textarea>
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
                        <div style="width: 20%; padding-top:10px; margin-left: 40%; text-align:center; ">
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

    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>

    <div class="clear"></div>
    <div id="divReport" runat="server" visible="false" align="center" class="large-12 medium-12 small-12">
        <%-- style="height:200px;overflow:scroll;"--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="table table-hover msi" GridLines="Horizontal" Font-Size="12px" PageSize="30" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black">
                    <Columns>
                        <asp:TemplateField HeaderText="P Type" FooterStyle-Wrap="false" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P ID">
                            <ItemTemplate>
                                <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                     target="_blank"
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91" title="view ticket details">
                                    <%--<asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)--%>
                                    <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order ID">
                            <ItemTemplate>
                                <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                    <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pnr">
                            <ItemTemplate>
                                <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ticket No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
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
                        <%-- <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>--%>
                        <asp:BoundField HeaderText="DepTime" DataField="DepTime"></asp:BoundField>
                      <%--  <asp:BoundField HeaderText="Journey Date" DataField="JourneyDate"></asp:BoundField>--%>

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
                    <%--    <asp:TemplateField HeaderText="FareRule">
                            <ItemTemplate>

                                <span class="fareRuleToolTip">
                                    <img src='<%#ResolveClientUrl("~/images/fare-rules.png")%>' class="cursorpointer " alt="Click to View Full Details" title="Click to View Full Details" style="height: 20px; cursor: pointer;" /></span>
                                <div class="hide"><%#Eval("FareRule")%> </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="Partner Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PartnerName" runat="server" Text='<%#Eval("PartnerName")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Mode">
                            <ItemTemplate>
                                <asp:Label ID="PaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PGCharges">
                            <ItemTemplate>
                                <asp:Label ID="lblPGCharges" runat="server" Text='<%#Eval("PGCharges")%>' Visible='<%# GetVisibleStatus() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    </Columns>



                    <FooterStyle />
                    <HeaderStyle  Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />



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
        <%--</div>--%>
    </div>
    <div class="fade" title="1apiUKNRMLITZNRML_O" style="display: none;">
        <div class="depcity">
            <div style="width: 98%; margin: 1%; padding: 2%;position: fixed;
    top: 20%; background: #f9f9f9; box-shadow: 0px 0px 4px #333;">
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
    
    </div>
    </form>
</body>
</html>
