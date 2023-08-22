<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="StaffTransaction.aspx.vb" Inherits="Reports_Accounts_StaffTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
<%--    <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />--%>
    <%--<style>

        .msi {
            width:130%!important;
            max-width:130%!important;
        }
    </style>--%>
   
    
       <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Accounts</a></li>
        <li><a href="#">Staff Transaction Details</a></li>
        
    </ol>

    <div class="card-main">
       
        <div class="card-body">
        <div class="inner-box ">
            <div class="row">




                    <div class="col-md-3 col-xs-12">
                       <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        
                            <input type="text" name="From" id="From" placeholder="Select Date" class="theme-search-area-section-input" readonly="readonly" />
                           </div>
                           </div>
                        
                    </div>

                    <div class="col-md-3 col-xs-12">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                            <input type="text" name="To" placeholder="Select Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
                          </div>
                            </div>
                    </div>


                    <div class="col-md-3 col-xs-12" id="tr_Cat" runat="server" style="display: none;">
                        <asp:DropDownList ID="ddl_Category" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-12" id="tr_BookingType" runat="server" style="display: none;">
                        <asp:DropDownList ID="ddl_BookingType" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>

                  <div class="btn-search col-md-3 col-xs-12">
                           
                        
                      <asp:LinkButton ID="btn_search" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>

                            <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>
                        </div>

               <%--     <div class="col-md-12 col-xs-12">
                        
                      
                        
                        <div class="btn-export">
                          <asp:Button ID="btn_export" runat="server" class="btn cmn-btn" Text="Export" />
                      </div>

                    </div>--%>
                    
                       
                  
                </div>



            </div>

        </div>
            </div>


 

      <div class="" runat="server">
     
    <div class="table-responsive">

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <asp:GridView ID="Grid_Ledger" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="rtable" GridLines="Both" Font-Size="12px" PageSize="30">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <%--<asp:BoundField HeaderText="OrderId" DataField="OrderId" />--%>
                        <asp:TemplateField HeaderText="Order ID">
                            <ItemTemplate>
                                <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                    <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ServiceType" DataField="ServiceType" />
                        <asp:BoundField HeaderText="Module" DataField="Module" />
                        <asp:BoundField HeaderText="TransAmount" DataField="TransAmount" />
                        <asp:BoundField HeaderText="BookedBy" DataField="CreatedBy" />
                        <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                        <asp:BoundField HeaderText="AgencyId" DataField="AgencyId" />
                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                        <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate" />
                        <asp:BoundField HeaderText="Debit" DataField="Debit" />
                        <asp:BoundField HeaderText="Credit" DataField="Credit" />
                        <asp:BoundField HeaderText="Balance" DataField="AvalBal" />
                        <asp:BoundField DataField="AgentMobile" HeaderText="AgentMobile" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField HeaderText="StaffId" DataField="StaffId" />

                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</div>

    <div style="text-align: center" class="">

        <asp:Label ID="lblTitle" runat="server"></asp:Label>&nbsp; <span>
            <b>
                <asp:Label ID="lblClosingBal" runat="server"></asp:Label></b></span>

    </div>
    <div class="clear"></div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/JS/Distributor.js") %>"></script>
    <style type="text/css">
        .bdrbtm1 {
            border-bottom: 2px solid #ddd;
        }
    </style>
</asp:Content>
