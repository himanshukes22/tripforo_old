<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="RejectedTicket.aspx.vb" Inherits="SprReports_RejectedTicket" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />


    <%-- <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <%--<link rel="stylesheet" href="../chosen/chosen.css" />--%>
    <%-- <link href="../CSS/style.css" rel="stylesheet" type="text/css" />--%>

    <%--    <script src="../chosen/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script src="../chosen/chosen.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>--%>
   <%-- <style>
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
    padding: 8px 0px 10px 2px;
    line-height: 1.42857143;
    vertical-align: top;
    border-top: 1px solid #ddd;
    text-align:center;
}
    </style>--%>
    


   

     


  <div class="container">
    <div class="card-header">
        <div class="col-md-9">
            <h3 style="text-align: center;">Rejected Report</h3>
            <hr />
        </div>
    </div>
    <div class="card-body">
        <div class="col-md-9">
           <div class="row">
                <div class="col-md-3">
                    <label>From</label>
                    <input type="text" name="From" id="From" placeholder="Select Date" class="form-control" readonly="readonly" />
                </div>
                <div class="col-md-3">
                    <label>To</label>
                    <input type="text" name="To" placeholder="Select Date" id="To" class="form-control" readonly="readonly" />
                </div>
                <div class="col-md-3">
                    <label>PNR</label>
                    <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Order ID</label>
                    <asp:TextBox ID="txt_OrderId" placeholder="Enter OrderId" class="form-control" runat="server"></asp:TextBox>
                </div>
               </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <label>Pax Name</label>
                    <asp:TextBox ID="txt_PaxName" placeholder="Enter Pax Name" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Ticket No.</label>
                    <asp:TextBox ID="txt_TktNo" placeholder="Enter TicketNo" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Airline</label>
                    <asp:TextBox ID="txt_AirPNR" placeholder="Enter Airline" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3" id="tdTripNonExec2" runat="server">
                    <label>Flight Type</label>
                    <asp:DropDownList class="form-control" ID="ddlTrip" runat="server">
                        <asp:ListItem Value="">-----Select-----</asp:ListItem>
                        <asp:ListItem Value="D">Domestic</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                    </asp:DropDownList>
                </div>
                </div>
            <br />
            <div class="row">
                <div class="col-md-3" id="td_Agency" runat="server">
                    <label>Agency Name/ID</label>
                    <input type="text" class="form-control" id="txtAgencyName" placeholder="Enter Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
                <div class="col-md-4">
            <asp:Button ID="btn_result" runat="server" class="btn btn-danger" Text="Search Result" />
            <asp:Button ID="btn_export" runat="server" class="btn btn-danger" Text="Export" />
        </div>
                </div>
            <br />
        </div>
        
    <%--    <div class="row" style="padding: 10px 10px 10px 10px;">
            <div class="col-md-9 col-xs-12 col-md-push-1">
                <div style="color: #FF0000">
                    * N.B: To get Today's booking without above parameter,do not fill any field, only
                                click on search your booking.
                </div>
            </div>

        </div>--%>
    </div>
      </div>

    <br />


 
       
            <div class="table-responsive">
               
                <table border="0" cellpadding="0" cellspacing="0"  Class="table table-hover">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False"  CssClass="rtable" GridLines="None" Font-Size="12px"
                                        PageSize="30">
                                        <Columns>
                                            <%-- <asp:TemplateField HeaderText="Pax Type">
                                        <ItemTemplate>
                                            <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Pax ID">
                                        <ItemTemplate>
                                            <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                                rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                color: #004b91">
                                                <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Order ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pnr">
                                                <ItemTemplate>
                                                    <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Ticket No">
                                        <ItemTemplate>
                                            <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Agent ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="AgentID" runat="server" Text='<%#Eval("AgentId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Executive ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="ExcutiveID" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rejected Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="RejectRemark" runat="server" Text='<%#Eval("RejectedRemark")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AirLine">
                                                <ItemTemplate>
                                                    <asp:Label ID="Airline" runat="server" Text='<%#Eval("VC")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Sector" DataField="sector"></asp:BoundField>
                                            <asp:BoundField HeaderText="Trip" DataField="trip"></asp:BoundField>
                                            <asp:BoundField HeaderText="Net Fare" DataField="TotalAfterDis">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                                            <asp:BoundField HeaderText="Booking Date" DataField="CreateDate"></asp:BoundField>
                                            <asp:TemplateField HeaderText="PaymentMode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
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
                        </td>
                    </tr>
                </table>

            </div>
  




 
    <%-- <script type="text/javascript">
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
