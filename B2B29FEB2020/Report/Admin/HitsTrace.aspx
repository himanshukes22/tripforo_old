<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="true"
    CodeFile="HitsTrace.aspx.cs" Inherits="SprReports_HitsTrace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/TableGrid.css" rel="stylesheet" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
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

    <script type="text/javascript">

        $(function () {
            $("#ctl00_ContentPlaceHolder1_From").datepicker({

                numberOfMonths: 2, dateFormat: "dd-mm-y", maxDate: "+0d", showbuttonpanel: true, mindate: 0, changemonth: true, changeyear: true, yearrange: '1970:2010'
            })
        });
        function validateCalender() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_From').value == "") {
                alert("Please select Date.");
                return false;
            }
            else {
                document.getElementById('Img2').style.display = "";
                return true;
            }


        }
    </script>

    <table cellspacing="10" cellpadding="0" border="0" align="left">
        <tr>
            <td width="2%"></td>
            <td>
                <table cellspacing="10" cellpadding="3" align="center" style="background: #fff;" class="tbltbl">
                    <tr>
                        <td align="center" style="padding: 5px 2px 5px 2px; color: #004b91; font-size: 14px; font-weight: bold; font-family: arial, Helvetica, sans-serif; background-color: #CCCCCC;">HITS DETAILS (2ND SERVER)
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 2px 10px 2px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="90" style="font-weight: bold" height="25">Date
                                    </td>
                                    <td width="130">
                                        <input type="text" name="From" id="From" class="txtCalander" readonly="readonly"
                                            runat="server" style="width: 100px" />
                                    </td>
                                    <td width="90" style="font-weight: bold">Time
                                    </td>
                                    <td width="120px">
                                        <asp:DropDownList ID="ddlTime" runat="server" Style="width: 100px;">
                                            <asp:ListItem Value="Today">Today</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_Details" runat="server" Text="Details" CssClass="button" OnClientClick="return validateCalender();"
                                            OnClick="btn_Details_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <img id="Img2" src="<%= ResolveUrl("~/Rail/images/loading1.gif") %>" style="display: none" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="tblresult" visible="false" runat="server" cellpadding="0"
        cellspacing="0" style="display: table; margin: auto" align="center">
        <tr>
            <td width="2%"></td>
            <td width="25%" valign="top">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr style="font-size: 16px; color: #FFFFFF; background-color: #CC0066; height: 25px; font-family: Arial, Helvetica, sans-serif;">
                        <%--<th style="padding-left: 50px">
                TOTAL HITS
            </th>--%>
                        <td colspan="3" style="padding-left: 50px; color: #FFFFFF; background-color: #CC0066; height: 25px; font-family: Arial, Helvetica, sans-serif; font-weight: bold;">TOTAL HITS&nbsp;&nbsp; :: &nbsp;&nbsp;
                            <asp:Label ID="lblTotalHits" runat="server" Font-Size="16" />
                        </td>
                    </tr>
                    <%--<tr>
            <td colspan="3" style="padding: 5px 20px 5px 20px;">
           <table cellspacing="0" cellpadding="10" width="100%" align="center">
            <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px;
            font-weight: bold;">
            <td align="center">
                Search
            </td>
            <td align="center">
                Result
            </td>
            <td align="center">
                Pax Details
            </td>
            <td align="center">
                Book
            </td>
        </tr>
            </table>
            </td>
            </tr>--%>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">DOMESTIC FLIGHT</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Book
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblDomFltSearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblDomFltResult" runat="server" />--%>
                                            <asp:LinkButton ID="lblDomFltResult" runat="server" OnClick="lblDomFltResult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblDomFltPax" runat="server" />--%>
                                            <asp:LinkButton ID="lblDomFltPax" runat="server" OnClick="lblDomFltPax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblDomFltBook" runat="server" />--%>
                                            <asp:LinkButton ID="lblDomFltBook" runat="server" OnClick="lblDomFltBook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">INTERNATIONAL FLIGHT</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <%--           <tr>
            <th colspan="4" class="text1">
                InterNational Flight
            </th>
        </tr>--%>
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Book
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblIntFltSearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblIntFltResult" runat="server" />--%>
                                            <asp:LinkButton ID="lblIntFltResult" runat="server" OnClick="lblIntFltResult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblIntFltPax" runat="server" />--%>
                                            <asp:LinkButton ID="lblIntFltPax" runat="server" OnClick="lblIntFltPax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblIntFltBook" runat="server" />--%>
                                            <asp:LinkButton ID="lblIntFltBook" runat="server" OnClick="lblIntFltBook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">HOTEL</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Book
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblHtlSearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblHtlResult" runat="server" />--%>
                                            <asp:LinkButton ID="lblHtlResult" runat="server" OnClick="lblHtlResult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblHtlPax" runat="server" />--%>
                                            <asp:LinkButton ID="lblHtlPax" runat="server" OnClick="lblHtlPax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblHtlBook" runat="server" />--%>
                                            <asp:LinkButton ID="lblHtlBook" runat="server" OnClick="lblHtlBook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">BUS</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Book
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblBusSearch" runat="server" />

                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblBusResult" runat="server" />--%>
                                            <asp:LinkButton ID="lblBusResult" runat="server" OnClick="lblBusResult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblBusPax" runat="server" />--%>
                                            <asp:LinkButton ID="lblBusPax" runat="server" OnClick="lblBusPax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lblBusBook" runat="server" />--%>
                                            <asp:LinkButton ID="lblBusBook" runat="server" OnClick="lblBusBook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">UTILITY</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td></td>
                                        <td align="center" colspan="2">Recharge
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblUtlSearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblUtlResult" runat="server" />
                                        </td>
                                        <td></td>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="lblUtlRecharge" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">SIGHTSEEING</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Book
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_sssearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_ssresult" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_ssresult" runat="server" OnClick="lbl_ssresult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_sspax" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_sspax" runat="server" OnClick="lbl_sspax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_ssbook" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_ssbook" runat="server" OnClick="lbl_ssbook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset style="padding: 5px 10px 5px 10px;">
                                <legend style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold">PACKAGES</legend>
                                <table cellspacing="0" cellpadding="10" width="100%" align="center">
                                    <tr style="font-size: 12px; color: #000; background-color: #F5F5F5; height: 25px; font-weight: bold;">
                                        <td align="center">Search
                                        </td>
                                        <td align="center">Result
                                        </td>
                                        <td align="center">Pax Details
                                        </td>
                                        <td align="center">Query Posted
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_pkgsearch" runat="server" />
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_pkgresult" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_pkgresult" runat="server" OnClick="lbl_pkgresult_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_pkgpax" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_pkgpax" runat="server" OnClick="lbl_pkgpax_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <%--<asp:Label ID="lbl_pkgbook" runat="server" />--%>
                                            <asp:LinkButton ID="lbl_pkgbook" runat="server" OnClick="lbl_pkgbook_Click" Font-Bold="True" Font-Underline="True"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="3%"></td>
            <td width="70%" valign="top" id="td_agentid" runat="server">
                <%--<asp:GridView ID="GridView1" runat="server" Width="20%" AutoGenerateColumns="false"
                    PageSize="1000" CssClass="mGrid" Height="20px">
                    <Columns>
                        <asp:TemplateField HeaderText="AGENTID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_COUNTER" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   
                </asp:GridView>--%>
                <table>
                    <tr>
                        <td id="td_module" runat="server" style="padding-bottom: 10px; font-family: arial, Helvetica, sans-serif; font-size: 15px; font-weight: bold; color: #000000"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                            <asp:DataList ID="DataList1" runat="server" RepeatColumns="10">
                                <%--<HeaderTemplate>
                        Customer Details</HeaderTemplate>--%>
                                <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" Height="20px" Font-Size="12px" />

                                <ItemTemplate>
                                    <a href='Update_Agent.aspx?AgentID=<%#Eval("USERNAME")%>' rel="lyteframe" rev="width: 900px; height: 400px; overflow:hidden;"
                                        target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                                        <asp:Label ID="lbl_agentid" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label></a>

                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

</asp:Content>
