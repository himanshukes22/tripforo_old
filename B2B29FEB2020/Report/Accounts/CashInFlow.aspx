<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="CashInFlow.aspx.vb" Inherits="Reports_Accounts_CashInFlow" %>

<%@ Register Src="~/UserControl/AccountsControl.ascx" TagPrefix="uc1" TagName="Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../css/main2.css" rel="stylesheet" type="text/css" />--%>

    <%--  <script src="../../JS/JScript.js" type="text/javascript"></script>

    <script src="../../JS/lytebox.js" type="text/javascript"></script>

    <link href="../../CSS/lytebox.css" rel="stylesheet" type="text/css" />--%>

    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <%-- <link href="../../css/basic.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />

    <%-- <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />--%>




    <%--<style type="text/css">
        .txtBox .txtCalander {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }

        .table-bordered > tbody > tr > th {
            text-align: center;
        }
    </style>--%>

    <script type='text/javascript'>
        function validate() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_TC").value == "") {
                alert('Please Fill TC');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_TC").focus();
                return false;

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_DBN").value == "") {
                alert('Please Fill DBN');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_DBN").focus();
                return false;

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Rmk").value == "") {
                alert('Please Fill Rmk');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Rmk").focus();
                return false;

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_Amt").value == "") {
                alert('Please Fill Amt');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_Amt").focus();
                return false;

            }


        }
    </script>


    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Wallet</a></li>
        <li><a href="#">Cash-In-Flow Report</a></li>

    </ol>


    <div class="card-main">
        <div class="card-body">
            <div class="row">
                <div class="col-md-9 col-md-push-1 col-xs-12">
                    <div class="form-groups" id="tr_SearchType" runat="server" visible="false">
                        <asp:RadioButton ID="RB_Agent" runat="server" Checked="true" GroupName="Trip" onclick="Show(this)"
                            Text="Agent" />
                        &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RB_Distr" runat="server" GroupName="Trip" onclick="Hide(this)"
                            Text="Own" />
                    </div>

                </div>
            </div>

            <div class="inner-box ">

                <div class="row">
                    <div class="col-md-3 col-xs-12">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="From" placeholder="Select Date" id="From" class="theme-search-area-section-input" readonly="readonly" />
                            </div>
                        </div>
                    </div>


                    <div class="col-md-3 col-xs-12">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-calendar"></i>
                                <input type="text" name="To" id="To" placeholder="Select Date" class="theme-search-area-section-input" readonly="readonly" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3 col-xs-12">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                        <asp:DropDownList ID="ddl_UploadType" placeholder="Upload Type" runat="server" class="theme-search-area-section-input">
                            <asp:ListItem Selected="True" Value="Select Type">--Select Type--</asp:ListItem>
                            <asp:ListItem Value="CA">Cash</asp:ListItem>
                            <asp:ListItem Value="CR">Credit</asp:ListItem>
                            <asp:ListItem Value="CC">Card</asp:ListItem>
                        </asp:DropDownList>
                                </div>
                            </div>
                    </div>


                    <div class="col-md-3 col-xs-12">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                                <asp:DropDownList ID="ddlInflowtype" runat="server" AutoPostBack="True" class="theme-search-area-section-input" placeholder="Cash Flow Type">
                                    <asp:ListItem Value="InFlow">--Select Flow Type--</asp:ListItem>
                                    <asp:ListItem Value="InFlow">InFlow</asp:ListItem>
                                    <asp:ListItem Value="OutFlow">OutFlow</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-md-3 col-xs-12" id="tr_UploadType" runat="server">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                                <asp:RadioButtonList ID="RBL_Type" runat="server" placeholder="Agency Type" AutoPostBack="True" RepeatDirection="Horizontal"
                                    Width="290px" CellPadding="4" CellSpacing="4">
                                    <asp:ListItem Value="CA" Selected="True">&nbsp;Cash</asp:ListItem>
                                    <asp:ListItem Value="CR">&nbsp;Credit</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-12" id="tr_UploadCategory" runat="server">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                <asp:DropDownList ID="ddl_Category" placeholder="Agency Category" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="btn-search col-md-3">

                        <asp:LinkButton ID="btn_search" runat="server" class="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                        <asp:LinkButton ID="btn_export" runat="server" CssClass="btn cmn-btn"><i class="fa fa-download" style="font-size: 10px;"></i>  Export</asp:LinkButton>

                    </div>

                </div>
                <%--    <div class="col-md-12">
            
                    
            
                        <div class="btn-export">
                <asp:Button ID="btn_Export" runat="server" Text="Export" OnClientClick="return Validation()" CssClass="btn cmn-btn " />
                            </div>
    </div>
                --%>
                <br />
                <br />

                <div class="row">
                    <div width="100%" id="td_ag">
                        <div class="form-groups col-md-3 col-xs-12" id="tr_AgencyName" runat="server">Agency Name</div>
                        <div class="form-groups col-md-3 col-xs-12" valign="top" id="tr_Agency" runat="server">
                            <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);" class="theme-search-area-section-input"
                                onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                            <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                        </div>
                    </div>
                    <asp:Label CssClass="pnrdtls" ID="lbl_amount" runat="server"></asp:Label>


                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>

                            <div class="col-md-12 col-xs-12">

                                <div id="td_SalesRmk" runat="server" visible="false" align="center">
                                    <div id="basic-modal-content3" style="padding: 10px; overflow: auto; width: 100%; font-size: 13px; border: thin solid #ccc;">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 text-center search-text">
                                                Update Deposite Details
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12">


                                            <div class="col-md-3 col-sm-12">
                                                TransactionID/Cheque No
                                            </div>
                                            <div class="col-md-3 col-sm-12">
                                                <asp:TextBox ID="txt_TC" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <iv class="col-md-3 col-sm-12">Deposite Bank Name
                                        </iv>
                                            <div class="col-md-3 col-sm-12">
                                                <asp:TextBox ID="txt_DBN" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12">
                                                Amount
                                            </div>
                                            <div class="col-md-3 col-sm-12">
                                                <asp:TextBox ID="txt_Amt" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>


                                            <div class="col-md-3 col-sm-12">Remark </div>

                                            <div class="col-md-3 col-sm-12">
                                                <asp:TextBox ID="txt_Rmk" runat="server" Height="50px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>

                                            <div class="col-md-1 col-sm-1">
                                                <asp:CheckBox ID="chk_status" runat="server" />
                                            </div>

                                            <div class="col-md-9 col-sm-11">
                                                Rest of the amount should be debited from portal balance
                                                        <br />
                                                &nbsp;<asp:Label ID="lbl_msg" runat="server" ForeColor="#FF3300" Font-Bold="True"></asp:Label>
                                            </div>
                                            <div class="col-md-2 col-sm-12">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Update" OnClientClick="return validate();"
                                                    CssClass="buttonfltbks" />
                                                &nbsp;<asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="buttonfltbk" />
                                            </div>

                                        </div>
                                    </div>
                                </div>



                                <div id="basic-modal-content" class="cashflow-table">
                                    <table width="100%" border="0" class="rtable" cellpadding="10" cellspacing="10">
                                        <tr>
                                            <td colspan="4" style="font-family: arial, Helvetica, sans-serif; font-size: 14px;" align="left">
                                                <p class="bld"><b>(Executive Updated Upload Details)</b></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="180px" style=""><b>TransactionID/ChequeNo :</b>
                                            </td>
                                            <td id="td_TC" runat="server" width="350px"></td>
                                            <td align="left" width="160px" style=""><b>Deposite Bank Name :</b>
                                            </td>
                                            <td id="td_DBN" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="color: #767676;"><b>Amount :</b>
                                            </td>
                                            <td id="td_Amt" runat="server"></td>
                                            <td align="left" style="color: #767676;"><b>Updated Date :</b>
                                            </td>
                                            <td id="td_UD" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style=""><b>Debit Portal Balance :</b>
                                            </td>
                                            <td id="td_DPB" runat="server" class="style1"></td>
                                            <td align="left" style="">Remark :
                                            </td>
                                            <td id="td_Rmk" runat="server" align="left" class="style1"></td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="">

        <div class="table-responsive">

            <asp:GridView ID="grd_CashInflow" runat="server" AutoGenerateColumns="false" DataKeyNames="AccID"
                CssClass="rtable" AllowPaging="True" PageSize="30" Style="text-transform: none">
                <Columns>
                    <%--<asp:TemplateField HeaderText="SL&nbsp;No.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_counter" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Invoice&nbsp;No.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Invoice" runat="server" Text='<%#Eval("InvoiceNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agent&nbsp;ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_agentid" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agency&nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                        <ItemTemplate>
                            <asp:Label ID="lbl_agencyname" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lbl_amt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload&nbsp;Type">
                        <ItemTemplate>
                            <asp:Label ID="lbl_type" runat="server" Text='<%#Eval("UploadType") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_updatetype" runat="server" Text='<%#Eval("UploadType") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                        <ItemTemplate>
                            <asp:Label ID="lbl_remark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="YtrRcptNo">
                        <ItemTemplate>
                            <asp:Label ID="lbl_YtrRcptNo" runat="server" Text='<%#Eval("YtrRcptNo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_YtrRcptNo" runat="server" Text='<%#Eval("YtrRcptNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Updated&nbsp;Remark">
                        <ItemTemplate>
                            <asp:Label ID="lbl_updatedremark" runat="server" Text='<%#Eval("UpdatedRemark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_updateremark" runat="server" Text='<%#Eval("UpdatedRemark") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Cdate" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Updated&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                        <ItemTemplate>
                            <asp:Label ID="lbl_updateddate" runat="server" Text='<%#Eval("UpdatedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View&nbsp;Adjustment">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_adjdtl" runat="server">
                                                    <a href='UploadAdjustment.aspx?Counter=<%#Eval("Counter")%>&Type=View'
                                                        rel="lyteframe" rev="width: 900px; height: 300px; overflow:hidden;" target="_blank"
                                                        style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                        color: #161946"> Details
                                                        </a></asp:LinkButton>
                            <%--   <asp:Button ID="btn_edit" runat="server" Text="Edit" Font-Bold="true" CommandName="Edit" />--%>
                        </ItemTemplate>
                        <%--<EditItemTemplate>
                                                        <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="Update" Font-Bold="true" />
                                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CommandName="Cancel" Font-Bold="true" />
                                                    </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit/Update">
                        <ItemTemplate>
                            <asp:Button ID="btn_edit" runat="server" Text="Edit" Font-Bold="true" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="Update" Font-Bold="true" />
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CommandName="Cancel" Font-Bold="true" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Adjustment">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_adj" runat="server">
                                                    <a href='UploadAdjustment.aspx?Counter=<%#Eval("Counter")%>&Type=Insert'
                                                        rel="lyteframe" rev="width: 900px; height: 300px; overflow:hidden;" target="_blank"
                                                        style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                        color: #161946"> AdjustAmount
                                                        </a></asp:LinkButton>
                            <%--   <asp:Button ID="btn_edit" runat="server" Text="Edit" Font-Bold="true" CommandName="Edit" />--%>
                        </ItemTemplate>
                        <%--<EditItemTemplate>
                                                        <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="Update" Font-Bold="true" />
                                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CommandName="Cancel" Font-Bold="true" />
                                                    </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Agent_Type" runat="server" Text='<%#Eval("Agent_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SalesExecID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SalesExecID" runat="server" Text='<%#Eval("SalesExecID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit Details" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_EditPayment" runat="server" CommandName="EditDetail" CommandArgument='<%#Eval("Counter") %>'
                                Font-Bold="True" ForeColor="#161946" Font-Size="10px">Update&nbsp;Payment&nbsp;Details</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_View" runat="server" Visible="false" CommandName="View" CommandArgument='<%#Eval("Counter") %>'
                                Font-Bold="True" ForeColor="#161946" Font-Size="10px">View&nbsp;Payment&nbsp;Details</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SalesUpDate" runat="server" Text='<%#Eval("UpdatedDateSales") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>
    </div>

    <%-- <div class="large-2 medium-2 small-12 columns">
            <uc1:Account runat="server" ID="Settings" />
        </div>--%>
    <br />


    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
    <script src="../../Utility/JS/jquery.simplemodal.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/JS/Distributor.js") %>"></script>
    <script type='text/javascript'>
        function openDialog() {
            $(function () {
                //                alert('hi');
                $('#basic-modal-content').modal();
                return false;
            });
        }
    </script>
</asp:Content>
