<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="SalesExecRegistration.aspx.vb" Inherits="Reports_Admin_SalesExecRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style>
        input[type="text"], input[type="password"], select
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
    </style>
     <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
 <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <script src="../../JS/SalesJScript.js" type="text/javascript"></script>
  
        <table cellspacing="10" cellpadding="10" border="0" align="center" class="tbltbl" width="800">
            
            <tr>
                
                <td bgcolor="#20313f" height="25px">
                    <h2>
                        Sales Executive Registration</h2>
                </td>
               
            </tr>
            <tr>
                
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 90%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="4" align="center">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" height="20px" align="left" class="SubHeading">
                                            <asp:Label ID="lbl_msg" runat="server" Font-Size="15px" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" height="40px" align="left" class="SubHeading">
                                            Personal Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="Text">
                                            First Name :*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_Fname" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Last Name :*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_LName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Location :*
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="txt_Loc" runat="server"></asp:TextBox>--%><asp:TextBox ID="txt_Loc" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="SubHeading">
                                            Contact Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Mobile No. :*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_Mno" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Email ID :*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_EmailID" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" height="40px" class="SubHeading">
                                            Authentication Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Password :*
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txt_Pwd" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Text" height="35px">
                                            Confirm Password :*
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txt_CPwd" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <%--<asp:Button ID="btn_Submit" runat="server" Text="Submit" height="35px" 
            Width="100px" onclick="btn_Submit_Click" OnClientClick="return Sales()" />--%>
                                            <asp:Button ID="btn_Submit" runat="server" Text="Submit" Height="35px" Width="100px"
                                                OnClientClick="return Validate()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                </td>
               
            </tr>
            
        </table>
    
</asp:Content>

