<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="RequestAccount.aspx.vb" Inherits="SprReports_OLSeries_RequestAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="CSS/Series.css" rel="stylesheet" type="text/css" />
 <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
        <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="JS/JScript.js" type="text/javascript"></script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
        <style>
        input[type="text"], input[type="password"], select,textarea
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
<div>
              <table  cellpadding="0" cellspacing="0" align="center" style="margin: auto" width="100%">
            <tr>
                <td align="center" class="Heading" style="padding-bottom: 5px">
                    Post Request
                </td>
            </tr>
            <tr>
                <td align="center">
                   
                        
                        <table cellpadding="0" cellspacing="10" width="600px" align="center" class="tbltbl">
                            <tr>
                                <td align="left" class="text1">
                                    Agency Name:
                                </td>
                                <td align="left">
                                    <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                </td>
                                <td align="left" class="text1">
                                    Amount:
                                </td>
                                <td align="left">
                                    <input type="text" id="txtamount" name="txtamount" value="" 
                                        style="width: 80px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="text1">
                                    Remark:
                                </td>
                                <td align="left" colspan="3">
                                    <textarea id="txtremark" name="txtremark" class="multitxt" rows="2" 
                                        style="width: 400px"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" align="right">
                                    &nbsp;</td>
                                <td align="left" colspan="3">
                                    <asp:Button ID="btn_post" runat="server" Text="POST" CssClass="button" 
                                        OnClientClick="return ValidateACC()" />
                                </td>
                            </tr>
                        </table>
                   
                </td>
            </tr>
        </table>
    </div>
     <script type="text/javascript">
        
         var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

