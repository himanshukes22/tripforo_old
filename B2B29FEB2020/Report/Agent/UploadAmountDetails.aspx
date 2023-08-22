<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="UploadAmountDetails.aspx.vb" Inherits="Reports_Agent_UploadAmountDetails"
    Title="Upload Amount Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

    
 



            
    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Wallet</a></li>
        <li><a href="#">Upload Amount Report</a></li>
        
    </ol>

    <div class="card-main">
 

        <div class="card-body ">
    <div class="row" >
         <div id="tr_SearchType" runat="server" visible="false" class="col-md-10">
                <div class="form-groups">
                    <asp:RadioButton ID="RB_Agent" runat="server" Checked="true" GroupName="Trip" onclick="Show(this)"
                        Text="Agent" />
                    &nbsp;&nbsp;   <asp:RadioButton ID="RB_Distr" runat="server" GroupName="Trip" onclick="Hide(this)"
                                                                                Text="Own" />
                </div>

    </div>
        </div>

        <div class="inner-box upload-amt">
            
        
            <div class="row">
            <div class="col-md-3 col-xs-12">
               
                 <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                <input type="text" name="From" id="From" placeholder="select Date" class="theme-search-area-section-input" readonly="readonly" />
                    </div>
                     </div>
             

            </div>
            <div class="col-md-3 col-xs-12">
                 <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                <input type="text" placeholder="Select Date" name="To" id="To" class="theme-search-area-section-input" readonly="readonly" />
                    </div>
                     </div>
           
            </div>
            <div class="col-md-3 col-xs-12">
                <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                <asp:DropDownList ID="ddl_PType" placeholder="Payment Mode" class="theme-search-area-section-input" runat="server">
                    <asp:ListItem Text="--Select Payment Mode--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                    <asp:ListItem Text="Cash Deposite In Bank" Value="Cash Deposite In Bank"></asp:ListItem>
                    <asp:ListItem Text="NetBanking" Value="NetBanking"></asp:ListItem>
                    <asp:ListItem Text="RTGS" Value="RTGS"></asp:ListItem>
                </asp:DropDownList>
                                </div>
                    </div>
            </div>

                    <div class="col-md-3 col-xs-12">
                         <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-tag"></i>
                <asp:DropDownList ID="ddl_status" class="theme-search-area-section-input" runat="server" placeholder="Status">
                    <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="InProcess" Value="InProcess"></asp:ListItem>
                    <asp:ListItem Text="Confirm" Value="Confirm"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                </asp:DropDownList>
                                </div>
                             </div>
            </div>
            
                     </div>
            <br />
            <div class="row">
                       <div class="btn-search col-md-3">
                             <asp:LinkButton ID="btn_showdetails" runat="server" CssClass="btn cmn-btn"><i class="fa fa-search" style="font-size: 10px;"></i>  Search</asp:LinkButton>
                    </div>
            </div>
               
          

            <div class="row">
            
            <div id="td_Agency" runat="server">               
                <div class="col-md-3 col-xs-12">
                    <asp:DropDownList ID="DropDownListADJ" class="form-control" placeholer="Upload Type" runat="server">
                        <asp:ListItem Text="--Select--" Value="Select" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Fresh Upload" Value="FU"></asp:ListItem>
                      
                    </asp:DropDownList>

                </div>
                 <div class="col-md-3 col-xs-12" id="td_ag">
                    <input type="text" id="txtAgencyName" class="form-control" name="txtAgencyName" onfocus="focusObj(this);"
                        onblur="blurObj(this);" defvalue="Agency Name or ID"
                        value="Agency Name or ID" />
                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                </div>
                 
                </div>
            </div>
           
        </div>

       
</div>
     
          </div>

      <div class="">
  

         <div class="table-responsive">
   
                <asp:GridView ID="grd_deposit" runat="server" AutoGenerateColumns="false" CssClass="rtable" GridLines="None" Font-Size="12px">
                    <Columns>
                        <asp:BoundField HeaderText="Transaction&nbsp;Date" DataField="Date" />
                        <asp:BoundField HeaderText="Agency&nbsp;Name" DataField="AgencyName" />
                        <asp:BoundField HeaderText="User_Id" DataField="AgencyID" />
                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                        <asp:BoundField HeaderText="ModeOfPayment" DataField="ModeOfPayment" />
                        <asp:BoundField HeaderText="Bank&nbsp;Name" DataField="BankName" />
                        <asp:BoundField HeaderText="ChequeNo" DataField="ChequeNo" />
                        <asp:BoundField HeaderText="Cheque&nbsp;Date" DataField="ChequeDate" />
                        <asp:BoundField HeaderText="TransactionId" DataField="TransactionID" />
                        <asp:BoundField HeaderText="BankAreaCode" DataField="BankAreaCode" />
                        <asp:BoundField HeaderText="Deposit&nbsp;City" DataField="DepositCity" />
                        <asp:BoundField HeaderText="Deposite&nbsp;Office" DataField="DepositeOffice" />
                        <asp:BoundField HeaderText="Concern&nbsp;Person" DataField="ConcernPerson" />
                        <asp:BoundField HeaderText="Reciept&nbsp;No" DataField="RecieptNo" />
                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <asp:BoundField HeaderText="Remark&nbsp;By&nbsp;Account" DataField="RemarkByAccounts" />
                        <asp:BoundField HeaderText="Account&nbsp;Id" DataField="AccountID" />
                        <%--<asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />--%>
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
    <script type="text/javascript" src="<%=ResolveUrl("~/JS/Distributor.js") %>"></script>

</asp:Content>
