<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="ReIssueReportDom.aspx.vb" Inherits="Reports_Reissue_ReIssueReportDom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <%--  <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />

    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>


    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
    <div class="large-3 medium-3 small-12 columns">
       <%--<uc1:LeftMenu runat="server" ID="LeftMenu" />--%>       
   </div>
    <div class="large-8 medium-8 small-12 columns">
        <div class="large-12 medium-12 small-12">
            <div class="large-12 medium-12 small-12 bld blue">Search Domestic Reissue
            </div>
         <div class="clear1"></div>
       
           
            <div class="large-2 medium-3 small-3 columns">From Date
            </div>
            <div class="large-3 medium-3 small-9  columns">

                <input type="text" name="From" id="From" class="txtCalander"
                    readonly="readonly" />

            </div>
            <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">To Date
            </div>
            <div class="large-3 medium-3 small-9  columns">

                <input type="text" name="To" id="To" class="txtCalander" readonly="readonly"/>
            </div>
            <div class="clear"></div>
            <div class="large-2 medium-3 small-3 columns">
                                        PNR
            </div>
            <div class="large-3 medium-3 small-9  columns">
                <asp:TextBox ID="txt_PNR" runat="server"></asp:TextBox>
            </div>
            <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">OrderId
            </div>
            <div class="large-3 medium-3 small-9  columns">
                <asp:TextBox ID="txt_OrderId" runat="server"></asp:TextBox>
            </div>
            <div class="clear"></div>
       
            <div class="large-2 medium-3 small-3 columns">Pax Name
            </div>
            <div class="large-3 medium-3 small-9  columns">
                <asp:TextBox ID="txt_PaxName" runat="server"></asp:TextBox>
            </div>
            <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">Ticket No
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
            <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">&nbsp;
            </div>
            <div class="large-3 medium-3 small-9  columns">&nbsp;
            </div>

            <div class="clear"></div>
       
            <div id="td_Agency" runat="server">
               

                            
                                <div id="tr_ExecID" runat="server">
                                    <div class="large-2 medium-3 small-3 columns">Exec ID
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:DropDownList ID="ddl_ExecID" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">Status
                                    </div>
                                    <div class="large-3 medium-3 small-9  columns">
                                        <asp:DropDownList ID="ddl_Status" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                </div>
                           
                <div class="clear"></div>
                 <div class="large-12 medium-12 small-12">

                       
                        <div class="large-2 medium-3 small-3 columns">Agency</div>
                        <div class="large-3 medium-3 small-9  columns">
                            <%--<asp:TextBox ID="txt_agencyid" runat="server" CssClass="textboxflight"></asp:TextBox>--%><input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                                onblur="blurObj(this);"
                                defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                        </div>

                     <div class="large-3 medium-3 small-3 large-push-1 medium-push-1 columns">&nbsp;
            </div>
            <div class="large-3 medium-3 small-9  columns">&nbsp;
            </div>
                   
            </div>

        <div class="clear"></div>

                <div class="large-6 medium-6 small-12 largr-push-6 medium-push-6">
            <div  class="large-6 medium-6 small-6 columns">
                <asp:Button ID="btn_result" runat="server" Text="Search Result" /></div>
                    <div  class="large-6 medium-6 small-6 columns"><asp:Button
                    ID="btn_export" runat="server" Text="Export" />
            </div>
                    </div>
       <div class="clear"></div>
                <div class="large-12 medium-12 small-12">
            <div style="color: #FF0000">* N.B: To get Today's booking without above parameter,do not fill any field, only click on search your booking.
            </div>
       </div>
    </div>



    
        <div class="clear1"></div>
        <div class="large-12 medium-12 small-12">
            
                <div>
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="grd_paxstatusinfo" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px" 
                                AllowPaging="True" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agency Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaxID" runat="server" Text='<%#Eval("PaxID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pax Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label>
                                            <asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Pax LastName">
                                        <ItemTemplate>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Pnr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpnr" runat="server" Text='<%#Eval("pnr_locator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticket Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltktno" runat="server" Text='<%#Eval("Tkt_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Airline">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVC" runat="server" Text='<%#Eval("VC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sector">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flight Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblflightno" runat="server" Text='<%#Eval("FlightNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("departure_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Fare">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("TotalFare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fare After Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reissue Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrissuechrge" runat="server" Text='<%#Eval("ReissueCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrvcharge" runat="server" Text='<%#Eval("ServiceCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fare Difference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfarediff" runat="server" Text='<%#Eval("FareDiff") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExecutiveId" runat="server" Text='<%#Eval("ExecutiveID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive Reject Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRejectionComment" runat="server" Text='<%#Eval("RejectionComment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsubmit" runat="server" Text='<%#Eval("SubmitDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accept Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldateA" runat="server" Text='<%#Eval("AcceptDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldateU" runat="server" Text='<%#Eval("UpdateDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                               
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
