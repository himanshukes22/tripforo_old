<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="ProcessImportPnrIntl.aspx.vb" Inherits="Reports_Import_ProcessImportPnrIntl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <div align="center">
        <table cellspacing="5" cellpadding="0" border="0" class="tbltbl" width="950px">
            <tr>
                <td align="center" height="50px" class="Heading" style="font-size: 25px; color: #004b91; font-family: arial, Helvetica, sans-serif; font-weight: bold;">International PNR Import processing detail
                </td>
            </tr>
            <tr>
                <td id="td_RejectRmk" runat="server" align="right" visible="false" colspan="2" style="padding: 10px; border: thin solid #999999;">
                    <asp:TextBox ID="txt_RejectRmk" runat="server"
                        TextMode="MultiLine" Height="60px"
                        Width="350px" BackColor="#FFFFCC" MaxLength="500"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Submit Remark" CssClass="button" />
                    <%--<asp:Button ID="btn_Comment" runat="server" Text="Submit Comment" OnClientClick="return ValidateReject()" />--%>&nbsp;&nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="Cancle" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td id="td_AgentRmk" runat="server" align="right" visible="false" colspan="2" style="padding: 10px; border: thin solid #999999;">
                    <asp:TextBox ID="txt_AgentRmk" runat="server"
                        TextMode="MultiLine" Height="60px"
                        Width="350px" BackColor="#FFFFCC" MaxLength="500"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="btn_AgentRmk" runat="server" Text="Submit Remark"
                        CssClass="button" />
                    <%--<asp:Button ID="btn_Comment" runat="server" Text="Submit Comment" OnClientClick="return ValidateReject()" />--%>&nbsp;&nbsp;
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancle" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridImportProxyDetail" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-hover" GridLines="None" Font-Size="12px" AllowPaging="True" PageSize="30">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a id="ancher" runat="server" rel="lyteframe" rev="width: 800px; height: 300px; overflow:hidden;"
                                        target="_blank" style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #800000; font-weight: bold;"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a id="ancherupdate" runat="server" style="font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #800000; font-weight: bold;"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OrderId">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_OrderId" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PNR">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PnrNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AgentID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_AgentID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ag_Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Ag_Name" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Depart" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departure Date">
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
                                    <asp:Label ID="lbl_SalesExecId" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="SubmitDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AcceptedDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_AcceptedDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ITZ_Reject" runat="server" CommandName="Reject" CommandArgument='<%#Eval("OrderId") %>'
                                        Font-Bold="True" Font-Underline="False">Reject</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent Remark">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ITZ_AgentRmk" runat="server" CommandName="AgentRemark" CommandArgument='<%#Eval("OrderId") %>'
                                        Font-Bold="True" Font-Underline="False"><img src="../../images/Rmk.jpg"  border="0" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
