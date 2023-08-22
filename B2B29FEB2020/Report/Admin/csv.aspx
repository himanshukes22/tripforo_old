<%@ Page Language="C#" AutoEventWireup="true" CodeFile="csv.aspx.cs" Inherits="csv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="<%=ResolveUrl("~/styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet"
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<input id="xyz" type ="text"  runat="server" onclick="return xyz_onclick()" />--%>
        <asp:TextBox ID="xyz" runat="server" OnTextChanged="xyz_TextChanged"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUpload" runat="server" Text="Submit" OnClick="btnUpload_Click" />
    </div>
    <div id="maindiv" runat="server">
        <asp:Panel ID="pnlNewFiles" runat="server">
        </asp:Panel>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Total no of hits :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Total no of unique hits :"></asp:Label>
                </td>
                <%-- <td><asp:Label ID="Label4" runat="server" Text="Total unique hits :"></asp:Label>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label3" runat="server" Text="Total no of source:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label4" runat="server" Text="Total no of path:"></asp:Label>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    Path
                </td>
                <td>
                    <asp:DropDownList ID="ddlpath" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="DNS" CssClass="table table-hover" GridLines="None" Font-Size="12px"  >
            
            
            <Columns>
                <asp:TemplateField HeaderText="AUTH_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="AUTH_TYPE" runat="server" Text='<%#Eval("AUTH_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AUTH_USER">
                    <ItemTemplate>
                        <asp:Label ID="lblAUTH_USER" runat="server" Text='<%#Eval("AUTH_USER") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CITY">
                    <ItemTemplate>
                        <asp:Label ID="lblCITY" runat="server" Text='<%#Eval("CITY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COUNTRY_NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblCOUNTRY_NAME" runat="server" Text='<%#Eval("COUNTRY_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COUNTRY_CODE">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRY_CODE" runat="server" Text='<%#Eval("COUNTRY_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COUNTRY_NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblCOUNTRY_NAME" runat="server" Text='<%#Eval("COUNTRY_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DNS">
                    <ItemTemplate>
                        <asp:Label ID="lblDNS" runat="server" Text='<%#Eval("DNS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DOMAIN">
                    <ItemTemplate>
                        <asp:Label ID="lblDOMAIN" runat="server" Text='<%#Eval("DOMAIN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DOMAIN_NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblDOMAIN_NAME" runat="server" Text='<%#Eval("DOMAIN_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HTTP_HOST">
                    <ItemTemplate>
                        <asp:Label ID="lblHTTP_HOST" runat="server" Text='<%#Eval("HTTP_HOST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISP_NAME">
                    <ItemTemplate>
                        <asp:Label ID="ISP_NAME" runat="server" Text='<%#Eval("ISP_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LATITUDE">
                    <ItemTemplate>
                        <asp:Label ID="LATITUDE" runat="server" Text='<%#Eval("LATITUDE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LONGITUDE">
                    <ItemTemplate>
                        <asp:Label ID="LONGITUDE" runat="server" Text='<%#Eval("LONGITUDE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PageView">
                    <HeaderTemplate>
                        <asp:Label ID="lb4header" runat="server" Text="PageView"></asp:Label>
                        <asp:DropDownList runat="server" ID="dd4FilterTypeLine">
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="PageView" runat="server" Text='<%#Eval("PageView") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Path">
                    <HeaderTemplate>
                        <asp:Label ID="lb2header" runat="server" Text="Path"></asp:Label>
                        <%--<asp:DropDownList runat="server" ID="dd2FilterTypeLine" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>--%>
                        <%--  <s:dropdownlist id="myDropDownList" selectedindex="@{pm.selectedIndex}" />--%>
                    </HeaderTemplate>
                    <%--<ItemTemplate>
                        <asp:Label ID="Path" runat="server" Text='<%#Eval("Path") %>'></asp:Label>
                    </ItemTemplate>--%>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="REGION">
                    <ItemTemplate>
                        <asp:Label ID="REGION" runat="server" Text='<%#Eval("REGION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="REQUEST_METHOD">
                    <ItemTemplate>
                        <asp:Label ID="SCRIPT_NAME" runat="server" Text='<%#Eval("REQUEST_METHOD") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SCRIPT_NAME">
                    <ItemTemplate>
                        <asp:Label ID="SCRIPT_NAME" runat="server" Text='<%#Eval("SCRIPT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISP_NAME">
                    <ItemTemplate>
                        <asp:Label ID="ISP_NAME" runat="server" Text='<%#Eval("ISP_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVER_NAME">
                    <ItemTemplate>
                        <asp:Label ID="SERVER_NAME" runat="server" Text='<%#Eval("SERVER_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVER_PORT">
                    <ItemTemplate>
                        <asp:Label ID="SERVER_PORT" runat="server" Text='<%#Eval("SERVER_PORT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SOURCE">
                    <HeaderTemplate>
                        <asp:Label ID="lb5header" runat="server" Text="SOURCE"></asp:Label>
                        <asp:DropDownList runat="server" ID="dd5FilterTypeLine">
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="SOURCE" runat="server" Text='<%#Eval("SOURCE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TIME_ZONE">
                    <ItemTemplate>
                        <asp:Label ID="TIME_ZONE" runat="server" Text='<%#Eval("TIME_ZONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label ID="Username" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VISITOR_IPADDR">
                    <HeaderTemplate>
                        <asp:Label ID="lblheader" runat="server" Text="VISITOR_IPADDR"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlFilterTypeLine">
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="VISITOR_IPADDR" runat="server" Text='<%#Eval("VISITOR_IPADDR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VISTING_TIME">
                    <ItemTemplate>
                        <asp:Label ID="VISTING_TIME" runat="server" Text='<%#Eval("VISTING_TIME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ZIP_CODE">
                    <ItemTemplate>
                        <asp:Label ID="ZIP_CODE" runat="server" Text='<%#Eval("ZIP_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
   

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
        $(function() {
            $("#xyz").datepicker({

                numberOfMonths: 1, dateFormat: "dd-mm-y", maxDate: 0, minDate: "-1y", showOtherMonths: true, selectOtherMonths: false
            });
        });
        function xyz_onclick() {

        }

    </script>

    </form>
</body>
</html>
