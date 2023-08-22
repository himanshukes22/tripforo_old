<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="AddBankDetails.aspx.vb" Inherits="SprReports_Distr_AddBankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />

    <style>
        label {
            color: #000;
        }

        table {
            /*width: 750px;*/
            border-collapse: collapse;
            /*margin:50px auto;*/
        }

        tr:nth-of-type(odd) {
            background: #eee;
        }

        th {
            background: #0952a4;
            color: white;
            /*font-weight: bold;*/
        }

        td, th {
            padding: 2px;
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
        <li><a href="#">Add Bank Details</a></li>
        
    </ol>


    <div class="card-main">


        <div class="inner-box">


           

               <div class="row">
                <div class="col-md-3">
                    <label>Bank Name</label>
                    <asp:TextBox ID="txt_bankname" placeholder="Enter Bank Name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Branch Name</label>
                    <asp:TextBox ID="txt_branchname" placeholder="Enter Branch Name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Area</label>
                    <asp:TextBox ID="txt_area" placeholder="Enter Area" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Account No.</label>
                    <asp:TextBox ID="txt_accno" placeholder="Enter Account No." CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                   </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <label>IFSC/NEFT Code</label>
                    <asp:TextBox ID="txt_code" placeholder="Enter IFSC/NEFT Code" CssClass="form-control" runat="server"></asp:TextBox>
                </div>


                           
                    <div class="btn-save col-md-3">
                     <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn cmn-btn" />
                    </div>
               
            </div>
            
      

        </div>
     
    </div>





   <div class="card-main">
     
        <div class="table-responsive">
            <asp:UpdatePanel ID="BlockAirlineUpdPanel" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GrdMarkup" runat="server" AutoGenerateColumns="False" CssClass="rtable" GridLines="None" Font-Size="12px" AllowPaging="true" PageSize="100" OnPageIndexChanging="GrdMarkup_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Bank Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankName" runat="server" Text='<%# Bind("BankName") %>' Width="83px" MaxLength="200"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBankName" runat="server" Text='<%#Eval("BankName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Bind("BranchName") %>' Width="83px" MaxLength="200"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtArea" runat="server" Text='<%# Bind("Area") %>' Width="83px" MaxLength="200"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblArea" runat="server" Text='<%#Eval("Area")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AccountNumber">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAccountNumber" runat="server" Text='<%# Bind("AccountNumber") %>' Width="83px" MaxLength="200"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountNumber" runat="server" Text='<%#Eval("AccountNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NEFTCode">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNEFTCode" runat="server" Text='<%# Bind("NEFTCode") %>' Width="83px" MaxLength="200"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNEFTCode" runat="server" Text='<%#Eval("NEFTCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" ReadOnly="true" />
                            <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="EDIT"
                                        ImageUrl="~/Images/edit_new.png" ToolTip="Edit"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="UPDATE"
                                        ImageUrl="../../Images/update_new.png" ToolTip="Update"></asp:ImageButton>
                                    <asp:ImageButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="CANCEL" ToolTip="Cancel"
                                        ForeColor="#20313f" Font-Strikeout="False" Font-Overline="False" Font-Bold="true" ImageUrl="../../Images/cancel_new.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DELETE">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lbkDelete" runat="server" CausesValidation="True" CommandName="Delete" Text="DELETE" ToolTip="Delete" ImageUrl="../../Images/delete_new.png" OnClientClick="return confirm('Are you sure to delete it?');"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
           
    </div>
</asp:Content>
