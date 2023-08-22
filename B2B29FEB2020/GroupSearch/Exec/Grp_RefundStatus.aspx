<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="Grp_RefundStatus.aspx.cs" Inherits="GroupSearch_Exec_Grp_RefundStatus" %>

<%--<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style>
        .mscd {
            max-width: 170% !important;
            width: 170% !important;
        }

        .btn-warning {
            color: #fff;
            background-color: #f58220 !important;
            border-color: #f58220 !important;
        }

        .btn-primary {
            color: #fff;
            background-color: #337ab7 !important;
            border-color: #2e6da4 !important;
        }

        .form-control, select {
            display: block !important;
            width: 100% !important;
            height: 38px !important;
            padding: 6px 12px !important;
            font-size: 14px !important;
            line-height: 1.42857143 !important;
            color: #a2a2a2 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;
            border-radius: 0px !important;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075) !important;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075) !important;
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s !important;
        }
    </style>
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
    <div class="col-md-12 text-center search-text  ">
           Group Refund Status
        </div>
    <div class="row " style="padding-top: 30px;">
        
        <div class="col-md-10" style="padding-left: 100px;">
            <div class="form-inlines">
                <div class="form-groups">

                    <asp:DropDownList CssClass="form-control" ID="ddl_status" runat="server">
                        <asp:ListItem Selected="True" Value="N">---All ---</asp:ListItem>
                        <asp:ListItem>Rejected</asp:ListItem>
                        <asp:ListItem>Refunded</asp:ListItem>
                        <asp:ListItem>Cancellation Requested</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-groups">
                    <asp:TextBox ID="txt_RequestID" CssClass="form-control" placeholder="Request Id" runat="server"></asp:TextBox>
                </div>


                <div class="form-groups">
                    <asp:TextBox ID="txt_fromDate" CssClass="form-control date" placeholder="Date From" runat="server"></asp:TextBox>
                </div>

                <div class="form-groups">
                    <asp:TextBox ID="txt_todate" CssClass="form-control date" placeholder="Date To" runat="server"></asp:TextBox>
                </div>


            </div>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btn_submit" runat="server" Text="Search" class="buttonfltbks" OnClick="btn_submit_Click" />
            &nbsp;<asp:Button ID="btn_export" runat="server" Text="Export" class="buttonfltbk" OnClick="btn_export_Click" />
        </div>
    </div>
    <div id="divReport" runat="server" style="margin-top: 20px">
        <div id="DivExec" runat="server"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="GrpBookingDetails" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="GrpBookingDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CssClass="table table-hover mscd" GridLines="None" Font-Size="12px" OnPageIndexChanging="GrpBookingDetails_PageIndexChanging"
                    PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="RequestID">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("RequestedID") %>");'>
                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestedID")%>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refund ID">
                            <ItemTemplate>
                                <asp:Label ID="RefundRequestID" runat="server" Text='<%#Eval("RefundRequestID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Name">
                            <ItemTemplate>
                                <asp:Label ID="lblbookingname" runat="server" Text='<%#Eval("BookingName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip">
                            <ItemTemplate>
                                <asp:Label ID="lblTrip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip Type">
                            <ItemTemplate>
                                <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Passangers">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfPax" runat="server" Text='<%#Eval("TotalPax")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey">
                            <ItemTemplate>
                                <asp:Label ID="lblJourney" runat="server" Text='<%#Eval("Journey")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJouneryDate" runat="server" Text='<%#Eval("JouneryDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_BookedPrice" runat="server" Text='<%#Eval("BookingFare")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refund Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Refundamt" runat="server" Text='<%#Eval("TotalRefund")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancellation Charge">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CancellationCharge" runat="server" Text='<%#Eval("CancellationCharge")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Charge">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ServiceCharge" runat="server" Text='<%#Eval("ServiceCharge")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PgCharges">
                            <ItemTemplate>
                                <asp:Label ID="lblPgCharges" runat="server" Text='<%#Eval("PGCharges")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancellation Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblCancellationRemarks" runat="server" Text='<%#Eval("CancelRemarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AgentID">
                            <ItemTemplate>
                                <asp:Label ID="lblAgentID" runat="server" Text='<%#Eval("AgentID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancellation By">
                            <ItemTemplate>
                                <asp:Label ID="lblCancellation" runat="server" Text='<%#Eval("CanceledBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancellation Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCancellationdate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refunded By">
                            <ItemTemplate>
                                <asp:Label ID="lblRefunded" runat="server" Text='<%#Eval("RefundedBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refunded Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRefundeddate" runat="server" Text='<%#Eval("RefundedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected By">
                            <ItemTemplate>
                                <asp:Label ID="lblRejectedby" runat="server" Text='<%#Eval("RejectedBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRejecteddate" runat="server" Text='<%#Eval("RejectedDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
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
