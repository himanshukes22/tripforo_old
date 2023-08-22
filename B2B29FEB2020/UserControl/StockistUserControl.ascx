<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StockistUserControl.ascx.vb"
    Inherits="UserControl_StockistUserControl" %>


<link href="<%=ResolveUrl("~/CSS/main2.css") %>" rel="stylesheet" type="text/css" />
<table cellspacing="10" cellpadding="0" border="0" align="center" style="margin: auto">
    <tr>
        <td style="color: #000000; padding-bottom: 2px; padding-top: 2px; font-size: 20px; font-weight: bold"
            align="center">LIST OF STOCKIST
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                PageSize="50" CssClass="GridViewStyle" Font-Size="Large">
                <Columns>
                    <asp:TemplateField HeaderText="AGENCY NAME">
                        <ItemTemplate>
                            <asp:Label ID="name" runat="server" Text='<%# Eval("Agency_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="STATE">
                        <ItemTemplate>
                            <asp:Label ID="city" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="RowStyle" />
                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
</table>


<%--<script src="<%=ResolveUrl("~/JS/jquery.js")%>" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/Scripts/jquery-1.7.1.min.js")%>" type="text/javascript"></script>--%>

<asp:HiddenField ID="hdnAgentState" runat="server" Value="" />

<script type="text/javascript">
    $(document).ready(function () {        
        if ($.trim($("#ctl00_ContentPlaceHolder1_stockist1_hdnAgentState").val()).toLowerCase() == "bihar" && $("#ctl00_ContentPlaceHolder1_stockist1_hdnAgentState").val() != "" && $("#ctl00_ContentPlaceHolder1_stockist1_GridView1").html() != "" && $("#ctl00_ContentPlaceHolder1_stockist1_GridView1").html() != null && $("#ctl00_ContentPlaceHolder1_stockist1_GridView1").html() != undefined) {
            $("#divStockistList").fadeIn();
        }
        $("#imgCloseCHSch").click(function () {
            $("#divStockistList").fadeOut("slow");
        });
    });

</script>
