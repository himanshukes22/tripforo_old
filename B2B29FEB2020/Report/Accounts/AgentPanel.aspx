<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="AgentPanel.aspx.vb" Inherits="Reports_Accounts_AgentPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <style>

        .deposit__request-heading {
    background: #fff;
    padding: 10px;
    border-radius: 5px;
    font-size: 18px;
    box-shadow: 1px 1px 15px rgb(0 0 0 / 10%);
    cursor: pointer;
}
        .deposit__request-angleicon {
    float: right;
    background: #ccc;
    width: 22px;
    height: 22px;
    border-radius: 50%;
    text-align: center;
    line-height: 23px;
}
     
    </style>--%>

    <style type="text/css">
        .files .upload {
    outline: 2px dashed #92b0b3;
    outline-offset: -3px;
    -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
    transition: outline-offset .15s ease-in-out, background-color .15s linear;
    padding: 8px 0px 39px 17%;
    text-align: center !important;
    margin: 0;
    width: 100% !important;
}
.files .upload:focus{     outline: 2px dashed #92b0b3;  outline-offset: -10px;
    -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
    transition: outline-offset .15s ease-in-out, background-color .15s linear; border:1px solid #92b0b3;
 }
.files{ position:relative}
.files:after { 
pointer-events: none;
    position: absolute;
    top: 11px;
    left: -290px;
    width: 20px;
    right: 0;
    height: 29px;
    content: "";
    background-image: url(https://image.flaticon.com/icons/png/128/109/109612.png);
    display: block;
    margin: 0 auto;
    background-size: 100%;
    background-repeat: no-repeat;
}
.color .upload{ background-color:#f1f1f1;}
.files:before {
    position: absolute;
    bottom: 10px;
    left: -139px;
    pointer-events: none;
    width: 100%;
    right: 0px;
    height: 9px;
    content: " or drag it here. ";
    display: block;
    margin: 0 auto;
    color: #2ea591;
    font-weight: 600;
    text-transform: capitalize;
    text-align: center;
}
    </style>

        <link rel="stylesheet" type="text/css" href="../../CSS/jquery-ui-1.8.8.custom.css" />

    <%--link href="../../CSS/style.css" rel="stylesheet" type="text/css"/>
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    --%><link href="../../css/main2.css" rel="stylesheet" type="text/css" />
        


    


     
    <script type="text/javascript" language="javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
                return true;
            }
            else {

                return false;
            }
        }

        function isChar(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 8)) {
                return true;
            }
            else {

                return false;
            }
        }

    </script>

    <%--<script src="JS/JScript.js" type="text/javascript"></script>--%>

    <script type="text/javascript" language="javascript">
        function ValidateAgent() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_txt_amount").value == "") {
                alert('Please Fill Amount');
                document.getElementById("ctl00_ContentPlaceHolder1_txt_amount").focus();
                return false;
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "Select Payment Mode") {
                alert('Please Select Payment Mode');
                document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").focus();
                return false;
            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "Cash") {

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_city").value == "") {
                    alert('Please Fill Deposite City');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_city").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_depositedate").value == "") {
                    alert('Please Fill Deposite Date');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_depositedate").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_Office").value == "--Select Office--") {
                    alert('Please Select Office');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_Office").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_concernperson").value == "") {
                    alert('Please Fill Concern Person');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_concernperson").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {
                    alert('Please Fill Remark');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
                    return false;
                }


            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "Cash Deposite In Bank") {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").value == "--Select Bank--") {
                    alert('Please Select Bank Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_BranchCode").value == "") {
                    alert('Please Fill Branch Code');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_BranchCode").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {
                    alert('Please Fill Remark');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
                    return false;
                }

            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "Cheque") {

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_chequedate").value == "") {
                    alert('Please Fill Cheque Date');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_chequedate").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_chequeno").value == "") {
                    alert('Please Fill Cheque Number');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_chequeno").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_BankName").value == "") {
                    alert('Please Fill Bank Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_BankName").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").value == "--Select Bank--") {
                    alert('Please Select Bank Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {
                    alert('Please Fill Remark');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
                    return false;
                }

            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "NetBanking" || document.getElementById("ctl00_ContentPlaceHolder1_ddl_modepayment").value == "RTGS") {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").value == "--Select Bank--") {
                    alert('Please Select Bank Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_ddl_BankName").focus();
                    return false;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_tranid").value == "") {
                    alert('Please Fill Transaction ID');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_tranid").focus();
                    return false;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").value == "") {
                    alert('Please Fill Remark');
                    document.getElementById("ctl00_ContentPlaceHolder1_txt_remark").focus();
                    return false;
                }

            }

        }


    </script>
     
     
