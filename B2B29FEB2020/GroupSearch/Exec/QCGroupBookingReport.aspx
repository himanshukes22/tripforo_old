<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="QCGroupBookingReport.aspx.cs" Inherits="GroupSearch_Exec_QCGroupBookingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <script type="text/javascript">
        function openPopup(Remarks) {
            $('#lbl_Remarks').text(Remarks);
            $("#popupdiv").dialog({
                title: "Remarks",
                width: 400,
                height: 350,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
        }
        function CloseAndRefresh() {
            open(location, '_self').close();
            window.open("ExecRequestDetails.aspx");
        }
    </script>
    <script type="text/javascript">
        $(".loader").click(function (e) {
            $("#waitMessage").show();
        });
    </script>
    <script type="text/javascript">
        function loadimg() {
            $("#waitMessage").show();
        }
    </script>
    <style>
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 220px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                /* Position the tooltip */
                position: absolute;
                z-index: 1;
            }

            .tooltip:hover .tooltiptext {
                visibility: visible;
            }
    </style>
    <%--<script type="text/javascript">
        function InitializeToolTip() {
            $(".gridViewToolTip").tooltip({
                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function () {
                    return $($(this).next().html());
                },
                showURL: false
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            InitializeToolTip();
        })
    </script>--%>
    <div class="large-12 medium-12 small-12 columns" style="margin-top: 80px">
        <div class="large-3 medium-3 small-12 columns">
        </div>
        <div class="large-6 medium-6 small-12 columns large-push-1 medium-push-1 end">
            <div class="large-2 medium-2 small-6 columns">
                Status:
            </div>
            <div class="large-3 medium-3 small-6 columns">
                <asp:DropDownList ID="ddl_status" runat="server">
                    <asp:ListItem Selected="True" Value="N">---All ---</asp:ListItem>
                    <asp:ListItem>Requested</asp:ListItem>
                    <asp:ListItem>Freezed</asp:ListItem>
                    <asp:ListItem>Paid</asp:ListItem>
                    <asp:ListItem>Ticketed</asp:ListItem>
                    <asp:ListItem>Cancelled</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                    <asp:ListItem>Cancellation Requested</asp:ListItem>
                    <asp:ListItem>Refunded</asp:ListItem>
                    <asp:ListItem>Quoted</asp:ListItem>
                    <asp:ListItem>Accepted</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="large-2 medium-2 small-6 large-push-2 medium-push-2 columns">Request Id: </div>
            <div class="large-3 medium-3 small-6 large-push-2 medium-push-2 columns">
                <asp:TextBox ID="txt_RequestID" runat="server"></asp:TextBox>
            </div>
            <div class="clear1"></div>
            <div class="large-2 medium-2 small-6 columns">Date From :</div>
            <div class="large-3 medium-3 small-6 columns">
                <asp:TextBox ID="txt_fromDate" CssClass="date" runat="server"></asp:TextBox>
            </div>
            <div class="large-2 medium-2 small-6 large-push-2 medium-push-2 columns">Date To:</div>
            <div class="large-3 medium-3 small-6 large-push-2 medium-push-2 columns">
                <asp:TextBox ID="txt_todate" CssClass="date" runat="server"></asp:TextBox>
            </div>
            <div class="clear1"></div>
            <div class="large-2 medium-2 small-6 large-push-10 medium-push-10">
                <asp:Button ID="btn_submit" runat="server" Text="Search"  CssClass="buttonfltbk"  OnClick="btn_submit_Click" />
            </div>
        </div>
    </div>
    <div id="divReport" runat="server" class="large-12 medium-12 small-12" style="margin-top: 20px">
        <div>
            <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" PageSize="50" AllowPaging="true" OnPageIndexChanging="gvEmployee_PageIndexChanging">
                <HeaderStyle BackColor="#3E3E3E" Font-Names="Calibri" ForeColor="White" />
                <RowStyle Font-Names="Calibri" />
                <Columns>
                    <asp:TemplateField HeaderText="RequestId">
                        <ItemTemplate>
                            <a href="#" onclick='openWindow("<%# Eval("RequestId") %>");'>
                                <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestId")%>'></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BookingName" HeaderText="Booking Name" />
                    <asp:BoundField DataField="Trip" HeaderText="Trip" />
                    <asp:BoundField DataField="TripType" HeaderText="Trip Type" />
                    <asp:BoundField DataField="NoOfPax" HeaderText="Total Pax" />
                    <asp:BoundField DataField="ExpactedPrice" HeaderText="Expacted Price" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />
                    <asp:BoundField DataField="BookedPrice" HeaderText="Booking Price" />
                    <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                    <asp:BoundField DataField="PgCharges" HeaderText="Pg Charges" />
                    <asp:TemplateField>
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <div class="tooltip">
                                Activity
  <span class="tooltiptext">Requested By: <%# Eval("CreatedBy")%>
      <br />
      Requested Date: <%# Eval("CreatedDate")%>
      <br />
      Accepted By: <%# Eval("AcceptBy")%>
      <br />
      Accepted Date: <%# Eval("AcceptDate")%>
      <br />
      Rejected By: <%# Eval("RejectBy")%>
      <br />
      RejectDate Date: <%# Eval("AcceptDate")%>
      <br />
      Quoted By: <%# Eval("QuotedBy")%>
      <br />
      Quoted Date: <%# Eval("QuotedDate")%>
      <br />
      Freezed By: <%# Eval("FreezedBy")%>
      <br />
      Freezed Date: <%# Eval("FreezedDate")%>
      <br />
      Payement By: <%# Eval("PayementBy")%>
      <br />
      Payement Date: <%# Eval("PayementDate")%>
      <br />
      Ticketed By: <%# Eval("TicketedBy")%>
      <br />
      Ticketed Date: <%# Eval("TicketedDate")%>
      <br />
      Cancelled By: <%# Eval("CancelledBy")%>
      <br />
      Cancelled Date: <%# Eval("CancelledDate")%>
      <br />
  </span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>
    <div id="waitMessage" style="display: none;">
        <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; /* border: 5px solid #d1d1d1; */ /* border-radius: 10px; */">
            <div style="position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>
    </div>
    <%--<div id="waitMessage" style="display: none;">
        <div class="" style="text-align: center; opacity: 0.7; position: absolute; z-index: 99999; top: 10px; width: 100%; height: 100%; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; border-radius: 10px;">
            Please wait....<br />
            <br />
            <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
            <br />
        </div>
    </div>--%>
    <div id="popupdiv" title="Basic modal dialog" style="display: none">
        Remarks:
            <label id="lbl_Remarks"></label>
    </div>
    <link href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        function openPopup(Remarks) {
            $('#lbl_Remarks').text(Remarks);
            $("#popupdiv").dialog({
                title: "Remarks",
                width: 400,
                height: 350,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
        }
    </script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
    <script type="text/javascript">
        function openWindow(Requestid) {
            window.open('../GroupRequestidDetails.aspx?RequestID=' + Requestid, 'open_window', ' width=1024, height=720, left=0, top=0,status=yes,toolbar=no,scrollbars=yes');
        }
    </script>
    <script type="text/javascript">
        function openPopup(Remarks) {
            $('#lbl_Remarks').text(Remarks);
            $("#popupdiv").dialog({
                title: "Remarks",
                width: 400,
                height: 350,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Remark can not be blank,Please Fill Remark");
                    $("#waitMessage").hide();
                }
                    break;
            }
        }
    </script>
</asp:Content>

