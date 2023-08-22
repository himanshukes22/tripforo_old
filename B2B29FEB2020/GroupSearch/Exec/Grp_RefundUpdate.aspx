<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grp_RefundUpdate.aspx.cs" Inherits="GroupSearch_Exec_Grp_RefundUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/itz.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Hotel/css/HotelStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ReissueRefund.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        function Reissue() {
            var riChrg = $('#txt_Reissue_charge').val()
            var ReSerChg = $('#txt_Service_charge').val()
            var txtRemark = $('#txtRemark').val()

            if ($.trim(riChrg) == "") {
                alert("Cancellation Charge can not be blank,,min value zero");
                return false;
            }
            if ($.trim(ReSerChg) == "") {
                alert("Service charge can not be blank,min value zero");
                return false;
            }
            if ($.trim(txtRemark) == "") {
                alert("Remark can not be blank, please fill the remark");
                return false;
            }
           // $("#waitMessage").show();
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Amount refunded successfully!!");
                    window.opener.document.location.href = 'GrpRefundRequestInprocess.aspx';
                    window.close();
                }
                    break;
                case 2: {
                    alert("Cancellation amount should be less then payable amount!!");
                    $("#waitMessage").hide();
                }
                    break;
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({
                dateFormat: 'dd/mm/yy',
                minDate: 0,
            });
        });
    </script>
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode > 47 && charCode < 58 || charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || (charCode == 8 || charCode == 45))) {
                return false;
            }
            status = "";
            return true;
        }
    </script>
    <%--<script type="text/javascript">
        function closechildwindow() {
           
        }
        onunload="closechildwindow()"
</script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="divUpdate w80 auto">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="boxshadow">
                <tr>
                    <td align="left" style="color: #fff; font-weight: bold; margin-bottom: 20px;" colspan="6">Refund Request
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="clear1"></td>
                </tr>
                <tr>
                    <td class="bld" width="130px" height="20px" align="left">Agent ID:
                    </td>
                    <td id="td_AgentID" runat="server" class="w20"></td>
                    <td class="bld" width="130px" align="left">Agency Name:
                    </td>
                    <td id="td_AgencyName" runat="server" class="w20"></td>
                    <td class="bld" width="130px" height="20px" align="left">Credit Limit:
                    </td>
                    <td id="td_CardLimit" runat="server" class="w20"></td>
                </tr>
                <tr>
                    <td class="bld" width="130px" align="left">Mobile No:
                    </td>
                    <td id="td_AgentMobNo" runat="server" class="w20"></td>
                    <td class="bld" width="130px" height="20px" align="left">Email:
                    </td>
                    <td id="td_Email" runat="server" class="w20"></td>
                    <td class="bld" align="left" width="130px">Address:
                    </td>
                    <td id="td_AgentAddress" runat="server" class="w20"></td>
                </tr>
                <tr>
                    <td class="w100" colspan="6">
                        <asp:GridView ID="GridRefundRequest" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" PageSize="30">
                            <Columns>
                                <asp:TemplateField HeaderText="RequestID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refndRequestID" runat="server" Text='<%#Eval("RefundRequestID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bookinng ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_RequestID" runat="server" Text='<%#Eval("RequestedID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trip">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Trip" runat="server" Text='<%#Eval("Trip") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trip Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Triptype" runat="server" Text='<%#Eval("TripType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Journey">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_journey" runat="server" Text='<%#Eval("Journey") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Journey Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_doj" runat="server" Text='<%#Eval("JouneryDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Passanges">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_NOP" runat="server" Text='<%#Eval("TotalPax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Booking Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_bookingprice" runat="server" Text='<%#Eval("BookingFare") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PaymentStatus" runat="server" Text='<%#Eval("CancelStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancellation Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_EXCEremarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Pmode" runat="server" Text='<%#Eval("PaymentMode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PG Charges">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pgcharge" runat="server" Text='<%#Eval("PGCharges") %>'></asp:Label>
                                        <asp:Label ID="lbl_agentid" Visible="false" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                            <PagerStyle />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HeaderStyle" Height="50px" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td colspan="6">
                                    <hr style="border: 1px dotted Black; margin: 4px 0;" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" class="bld">
                                    <table width="100%">
                                        <tr>
                                            <td>Cancellation Charge :
                                            </td>
                                            <td>
                                                <input id="txt_Reissue_charge" maxlength="10" runat="server" name="txt_Reissue_charge" type="text" style="width: 130px;" onkeypress="return NumericOnly(event)" />
                                            </td>
                                            <td></td>
                                            <td>Service Charge :
                                            </td>
                                            <td>
                                                <input id="txt_Service_charge" maxlength="10" runat="server" name="txt_Service_charge" type="text" style="width: 130px;" onkeypress="return NumericOnly(event)" />
                                            </td>
                                            <td></td>
                                            <td style="width: 65px;" class="bld">Remark:</td>
                                            <td>
                                                <textarea id="txtRemark" runat="server" cols="31" rows="2" name="txtRemark" onblur="return Reissue(this);"></textarea>
                                                <span style="color: Red;">&nbsp;*</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" align="center">
                                                <div id="div_Progress" style="display: none">
                                                    <b>Refund In Progress.</b>  Please do not 'refresh' or 'back' button 
                                            <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                                                </div>
                                                <div id="div_Submit">
                                                    <%--<asp:Button ID="btn_Update" runat="server" Text="Submit" OnClick="btn_Update_Click" OnClientClick="return Reissue(this);" CssClass="buttonfltbk rgt" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  --%>                                       
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="buttonfltbks rgt" />
                                                </div>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center">
                                    <asp:Label ID="lblreissuemsg" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div id="MapPopup_box">
                            <div id="Mapmnfvc">
                                <div class="Mapmnfvc1">
                                    <div class="Mapmnfvc2">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="waitMessage" style="display: none;">
            <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000;">
                <div style="position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
                    Please wait....<br />
                    <br />
                    <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
