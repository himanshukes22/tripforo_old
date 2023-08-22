<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HtlRefundReport.aspx.vb" Inherits="SprReports_Refund_HtlRefundReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="<%=ResolveUrl("~/Hotel/css/HotelStyleSheet.css") %>" rel="stylesheet" type="text/css" />
 <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
 <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>  
 <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/gridview-readonly-script.js")%>"></script>
<table style="width: 100%; margin: 0 auto;" align="center">
    <tr>
        <td>
            <table cellspacing="4" cellpadding="10" align="center" class="tbltbl" width="740px" style="background: #fff;">
            <tr>
                <td style="color: #004b91; font-size: 13px; font-weight: bold">
                    Search Cancelled hotel
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:74px; font-weight:bold;">
                                From date
                            </td>
                            <td width="128px">
                                <input type="text" name="From" id="From" readonly="readonly" class="txtCalander"   />
                            </td>
                            <td style="width:74px; font-weight:bold;">
                                To date
                            </td>
                            <td width="128px">
                                <input type="text" name="To" id="To" readonly="readonly" class="txtCalander"    />
                            </td>
                            
                            <td style="width:74px; font-weight:bold;">
                                Trip type
                            </td>
                            <td width="128px">
                                <asp:DropDownList ID="Triptype" runat="server" class="CheckoutDDL" Width="110px">
                                    <asp:ListItem Text="Select trip type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                                    <asp:ListItem Text="International" Value="International"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:4px;"><td></td></tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="left">
                        <tr>
                            <td style="width:74px; font-weight:bold;">
                                Order ID
                            </td>
                            <td width="128px">
                                <asp:TextBox ID="txt_OrderId" runat="server" class="txtBox" ></asp:TextBox>
                            </td>
                            <td style="width:74px; font-weight:bold;">
                                Booking ID
                            </td>
                            <td width="128px">
                                <asp:TextBox ID="txt_bookingID" runat="server" class="txtBox"></asp:TextBox>
                            </td>
                            <td style="width:74px; font-weight:bold;">
                                Hotel name
                            </td>
                            <td width="128px">
                                <asp:TextBox ID="txt_htlcode" runat="server" class="txtBox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:4px;"><td></td></tr>
            <tr id="TDAgency" runat="server">
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td  width="85px" style="font-weight:bold;">
                                Agency Name
                            </td>
                            <td width="200px">
                                <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 182px" onfocus="focusObj(this);"
                                    onblur="blurObj(this);" value="Agency Name or ID" />
                                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                            </td>
                            <td id="tr_ExecID" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" style="font-weight: bold" width="40px">
                                            Exec ID
                                        </td>
                                        <td align="left" width="110px">
                                            <asp:DropDownList ID="ddl_ExecID" runat="server" Width="110px" class="CheckoutDDL">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="font-weight: bold" width="65px">
                                        Status
                                        </td>
                                        <td align="left" width="110px">
                                            <asp:DropDownList ID="ddl_Status" runat="server" Width="110px" class="CheckoutDDL">
                                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                                <asp:ListItem Text="InProcess" Value="InProcess"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="font-size: 11px; line-height: 20px; text-align: justify; color: Red;">
                                * To get today booking, do not fill any field, only click on Search Result Button.
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" />
                                 &nbsp;&nbsp;<asp:Button ID="btn_export" runat="server" CssClass="buttonfltbk" Text="Export" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
            <table cellspacing="4" cellpadding="0" border="0" align="center" class="tbltbl">
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; color: #004b91; height: 20px; padding-right: 15px;"
                            align="left">
                            <div style="float: left;">
                                Total Refund Amount :
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label></div>
                            <div style="float: right; margin-left: 40px;">
                                Total Hotel Cancelled :
                                <asp:Label ID="lbl_counttkt" runat="server"></asp:Label></div>
                        </td>
                    </tr>
                </table>
        </td>
         </tr>
        <tr>
        <td align="center"> 
