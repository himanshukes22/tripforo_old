<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="ModuleAccess.aspx.vb" Inherits="SprReports_Admin_ModuleAccess" %>
<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .ONCLASS {
        }

        .OFFCLASS {
        }

        .OFFGRID {
            background-color: #F1F1F1;
        }

        .ONGRID {
            background-color: White;
        }

        .HDRCLS {
            text-align: left;
        }
    </style>
    <%--<div>--%>

    <%--<asp:GridView ID="GridView1" runat="server" AllowPaging="false" 
            AllowSorting="false" AutoGenerateColumns="False" OnRowCommand="GridView1_Command">               
           <Columns>  
                 <asp:BoundField DataField="COUNTER" HeaderText="COUNTER"/>
                <asp:BoundField DataField="USERID" HeaderText="USERID"/>
                <asp:BoundField DataField="MODULE" HeaderText="MODULE" />
                <asp:BoundField DataField="MODULETYPE" HeaderText="MODULETYPE" />   
                <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
               <asp:TemplateField HeaderText="STATUS">
                <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%#Eval("STATUS") %>'></asp:LinkButton>
                </ItemTemplate>
                </asp:TemplateField>                   
     
           </Columns>
       </asp:GridView>
    --%>    <%--</div>--%>
    <table class="w20 lft mlft10">
        <tr>
            <td>
                <uc1:Settings runat="server" ID="Settings" />
            </td>
        </tr>
    </table>
    <div class="boxshadow lft w70">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 20%"></td>
                        <td style="padding: 8px; color: #fff;">MODULE ACCESS
                        </td>
                        <td style="width: 20%"></td>
                    </tr>
                    <tr>
                        <td style="width: 20%"></td>
                        <td align="center" style="width: 60%; color: #000000; padding-bottom: 10px; padding-top: 10px; font-size: 14px; font-weight: bold">SELECT USER ID   &nbsp;&nbsp;&nbsp;&nbsp;         
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="300px" Height="25px">
                            </asp:DropDownList>

                        </td>
                        <td style="width: 20%"></td>
                    </tr>

                    <tr>
                        <td style="width: 20%"></td>
                        <td style="width: 60%" align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowCommand="GridView1_Command" PageSize="1000"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                <Columns>
                                    <asp:TemplateField HeaderText="COUNTER" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_COUNTER" runat="server" Text='<%# Eval("COUNTER") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="STATUS" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="USERID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_USERID" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MODULE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MODULE" runat="server" Text='<%# Eval("MODULE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MODULETYPE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MODULETYPE" runat="server" Text='<%# Eval("MODULETYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MODULE STATUS">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" style="border-top-style: 0; border-right-style: 0; border-bottom-style: 0; border-left-style: 0; border-width: 0px;">
                                                        <asp:LinkButton ID="lnk_ON" runat="server" CommandName="OFF" CssClass="ONCLASS"><img src="../../Images/on.png" /></asp:LinkButton><asp:LinkButton
                                                            ID="lnk_OFF" runat="server" CommandName="ON" CssClass="OFFCLASS"><img src="../../Images/off.png"  /></asp:LinkButton>
                                                        <%-- <asp:ImageButton ID="img_ON" runat="server" CommandName="ONN"  ImageUrl="~/Images/arrow.jpg" /> <asp:ImageButton runat="server" ID="img_OFF" CommandName="OFFF" ImageUrl="~/Images/ak.gif" />--%>
                                                    </td>
                                                    <%--<td align="right"  style="border-top-style: 0; border-right-style: 0; border-bottom-style: 0; border-left-style: 0; border-width: 0px"> <asp:LinkButton ID="lnk_OFF" runat="server" CommandName="OFF" Width="80px" Height="25px" ></asp:LinkButton></td>--%>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%-- <RowStyle CssClass="RowStyle" Font-Size="10px" Height="10px" />
                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HDRCLS HeaderStyle" Font-Size="11px" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />--%>
                            </asp:GridView>
                        </td>
                        <td style="width: 20%"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

