<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CancellationReport.aspx.vb" Inherits="SprReports_Refund_CancellationReport" MasterPageFile="~/MasterPageForDash.master" %>


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
        <li><a href="#">Refund Reports</a></li>
        
    </ol>

    <div class="card-main">
       
      <div class="card-body report">
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
                  <div class="col-md-3" id="tdTripNonExec2" runat="server">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:DropDownList class="theme-search-area-section-input" ID="ddlTripRefunDomIntl" runat="server">
                        <asp:ListItem Value="">Select Trip Type</asp:ListItem>
                        <asp:ListItem Value="D">Domestic</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                    </asp:DropDownList>
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
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                        </div>
                </div>                
                <div class="col-md-3">
                   <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:TextBox ID="txt_TktNo" placeholder="Enter Ticket No." class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                       </div>
                </div>          
              
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
              </div>
            <br />
            <div class="row">                
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
                <div class="col-md-3">
                  
                      <div class="btn-search">
                          
                    <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                </div>
                </div>
            </div>
            <div class="row">
                <div id="td_Agency" runat="server" class="col-md-3">
                    <div id="tr_ExecID" runat="server">
                        <div class="col-md-6">
                            <asp:DropDownList class="form-control" ID="ddl_ExecID" runat="server">
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddl_Status" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        
                    </div>
                </div>
              
                <div class="form-groups" id="tdTripNonExec1" runat="server"></div>
             
          </div>            
           

        </div>
        
        
  </div>
        
    </div>


    <br />




     <div id="divReport" class="card-main" runat="server" visible="false">
        <div class="card-header">
            <h3 class="main-heading"><asp:Button ID="btn_export" runat="server" CssClass="btn-export" Text="Download"/><i class="icofont-download-alt"></i></h3>         
        </div>
    <div class="table-responsive">
  <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd_report" runat="server" AutoGenerateColumns="False"  CssClass="rtable" GridLines="None" Font-Size="12px" AllowPaging="true" PageSize="30" >
                    <Columns>
                        <asp:TemplateField HeaderText="Credit Node">
                          <ItemTemplate>
                                <a target="_blank" href='../Accounts/CreditNodeDomDetails.aspx?RefundID=<%#Eval("RefundID")%> &TicketID=<%#Eval("Tkt_No")%>'>
                                    <asp:Label ID="lblRefundID" runat="server" Text='<%#Eval("RefundID") %>' ForeColor="#004b91"
                                        Font-Bold="True" Font-Underline="True"></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <asp:TemplateField HeaderText="P Type">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Name">
                            <ItemTemplate>
                                <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label>&nbsp;<asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Pax LastName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
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
                                <asp:Label ID="lbldestination" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Cancellation Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblcharge" runat="server" Text='<%#Eval("CancellationCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sevice Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblsrvcharge" runat="server" Text='<%#Eval("ServiceCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refunded Fare">
                            <ItemTemplate>
                                <asp:Label ID="lblrefund" runat="server" Text='<%#Eval("RefundFare") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblcancel" runat="server" Text='<%#Eval("RegardingCancel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec ID">
                            <ItemTemplate>
                                <asp:Label ID="lblexecutive" runat="server" Text='<%#Eval("ExecutiveID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec Rejected Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblRejectComment" runat="server" Text='<%#Eval("RejectComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exec Updated Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblUpComment" runat="server" Text='<%#Eval("UpdateRemark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refund Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("SubmitDate") %>'></asp:Label>
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
                        <asp:BoundField DataField="CancelStatus" HeaderText="Cancel Status" />
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PgMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PG Charges">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Charges" runat="server" Text='<%#Eval("PgCharges") %>'></asp:Label>
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
        <%--</div>--%>
        <%--</td>
                </tr>
            </table>--%>
    </div>
</div>

 

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
