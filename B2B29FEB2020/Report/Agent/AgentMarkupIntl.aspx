<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="AgentMarkupIntl.aspx.vb" Inherits="Reports_Agent_AgentMarkupIntl" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../chosen/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../chosen/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58 || charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
    </script>
    <script type="text/javascript" src="../../js/chrome.js"></script>
   
     <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Settings</a></li>
        <li><a href="#">International Airline Markup</a></li>
        
    </ol>


    <div class="card-main">

    <div class="inner-box" style="padding:15px;">
        
           <div class="row">
                <div class="col-md-3 col-xs-3">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                    <asp:TextBox runat="server" ID="txt_AgentID" placeholder="Agent ID" class="theme-search-area-section-input" ReadOnly="true"></asp:TextBox>
                                </div>
                        </div>
                </div>
                <div class="col-md-3 col-xs-3">
                     <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:DropDownList runat="server" data-placeholder="Choose a Airline..." TabIndex="2" class="theme-search-area-section-input" ID="airline_code">
                    </asp:DropDownList>
                                </div>
                         </div>
                </div>
                <div class="col-md-3 col-xs-3">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                    <asp:TextBox ID="mk" runat="server" placeholder="Markup" onKeyPress="return checkit(event)" class="theme-search-area-section-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*" Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                </div>
                        </div>
                            </div>
                <div class="col-md-3 col-xs-3" id="tr_Cat" runat="server">
                       <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                    <asp:DropDownList ID="ddl_MarkupType" runat="server" CssClass="theme-search-area-section-input" placeholder="MarkUp Type">
                        <asp:ListItem>Select Markup Type</asp:ListItem>
                        <asp:ListItem Value="F" Selected="true">Fixed</asp:ListItem>
                        <asp:ListItem Value="P" ForeColor="#999999">Percentage</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                           </div>
                </div>
                 </div>
        <br />
        <div class="row">
                  <div class="btn-addd col-md-3 col-xs-4">
                 <asp:Button ID="btnAdd" runat="server" OnClientClick="return confirm('Are you sure you want to add this?');" class="btn cmn-btn" Text="New Entry" />
            </div>
            </div>

        <div class="col-md-12 col-xs-12">
            
         


            <asp:Label ID="lbl" runat="server" Style="color: #CC0000;" Visible="False"></asp:Label>
            <asp:Label ID="lbl_msg" runat="server" Style="color: #CC0000;" Font-Bold="True" Visible="False"></asp:Label>
        </div>
          <br />
        <br />
        </div>
        </div>


    <div class="card-main">
        
        <div class="large-12 medium-12 small-12">
            <div>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                    Visible="false">
                    <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                    <asp:ListItem Text="International" Selected="True" Value="I"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="clear1"></div>

            <div class="large-12 medium-12 small-12">
                <asp:UpdatePanel ID="UP" runat="server">
                    <ContentTemplate>
                        <div class="large-12 medium-12 small-12">

                         
                            <div class="clear1"></div>
                            <div class="large-12 medium-12 small-12">
                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                  <%--  <Columns>
                                        <asp:CommandField ShowEditButton="True" />                                  
                                        <asp:TemplateField HeaderText="Sr.No" Visible="false" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="AirlineCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("AirlineCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>      
                                        <asp:BoundField DataField="Trip" HeaderText="Trip" ControlStyle-CssClass="textboxflight1"
                                            ReadOnly="true" Visible="false">
                                            <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="Markup Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MarkupValue" runat="server" Text='<%#Eval("MarkupValue")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_markupvalue" runat="server" onkeypress="return checkit(event)" Text='<%#Eval("MarkupValue")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>                                    
                                        <asp:TemplateField HeaderText="Markup Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MarkupType" runat="server" Text='<%#Eval("MarkupType")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_mktyp" runat="server">
                                                    <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                                                    <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>--%>
                                    <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:TemplateField HeaderText="Agent ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluser_id" runat="server" Text='<%#Eval("Userid")%>'></asp:Label>
                                            <asp:Label ID="lbl_counter" Visible="false" runat="server" Text='<%#Eval("counter")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AirlineName" HeaderText="Airline" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger" />
                                    <asp:TemplateField HeaderText="Markup Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MarkupValue" runat="server" Text='<%#Eval("MarkupValue")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_markupvalue" runat="server" onkeypress="return checkit(event)" Text='<%#Eval("MarkupValue")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Markup Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MarkupType" runat="server" Text='<%#Eval("MarkupType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddl_mktyp" runat="server">
                                                <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                                                <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    
                                </Columns>
                                </asp:GridView>
                            </div>
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
            </div>

        </div>
    </div>
    



    





    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8)) {
                //                alert("This field accepts only Float Number.");
                return false;
            }
            status = "";
            return true;
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Data has been updated successfully !!");
                }
                    break;
                case 2: {
                    alert("Markup cannot be blank");
                }
                    break;
                case 3: {
                    alert("MarkUp For This AirLine Is Already Exist.");
                }
                    break;
            }
        }
    </script>

</asp:Content>
