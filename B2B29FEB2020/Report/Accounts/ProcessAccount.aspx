<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="ProcessAccount.aspx.vb" Inherits="Reports_Accounts_ProcessAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style>
        input[type="text"], input[type="password"], select, radio, legend, fieldset, textarea
        {
            border: 1px solid #004b91;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>--%>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />


    <script src="../../JS/lytebox1.js" type="text/javascript"></script>


    <link href="../../CSS/lytebox.css" rel="stylesheet" type="text/css" />

    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 5%"></td>
            <td style="width: 90%; padding-top: 20px; padding-bottom: 10px;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 30%"></td>
                        <td style="width: 40%">
                            <fieldset style="border: 1px solid #004b91; text-align: center; width: 100%; padding-top: 10px; padding-bottom: 15px; padding-left: 15px;">
                                <legend style="padding: 2px 5px 5px 2px; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; color: #004b91; width: 470px; text-align: center; background-color: #EAEAEA; vertical-align: middle;">Search Record By Type And Category</legend>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 15%"></td>
                                        <td style="width: 100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="padding: 10px 5px 10px 5px; font-weight: bold; font-family: arial, Helvetica, sans-serif; font-size: 13px;"
                                                        width="125px" align="left">Upload Type :
                                                    </td>
                                                    <td align="left">
                                                        <fieldset style="border: thin solid #004b91; width: 140px;">
                                                            <asp:RadioButtonList ID="RBL_Type" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                CellPadding="2" CellSpacing="2" Font-Size="12px" Font-Names="Arial" Width="140px">
                                                            </asp:RadioButtonList>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr id="tr_Cat" runat="server">
                                                    <td style="padding: 10px 5px 10px 5px; font-weight: bold; font-family: arial, Helvetica, sans-serif; font-size: 13px;"
                                                        align="left">Upload Category :
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddl_Category" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 30%">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 5%"></td>
        </tr>
    </table>
    <div class="divcls">
        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl" width="700px">

            <tr>

                <td class="bodytext" style="padding: 10px; background: #fff;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="190px" class="h2" style="padding-bottom: 3px; color: #004b91;" valign="bottom">Search By Agency Name&nbsp;&nbsp;
                                        </td>
                                        <td width="140px" style="padding-bottom: 3px" valign="bottom">
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Width="130px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td id="td_Reject" runat="server" visible="false" valign="top" align="right">
                                                        <fieldset style="padding: 5px 5px 5px 5px; border: 2px solid #004b91; width: 70%;">
                                                            <legend style="border: thin solid #004b91; width: 110px; font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Submit Comment&nbsp;&nbsp;</legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="center" style="padding-top: 10px">
                                                                        <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine" Height="60px" Width="350px"
                                                                            BackColor="#FFFFCC"></asp:TextBox><br />
                                                                        <br />
                                                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="buttonfltbks" />&nbsp;&nbsp;
                                                                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancle" CssClass="buttonfltbk" />
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
                            <td class="contdtls">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grd_accdeposit" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_accdeposit_RowCommand"
                                    OnRowDataBound="grd_accdeposit_RowDataBound" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <%--   <asp:BoundField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp;" DataField="Counter" />--%>
                                        <asp:TemplateField HeaderText="Agency Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_agencyid" runat="server" Text='<%# Eval("AgencyName").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="AgencyID" DataField="AgencyID" />--%>
                                        <asp:TemplateField HeaderText="Agency ID">
                                            <ItemTemplate>

                                                <div>
                                                    <%--<a href="UploadCredit.aspx?AgentID=<%#Eval("AgencyID")%>&ID=<%#Eval("Counter")%>&Amount=<%#Eval("Amount")%>#lightbox">
                                                                            <asp:Label ID="lbl_uid" runat="server" Text='<%#Eval("AgencyID")%>' Font-Bold="True"
                                                                                ForeColor="#004b91"></asp:Label></a>--%>
                                                    <a href='UploadCredit.aspx?AgentID=<%#Eval("AgencyID")%>&ID=<%#Eval("Counter")%>&Amount=<%#Eval("Amount")%>' rel="lyteframe"
                                                        rev="width: 900px; height: 300px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                                        <asp:Label ID="OrderID" runat="server" Text='<%#Eval("AgencyID")%>'></asp:Label></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                        <asp:BoundField HeaderText="ModeOfPayment" DataField="ModeOfPayment" />
                                        <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                                        <asp:BoundField HeaderText="ChequeNo" DataField="ChequeNo" />
                                        <asp:BoundField HeaderText="ChequeDate" DataField="ChequeDate" />
                                        <asp:BoundField HeaderText="TransactionID" DataField="TransactionID" />
                                        <asp:BoundField HeaderText="Bank AreaCode" DataField="BankAreaCode" />
                                        <asp:BoundField HeaderText="Deposit City" DataField="DepositCity" />
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
                                        <%--<asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>

        </table>
    </div>
</asp:Content>
