<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="GroupDetails.aspx.cs" Inherits="GroupSearch_GroupDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
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
        .btn-warning {
    color: #fff;
    background-color: #f58220!important;
    border-color: #f58220!important;
}
        .btn-primary {
    color: #fff;
    background-color: #337ab7!important;
    border-color: #2e6da4!important;
}
        .form-control, select {
    display: block!important;
    width: 100%!important;
    height: 38px!important;
    padding: 6px 12px!important;
    font-size: 14px!important;
    line-height: 1.42857143!important;
    color: #a2a2a2!important;
    background-color: #fff!important;
    background-image: none!important;
    border: 1px solid #ccc!important;
    border-radius: 0px!important;
    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075)!important;
    box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075)!important;
    -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s!important;
    -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s!important;
    transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s!important;
}
    </style>
    <div class="row " style=" padding-top:20px; "">
        <div class="col-md-12 text-center search-text  ">
            Group Details
        </div>
        <div class="col-md-10" style="padding-left: 100px; padding-top:20px; ">
            <div class="form-inlines">
                <div class="form-groups">

                    <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control" >
                        <asp:ListItem Selected="True" Value="N">All</asp:ListItem>
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

                <div class="form-groups">
                    <asp:TextBox placeholder="Request Id" CssClass="form-control" ID="txt_RequestID" runat="server"></asp:TextBox>
                </div>

                <div class="form-groups">
                    <asp:TextBox ID="txt_fromDate" placeholder="Date From" CssClass="form-control date" runat="server"></asp:TextBox>
                </div>

                <div class="form-groups">
                    <asp:TextBox ID="txt_todate" placeholder="Date To" CssClass="form-control date" runat="server"></asp:TextBox>
                </div>


            </div>
        </div>






        <div class="col-md-2">
            <asp:Button ID="btn_submit" runat="server" Text="Search"   CssClass="buttonfltbks" OnClick="btn_submit_Click" />
            &nbsp;<asp:Button ID="btn_export" runat="server" Text="Export"    class="buttonfltbk" OnClick="btn_export_Click" />
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
                    AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px"
                    PageSize="30" OnRowCommand="GrpBookingDetails_RowCommand" OnRowDataBound="GrpBookingDetails_RowDataBound" OnPageIndexChanging="GrpBookingDetails_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="RequestID">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("RequestID") %>");'>
                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label></a>
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
                                <asp:Label ID="lblNoOfPax" runat="server" Text='<%#Eval("NoOfPax")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expected Fare">
                            <ItemTemplate>
                                <asp:Label ID="lblExpectedPrice" runat="server" Text='<%#Eval("ExpactedPrice")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey">
                            <ItemTemplate>
                                <asp:Label ID="lblJourney" runat="server" Text='<%#Eval("Journey")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journey Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJourneydate" runat="server" Text='<%#Eval("JourneyDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Status">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentStatus" runat="server" Text='<%#Eval("PaymentStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_AcceptBy" runat="server" CommandName="Accept" OnClientClick="return loadimg();" CommandArgument='<%#Eval("RequestID") %>'
                                    Font-Bold="True" Font-Underline="False">Accept</asp:LinkButton>
                                || 
                                 <asp:LinkButton ID="lnk_RejectBy" runat="server" CommandName="Reject" CssClass="loader" CommandArgument='<%#Eval("RequestID") %>'
                                     Font-Bold="True" Font-Underline="False">Reject</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_BookedPrice" runat="server" Text='<%#Eval("BookedPrice")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PaymentMode">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Eval("PaymentMode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PgCharges">
                            <ItemTemplate>
                                <asp:Label ID="lblPgCharges" runat="server" Text='<%#Eval("PgCharges")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CancellationRemarks">
                            <ItemTemplate>
                                <asp:Label ID="lblCancellationRemarks" runat="server" Text='<%#Eval("CancellationRemarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemark" Visible="false" runat="server" Height="47px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSubmit_1" runat="server" Visible="false"
                                    CommandArgument='<%# Eval("RequestID") %>' OnClientClick="return loadimg();" CommandName="CancelReqSubmit"><img src="../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                <asp:LinkButton ID="lnkHides_1" runat="server" Visible="false"
                                    CommandName="ReqCancel" CommandArgument='<%# Eval("RequestID") %>'><img src="../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="link_Invoice" runat="server" CommandArgument='<%#Eval("RequestID") %>'
                                    Font-Bold="True" Font-Underline="False" OnClick="link_Invoice_Click">View Invoice</asp:LinkButton>
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
            window.open('GroupRequestidDetails.aspx?RequestID=' + Requestid, 'open_window', ' width=1024, height=720, left=0, top=0,status=yes,toolbar=no,scrollbars=yes');
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

