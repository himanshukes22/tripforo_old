<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="DomSaleRegister.aspx.vb" Inherits="Reports_Accounts_DomSaleRegister" %>

<%@ Register Src="~/UserControl/AccountsControl.ascx" TagPrefix="uc1" TagName="Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%--<link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />--%>
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




    <%--<style>
        .asvd {
            width:120%!important;
            max-width:120%!important;
        }
    </style>--%>

    <%--<link href="../../CSS/itz.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

   <%-- <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />--%>
 
    

       <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Accounts</a></li>
        <li><a href="#">Domestic Sale Register</a></li>
        
    </ol>


   <div class="card-main">
       
          <div class="card-body"> 
           
       <div class="inner-box" >
            <div class="row">               
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                       
                        <input type="text" name="From" id="From" placeholder="Select Date" class="theme-search-area-section-input" readonly="readonly" />
                        </div>
                            </div>
                    </div>
                    <div class="col-md-3">
                        
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                        <input type="text" name="To" placeholder="Select Date" id="To" class="theme-search-area-section-input" readonly="readonly" />
                           </div>
                            </div>
                    </div>
                    <div class="col-md-3">
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:TextBox ID="txt_PNR" placeholder="Enter PNR" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                             </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:TextBox ID="txt_OrderId" placeholder="Enter OrderId" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                    </div>

                </div>
           <br />
           <div class="row">
                    <div class="col-md-3">
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                        <asp:TextBox ID="txt_PaxName" placeholder="Enter Pax Name" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                             </div>
                    </div>
                    <div class="col-md-3">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:TextBox ID="txt_TktNo" placeholder=" Enter Ticket No." class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                    <div class="col-md-3">
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-plane"></i>
                        <asp:TextBox ID="txt_Airline" placeholder="Enter Airline" class="theme-search-area-section-input" runat="server"></asp:TextBox>
                                </div>
                             </div>
                    </div>

                    <div class="col-md-3" id="td_Agency" runat="server">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-user"></i>
                        <input type="text" class="theme-search-area-section-input" id="txtAgencyName" placeholder="Enter Agency Name or ID" name="txtAgencyName" onfocus="focusObj(this);"
                            onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                </div>
                            </div>
                    </div>
                    <div class="col-md-3" style="visibility: hidden;display:none;">
                        <span id="spn_Projects" runat="server">
                            <asp:DropDownList ID="DropDownListProject" runat="server" class="theme-search-area-section-input">
                            </asp:DropDownList>
                        </span>
                    </div>

                 <div class="btn-search col-md-3">
                       <asp:LinkButton ID="btn_result" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                            <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>

                     </div>
            
         <%--   <div class="col-md-12">
                
                    
                    <div class="btn-export">
                         <asp:Button ID="btn_export" runat="server" class="btn cmn-btn" Text="Export" />                            
                    </div>
            </div>--%>

                
                </div>
   
        </div>
           
        </div>
      </div>

           


   


    <div align="center">
        <div class="clear1"></div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="padding-bottom: 5px; display: none">
                    <table id="PrintVisible" runat="server" visible="false">
                        <tr>
                            <td>
                                <b>Print Invoice Pages :</b>&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxPrintNo" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="buttonfltbk" />
                                &nbsp;&nbsp;&nbsp;(Ex: 1-3 or 3-10)&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                  
                </td>
            </tr>
        </table>
    </div>


     <div class="" runat="server">
     
    <div class="table-responsive">
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd_IntsaleRegis" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                CssClass="rtable" GridLines="Both" Font-Size="12px" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="ORDERID">
                                        <ItemTemplate>
                                            <a href='IntInvoiceDetails.aspx?OrderId=<%#Eval("OrderId") %>&amp;invno=<%#Eval("OrderId") %>&amp;tktno=<%#Eval("TicketNumber") %>&amp;AgentID=<%#Eval("AgentId") %>'
                                                style="color: #000; font-size: 11px; font-weight: bold" target="_blank">
                                                <asp:Label ID="lbl_order" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                &nbsp;(Invoice)</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EasyID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sectorEA" runat="server" Text='<%#Eval("EasyID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EasyTransNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sectorE" runat="server" Text='<%#Eval("EasyTransNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AgencyID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sectorA" runat="server" Text='<%#Eval("AgencyID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AGENCY&nbsp;NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sector0" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                            &nbsp;<asp:Label ID="lbl_fname" runat="server" Text='<%#Eval("fname") %>'></asp:Label>&nbsp;<asp:Label
                                                ID="lbl_mname" runat="server" Text='<%#Eval("mname") %>'></asp:Label>&nbsp;<asp:Label
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
                                    <asp:TemplateField HeaderText="BASEFARE">
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_totdis" runat="server" Text='<%#Eval("TotalDiscount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TDS">
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
                                    <asp:TemplateField HeaderText="CREATEDDATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>  
                        </div>
</div>

    <div id="DivPrint" runat="server" visible="true">
    </div>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script type="text/javascript">
        function datediffreance() {

            var dat1 = document.getElementById('From').value;

            if (dat1 == "") {
                dat1 = new Date(yyyy, mm, dd);
            }
            var date1 = new Date(dat1)//converts string to date object
            // alert(date1);
            var dat2 = document.getElementById('To').value;
            if (dat2 == "") {
                dat2 = new Date(yyyy, mm, dd);
            }
            var date2 = new Date(dat2)
            //alert(date2);

            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var diffDays = Math.abs((date1.getTime() - date2.getTime()) / (oneDay));
            if (diffDays >= 61) {
                alert("Selected date must be less than two months");
                return false;
            }
        }
    </script>
</asp:Content>
