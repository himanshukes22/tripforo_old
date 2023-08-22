<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TktRptDom_RefundUpdated.aspx.vb"
    Inherits="Reports_Refund_TktRptDom_RefundUpdated" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/transtour.css" rel="stylesheet" type="text/css" />
    <link href="../../css/core_style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/lytebox.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/lytebox.js" type="text/javascript"></script>
    <link href="../../CSS/itz.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function Validate() {

            if (document.getElementById("<%=txt_charge.ClientID%>").value == "") {
                alert("Please Provide Refund Charge");
                document.getElementById("<%=txt_charge.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txt_Service.ClientID%>").value == "") {
                alert("Please Provide Service Charge");
                document.getElementById("<%=txt_Service.ClientID%>").focus();
                return false;
            }

            if (confirm("Are you sure!")) {
                document.getElementById("div_Submit").style.display = "none";
                document.getElementById("div_Progress").style.display = "block";
                return true;

            }
            else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        #div_Submit {
            height: 36px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnRefundid" runat="server" />
        <div class="divUpdate w100">
            <table border="1" cellpadding="0" cellspacing="0" width="100%" style="background: #fff;">
                <tr>
                    <td>
                        <h2 style="color: #000">Update Refund</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="boxshadow w100">
                            <tr>
                                <td align="left" style="color: #fff; font-weight: bold; padding-top: 7px;" colspan="4">Agent Detail
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="clear1"></td>
                            </tr>
                            <tr>
                                <td class="bld " width="110px" height="20px" align="left">Agent ID:
                                </td>
                                <td id="td_AgentID" runat="server" class="Text" width="110px"></td>
                                <td class="bld " width="150px" height="20px" align="left">Available Credit Limit:
                                </td>
                                <td id="td_CardLimit" runat="server" class="Text"></td>
                            </tr>
                            <tr>
                                <td class="bld " width="110px" height="20px" align="left">Agent Name:
                                </td>
                                <td id="td_AgentName" runat="server" class="Text" width="110px"></td>
                                <td class="bld " align="left" width="110px">Address:
                                </td>
                                <td id="td_AgentAddress" runat="server" class="Text" width="191px"></td>
                            </tr>
                            <tr>
                                <td class="bld " width="110px" height="20px" align="left">Street:
                                </td>
                                <td id="td_Street" runat="server" class="Text"></td>
                                <td class="bld " align="left">Mobile No:
                                </td>
                                <td id="td_AgentMobNo" runat="server" class="Text"></td>
                            </tr>
                            <tr>
                                <td class="bld " width="110px" height="20px" align="left">Email:
                                </td>
                                <td id="td_Email" runat="server" class="Text"></td>
                                <td class="bld " align="left">Agency Name:
                                </td>
                                <td class="Text" id="td_AgencyName" runat="server"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grd_Pax" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Pax ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaxID" runat="server" Text='<%#Eval("PaxId") %>'></asp:Label>
                                                                <asp:HiddenField ID="HiddenMgtFee" runat="server" Value='<%#Eval("MgtFee") %>' />
                                                                <asp:HiddenField ID="HiddenSrvTax" runat="server" Value='<%#Eval("Service_Tax") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PNR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpnr" runat="server" Text='<%#Eval("pnr_locator") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ticket No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltktno" runat="server" Text='<%#Eval("Tkt_No") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblorderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sector">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pax FirstName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpaxfname" runat="server" Text='<%#Eval("pax_fname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pax LastName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllastname" runat="server" Text='<%#Eval("pax_lname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pax Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpaxtype" runat="server" Text='<%#Eval("pax_type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Departure Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("departure_date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UserID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Agency Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblagencyname" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Fare">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("totalfare") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net Fare">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTDS" runat="server" Text='<%#Eval("TDS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Booking Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBookingDate" runat="server" Text='<%#Eval("Booking_date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrm" runat="server" Text='<%#Eval("RegardingCancel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Partner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartner" runat="server" Text='<%#Eval("PartnerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--                                                <asp:TemplateField HeaderText="Cancellation Chrage">
                                                    <ItemTemplate>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Service Chrage">
                                                    <ItemTemplate>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                    
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="clear1"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="130px" align="right">
                                                <b>Refund Charge :</b></td>
                                            <td width="20%">
                                                <asp:TextBox ID="txt_charge" runat="server" Height="29px" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                            </td>
                                            <td width="130px" align="right">
                                                <b>Service Charge :</b></td>
                                            <td width="20%">
                                                <asp:TextBox ID="txt_Service" runat="server" Height="29px" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                            </td>
                                            <td width="130px" align="right">
                                                <b>Remark : </b>
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" class="clear1"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="center">
                                                <div id="div_Progress" style="display: none">
                                                    <b>Refund In Progress.</b> Please do not 'refresh' or 'back' button
                                    <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                                                </div>
                                                <div id="div_Submit">
                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return Validate();"
                                                        CssClass="button rgt" Width="150px" />
                                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button rgt" Width="150px" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" class="clear"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbluserid" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="clear1"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
