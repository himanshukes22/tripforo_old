<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="AcceptPnrImport.aspx.vb" Inherits="Reports_Import_AcceptPnrImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <div align="center">
        <table cellspacing="10" cellpadding="0" border="0"  class="tbltbl"
            width="950px">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center" height="50px" style="font-size: 25px; color: #004b91; font-family: arial, Helvetica, sans-serif;
                                font-weight: bold;">
                                Domestic PNR Import request detail
                            </td>
                        </tr>
                        <tr id="td_RejectRmk" runat="server" visible="false">
                            <td align="right" colspan="2" style="padding: 10px; border: thin solid #999999;">
                                 <asp:TextBox  ID="txt_RejectRmk" runat="server" TextMode="MultiLine" Height="60px"
                                    Width="350px" BackColor="#FFFFCC" MaxLength="500"></asp:TextBox><br />
                                <br />
                                <asp:Button ID="btn_AgentRmk" runat="server" Text="Submit Remark" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancle" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridImportProxyDetail" runat="server" AutoGenerateColumns="False"
                                     CssClass="table table-hover" GridLines="None" Font-Size="12px" AllowPaging="True" PageSize="30" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a id="ancher" runat="server" rel="lyteframe" rev="width: 500px; height: 300px; overflow:hidden;"
                                                    target="_blank" style="font-family: arial, Helvetica, sans-serif; font-size: 12px;
                                                    color: #800000; font-weight: bold;"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PNR">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PnrNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OrderId">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderId" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AgentID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AgentID" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agency Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Ag_Name" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Depart" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DepartureDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Status" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Agent&nbsp;Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_agenttype" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                           <asp:TemplateField HeaderText="Sales&nbsp;Executive">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SalesExecId" runat="server" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Submit Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ITZ_Accept" runat="server" CommandName="Accept" Font-Bold="True"
                                                    Font-Underline="False" OnClick="ITZ_Accept_Click">Accept</asp:LinkButton>
                                                ||
                                                <asp:LinkButton ID="ITZ_Reject" runat="server" CommandName="Reject" Font-Bold="True"
                                                    Font-Underline="False" OnClick="ITZ_Reject_Click">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
