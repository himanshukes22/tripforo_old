<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="ProxyTicketDetail.aspx.vb" Inherits="Reports_Proxy_ProxyTicketDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
   <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
             <div align="center">
        <table cellspacing="10" cellpadding="0" border="0"  class="tbltbl"
            width="950px">
                                <tr>
                                    <td align="center" class="Heading" height="20px" 
                                        style="font-size: 24px; color: #004b91; font-weight: bold; font-family: arial, Helvetica, sans-serif;">
                                        Proxy Ticket Detail
                                    </td>
                                </tr>
                                <tr>
                                    <td id="td_Reject" runat="server" visible="false" colspan="2" style="padding: 10px;">
                                        <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                            <legend style="border: thin solid #004b91; width: 110px; font-family: arial, Helvetica, sans-serif;
                                                font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Submit Comment</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                         <asp:TextBox  ID="txt_Reject" runat="server" 
                                                             TextMode="MultiLine" Height="60px" Width="350px"
                                                            BackColor="#FFFFCC" MaxLength="500"></asp:TextBox><br />
                                                        <br />
                                                        <asp:Button ID="btn_Comment" runat="server" Text="Submit" CssClass="button" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancle" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="td_AgentRmk" runat="server" visible="false" colspan="2" style="padding: 10px;">
                                        <fieldset style="padding: 10px; border: 2px solid #004b91;">
                                            <legend style="border: thin solid #004b91; width: 150px; font-family: arial, Helvetica, sans-serif;
                                                font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Submit Agent Comment</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                         <asp:TextBox  ID="txt_AgentRmk" runat="server" 
                                                             TextMode="MultiLine" Height="60px"
                                                            Width="350px" BackColor="#FFFFCC" MaxLength="500"></asp:TextBox><br />
                                                        <br />
                                                        <asp:Button ID="btn_AgentRmk" runat="server" Text="Submit" CssClass="button" />
                                                        <%--<asp:Button ID="btn_Comment" runat="server" Text="Submit Comment" OnClientClick="return ValidateReject()" />--%>&nbsp;&nbsp;
                                                        <asp:Button ID="Button2" runat="server" Text="Cancle" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:GridView ID="GridProxyDetail" runat="server">
        
        
        </asp:GridView>--%>
                                        <asp:GridView ID="GridProxyDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="ProxyID"
                                            OnPageIndexChanging="GridProxyDetail_Changing" CssClass="GridViewStyle" AllowPaging="True"
                                            PageSize="30" OnRowDataBound="GridRowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ProxyID">
                                                    <ItemTemplate>
                                                        <a id="ancher" href='PassengerDetail.aspx?ProxyID=<%#Eval("ProxyID")%>' rel="lyteframe"
                                                            rev="width: 800px; height: 400px; overflow:hidden;" target="_blank" style="font-family: arial, Helvetica, sans-serif;
                                                            font-size: 12px; color: #800000; font-weight: bold;">
                                                            <asp:Label ID="lbl_ProxyID" runat="server" Text='<%#Eval("ProxyID") %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UpdateProxy">
                                                    <ItemTemplate>
                                                        <a id="ancher" href='InsertProxy.aspx?ProxyID=<%#Eval("ProxyID")%>' style="font-family: arial, Helvetica, sans-serif;
                                                            font-size: 12px; color: #800000; font-weight: bold;">UpdateProxy</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BookingType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_BookingType" runat="server" Text='<%#Eval("BookingType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TravelType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TravelType" runat="server" Text='<%#Eval("TravelType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ProxyFrom" runat="server" Text='<%#Eval("ProxyFrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ProxyTo" runat="server" Text='<%#Eval("ProxyTo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DDate" runat="server" Text='<%#Eval("DepartDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_RDate" runat="server" Text='<%#Eval("ReturnDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Class" runat="server" Text='<%#Eval("Class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Airlines">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Airlines" runat="server" Text='<%#Eval("Airlines") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Classes">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Classes" runat="server" Text='<%#Eval("Classes") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PaymentMode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%#Eval("PaymentMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>' Font-Bold="True"
                                                            ForeColor="Red"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Remark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_Reject" runat="server" CommandName="Reject" CommandArgument='<%#Eval("ProxyID") %>'
                                                            Font-Bold="True" Font-Underline="False" ForeColor="Red">Reject</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CRemark" runat="server" Text='<%#Eval("UpdateRemark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Remark">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LB_AgentComment" runat="server" CommandName="AgentComment" CommandArgument='<%#Eval("ProxyID") %>'
                                                            Font-Bold="True" Font-Underline="False" ForeColor="Red"><img src="../../images/Rmk.jpg"  border="0" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Agent&nbsp;Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_agenttype" runat="server" Text='<%#Eval("AgentType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                           <asp:TemplateField HeaderText="Sales&nbsp;Executive">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SalesExecId" runat="server" Text='<%#Eval("SalesExecId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                                
                                                <asp:TemplateField HeaderText="Requested Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_RequestedDate" runat="server" Text='<%#Eval("requestDateTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accepted Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_AcceptedDate" runat="server" Text='<%#Eval("AcceptedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
          </div> 
</asp:Content>
