<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="CommissionReport.aspx.vb" Inherits="SprReports_Accounts_CommissionReport" %>


<%@ Register Src="~/UserControl/AccountsControl.ascx" TagPrefix="uc1" TagName="Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style>
        .absss {
            width:130%!important;
            max-width:130%!important;
        }
    </style>--%>
    <style>
    a, p, li, td, span {
    font-size: 11.5px;
    /* line-height: 24px; */
    font-family: "Amazon Ember",Arial,sans-serif;
    font-weight: 400;
}
        </style>
   

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
        }
    </script>
    <%-- <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

    <div class="container">
        <div class="card-header">
        <div class="col-md-9">
           <h3 style="text-align: center;">Commission Report</h3>
            <hr />
        </div>
            </div>
    <div class="card-body">
    <div class="row">
        <div class="col-md-9">
        <div class="row">
                <div class="col-md-3">
                    <label>From</label>
                    <input type="text" name="From" id="From" placeholder="select Date" class="form-control" readonly="readonly" />
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
                    <label>Order Id</label>
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
                    <asp:TextBox ID="txt_Airline" placeholder="Enter Airline" class="form-control" runat="server"></asp:TextBox>
              </div>
                <div class="col-md-3">
                   <%-- <label>Flight Type</label>--%>
                <asp:DropDownList ID="ddl_SelectDDL" runat="server">          
                                            <asp:ListItem Value="D">Dom.</asp:ListItem>
                                            <asp:ListItem Value="I">Int.</asp:ListItem>                
                                        </asp:DropDownList>
                 </div>
                </div>
            <br />
                <div class="row">
                <div class="col-md-3" id="td_Agency" runat="server">
                    <label>Agency Name/ID</label>
                    <input type="text" class="form-control" id="txtAgencyName" placeholder="EnterAgency Name/ID" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
                <div class="col-md-3" style="visibility: hidden;display:none;">
                    <span id="spn_Projects" runat="server">
                        <asp:DropDownList ID="DropDownListProject" runat="server" class="form-control">
                        </asp:DropDownList>
                    </span>
                </div>
                <div class="col-md-4">
            <asp:Button ID="btn_result" runat="server" class="btn btn-danger" Text="Search Result" />
            <asp:Button ID="btn_export" runat="server" class="btn btn-danger" Text="Export" />
        </div>
                    </div>

         </div>
        
        <div class="clear"></div>
     <%--   <div class=" " style="padding: 10px 10px 10px 10px;">
            <div class="col-md-9 col-md-push-1 col-xs-12">
                <div style="color: #FF0000">
                    * N.B: To get Today's Commission without above parameter,do not fill any field, only
                                click on search your booking.
                </div>
            </div>

        </div>--%>
    </div>
        </div>
        <br />
</div>






<%--    <div class="col-sm-12 col-xs-12">
        <div class="w98 auto">
            <div class="clear"></div>
        </div>
        <div class="clear"></div>
    </div>--%>
    <div align="center">
        <div class="clear1"></div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="padding-bottom: 5px; display: none">
                    <table id="PrintVisible" runat="server" visible="false">
                        <tr>
                            <td><b>Print Invoice Pages :</b>&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="TextBoxPrintNo" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;</td>
                            <td>
                                <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-warning" />
                                &nbsp;&nbsp;&nbsp;(Ex: 1-3 or 3-10)&nbsp;&nbsp; </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <div style="text-align: left;">

                        <asp:Label align="left" ID="totalsum" runat="server" Text=""></asp:Label>
                    </div>

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
                </td>
            </tr>
        </table>
    </div>

    <div class="table-responsive">
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd_IntsaleRegis" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
                                CssClass="rtable" GridLines="None" Font-Size="12px" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="ORDERID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label>
                                      <%--      <a href='IntInvoiceDetails.aspx?OrderId=<%#Eval("OrderId") %>&amp;invno=<%#Eval("OrderId") %>&amp;tktno=<%#Eval("TicketNumber") %>&amp;AgentID=<%#Eval("AgentId") %>'
                                                style="color: #004b91; font-size: 13px; font-weight: bold" target="_blank">
                                                <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                &nbsp;(Invoice)</a>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="EasyID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("EasyID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   <%-- <asp:TemplateField HeaderText="EasyTransNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("EasyTransNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                  <%--  <asp:TemplateField HeaderText="AGENTID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector" runat="server" Text='<%#Eval("AgentId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AGENCY&nbsp;NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector0" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="GDSPNR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_gdspnr" runat="server" Text='<%#Eval("GdsPnr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIRLINE&nbsp;PNR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_airpnrr" runat="server" Text='<%#Eval("AirlinePnr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SECTOR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector1" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VC">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector2" runat="server" Text='<%#Eval("VC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAX&nbsp;NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tittle" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                            &nbsp;<asp:Label ID="lbl_fname" runat="server" Text='<%#Eval("fname") %>'></asp:Label>
                                            &nbsp;<asp:Label ID="lbl_mname" runat="server" Text='<%#Eval("mname") %>'></asp:Label>&nbsp;<asp:Label
                                                ID="lbl_lname" runat="server" Text='<%#Eval("lname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAX&nbsp;TYPE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_paxtype" runat="server" Text='<%#Eval("Paxtype") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIRLINE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAirLine" runat="server" Text='<%#Eval("AirLine") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TICKET&nbsp;NUMBER">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tkt" runat="server" Text='<%#Eval("Ticketnumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="BASEFARE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_basefare" runat="server" Text='<%#Eval("basefare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="YQ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_yq" runat="server" Text='<%#Eval("YQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tottax" runat="server" Text='<%#Eval("TotalTax") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SERVICE&nbsp;TAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sertax" runat="server" Text='<%#Eval("servicetax") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRANFEE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tranfee" runat="server" Text='<%#Eval("TranFee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MGTFEE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tranfee" runat="server" Text='<%#Eval("MgtFee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="TOTAL&nbsp;DISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_totdis" runat="server" Text='<%#Eval("TotalDiscount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="TDS">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tds" runat="server" Text='<%#Eval("Tds") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOTAL&nbsp;FARE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_totfare" runat="server" Text='<%#Eval("totalfare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NETFARE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_totbookcost" runat="server" Text='<%#Eval("totalafterdis") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CREATE&nbsp;DDATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>

                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
    <div id="DivPrint" runat="server" visible="true">
    </div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>