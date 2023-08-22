<%@ Page Language="VB" MasterPageFile="~/MasterForHome.master" enableEventValidation="false" AutoEventWireup="false" CodeFile="HtlBookingRpt.aspx.vb" Inherits="SprReports_Hotel_HtlBookingRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=ResolveUrl("~/Hotel/css/B2Bhotelengine.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Hotel/JS/HotelRefund.js")%>" type="text/javascript"></script>
    <div class="mtop80"></div>  
        <div class="row">
            <div class="col-md-12 text-center search-text  ">
                Search hotel Booking
            </div>           
        </div>   
       <div class="col-md-9 col-xs-12 col-md-push-1">
            <form class="form-horizontal">
                <div class="form-groups col-md-2 col-xs-12">
                    <input placeholder="From Date " class="form-control" type="text" name="From" id="From" readonly="readonly" />
                    <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                    <input placeholder="To Date " class="form-control" type="text" name="To" id="To" readonly="readonly" />
                    <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                    <input placeholder="Checkin" class="form-control" type="text" name="Checkin" id="Checkin" />
                    <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                    <asp:TextBox ID="txt_OrderId" placeholder="Order ID" runat="server" class="form-control"></asp:TextBox>
                    <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                    <asp:TextBox ID="txt_htlcode" runat="server" placeholder="Hotel Name" class="form-control"></asp:TextBox>
                    <div class="clear1"></div>
                </div>
                  <div class="form-groups col-md-2 col-xs-12">
                    <asp:TextBox ID="txt_bookingID" runat="server" class="form-control" placeholder="Booking ID"></asp:TextBox>
                    <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                    <asp:TextBox ID="txt_roomcode" runat="server" placeholder="Room Name" class="form-control"></asp:TextBox>
                    <div class="clear1"></div>
                </div>
                 <div class="form-groups col-md-2 col-xs-12">
                    <asp:DropDownList ID="Triptype" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select trip type" Value="">Trip Type</asp:ListItem>
                        <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                        <asp:ListItem Text="International" Value="International"></asp:ListItem>
                    </asp:DropDownList>
                      <div class="clear1"></div>
                </div>
                <div class="form-groups col-md-2 col-xs-12">
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                
                                 <asp:ListItem Text="Confirm" Value="Confirm"></asp:ListItem>
                                  <asp:ListItem Text="Hold" Value="Hold"></asp:ListItem>
                                  <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                        </asp:DropDownList>
                     <div class="clear1"></div>
                            </div>
                   <div class="form-groups col-md-4 col-xs-12">
                         <div id="TDAgency" runat="server">
                            <input placeholder="Agency Name " class="form-control" type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObj(this);"
                                onblur="blurObj(this);" value="Agency Name or ID" />
                            <input placeholder=" Agency Name" class="form-control" type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>
                     <div class="clear1"></div>
             </div>
 <div class="col-md-3 col-xs-12 col-md-push-1">
          
            <asp:Button ID="btn_result" runat="server" Text="Search Result" CssClass="buttonfltbks" />
       
            <asp:Button ID="btn_export" runat="server" CssClass="buttonfltbk" Text="Export" />
                      <div class="clear1"></div>
        </div>
               
            </form>
           <div class="col-md-12 col-sm-12 col-xs-12 heading">

                <div class="col-md-6 col-xs-12" style="font-size: 11px; line-height: 20px; text-align: justify; color: Red;">
                    * To get today booking, do not fill any field, only click on Search Result Button.
                </div>
                <div class="col-md-3 col-xs-12" style="color: red; font-weight:bold;">
                    Total Amount :
                                <asp:Label ID="lbl_Total" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-12 " style="color: red; font-weight:bold;">
                    Total Hotel Booked :
                                <asp:Label ID="lbl_counttkt" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="clear1"></div>
    <div class="col-md-6 col-sm-12 col-xs-12" style="line-height: 28px;">
        <div class="col-md-4 medium-7 small-6  "></div>
        
    </div>


    <asp:GridView ID="GrdReport" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        PageSize="40"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
        <Columns>
            <asp:TemplateField HeaderText="OrderID">
                <ItemTemplate>
                    <a href='../../Hotel/BookingSummaryHtl.aspx?OrderId=<%#Eval("OrderId")%>' rel="lyteframe"
                        rev="width: 830px; height: 400px; overflow:hidden;" target="_blank" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #004b91">
                        <asp:Label ID="lblBID" runat="server" Text='<%#Eval("OrderId")%>'></asp:Label></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BookingID" HeaderText="BookingID" ReadOnly="true" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="AgentID" HeaderText="Agent ID " />
            <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
            <asp:TemplateField HeaderText="Room Name">
                <ItemTemplate>
                    <div><%#Eval("RoomName")%> </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StarRating" HeaderText="Star" />
            <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
            <asp:BoundField DataField="NetCost" HeaderText="Cost to Agent" />
            <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
            <asp:TemplateField HeaderText="Cancellation" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkRefund" runat="server" Font-Strikeout="False" Font-Overline="False"
                        CommandArgument='<%#Eval("OrderID") %>' CommandName='<%#Eval("HotelName") %>' ToolTip="Cancellation / Modification" OnClick="lnkRefund_Click">
                                 <img src="../../Images/refund.jpg" border="0" />
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
            <asp:BoundField DataField="PgCharges" HeaderText="PG Charge" />
        </Columns>
    </asp:GridView>


    
    <div id="htlRfndPopup">
        <div class="refundbox">
            <div style="font-weight: bold; font-size: 16px; text-align: center; width: 100%;">
                <div id="RemarkTitle" runat="server"></div>
                <div style="float: right; width: 20px; height: 20px; margin: -20px -13px 0 0;">
                    <a href="javascript:ShowHide('hide');">
                        <img src="<%=ResolveUrl("~/Images/close.png") %>" height="20px" /></a>
                </div>
            </div>
            
                <div class="col-md-12 col-sm-12 col-xs-12">

                    <div class="col-md-2 col-sm-2 col-xs-3   bld">
                        Hotel Name:
                    </div>
                    <div class="col-md-10 medium-10 col-xs-9  " id="HotelName" runat="server"></div>
                    <div class="clear"></div>
                    <div class="col-md-1 medium-1 col-xs-3   bld">
                        Net Cost:
                    </div>
                    <div class="col-md-2 col-sm-2 col-xs-9  " id="amt" runat="server"></div>
                    <div class="large-1 col-sm-1 col-xs-3  large-push-1 medium-push-1  bld">
                        No. of Room:
                    </div>
                    <div class="col-md-1 col-sm-1 col-xs-9 large-push-1 medium-push-1  " id="room" runat="server"></div>

                    <div class="col-md-1 col-sm-1 col-xs-3 large-push-1 medium-push-1   bld">
                        No. of Night:
                    </div>
                    <div class="col-md-1 col-sm-1 col-xs-9 large-push-1 medium-push-1   end" id="night" runat="server"></div>
                  
                    <div class="col-md-1 col-sm-1 col-xs-3   bld">
                        No. of Adult:
                    </div>
                    <div class="col-md-1 col-sm-1 col-xs-9  " id="adt" runat="server"></div>

                    <div class="col-md-1 col-sm-1 col-xs-3 large-push-1 medium-push-1   bld">
                        No. of Child:
                    </div>
                    <div class="col-md-1 col-sm-1 col-xs-9 large-push-1 medium-push-1   end" id="chd" runat="server"></div>

                </div>
            
            <div class="clear1"></div>
            <div id="policy" runat="server" class="col-md-12 col-sm-12 col-xs-12"></div>
            <div class="col-md-12 col-sm-12 col-xs-12" >
                <div style="font-weight: bold;">
                    Cancellation Remark:
                </div>
                <div class="col-md-8 col-sm-8 col-xs-12" >
                    <textarea id="txtRemarkss" cols="103" rows="1" name="txtRemarkss"></textarea>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <asp:Button ID="btn_Refund" runat="server" Text="Hotel Cancelltion" CssClass="buttonfltbk" ToolTip="Auto Cancel of Hotel"
                        OnClientClick="return RemarkValidation('cancellation')" Style="width:135px !important" />
                </div>
                <div class="clear1"></div>
            </div>
             <div style="visibility: hidden;">
                <div>
                    <div style="font-weight: bold;">
                        Full Cancellation
                                                        <input placeholder=" " class="form-control" id="ChkFullCan" type="radio" name="Can" checked="checked" />
                    </div>
                    <div style="font-weight: bold; padding-left: 20px;">
                        Partial Cancellation
                                                        <input placeholder=" " class="form-control" id="ChkParcialCan" type="radio" name="Can" />
                    </div>
                </div>
            </div>
            <div>
                <input class="form-control" id="StartDate" type="hidden" name="StartDate" />
                <input class="form-control" id="EndDate" type="hidden" name="EndDate" />
                <input class="form-control" id="Parcial" type="hidden" name="Parcial" value="false" />
                <input class="form-control" id="OrderIDS" type="hidden" name="OrderIDS" runat="server" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getDate()) + '-' + (myDate.getMonth() + 1) + '-' + myDate.getFullYear();
        $("#From").val(currDate); $("#To").val(currDate);
        var UrlBase = '<%=ResolveUrl("~/") %>';
        var CheckindtPickerOptions = { numberOfMonths: 1, dateFormat: "dd-mm-yy", maxDate: "+1y", minDate: "-1y", showOtherMonths: true, selectOtherMonths: false };
        $("#Checkin").datepicker(CheckindtPickerOptions);
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>
</asp:Content>
