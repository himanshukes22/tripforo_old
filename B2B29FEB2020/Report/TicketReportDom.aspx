<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="TicketReportDom.aspx.vb" Inherits="Reports_TicketReportDom" %>

<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    

    <script src="<%=ResolveUrl("~/Scripts/ReissueRefund.js")%>" type="text/javascript"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PopupScript.js")%>"></script>

    <script language="JavaScript" type="text/javascript">

        function popupLoad(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
           
            ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType);
            if (RqustType == "Reissue") {
                $("#trCancelledBy").hide();
            }
            else {
                $("#trCancelledBy").show();
            }            
            loading(); // loading
            setTimeout(function() { // then show popup, deley in .5 second
                loadPopup(); // function show popup 
            }, 100); // .5 second
            if ($('.drop option').length == 0) {
                $("#trCancelledBy").hide();
            }
        }
       
      
    </script>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
   <div class="large-3 medium-3 small-12 columns">
       <uc1:LeftMenu runat="server" ID="LeftMenu" />     
   </div>
    <div class="large-8 medium-8 small-12 columns">
        
            <div class="large-12 medium-12 small-12">
                        <div class="large-12 medium-12 small-12 bld blue">
                            Domestic Ticket Report
                        </div>
                    <div class="clear1"></div>
                    
                               
                                    <div class="large-2 medium-3 small-3 columns">
                                        From Date
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly" />
                                             
                                    </div>
                                    <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">
                                        To Date
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" />
                                    </div>
                                    <div class="clear"></div>

                                    <div class="large-2 medium-3 small-3 columns">
                                        PNR
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:TextBox ID="txt_PNR" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">
                                        OrderId
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:TextBox ID="txt_OrderId" runat="server"></asp:TextBox>
                                    </div>
                                <div class="clear"></div>
                       

                
                
       
                                    <div class="large-2 medium-3 small-3 columns">
                                        Pax Name
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:TextBox ID="txt_PaxName" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">
                                       Ticket No
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:TextBox ID="txt_TktNo" runat="server"></asp:TextBox>
                                    </div>
                 <div class="clear"></div>

                                    <div class="large-2 medium-3 small-3 columns">
                                        Airline
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:TextBox ID="txt_AirPNR" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">
                                        &nbsp;
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        &nbsp;
                                    </div>
                                </div>

                  <div class="clear1"></div>
                        
                                    <div class="large-12 medium-12 small-12" id="td_Agency" runat="server">
                                        
                                                <div class="large-2 medium-3 small-3 columns">
                                                    Agency Name
                                                </div>
                                                <div class="large-2 medium-2 small-9 columns">
                                                    <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%>
                                                    <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                                </div>
                                            <div class="large-8 medium-7 small-12 columns">&nbsp;</div>
                                    </div>


                                <div class="clear1"></div>


                               <div class="large-6 medium-6 small-12 large-push-5  medium-push-5">
                          <div class="large-6 medium-6 small-6 columns"> <asp:Button ID="btn_result" CssClass="buttonfltbk" runat="server" Text="Search Result"  /></div>  
                                  <div class="large-6 medium-6 small-6 columns"> <asp:Button ID="btn_export" CssClass="buttonfltbk" runat="server" Text="Export" /></div>
                                    </div>
                 <div class="clear1"></div>
                    
                        <div class="large-10 medium-12 small-12 large-push-1" style="color: #FF0000">
                            * N.B: To get Today's booking without above parameter,do not fill any field, only
                            click on search your booking.
                        </div>
                    
                
           <div class="clear1"></div>
    
        
                    <div class="large-9 medium-12 small-12 large-push-1 columns">
                       
                            
                                <div class="large-6 medium-6 small-12 columns">
                                    Total Ticket Sale :
                                    <asp:Label ID="lbl_Total" runat="server"></asp:Label></div>
                                <div class="large-6 medium-6 small-12 columns">
                                    Total Ticket Issued :
                                    <asp:Label ID="lbl_counttkt" runat="server"></asp:Label></div>
                           
                       
                    </div>

        <div class="clear1"></div>
        </div>
                  <div id="toPopup" class="tbltbl large-12 medium-12 small-12">
                        <div class="close">
                        </div>
                        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
                        <div id="popup_content">
                            <!--your content start-->
                            <table border="0" cellpadding="10" cellspacing="5" style="font-family: arial, Helvetica, sans-serif;
                                font-size: 12px; font-weight: normal; font-style: normal; color: #000000">
                                <tr>
                                    <td>
                                        <b>PNR :</b> <span id="PNR"></span>
                                    </td>
                                    <td>
                                       <b> Ticket No:</b> <span id="TktNo"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b>PAX NAME :</b> <span id="Paxname"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b class="lft"> Remark:</b>
                                            <span><input id="RemarksType" type="text" name="RemarksType" readonly="readonly" style="border-style: none;
                                                font-weight: bold;" /></span>
                                           
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <textarea id="txtRemark" name="txtRemark" cols="40" rows="4" style="border: thin solid #808080"></textarea>
                                    </td>
                                </tr>
                                <tr id="trCancelledBy" visible="false">
                                    <td>
                                        <b>Cancelled By:</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpCancelledBy" CssClass="drop" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div align="right">
                                            <asp:Button ID="btnRemark" runat="server" Text="Submit" CssClass="button rgt" OnClientClick="return validateremark();" />
                                            <input id="txtPaxid" name="txtPaxid" type="hidden" /><input id="txtPaxType" name="txtPaxType"
                                                type="hidden" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--your content end-->
                    </div>
                    <div class="loader">
                    </div>
                    <div id="backgroundPopup">
                    </div>
               
                
            
      
    <div class="large-12 medium-12 small-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="ticket_grdview" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px"
                                PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Pax Type" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="PaxType" runat="server" Text='<%#Eval("PaxType")%>' ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax ID" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=<%#Eval("PaxId")%>'
                                                rel="lyteframe" rev="width: 900px; height: 500px; overflow:hidden;" target="_blank"
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                color: #004b91">
                                                <asp:Label ID="TID" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>(TktDetail)
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order ID">
                                        <ItemTemplate>
                                            <a href='PnrSummaryIntl.aspx?OrderId=<%#Eval("OrderId")%> &TransID=' rel="lyteframe"
                                                rev="width: 900px; height: 500px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: 12px; font-weight: bold; color: #004b91">
                                                <asp:Label ID="OrderID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pnr" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="GdsPNR" runat="server" Text='<%#Eval("GdsPnr")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticket No" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="TktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax Name">
                                        <ItemTemplate>
                                            <asp:Label ID="PaxFNAme" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                            <asp:Label ID="PaxLName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent ID">
                                        <ItemTemplate>
                                            <asp:Label ID="AgentID" runat="server" Text='<%#Eval("AgentId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agency Name" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="AgencyName" runat="server" Text='<%#Eval("AgencyName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excutive ID" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="ExcutiveID" runat="server" Text='<%#Eval("ExecutiveId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AirLine" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemTemplate>
                                            <asp:Label ID="Airline" runat="server" Text='<%#Eval("VC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="SECTOR" DataField="sector" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger"></asp:BoundField>
                                    <asp:BoundField HeaderText="Net Fare" DataField="TotalAfterDis" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger">
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger"></asp:BoundField>
                                    <asp:BoundField HeaderText="Booking Date" DataField="CreateDate" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger"></asp:BoundField>
                                    <asp:TemplateField HeaderText="REISSUE">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkreissue" runat="server" Font-Strikeout="False" Font-Overline="False"
                                                CommandArgument='<%#Eval("PaxId") %>' CommandName='Reissue' OnClick="lnkreissue_Click"
                                                ToolTip="Reissue Request">Reissue
                                               <%--   <img src="../Images/reissue.jpg" />--%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REFUND">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkrefund" runat="server" Font-Strikeout="False" Font-Overline="False"
                                                CommandArgument='<%#Eval("PaxId") %>' CommandName='Refund' OnClick="lnkrefund_Click"
                                                ToolTip="Refund Request">Refund
                                              <%--   <img src="../Images/refund.jpg" />--%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                              
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
