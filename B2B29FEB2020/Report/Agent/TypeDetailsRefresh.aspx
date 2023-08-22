<%--<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgentTypeDetails.aspx.vb" Inherits="SprReports_Agent_AgentTypeDetails" %>--%>

<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="TypeDetailsRefresh.aspx.vb" Inherits="SprReports_Agent_AgentTypeDetails" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


      <script language="javascript" type="text/javascript">
          function validateSearch() {

              if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_ptype").value == "ALL") {
                  alert('Specify Group Type');
                  document.getElementById("ctl00_ContentPlaceHolder1_ddl_ptype").focus();
                  return false;
              }

          }

    </script>




    <div class="mtop80"></div>
    <div class="large-3 medium-3 small-12 columns">
       
               <%-- <uc1:Settings runat="server" ID="Settings" />--%>
            
    </div>

   <div class="large-9 medium-9 small-12 columns">
       
        <div class="large-12 medium-12 small-12 heading">
             <div class="large-12 medium-12 small-12 heading1">Group Type Details
            </div>
            <div class="clear1"></div>
        <asp:Label ID="LableMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="large-12 medium-12 small-12">
           <div class="large-6 medium-6 small-12 columns">
                <div class="large-2 medium-3 small-3 columns">Group Type</div>
                <div class="large-6 medium-6 small-9  columns">
                      <asp:DropDownList ID="ddl_ptype" CssClass="form-control" runat="server"  AppendDataBoundItems="true">
                      </asp:DropDownList>
                    <asp:TextBox ID="TextBoxGroupType" Style="display:none" runat="server"></asp:TextBox></div>
               <div class="clear"></div>
                <div class="large-12 medium-12 small-12  columns">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ErrorMessage="Group Type is Required." ControlToValidate="TextBoxGroupType" ValidationGroup="ins"></asp:RequiredFieldValidator> </div>
                </div>
                  <div class="clear"></div>
           
                <div class="large-2 medium-3 small-6 large-push-10 medium-push-9 small-push6">
                    <asp:Button runat="server" CssClass="buttonfltbk" ID="ButtonSubmit" OnClientClick="return validateSearch()" Text="Insert" style="WIDTH: 121PX;FLOAT: right;"/>
                </div>
            <br />
                <div style="color:red;font-family:'Times New Roman', Times, serif;font-weight:bolder"> Only,New Records are Insert and Nothing Do in Exists Record</div>

        </div>

            <div class="clear1"></div>
</div>
       <div class="clear1"></div>
       

    </div>
</asp:Content>