<script>
    $(document).ready(function () {
        $("#bkdet").click(function () {
            $(".bank").toggle();
        });
    });
</script>

        
    <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Wallet</a></li>
        <li><a href="#">Credit Upload Form</a></li>
        
    </ol>


       

         <h3 class="" style="color: #000;text-align: left;background: #fff;padding: 10px;border-radius: 5px;box-shadow: 1px 1px 15px rgb(0 0 0 / 10%);">Other Bank Details<i class="fa fa-angle-down" id="bkdet" style="float:right;"></i> </h3>
      

    <div class="card-main bank">
       
    <div class="inner-box credit-upload">
        <asp:GridView ID="GridView1" runat="server" CssClass="rtable"  AutoGenerateColumns="false" DataKeyNames="BankName"
 EmptyDataText="No records has been added." style="width: 100%;margin-bottom: 0;border-radius:5px;">
<Columns>
    <asp:TemplateField HeaderText="Bank" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblbankname" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtbankname" runat="server" Text='<%# Eval("BankName") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Branch" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblbranchname" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtbranchname" runat="server" Text='<%# Eval("BranchName") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>

        <asp:TemplateField HeaderText="Area" ItemStyle-Width="150" Visible="false">
        <ItemTemplate>
            <asp:Label ID="lblarea" runat="server" Text='<%# Eval("Area") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtarea" runat="server" Text='<%# Eval("Area") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>

        <asp:TemplateField HeaderText="Acc. No." ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblaccnum" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtaccnum" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>

        <asp:TemplateField HeaderText="IFSC" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblifsc" runat="server" Text='<%# Eval("NEFTCode") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtifsc" runat="server" Text='<%# Eval("NEFTCode") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>


