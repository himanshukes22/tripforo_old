<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true" CodeFile="NewExecutive.aspx.cs" Inherits="NewExecutive" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .page-wrapperss {
            background-color: #fff;
            margin-left: 15px;
        }
    </style>
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
   
        function checkitt(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58)) {
                return false;
            }
            status = "";
            return true;
        }

        function getKeyCode(e) {
            if (window.event)
                return window.event.keyCode;
            else if (e)
                return e.which;
            else
                return null;
        }
        function keyRestrict(e, validchars) {
            var key = '', keychar = '';
            key = getKeyCode(e);
            if (key == null) return true;
            keychar = String.fromCharCode(key);
            keychar = keychar.toLowerCase();
            validchars = validchars.toLowerCase();
            if (validchars.indexOf(keychar) != -1)
                return true;
            if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
                return true;
            return false;
        }
    </script>
    <script src="<%=ResolveUrl("~/Js/jquery-ui-1.8.8.custom.min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Js/jquery-1.7.1.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=Submit.ClientID%>').click(function (event) {

                var returntypp = true;
                if ($.trim($("#<%=txtemail.ClientID%>").val()) == "") {

                    $("#<%=txtemail.ClientID%>").focus();
                    $('.error').show();
                    returntypp = false;
                }
                else {
                    $('.error').hide();
                }
                if ($.trim($("#<%=txtpassword.ClientID%>").val()) == "") {

                    $("#<%=txtpassword.ClientID%>").focus();
                    $('.error1').show();
                    returntypp = false;
                }
                else {
                    $('.error1').hide();
                }
                if ($.trim($("#<%=txtname.ClientID%>").val()) == "") {

                    $("#<%=txtname.ClientID%>").focus();
                    $('.error2').show();
                    returntypp = false;
                }
                else {
                    $('.error2').hide();
                }

                if ($.trim($("#<%=txtmobile.ClientID%>").val()) == "") {

                    $("#<%=txtmobile.ClientID%>").focus();
                    $('.error3').show();
                    returntypp = false;
                }
                else {
                    $('.error3').hide();
                }

               
                return returntypp;
            });

        });
        //function confirmUpdate(thisObj) {
           

           
        //    if ($.trim($("#ctl00_ContentPlaceHolder1_GridView1_ctl02_lbtnEdit").val()) == "") {
                
        //        $("#ctl00_ContentPlaceHolder1_GridView1_ctl02_lbtnEdit").focus();
        //        return false;
        //    }
        //    else {
        //        alert("hi");
        //        var upd = confirm('Are you sure to update this configuration');
        //        if (upd == true) {
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
        //    }
        //}
        function confirmDelete() {
            var upd = confirm('Are you sure to delete this configuration');
            if (upd == true) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
   
   

          <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Settings</a></li>
        <li><a href="#">Add new staff</a></li>
        
    </ol>
        <div class="card-main"> 
        <div class="inner-box add-staff" style="padding:15px;">
                   
                        <div class="row">
                            <div class="col-md-4">
                                <div class="  ">
                                    <label for="exampleInputPassword1" id="lblemail" runat="server"></label>
                                      <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                    <asp:TextBox CssClass="theme-search-area-section-input" runat="server" placeholder="Email" ID="txtemail" MaxLength="50"></asp:TextBox>
                                 


                                </div>
                            </div>
                                    </div>
                                   <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="txtemail" ErrorMessage="*"
                                        Display="dynamic" ValidationGroup="group1"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtemail" ValidationGroup="group1" ErrorMessage=" enter valid email "
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>


                            <div class="col-md-4">
                                <div class=" ">
                                    <label for="exampleInputPassword1" id="lblpassword" runat="server"></label>
                                      <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-lock"></i>
                                    <asp:TextBox CssClass="theme-search-area-section-input" placeholder="Password" runat="server" ID="txtpassword" MaxLength="40" TextMode="Password"></asp:TextBox>
                                   
                                </div>
                                          </div>
                            </div>
                                 <asp:RequiredFieldValidator ID="RFVMK1" runat="server" ControlToValidate="txtpassword" ErrorMessage="*"
                                        Display="dynamic" ValidationGroup="group1"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtpassword" ValidationGroup="group1" Display="dynamic" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{5,}$" ErrorMessage="Minimum 5 characters atleast 1 Alphabet, 1 Number and 1 Special Character" ForeColor="Red" />
                            </div>

                            <div class="col-md-4">
                                <div class=" ">
                                    <label for="exampleInputPassword1" id="lblmobile" runat="server"></label>
                                      <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-phone"></i>
                                    
                                    <asp:TextBox CssClass="theme-search-area-section-input" placeholder="Mobile" onkeypress="return checkitt(event)" runat="server" ID="txtmobile" MaxLength="10"></asp:TextBox>
                                   
                                </div>
                                          </div>
                                    </div>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmobile" ErrorMessage="*"
                                        Display="dynamic" ValidationGroup="group1"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="group1"
                                        ControlToValidate="txtmobile" ErrorMessage="Please Fill valid 10 digit mobile no."
                                        ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                            </div>



                            <div class="col-md-4">
                                <div class=" ">
                                    <label for="exampleInputPassword1" id="lblname" runat="server"></label>
                                      <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                    
                                    <asp:TextBox CssClass="theme-search-area-section-input" placeholder="Name" onKeyPress="return keyRestrict(event,' abcdefghijklmnopqrstuvwxyz');" runat="server" ID="txtname" MaxLength="50"></asp:TextBox>
                                   
                                </div>
                                          </div>
                                </div>
                                 <asp:RequiredFieldValidator ID="RFVMK2" runat="server" ControlToValidate="txtname" ErrorMessage="*"
                                        Display="dynamic" ValidationGroup="group1"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtname" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{5,30}$" runat="server" ErrorMessage="Minimum 5 and Maximum 50 characters required."></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtname" ValidationGroup="group1"
                                        ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Valid characters: Alphabets and space." />
                            </div>
                            <div class="col-md-4">
                                <div>
                                     <label for="exampleInputEmail1" id="lblBranch"></label>
                                      <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                                   
                                    <asp:TextBox CssClass="theme-search-area-section-input" placeholder="Address" runat="server" ID="txtAddress" MaxLength="200"></asp:TextBox>
                                </div>
                                          </div>
                                    </div>

                            </div>

                            <div class="col-md-4">
                                <div>
                                    <label for="exampleInputEmail1" id="lblrole_type">Active Service</label>
                                    <asp:CheckBoxList ID="CheckBoxServiceType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="true" Text="Login"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="Flight"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="Hotel"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="Bus"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>

                            </div>



                        </div>


                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    
                                  <div class="btn-save">
                                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn cmn-btn" OnClick="BtnSubmit_Click" ValidationGroup="group1" />
                                   </div>
                                    <label for="exampleInputPassword1" id="Label1" runat="server" style="color: red;"></label>

                                </div>
                            </div>
                        </div>
                        <br />

            
                            
                            </div>


            </div>

         <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="overflow: auto;">
                                    <ContentTemplate>
                                        <asp:GridView ID="grd_P_IntlDiscount" runat="server" AutoGenerateColumns="false"
                                            CssClass="rtable" GridLines="None" Width="100%" PageSize="30" OnRowCancelingEdit="grd_P_IntlDiscount_RowCancelingEdit"
                                            OnRowEditing="grd_P_IntlDiscount_RowEditing" OnRowUpdating="grd_P_IntlDiscount_RowUpdating" OnRowDeleting="OnRowDeleting" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="DEBIT/CREDIT" SortExpression="DEBIT/CREDIT">
                                                    <ItemTemplate>
                                                        <a href='DebitCredit.aspx?UserId=<%#Eval("UserId")%>&ID=<%#Eval("StaffId")%>' rel="lyteframe" rev="width: 900px; height: 400px; overflow:hidden;"
                                                            target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">DEBIT/CREDIT                                                          
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="UserId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdPassword" runat="server" Text='<%#Eval("Password") %>' Width="100px" BackColor="#ffff66"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdName" runat="server" Text='<%#Eval("Name") %>' Width="100px" BackColor="#ffff66"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdMobile" runat="server" Text='<%#Eval("Mobile") %>' Width="100px" BackColor="#ffff66"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdEmail" runat="server" Text='<%#Eval("Email") %>' Width="100px" BackColor="#ffff66"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdAddress" runat="server" Text='<%#Eval("Address") %>' Width="100px" BackColor="#ffff66"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login_Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("sStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_IsActive" runat="server" Width="150px" DataValueField='<%#Eval("Status")%>' SelectedValue='<%#Eval("Status")%>'>
                                                            <asp:ListItem Value="True" Text="ACTIVE"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="DACTIVEEACTIVE"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Flight">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlight" runat="server" Text='<%#Eval("sFlight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_Flight" runat="server" Width="150px" DataValueField='<%#Eval("Flight")%>' SelectedValue='<%#Eval("Flight")%>'>
                                                            <asp:ListItem Value="True" Text="ACTIVE"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="DEACTIVE"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hotel">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHotel" runat="server" Text='<%#Eval("sHotel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_Hotel" runat="server" Width="150px" DataValueField='<%#Eval("Hotel")%>' SelectedValue='<%#Eval("Hotel")%>'>
                                                            <asp:ListItem Value="True" Text="ACTIVE"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="DEACTIVE"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBus" runat="server" Text='<%#Eval("sBus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_Bus" runat="server" Width="150px" DataValueField='<%#Eval("Bus")%>' SelectedValue='<%#Eval("Bus")%>'>
                                                            <asp:ListItem Value="True" Text="ACTIVE"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="DEACTIVE"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CheckBalance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCheckBalance" runat="server" Text='<%#Eval("sCheckBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_CheckBalance" runat="server" Width="150px" DataValueField='<%#Eval("CheckBalance")%>' SelectedValue='<%#Eval("CheckBalance")%>'>
                                                            <asp:ListItem Value="True" Text="ACTIVE"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="DEACTIVE"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance">
                                                    <ItemTemplate>                                                        
                                                        <asp:Label ID="Balance" runat="server" Text='<%#Eval("CreditLimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="LastTransDate">
                                                    <ItemTemplate>                                                        
                                                        <asp:Label ID="CreditLimitTrnsDate" runat="server" Text='<%#Eval("CreditLimitTrnsDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CreatedDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CreatedBy">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--CreatedDate, CreatedBy, UpdatedDate, UpdatedBy--%>
                                                <asp:TemplateField HeaderText="UpdatedDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UpdatedBy">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EDIT">
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnledit" runat="server" Text="Edit" CommandName="Edit" Font-Bold="true"
                                                            CssClass="newbutton_2" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="lnlupdate" runat="server" Text="Update" CommandName="Update" Font-Bold="true" CssClass="newbutton_2" />
                                                        <asp:Button ID="lnlcancel" runat="server" Text="Cancel" CommandName="Cancel" Font-Bold="true"
                                                            CssClass="newbutton_2" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Button ID="btn_delete" CssClass="newbutton_2" runat="server" Text="Delete" CommandName="Delete" OnClientClick="if(!confirm('Do you want to delete?')){ return false; };" Font-Bold="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <EmptyDataTemplate>No records Found</EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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


</asp:Content>

