<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="HotelRefundReport.aspx.vb" Inherits="SprReports_Refund_HotelRefundReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/Hotel/css/B2Bhotelengine.css") %>" rel="stylesheet" type="text/css" />
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <%--<link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/gridview-readonly-script.js")%>"></script>
    <script src="<%=ResolveUrl("~/Hotel/JS/HotelRefund.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">

        <div class="large-3 medium-3 small-12 columns">

            <%--<%--<uc1:HotelMenu runat="server" ID="Settings"></uc1:HotelMenu>--%>
        </div>
        <div class="row">
            <div class="col-md-12 text-center search-text  ">
                Search Cancelled hotel
            </div>
        </div>
        <div class="row">
            <div class="col-md-9 col-xs-12 col-md-push-1">
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" name="From" id="From" class="form-control" readonly="readonly" placeholder=" Room Name" />
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" name="To" id="To" readonly="readonly" placeholder=" To date" class="form-control" />
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <input type="text" name="Checkin" placeholder="Checkin" id="Checkin" class="form-control" />
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:DropDownList ID="Triptype" runat="server" class="form-control">
                        <asp:ListItem Text="Select trip type" Value=""></asp:ListItem>
                        <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                        <asp:ListItem Text="International" Value="International"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="form-groups col-md-3 col-xs-12">
                     <asp:TextBox ID="txt_OrderId" runat="server" placeholder=" Order ID"   class="form-control"></asp:TextBox>
                </div>
                <div class="form-groups col-md-3 col-xs-12">
                    <asp:TextBox ID="txt_bookingID" placeholder=" Booking ID" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-groups col-md-3 col-xs-12">
                     <asp:TextBox ID="txt_htlcode" placeholder="Hotel name" runat="server" class="form-control"></asp:TextBox>
                </div>
                  <div class="form-groups col-md-3 col-xs-12">
                      <asp:TextBox ID="txt_roomcode" placeholder="Room name" runat="server" class="form-control"></asp:TextBox>
                     
                </div>
              
               

             
           
            <div class="form-groups col-md-3 col-xs-12" placeholder="Status">
                    <asp:DropDownList ID="ddl_Status" runat="server"  class="form-control">
                        <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                        <asp:ListItem Text="InProcess" Value="InProcess"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    </asp:DropDownList>
                </div>

             <div class="form-groups col-md-3 col-xs-12">
                    <span id="tr_ExecID" runat="server">
                    <asp:DropDownList ID="ddl_ExecID" runat="server" class="form-control" placeholder="Exec ID">
                    </asp:DropDownList>
                         </span>      
                </div>
               
               
                <div class="form-groups col-md-3 col-xs-12">
                <span id="TDAgency" runat="server">
                <input type="text" id="txtAgencyName" name="txtAgencyName" class="form-control" onfocus="focusObj(this);"
                    onblur="blurObj(this);" placeholder="Agency Name or ID" />
                <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                     </span>
            </div>
            
               <div class="col-md-12">
                    * To get today booking, do not fill any field, only click on Search Result Button.
                </div>

                <div class="clear1"></div>

      <div class="col-md-6 col-sm-12 col-sm-push-3">
            <div style="float: left; color:red; ">
                Total Refund Amount :
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
            </div>
            <div style="float: right; margin-left: 40px; color:green; ">
                Total Hotel Cancelled :
                                <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
            </div>
        </div>
           
            </div>
            <div class="col-md-2 col-md-push-1 col-xs-12">
                
           <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" />
                  <asp:Button ID="btn_export" runat="server" CssClass="buttonfltbk" Text="Export" />
       
            </div>

        </div>           
       
    </div>
       
    <div class="large-12 medium-12 small-12 " align="center">
        <asp:GridView ID="grd_RefundReport" runat="server" AutoGenerateColumns="False" AllowPaging="True"  CssClass="table table-hover" GridLines="None" Font-Size="12px"
            PageSize="40" >
            <Columns>
                <asp:TemplateField HeaderText="Credit Node">
                    <ItemTemplate>
                        <a href='../Accounts/HotelCreditNote.aspx?OrderId=<%#Eval("CancID") %>&amp;AgentID=<%#Eval("BookedBy") %>' rel="lyteframe"
                            rev="width: 920px; height: 407px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">Invoice</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BookingID" HeaderText="BookingID" ReadOnly="true" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="BookedBy" HeaderText="Agent ID " />

                <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                <asp:BoundField DataField="NetCost" HeaderText="Cost to Agent" />
                <asp:BoundField DataField="RefundFare" HeaderText="Refund Fare" />
                <asp:BoundField DataField="CancelCharge" HeaderText="Cancel Charge" />
                <asp:BoundField DataField="ServiceCharge" HeaderText="Service Charge" />
                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                <asp:BoundField DataField="PgCharges" HeaderText="PG Charges" />
                <asp:TemplateField HeaderText="DETAIL">
                    <ItemTemplate>
                        <div class="tag">
                            <a href="#" class="gridViewToolTip">
                                <img src="<%=ResolveUrl("~/Images/view_icon.gif")%>" border="0" /></a>
                            <div id="tooltip" style="display: none;">
                                <div style="float: left;">
                                    <table width="100%" cellpadding="11" cellspacing="11" border="0">
                                        <tr>
                                            <td style="width: 110px; font-weight: bold;">Hotel Name:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("HotelName")%>  
                                            </td>
                                            <td style="width: 110px; font-weight: bold;">CheckIn Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("CheckIN")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; font-weight: bold;">Room Name:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("RoomName")%>  
                                            </td>

                                            <td style="width: 110px; font-weight: bold;">ChekOut Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("CheckOut")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; font-weight: bold;">Trip Type:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("TripType")%>  
                                            </td>
                                            <td style="width: 110px; font-weight: bold;">Booking Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("BookingDate")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; font-weight: bold;">Star Rating:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("StarRating")%> 
                                            </td>
                                            <td style="width: 110px; font-weight: bold;">Executive ID:
                                            </td>
                                            <td style="width: 166px;">
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
                                            <td style="width: 130px; font-weight: bold;">Request Remark:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("RequestRemark")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; font-weight: bold;">Request Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("RequestDate")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; font-weight: bold;">Accept Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("AcceptDate") %> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; font-weight: bold;">Updated Remark:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("UpdateRemark")%>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; font-weight: bold;">Updated Date:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("UpdateDate") %>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; font-weight: bold;">Reject Remark:
                                            </td>
                                            <td style="width: 166px;">
                                                <%#Eval("RejectRemark")%>
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
    </div>

    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getDate()) + '-' + (myDate.getMonth() + 1) + '-' + myDate.getFullYear();
        $("#From").val(currDate); $("#To").val(currDate);

        var UrlBase = '<%=ResolveUrl("~/") %>';
        var CheckindtPickerOptions = { numberOfMonths: 1, dateFormat: "dd-mm-yy", maxDate: "+1y", minDate: "-1y", showOtherMonths: true, selectOtherMonths: false };
        $("#Checkin").datepicker(CheckindtPickerOptions);

        $(function () {
            InitializeToolTip();
        });
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>