<%--    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150"/>--%>
</Columns>
</asp:GridView>
                    </div>
        </div>
       
   

      <div class="card-main">
       
    <div class="inner-box credit-upload" style="padding:15px;">
        <div class="row">
            <div class="col-md-12" style="display:none">
               <table class="w70 auto boxshadow">
                     <tr >
            
                            <td align="left"  colspan="4">
                                Deposit Request Form
                            </td>
                        </tr>
                   
                            <tr>
                                <td class="fltdtls" colspan="4" id="td_UploadType" runat="server" visible="false">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" width="170" style="font-size:14px; font-weight:bold; position:relative;top:-4px;">
                                                Upload Request Type :
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RBL_UploadType" runat="server" RepeatDirection="Horizontal"
                                                    Width="300px">
                                                    <asp:ListItem Value="CA" Selected="True"> Fresh Upload</asp:ListItem>
                                                    <asp:ListItem Value="AD" > Adjustment</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                   </table>
                </div>
           
       
            <div class="col-md-4">
           <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-credit-card"></i>
                <asp:TextBox ID="txt_amount" runat="server" MaxLength="10" placeholder="Enter Amount" class="theme-search-area-section-input" onKeyPress="return isNumberKey(event)"></asp:TextBox>
            </div>
               </div>
          </div> 

            <div class="col-md-4 col-xs-6">
          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-tag"></i>
                <asp:DropDownList ID="ddl_modepayment" runat="server" placeholder="Select Payment Mode" AutoPostBack="True" class="theme-search-area-section-input">
                </asp:DropDownList>
          </div>
                </div>
                </div>

            <div id="check_info" runat="server" visible="false">
                <div class="col-md-4 col-xs-6">
             <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_chequedate" class="theme-search-area-section-input" placeholder="Select Cheque Date" runat="server"></asp:TextBox>
              </div>
                 </div>
                    </div>

                <div class="col-md-4 col-xs-6">
               <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-tag"></i>
                <asp:TextBox ID="txt_chequeno" class="theme-search-area-section-input" placeholder="Enter Cheque No." runat="server" ></asp:TextBox>
              </div>
                   </div>
                    </div>

             <div class="col-md-4 col-xs-6">
             <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-bank"></i>
                 <asp:TextBox ID="txt_BankName" placeholder="Enter Bank Name" runat="server" class="theme-search-area-section-input"   AutoPostBack="True"></asp:TextBox>
                   </div>
                 </div>
                   </div>

            </div>

            <div width="110px" align="left" id="td_Bank" visible="false" runat="server" style="display: none;" class="fltdtls">
            </div>

            <div class="col-md-4 col-xs-6" id="td_Bank1" visible="false" runat="server">
          <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-bank"></i>
                <asp:DropDownList ID="ddl_BankName" runat="server" class="theme-search-area-section-input" placeholder="Select Deposite Bank" AutoPostBack="True">
                </asp:DropDownList>
            </div>
              </div>
                </div>

            <div id="td_BranchAcc" runat="server" visible="false">

                <div class="col-md-4 col-xs-6">
                <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-bank"></i>
                    <asp:DropDownList ID="ddl_Banch" class="theme-search-area-section-input" placeholder="Select Deposite Branch" runat="server">
                    </asp:DropDownList>
                                </div>
                    </div>
               
                    </div>

                <div class="col-md-4 col-xs-6">
           <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-bank"></i>
                    <asp:DropDownList ID="ddl_Account" class="theme-search-area-section-input" placeholder="Enter A/C No" runat="server">
                    </asp:DropDownList>
                                </div>
               </div>
                </div>

            </div>


            <div id="div_Bankinfo" runat="server" visible="false">
                <div id="td_BACode" runat="server" visible="false" class="fltdtls hid">
                   
                </div>

                <div class="col-md-4 col-xs-6"  id="td_BACode1" runat="server" visible="false">
                <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-bank"></i>
                    <%--<asp:TextBox ID="txt_areacode" placeholder="Bank Area Code" runat="server" class="theme-search-area-section-inputaa"></asp:TextBox>--%>
                    <asp:TextBox ID="txt_areacode" placeholder="Enter Bank Area Code" runat="server" class="theme-search-area-section-input"></asp:TextBox>
                    </div>
                    </div>
                </div>
                <div class="fltdtls hid"  id="td_transid" visible="false" runat="server">
                    
                </div>

                 <div class="col-md-4 col-xs-6"   id="td_transid1" visible="false" runat="server">
                   <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_tranid" runat="server" placeholder="Enter Transaction ID" class="theme-search-area-section-input"></asp:TextBox>
                </div>
                       </div>
                     </div>

                <div class="fltdtls hid" width="100px"  id="td_BCode" visible="false" runat="server">
                   
                </div>

                <div class="col-md-4 col-xs-6" id="td_BCode1" visible="false" runat="server">
                <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_BranchCode" runat="server" placeholder="Enter Branch Code" class="theme-search-area-section-input"></asp:TextBox>
               </div>
                    </div>
                    </div>
            </div>

            <div id="tr_Deposite" runat="server" visible="false">
                <div class="col-md-4 col-xs-6">
                        <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_city" runat="server" class="theme-search-area-section-input" placeholder="Enter Deposit City" onKeyPress="return isChar(event)" ></asp:TextBox>
                </div>
                            </div>
                    </div>

                <div class="col-md-4 col-xs-6">
                  <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_depositedate" runat="server" class="theme-search-area-section-input" placeholder="Select Deposite Date"></asp:TextBox>
                </div>
                      </div>
                    </div>

                <div class="col-md-4 col-xs-6">
               <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:DropDownList ID="ddl_Office" runat="server" class="theme-search-area-section-input" placeholder="Deposite Office">
                       
                    </asp:DropDownList>
               </div>
                   </div>
                    </div>
                <%-- </div>--%>
            </div>
            <div id="tr_conper" runat="server" visible="false">
                <div class="col-md-4 col-xs-6">
                  <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_concernperson" runat="server" class="theme-search-area-section-input" placeholder="Enter Concern Person" onKeyPress="return isChar(event)"></asp:TextBox>
             </div>
                      </div>
                    </div>

                <div class="col-md-4 col-xs-6">
                 <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_ReceiptNo" runat="server" class="theme-search-area-section-input" placeholder="Receipt No"></asp:TextBox>
               </div>
                     </div>
                    </div>
            </div>
              
                <div class="col-md-4 col-xs-6">
                  <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="TxtRefNo" runat="server" class="theme-search-area-section-input" placeholder="Enter Reference No"  MaxLength="30" ></asp:TextBox>
                </div>
                      </div>
               </div>
            
               <div class="col-md-4 col-xs-6">
                  <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                              <i class="theme-search-area-section-icon lin lin-calendar"></i>
                    <asp:TextBox ID="txt_remark" runat="server" class="theme-search-area-section-input"   placeholder="Remark" ></asp:TextBox>
              </div>
                      </div>
                   </div>
                
             <div class="col-md-4 col-xs-6">
                <div class="form-group files">
                                <asp:FileUpload class="form-control upload" ID="fluPdfUpload" runat="server" accept="image/png,image/jpeg,application/pdf" style="margin-top: 16px;"/>
                    </div>
            
                   </div>

          
               <div class="col-md-4 col-xs-6">
