<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="ProxyPaxDetails.aspx.vb" Inherits="Report_Proxy_ProxyPaxDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Flight</a></li>
        <li><a href="DashboardForOffline.aspx">Offline Request</a></li>
              

        <li><a href="ProxyPaxUpdate.aspx">Update Request</a></li>
       <li class="dropdown">
                <a href="#lala" class="dropdown-toggle" data-toggle="dropdown" >
                   Manage Booking
                   <b class="caret"></b>
                </a>
                <ul class="dropdown-menu" role="listbox">
                   <li class="divider"></li>
                   <li><a href="Proxy.aspx" role="option">View Request</a></li>
                   <li class="divider"></li>
                   <li><a href="ProxyPaxUpdate.aspx" role="option">Update Request</a></li>
                </ul>
             </li>

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

    <div id="divReport" runat="server" visible="true">
        <%--<div class="card-header">
        </div>--%>
        <div class="table-responsive">
            <%-- style="height:200px;overflow:scroll;"--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Update_GV" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="30">

                        <Columns>
                           <%-- <asp:TemplateField HeaderText="Proxy ID">
                                
                                <ItemTemplate>
                                <asp:Label ID="Pid" runat="server" Text='<%# Eval("ProxyID") %>'></asp:Label>

                                    <i class="fa fa-user" style='<%# Eval("NameGiven").ToString() == "True" ? display:"inline-block": display:"none"%>'></i>

                                    <a href='<%# "ProxyEmp.aspx?Proxyid =" + Eval("ProxyID") %>' &Adult='<%# Eval("Adult") %>' &Child='<%# Eval("Child") %>' &Infrant='<%# Eval("Infrant") %>' 
                   
                                      &TravelType='<%# Eval("TravelType") %>' &AgentID =' <%# Eval("AgentID") %>'  rel="lyteframe" rev="width: 1200px; height: 500px; overflow:hidden;" target="_blank"
                                        
        style='<%# Eval("NameGiven").ToString() == "False" ? display:"inline-block": display:"none"%>'; font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight:bold; color: #004b91>
                                       
                                        <i class="fa fa-pencil-square-o"></i>
                                         
                                        <i class="fa fa-user-o" style='<%# Eval("NameGiven").ToString() == "False" ? "display:inline-block": "display:none"%>'></i>

                                    </a>



                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Request Date&Time">
                                <ItemTemplate>
                                    <asp:Label ID="rdt" runat="server" Text='<%# Eval("requestDateTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="st" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="Travel Type">
                                <ItemTemplate>
                                    <asp:Label ID="tt" runat="server" Text='<%# Eval("TravelType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:Label ID="fr" runat="server" Text='<%# Eval("ProxyFrom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="To">
                                <ItemTemplate>
                                    <asp:Label ID="to" runat="server" Text='<%# Eval("ProxyTo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Dep Date">
                                <ItemTemplate>
                                    <asp:Label ID="dd" runat="server" Text='<%# Eval("DepartDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            

                            <asp:TemplateField HeaderText="No Of Adult">
                                <ItemTemplate>
                                    <asp:Label ID="noa" runat="server" Text='<%# Eval("Adult") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No Of Child">
                                <ItemTemplate>
                                    <asp:Label ID="noc" runat="server" Text='<%# Eval("Child") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No of Infant">
                                <ItemTemplate>
                                    <asp:Label ID="noi" runat="server" Text='<%# Eval("Infrant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="c" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Prefered Airline">
                                <ItemTemplate>
                                    <asp:Label ID="pa" runat="server" Text='<%# Eval("Airlines") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            

                             <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="rmk" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Agent Name">
                                <ItemTemplate>
                                    <asp:Label ID="an" runat="server" Text='<%# Eval("Ag_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expected Amount">
                                <ItemTemplate>
                                    <asp:Label ID="ea" runat="server" Text='<%# Eval("ExpectedAmount") %>'></asp:Label>
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
     <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
     </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

