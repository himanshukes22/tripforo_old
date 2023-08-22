<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadAmount.aspx.vb" Inherits="SprReports_Distr_UploadAmount" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Credit and Debit</title>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/JScript.js" type="text/javascript"></script>

    <%--<style>
        input[type="text"], input[type="password"], select, textarea
        {
            border: 1px solid #004b91;
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

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
                return true;
            }
            else {

                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function validateAmt() {
            var val = document.getElementById("txt_crd_val").value;
            var r = document.getElementById("txt_rm").value;
            if (r == "") {
                alert("Please Enter Remark."); return false;
            }
            if (val == "") {
                alert("Please Enter Amount."); return false;

            }
            //if (val > 500000) {
            //    alert("Maximum update amount is 5 lacs."); return false;
            //}

            if (confirm("Are you sure??"))
                return true;
            return false;

        }
        function checkCreditTrasac(count, record) {

            var val = document.getElementById("txt_crd_val").value;
            var r = document.getElementById("txt_rm").value;
            if (r == "") {
                alert("Please Enter Remark."); return false;
            }
            if (val == "") {
                alert("Please Enter Amount."); return false;

            }
            //if (val > 500000) {
            //    alert("Maximum update amount is 5 lacs."); return false;
            //}

            if (confirm("Are you sure??")) {



                if (count > 0) {

                    if (confirm("Agent already on credit. Are you sure?")) {

                        if (record != "") {
                            if (confirm(record))

                                return true;
                            else
                                return false;

                        }

                    }
                    else {
                        return false;
                    }
                }
                else {

                    if (record != "") {
                        if (confirm(record))

                            return true;
                        else
                            return false;

                    }
                }
            }
            else {
                return false;
            }



        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tbl_Upload" runat="server">
                    <tr>
                        <td>
                            <div style="padding-top: 5px; padding-bottom: 5px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-right: 17px">
                                            <fieldset style="border: thin solid #004b91; padding-left: 10px">
                                                <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;">
                                                    Agency Infromation</legend>
                                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="h2" height="25" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;" width="120">
                                                                        User ID :
                                                                    </td>
                                                                    <td id="td_AgentID" runat="server" width="130" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;" width="100">
                                                                        Agency Name :
                                                                    </td>
                                                                    <td id="td_AgencyName" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="h2" height="25" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;" width="120">
                                                                       Agency ID :
                                                                    </td>
                                                                    <td id="TdAgencyId" runat="server" width="130" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                        &nbsp;
                                                                    </td>

                                                                    <td class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;" width="100">
                                                                        Type :
                                                                    </td>

                                                                    <td  class="h2"  runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" id="td_Address" runat="server" height="10" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" id="td_Address1" runat="server" height="10" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="h2" height="25" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;">
                                                                        Available Balance :
                                                                    </td>
                                                                    <td id="td_Aval_Bal" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                    <td class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;">
                                                                        Pan No :
                                                                    </td>
                                                                    <td id="td_pan" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="h2" height="25" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;">
                                                                        Mobile No :
                                                                    </td>
                                                                    <td id="td_Mobile" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                    <td class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;">
                                                                        Email :
                                                                    </td>
                                                                    <td id="td_Email" runat="server" style="font-family: arial, Helvetica, sans-serif;
                                                                        color: #000000; font-size: 12px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;"
                                                                        align="left" class="h2" width="100">
                                                                        Remark :
                                                                    </td>
                                                                    <td align="left" width="320px">
                                                                        <asp:TextBox ID="txt_rm" runat="server" TextMode="MultiLine" Rows="3" Columns="25"
                                                                            Width="300px" BackColor="#F0F0F0"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold;
                                                                        color: #000000;" width="90px">
                                                                      
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Yatra" runat="server" Width="100px" style="display:none;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;"
                                                                        align="left" width="100">
                                                                        Amount :
                                                                    </td>
                                                                    <td align="left" width="125" style="padding-top: 17px">
                                                                        <asp:TextBox ID="txt_crd_val" runat="server" onkeypress="return isNumberKey(event)"
                                                                            Width="120px" BackColor="#F0F0F0"></asp:TextBox>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;
                                                                        padding-left: 10px;" width="90px">
                                                                        Upload Type:
                                                                    </td>
                                                                    <td width="105px">
                                                                        <asp:DropDownList ID="ddl_uploadtype" runat="server" Width="100px">
                                                                            <asp:ListItem Selected="True" Value="CA">Cash</asp:ListItem>
                                                                            <%--<asp:ListItem Value="CR">Credit</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td id="td_spl" runat="server" width="160px">
                                                                        <table>
                                                                            <tr>
                                                                                <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;
                                                                                    padding-left: 10px; padding-right: 5px;">
                                                                                    Spl. Upload
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChkSpl" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td width="60" align="center">
                                                                        <asp:ImageButton ID="plus" runat="server" ImageUrl="../../Images/btn_plus.gif" />
                                                                        <asp:ImageButton ID="minus_Series" runat="server" ImageUrl="../../Images/btn_minus.gif"
                                                                            OnClientClick="return validateAmt();" Visible="False" />
                                                                    </td>
                                                                    <td valign="middle">
                                                                        <asp:ImageButton ID="minus" runat="server" ImageUrl="../../Images/btn_minus.gif"
                                                                            OnClientClick="return validateAmt();" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddAgentAgencyId" runat="server" />
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td id="td_msg" runat="server" visible="false" align="center" height="300px" class="SubHeading"
                            valign="middle">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                    <img alt="loading" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                    <br />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    </form>
</body>
</html>
