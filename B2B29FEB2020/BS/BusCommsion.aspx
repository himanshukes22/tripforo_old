<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="BusCommsion.aspx.cs" Inherits="BS_BusCommsion" %>
<%@ Register Src="~/UserControl/BusControlsetting.ascx" TagPrefix="busMenu" TagName="BusControlsetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../../js/chrome.js"></script>
     <link href="../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
  <%--     <link href="<%=ResolveUrl("~/BS/CSS/CommonCss.css")%>" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../../js/chrome.js"></script>--%>
   
    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
     <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">
          <busMenu:BusControlsetting runat="server" id="BusControlsetting" />
        </div>
        </div>
    <div style="width: 45%; margin: auto; padding: 10px; border: 1px solid #ccc; text-align: left;">
        <div style="line-height: 30px; font-weight: bold; font-size: 16px; color: #888; padding-left: 20px; border-bottom: 2px solid #ccc;">
            Bus Commission
        </div>

        <table style="width: 100%;" cellspacing="10">
            <tr>
              
                 <td class="fontStyle">Agent Type
                </td>
                <td class="fontStyle"> Commission Type 
                </td>
                 <td class="fontStyle">Commission
                </td>
            </tr>
             <tr>
                <td>
                 
                     <asp:DropDownList ID="DropDownListType" runat="server"></asp:DropDownList>
                    
                </td>
                <td>
                     <asp:DropDownList ID="ddl_commisionType" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                        <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                        <asp:ListItem Value="P">Percentage</asp:ListItem>
                    </asp:DropDownList>
                </td>

                 <td>
                     <asp:TextBox runat="server" ID="txtCommision" onkeypress="phone_vali();" MaxLength="6"></asp:TextBox>
                </td>
            </tr>

             <tr>
              
                 <td class="fontStyle">Status
                </td>
                <td class="fontStyle"> Provider Name
                </td>
            </tr>
             <tr>
                <td>
                 <asp:DropDownList ID="ddlstatus" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                        <asp:ListItem Value="Active" >Active</asp:ListItem>
                        <asp:ListItem Value="InActive">InActive</asp:ListItem>
                    </asp:DropDownList>
                    
                    
                </td>
                <td>
                     <asp:DropDownList ID="ddlprovidername" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                        <asp:ListItem Value="RB" Selected="true">RedBus</asp:ListItem>
                       <%-- <asp:ListItem Value="ES">EstService</asp:ListItem>
                           <asp:ListItem Value="TY">Travelyari</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
             
                <td>
                    <asp:Button ID="Button1" CssClass="buttonfltbk" runat="server" OnClick="btnAdd_Click" Text="Add New" />
                </td>
                
            </tr>

            <tr>
                 <td>
                <asp:Label ID="lbl" runat="server" Style="color: #CC0000;"></asp:Label>
                     </td>
                </tr>
        </table>
           
                            
                      
    </div>

    <table style="width: 100%;" cellspacing="10">
        <tr>
            <td>
                <asp:UpdatePanel ID="UP" runat="server">
                    <ContentTemplate>                     
                        <div class="clear1"></div>
                        <div class="large-12 medium-12 small-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                CssClass="GridViewStyle" Width="100%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />

                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                 
                                    <asp:BoundField DataField="AG_TYPE" HeaderText="Agent Type" ControlStyle-CssClass="textboxflight1" ReadOnly="true" />
                                   <%-- <asp:BoundField DataField="COMMISSION" HeaderText="Commission" ControlStyle-CssClass="textboxflight1" />--%>


                                     <asp:TemplateField HeaderText="Commission">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_COMMISSION" runat="server" Text='<%# Eval("COMMISSION")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_COMMISSION" runat="server" Text='<%# Eval("COMMISSION")%>'  onkeypress="phone_vali();" MaxLength="6"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Commission_type" HeaderText="Commission_Type" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true" />
                                    <%-- <asp:BoundField DataField="AG_STATUS" HeaderText="Agent Status" ControlStyle-CssClass="textboxflight1"
                                        />--%>

                                      <asp:TemplateField HeaderText="Agent Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# GetStatusVal( Eval("AG_STATUS")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="200px" SelectedValue='<%# Eval("AG_STATUS").ToString() %>'>
                                            <asp:ListItem Value="Active" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="Inactive" Text="Inactive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                     <asp:BoundField DataField="CREATEDDATE" HeaderText="Created Date" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </div>
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
            </td>

        </tr>
    </table>

 <%--   <script src="<%= ResolveUrl("~/BS/JS/BusSearch.js")%>" type="text/javascript"></script>--%>
    
</asp:Content>

