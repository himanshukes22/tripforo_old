<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="Reports_Sales_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
 <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/SalesJScript.js" type="text/javascript"></script>

<div class="divcls">
        <table cellpadding="0" cellspacing="0" align="center" style="background: #fff;">
            <tr>
                <td height="10" valign="top" style="width: 10px">
                    <img src="../../images/box-tpr.jpg" width="10" height="10" />
                </td>
                <td style="background: url(../../images/box-tp.jpg) repeat-x left bottom;" height="10">
                </td>
                <td valign="top">
                    <img src="../../images/box-tpl.jpg" width="10" height="10" />
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px; background: url(../../images/boxl.jpg) repeat-y left bottom;">
                </td>
                <td bgcolor="#20313f" height="25px">
                    <h2>
                        Profile Details</h2>
                </td>
                <td style="width: 10px; height: 10px; background: url(../../images/boxr.jpg) repeat-y left bottom;">
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px; background: url(../../images/boxl.jpg) repeat-y left bottom;">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" border>
                        <tr>
                            <td style="width: 15%">
                            </td>
                            <td style="width: 70%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="2" id="td_msg" runat="server" align="center" class="SubHeading">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig" width="280px">
                                            First Name :
                                        </td>
                                        <td id="td_Fname" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            Last Name :
                                        </td>
                                        <td id="td_Lname" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            Location :
                                        </td>
                                        <td id="td_Location" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            Mobile No :
                                        </td>
                                        <td class="Text">
                                            <asp:TextBox ID="txt_MNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            EmailID :
                                        </td>
                                        <td id="td_Email" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            Password :
                                        </td>
                                        <td class="Text">
                                            <asp:TextBox ID="txt_Pwd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px" class="TextBig">
                                            Registration Date :
                                        </td>
                                        <td id="td_RegDate" runat="server" class="Text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="45px" class="TextBig" id="ID" runat="server" visible="false">
                                        </td>
                                        <td align="right" height="40px">
                                            <asp:Button ID="btn_Update" runat="server" Text="Update Profile" OnClick="btn_Update_Click"
                                                OnClientClick="return Confirm()" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 15%">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px; height: 10px; background: url(../../images/boxr.jpg) repeat-y left bottom;">
                </td>
            </tr>
            <tr>
                <td height="10" valign="top" style="width: 10px">
                    <img src="../../images/box-bl.jpg" width="10" height="10" />
                </td>
                <td style="background: url(../../images/box-bottom.jpg) repeat-x left bottom;" height="10">
                </td>
                <td valign="top">
                    <img src="../../images/box-br.jpg" width="10" height="10" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

