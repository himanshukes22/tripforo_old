<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="LedgerWithRemark.aspx.vb" Inherits="Report_Accounts_LedgerWithRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />

    <div class="container">
        <div class="card-header">
            <div class="col-md-9">
                <h3 style="text-align: center;">Ledger Report</h3>
                <hr />
            </div>
        </div>
        <div class="card-body">
            <div class="col-md-12">
                <div class="row" style="position: relative; padding-bottom: 37px;">
                    <div class="form-inlines">
                        <div class="form-group" id="tr_SearchType" runat="server" visible="false">
                            <asp:RadioButton ID="RB_Agent" runat="server" Checked="true" GroupName="Trip" onclick="ShowLed(this)" Text="Agent" />
                            <asp:RadioButton ID="RB_Distr" runat="server" GroupName="Trip" onclick="HideLed(this)" Text="Own" />
                        </div>
                    </div>



                    <div class=" col-md-3">
                        <label>From</label>
                        <div class="inputWithIcon">

                            <input type="text" name="From" id="From" placeholder="Select Date" class="form-control" readonly="readonly" aria-describedby="basic-addon1" />
                            <i class="fa fa-calendar fa-lg fa-fw" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label>To</label>
                        <div class="inputWithIcon">
                            <input type="text" name="To" placeholder="Select Date" id="To" class="form-control" readonly="readonly" />
                            <i class="fa fa-calendar fa-lg fa-fw" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-md-3" id="tr_UploadType" runat="server">
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
                    </div>


                    <div class="col-md-3" id="tr_AgencyName" runat="server">
                        <input type="text" class="form-control" id="txtAgencyName" placeholder="Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>


                    <div class="col-md-3" style="position: relative; top: 25px;">
                        <label style="color: #0952a4;">.</label>
                        <asp:Button ID="btn_search" runat="server" class="btn btn-danger" Text="Search Result" />
                        <asp:Button ID="btn_export" runat="server" class="btn btn-danger" Text="Export" />
                    </div>




                </div>

            </div>

        </div>



        <br />
        <br />
    </div>

    <br />
    <br />

    <div class="">

        <div class="table-responsive">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Grid_Ledger" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="rtable" GridLines="Both">
                        <Columns>
                            <asp:BoundField HeaderText="Created Date" DataField="CreatedDate1"></asp:BoundField>
                            <asp:TemplateField HeaderText="Order No">
                                <ItemTemplate>

                                    <span><%#Eval("Link")%>
                                        <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                        <%--(Invoice)--%></a>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>


<%--                            <asp:TemplateField HeaderText="Pnr">
                                <ItemTemplate>
                                    <span><%#Eval("InvoiceLink")%><asp:Label ID="Pnr" runat="server" Text='<%#Eval("PnrNo")%>'></asp:Label>

                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="Invoicedetail" DataField="Invoicedetail"></asp:BoundField>
                            <asp:BoundField HeaderText="Product" DataField="BookingType"></asp:BoundField>
                            <asp:BoundField HeaderText="Debit" DataField="Debit"></asp:BoundField>
                            <asp:BoundField HeaderText="Credit" DataField="Credit"></asp:BoundField>
                            <asp:BoundField HeaderText="Balance" DataField="Aval_Balance"></asp:BoundField>
                            
                            <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                        </Columns>

                    </asp:GridView>





                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <hr />
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

