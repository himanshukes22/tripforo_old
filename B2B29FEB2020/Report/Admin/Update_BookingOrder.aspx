<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Update_BookingOrder.aspx.vb"
    Inherits="Update_BookingOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode == 08 || charCode == 46 || charCode == 32)) {
                return true;
            }
            else {
                return false;
            }
        }


    </script>
     <link href="../../CSS/foundation.min.css" rel="stylesheet" />
    <link href="../../CSS/foundation.css" rel="stylesheet" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <style>
        input[type="text"], input[type="password"], select, textarea {
            border: 1px solid #808080;
            padding: 2px;
            font-size: 1em;
            color: #444;
            width: 150px;
            font-family: arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: normal;
            border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -o-border-radius: 3px 3px 3px 3px;
        }
    </style>

</head>


<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;
        </div>
        <div align="center">
            <table width="90%">
                <tr>
                    <td align="right" style="font-weight: bold; font-size: 15px; color: #004b91;">Booking Reference No.
                    </td>
                    <td id="tdRefNo" runat="server" align="left" style="font-weight: bold;"></td>
                </tr>
            </table>

            <div>
                &nbsp;
            </div>
            <table width="90%" class="tbltbl" border="0" cellpadding="10" cellspacing="10">
                <tr>
                    <td align="left" style="font-weight: bold; font-size: 14px;">PNR Details
                    </td>
                </tr>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <tr>


                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GvFlightHeader" runat="server" AutoGenerateColumns="false" Width="100%"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Booking Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBookingDate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gds Pnr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGdsPnr" runat="server" Text='<%#Eval("GDsPnr")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGdsPnr" runat="server" Width="70px" Text='<%#Eval("GDsPnr")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline Pnr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAirlinePnr" runat="server" Text='<%#Eval("AirlinePnr")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAirlinePnr" runat="server" Width="70px" Text='<%#Eval("AirlinePnr")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtStatus" runat="server" Width="80px" Text='<%#Eval("Status")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField HeaderText="Modify Status" DataField="MordifyStatus"></asp:BoundField>
                                         <asp:BoundField HeaderText="Partner Name" DataField="PartnerName"></asp:BoundField>
                                        <asp:BoundField HeaderText="API ID" DataField="APIID"></asp:BoundField>
                                         <asp:TemplateField HeaderText="Message">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmsg" runat="server"  ForeColor="Red" Text='<%#Eval("msg")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Edit/Update">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="newbutton_2"/>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="newbutton_2" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="newbutton_2" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                </asp:GridView>
                                <b>
                                    <asp:Label ID="lblUpdateFltHeader" runat="server" Visible="false"></asp:Label></b>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                                </div>
                                <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                    Please Wait....<br />
                                    <br />
                                    <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                    <br />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>


                </tr>

            </table>

            <div>
                &nbsp;
            </div>
            <table width="90%" class="tbltbl" border="0" cellpadding="10" cellspacing="10">
                <tr>
                    <td align="left" style="font-weight: bold; font-size: 14px;">Passenger Information
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GvTravellerInformation" runat="server" AutoGenerateColumns="false"
                                    Width="100%" CssClass="GridViewStyle">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaxId" runat="server" Text='<%#Eval("PaxId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTitle" Width="50px" runat="server" Text='<%#Eval("Title")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFname" Width="100px" runat="server" Text='<%#Eval("FName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLName" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLname" Width="100px" runat="server" Text='<%#Eval("LName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtType" MaxLength="3" runat="server" Width="60px" Text='<%#Eval("PaxType")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ticket No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTktNo" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTktNo" runat="server" Width="80px" Text='<%#Eval("TicketNumber")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText ="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsts" runat="server" Text='<%#Eval("MORDIFYSTATUS")%>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText ="Message">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsts" runat="server"  ForeColor="Red" Text='<%#Eval("msg")%>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remark" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Edit/Update">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="newbutton_2" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="newbutton_2"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="newbutton_2"/>
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                </asp:GridView>
                                <b>
                                    <asp:Label ID="lblUpdatePax" runat="server" Visible="false"></asp:Label></b>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                                </div>
                                <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                    Please Wait....<br />
                                    <br />
                                    <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                    <br />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>

            </table>

            <div>
                &nbsp;
            </div>
            <table width="90%" class="tbltbl" border="0" cellpadding="10" cellspacing="10">
                <tr>
                    <td align="left" style="font-weight: bold; font-size: 14px;">Flight Information
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GvFlightDetails" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="GridViewStyle">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFltId" runat="server" Text='<%#Eval("FltId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DepCity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepcityName" runat="server" Text='<%#Eval("DepCityOrAirportName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDepcityName" Width="80px" runat="server" Text='<%#Eval("DepCityOrAirportName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DCityCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepCityOrAirportCode" runat="server" Text='<%#Eval("DepCityOrAirportCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDepcityCode" Width="70px" MaxLength="3" runat="server" Text='<%#Eval("DepCityOrAirportCode")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ArrCity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrCityOrAirportName" runat="server" Text='<%#Eval("ArrCityOrAirportName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtArrcityName" Width="80px" runat="server" Text='<%#Eval("ArrCityOrAirportName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACityCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrCityOrAirportCode" runat="server" Text='<%#Eval("ArrCityOrAirportCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtArrcityCode" MaxLength="3" Width="70px" runat="server" Text='<%#Eval("ArrCityOrAirportCode")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAirlineName" runat="server" Text='<%#Eval("AirlineName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAirlineName" Width="80px" runat="server" Text='<%#Eval("AirlineName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AirCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAirlineCode" runat="server" Text='<%#Eval("AirlineCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAirlineCode" Width="60px" runat="server" Text='<%#Eval("AirlineCode")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FltNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFltNumber" runat="server" Text='<%#Eval("FltNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFltNo" Width="50px" runat="server" Text='<%#Eval("FltNumber")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DepDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepDate" runat="server" Text='<%#Eval("DepDate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDepDate" MaxLength="6" onkeypress="return isNumberKey(event)" Width="60px" runat="server" Text='<%#Eval("DepDate")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DepTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepTime" runat="server" Text='<%#Eval("DepTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDepTime" onkeypress="return isNumberKey(event)" Width="50px" runat="server" Text='<%#Eval("DepTime")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ArrTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrTime" runat="server" Text='<%#Eval("ArrTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtArrTime" onkeypress="return isNumberKey(event)" Width="50px" runat="server" Text='<%#Eval("ArrTime")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AirCraft">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAirCraft" runat="server" Text='<%#Eval("AirCraft")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAirCraft" Width="50px" runat="server" Text='<%#Eval("AirCraft")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="server" Width="100px" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Edit/Update">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="newbutton_2"/>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="newbutton_2"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="newbutton_2"/>
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                </asp:GridView>
                                <b>
                                    <asp:Label ID="lblUpdateFlight" runat="server" Visible="false"></asp:Label></b>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                            <ProgressTemplate>
                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                                </div>
                                <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                    Please Wait....<br />
                                    <br />
                                    <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                    <br />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </td>
                </tr>

            </table>

            <div>
                &nbsp;
            </div>
            <table width="90%" class="tbltbl" border="0" cellpadding="10" cellspacing="10">
                <tr>
                    <td align="left" style="font-weight: bold; font-size: 14px;">Fare Information
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFareInformation" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                &nbsp;
            </div>
        </div>
    </form>
</body>
</html>





