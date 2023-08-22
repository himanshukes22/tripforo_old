<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="PGCharges.aspx.cs" Inherits="SprReports_Accounts_PGCharges" %>
<%@ Register Src="~/UserControl/AccountsControl.ascx" TagPrefix="uc1" TagName="Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />--%>
    <div">
         <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
          <div class="large-3 medium-3 small-12 columns">
                <uc1:Account runat="server" id="Settings" />
            </div>
       
        <div class="large-8 medium-8 small-12 columns end" style="padding-top:20px;">
            <div class="page-wrapperss">

                <div class="panel panel-primary">
                    <div class="large-12 medium-12 small-12 heading" style="margin-left :20px;">
                        <h3 class="large-12 medium-12 small-12 heading1">Payment Gateway Transaction Charges</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width:45%; margin-left :20px;">
                            <%--<div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">From Date</label>
                                    <input type="text" name="From" id="From" class="form-control" readonly="readonly" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">To Date</label>
                                    <input type="text" name="To" id="To" class="form-control" readonly="readonly" />
                                </div>
                            </div>--%>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Payment Mode :</label>
                                    <asp:DropDownList ID="ddlCardType" runat="server" AutoPostBack="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>                               
                            </div>
                            <div class="col-md-3">
                                 <div class="form-group">
                                    <label for="exampleInputEmail1">Charge Type :</label>
                                    <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="P" Selected="true">Percentage</asp:ListItem>
                                        <asp:ListItem Value="F">Fixed</asp:ListItem>                                        
                                    </asp:DropDownList>
                                </div>
                                </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Transaction Charges</label>
                                    <asp:TextBox ID="txtPgCharges" runat="server" CssClass="form-control" onkeypress="return keyRestrict(event,'.0123456789');" MaxLength="7"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <%--<asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="button buttonBlue"  />--%>
                                    <asp:Button ID="btnUpdate"  Width="150px" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="button buttonBlue" OnClientClick="return ValidateDetails();" />
                                </div>
                            </div>
                        </div>

                        <div style="width:90%; margin-left :20px;">

                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UP" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                            <Columns>                                             
                                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ControlStyle-CssClass="textboxflight1" /> 
                                                <asp:BoundField DataField="CType" HeaderText="Charge Type" ControlStyle-CssClass="textboxflight1" />
                                                <asp:BoundField DataField="Charges" HeaderText="Charges" ControlStyle-CssClass="textboxflight1" />                                                
                                                <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" ControlStyle-CssClass="textboxflight1" /> 
                                                <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" ControlStyle-CssClass="textboxflight1" />                                                 
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
            </div>
        </div>
        </div>
        </div>
      <script type="text/javascript">
          var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>    
     <script type="text/javascript">
         function getKeyCode(e) {
             if (window.event)
                 return window.event.keyCode;
             else if (e)
                 return e.which;
             else
                 return null;
         }
         function keyRestrict(e, validchars) {
             var key = '', keychar = '';
             key = getKeyCode(e);
             if (key == null) return true;
             keychar = String.fromCharCode(key);
             keychar = keychar.toLowerCase();
             validchars = validchars.toLowerCase();
             if (validchars.indexOf(keychar) != -1)
                 return true;
             if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
                 return true;
             return false;
         }
         function ValidateDetails() {
             if ($("#ctl00_ContentPlaceHolder1_ddlCardType").val() == "0") {
                 alert("Plesase select payment mode");
                 $("#ctl00_ContentPlaceHolder1_ddlCardType").focus();
                 return false;
             }


             if ($("#ctl00_ContentPlaceHolder1_ddlChargeType").val() == "0") {
                 alert("Please select  charge type");
                 $("#ctl00_ContentPlaceHolder1_ddlChargeType").focus();
                 return false;
             }

             if ($("#ctl00_ContentPlaceHolder1_txtPgCharges").val() == "") {
                 alert("Please enter transaction charges");
                 $("#ctl00_ContentPlaceHolder1_txtPgCharges").focus();
                 return false;
             }

             var bConfirm = confirm("Are you sure want to update transaction charge ?");
             if (bConfirm) {
                 return true;
             }
             else
                 return false;
         }
    </script>
</asp:Content>

 