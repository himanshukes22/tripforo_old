<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="HtlDiscount.aspx.vb" Inherits="SprReports_Admin_HtlDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=ResolveUrl("../../Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet" type="text/css" />
    <link href="~/CSS/main2.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Validate() {
            if (document.getElementById("<%=txtAmt.ClientID%>").value == "") {
                alert("Please Provide Markup Amount");
                document.getElementById("<%=txtAmt.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("txtAgencyName").value == "") {
                alert("Please Select A Agency Name");
                document.getElementById("txtAgencyName").focus();
                return false;
            }
            if (document.getElementById("htlCity").value == "") {
                alert("Please Select A City");
                document.getElementById("htlCity").focus();
                return false;
            }
            var S = document.getElementById("<%=ddlStar.ClientID%>").selectedIndex;
            if (S == 0) {
                alert("Please Select Star");
                document.getElementById("<%=ddlStar.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8)) {
                return false;
            }
            return true;
        }
    </script>
    <table align="left" cellspacing="10" cellpadding="10" style="background: #fff; padding-left: 272px" width="83%">
        <tr>
            <td>
                <table width="92%" class="tbltbl">
                    <tr>
                        <td style="color: #004b91; font-size: 13px; font-weight: bold">Hotel Discount
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-weight: bold; width: 83px;">Discount(%) :
                                                </td>
                                                <td height="25px" width="130px">
                                                    <asp:TextBox runat="server" ID="txtAmt" TabIndex="0" onKeyPress="return checkit(event)"></asp:TextBox>
                                                </td>

                                                <td style="font-weight: bold; width: 65px" align="right">City : 
                                                </td>
                                                <td width="130px">
                                                    <input type="text" id="htlCity" name="htlCity" defvalue="ALL" autocomplete="off" value="ALL" tabindex="1" />
                                                    <input type="hidden" id="htlcitylist" name="htlcitylist" value="" />
                                                </td>
                                                <td style="font-weight: bold; width: 65px" align="right">Star : 
                                                </td>
                                                <td height="25px" width="83px">
                                                    <asp:DropDownList runat="server" TabIndex="2" ID="ddlStar" Width="74px">
                                                        <asp:ListItem Value="0">--Select Star--</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="1.5">1.5</asp:ListItem>
                                                        <asp:ListItem Value="2.5">2.5</asp:ListItem>
                                                        <asp:ListItem Value="3.5">3.5</asp:ListItem>
                                                        <asp:ListItem Value="4.5">4.5</asp:ListItem>
                                                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="font-weight: bold">Agency Name :
                                                </td>
                                                <td colspan="2">
                                                    <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                        onblur="blurObj(this);" defvalue="ALL" autocomplete="off" value="ALL" tabindex="3" />
                                                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                                </td>
                                                <td width="209px"></td>
                                                <td style="height: 30px;" align="right">
                                                    <asp:Button ID="btnAdd" runat="server" Text="New Entry" CssClass="button" OnClientClick="return Validate()" TabIndex="4" />
                                                    <input type="hidden" name="From" id="From" />
                                                    <input type="hidden" name="To" id="To" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table align="left" style="padding-left: 310px" width="83%">
        <tr>
            <td align="center">
                <asp:GridView ID="GrdMarkup" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px">
                    <Columns>
                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update" ForeColor="#004b91" Font-Strikeout="False" Font-Overline="False" Font-Bold="true"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" ForeColor="#004b91" Font-Strikeout="False" Font-Overline="False" Font-Bold="true"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Edit" ForeColor="#004b91" Font-Strikeout="False" Font-Overline="false" Font-Bold="true"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("DisID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hotel Type">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHtlType" runat="server" Text='<%#Bind("Trip") %>' MaxLength="1" Width="74px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblHtlType" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCity" runat="server" Text='<%#Bind("City") %>' Width="92px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Star">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStar" runat="server" Text='<%# Bind("Star") %>' Width="92px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStar" runat="server" Text='<%#Eval("Star")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Discount(%)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAmt" runat="server" Text='<%# Bind("Percentage") %>' Width="92px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("Percentage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agent ID">
                            <ItemTemplate>
                                <asp:Label ID="lblAgentID" runat="server" Text='<%#Eval("Agent")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    
                </asp:GridView>
            </td>
        </tr>

    </table>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js")%>"></script>
    <script type="text/javascript">
        $(function () {
            $("#htlCity").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: UrlBase + "CitySearch.asmx/FetchCityList",
                        data: "{ 'city': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.AirportCode + "," + item.CityName + "," + item.CountryCode,
                                    value: item.CityName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    $("#htlcitylist").val(ui.item.label);
                }
            });
        });
    </script>
</asp:Content>

