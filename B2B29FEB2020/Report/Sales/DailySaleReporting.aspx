<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="DailySaleReporting.aspx.vb" Inherits="SprReports_Sales_DailySaleReporting" %>

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

    <table cellpadding="0" cellspacing="15" align="center" class="tbltbl">
        <tr>
            <td align="left" colspan="4" class="Heading"
                style="padding-bottom: 5px; font-size: 12px;">Search Daily Report
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td width="90px" class="Textsmall">From
                        </td>
                        <td>
                            <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                style="width: 100px" />

                        </td>
                        <td style="padding-left: 10px" width="60px" class="Textsmall">To
                        </td>
                        <td>
                            <input type="text" name="To" id="To" class="txtCalander" readonly="readonly" style="width: 100px" />


                        </td>
                    </tr>
                </table>


            </td>

        </tr>
        <tr>
            <td id="td_salesref" runat="server">
                <table width="100%">
                    <tr>
                        <td width="90px" class="Textsmall">Sales Ref 
                        </td>
                        <td>
                            <asp:DropDownList ID="Sales_DDL" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>

            <td align="left" colspan="3">
                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="button " />
            </td>
        </tr>
    </table>
    </br>
    <div align="center">
        <table width="1000px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gvDSL" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px">

                        <Columns>
                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblcounter" runat="server" Text='<%#Eval("Counter")%>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Agency City">
                                <ItemTemplate>
                                    <asp:Label ID="lblagencycity" runat="server" Text='<%#Eval("Agency_City")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtagencycity" runat="server" Text='<%#Eval("Agency_City")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agency Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agenct_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtagencyname" runat="server" Text='<%#Eval("Agenct_Name")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person">
                                <ItemTemplate>
                                    <asp:Label ID="lblctcperson" runat="server" Text='<%#Eval("Ctc_Person")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtctcperson" runat="server" Text='<%#Eval("Ctc_Person")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblctcpersonno" runat="server" Text='<%#Eval("Ctc_PersonNo")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtctcpersonno" runat="server" Text='<%#Eval("Ctc_PersonNo")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Inputdate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblremark" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtremark" runat="server" Text='<%#Eval("Remarks")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales User Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblsalesUid" runat="server" Text='<%#Eval("User_id")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update/Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="small" CommandName="Edit" />
                                    <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="small " CommandName="delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="update" CssClass="small " />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CommandName="cancel" CssClass="small " />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
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

