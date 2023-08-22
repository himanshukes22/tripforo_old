<%--<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgentTypeDetails.aspx.vb" Inherits="SprReports_Agent_AgentTypeDetails" %>--%>

<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="AgentTypeDetails.aspx.vb" Inherits="SprReports_Agent_AgentTypeDetails" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<style>
        /*label {
            color: orange;
        }*/

        table {
            /*width: 750px;*/
            border-collapse: collapse;
            /*margin:50px auto;*/
        }

        tr:nth-of-type(odd) {
            background: #eee;
        }

        th {
            background: #2e323e;            
            font-size:14px;
            color: white;
            /*font-weight: bold;*/
        }

        td, th {
            padding: 5px;
            border: 1px solid #ccc;
            text-align: left;
            /*font-size: 18px;*/
        }


        @media only screen and (max-width: 760px), (min-device-width: 768px) and (max-device-width: 1024px) {

            table {
                width: 100%;
            }


            table, thead, tbody, th, td, tr {
                display: block;
                padding: inherit;
            }


                thead tr {
                    position: absolute;
                    top: -9999px;
                    left: -9999px;
                }

            tr {
                border: 1px solid #ccc;
            }

            td {
                border: none;
                border-bottom: 1px solid #eee;
                position: relative;
                padding-left: 50%;
            }

                td:before {
                    position: absolute;
                    top: 6px;
                    left: 6px;
                    width: 45%;
                    padding-right: 10px;
                    white-space: nowrap;
                    content: attr(data-column);
                    color: #000;
                    font-weight: bold;
                }
        }
    </style>


        <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Upload</a></li>
        <li><a href="#">Group Type Details</a></li>
        
    </ol>

       
        <div class="card-main">
        
        
        <asp:Label ID="LableMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="inner-box">
           <div class="row">
               
                <div class="col-md-3">
                   
                <asp:TextBox runat="server" ID="TextBoxGroupType" placeholder="Enter Agent Type"  MaxLength="20" onkeypress="return keyRestrict(event,'abcdefghijklmnopqrstuvwxyz0123456789');"></asp:TextBox>
               </div>
              
                <div class="large-12 medium-12 small-12  columns" style="display:none;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ErrorMessage="Group Type is Required." ControlToValidate="TextBoxGroupType" ValidationGroup="ins"></asp:RequiredFieldValidator> 

                </div>
               
             
                
                <div class="col-md-3">
                   
                    <asp:TextBox ID="TextBoxDesc" runat="server" placeholder="Description" CssClass="form-control"></asp:TextBox>
                </div>
          
                <div class="large-12 medium-12 small-12 columns" style="display:none;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesc" runat="server" ErrorMessage="Description is Required."  ControlToValidate="TextBoxDesc"  ValidationGroup="ins"></asp:RequiredFieldValidator>

                </div>
               
               <div class="btn-add col-md-3">
                        <asp:Button runat="server" CssClass="btn cmn-btn" ID="ButtonSubmit" Text="Add Group Type"  ValidationGroup="ins" />
                    </div>
           
               
             
               
                

        </div>
      </div>

             <%--  <div class="">
                  <div style="color:red;font-family:'Times New Roman', Times, serif;font-weight:bolder;margin-left: 10px;">Note:-When Will you Create New Group that time Your Dealsheets copy as same in New Created Group</div>
                </div>--%>

</div>


        <div class="">
        
                   <div class="table-responsive" >
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" 
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
             CssClass="rtable" GridLines="None" Font-Size="12px" style="width: 100%;">
            <Columns>
                <asp:CommandField  ShowEditButton="True" />
                <asp:TemplateField HeaderText="Group Type">

                    <ItemTemplate>
                        <asp:Label ID="LableGroupType" runat="server" Text='<%# Eval("GroupType")%>'></asp:Label>
                    </ItemTemplate>
                                       
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="LableDesc" runat="server" Text='<%# Eval("Text")%>'></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDesc" runat="server" Text='<%# Eval("Text")%>'></asp:TextBox>
                    </EditItemTemplate>
                      
                   
                </asp:TemplateField>

               <asp:CommandField ShowDeleteButton="True" />
               
            </Columns>
            
        </asp:GridView>
               </div>
               </div>

    
     <%--   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" 
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
             CssClass="" GridLines="None" Font-Size="12px">
            <Columns>
                <asp:CommandField  ShowEditButton="True" />
                <asp:TemplateField HeaderText="Group Type">

                    <ItemTemplate>
                        <asp:Label ID="LableGroupType" runat="server" Text='<%# Eval("GroupType")%>'></asp:Label>
                    </ItemTemplate>
                                       
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="LableDesc" runat="server" Text='<%# Eval("Text")%>'></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDesc" runat="server" Text='<%# Eval("Text")%>'></asp:TextBox>
                    </EditItemTemplate>
                      
                   
                </asp:TemplateField>

               <asp:CommandField ShowDeleteButton="True" />
               
            </Columns>
            
        </asp:GridView>--%>
   


         <script type="text/javascript">
             $(document).ready(function () {
                 $("#ctl00_ContentPlaceHolder1_TextBoxGroupType").click(function () {

                     $("#ctl00_ContentPlaceHolder1_LableMsg").hide();


                 });
                 $("#ctl00_ContentPlaceHolder1_TextBoxDesc").click(function () {

                     $("#ctl00_ContentPlaceHolder1_LableMsg").hide();
                 });
             });


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

    </script>
</asp:Content>
