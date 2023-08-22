<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="ExecutiveDetails.aspx.vb" Inherits="Reports_Admin_ExecutiveDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../../js/chrome.js"></script>
    <%-- <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>

    <style type="text/css">
        .log-buttion {
            color: #fff;
            font-size: 24px;
            text-decoration: none;
            margin-left: 560px;
            cursor: pointer;
        }

        .topbutton {
            float: right;
            margin-right: 20px;
            padding-top: 10px;
            font-size: 10px;
        }

        #toPopup {
            font-family: "lucida grande",tahoma,verdana,arial,sans-serif;
            background: none repeat scroll 0 0 #FFFFFF;
            border: 10px solid #ccc;
            border-radius: 3px 3px 3px 3px;
            color: #333333;
            display: none;
            font-size: 14px;
            left: 65%;
            margin-left: -402px;
            position: fixed;
            top: 35%;
            width: 350px;
            height: 150px;
            z-index: 2;
        }

        .topbutton1 {
            color: #fff;
            text-decoration: none;
            padding: 0px 10px 0px 10px;
        }

        .toPopup {
            background: url(images/fade.png);
            display: none;
            left: 0;
            position: fixed;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
        }

        #toPopup1 {
            font-family: "lucida grande",tahoma,verdana,arial,sans-serif;
            background: none repeat scroll 0 0 #FFFFFF;
            border: 10px solid #ccc;
            border-radius: 3px 3px 3px 3px;
            color: #333333;
            display: none;
            font-size: 14px;
            left: 65%;
            margin-left: -402px;
            position: fixed;
            top: 35%;
            width: 350px;
            height: 150px;
            z-index: 2;
        }

        .toPopup1 {
            background: url(images/fade.png);
            display: none;
            left: 0;
            position: fixed;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
        }
    </style>

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
           
            $(".totoPopup").live("click", function () {
                $(".toPopup").fadeIn();

            });
            //$(".totoPopup").click(function () {
            //    $(".toPopup").fadeIn();
               
            //});
            $("#CANCEL").live("click", function () {
                $(".toPopup").fadeOut();

            });
           
            $("[id*=DropDownListStaffType").live("change",function () {
                if ($("[id*=DropDownListStaffType").val() == "E") {
                    $("#trip").show();

                }
                else {
                    $("#trip").hide();
                }
            });


        });

    </script>
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--<input type="button"  class="totoPopup button"  name="Add New Executive"  />--%>
            <%--<div class="totoPopup" style="position: fixed; top: 180px; background: #fff; padding: 10px; cursor: pointer; left: 0px; width: 108px; font-weight: bold; box-shadow: 1px 1px 5px #333;">
                <div style="width: 100%;">
                    <img src="images/issuei.jpg" style="width: 100%;" /></div>
                <div style="width: 94%; padding: 3%; font-size: 18px; line-height: 30px; color: #555;">Report an Issue and Get a Quick Response</div>
            </div>--%>
          

            <div class="toPopup" style="display: none; background-color:#f9f9f9;">
                <div style="margin: 50px auto 0;" class="large-7 medium-12 small-12 heading">
                    <div class=" heading1">
                        
                       Add New Executive
                    </div>
                    <div class="clear1"></div>
                    <div>
                          <asp:Label ID="LabelMsg" runat="server" Text="Executive succesfully created." Font-Bold="true" ForeColor="Red"></asp:Label>
                        <div class="large-12 medium-12 small-12">
                            
                                <div class="large-2 medium-2 small-4 columns">Email</div>
                                <div class="large-3 medium-3 small-8 columns">
                                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                              
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Email is Required" ControlToValidate="TextBoxEmail" ValidationGroup="N"></asp:RequiredFieldValidator><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ErrorMessage="Email Id seems to be incorrect." ControlToValidate="TextBoxEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="N"></asp:RegularExpressionValidator></div>
                             
                                <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">Password</div>
                                <div class="large-3 medium-3 small-8 columns  large-push-2 medium-push-2">
                                    <asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password"></asp:TextBox>
                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is Required" ControlToValidate="TextBoxPass" ValidationGroup="N"></asp:RequiredFieldValidator></div>
                            <div class="clear"></div>

                                <div class="large-2 medium-2 small-4 columns">Name</div>
                                <div class="large-3 medium-3 small-8 columns">
                                    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ErrorMessage="Name is Required" ControlToValidate="TextBoxName" ValidationGroup="N"></asp:RequiredFieldValidator></div>
                            
                                <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">Mobile No</div>
                                <div class="large-3 medium-3 small-8 columns large-push-2 medium-push-2">
                                    <asp:TextBox ID="TextBoxMobileNo" runat="server"></asp:TextBox>
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorMobileNo" runat="server" ErrorMessage="Mobile No is Required." ControlToValidate="TextBoxMobileNo" ValidationGroup="N"></asp:RequiredFieldValidator>
                                   
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server" ErrorMessage="Mobile no must ten digit number." ControlToValidate="TextBoxMobileNo" ValidationExpression="^\d{10}$" ValidationGroup="N"></asp:RegularExpressionValidator>
                                </div>
                           <div class="clear"></div>
                                <div class="large-2 medium-2 small-4 columns">Department</div>
                                <div class="large-3 medium-3 small-8 columns">
                                    <asp:DropDownList ID="DropDownListStaffType" runat="server" >
                                        <asp:ListItem Selected="True" Text="Accounts Staff" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Operational Staff" Value="E"></asp:ListItem>
                                    </asp:DropDownList></div>
                                
                            <div id="trip" style="display:none;">
                                <div class="large-2 medium-2 small-4 columns  large-push-2 medium-push-2">Trip</div>
                                <div class="large-3 medium-3 small-8  columns" >
                                    <asp:DropDownList ID="DropDownListTrip" runat="server" >
                                        <asp:ListItem Selected="True" Text="Domestic" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="International" Value="I"></asp:ListItem>
                                    </asp:DropDownList></div>
                                
                            </div>
                            <div class="clear"></div>
                            
                                <div class="large-2 medium-2 small-4 large-push-2 small-push-2 columns">
                                    <asp:Button ID="ButtonSubmit" runat="server" CssClass="btn" Text="Submit" ValidationGroup="N" />
                                  

                                     
                                </div>
                                <div class="large-2 medium-2 small-4 large-push-2 small-push-2 columns"><div class="btn"  id="CANCEL" >Cancel</div></div>
                            

                              <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>



            <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl">
                <tr>
                    <td style="background: #fff; width: 900px;">
                        <div class="totoPopup btn" style="width:150px;">New Executive</div>
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="New Executive" Visible="false"
                            CssClass="btn" /><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                            CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <%--<asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />--%>

                                 <asp:TemplateField >
                                    <ItemTemplate>                                        
                                        <asp:Label  runat="server" ID="labelsrno" Text='<%# Eval("counter")%>'  CssClass="hide" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="User ID" >
                                    <ItemTemplate>                                        
                                        <asp:Label  runat="server" ID="labeluserId" Text='<%# Eval("user_id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField DataField="user_id" HeaderText="User ID" ControlStyle-CssClass="textboxflight1" >
                                    <ControlStyle CssClass="textboxflight1" ></ControlStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="user_pwd" HeaderText="Password" ControlStyle-CssClass="textboxflight1">
                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        
                                        <asp:Label  runat="server" ID="labeldep" Text='<%# GetDeptname(Eval("Dept"))%>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Trip">
                                    <ItemTemplate>
                                        
                                        <asp:Label  runat="server" ID="labeltrip" Text='<%# Eval("Trip")%>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                               <%-- <asp:BoundField DataField="Dept" HeaderText="Department" ControlStyle-CssClass="textboxflight1">
                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                </asp:BoundField>--%>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                            
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>
