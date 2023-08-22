<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="SprReports_Admin_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<style>
        input[type="text"], input[type="password"], select, textarea
        {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>--%>

    <script language="javascript" type="text/javascript">
        function checkpwd() {
            if (document.getElementById("").value != document.getElementById("").value) {
                alert('Please Enter Same Password');

            } 
        }
    </script>

   <%-- <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="mtop80"></div>
            <div class="large-10 medium-10 small-12 large-push-1 medium-push-1 heading">
                <div class="large-12 medium-12 small-12 heading1">Profile</div>
                   <div class="clear1"></div>
                         <div class="large-6 medium-6 small-6 columns">
                                    Login Details
                                </div>
                                <div class="large-1 medium-1 small-2 columns end">
                                    <asp:LinkButton ID="LinkEdit" runat="server" Font-Bold="True"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton>
                                </div>
                          <div class="clear1"></div>
               <div class="large-12 medium-12 small-12">
                                                        <div class="large-6 medium-6 small-6 columns">
                                                            Username
                                                        </div>
                                                        <div id="td_username" runat="server" class="large-6 medium-6 small-6 columns">
                                                        </div>
                   <div class="clear1"></div>
               </div>
                <div class="clear1"></div>
                                                   
                                                    <div id="td_login" runat="server" class="large-12 medium-12 small-12">
                                                        <div class="large-6 medium-6 small-6 columns">
                                                            Password
                                                        </div>
                                                        <div class="large-6 medium-6 small-6 columns">
                                                            ******
                                                        </div>
                                                    </div>
                                                
                                            <div class="clear"></div>
                                            <div id="td_login1" runat="server" visible="false" class="large-12 medium-12 small-12">
                                                
                                                        <div class="large-1 medium-1 small-3 columns">
                                                            Password
                                                        </div>
                                                        <div class="large-3 medium-3 small-9 columns">
                                                            <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    
                                                        <div class="large-2 medium-3 small-3 columns large-push-2 medium-push-2">
                                                            Confirm Password
                                                        </div>
                                                        <div class="large-3 medium-3 small-9 columns large-push-2 medium-push-2">
                                                            <asp:TextBox ID="txt_cpassword" runat="server" TextMode="Password"></asp:TextBox>
                                                            <asp:Label ID="lbl_msg" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    <div class="clear"></div>
                                                        <div class="large-2 medium-2 small-6 columns">
                                                            <asp:Button ID="btn_Save" runat="server" Text="Save" />
                                                            
                                                        </div>
                                                        <div class="large-2 medium-2 small-6 columns end"><asp:Button ID="lnk_Cancel" runat="server" CssClass="cancelprofile"
                                                                Font-Bold="False"  Text="Cancel" /></div>
                                                    
                                            </div>
                                    <div class="clear1"></div>   
                                
                        <div class="large-12 medium-12 small-12"><hr /></div>
                           
                                <div class="large-6 medium-6 small-6 columns">
                                    Details
                                </div>
                                <div class="large-1 medium-1 small-2 columns end">
                                    <asp:LinkButton ID="LinkEditAdd" runat="server" Font-Bold="True"><img src="../../Images/edit.png" alt="Edit" /></asp:LinkButton>
                                </div>
                            
                        
                    <div class="clear1"></div>
                                <div id="td_Address" runat="server" valign="top">
                                    
                                            <div class="large-1 medium-2 small-3 columns">
                                                Name
                                            </div>
                                            <div id="td_name" runat="server" class="large-3 medium-3 small-9 columns">
                                            </div>
                                       
                                            <div class="large-1 medium-2 small-3 columns large-push-2 medium-push-2">
                                                EmailId
                                            </div>
                                            <div id="td_email" runat="server" class="large-3 medium-3 small-9 columns  large-push-2 medium-push-2">
                                            </div>
                                       <div class="clear1"></div>
                                            <div class="large-1 medium-2 small-3 columns">
                                                Mobile
                                            </div>
                                            <div id="td_mobile" runat="server" class="large-3 medium-3 small-9 columns">
                                            </div>
                                       
                                            <div class="large-1 medium-2 small-3 columns large-push-2 medium-push-2">
                                                
                                            </div>
                                            <div id="td_Country" runat="server" class="large-3 medium-3 small-9 columns  large-push-2 medium-push-2">
                                            </div>
                                        
                                
                <div class="clear"></div>
                                <div id="td_Address1" runat="server" visible="false" class="large-12 medium-12 small-12">
                                    <div class="large-1 medium-2 small-3 columns">
                                        
                                                Name
                                            </div>
                                            <div class="large-3 medium-3 small-9 columns">
                                                <asp:TextBox ID="txt_name" runat="server" ></asp:TextBox>
                                            </div>
                                       
                                            <div class="large-3 medium-3 small-9 columns  large-push-2 medium-push-2">
                                                Email
                                            </div>
                                            <div class="large-3 medium-3 small-9 columns  large-push-2 medium-push-2">
                                                <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                                            </div>
                                        <div class="clear1"></div>
                                            <div class="large-1 medium-2 small-9 columns">
                                                Mobile
                                            </div>
                                            <div class="large-3 medium-3 small-9 columns">
                                                <asp:TextBox ID="txt_Mobile" runat="server"></asp:TextBox>
                                            </div>
                                       <div class="clear"></div>
                                            <div class="large-2 medium-2 small-6">
                                                <asp:Button ID="btn_Saveadd" runat="server" Text="Save" /></div>
                                              <div class="large-2 medium-2 small-6 end"><asp:Button ID="lnk_CancelAdd" runat="server" CssClass="cancelprofile"
                                                    Text="Cancel" /></div>
                                            </div>
                                            
                                    </div>
                                </div>
                           
            </div>
        </ContentTemplate>
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="button_upload" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
</asp:Content>

