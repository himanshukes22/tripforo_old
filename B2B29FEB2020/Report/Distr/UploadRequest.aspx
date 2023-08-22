<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="UploadRequest.aspx.vb" Inherits="SprReports_Distr_UploadRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
  <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
   
    <div class="row">
        <div class="col-md-12 " style="padding: 20px;">
        <table cellspacing="10" cellpadding="0" border="0" align="center" width="100%">
            <tr>
                <td class="bodytext" style="padding: 10px; background: #fff;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" class="Heading" colspan="6" style="font-size: 20px; padding-bottom: 10px; padding-top: 10px;">
                                             Pending Deposite Details
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="190px" style="padding-bottom: 6px; color: #004b91; padding-left:24px;" 
                                            valign="bottom">
                                            Search By Agency Name&nbsp;&nbsp;
                                        </td>
                                        <td width="140px" style="padding-bottom: 6px" valign="bottom">
                                            <%--<asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Width="130px"></asp:TextBox>--%>
                                             <input type="text" id="txtAgencyName" name="txtAgencyName" 
                                                style="width: 130px" onfocus="focusObj(this);"
                                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" 
                                                value="Agency Name or ID" />&nbsp;<input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" /></td>
                                      <td style="padding-bottom: 6px; color: #004b91;" valign="bottom">
                                      <asp:Button 
                                                ID="btn_AgencySubmit" runat="server" Text="Submit" CssClass="buttonfltbks" />
                                      </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td id="td_Reject" runat="server" visible="false" valign="top" align="right">
                                                        <fieldset style="padding: 5px 5px 5px 5px; border: 2px solid #004b91; width: 70%;">
                                                            <legend style="border: thin solid #004b91; width: 80px; font-family: arial, Helvetica, sans-serif;
                                                                font-size: 12px; font-weight: bold; color: #004b91; text-align: center;">&nbsp; 
                                                                Remark&nbsp;&nbsp;</legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="center" style="padding-top: 10px">
                                                                        <asp:TextBox ID="txt_Reject" runat="server" class="form-control" TextMode="MultiLine" Height="60px" Width="350px"
                                                                            BackColor="#FFFFCC"></asp:TextBox><br />
                                                                        <br />
                                                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="buttonfltbks" />&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="buttonfltbks" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="contdtls">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top:30px;">
                                <asp:GridView ID="grd_accdeposit" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_accdeposit_RowCommand"
                                    OnRowDataBound="grd_accdeposit_RowDataBound"  CssClass="table text-center table-hover" GridLines="None" Font-Size="12px">
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
                                                <asp:Label ID="lbl_agencyid" runat="server" Text='<%#Eval("AgencyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="AgencyID" DataField="AgencyID" />--%>
                                        <asp:TemplateField HeaderText="Agency&nbsp;Id">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lbl_uid" runat="server" Text='<%#Eval("AgencyID")%>' Font-Bold="True"
                                                    ForeColor="#004b91"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                        <asp:BoundField HeaderText="ModeOfPayment" DataField="ModeOfPayment" />
                                        <asp:BoundField HeaderText="Bank&nbsp;Name" DataField="BankName" />
                                        <asp:BoundField HeaderText="Cheque&nbsp;No" DataField="ChequeNo" />
                                        <asp:BoundField HeaderText="Cheque&nbsp;Date" DataField="ChequeDate" />
                                        <asp:BoundField HeaderText="TransactionId" DataField="TransactionID" />
                                        <asp:BoundField HeaderText="Bank&nbsp;Area&nbsp;Code" DataField="BankAreaCode" />
                                        <asp:BoundField HeaderText="Deposit&nbsp;City" DataField="DepositCity" />
                                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                        <asp:TemplateField HeaderText="Accept||Reject">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkaccept" runat="server" Text="Accept" CommandName="accept"
                                                    CommandArgument='<%#Eval("Counter") %>' ForeColor="#004b91" Font-Bold="True">Accept</asp:LinkButton>&nbsp;&nbsp;||&nbsp;&nbsp;<asp:LinkButton ID="lnkcancel" runat="server" CommandName="reject" CommandArgument='<%#Eval("Counter") %>' ForeColor="Red" Font-Bold="True">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td id="Dop1" runat="server" visible="false">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
  </div>
        </div>
      <script type="text/javascript">
          var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>


