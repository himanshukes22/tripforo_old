<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="TotalSalesReport.aspx.vb" Inherits="SprReports_Sales_TotalSalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style>
        input[type="text"], input[type="password"], select {
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
    <%--<script type="text/javascript">
          function gt() {
              document.getElementById("td_GroupType").style.display = "none";
              document.getElementById("td_GroupType1").style.display = "none";
          }
      </script>--%>
    <%--<script type="text/javascript">


        function Show(obj) {

            if (obj.checked) {
                document.getElementById("td_GroupType").style.display = "block";
                document.getElementById("td_GroupType1").style.display = "block";
                document.getElementById("td_SalesID").style.display = "none";
                document.getElementById("td_SalesID1").style.display = "none";
                document.getElementById("SalesType").style.display = "block";
                document.getElementById("SalesType1").style.display = "none";
            }
        }
        function Hide(obj) {
            if (obj.checked) {
                document.getElementById("td_GroupType").style.display = "none";
                document.getElementById("td_GroupType1").style.display = "none";
                document.getElementById("td_SalesID").style.display = "block";
                document.getElementById("td_SalesID1").style.display = "block";
                document.getElementById("SalesType").style.display = "none";
                document.getElementById("SalesType1").style.display = "block";
            }


        }
       
   
    </script>--%>

    <table cellspacing="2" cellpadding="0" border="0" align="center" class="tbltbl" width="600px">
        <tr>
            <td>
                <table cellspacing="10" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <table cellspacing="10" cellpadding="0" border="0">
                                <tr>
                                    <td style="font-weight: bold" width="100px">From
                                    </td>
                                    <td>
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                            style="width: 100px" />
                                    </td>
                                    <td width="80px" style="font-weight: bold">To</td>
                                    <td>
                                        <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold" width="100px">Agency Name</td>
                                    <td>
                                        <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 130px" onfocus="focusObj(this);"
                                            onblur="blurObj(this);"
                                            defvalue="Agency Name or ID" autocomplete="off"
                                            value="Agency Name or ID" />
                                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                    </td>


                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<div  style="white-space:nowrap">--%>

                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr id="td_salesref" runat="server">
                                                            <td style="font-weight: bold;" align="center" width="300px">
                                                                <asp:RadioButtonList
                                                                    ID="RBL_RTYPE" runat="server" AutoPostBack="True"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="SALES">Sales Id</asp:ListItem>
                                                                    <asp:ListItem Value="AGENTTYPE">Agent Type</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td id="td_SalesID" runat="server" visible="false">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="font-weight: bold;" width="140px">Sales Id&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="Sales_DDL" runat="server"></asp:DropDownList>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td id="td_GroupType" runat="server" visible="false">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>

                                                                        <td style="font-weight: bold;" width="140px">Group Type</td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddl_type" runat="server"></asp:DropDownList></td>

                                                                    </tr>
                                                                </table>
                                                            </td>



                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4">
                                                                <asp:Button ID="btn_result" runat="server" Text="Sale Result" CssClass="button" />
                                                                &nbsp;<asp:Button ID="btn_totsale" runat="server" Text="Total Sale" CssClass="button" />
                                                                &nbsp;<asp:Button ID="btn_Type" runat="server" Text="Sale By Type" CssClass="button" />
                                                                &nbsp;<div style='display: none;'>
                                                                    <asp:Button
                                                                        ID="btn_mail" runat="server" Text="Mail" CssClass="button" Visible="false" OnClientClick="return confirm ('Are you sure ?')" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--</div>--%>
                                                </td>


                                            </tr>
                                        </table>
                                        <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
                                    </td>
                                </tr>




                                <%--<tr>
 <td>
 For total sales and mail
 </td>
 </tr>--%>
                            </table>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>

    </table>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" style="padding-top: 7px">
                <asp:Button ID="btn_resultexportexcel" runat="server" Text="Export To Excel"
                    Visible="false" CssClass="newbutton_2" Font-Bold="True" Height="30px"
                    Width="120px" />&nbsp;<asp:Button ID="btn_resultexportword" runat="server"
                        Text="Export To Word" Visible="false" CssClass="newbutton_2" Font-Bold="True"
                        Height="30px" Width="120px" />
                <asp:Button ID="btn_totsaleexportexcel" runat="server" Text="Export To Excel"
                    Visible="false" CssClass="newbutton_2" Font-Bold="True" Height="30px"
                    Width="120px" />&nbsp;<asp:Button ID="btn_totsaleexportword" runat="server"
                        Text="Export To Word" Visible="false" CssClass="newbutton_2" Font-Bold="True"
                        Height="30px" Width="120px" />

            </td>
        </tr>
        <tr>
            <td style="padding: 5px;" align="center">
                <asp:Label ID="lblsale" runat="server"></asp:Label>
                <div style="display: none">
                    <asp:Label ID="lblmail" runat="server"></asp:Label>
                </div>
                <div id="totss" runat="server" style="text-align: center">
                    <asp:Label ID="lbl_totsale" runat="server"></asp:Label>
                </div>

            </td>
        </tr>
        <%--<tr>
<td>

</td>
</tr>--%>
    </table>


    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

