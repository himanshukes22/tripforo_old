<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="AgentProxyTicket.aspx.vb" Inherits="Reports_Proxy_AgentProxyTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





                <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="#">Offline Booking Report</a></li>
        
    </ol>

    <div class="card-main">
       

        <div class="inner-box" style="padding:15px;">

            <div class="row">

                <div class="col-md-3">
                 
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <input type="text" name="From" id="From" class="theme-search-area-section-input" readonly="readonly" placeholder="Select Date" />
                                </div>
                        </div>
                </div>

                <div class="col-md-3">
                   <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <input type="text" name="To" id="To" class="theme-search-area-section-input"  placeholder="Select Date" readonly="readonly" style="" />
                                </div>
                       </div>
                </div>

                <div id="tr_ExecID" runat="server">


                    <div class="col-md-3">
                       <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:DropDownList ID="ddl_TripType" runat="server" CssClass="theme-search-area-section-input">
                            <asp:ListItem Value="Select">Select Trip Type</asp:ListItem>
                            <asp:ListItem Value="D">Domestic</asp:ListItem>
                            <asp:ListItem Value="I">International</asp:ListItem>
                        </asp:DropDownList>
                                </div>
                           </div>
                    </div>

                    <div class="col-md-3" id="td_Agency" runat="server">
                       <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                        <input type="text" id="txtAgencyName"  name="txtAgencyName" class="theme-search-area-section-input" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                </div>
                           </div>
                    </div>

                    <div class="col-md-3" style="display: none">
                        Exec ID :
                        <asp:DropDownList ID="ddl_ExecID" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                 
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:DropDownList ID="ddl_Status" CssClass="theme-search-area-section-input" runat="server">
                           <asp:ListItem>Select Status</asp:ListItem>
                        </asp:DropDownList>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
               
              <div class="btn-view col-md-3">
                <asp:LinkButton ID="btn_search" runat="server" Text="View Detail" CssClass="btn cmn-btn" ToolTip="Search by Date"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
              </div>
                    
         
       
            </div>

          
        </div>
    </div>

      <div id="divReport" class="card-main" runat="server" visible="false">
        <div class="card-header">
            <h3 class="main-heading"><asp:Button ID="btn_export" runat="server" CssClass="btn-export" Text="Download"/><i class="icofont-download-alt"></i></h3>         
        </div>
    <div class="table-responsive">

        <table>


            <asp:GridView ID="GridProxyDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="ProxyID"
                OnPageIndexChanging="GridProxyDetail_Changing" Width="100%" CssClass="rtable"
                PageSize="30" AllowPaging="True">
                <Columns>
                    <asp:TemplateField HeaderText="ProxyID">
                        <ItemTemplate>
                            <a id="ancher" href='PassengerDetail.aspx?ProxyID=<%#Eval("ProxyID")%>' onclick="wopen('package_detail.aspx', 'popup',600, 300); return false;"
                                rel="lyteframe" rev="width: 800px; height: 400px; overflow:hidden;" target="_blank"
                                style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #800000; font-weight: bold;">
                                <asp:Label ID="lbl_ProxyID" runat="server" Text='<%#Eval("ProxyID") %>'></asp:Label>(View)</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AgentID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_AgentID" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ag_Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Ag_Name" runat="server" Text='<%#Eval("Ag_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BookingType">
                        <ItemTemplate>
                            <asp:Label ID="lbl_BookingType" runat="server" Text='<%#Eval("BookingType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TravelType">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TravelType" runat="server" Text='<%#Eval("TravelType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ProxyFrom" runat="server" Text='<%#Eval("ProxyFrom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ProxyTo" runat="server" Text='<%#Eval("ProxyTo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DDate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DDate" runat="server" Text='<%#Eval("DepartDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RDate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_RDate" runat="server" Text='<%#Eval("ReturnDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Class" runat="server" Text='<%#Eval("Class") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Airlines">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Airlines" runat="server" Text='<%#Eval("Airlines") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Classes">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Classes" runat="server" Text='<%#Eval("Classes") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PaymentMode">
                        <ItemTemplate>
                            <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PaymentMode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Remark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alert Message" ItemStyle-ForeColor="Red">
                        <ItemTemplate>
                            <asp:Label ID="lbl_UpdateRemark" runat="server" Text='<%#Eval("UpdateRemark") %>'
                                ForeColor="Red" Font-Bold="True"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle ForeColor="Red"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trip">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ProxyType" runat="server" Text='<%#Eval("ProxyType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExecutiveID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ExecutiveID" runat="server" Text='<%#Eval("Exec_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RequestedDate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_requestDateTime" runat="server" Text='<%#Eval("requestDateTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AcceptedDate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_AcceptedDateTime" runat="server" Text='<%#Eval("AcceptedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UpdatedDate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_UpdatedDate" runat="server" Text='<%#Eval("UpdatedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Accept">
    <ItemTemplate>
   <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="Accept" CommandArgument='<%#Eval("ProxyID") %>' Font-Bold="True" Font-Underline="False">Accept</asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>--%>
                </Columns>
                <RowStyle CssClass="RowStyle" />
                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" Height="50px" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>


        </table>
    </div>
</div>
    



    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
