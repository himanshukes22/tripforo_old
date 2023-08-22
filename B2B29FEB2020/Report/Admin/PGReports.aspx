<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="PGReports.aspx.vb" Inherits="SprReports_Accounts_PGReports" %>

<%@ Register Src="~/UserControl/AccountsControl.ascx" TagPrefix="uc1" TagName="Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />--%>
    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Ticket Details</title></head><body>" + prtContent.innerHTML + "</body></html>");
            prtContent.innerHTML = "";
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

            prtContent.innerHTML = "";
            //prtContent.innerHTML = strOldOne;
        }
    </script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />


    <%--<link href="../../CSS/itz.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <%-- <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />    
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
    <div class="large-3 medium-3 small-12 columns">
                <uc1:Account runat="server" id="Settings" />
            </div>
      
    <div class="large-8 medium-8 small-12 columns end">
        <div class="large-12 medium-12 small-12 heading">
             <div class="large-12 medium-12 small-12 heading1">Domestic Sale Register</div>
            <div class="clear1"></div>
        
            <div class="large-1 medium-1 small-3 columns">From Date
            </div>
            <div class="large-2 medium-2 small-9  columns">
                <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                    />
            </div>
            <div class="large-1 medium-1 small-3 large-push-1 medium-push-1 columns">To Date
            </div>
            <div class="large-2 medium-2 small-9 large-push-1 medium-push-1 columns">
                <input type="text" name="To" id="To" class="txtCalander" readonly="readonly"/>
            </div>
            <div class="large-1 medium-1 small-3 large-push-2 medium-push-2 columns">OrderId
            </div>
            <div class="large-2 medium-2 small-9 large-push-2 medium-push-2 columns">
               <asp:TextBox ID="txt_OrderId" runat="server"></asp:TextBox>
            </div>
            <div class="clear"></div>
            <%--<span id="spn_Projects" runat="server">Payment Status </span> <span id="spn_Projects1" runat="server"> </span>--%>

    <%--   <div class="large-1 medium-1 small-3 columns">Status
            </div>
            <div class="large-2 medium-2 small-9  columns">                
                    <asp:DropDownList ID="drpPaymentStatus" runat="server">
                    </asp:DropDownList>               
            </div>
            <div id="td_Agency" runat="server" class="large-1 medium-1 small-3 columns large-push-2 medium-push-2 ">
                Agency Name                       
                        <div class="large-2 medium-2 small-2 columns end large-push-2 medium-push-2">--%>
    <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%>
    <%--    <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                        </div>
                 </div>
                    
            
        <div class="clear"></div>

        <div class="large-4 medium-6 small-12 large-push-8 medium-push-6">        
            <div class="large-4 medium-4 small-6 pull-right columns">               
                 <asp:Button
                    ID="btn_export" runat="server" Text="Export"/></div>
             <div class="large-6 medium-8 small-6 pull-right columns"><asp:Button ID="btn_result" runat="server" Text="Search Result" />
            </div>          
             <div class="clear"></div>
        </div>