<br />
                <div class="btn-save">

                    <asp:Button ID="btn_submitt" Text="Submit" runat="server" CssClass="btn cmn-btn" OnClientClick="return ValidateAgent();" />
                </div>

                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </div>       
           
            
              
            
        </div>


        </div>

       
          </div>




        <%--<script src="../../JS/calendar1.js"></script>
    <script type="text/javascript">

        if (document.getElementById('ctl00_ContentPlaceHolder1_ddl_modepayment').value == "Cheque") {
            var cal3 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_txt_chequedate']);
            cal3.year_scroll = true;
            cal3.time_comp = true;
        }
        if (document.getElementById('ctl00_ContentPlaceHolder1_ddl_modepayment').value == "Cash") {
            var cal4 = new calendar1(document.forms['aspnetForm'].elements['ctl00_ContentPlaceHolder1_txt_depositedate']);
            cal4.year_scroll = true;
            cal4.time_comp = true;
        }
    </script>--%>
    

    <script type="text/javascript" src="../../Scripts/jquery-1.4.4.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.8.custom.min.js"></script>

     <script type="text/javascript">


         $(function () {
             $("#ctl00_ContentPlaceHolder1_txt_chequedate").datepicker(
                  {
                      numberOfMonths: 1,

                      //showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                      //changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                      //selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                      autoSize: true, dateFormat: 'dd/mm/yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                      changeYear: false, hideIfNoPrevNext: false, maxDate: 0, minDate: '-1y', navigationAsDateFormat: true, defaultDate: +7, showAnim: 'toggle', showOtherMonths: true,
                      selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                  }
             ).datepicker("setDate", new Date().getDate - 1);

         });
         $(function () {
             $("#ctl00_ContentPlaceHolder1_txt_depositedate").datepicker(
                  {
                      numberOfMonths: 1,

                      //showButtonPanel: true, autoSize: true, dateFormat: 'dd-mm-yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: true,
                      //changeYear: true, hideIfNoPrevNext: true, navigationAsDateFormat: true, defaultDate: +7, showAnim: 'slide', showOtherMonths: true,
                      //selectOtherMonths: true, showOn: "button", buttonImage: '../../Images/cal.gif', buttonImageOnly: true
                      autoSize: true, dateFormat: 'dd/mm/yy', closeText: 'X', duration: 'slow', gotoCurrent: true, changeMonth: false,
                      changeYear: false, hideIfNoPrevNext: false, maxDate: 0, minDate: '-1y', navigationAsDateFormat: true, defaultDate: +7, showAnim: 'toggle', showOtherMonths: true,
                      selectOtherMonths: true, showoff: "button", buttonImageOnly: true
                  }
             ).datepicker("setDate", new Date().getDate - 1);

         });
    </script>



</asp:Content>
