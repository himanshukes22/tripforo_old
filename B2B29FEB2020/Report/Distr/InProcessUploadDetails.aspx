<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="InProcessUploadDetails.aspx.vb" Inherits="SprReports_Distr_InProcessUploadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

      <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"

    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    --%>
   <div class="row">
      
        <div class="col-md-9 col-xs-12 col-md-push-1" style="margin-top:10px;">
             <div class="clear1"></div>
        <div class="w90 auto">
                        <div class="w100">
                                  
                                        <div class="col-md-3 col-xs-12" style="color: #004b91;">Search By Agency Name
                                        </div>
                                        <div class="col-md-3 col-xs-12">
                                            <input type="text" id="txtAgencyName" name="txtAgencyName"
                                                style="width: 130px" onfocus="focusObj(this);" class="form-control"
                                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off"
                                                value="Agency Name or ID" />&nbsp;<input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                        </div>
                                        <div class="col-md-3 col-xs-12 pull-right">
                                            <asp:Button ID="btn_searchag" runat="server" Text="Search" CssClass="buttonfltbks" />
                                        </div>
                            <div class="clear1"></div>
                                        <div class="w100">
                                            
                                                
                                                    <div id="td_Reject" runat="server" visible="false" valign="top" align="right">
                                                        <fieldset style="padding: 5px 5px 5px 5px; border: 2px solid #004b91; width: 70%;">
                                                            <legend style="border: thin solid #004b91; width: 110px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Submit Comment&nbsp;&nbsp;</legend>
                                                            <div class="w100">
                                                               
                                                                    <div align="center" style="padding-top: 10px">
                                                                        <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine" Height="60px" Width="350px"
                                                                            BackColor="#FFFFCC"></asp:TextBox><br />
                                                                        <br />
                                                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="buttonfltbks" />&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancle" CssClass="buttonfltbks" />
                                                                    </div>
                                                                
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                               
                                            
                                        </div>
                                   
                                </div>
                            <div class="contdtls">&nbsp;
                            </div>
                        
                      
                    </div>
    </div>
       </div>
    
      <div class="row">
        <div class="col-md-10" style="padding: 50px; width:100%">
            <asp:GridView ID="grd_accdeposit" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_accdeposit_RowCommand"
                                    OnRowDataBound="grd_accdeposit_RowDataBound" CssClass="table table-bordered text-center table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <%--   <asp:BoundField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp;" DataField="Counter" />--%>
                                        <asp:TemplateField HeaderText="Agency&nbsp;Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_agencyid" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="AgencyID" DataField="AgencyID" />--%>
                                        <asp:TemplateField HeaderText="Agency&nbsp;Id">
                                            <ItemTemplate>

                                                <div>
                                                    <a href="UploadAmount.aspx?AgentID=<%#Eval("AgencyID")%>&ID=<%#Eval("Counter")%>&Amount=<%#Eval("Amount")%>" rel="lyteframe"
                                                        rev="width: 800px; height: 280px; overflow:hidden;" target="_blank">
                                                        <asp:Label ID="lbl_uid" runat="server" Text='<%#Eval("AgencyID")%>' Font-Bold="True"
                                                            ForeColor="#004b91"></asp:Label></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                        <asp:BoundField HeaderText="ModeOfPayment" DataField="ModeOfPayment" />
                                        <asp:BoundField HeaderText="Bank&nbsp;Name" DataField="BankName" />
                                        <asp:BoundField HeaderText="ChequeNo" DataField="ChequeNo" />
                                        <asp:BoundField HeaderText="Cheque&nbsp;Date" DataField="ChequeDate" />
                                        <asp:BoundField HeaderText="TransactionId" DataField="TransactionID" />
                                        <asp:BoundField HeaderText="Bank&nbsp;Area&nbsp;Code" DataField="BankAreaCode" />
                                        <asp:BoundField HeaderText="Deposit&nbsp;City" DataField="DepositCity" />
                                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                        <asp:TemplateField HeaderText="Reject">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkaccept" runat="server" Text="Accept" CommandName="accept"
                                                    CommandArgument='<%#Eval("AgencyID") %>'></asp:LinkButton>/--%>
                                                <asp:LinkButton ID="lnkcancel" runat="server" CommandName="reject" CommandArgument='<%#Eval("Counter") %>'
                                                    ForeColor="Red" Font-Bold="True">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>

        </div>
    </div>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <style type="text/css">
        #lbMain
{
            top:130px !important;
        }    </style>

</asp:Content>

