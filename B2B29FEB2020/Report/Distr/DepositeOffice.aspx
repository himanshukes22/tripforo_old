<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="DepositeOffice.aspx.vb" Inherits="SprReports_Distr_DepositeOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>

     <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%=ResolveUrl("~/CSS/newcss/main.css")%>"
        rel="stylesheet" />
    


    <div class="row">
        <div class="col-md-12 text-center search-text  ">
            Add Deposite Office Name
        </div>
    </div>

        <div class="row">
         <div class="col-md-11 col-md-push-1 col-xs-12">
            <div class="form-inlines">
                <div class="form-groups col-md-3 col-xs-12">                   
                      <asp:TextBox ID="txt_branchname" runat="server" CssClass="form-control" placeholder="Office Branch Name" MaxLength="150"></asp:TextBox>
                </div>  
                <div class="form-groups col-md-3 col-xs-12">
                     <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="buttonfltbks" />
                </div>              
                </div>
             </div>
            </div>
    <div class="row">
         <div class="col-md-12 col-xs-12">
    <table style="margin: 0 auto; width: 100%;">
        
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="BlockAirlineUpdPanel" runat="server">
                 <ContentTemplate>
             <asp:GridView ID="GrdMarkup" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px"    AllowPaging="true" PageSize="100" onpageindexchanging="GrdMarkup_PageIndexChanging">
            <Columns>                
                <asp:TemplateField HeaderText="User_ID">                   
                    <ItemTemplate>
                        <asp:Label ID="lblDISTRID" runat="server" Text='<%#Eval("DISTRID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deposite_Office_Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Bind("OFFICE")%>'  Width="83px"  MaxLength="200"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("OFFICE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="CRETAEDDATE" HeaderText="CreatedDate"  ReadOnly="true"  />
                 <asp:TemplateField HeaderText="EDIT">
                 <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"  Text="EDIT"
                            ImageUrl="~/Images/edit_new.png" ToolTip="Edit"></asp:ImageButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="UPDATE"
                            ImageUrl="~/Images/update_new.png" ToolTip="Update">
                         </asp:ImageButton>
                        <asp:ImageButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="CANCEL" ToolTip="Cancel" ForeColor="#20313f"  ImageUrl="~/Images/cancel_new.png" Font-Strikeout="False" Font-Overline="False" Font-Bold="true"></asp:ImageButton>
                    </EditItemTemplate>
                </asp:TemplateField>                         
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DELETE">
                 <ItemTemplate>
                        <asp:ImageButton ID="lbkDelete" runat="server" CausesValidation="True" CommandName="Delete" Text="DELETE" ToolTip="Delete" ImageUrl="../../Images/delete_new.png"  OnClientClick="if(!confirm('Do you want to delete?')){ return false; };"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                 
        </asp:GridView>                                    
         </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
         </div></div>
</asp:Content>
