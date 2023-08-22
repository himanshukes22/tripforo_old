<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HoldPnrReport.aspx.vb" Inherits="SprReports_HoldPNR_HoldPnrReport" MasterPageFile="~/MasterPageForDash.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .HeaderStyle th {
            white-space: nowrap;
        }
    </style>

     


    
    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">Hold PNR'S</a></li>
        
    </ol>
  

    <div class="card-main">
  
 <div class="card-body">    
             <div class="inner-box">
                   <div class="row">
                        <div class="col-md-3">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                            <input type="text" name="From" id="From" placeholder="Select Date" class="theme-search-area-section-input" readonly="readonly" />
                                </div>
                              </div>
                        </div>
                        <div class="col-md-3">
                           <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                            <input type="text" name="To" placeholder="Select Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
                                </div>
                               </div>
                        </div>
                        <div class="col-md-3">
                           <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                            <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                               </div>
                        </div>
                        <div class="col-md-3">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-tag"></i>
                            <asp:TextBox ID="txt_OrderId" placeholder="Enter OrderId" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                              </div>
                        </div>
                       </div>
                 <br />
                 <div class="row">
                        <div class="col-md-3">
                            <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                            <asp:TextBox ID="txt_PaxName" placeholder="Enter Pax Name" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                                </div>
                        </div>
                        <div class="col-md-3">
                           <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                            <asp:TextBox ID="txt_AirPNR" placeholder="Enter Airline" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                               </div>
                        </div>

                        <div class="btn-search col-md-3">
                                     <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                         <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>
                                </div>

                                     

                     </div>
                        
                       
                   
                </div>
         
    </div>




    <div class="col-md-12 heading hidden">
 
        <div class="clear"></div>
        <div class="col-md-3 col-sm-12" style="display: none;">
            Ticket No
        </div>
        <div class="large-2 medium-2 small-9 columns" style="display: none;">
            <asp:TextBox ID="txt_TktNo" runat="server" Width="100px"></asp:TextBox>
        </div>
        <div class="large-1 medium-1 small-3 columns" id="tdTripNonExec1" runat="server" style="display: none;">
            Trip
        </div>
        <div id="tdTripNonExec2" runat="server" class="large-2 medium-2 small-9 columns" style="display: none;">
            <asp:DropDownList ID="ddlTripRefunDomIntl" runat="server">
                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                <asp:ListItem Value="D">Domestic</asp:ListItem>
                <asp:ListItem Value="I">International</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="clear"></div>
        <div id="td_Agency" runat="server" style="display: none;">


            <div id="tr_ExecID" runat="server">
                <div class="large-1 medium-1 small-3 columns">
                    Exec ID
                </div>
                <div class="large-2 medium-2 small-9  columns">
                    <asp:DropDownList ID="ddl_ExecID" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="large-1 medium-1 small-3 large-push-1 medium-push-1 columns" style="display: none;">
                    Status
                </div>
                <div class="large-2 medium-2 small-9 large-push-1 medium-push-1 columns" style="display: none;">
                    <asp:DropDownList ID="ddl_Status" runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="large-1 medium-1 small-3 large-push-1 medium-push-1 columns">
                Agency
            </div>
            <div class="large-2 medium-2 small-9 large-push-1 medium-push-1 columns end">
                <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%><input
                    type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                    onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
            </div>

        </div>

        
        <div class="clear"></div>
    </div>
     </div>
       

    


     <div id="divReport" class="" runat="server" visible="false">
        <div class="card-header">
        </div>
        <div class="table-responsive" >
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="10">
                <Columns>
                    <asp:TemplateField HeaderText="Order Id">
                        <ItemTemplate>
                            <%--<a id="ancher" href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%>' target="_blank"
                                style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; font-weight: bold;">
                                <asp:Label ID="lbl_OrderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>(View)</a>--%>
                            <a id="ancher" href='../PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%>' target="_blank"
                                style=" font-size: 12px; color: #000; font-weight: bold;" title="click to view">
                                <asp:Label ID="lbl_OrderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Customer Id">
                        <ItemTemplate>
                            <asp:Label ID="lbl_AgentID" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="PNR">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PNR" runat="server" Text='<%#Eval("GdsPnr") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sector">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Sector" runat="server" Text='<%#Eval("sector") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Duration">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Duration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Trip">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TripType" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Trip">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Trip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Tour Code">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TourCode" runat="server" Text='<%#Eval("TourCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Total Amt">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TotalBookingCost" runat="server" Text='<%#Eval("TotalBookingCost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Net Amt">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TotalAfterDis" runat="server" Text='<%#Eval("TotalAfterDis") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pax">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PgFName" runat="server" Text='<%#Eval("PgFName") %>'></asp:Label>
                            <asp:Label ID="lbl_PgLName" runat="server" Text='<%#Eval("PgLName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PgMobile" runat="server" Text='<%#Eval("PgMobile") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PgEmail" runat="server" Text='<%#Eval("PgEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lbl_CreateDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Partner">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Partner" runat="server" Text='<%#Eval("PartnerName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Payment Mode">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Payment" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PG Charges">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PGCharges" runat="server" Text='<%#Eval("PgCharges")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Holld Charges">
                        <ItemTemplate>
                            <asp:Label ID="lbl_HoldCharge" runat="server" Text='<%#Eval("HoldCharge")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Holld Booking">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IsHoldByAgent" runat="server" Text='<%#Eval("IsHoldByAgent")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Issue_Hold_Ticket">
                        <ItemTemplate>                            
                              <a target="_blank" href="<%#Eval("URL")%>"
                                                            rel="lyteframe" rev="width: 900px; height: 280px; overflow:hidden;" target="_blank"
                                                            style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                            color: #004b91"> <asp:Label ID="lbl_Issue_Hold_Ticket" runat="server" Text='<%#Eval("HoldTicket")%>'></asp:Label> </a> 
                            
                            
                          <%--  <a target="_blank" href="ConfirmHoldTicket.aspx?AgentID=<%#Eval("AgentId")%>"
                                                            rel="lyteframe" rev="width: 900px; height: 280px; overflow:hidden;" target="_blank"
                                                            style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                            color: #004b91"> <asp:Label ID="Label1" runat="server" Text='<%#Eval("HoldTicket")%>'></asp:Label> </a> --%>                        
                            <%--<asp:Label Text='<%# If(Eval("IsHoldByAgent").ToString() = "A", "Absent", "Present")%>' runat="server" />--%>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
        </div>
    </div>


    <div class="large-12 medium-12 small-12">

        <div class=" large-8 medium-8 small-12 columns end">
        </div>
    </div>

    <div class="clear1"></div>



    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>
