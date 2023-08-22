<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="AgentMarkupset.aspx.vb" Inherits="Transfer_AgentMarkupset" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--<link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="../../js/chrome.js"></script>

    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
<div class="mtop80"></div>
   <div class="large-3 medium-3 small-12 columns">
           &nbsp; 
   </div>
     <div class="large-8 medium-8 small-12 columns">
       
            <div class="redlnk large-12 medium-12 small-12">
                <div class="large-2 medium-2 small-3 columns">
                    Agent ID:
                </div>
                <div class="large-3 medium-3 small-9 columns large-pull-7 medium-pull-7">
                    <asp:TextBox runat="server" ID="uid" ReadOnly="true"></asp:TextBox>
                </div>
                
                
            </div>
         <div class="clear1"></div>
            <div class="redlnk large-12 medium-12 small-12"">
                <div class="large-2 medium-2 small-4 columns">
                    MarkUp Per Pax:
                </div>
                <div class="large-3 medium-3 small-8 columns">
                    <asp:TextBox runat="server" ID="mk"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                        Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                </div>
                <div class="large-2 medium-2 small-4 large-push-2 medium-push-2 columns">
                    MarkUpType:
                </div>
                <div class="large-3 medium-3 small-8 columns">
                <asp:DropDownList ID="ddl_mktyp" runat="server" class="ddlBoxpax">
                            <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                            <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                             </asp:DropDownList>
                   <%-- <asp:TextBox runat="server" ID="mktyp" Text="Fixed" ReadOnly="true" Style="width: 40px"></asp:TextBox>--%>
                </div>
                 
            </div>
           <div class="clear1"></div>
                <div class="large-12 medium-12 small-12">
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                            
                           
                                    <div class="large-2 medium-2 small-12 columns large-push-10 medium-push-10">
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="buttonfltbk" Text="submit" />
                                    </div>
                                    <div class="large-12 medium-12 small-12">
                                        <asp:Label ID="lbl" runat="server" Style="color: #CC0000; "></asp:Label>
                                    </div>
                            
                               
                                    <div class="large-12 medium-12 small-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                                  PageSize="8"  CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                                           
                                            <Columns>
                                              
                                                <asp:BoundField DataField="COUNTER" HeaderText="Sr.No" ReadOnly="True" />
                                                <asp:BoundField DataField="USERID" HeaderText="Agent ID" ControlStyle-CssClass="textboxflight1"
                                                    ReadOnly="true" />
                                              <%--  <asp:BoundField DataField="TRANSFER_TYPE" HeaderText="TRANSFER_TYPE" ControlStyle-CssClass="textboxflight1"
                                                    ReadOnly="true" />--%>
                                                <asp:BoundField DataField="MARKUP_VALUE" HeaderText="Mark Up" ControlStyle-CssClass="textboxflight1" />
                                                <asp:BoundField DataField="MARKUP_TYPE" HeaderText="MarkUpType" ControlStyle-CssClass="textboxflight1"
                                                    ReadOnly="true" />
                                                
                                            </Columns>
                                            
                                        </asp:GridView>
                                    </div>
                               
                        </ContentTemplate>
                    </asp:UpdatePanel></div>

         <div class="clear1"></div>

        </div>

    <div class="clear1"></div>
        <div class="large-12 medium-12 small-12">
                    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
                                <ProgressTemplate>
                                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                        padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                        z-index: 1000;">
                                    </div>
                                    <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                        z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                        font-weight: bold; color: #000000">
                                        Please Wait....<br />
                                        <br />
                                        <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                        <br />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                </div>
            
       
</asp:Content>

