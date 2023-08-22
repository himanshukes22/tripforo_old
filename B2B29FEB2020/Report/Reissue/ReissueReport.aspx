<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReissueReport.aspx.vb" Inherits="SprReports_Reissue_ReissueReport" MasterPageFile="~/MasterPageForDash.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />

    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>


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
        <li><a href="#">Reissue Reports</a></li>
        
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
                    <asp:TextBox ID="txt_TktNo" placeholder="Enter TicketNo" class="theme-search-area-section-input" runat="server"></asp:TextBox>
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
                <div class="col-md-3" id="tdTripNonExec2" runat="server">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:DropDownList class="theme-search-area-section-input" ID="ddlTripReissueDomIntl" runat="server">
                        <asp:ListItem Value="">Select Trip Type</asp:ListItem>
                        <asp:ListItem Value="D">Domestic</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                        </div>
                </div>
                </div>
            <br />
            <div class="row">
                <div id="td_Agency" runat="server" class="col-md-3">
                    <div id="tr_ExecID" runat="server" class="w100">
                        <div class="form-groups col-md-6 col-xs-6">
                            <asp:DropDownList class="theme-search-area-section-input" ID="ddl_ExecID" runat="server">
                            </asp:DropDownList>

                            </div>
                            <div class="form-groups col-md-6 col-xs-6">
                                <asp:DropDownList ID="ddl_Status" class="theme-search-area-section-input" runat="server">
                                </asp:DropDownList>
                            </div>
                        
                    </div>
                </div>
                <div class="col-md-3">
                     <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                    <input type="text" class="theme-search-area-section-input" id="txtAgencyName" placeholder="Enter Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                </div>
                         </div>
                </div>
                 <div class="btn-search col-md-3">
                     <label></label>
                                <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                         <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>
                 </div>
                </div>


                <div class="" id="tdTripNonExec1" runat="server" style="visibility: hidden"></div>

         
        </div>
        
      <%--  <div class="row" style="padding: 10px 10px 10px 10px;">
            <div class="col-md-9 col-xs-12 col-md-push-1">
                <div style="color: #FF0000">
                    * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                </div>
            </div>

        </div>--%>
    </div>
</div>





    <div id="divReport" class="card-main" runat="server" visible="false">
      
    <div class="table-responsive">
       
        <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd_paxstatusinfo" runat="server" AutoGenerateColumns="False"  CssClass="rtable" GridLines="None" Font-Size="12px"
                    AllowPaging="True" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="Customer ID">
                            <ItemTemplate>
                                <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agency Name">
                            <ItemTemplate>
                                <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P ID">
                            <ItemTemplate>
                                <asp:Label ID="lblPaxID" runat="server" Text='<%#Eval("PaxID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Type">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Name">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label>
                                <asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--  <asp:TemplateField HeaderText="Pax LastName">
                                        <ItemTemplate>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Pnr">
                            <ItemTemplate>
                                <asp:Label ID="lblpnr" runat="server" Text='<%#Eval("pnr_locator") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ticket Number">
                            <ItemTemplate>
                                <asp:Label ID="lbltktno" runat="server" Text='<%#Eval("Tkt_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Airline">
                            <ItemTemplate>
                                <asp:Label ID="lblVC" runat="server" Text='<%#Eval("VC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector">
                            <ItemTemplate>
                                <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Flight Number">
                            <ItemTemplate>
                                <asp:Label ID="lblflightno" runat="server" Text='<%#Eval("FlightNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Departure Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("departure_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T Fare">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFare") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fare After Discount">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reissue Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblrissuechrge" runat="server" Text='<%#Eval("ReissueCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblsrvcharge" runat="server" Text='<%#Eval("ServiceCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fare Difference">
                            <ItemTemplate>
                                <asp:Label ID="lblfarediff" runat="server" Text='<%#Eval("FareDiff") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec ID">
                            <ItemTemplate>
                                <asp:Label ID="lblExecutiveId" runat="server" Text='<%#Eval("ExecutiveID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec Reject Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblRejectionComment" runat="server" Text='<%#Eval("RejectionComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Date">
                            <ItemTemplate>
                                <asp:Label ID="lblsubmit" runat="server" Text='<%#Eval("SubmitDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldateA" runat="server" Text='<%#Eval("AcceptDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldateU" runat="server" Text='<%#Eval("UpdateDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Partner Name">
                            <ItemTemplate>
                                <asp:Label ID="PartnerName" runat="server" Text='<%#Eval("PartnerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PGCharges">
                            <ItemTemplate>
                                <asp:Label ID="lblPGCharges" runat="server" Text='<%#Eval("PGCharges")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
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
    <br />

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>


</asp:Content>