<div style="color: #FF0000">* N.B: To get Today's transaction without above parameter,do not fill any field, only
                            click on search your transaction.
            </div>
        </div>
  <div class="clear"></div>
    </div>
        <div class="clear"></div>
    </div>
           
    <div align="center">
        <div class="clear1"></div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            
            <tr>
                <td>
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd_IntsaleRegis" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                CssClass="GridViewStyle" PageSize="30">
                                <Columns>--%>
    <%--<asp:TemplateField HeaderText="ORDERID">
                                        <ItemTemplate>
                                            <a href='IntInvoiceDetails.aspx?OrderId=<%#Eval("OrderId") %>&amp;invno=<%#Eval("OrderId") %>&amp;tktno=<%#Eval("TicketNumber") %>&amp;AgentID=<%#Eval("AgentId") %>'
                                                style="color: #004b91; font-size: 11px; font-weight: bold" target="_blank">
                                                <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                &nbsp;(Invoice)</a>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>--%>
    <%--<asp:TemplateField HeaderText="Order Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Tracking Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("Trackingid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank RefNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("BankRefNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="TId">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_gdspnr" runat="server" Text='<%#Eval("TId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIRLINE&nbsp;PNR">
                                        <ItemTemplate>                                            
                                            <asp:Label ID="lbl_totbookcost" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector1" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tkt" runat="server" Text='<%#Eval("AgentId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AGENCY&nbsp;NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector0" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                                                       
                                    <asp:TemplateField HeaderText="Service Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("ServiceType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                     <asp:TemplateField HeaderText="Error Message">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector2" runat="server" Text='<%#Eval("ErrorText")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                 
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
    <%--<asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
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
                    </asp:UpdateProgress>--%>
    <%--   </td>
            </tr>
        </table>
    </div>
    <div id="DivPrint" runat="server" visible="true">
    </div>--%>



    <style type="text/css">
        .page-wrapperss {
            background-color: #fff;
            margin-left: 15px;
        }
    </style>

    <div>
        <div class="mtop80"></div>
        <div class="large-12 medium-12 small-12">
            <div class="large-3 medium-3 small-12 columns">
                <uc1:Account runat="server" ID="Settings" />
            </div>
            <div class="large-8 medium-8 small-12 columns end" style="padding-top: 20px;">
                <div class="page-wrapperss">

                    <div class="panel panel-primary">
                        <div class="large-12 medium-12 small-12 heading" style="margin-left: 20px;">
                            <div class="large-12 medium-12 small-12 heading1">PG Report</div>
                        </div>
                        <div class="panel-body">

                            <div style="width: 45%; margin-left: 20px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">From Date</label>
                                        <input type="text" name="From" id="From" class="form-control" readonly="readonly" />
                                        <label for="exampleInputPassword1">To Date</label>
                                        <input type="text" name="To" id="To" class="form-control" readonly="readonly" />
                                        <label for="exampleInputPassword1">OrderId</label>
                                        <asp:TextBox ID="txt_OrderId" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="exampleInputEmail1">Status :</label>
                                        <asp:DropDownList CssClass="form-control" ID="drpPaymentStatus" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                <div class="form-group">
                                   
                                </div>
                            </div>--%>
                                <%-- <div class="col-md-3">
                                <div class="form-group">
                                    
                                </div>
                            </div>

                           <div class="col-md-3">
                            <div class="form-group">
                                
                                </div>
                                </div>
                         </div>--%>

                                <div class="row">

                                    <div class="col-md-3" id="td_Agency" runat="server">
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Agency Name</label>
                                            <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" class="form-control" />
                                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btn_result" Width="150px" runat="server" Text="Search Result" CssClass="button buttonBlue" />
                                            <asp:Button ID="btn_export" Width="150px" runat="server" Text="Export" CssClass="button buttonBlue" />
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                   
                                </div>
                            </div>--%>
                                </div>

                                <div class="row">
                                    <div style="color: #FF0000">
                                        * N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; margin-left: 20px;">
                                <div id="divReport" runat="server" visible="true" style="background-color: #fff; overflow-y: scroll;" class="large-12 medium-12 small-12">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">

                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grd_IntsaleRegis" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                                             CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="ORDERID">
                                        <ItemTemplate>
                                            <a href='IntInvoiceDetails.aspx?OrderId=<%#Eval("OrderId") %>&amp;invno=<%#Eval("OrderId") %>&amp;tktno=<%#Eval("TicketNumber") %>&amp;AgentID=<%#Eval("AgentId") %>'
                                                style="color: #004b91; font-size: 11px; font-weight: bold" target="_blank">
                                                <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                &nbsp;(Invoice)</a>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Order Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tracking Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("Trackingid")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank RefNo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("BankRefNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="TId">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_gdspnr" runat="server" Text='<%#Eval("TId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TotalAmount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_totbookcost" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Original Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_orgamount" runat="server" Text='<%#Eval("OriginalAmount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dis. Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("DiscountValue")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Merchant Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("MerAamount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector1" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Agent Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_tkt" runat="server" Text='<%#Eval("AgentId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="AGENCY&nbsp;NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector0" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("ServiceType")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payment Mode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PG Charges">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_PGcharges" runat="server" Text='<%#Eval("TotalCharges")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Card/Bank Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("CardName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_CDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Error Message">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sector2" runat="server" Text='<%#Eval("ErrorText")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="RowStyle" />
                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle CssClass="HeaderStyle" />
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div id="DivPrint" runat="server" visible="true"></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>





