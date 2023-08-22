<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="DailySaleReport.aspx.vb" Inherits="SprReports_Sales_DailySaleReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style>
        input[type="text"], input[type="password"], select, textarea {
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
    <style type="text/css">
        .txtBox {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }

        .txtCalander {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function validate() {
            var date = document.getElementById('txtdate')
            if (date.value == "") {
                alert('Please Select Date !')
                date.focus();
                return false;
            }
            var city = document.getElementById('txtagencycity')
            if (city.value == "") {
                alert('Please Enter Agency City!')
                city.focus();
                return false;
            }
            var AgencyName = document.getElementById('txtagencyname')
            if (AgencyName.value == "") {
                alert('Please Enter Agency Name!')
                AgencyName.focus();
                return false;
            }
            var ctcperson = document.getElementById('txtctcperson')
            if (ctcperson.value == "") {
                alert('Please Enter Contact Person!')
                ctcperson.focus();
                return false;

            }
            var ctcno = document.getElementById('txtctcno').value
            if (ctcno.length != 10) {
                alert('Please Enter 10 digit mobile Number!')
                return false;
            }
            var remark = document.getElementById('txtremark')
            if (remark.value == "") {
                alert('Please Enter Remark!')
                return false;
            }

        }
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

    </script>
    <table cellpadding="0" cellspacing="0" align="center" style="margin: auto" width="100%">
        <tr>
            <td class="Heading" align="center" colspan="5" style="padding-bottom: 5px">&nbsp;Daily Report Form</td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="15" align="center" class="tbltbl">
                    <tr>
                        <td colspan="1" width="150px" class="Textsmall">Date :
                        </td>
                        <td>
                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                style="width: 100px" />

                        </td>
                        <td class="Textsmall">Agency City :
                        </td>
                        <td>
                            <asp:TextBox ID="txtagencycity" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Textsmall">Contact Person:
                        </td>
                        <td>
                            <asp:TextBox ID="txtctcperson" runat="server"></asp:TextBox>
                        </td>
                        <td class="Textsmall">Contact Person's No:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtctcno" runat="server" onkeypress="return phone_vali();"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td valign="top" class="Textsmall">Agency Name :</td>
                        <td valign="top">
                            <input type="text" id="txtAgencyName1" name="txtAgencyName1" style="width: 200px" />

                            <td valign="middle">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="Textsmall" valign="top">Remarks:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtremark" runat="server" Height="70px"
                                TextMode="MultiLine" Width="400px" CssClass="Textsmall"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btninsert" OnClientClick="return validate();" runat="server" CssClass="button"
                                Text="INSERT" />
                        </td>
                    </tr>
            </td>
    </table>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

