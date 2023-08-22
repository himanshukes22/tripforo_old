<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="LedgerSingleOrderID.aspx.vb" Inherits="Reports_Accounts_LedgerSingleOrderID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    
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


    <div class="card-main">
        <div class="card-header">
                <h3 class="main-heading">Ledger Report</h3>
        </div>
        <div class="card-body">
            <div class="inner-box">
                <div class="row">

                    <div class=" col-md-4">
                        <label>From</label>
                        <div class="inputWithIcon">
                            <input type="text" name="From" id="From" placeholder="Select Date" class="form-control" readonly="readonly" aria-describedby="basic-addon1" />
                            <i class="fa fa-calendar fa-lg fa-fw" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>To</label>
                        <div class="inputWithIcon">
                            <input type="text" name="To" placeholder="Select Date" id="To" class="form-control" readonly="readonly" />
                            <i class="fa fa-calendar fa-lg fa-fw" aria-hidden="true"></i>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <label>Amount</label>
                        <div class="inputWithIcon">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="Amount" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <i class="fa fa-inr" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>Order No</label>
                        <div class="inputWithIcon">
                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" placeholder="Order No"></asp:TextBox>
                            <i class="fa fa-inr" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>Air Code</label>
                        <div class="inputWithIcon">
                            <asp:TextBox ID="txtAirCode" runat="server" CssClass="form-control" placeholder="Air Code"></asp:TextBox>
                            <i class="fa fa-inr" aria-hidden="true"></i>
                        </div>
                    </div>
                    
                    <div class=" col-md-4" id="tr_SearchType" runat="server" visible="false">
                        <div class="form-inlines">
                            <div class="form-group">
                                <asp:RadioButton ID="RB_Agent" runat="server" Checked="true" GroupName="Trip" onclick="ShowLed(this)" Text="Agent" />
                                <asp:RadioButton ID="RB_Distr" runat="server" GroupName="Trip" onclick="HideLed(this)" Text="Own" />
                            </div>
                        </div>  
                     </div>
                    <div class="col-md-4" id="tr_UploadType" runat="server">
                        <asp:RadioButtonList ID="RBL_Type" runat="server" class="form-control" AutoPostBack="True" RepeatDirection="Horizontal"
                            CellPadding="2" CellSpacing="2">
                        </asp:RadioButtonList>
                    </div>

                    <div class="col-md-4" id="tr_Cat" runat="server">
                        <asp:DropDownList ID="ddl_Category" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4" id="tr_BookingType" runat="server">
                         <label>Booking Type</label>
                        <asp:DropDownList ID="ddl_BookingType" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>


                    <div class="col-md-4" id="tr_AgencyName" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>


                    <div class="col-md-12">                        
                        <div class="btn-search">
                          <asp:Button ID="btn_search" runat="server" class="btn cmn-btn" Text="Search Result" />
                        </div>
                        
                        <div class="btn-export">
                          <asp:Button ID="btn_export" runat="server" class="btn cmn-btn" Text="Export" />
                         </div>

                    </div>




                </div>

            </div>

        </div>


    </div>


    <div class="">

        <div class="table-responsive">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Grid_Ledger" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="rtable" GridLines="Both">
                        <Columns>
                            <asp:BoundField DataField="AgencyID" HeaderText="AgencyID" />

                            <asp:BoundField DataField="AgencyName" HeaderText="Agency_Name" />
                            <asp:TemplateField HeaderText="Order No">
                                <ItemTemplate>

                                    <span><%#Eval("Link")%>
                                        <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                        <%--(Invoice)--%></a>
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






                            <asp:BoundField HeaderText="Aircode" DataField="TicketingCarrier"></asp:BoundField>
                            <%--<asp:BoundField HeaderText="TicketNo" DataField="TicketNo"  ></asp:BoundField>  --%>
                            <asp:BoundField HeaderText="DR." DataField="Debit"></asp:BoundField>
                            <asp:BoundField HeaderText="CR." DataField="Credit"></asp:BoundField>
                            <asp:BoundField HeaderText="Balance" DataField="Aval_Balance"></asp:BoundField>
                            <%--<asp:BoundField HeaderText="DueAmount" DataField="DueAmount"></asp:BoundField>                                    --%>
                            <asp:BoundField HeaderText="Booking Type" DataField="BookingType"></asp:BoundField>
                            <asp:BoundField HeaderText="Created Date" DataField="CreatedDate1"></asp:BoundField>
                            <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                        </Columns>

                    </asp:GridView>





                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <div>

            <asp:Label ID="lblTitle" runat="server"></asp:Label>&nbsp; <span>
                <b>
                    <asp:Label ID="lblClosingBal" runat="server"></asp:Label></b></span>

        </div>

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


    <script type="text/javascript">
        $(document).ready(function () {
            $("#accordian a").click(function () {
                var link = $(this);
                var closest_ul = link.closest("ul");
                var parallel_active_links = closest_ul.find(".active")
                var closest_li = link.closest("li");
                var link_status = closest_li.hasClass("active");
                var count = 0;

                closest_ul.find("ul").slideUp(function () {
                    if (++count == closest_ul.find("ul").length)
                        parallel_active_links.removeClass("active");
                });

                if (!link_status) {
                    closest_li.children("ul").slideDown();
                    closest_li.addClass("active");
                }
            })
        })
    </script>





</asp:Content>