<table cellspacing="0" cellpadding="0" border="0" style="background: #fff; width: 1100px;" >
<tr>
<td align="center">
    <asp:GridView ID="grd_RefundReport" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        PageSize="40" CssClass="table table-hover" GridLines="None" Font-Size="12px">
    <Columns>
        <asp:TemplateField HeaderText="Credit Node">
            <ItemTemplate>
                <a href='../Accounts/HotelCreditNote.aspx?OrderId=<%#Eval("RefundID") %>&amp;AgentID=<%#Eval("AgentId") %>' rel="lyteframe"
                        rev="width: 920px; height: 407px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif;
                        font-size: 12px; font-weight: bold; color: #004b91">Invoice</a>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="BookingID" HeaderText="BookingID" ReadOnly="true" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="AgentID" HeaderText="Agent ID " />
            <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name" />
            <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
            <asp:BoundField DataField="NetCost" HeaderText="Net Cost" />
            <asp:BoundField DataField="RefundFare" HeaderText="Refund Fare" />
            <asp:BoundField DataField="CancelCharge" HeaderText="Cancel Charge" />
            <asp:BoundField DataField="ServiceCharge" HeaderText="Service Charge" />
            <asp:TemplateField HeaderText="DETAIL">
                <ItemTemplate>
                    <div class="tag">
                        <a href="#" class="gridViewToolTip">
                            <img src="<%=ResolveUrl("~/Images/view_icon.gif")%>" border="0" /></a>
                        <div id="tooltip" style="display: none;">
                            <div style="float: left;">
                                <table width="100%" cellpadding="11" cellspacing="11" border="0">
                                 <tr>
                                        <td style="width:110px; font-weight:bold;">
                                            Hotel Name:
                                        </td>
                                        <td style="width:166px;">
                                            <%#Eval("HtlName")%>  
                                        </td>
                                        <td style="width:110px; font-weight:bold;">
                                            CheckIn Date:
                                        </td>
                                        <td style="width:166px;">
                                            <%#Eval("CheckIN")%>  
                                        </td>
                                    </tr>
                                    <tr>
                                         <td style="width:110px; font-weight:bold;">
                                            Room Name:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("RoomName")%>  
                                        </td>
                                       
                                         <td style="width:110px; font-weight:bold;">
                                            ChekOut Date:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("CheckOut")%>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:110px; font-weight:bold;">
                                            Hotel Type:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("HtlType")%>  
                                        </td>
                                        <td style="width:110px; font-weight:bold;">
                                            Booking Date:
                                        </td>
                                        <td style="width:166px;">
                                            <%#Eval("BookingDate")%>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:110px; font-weight:bold;">
                                            Star Rating:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("StarRating")%> 
                                        </td>
                                        <td style="width:110px; font-weight:bold;">
                                         Executive ID:
                                        </td>
                                        <td style="width:166px;">
                                            <%#Eval("ExecutiveID")%>  
                                        </td>
                                    </tr>    
                                </table>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <div class="tag">
                        <a href="#" class="gridViewToolTip">
                            <img src="<%=ResolveUrl("~/Images/view_icon.gif")%>" border="0" /></a>
                        <div id="tooltip" style="display: none;">
                            <div style="float: left;">
                                <table width="100%" cellpadding="11" cellspacing="11" border="0">
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Request Remark:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("RegardingCancel")%>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Request Date:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("SubmitDate")%>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Accept Date:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("AcceptDate") %> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Updated Remark:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("UpdateDate")%>  
                                        </td>
                                    </tr>                                                                      
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Updated Date:
                                        </td>
                                        <td style="width:166px;">
                                             <%#Eval("UpdateDate") %>  
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td style="width:130px; font-weight:bold;">
                                            Reject Remark:
                                        </td>
                                        <td style="width:166px;">
                                            <%#Eval("RejectComment")%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>              
    </Columns>
    </asp:GridView>
</td>
</tr>
</table>
</td>
         </tr>
         </table>
<script type="text/javascript">
   var UrlBase = '<%=ResolveUrl("~/") %>';
   $(function() {
       InitializeToolTip();
   });
</script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

