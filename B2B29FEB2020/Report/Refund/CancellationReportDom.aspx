<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="CancellationReportDom.aspx.vb" Inherits="Reports_Refund_CancellationReportDom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
     <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
    
        <div class="large-3 medium-3 small-12 columns">
      <%-- <uc1:LeftMenu runat="server" ID="LeftMenu" />      --%> 
   </div>
       
                    <div class="large-8 medium-8 small-12 columns">
                        
                            <div class="large-12 medium-12 small-12 bld blue">
                                Search Domestic Refund
                            </div>
                        
                            <div class="clear1">
                                
                            </div>
                       
                            
                                        <div class="large-2 medium-3 small-3 columns">
                                            From Date
                                        </div>
                                        <div class="large-3 medium-3 small-9  columns">
                                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                                />
                                        </div>
                                        <div class="large-2 medium-3 small-3 large-push-1 medium-push-1 columns">
                                            To Date
                                        </div>
                                        <div class="large-3 medium-3 small-9 large-push-2 columns">
                                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly"/>
                                        </div>
                        <div class="clear"></div>
                                        <div class="large-2 medium-3 small-3 columns">
                                            PNR
                                        </div>
                                        <div class="large-3 medium-3 small-9  columns">
                                            <asp:TextBox ID="txt_PNR" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="large-2 medium-3 small-3 large-push-1 medium-push-1 columns">
                                            OrderId
                                        </div>
                                        <div class="large-3 medium-3 small-9 large-push-2 columns">
                                            <asp:TextBox ID="txt_OrderId" runat="server"></asp:TextBox>
                                        </div>
                                   
                        <div class="clear"></div>
                            
                                   
                                        <div class="large-2 medium-3 small-3 columns">
                                            Pax Name
                                        </div>
                                        <div class="large-3 medium-3 small-9  columns">
                                            <asp:TextBox ID="txt_PaxName" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="large-2 medium-3 small-3 large-push-1 medium-push-1 columns">
                                            Ticket No
                                        </div>
                                        <div class="large-3 medium-3 small-9 large-push-2 columns">
                                            <asp:TextBox ID="txt_TktNo" runat="server" ></asp:TextBox>
                                        </div>
                        <div class="clear"></div>
                                        <div class="large-2 medium-3 small-3 columns">
                                             Airline
                                        </div>
                                        <div class="large-3 medium-3 small-9  columns">
                                            <asp:TextBox ID="txt_AirPNR" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="clear1"></div>
                                    
                            <div id="td_Agency" runat="server">
                               
                                            
                                                            <div id="tr_ExecID" runat="server">
                                                                <div class="large-2 medium-3 small-3 columns">
                                                                    Exec ID
                                                                </div>
                                                                <div class="large-3 medium-3 small-9 columns">
                                                                    <asp:DropDownList ID="ddl_ExecID" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="large-3 medium-3 small-3 columns large-push-1 medium-push-1 ">
                                                                    Status
                                                                </div>
                                                                <div class="large-3 medium-3 small-9 columns large-push-2 ">
                                                                    <asp:DropDownList ID="ddl_Status" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        <div class="clear"></div>
                                                    <div class="large-3 medium-3 small-3 columns">
                                                        Agency
                                                    </div>
                                                    <div class="large-3 medium-3 small-9 columns">
                                                        <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%><input
                                                            type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                                                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                                    </div>
                                <div class="clear1"></div>
                                                <div class="large-4 medium-6 small-12 columns">
                                          <div class="large-6 medium-6 small-6 columns">  <asp:Button ID="btn_result" runat="server" Text="Search Result"/></div>
                                              <div class="large-6 medium-6 small-6 columns">&nbsp;&nbsp;<asp:Button
                                                ID="btn_export" runat="server" Text="Export" /></div>
                                        </div>
                                        </div>
                       <div class="clear"></div>

                            <div class="large-12 medium-12 small-12" style="color: #FF0000">
                                * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                            </div>
                       
                    </div>
              
    
    
         <div class="clear1"></div>
        <div class="large-12 medium-12 small-12">
           
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd_report" runat="server" AutoGenerateColumns="False" Width="100%"
                                 CssClass="table table-hover" GridLines="None" Font-Size="12px" AllowPaging="true" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Credit Node">
                                        <ItemTemplate>
                                            <a target="_blank" href="../Accounts/CreditNodeDomDetails.aspx?RefundID=<%#Eval("RefundID")%>">
                                                <asp:Label ID="lblRefundID" runat="server" Text='<%#Eval("RefundID") %>' ForeColor="#004b91"
                                                    Font-Bold="True" Font-Underline="True"></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agency Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label><asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
                                            
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
                                    <asp:TemplateField HeaderText="Total Fare">
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
                                    <asp:TemplateField HeaderText="Refund Fare">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefund" runat="server" Text='<%#Eval("RefundFare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcancel" runat="server" Text='<%#Eval("RegardingCancel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblexecutive" runat="server" Text='<%#Eval("ExecutiveID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive Rejected Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRejectComment" runat="server" Text='<%#Eval("RejectComment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive Updated Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpComment" runat="server" Text='<%#Eval("UpdateRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("SubmitDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accept Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldateA" runat="server" Text='<%#Eval("AcceptDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldateU" runat="server" Text='<%#Eval("UpdateDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
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
               
        </div>

        </div>
   
    <%--<script type="text/javascript">
        var cal1 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_From']);
        cal1.year_scroll = true;
        cal1.time_comp = true;
        var cal2 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_To']);
        cal2.year_scroll = true;
        cal2.time_comp = true;	
    </script>--%>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
