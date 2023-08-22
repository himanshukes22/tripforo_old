<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Ledger.aspx.vb" Inherits="Reports_Accounts_Ledger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
   <%-- <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />--%>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57 || charCode == 08) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        a {
    color: #000000;
    font-weight: 600;
}
    </style>

       <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Accounts</a></li>
        <li><a href="#">Ledger Details</a></li>
        
    </ol>


    <div class="card-main">

    
        <div class="card-body">

            <div class="inner-box">

                <div class="row">
                    <div class="col-md-3">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        <input type="text" name="From" id="From" placeholder="From Date" class="theme-search-area-section-input" readonly="readonly" />
                                </div>
                              </div>
                    </div>
                    <div class="col-md-3">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        <input type="text" name="To" placeholder="To Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
                                </div>
                              </div>
                    </div>
                    <div class="col-md-3">
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="theme-search-area-section-input" placeholder="Amount" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
                             </div>
                            </div>
                    <div class="col-md-3">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-tag"></i>
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="theme-search-area-section-input" placeholder="Order Number"></asp:TextBox>
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
                        <asp:TextBox ID="txtAirCode" runat="server" CssClass="theme-search-area-section-input" placeholder="Air Code"></asp:TextBox>
                                </div>
                             </div>
                    </div>  
                    <div class="col-md-3" id="tr_SearchType" runat="server" visible="false">
                        <div class="form-groups">
                            <asp:RadioButton ID="RB_Agent" runat="server" Checked="true" GroupName="Trip" onclick="ShowLed(this)" Text="Agent" />
                            <asp:RadioButton ID="RB_Distr" runat="server" GroupName="Trip" onclick="HideLed(this)" Text="Own" />
                        </div>
                    </div>
                    <div class="col-md-3" id="tr_UploadType" runat="server">
                        <asp:RadioButtonList ID="RBL_Type" runat="server" class="theme-search-area-section-input" AutoPostBack="True" RepeatDirection="Horizontal"
                            CellPadding="2" CellSpacing="2">
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3" id="tr_Cat" runat="server">
                        <asp:DropDownList ID="ddl_Category" class="theme-search-area-section-input" runat="server">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-3" id="tr_BookingType" runat="server">
                          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-document"></i>
                        <asp:DropDownList ID="ddl_BookingType" class="theme-search-area-section-input" runat="server">
                        </asp:DropDownList>
                                </div>
                              </div>
                    </div>
                    </div>
                <br />
                <div class="row">
                   
                    <div class="col-md-3" id="tr_AgencyName" runat="server">
                        <input type="text" class="theme-search-area-section-input" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>

                     <div class="btn-search col-md-3">
                      
                         <asp:LinkButton ID="btn_search" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>


                            <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>

                        </div>


               
                </div>

                <div class="clear"></div>


                <%--                <div class="col-md-9">
                    <div class="row">--%>
                <%-- <div class="col-md-3">
                            <input type="text" name="From" id="From" placeholder="From Date" class="form-control" readonly="readonly" />
                        </div>--%>
                <%-- <div class="col-md-3">
                            <input type="text" name="To" placeholder="To Date" id="To" class="form-control" readonly="readonly" />
                        </div>--%>
                <%--<div class="col-md-3" id="tr_UploadType" runat="server">
                            <asp:RadioButtonList ID="RBL_Type" runat="server" class="form-control" AutoPostBack="True" RepeatDirection="Horizontal"
                                CellPadding="2" CellSpacing="2">
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-3" id="tr_Cat" runat="server">
                            <asp:DropDownList ID="ddl_Category" class="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3" id="tr_BookingType" runat="server">
                            <asp:DropDownList ID="ddl_BookingType" class="form-control" runat="server">
                            </asp:DropDownList>
                        </div>--%>

                <%--<div id="tr_Agency" runat="server"></div>--%>
                <%--   <div class="col-md-3" id="tr_AgencyName" runat="server">
                            <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                        </div>--%>

                <%--  <div class="col-md-12 col-xs-12">
                            
                        <div class="btn-search">
                            <asp:Button ID="btn_search" runat="server" class="btn cmn-btn" Text="Search Result" />
                             </div>
                        <div class="btn-export">
                            <asp:Button ID="btn_export" runat="server" class="btn cmn-btn" Text="Export" />
                            </div>
                        </div>--%>


                <%--                </div>
                </div>--%>
            </div>


        </div>
        <div class="clear1"></div>
    </div>

    <div class="clear1"></div>

     <div class="" runat="server">
    
    <div class="table-responsive">
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <asp:GridView ID="Grid_Ledger" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="rtable" GridLines="Both" Font-Size="12px" PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="AgencyID" HeaderText="AgencyID" visible="false"/>
                        <asp:BoundField DataField="AgencyName" HeaderText="Agency_Name" visible="false"/>
                        <asp:TemplateField HeaderText="Order No">
                            <ItemTemplate>

                                <span><%#Eval("Link")%>
                                    <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                    &nbsp;(Invoice)</a>
                                </span>


                                <%-- <%If(Eval("ValinFlt")) > 0 Then%>) 
                                            <a href='IntInvoiceDetails.aspx?OrderId=<%#Eval("InvoiceNo")%>&amp;invno=<%#Eval("InvoiceNo")%>&amp;tktno=<%#Eval("TicketNo")%>&amp;AgentID=<%#Eval("AgentId") %>'
                                                style="color: #004b91; font-size: 13px; font-weight: bold" target="_blank">
                                                <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label><br/>
                                                &nbsp;(Invoice)</a>

                                               <%Else%>
                                               <asp:Label ID="Label1" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label><br/>
                                             <%End If%>--%>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Pnr">
                            <ItemTemplate>
                                <span><%#Eval("InvoiceLink")%><asp:Label ID="Pnr" runat="server" Text='<%#Eval("PnrNo")%>'></asp:Label>

                                </span>
                                <%-- <a href='<%#Eval("InvoiceLink")%>' rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                <asp:Label ID="Pnr" runat="server" Text='<%#Eval("PnrNo")%>'></asp:Label></a>--%>
                            </ItemTemplate>
                        </asp:TemplateField>







                        <asp:BoundField HeaderText="Aircode" DataField="Aircode"></asp:BoundField>
                        <asp:BoundField HeaderText="TicketNo" DataField="TicketNo"></asp:BoundField>
                        <asp:BoundField HeaderText="DR." DataField="Debit"></asp:BoundField>
                        <asp:BoundField HeaderText="CR." DataField="Credit"></asp:BoundField>
                        <asp:BoundField HeaderText="Balance" DataField="Aval_Balance"></asp:BoundField>
                        <%--<asp:BoundField HeaderText="DueAmount" DataField="DueAmount"></asp:BoundField>                                    --%>
                        <asp:BoundField HeaderText="Booking Type" DataField="BookingType"></asp:BoundField>
                        <asp:BoundField HeaderText="Created Date" DataField="CreatedDate"></asp:BoundField>
                        <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>
</div>

    <div style="text-align: center" class="col-lg-3 col-lg-push-8">

        <asp:Label ID="lblTitle" runat="server"></asp:Label>&nbsp; <span>
            <b>
                <asp:Label ID="lblClosingBal" runat="server"></asp:Label></b></span>

    </div>

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
