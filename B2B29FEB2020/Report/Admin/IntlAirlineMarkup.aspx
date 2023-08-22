<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="IntlAirlineMarkup.aspx.vb" Inherits="Reports_Admin_IntlAirlineMarkup" %>
 <%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />

    <%-- <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode


            if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8 || charCode == 46)) {
                //                alert("This field accepts only Float Number.");
                return false;


            }

            status = "";
            return true;

        }

        function Validate() {


            if (document.getElementById("mk").value == "") {

                alert('Please Fill Markup');
                document.getElementById("mk").focus();
                return false;
            }
            if (confirm("Are you sure!"))
                return true;
            return false;
        }


    </script>

    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
         <div class="large-3 medium-3 small-12 columns">
       
                <uc1:Settings runat="server" ID="Settings" />
            
    </div>
       <%-- <div class="large-3 medium-3 small-12 columns">


            <div class="fltnewmenu1">
                <a href="<%= ResolveUrl("~/Report/Admin/DomAirlineMarkup.aspx")%>">Dom. Airline Markup</a>
            </div>

            <div class="fltnewmenu1">
                <a href="<%= ResolveUrl("~/Report/Admin/IntlAirlineMarkup.aspx")%>">Intl. Airline Markup</a>
            </div>

            <%--<tr><td class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Accounts/DomSaleRegister.aspx")%>">Dom. Sale Register</a>
    </td></tr>

       <tr><td class="fltnewmenu1">
    <a  href="<%= ResolveUrl("~/Report/Accounts/IntlSaleRegister.aspx")%>">Intl. Sale Register</a>
    </td></tr>
        </div>--%>
       <div class="large-8 medium-8 small-12 columns heading">
                                            <div class="large-12 medium-12 small-12 heading1">Intl. Airline Markup
                            </div>
                                            <div class="clear1"></div>
                                            
      
                                            <div class="large-12 medium-12 small-12">
                                                <div class="large-4 medium-4 small-12 columns" style="display: none">
                
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                        <asp:ListItem Text="International" Selected="True" Value="I"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
           
                        <div class="redlnk large-12 medium-12 small-12">
                            <div class="large-2 medium-2 small-4 columns">Agent Type:
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                 <asp:DropDownList  ID="DropDownListType" runat="server"></asp:DropDownList>
                              <%--  <input type="text" id="txtAgencyName" name="txtAgencyName"  onfocus="focusObj(this);"
                                    onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />--%>
                            </div>
                            <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">Airline Code:
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:DropDownList runat="server" data-placeholder="Choose a Airline..." TabIndex="2"
                                    ID="airline_code" CssClass="chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                                                  <div class="clear1"></div>
                        <div class="redlnk large-12 medium-12 small-12">
                             <div class="large-2 medium-2 small-4 columns">Markup Type :
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:DropDownList ID="ddl_MarkupType" runat="server">
                                    <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                                    <asp:ListItem Value="P">Percentage</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">Mark Up per pax:
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:TextBox runat="server" ID="mk" onKeyPress="return checkit(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                        Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="clear1"></div></div>
             <div class="clear1"></div>
                            <div  class="large-12 medium-12 small-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        &nbsp;<asp:Label ID="lbl" runat="server" Style="color: #CC0000;"
                                            Font-Bold="True" Visible="False"></asp:Label><asp:Label ID="lbl_msg" runat="server"
                                                Style="color: #CC0000;" Font-Bold="True" Visible="False"></asp:Label>
                                        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                                        <div class="large-4 medium-4 small-12 large-push-8 medium-push-8">
                                             <div class="large-6 medium-6 small-6 columns">
                                        <asp:Button ID="btnAdd" runat="server" Text="New Entry" OnClientClick="return Validate()" /></div>
                                            <div class="large-6 medium-6 small-6 columns">&nbsp;<asp:Button
                                            ID="btn_submit" runat="server" Text="Search"  ToolTip ="Search with Agent type"/></div>
                                            
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                         <div class="clear1"></div></div>
        <div class="clear1"></div>
            </div>
                <div  class="large-10 medium-10 small-12 large-push-1 medium-push-1">
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <%-- <asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />--%>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Counter") %>' CssClass="hide"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserId" HeaderText="Agent Type" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true">
                                        <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AirlineCode" HeaderText="AirlineCode" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true">
                                        <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Trip" HeaderText="Trip" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true">
                                        <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MarkupValue" HeaderText="Mark Up" ControlStyle-CssClass="textboxflight1">
                                        <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="MarkUp Type">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelMrkType" runat="server" Text='<%# Eval("MarkupType")%>'></asp:Label>
                                         </ItemTemplate>
                                        <EditItemTemplate>

                                             <asp:DropDownList ID="ddl_MarkupTypeE" runat="server" SelectedValue='<%# Eval("MarkupType")%>'>
                                    <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                                    <asp:ListItem Value="P">Percentage</asp:ListItem>
                                </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="MarkupType" HeaderText="MarkUp Type" ControlStyle-CssClass="textboxflight1">
                                        <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                    </asp:BoundField>--%>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UP">
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
                </div>
            

        <script type="text/javascript">
            var UrlBase = '<%=ResolveUrl("~/") %>';
        </script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>
