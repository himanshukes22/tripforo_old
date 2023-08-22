<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TktRptDom_ReIssueUpdate.aspx.vb" Inherits="SprReports_Reissue_TktRptDom_ReIssueUpdate" %>

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
    <script type="text/javascript">
        function Reissue() {
            var riChrg = $('#txt_Reissue_charge').val()
            var ReSerChg = $('#txt_Service_charge').val()
            var FareDiff = $('#txt_farediff').val()
            var txtRemark = $('#txtRemark').val()

            if ($.trim(riChrg) == "" || $.trim(riChrg) == "0") {
                alert("Reissue Charge must be greater than zero");
                return false;
            }
            if ($.trim(ReSerChg) == "") {
                alert("Service charge can not be blank,min value zero");
                return false;
            }
            if ($.trim(FareDiff) == "" || $.trim(FareDiff) == "0") {
                alert("Fare diff must be greater than zero");
                return false;
            }
            if ($.trim(txtRemark) == "") {
                alert("Remark can not be blank, please fill the remark");
                return false;
            }
        }

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="divUpdate w80 auto">

            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="boxshadow">
                <tr>
                    <td align="left" style="color: #fff; font-weight: bold; margin-bottom: 20px;" colspan="6">Update ReIssue
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
                        <asp:GridView ID="ReissueUpdateGrd" runat="server" AutoGenerateColumns="False"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                            <Columns>
                                <asp:TemplateField HeaderText="Pax ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaxID" runat="server" Text='<%#Eval("PaxId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorderId" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sector">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Total Fare">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalfare" runat="server" Text='<%#Eval("totalfare") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Fare AD">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalfareafterdiscount" runat="server" Text='<%#Eval("TotalFareAfterDiscount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreissueremark" runat="server" Text='<%#Eval("RegardingIssue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Partner Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartner" runat="server" Text='<%#Eval("PartnerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%" cellpadding="2" cellspacing="2">
                            <tr id="SectorTR1" runat="server" visible="false" class="bld">
                                <td id="Sector1" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px">Edit
                                </td>
                            </tr>
                            <tr style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo" type="text" runat="server" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight1')" />
                                </td>
                            </tr>
                            <tr id="SectorTR2" runat="server" visible="false" class="bld">
                                <td id="Sector2" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr id="TRSecondFlt" runat="server" visible="false" class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px"></td>
                            </tr>
                            <tr id="TRSecondFlt2" runat="server" visible="false" style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date2" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime2" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate2" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime2" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo2" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight2')" />
                                </td>
                            </tr>
                            <tr id="SectorTr3" runat="server" visible="false" class="bld">
                                <td id="Sector3" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr id="TRThirdFlt" runat="server" visible="false" class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px"></td>
                            </tr>
                            <tr id="TRThirdFlt2" runat="server" visible="false" style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date3" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime3" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate3" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime3" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo3" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight3')" />
                                </td>
                            </tr>

                            <tr id="SectorTr4" runat="server" visible="false" class="bld">
                                <td id="Sector4" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr id="TRFourthFlt" runat="server" visible="false" class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px"></td>
                            </tr>
                            <tr id="TRFourthFlt2" runat="server" visible="false" style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date4" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime4" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate4" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime4" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo4" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight4')" />
                                </td>
                            </tr>
                            <tr id="SectorTr5" runat="server" visible="false" class="bld">
                                <td id="Sector5" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr id="TRFifthFlt" runat="server" visible="false" class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px"></td>
                            </tr>
                            <tr id="TRFifthFlt2" runat="server" visible="false" style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date5" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime5" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate5" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime5" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo5" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight5')" />
                                </td>
                            </tr>
                            <tr id="SectorTr6" runat="server" visible="false" class="bld">
                                <td id="Sector6" runat="server" style="border: 1px dotted Black;"></td>
                                <td colspan="5">
                                    <hr style="border: 1px dotted Black;" />
                                </td>
                            </tr>
                            <tr id="TRSixthFlt" runat="server" visible="false" class="bld">
                                <td>Departure Date
                                </td>
                                <td>Departure Time
                                </td>
                                <td>Arival Date
                                </td>
                                <td>Arival Time
                                </td>
                                <td>Flight Number
                                </td>
                                <td width="20px"></td>
                            </tr>
                            <tr id="TRSixthFlt2" runat="server" visible="false" style="padding-left: 4px; height: 29px;">
                                <td>
                                    <input id="txt_date6" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtDepTime6" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalDate6" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtArivalTime6" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td>
                                    <input id="txtFltNo6" type="text" runat="server" readonly="readonly" style="border: none; width: 92px;" />
                                </td>
                                <td width="20px">
                                    <img src="../../Images/edit.png" onclick="return PopupDiv('Flight6')" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr style="border: 1px dotted Black; margin: 4px 0;" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" class="bld">
                                    <table width="100%">
                                        <tr>
                                            <td>Gds Pnr :
                                            </td>
                                            <td>
                                                <input id="txt_pnr" type="text" onkeypress="return checkit(event)" runat="server" style="width: 130px;" />
                                            </td>
                                            <td></td>
                                            <td>Airline Pnr :
                                            </td>
                                            <td>
                                                <input id="txtAirPnr" type="text" onkeypress="return checkit(event)" runat="server" style="width: 130px;" />
                                            </td>
                                            <td></td>
                                            <td>Ticket Number :
                                            </td>
                                            <td>
                                                <input id="txtTktNo" name="txtAirPnr" onkeypress="return checkit(event)" runat="server" style="width: 130px;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ReIssue Charge :
                                            </td>
                                            <td>
                                                <input id="txt_Reissue_charge" name="txt_Reissue_charge" type="text" style="width: 130px;" onblur="return Reissue(this);" onkeypress="return NumericOnly(event)" />
                                            </td>
                                            <td></td>
                                            <td>Service Charge :
                                            </td>
                                            <td>
                                                <input id="txt_Service_charge" name="txt_Service_charge" type="text" style="width: 130px;" onkeypress="return NumericOnly(event)" />
                                            </td>
                                            <td></td>
                                            <td>Fare Difference :</td>

                                            <td>
                                                <input id="txt_farediff" name="txt_farediff" type="text" style="width: 130px;" onblur="return Reissue(this);" onkeypress="return NumericOnly(event)" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <table width="100%">
                                        <tr>
                                            <td style="width: 65px;" class="bld">Remark:</td>
                                            <td>
                                                <textarea id="txtRemark" cols="31" rows="2" name="txtRemark" onblur="return Reissue(this);"></textarea>
                                                <span style="color: Red;">&nbsp;*</span>
                                            </td>
                                            <td colspan="4" align="center">
                                                <div id="div_Progress" style="display: none">
                                                    <b>Refund In Progress.</b>  Please do not 'refresh' or 'back' button 
                                            <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                                                </div>
                                                <div id="div_Submit">
                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return Reissue(this);" CssClass="button rgt" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                         
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button rgt" />
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
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 650px; margin: auto; background: #fff; -webkit-border-radius: 10px; -o-border-radius: 10px; -moz-border-radius: 10px; border: 4px solid #888; padding: 11px; box-shadow: 1px 1px 4px #000;">
                                            <tr>
                                                <td>
                                                    <table id="firstFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD1" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT1" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD1" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT1" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFlt1" runat="server" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit2" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt1');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit1" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="SecondFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title2" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD2" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT2" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD2" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT2" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflt2" runat="server" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit3" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt11');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit4" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="ThirdFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title3" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD3" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT3" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD3" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT3" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflt3" runat="server" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit5" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt111');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit6" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="FourthFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title4" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD4" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT4" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD4" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT4" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflt4" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit7" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt1111');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit8" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="FifthFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title5" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD5" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT5" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD5" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT5" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflt5" runat="server" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit9" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt11111');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit10" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="SixthFlt" cellpadding="11" cellspacing="11">
                                                        <tr>
                                                            <td colspan="2">
                                                                <input id="Title6" type="text" style="border: none; width: 130px; float: left;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Departure Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptD6" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Departure Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDptT6" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Arival Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrD6" runat="server" MaxLength="6" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(DDMMYY)
                                                            </td>
                                                            <td class="bld">Arival Time :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtArrT6" runat="server" MaxLength="4" onkeypress="return NumericOnly(event)"
                                                                    Width="92px"></asp:TextBox>(hhmm)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="bld">Flight No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflt6" runat="server" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="Submit11" type="submit" value="EDIT" class="button" onclick="return EditValue('Flt111111');" />
                                                                &nbsp;&nbsp;
                                                            <input id="Submit12" type="submit" value="CANCEL" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
