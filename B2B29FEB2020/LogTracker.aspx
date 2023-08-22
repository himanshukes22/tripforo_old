<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="LogTracker.aspx.cs" Inherits="SprReports_LogTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row " style="padding-top: 20px;">
        <div class="col-md-12 text-center search-text  ">
            Log Tracker
        </div>
        <div class="col-md-10" style="padding-left: 100px; padding-top: 20px;">
            <div class="form-inlines">
                <div class="form-groups">
                    <asp:TextBox placeholder="Order Id" CssClass="form-control" ID="txt_OrderId" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:TextBox ID="txt_PNR" placeholder="PNR No/Booking ID" CssClass="form-control date" runat="server"></asp:TextBox>
                </div>
                <div class="form-groups">
                    <asp:DropDownList ID="ddl_PTYPE" runat="server" CssClass="form-control">
                        <asp:ListItem Selected="True" Value="F">Flight</asp:ListItem>
                        <asp:ListItem Value="B">Bus</asp:ListItem>
                        <asp:ListItem Value="H">Hotel</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btn_submit" runat="server" Text="Search" OnClick="btn_submit_Click" CssClass="buttonfltbks" />
        </div>
    </div>

    <div id="divfltheader" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">FltHeader
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="FltHeader" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="HeaderId" DataField="HeaderId"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="sector" DataField="sector"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="MordifyStatus" DataField="MordifyStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="GdsPnr" DataField="GdsPnr"></asp:BoundField>
                            <asp:BoundField HeaderText="AirlinePnr" DataField="AirlinePnr"></asp:BoundField>
                            <asp:BoundField HeaderText="VC" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="Duration" DataField="Duration"></asp:BoundField>
                            <asp:BoundField HeaderText="TripType" DataField="TripType"></asp:BoundField>
                            <asp:BoundField HeaderText="Trip" DataField="Trip"></asp:BoundField>
                            <asp:BoundField HeaderText="TourCode" DataField="TourCode"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalBookingCost" DataField="TotalBookingCost"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalAfterDis" DataField="TotalAfterDis"></asp:BoundField>
                            <asp:BoundField HeaderText="SFDis" DataField="SFDis"></asp:BoundField>
                            <asp:BoundField HeaderText="AdditionalMarkup" DataField="AdditionalMarkup"></asp:BoundField>
                            <asp:BoundField HeaderText="Adult" DataField="Adult"></asp:BoundField>
                            <asp:BoundField HeaderText="Child" DataField="Child"></asp:BoundField>
                            <asp:BoundField HeaderText="Infant" DataField="Infant"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentId" DataField="AgentId"></asp:BoundField>
                            <asp:BoundField HeaderText="AgencyName" DataField="AgencyName"></asp:BoundField>
                            <asp:BoundField HeaderText="DistrId" DataField="DistrId"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveId" DataField="ExecutiveId"></asp:BoundField>
                            <asp:BoundField HeaderText="PaymentType" DataField="PaymentType"></asp:BoundField>
                            <asp:BoundField HeaderText="PgTitle" DataField="PgTitle"></asp:BoundField>
                            <asp:BoundField HeaderText="PgFName" DataField="PgFName"></asp:BoundField>
                            <asp:BoundField HeaderText="PgLName" DataField="PgLName"></asp:BoundField>
                            <asp:BoundField HeaderText="PgMobile" DataField="PgMobile"></asp:BoundField>
                            <asp:BoundField HeaderText="PgEmail" DataField="PgEmail"></asp:BoundField>
                            <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                            <asp:BoundField HeaderText="RejectedRemark" DataField="RejectedRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ResuID" DataField="ResuID"></asp:BoundField>
                            <asp:BoundField HeaderText="ResuCharge" DataField="ResuCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="ResuServiseCharge" DataField="ResuServiseCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="ResuFareDiff" DataField="ResuFareDiff"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxId" DataField="PaxId"></asp:BoundField>
                            <asp:BoundField HeaderText="ImportCharge" DataField="ImportCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="YCRN" DataField="YCRN"></asp:BoundField>
                            <asp:BoundField HeaderText="Y_CAN_FARE" DataField="Y_CAN_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="ProjectID" DataField="ProjectID"></asp:BoundField>
                            <asp:BoundField HeaderText="BillNoCorp" DataField="BillNoCorp"></asp:BoundField>
                            <asp:BoundField HeaderText="BookedBy" DataField="BookedBy"></asp:BoundField>
                            <asp:BoundField HeaderText="IsMobile" DataField="IsMobile"></asp:BoundField>
                            <asp:BoundField HeaderText="FareType" DataField="FareType"></asp:BoundField>
                            <asp:BoundField HeaderText="ReferenceNo" DataField="ReferenceNo"></asp:BoundField>
                            <asp:BoundField HeaderText="APIID" DataField="APIID"></asp:BoundField>
                            <asp:BoundField HeaderText="PartnerName" DataField="PartnerName"></asp:BoundField>
                            <asp:BoundField HeaderText="PreHoldREmark" DataField="PreHoldREmark"></asp:BoundField>
                            <asp:BoundField HeaderText="PreHoldUpdatedBy" DataField="PreHoldUpdatedBy"></asp:BoundField>
                            <asp:BoundField HeaderText="PreholdUpdateDate" DataField="PreholdUpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode"></asp:BoundField>
                            <asp:BoundField HeaderText="PgTid" DataField="PgTid"></asp:BoundField>
                            <asp:BoundField HeaderText="PgCharges" DataField="PgCharges"></asp:BoundField>
                            <asp:BoundField HeaderText="FareRule" DataField="FareRule"></asp:BoundField>
                            <asp:BoundField HeaderText="ReIssueRefundStatus" DataField="ReIssueRefundStatus"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divSelectedFlightDetails_Gal" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">SelectedFlightDetails_Gal
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="SelectedFlightDetails_Gal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrgDestTo" DataField="OrgDestTo"></asp:BoundField>
                            <asp:BoundField HeaderText="DepartureLocation" DataField="DepartureLocation"></asp:BoundField>
                            <asp:BoundField HeaderText="DepartureCityName" DataField="DepartureCityName"></asp:BoundField>
                            <asp:BoundField HeaderText="DepAirportCode" DataField="DepAirportCode"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrivalLocation" DataField="ArrivalLocation"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrivalCityName" DataField="ArrivalCityName"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrAirportCode" DataField="ArrAirportCode"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrivalTerminal" DataField="ArrivalTerminal"></asp:BoundField>
                            <asp:BoundField HeaderText="DepartureDate" DataField="DepartureDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Departure_Date" DataField="Departure_Date"></asp:BoundField>
                            <asp:BoundField HeaderText="DepartureTime" DataField="DepartureTime"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrivalDate" DataField="ArrivalDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Arrival_Date" DataField="Arrival_Date"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrivalTime" DataField="ArrivalTime"></asp:BoundField>
                            <asp:BoundField HeaderText="Adult" DataField="Adult"></asp:BoundField>
                            <asp:BoundField HeaderText="Child" DataField="Child"></asp:BoundField>
                            <asp:BoundField HeaderText="Infant" DataField="Infant"></asp:BoundField>
                            <asp:BoundField HeaderText="TotPax" DataField="TotPax"></asp:BoundField>
                            <asp:BoundField HeaderText="MarketingCarrier" DataField="MarketingCarrier"></asp:BoundField>
                            <asp:BoundField HeaderText="OperatingCarrier" DataField="OperatingCarrier"></asp:BoundField>
                            <asp:BoundField HeaderText="FlightIdentification" DataField="FlightIdentification"></asp:BoundField>
                            <asp:BoundField HeaderText="ValiDatingCarrier" DataField="ValiDatingCarrier"></asp:BoundField>
                            <asp:BoundField HeaderText="AirLineName" DataField="AirLineName"></asp:BoundField>
                            <asp:BoundField HeaderText="AvailableSeats" DataField="AvailableSeats"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtCabin" DataField="AdtCabin"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdCabin" DataField="ChdCabin"></asp:BoundField>
                            <asp:BoundField HeaderText="InfCabin" DataField="InfCabin"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtRbd" DataField="AdtRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdRbd" DataField="ChdRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="InfRbd" DataField="InfRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="RBD" DataField="RBD"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtFare" DataField="AdtFare"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtBfare" DataField="AdtBfare"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtTax" DataField="AdtTax"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdFare" DataField="ChdFare"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdBfare" DataField="ChdBfare"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdTax" DataField="ChdTax"></asp:BoundField>
                            <asp:BoundField HeaderText="InfFare" DataField="InfFare"></asp:BoundField>
                            <asp:BoundField HeaderText="InfBfare" DataField="InfBfare"></asp:BoundField>
                            <asp:BoundField HeaderText="InfTax" DataField="InfTax"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalBfare" DataField="TotalBfare"></asp:BoundField>
                            <asp:BoundField HeaderText="TotFare" DataField="TotFare"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalTax" DataField="TotalTax"></asp:BoundField>
                            <asp:BoundField HeaderText="NetFare" DataField="NetFare"></asp:BoundField>
                            <asp:BoundField HeaderText="STax" DataField="STax"></asp:BoundField>
                            <asp:BoundField HeaderText="TFee" DataField="TFee"></asp:BoundField>
                            <asp:BoundField HeaderText="DisCount" DataField="DisCount"></asp:BoundField>
                            <asp:BoundField HeaderText="Searchvalue" DataField="Searchvalue"></asp:BoundField>
                            <asp:BoundField HeaderText="LineItemNumber" DataField="LineItemNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="Leg" DataField="Leg"></asp:BoundField>
                            <asp:BoundField HeaderText="Flight" DataField="Flight"></asp:BoundField>
                            <asp:BoundField HeaderText="Provider" DataField="Provider"></asp:BoundField>
                            <asp:BoundField HeaderText="Tot_Dur" DataField="Tot_Dur"></asp:BoundField>
                            <asp:BoundField HeaderText="TripType" DataField="TripType"></asp:BoundField>
                            <asp:BoundField HeaderText="EQ" DataField="EQ"></asp:BoundField>
                            <asp:BoundField HeaderText="Stops" DataField="Stops"></asp:BoundField>
                            <asp:BoundField HeaderText="Trip" DataField="Trip"></asp:BoundField>
                            <asp:BoundField HeaderText="Sector" DataField="Sector"></asp:BoundField>
                            <asp:BoundField HeaderText="TripCnt" DataField="TripCnt"></asp:BoundField>
                            <asp:BoundField HeaderText="Currency" DataField="Currency"></asp:BoundField>
                            <asp:BoundField HeaderText="ADTAdminMrk" DataField="ADTAdminMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="ADTAgentMrk" DataField="ADTAgentMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="CHDAdminMrk" DataField="CHDAdminMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="CHDAgentMrk" DataField="CHDAgentMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="InfAdminMrk" DataField="InfAdminMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="InfAgentMrk" DataField="InfAgentMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="IATAComm" DataField="IATAComm"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtFareType" DataField="AdtFareType"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtFarebasis" DataField="AdtFarebasis"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdFareType" DataField="ChdFareType"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdFarebasis" DataField="ChdFarebasis"></asp:BoundField>
                            <asp:BoundField HeaderText="InfFareType" DataField="InfFareType"></asp:BoundField>
                            <asp:BoundField HeaderText="InfFarebasis" DataField="InfFarebasis"></asp:BoundField>
                            <asp:BoundField HeaderText="FareBasis" DataField="FareBasis"></asp:BoundField>
                            <asp:BoundField HeaderText="FBPaxType" DataField="FBPaxType"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtFSur" DataField="AdtFSur"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdFSur" DataField="ChdFSur"></asp:BoundField>
                            <asp:BoundField HeaderText="InfFSur" DataField="InfFSur"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFuelSur" DataField="TotalFuelSur"></asp:BoundField>
                            <asp:BoundField HeaderText="sno" DataField="sno"></asp:BoundField>
                            <asp:BoundField HeaderText="depdatelcc" DataField="depdatelcc"></asp:BoundField>
                            <asp:BoundField HeaderText="arrdatelcc" DataField="arrdatelcc"></asp:BoundField>
                            <asp:BoundField HeaderText="OriginalTF" DataField="OriginalTF"></asp:BoundField>
                            <asp:BoundField HeaderText="OriginalTT" DataField="OriginalTT"></asp:BoundField>
                            <asp:BoundField HeaderText="Track_id" DataField="Track_id"></asp:BoundField>
                            <asp:BoundField HeaderText="FlightStatus" DataField="FlightStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="Adt_Tax" DataField="Adt_Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtOT" DataField="AdtOT"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtSrvTax" DataField="AdtSrvTax"></asp:BoundField>
                            <asp:BoundField HeaderText="Chd_Tax" DataField="Chd_Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdOT" DataField="ChdOT"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdSrvTax" DataField="ChdSrvTax"></asp:BoundField>
                            <asp:BoundField HeaderText="Inf_Tax" DataField="Inf_Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="InfOT" DataField="InfOT"></asp:BoundField>
                            <asp:BoundField HeaderText="InfSrvTax" DataField="InfSrvTax"></asp:BoundField>
                            <asp:BoundField HeaderText="SrvTax" DataField="SrvTax"></asp:BoundField>
                            <asp:BoundField HeaderText="TF" DataField="TF"></asp:BoundField>
                            <asp:BoundField HeaderText="TC" DataField="TC"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtTds" DataField="AdtTds"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdTds" DataField="ChdTds"></asp:BoundField>
                            <asp:BoundField HeaderText="InfTds" DataField="InfTds"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtComm" DataField="AdtComm"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdComm" DataField="ChdComm"></asp:BoundField>
                            <asp:BoundField HeaderText="InfComm" DataField="InfComm"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtCB" DataField="AdtCB"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdCB" DataField="ChdCB"></asp:BoundField>
                            <asp:BoundField HeaderText="InfCB" DataField="InfCB"></asp:BoundField>
                            <asp:BoundField HeaderText="ElectronicTicketing" DataField="ElectronicTicketing"></asp:BoundField>
                            <asp:BoundField HeaderText="User_id" DataField="User_id"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtMgtFee" DataField="AdtMgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdMgtFee" DataField="ChdMgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="InfMgtFee" DataField="InfMgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="TotMgtFee" DataField="TotMgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="IsCorp" DataField="IsCorp"></asp:BoundField>
                            <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtComm1" DataField="AdtComm1"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdComm1" DataField="ChdComm1"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtSrvTax1" DataField="AdtSrvTax1"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdSrvTax1" DataField="ChdSrvTax1"></asp:BoundField>
                            <asp:BoundField HeaderText="IsMobile" DataField="IsMobile"></asp:BoundField>
                            <asp:BoundField HeaderText="RESULTTYPE" DataField="RESULTTYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="OldTrack_Id" DataField="OldTrack_Id"></asp:BoundField>
                            <asp:BoundField HeaderText="AirlineRemark" DataField="AirlineRemark"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divFltDetails" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">FltDetails
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="FltDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="DepCityOrAirportCode" DataField="DepCityOrAirportCode"></asp:BoundField>
                            <asp:BoundField HeaderText="DepCityOrAirportName" DataField="DepCityOrAirportName"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrCityOrAirportCode" DataField="ArrCityOrAirportCode"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrCityOrAirportName" DataField="ArrCityOrAirportName"></asp:BoundField>
                            <asp:BoundField HeaderText="DepDate" DataField="DepDate"></asp:BoundField>
                            <asp:BoundField HeaderText="DepTime" DataField="DepTime"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrDate" DataField="ArrDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrTime" DataField="ArrTime"></asp:BoundField>
                            <asp:BoundField HeaderText="AirlineCode" DataField="AirlineCode"></asp:BoundField>
                            <asp:BoundField HeaderText="AirlineName" DataField="AirlineName"></asp:BoundField>
                            <asp:BoundField HeaderText="FltNumber" DataField="FltNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="AirCraft" DataField="AirCraft"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtFareBasis" DataField="AdtFareBasis"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdFareBasis" DataField="ChdFareBasis"></asp:BoundField>
                            <asp:BoundField HeaderText="InfFareBasis" DataField="InfFareBasis"></asp:BoundField>
                            <asp:BoundField HeaderText="AdtRbd" DataField="AdtRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="ChdRbd" DataField="ChdRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="InfRbd" DataField="InfRbd"></asp:BoundField>
                            <asp:BoundField HeaderText="AvlSeat" DataField="AvlSeat"></asp:BoundField>
                            <asp:BoundField HeaderText="CreateDate" DataField="CreateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="YCRN" DataField="YCRN"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divFltFareDetails" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">FltFareDetails
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="FltFareDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="FareId" DataField="FareId"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="BaseFare" DataField="BaseFare"></asp:BoundField>
                            <asp:BoundField HeaderText="YQ" DataField="YQ"></asp:BoundField>
                            <asp:BoundField HeaderText="YR" DataField="YR"></asp:BoundField>
                            <asp:BoundField HeaderText="WO" DataField="WO"></asp:BoundField>
                            <asp:BoundField HeaderText="OT" DataField="OT"></asp:BoundField>
                            <asp:BoundField HeaderText="Qtax" DataField="Qtax"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalTax" DataField="TotalTax"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFare" DataField="TotalFare"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceTax" DataField="ServiceTax"></asp:BoundField>
                            <asp:BoundField HeaderText="TranFee" DataField="TranFee"></asp:BoundField>
                            <asp:BoundField HeaderText="AdminMrk" DataField="AdminMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentMrk" DataField="AgentMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="DistrMrk" DataField="DistrMrk"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalDiscount" DataField="TotalDiscount"></asp:BoundField>
                            <asp:BoundField HeaderText="PLb" DataField="PLb"></asp:BoundField>
                            <asp:BoundField HeaderText="Discount" DataField="Discount"></asp:BoundField>
                            <asp:BoundField HeaderText="CashBack" DataField="CashBack"></asp:BoundField>
                            <asp:BoundField HeaderText="Tds" DataField="Tds"></asp:BoundField>
                            <asp:BoundField HeaderText="TdsOn" DataField="TdsOn"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalAfterDis" DataField="TotalAfterDis"></asp:BoundField>
                            <asp:BoundField HeaderText="AvlBalance" DataField="AvlBalance"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxType" DataField="PaxType"></asp:BoundField>
                            <asp:BoundField HeaderText="CreateDate" DataField="CreateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="YCRN" DataField="YCRN"></asp:BoundField>
                            <asp:BoundField HeaderText="Y_CAN_FARE" DataField="Y_CAN_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="MgtFee" DataField="MgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceTax1" DataField="ServiceTax1"></asp:BoundField>
                            <asp:BoundField HeaderText="Discount1" DataField="Discount1"></asp:BoundField>
                            <asp:BoundField HeaderText="FareType" DataField="FareType"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divFltPaxDetails" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">FltPaxDetails
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="FltPaxDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="PaxId" DataField="PaxId"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="TransactionId" DataField="TransactionId"></asp:BoundField>
                            <asp:BoundField HeaderText="Title" DataField="Title"></asp:BoundField>
                            <asp:BoundField HeaderText="FName" DataField="FName"></asp:BoundField>
                            <asp:BoundField HeaderText="MName" DataField="MName"></asp:BoundField>
                            <asp:BoundField HeaderText="LName" DataField="LName"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxType" DataField="PaxType"></asp:BoundField>
                            <asp:BoundField HeaderText="TicketNumber" DataField="TicketNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="DOB" DataField="DOB"></asp:BoundField>
                            <asp:BoundField HeaderText="FFNumber" DataField="FFNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="FFAirline" DataField="FFAirline"></asp:BoundField>
                            <asp:BoundField HeaderText="MealType" DataField="MealType"></asp:BoundField>
                            <asp:BoundField HeaderText="SeatType" DataField="SeatType"></asp:BoundField>
                            <asp:BoundField HeaderText="IsPrimary" DataField="IsPrimary"></asp:BoundField>
                            <asp:BoundField HeaderText="InfAssociatePaxName" DataField="InfAssociatePaxName"></asp:BoundField>
                            <asp:BoundField HeaderText="MordifyStatus" DataField="MordifyStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="CreateDate" DataField="CreateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="YCRN" DataField="YCRN"></asp:BoundField>
                            <asp:BoundField HeaderText="Gender" DataField="Gender"></asp:BoundField>
                            <asp:BoundField HeaderText="PassportExpireDate" DataField="PassportExpireDate"></asp:BoundField>
                            <asp:BoundField HeaderText="PassportNo" DataField="PassportNo"></asp:BoundField>
                            <asp:BoundField HeaderText="IssueCountryCode" DataField="IssueCountryCode"></asp:BoundField>
                            <asp:BoundField HeaderText="NationalityCode" DataField="NationalityCode"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_Flt_Meal_And_Baggage_Request" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_Flt_Meal_And_Baggage_Request
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_Flt_Meal_And_Baggage_Request" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="BookingRefNo" DataField="BookingRefNo"></asp:BoundField>
                            <asp:BoundField HeaderText="Flt_HeaderID" DataField="Flt_HeaderID"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxID" DataField="PaxID"></asp:BoundField>
                            <asp:BoundField HeaderText="TripType" DataField="TripType"></asp:BoundField>
                            <asp:BoundField HeaderText="MealCode" DataField="MealCode"></asp:BoundField>
                            <asp:BoundField HeaderText="MealPrice" DataField="MealPrice"></asp:BoundField>
                            <asp:BoundField HeaderText="BaggageCode" DataField="BaggageCode"></asp:BoundField>
                            <asp:BoundField HeaderText="BaggagePrice" DataField="BaggagePrice"></asp:BoundField>
                            <asp:BoundField HeaderText="AirLineCode" DataField="AirLineCode"></asp:BoundField>
                            <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate"></asp:BoundField>
                            <asp:BoundField HeaderText="BaggageDesc" DataField="BaggageDesc"></asp:BoundField>
                            <asp:BoundField HeaderText="BaggageCategory" DataField="BaggageCategory"></asp:BoundField>
                            <asp:BoundField HeaderText="BaggagePriceWithNoTax" DataField="BaggagePriceWithNoTax"></asp:BoundField>
                            <asp:BoundField HeaderText="MealDesc" DataField="MealDesc"></asp:BoundField>
                            <asp:BoundField HeaderText="Provider" DataField="Provider"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_ITZ_TRANSACTIONS" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_ITZ_TRANSACTIONS
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_ITZ_TRANSACTIONS" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="AMT_TO_DED" DataField="AMT_TO_DED"></asp:BoundField>
                            <asp:BoundField HeaderText="AMT_TO_CRE" DataField="AMT_TO_CRE"></asp:BoundField>
                            <asp:BoundField HeaderText="COMMISSION_ITZ" DataField="COMMISSION_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="CONVENIENCEFEE_ITZ" DataField="CONVENIENCEFEE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="EASY_ORDERID_ITZ" DataField="EASY_ORDERID_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="EASY_TRAN_CODE_ITZ" DataField="EASY_TRAN_CODE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="DECODE_ITZ" DataField="DECODE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="B2C_MBL_NO_ITZ" DataField="B2C_MBL_NO_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="RATE_GROUP_ITZ" DataField="RATE_GROUP_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="SERIAL_NO_FROM" DataField="SERIAL_NO_FROM"></asp:BoundField>
                            <asp:BoundField HeaderText="SERIAL_NO_TO" DataField="SERIAL_NO_TO"></asp:BoundField>
                            <asp:BoundField HeaderText="SERVICE_TAX_ITZ" DataField="SERVICE_TAX_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="TDS_ITZ" DataField="TDS_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="TOTAL_AMT_DED_ITZ" DataField="TOTAL_AMT_DED_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="USER_NAME_ITZ" DataField="USER_NAME_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUND_TYPE_ITZ" DataField="REFUND_TYPE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="TRANS_TYPE" DataField="TRANS_TYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="AVAILABLE_BAL_ITZ" DataField="AVAILABLE_BAL_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="ACC_TYPE_NAME_ITZ" DataField="ACC_TYPE_NAME_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="MESSAGE_ITZ" DataField="MESSAGE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="MERCHANT_KEY_ITZ" DataField="MERCHANT_KEY_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="ERROR_CODE_ITZ" DataField="ERROR_CODE_ITZ"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATED_DATE" DataField="CREATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="UPDATED_DATE" DataField="UPDATED_DATE"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divReIssueIntl" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">ReIssueIntl
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="ReIssueIntl" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="pnr_locator" DataField="pnr_locator"></asp:BoundField>
                            <asp:BoundField HeaderText="Tkt_No" DataField="Tkt_No"></asp:BoundField>
                            <asp:BoundField HeaderText="Sector" DataField="Sector"></asp:BoundField>
                            <asp:BoundField HeaderText="FlightNo" DataField="FlightNo"></asp:BoundField>
                            <asp:BoundField HeaderText="departure" DataField="departure"></asp:BoundField>
                            <asp:BoundField HeaderText="destination" DataField="destination"></asp:BoundField>
                            <asp:BoundField HeaderText="Title" DataField="Title"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_fname" DataField="pax_fname"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_lname" DataField="pax_lname"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_type" DataField="pax_type"></asp:BoundField>
                            <asp:BoundField HeaderText="Booking_date" DataField="Booking_date"></asp:BoundField>
                            <asp:BoundField HeaderText="departure_date" DataField="departure_date"></asp:BoundField>
                            <asp:BoundField HeaderText="UserID" DataField="UserID"></asp:BoundField>
                            <asp:BoundField HeaderText="Agency_Name" DataField="Agency_Name"></asp:BoundField>
                            <asp:BoundField HeaderText="Block_Issue" DataField="Block_Issue"></asp:BoundField>
                            <asp:BoundField HeaderText="Base_Fare" DataField="Base_Fare"></asp:BoundField>
                            <asp:BoundField HeaderText="Tax" DataField="Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="YQ" DataField="YQ"></asp:BoundField>
                            <asp:BoundField HeaderText="Service_Tax" DataField="Service_Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="Tran_Fees" DataField="Tran_Fees"></asp:BoundField>
                            <asp:BoundField HeaderText="Discount" DataField="Discount"></asp:BoundField>
                            <asp:BoundField HeaderText="CB" DataField="CB"></asp:BoundField>
                            <asp:BoundField HeaderText="TDS" DataField="TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFare" DataField="TotalFare"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFareAfterDiscount" DataField="TotalFareAfterDiscount"></asp:BoundField>
                            <asp:BoundField HeaderText="ReIssueCharge" DataField="ReIssueCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceCharge" DataField="ServiceCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="FareDiff" DataField="FareDiff"></asp:BoundField>
                            <asp:BoundField HeaderText="RegardingIssue" DataField="RegardingIssue"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="SubmitDate" DataField="SubmitDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveID" DataField="ExecutiveID"></asp:BoundField>
                            <asp:BoundField HeaderText="Deptime" DataField="Deptime"></asp:BoundField>
                            <asp:BoundField HeaderText="AirPnr" DataField="AirPnr"></asp:BoundField>
                            <asp:BoundField HeaderText="RejectionComment" DataField="RejectionComment"></asp:BoundField>
                            <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                            <asp:BoundField HeaderText="VC" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderID" DataField="OrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="ReIssueID" DataField="ReIssueID"></asp:BoundField>
                            <asp:BoundField HeaderText="Trip" DataField="Trip"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="AcceptDate" DataField="AcceptDate"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxID" DataField="PaxID"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTRID" DataField="DISTRID"></asp:BoundField>
                            <asp:BoundField HeaderText="SCSRVTAX" DataField="SCSRVTAX"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divCancellationIntl" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">CancellationIntl
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="CancellationIntl" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="pnr_locator" DataField="pnr_locator"></asp:BoundField>
                            <asp:BoundField HeaderText="Tkt_No" DataField="Tkt_No"></asp:BoundField>
                            <asp:BoundField HeaderText="Sector" DataField="Sector"></asp:BoundField>
                            <asp:BoundField HeaderText="departure" DataField="departure"></asp:BoundField>
                            <asp:BoundField HeaderText="destination" DataField="destination"></asp:BoundField>
                            <asp:BoundField HeaderText="Title" DataField="Title"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_fname" DataField="pax_fname"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_lname" DataField="pax_lname"></asp:BoundField>
                            <asp:BoundField HeaderText="pax_type" DataField="pax_type"></asp:BoundField>
                            <asp:BoundField HeaderText="Booking_date" DataField="Booking_date"></asp:BoundField>
                            <asp:BoundField HeaderText="departure_date" DataField="departure_date"></asp:BoundField>
                            <asp:BoundField HeaderText="UserID" DataField="UserID"></asp:BoundField>
                            <asp:BoundField HeaderText="Agency_Name" DataField="Agency_Name"></asp:BoundField>
                            <asp:BoundField HeaderText="Block_Cancel" DataField="Block_Cancel"></asp:BoundField>
                            <asp:BoundField HeaderText="Base_Fare" DataField="Base_Fare"></asp:BoundField>
                            <asp:BoundField HeaderText="Tax" DataField="Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="YQ" DataField="YQ"></asp:BoundField>
                            <asp:BoundField HeaderText="Service_Tax" DataField="Service_Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="Tran_Fees" DataField="Tran_Fees"></asp:BoundField>
                            <asp:BoundField HeaderText="Discount" DataField="Discount"></asp:BoundField>
                            <asp:BoundField HeaderText="CB" DataField="CB"></asp:BoundField>
                            <asp:BoundField HeaderText="TDS" DataField="TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFare" DataField="TotalFare"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalFareAfterDiscount" DataField="TotalFareAfterDiscount"></asp:BoundField>
                            <asp:BoundField HeaderText="CancellationCharge" DataField="CancellationCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceCharge" DataField="ServiceCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundFare" DataField="RefundFare"></asp:BoundField>
                            <asp:BoundField HeaderText="RegardingCancel" DataField="RegardingCancel"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="SubmitDate" DataField="SubmitDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveID" DataField="ExecutiveID"></asp:BoundField>
                            <asp:BoundField HeaderText="RejectComment" DataField="RejectComment"></asp:BoundField>
                            <asp:BoundField HeaderText="VC" DataField="VC"></asp:BoundField>
                            <asp:BoundField HeaderText="Trip" DataField="Trip"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="DepTime" DataField="DepTime"></asp:BoundField>
                            <asp:BoundField HeaderText="ArrTime" DataField="ArrTime"></asp:BoundField>
                            <asp:BoundField HeaderText="AirLinePNR" DataField="AirLinePNR"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundID" DataField="RefundID"></asp:BoundField>
                            <asp:BoundField HeaderText="PaxID" DataField="PaxID"></asp:BoundField>
                            <asp:BoundField HeaderText="FlightNo" DataField="FlightNo"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateRemark" DataField="UpdateRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="ProjectID" DataField="ProjectID"></asp:BoundField>
                            <asp:BoundField HeaderText="MgtFee" DataField="MgtFee"></asp:BoundField>
                            <asp:BoundField HeaderText="CancelledBy" DataField="CancelledBy"></asp:BoundField>
                            <asp:BoundField HeaderText="BillNoCorp" DataField="BillNoCorp"></asp:BoundField>
                            <asp:BoundField HeaderText="BillNoAir" DataField="BillNoAir"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTRID" DataField="DISTRID"></asp:BoundField>
                            <asp:BoundField HeaderText="SCSRVTAX" DataField="SCSRVTAX"></asp:BoundField>
                            <asp:BoundField HeaderText="CancelStatus" DataField="CancelStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="PgCharges" DataField="PgCharges"></asp:BoundField>
                            <asp:BoundField HeaderText="PrevReissueID" DataField="PrevReissueID"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_YABookingLogs" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_YABookingLogs
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_YABookingLogs" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="Counter" DataField="Counter"></asp:BoundField>
                            <asp:BoundField HeaderText="Airline" DataField="Airline"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="PnrNo" DataField="PnrNo"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="BookReq" DataField="BookReq"></asp:BoundField>
                            <asp:BoundField HeaderText="BookRes" DataField="BookRes"></asp:BoundField>
                            <asp:BoundField HeaderText="RePriceReq" DataField="RePriceReq"></asp:BoundField>
                            <asp:BoundField HeaderText="RePriceRes" DataField="RePriceRes"></asp:BoundField>
                            <asp:BoundField HeaderText="OTHERS" DataField="OTHERS"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_TBOBookingLogs" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_TBOBookingLogs
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_TBOBookingLogs" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="Counter" DataField="Counter"></asp:BoundField>
                            <asp:BoundField HeaderText="Airline" DataField="Airline"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="PnrNo" DataField="PnrNo"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="BookReq" DataField="BookReq"></asp:BoundField>
                            <asp:BoundField HeaderText="BookRes" DataField="BookRes"></asp:BoundField>
                            <asp:BoundField HeaderText="RePriceReq" DataField="RePriceReq"></asp:BoundField>
                            <asp:BoundField HeaderText="RePriceRes" DataField="RePriceRes"></asp:BoundField>
                            <asp:BoundField HeaderText="OTHERS" DataField="OTHERS"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divGDSBookingLogs" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">GDSBookingLogs
                </td>
            </tr>
            <tr>
                <td>

                    <asp:GridView ID="GDSBookingLogs" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="Counter" DataField="Counter"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="Pnr" DataField="Pnr"></asp:BoundField>
                            <asp:BoundField HeaderText="SSReq" DataField="SSReq"></asp:BoundField>
                            <%-- <asp:TemplateField HeaderText="Departure">
                                <ItemTemplate>
                                    <div >
                                    </div>
                                    <asp:Literal ID="zerkername" runat="server" Text='<%# Eval("SSRes") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="SSRes" DataField="SSRes"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFReq1" DataField="PNBFReq1"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFRes1" DataField="PNBFRes1"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFReq2" DataField="PNBFReq2"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFRes2" DataField="PNBFRes2"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFReq3" DataField="PNBFReq3"></asp:BoundField>
                            <asp:BoundField HeaderText="PNBFRes3" DataField="PNBFRes3"></asp:BoundField>
                            <asp:BoundField HeaderText="SEReq" DataField="SEReq"></asp:BoundField>
                            <asp:BoundField HeaderText="SERes" DataField="SERes"></asp:BoundField>
                            <asp:BoundField HeaderText="Others" DataField="Others"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTicketingLog_GAL" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TicketingLog_GAL
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TicketingLog_GAL" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="Counter" DataField="Counter"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="SSReq" DataField="SSReq"></asp:BoundField>
                            <asp:BoundField HeaderText="SSRes" DataField="SSRes"></asp:BoundField>
                            <asp:BoundField HeaderText="PNRRTReq" DataField="PNRRTReq"></asp:BoundField>
                            <asp:BoundField HeaderText="PNRRTRes" DataField="PNRRTRes"></asp:BoundField>
                            <asp:BoundField HeaderText="DOCPRDReq" DataField="DOCPRDReq"></asp:BoundField>
                            <asp:BoundField HeaderText="DOCPRDRes" DataField="DOCPRDRes"></asp:BoundField>
                            <asp:BoundField HeaderText="PNRRT2Req" DataField="PNRRT2Req"></asp:BoundField>
                            <asp:BoundField HeaderText="PNRRT2Res" DataField="PNRRT2Res"></asp:BoundField>
                            <asp:BoundField HeaderText="SEReq" DataField="SEReq"></asp:BoundField>
                            <asp:BoundField HeaderText="SERes" DataField="SERes"></asp:BoundField>
                            <asp:BoundField HeaderText="CreateDate" DataField="CreateDate"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divPGLog" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">PGLog
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="PGLog" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="Id"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="TId" DataField="TId"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentId" DataField="AgentId"></asp:BoundField>
                            <asp:BoundField HeaderText="Request" DataField="Request"></asp:BoundField>
                            <asp:BoundField HeaderText="EncRequest" DataField="EncRequest"></asp:BoundField>
                            <asp:BoundField HeaderText="Response" DataField="Response"></asp:BoundField>
                            <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdatedDate" DataField="UpdatedDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ApiRequest" DataField="ApiRequest"></asp:BoundField>
                            <asp:BoundField HeaderText="ApiResponse" DataField="ApiResponse"></asp:BoundField>
                            <asp:BoundField HeaderText="ApiEncryptRequest" DataField="ApiEncryptRequest"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_RB_SEATBOOKINGDETAILS" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_RB_SEATBOOKINGDETAILS
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_RB_SEATBOOKINGDETAILS" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="TRIPID" DataField="TRIPID"></asp:BoundField>
                            <asp:BoundField HeaderText="SOURCE" DataField="SOURCE"></asp:BoundField>
                            <asp:BoundField HeaderText="DESTINATION" DataField="DESTINATION"></asp:BoundField>
                            <asp:BoundField HeaderText="BUSOPERATOR" DataField="BUSOPERATOR"></asp:BoundField>
                            <asp:BoundField HeaderText="SEATNO" DataField="SEATNO"></asp:BoundField>
                            <asp:BoundField HeaderText="LADIESSEAT" DataField="LADIESSEAT"></asp:BoundField>
                            <asp:BoundField HeaderText="FARE" DataField="FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="BOARDINGPOINT" DataField="BOARDINGPOINT"></asp:BoundField>
                            <asp:BoundField HeaderText="DROPPINGPOINT" DataField="DROPPINGPOINT"></asp:BoundField>
                            <asp:BoundField HeaderText="JOURNEYDATE" DataField="JOURNEYDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="PAXNAME" DataField="PAXNAME"></asp:BoundField>
                            <asp:BoundField HeaderText="GENDER" DataField="GENDER"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_IDTYPE" DataField="PRIMARY_PAX_IDTYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_IDNUMBER" DataField="PRIMARY_PAX_IDNUMBER"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_PAXMOB" DataField="PRIMARY_PAX_PAXMOB"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_PAXEMAIL" DataField="PRIMARY_PAX_PAXEMAIL"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENTID" DataField="AGENTID"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTR_ID" DataField="DISTR_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="ADMIN_MARKUP" DataField="ADMIN_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENT_MARKUP" DataField="AGENT_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTR_MARKUP" DataField="DISTR_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="ADMIN_COMM" DataField="ADMIN_COMM"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTR_COMM" DataField="DISTR_COMM"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_TDS" DataField="TA_TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_TDS" DataField="DI_TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="TOTAL_FARE" DataField="TOTAL_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_TOT_FARE" DataField="TA_TOT_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_TOT_FARE" DataField="DI_TOT_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_NET_FARE" DataField="TA_NET_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_NET_FARE" DataField="DI_NET_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="STAFF_ID" DataField="STAFF_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="EXEC_ID" DataField="EXEC_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="BOOKINGSTATUS" DataField="BOOKINGSTATUS"></asp:BoundField>
                            <asp:BoundField HeaderText="TICKETNO" DataField="TICKETNO"></asp:BoundField>
                            <asp:BoundField HeaderText="PNR" DataField="PNR"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCELLATIONCHARGE" DataField="CANCELLATIONCHARGE"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUNDEDAMOUNT" DataField="REFUNDEDAMOUNT"></asp:BoundField>
                            <asp:BoundField HeaderText="PARTIALCANCEL" DataField="PARTIALCANCEL"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATEDDATE" DataField="CREATEDDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="UPDATEDDATE" DataField="UPDATEDDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="REJECT_REMARK" DataField="REJECT_REMARK"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_IDPROOF_REQUIRED" DataField="PRIMARY_PAX_IDPROOF_REQUIRED"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARYPAX_ADDRESS" DataField="PRIMARYPAX_ADDRESS"></asp:BoundField>
                            <asp:BoundField HeaderText="ISPRIMARY" DataField="ISPRIMARY"></asp:BoundField>
                            <asp:BoundField HeaderText="YFLAG" DataField="YFLAG"></asp:BoundField>
                            <asp:BoundField HeaderText="YATRA_PNR" DataField="YATRA_PNR"></asp:BoundField>
                            <asp:BoundField HeaderText="TRIP_TYPE" DataField="TRIP_TYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="ORIGINAL_FARE" DataField="ORIGINAL_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="PROVIDER_NAME" DataField="PROVIDER_NAME"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCEL_POLICY" DataField="CANCEL_POLICY"></asp:BoundField>
                            <asp:BoundField HeaderText="PRIMARY_PAX_NAME" DataField="PRIMARY_PAX_NAME"></asp:BoundField>
                            <asp:BoundField HeaderText="USERID" DataField="USERID"></asp:BoundField>
                            <asp:BoundField HeaderText="PAX_ID" DataField="PAX_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="PAX_AGE" DataField="PAX_AGE"></asp:BoundField>
                            <asp:BoundField HeaderText="CancellationId" DataField="CancellationId"></asp:BoundField>
                            <asp:BoundField HeaderText="BOOKING_REF" DataField="BOOKING_REF"></asp:BoundField>
                            <asp:BoundField HeaderText="PAX_TITLE" DataField="PAX_TITLE"></asp:BoundField>
                            <asp:BoundField HeaderText="Paymentmode" DataField="Paymentmode"></asp:BoundField>
                            <asp:BoundField HeaderText="ModifyStatus" DataField="ModifyStatus"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_RB_SeatDetails" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_RB_SeatDetails
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_RB_SeatDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="TRIPID" DataField="TRIPID"></asp:BoundField>
                            <asp:BoundField HeaderText="SOURCE" DataField="SOURCE"></asp:BoundField>
                            <asp:BoundField HeaderText="DESTINATION" DataField="DESTINATION"></asp:BoundField>
                            <asp:BoundField HeaderText="JOURNEYDATE" DataField="JOURNEYDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="BUSOPERATOR" DataField="BUSOPERATOR"></asp:BoundField>
                            <asp:BoundField HeaderText="SEATNO" DataField="SEATNO"></asp:BoundField>
                            <asp:BoundField HeaderText="LADIESSEAT" DataField="LADIESSEAT"></asp:BoundField>
                            <asp:BoundField HeaderText="FARE" DataField="FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="BOARDINGPOINT" DataField="BOARDINGPOINT"></asp:BoundField>
                            <asp:BoundField HeaderText="DROPINGPOINT" DataField="DROPINGPOINT"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENTID" DataField="AGENTID"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATED_DATE" DataField="CREATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="UPDATED_DATE" DataField="UPDATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="STATUS" DataField="STATUS"></asp:BoundField>
                            <asp:BoundField HeaderText="DIST_ID" DataField="DIST_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="ADMIN_MARKUP" DataField="ADMIN_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENT_MARKUP" DataField="AGENT_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTR_MARKUP" DataField="DISTR_MARKUP"></asp:BoundField>
                            <asp:BoundField HeaderText="ADMIN_COMM" DataField="ADMIN_COMM"></asp:BoundField>
                            <asp:BoundField HeaderText="DISTR_COMM" DataField="DISTR_COMM"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_TDS" DataField="TA_TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_TDS" DataField="DI_TDS"></asp:BoundField>
                            <asp:BoundField HeaderText="TOTAL_FARE" DataField="TOTAL_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_TOT_FARE" DataField="TA_TOT_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_TOT_FARE" DataField="DI_TOT_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="TA_NET_FARE" DataField="TA_NET_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="DI_NET_FARE" DataField="DI_NET_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="STAFF_ID" DataField="STAFF_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="EXEC_ID" DataField="EXEC_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="REJECT_REMARK" DataField="REJECT_REMARK"></asp:BoundField>
                            <asp:BoundField HeaderText="ISIDPROOF_REQUIRED" DataField="ISIDPROOF_REQUIRED"></asp:BoundField>
                            <asp:BoundField HeaderText="TRIP_TYPE" DataField="TRIP_TYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="ORIGINAL_FARE" DataField="ORIGINAL_FARE"></asp:BoundField>
                            <asp:BoundField HeaderText="PROVIDER_NAME" DataField="PROVIDER_NAME"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCEL_POLICY" DataField="CANCEL_POLICY"></asp:BoundField>
                            <asp:BoundField HeaderText="PARTIAL_CANCEL" DataField="PARTIAL_CANCEL"></asp:BoundField>
                            <asp:BoundField HeaderText="NOOF_PAX" DataField="NOOF_PAX"></asp:BoundField>
                            <asp:BoundField HeaderText="BUS_TYPE" DataField="BUS_TYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingRefNo" DataField="BookingRefNo"></asp:BoundField>
                            <asp:BoundField HeaderText="Arr_Time" DataField="Arr_Time"></asp:BoundField>
                            <asp:BoundField HeaderText="Dept_Time" DataField="Dept_Time"></asp:BoundField>
                            <asp:BoundField HeaderText="Dur_Time" DataField="Dur_Time"></asp:BoundField>
                            <asp:BoundField HeaderText="USERID" DataField="USERID"></asp:BoundField>
                            <asp:BoundField HeaderText="AC_NONAC" DataField="AC_NONAC"></asp:BoundField>
                            <asp:BoundField HeaderText="SEAT_TYPE" DataField="SEAT_TYPE"></asp:BoundField>
                            <asp:BoundField HeaderText="totalFareWithTaxes" DataField="totalFareWithTaxes"></asp:BoundField>
                            <asp:BoundField HeaderText="serviceTaxAmount" DataField="serviceTaxAmount"></asp:BoundField>
                            <asp:BoundField HeaderText="ModifyStatus" DataField="ModifyStatus"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_RB_SEATBOOKREQUESTRESPONSE" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_RB_SEATBOOKREQUESTRESPONSE
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_RB_SEATBOOKREQUESTRESPONSE" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="BLOCK_KEY" DataField="BLOCK_KEY"></asp:BoundField>
                            <asp:BoundField HeaderText="TIN_NO" DataField="TIN_NO"></asp:BoundField>
                            <asp:BoundField HeaderText="BOOK_REQUEST" DataField="BOOK_REQUEST"></asp:BoundField>
                            <asp:BoundField HeaderText="BOOK_RESPONSE" DataField="BOOK_RESPONSE"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENTID" DataField="AGENTID"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATEDDATE" DataField="CREATEDDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="PROVIDER_NAME" DataField="PROVIDER_NAME"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_RB_CANCELLATIONDETAIL" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_RB_CANCELLATIONDETAIL
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_RB_CANCELLATIONDETAIL" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="TRIPID" DataField="TRIPID"></asp:BoundField>
                            <asp:BoundField HeaderText="SEATNO" DataField="SEATNO"></asp:BoundField>
                            <asp:BoundField HeaderText="PAXNAME" DataField="PAXNAME"></asp:BoundField>
                            <asp:BoundField HeaderText="GENDER" DataField="GENDER"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENTID" DataField="AGENTID"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCELLATIONID" DataField="CANCELLATIONID"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUND_STATUS" DataField="REFUND_STATUS"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUND_AMT" DataField="REFUND_AMT"></asp:BoundField>
                            <asp:BoundField HeaderText="APIREFUND_AMT" DataField="APIREFUND_AMT"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUND_SERVICECHRG" DataField="REFUND_SERVICECHRG"></asp:BoundField>
                            <asp:BoundField HeaderText="ACCEPTEDBY" DataField="ACCEPTEDBY"></asp:BoundField>
                            <asp:BoundField HeaderText="ACCEPTEDDATE" DataField="ACCEPTEDDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUNDEDBY" DataField="REFUNDEDBY"></asp:BoundField>
                            <asp:BoundField HeaderText="REFUNDEDDATE" DataField="REFUNDEDDATE"></asp:BoundField>
                            <asp:BoundField HeaderText="CancelCharge" DataField="CancelCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="PAYMENT_MODE" DataField="PAYMENT_MODE"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATED_DATE" DataField="CREATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="API_CANCELLATION_STATUS" DataField="API_CANCELLATION_STATUS"></asp:BoundField>
                            <asp:BoundField HeaderText="PNR" DataField="PNR"></asp:BoundField>
                            <asp:BoundField HeaderText="RejectRemark" DataField="RejectRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveID" DataField="ExecutiveID"></asp:BoundField>
                            <asp:BoundField HeaderText="EasyRefundID" DataField="EasyRefundID"></asp:BoundField>
                            <asp:BoundField HeaderText="ExceRemark" DataField="ExceRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="EASYTRANCODE_ITZ" DataField="EASYTRANCODE_ITZ"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divTBL_RB_CANCELREQUESTRESPONSE" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">TBL_RB_CANCELREQUESTRESPONSE
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="TBL_RB_CANCELREQUESTRESPONSE" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="ORDERID" DataField="ORDERID"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCELSEAT_REQUEST" DataField="CANCELSEAT_REQUEST"></asp:BoundField>
                            <asp:BoundField HeaderText="CANCELSEAT_RESPONSE" DataField="CANCELSEAT_RESPONSE"></asp:BoundField>
                            <asp:BoundField HeaderText="AGENT_ID" DataField="AGENT_ID"></asp:BoundField>
                            <asp:BoundField HeaderText="CREATED_DATE" DataField="CREATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="UPDATED_DATE" DataField="UPDATED_DATE"></asp:BoundField>
                            <asp:BoundField HeaderText="PROVIDER_NAME" DataField="PROVIDER_NAME"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>



    <div id="divT_HTL_HotelBookingHeader" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_HotelBookingHeader
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_HotelBookingHeader" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrderID" DataField="OrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingID" DataField="BookingID"></asp:BoundField>
                            <asp:BoundField HeaderText="ConfirmationNo" DataField="ConfirmationNo"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="DiscountAmt" DataField="DiscountAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalCost" DataField="TotalCost"></asp:BoundField>
                            <asp:BoundField HeaderText="NetCost" DataField="NetCost"></asp:BoundField>
                            <asp:BoundField HeaderText="BaseRate" DataField="BaseRate"></asp:BoundField>
                            <asp:BoundField HeaderText="Tax" DataField="Tax"></asp:BoundField>
                            <asp:BoundField HeaderText="ExtraGuestTax" DataField="ExtraGuestTax"></asp:BoundField>
                            <asp:BoundField HeaderText="Markup" DataField="Markup"></asp:BoundField>
                            <asp:BoundField HeaderText="ExchangeRate" DataField="ExchangeRate"></asp:BoundField>
                            <asp:BoundField HeaderText="CheckIN" DataField="CheckIN"></asp:BoundField>
                            <asp:BoundField HeaderText="CheckOut" DataField="CheckOut"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="StarRating" DataField="StarRating"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomCount" DataField="RoomCount"></asp:BoundField>
                            <asp:BoundField HeaderText="NightCount" DataField="NightCount"></asp:BoundField>
                            <asp:BoundField HeaderText="AdultCount" DataField="AdultCount"></asp:BoundField>
                            <asp:BoundField HeaderText="ChildCount" DataField="ChildCount"></asp:BoundField>
                            <asp:BoundField HeaderText="DiscounttMsg" DataField="DiscounttMsg"></asp:BoundField>
                            <asp:BoundField HeaderText="TripType" DataField="TripType"></asp:BoundField>
                            <asp:BoundField HeaderText="Provider" DataField="Provider"></asp:BoundField>
                            <asp:BoundField HeaderText="IP" DataField="IP"></asp:BoundField>
                            <asp:BoundField HeaderText="Currency" DataField="Currency"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveID" DataField="ExecutiveID"></asp:BoundField>
                            <asp:BoundField HeaderText="Remark" DataField="Remark"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundAmt" DataField="RefundAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="ModifyStatus" DataField="ModifyStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="YFlag" DataField="YFlag"></asp:BoundField>
                            <asp:BoundField HeaderText="LoginID" DataField="LoginID"></asp:BoundField>
                            <asp:BoundField HeaderText="AgencyName" DataField="AgencyName"></asp:BoundField>
                            <asp:BoundField HeaderText="CouponCode" DataField="CouponCode"></asp:BoundField>
                            <asp:BoundField HeaderText="CouponAmt" DataField="CouponAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="AdminMrkType" DataField="AdminMrkType"></asp:BoundField>
                            <asp:BoundField HeaderText="AdminMrkPer" DataField="AdminMrkPer"></asp:BoundField>
                            <asp:BoundField HeaderText="AdminMrkAmt" DataField="AdminMrkAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentMrkType" DataField="AgentMrkType"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentMrkPer" DataField="AgentMrkPer"></asp:BoundField>
                            <asp:BoundField HeaderText="AgentMrkAmt" DataField="AgentMrkAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="VServiceTaxAmount" DataField="VServiceTaxAmount"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceTaxAmount" DataField="ServiceTaxAmount"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceTaxPer" DataField="ServiceTaxPer"></asp:BoundField>
                            <asp:BoundField HeaderText="CommisionAmt" DataField="CommisionAmt"></asp:BoundField>
                            <asp:BoundField HeaderText="CommisionPer" DataField="CommisionPer"></asp:BoundField>
                            <asp:BoundField HeaderText="CommisionType" DataField="CommisionType"></asp:BoundField>
                            <asp:BoundField HeaderText="CorporateId" DataField="CorporateId"></asp:BoundField>
                            <asp:BoundField HeaderText="CorporateName" DataField="CorporateName"></asp:BoundField>
                            <asp:BoundField HeaderText="LoyaltyCardNumber" DataField="LoyaltyCardNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="PRILocator" DataField="PRILocator"></asp:BoundField>
                            <asp:BoundField HeaderText="OwningPCC" DataField="OwningPCC"></asp:BoundField>
                            <asp:BoundField HeaderText="HRLocator" DataField="HRLocator"></asp:BoundField>
                            <asp:BoundField HeaderText="BSIataNumber" DataField="BSIataNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="LPANubmer" DataField="LPANubmer"></asp:BoundField>
                            <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode"></asp:BoundField>
                            <asp:BoundField HeaderText="PgTid" DataField="PgTid"></asp:BoundField>
                            <asp:BoundField HeaderText="PgCharges" DataField="PgCharges"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_HTL_HotelDetail" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_HotelDetail
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_HotelDetail" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrderID" DataField="OrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelCode" DataField="HotelCode"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelName" DataField="HotelName"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomName" DataField="RoomName"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomTypeCode" DataField="RoomTypeCode"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomRatePlane" DataField="RoomRatePlane"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomRPH" DataField="RoomRPH"></asp:BoundField>
                            <asp:BoundField HeaderText="Address" DataField="Address"></asp:BoundField>
                            <asp:BoundField HeaderText="CityCode" DataField="CityCode"></asp:BoundField>
                            <asp:BoundField HeaderText="CountryCode" DataField="CountryCode"></asp:BoundField>
                            <asp:BoundField HeaderText="CountryName" DataField="CountryName"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelLocation" DataField="HotelLocation"></asp:BoundField>
                            <asp:BoundField HeaderText="ContactNo" DataField="ContactNo"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelFax" DataField="HotelFax"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelImage" DataField="HotelImage"></asp:BoundField>
                            <asp:BoundField HeaderText="PerRoomPreNightRate_org" DataField="PerRoomPreNightRate_org"></asp:BoundField>
                            <asp:BoundField HeaderText="PerRoomPreNightRate_mrk" DataField="PerRoomPreNightRate_mrk"></asp:BoundField>
                            <asp:BoundField HeaderText="PerRoomWise_Guest" DataField="PerRoomWise_Guest"></asp:BoundField>
                            <asp:BoundField HeaderText="Inclusion" DataField="Inclusion"></asp:BoundField>
                            <asp:BoundField HeaderText="EssentialInfo" DataField="EssentialInfo"></asp:BoundField>
                            <asp:BoundField HeaderText="SharingBedding" DataField="SharingBedding"></asp:BoundField>
                            <asp:BoundField HeaderText="CancellationPoli" DataField="CancellationPoli"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="SearchType" DataField="SearchType"></asp:BoundField>
                            <asp:BoundField HeaderText="APISearchKey" DataField="APISearchKey"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomKey" DataField="RoomKey"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelKey" DataField="HotelKey"></asp:BoundField>
                            <asp:BoundField HeaderText="Smoking" DataField="Smoking"></asp:BoundField>
                            <asp:BoundField HeaderText="Refundable" DataField="Refundable"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelEmailId" DataField="HotelEmailId"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_HTL_GuestDetail" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_GuestDetail
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_GuestDetail" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrderID" DataField="OrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomNo" DataField="RoomNo"></asp:BoundField>
                            <asp:BoundField HeaderText="GType" DataField="GType"></asp:BoundField>
                            <asp:BoundField HeaderText="GTitle" DataField="GTitle"></asp:BoundField>
                            <asp:BoundField HeaderText="GFName" DataField="GFName"></asp:BoundField>
                            <asp:BoundField HeaderText="GLName" DataField="GLName"></asp:BoundField>
                            <asp:BoundField HeaderText="GPhoneNo" DataField="GPhoneNo"></asp:BoundField>
                            <asp:BoundField HeaderText="GEmail" DataField="GEmail"></asp:BoundField>
                            <asp:BoundField HeaderText="Country" DataField="Country"></asp:BoundField>
                            <asp:BoundField HeaderText="State" DataField="State"></asp:BoundField>
                            <asp:BoundField HeaderText="City" DataField="City"></asp:BoundField>
                            <asp:BoundField HeaderText="Address" DataField="Address"></asp:BoundField>
                            <asp:BoundField HeaderText="PinCode" DataField="PinCode"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="childAge" DataField="childAge"></asp:BoundField>
                            <asp:BoundField HeaderText="DOB" DataField="DOB"></asp:BoundField>
                            <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_HTL_RoomDetail" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_RoomDetail
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_RoomDetail" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="OrderID" DataField="OrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomTypeCode" DataField="RoomTypeCode"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomName" DataField="RoomName"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomPlanCode" DataField="RoomPlanCode"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomRPH" DataField="RoomRPH"></asp:BoundField>
                            <asp:BoundField HeaderText="AdultCount" DataField="AdultCount"></asp:BoundField>
                            <asp:BoundField HeaderText="ChildCount" DataField="ChildCount"></asp:BoundField>
                            <asp:BoundField HeaderText="ChildAge" DataField="ChildAge"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomNo" DataField="RoomNo"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Cots" DataField="Cots"></asp:BoundField>
                            <asp:BoundField HeaderText="Dicription" DataField="Dicription"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_HTL_Cancellation" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_Cancellation
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_Cancellation" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="CancID" DataField="CancID"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundID" DataField="RefundID"></asp:BoundField>
                            <asp:BoundField HeaderText="OrderId" DataField="OrderId"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingID" DataField="BookingID"></asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                            <asp:BoundField HeaderText="TotalCost" DataField="TotalCost"></asp:BoundField>
                            <asp:BoundField HeaderText="NetCost" DataField="NetCost"></asp:BoundField>
                            <asp:BoundField HeaderText="CheckIN" DataField="CheckIN"></asp:BoundField>
                            <asp:BoundField HeaderText="CheckOut" DataField="CheckOut"></asp:BoundField>
                            <asp:BoundField HeaderText="HotelName" DataField="HotelName"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomName" DataField="RoomName"></asp:BoundField>
                            <asp:BoundField HeaderText="CancelNight" DataField="CancelNight"></asp:BoundField>
                            <asp:BoundField HeaderText="FromCancelDate" DataField="FromCancelDate"></asp:BoundField>
                            <asp:BoundField HeaderText="ProviderRefund" DataField="ProviderRefund"></asp:BoundField>
                            <asp:BoundField HeaderText="CancelCharge" DataField="CancelCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="ServiceCharge" DataField="ServiceCharge"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundFare" DataField="RefundFare"></asp:BoundField>
                            <asp:BoundField HeaderText="RequestBy" DataField="RequestBy"></asp:BoundField>
                            <asp:BoundField HeaderText="RequestDate" DataField="RequestDate"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateDate" DataField="UpdateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="AcceptDate" DataField="AcceptDate"></asp:BoundField>
                            <asp:BoundField HeaderText="RequestRemark" DataField="RequestRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdateRemark" DataField="UpdateRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="RejectRemark" DataField="RejectRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="ExecutiveID" DataField="ExecutiveID"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundBy" DataField="RefundBy"></asp:BoundField>
                            <asp:BoundField HeaderText="UpdatedBy" DataField="UpdatedBy"></asp:BoundField>
                            <asp:BoundField HeaderText="TripType" DataField="TripType"></asp:BoundField>
                            <asp:BoundField HeaderText="Provider" DataField="Provider"></asp:BoundField>
                            <asp:BoundField HeaderText="RoomCount" DataField="RoomCount"></asp:BoundField>
                            <asp:BoundField HeaderText="NightCount" DataField="NightCount"></asp:BoundField>
                            <asp:BoundField HeaderText="AdultCount" DataField="AdultCount"></asp:BoundField>
                            <asp:BoundField HeaderText="ChildCount" DataField="ChildCount"></asp:BoundField>
                            <asp:BoundField HeaderText="CancallationPolicy" DataField="CancallationPolicy"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundDate" DataField="RefundDate"></asp:BoundField>
                            <asp:BoundField HeaderText="RefundRemark" DataField="RefundRemark"></asp:BoundField>
                            <asp:BoundField HeaderText="ConfirmationNo" DataField="ConfirmationNo"></asp:BoundField>
                            <asp:BoundField HeaderText="StarRating" DataField="StarRating"></asp:BoundField>
                            <asp:BoundField HeaderText="BookedBy" DataField="BookedBy"></asp:BoundField>
                            <asp:BoundField HeaderText="PgTitle" DataField="PgTitle"></asp:BoundField>
                            <asp:BoundField HeaderText="PgFirstName" DataField="PgFirstName"></asp:BoundField>
                            <asp:BoundField HeaderText="PgLastName" DataField="PgLastName"></asp:BoundField>
                            <asp:BoundField HeaderText="PgEmail" DataField="PgEmail"></asp:BoundField>
                            <asp:BoundField HeaderText="AgencyName" DataField="AgencyName"></asp:BoundField>
                            <asp:BoundField HeaderText="BookingDate" DataField="BookingDate"></asp:BoundField>
                            <asp:BoundField HeaderText="CancellationID" DataField="CancellationID"></asp:BoundField>
                            <asp:BoundField HeaderText="PgCharges" DataField="PgCharges"></asp:BoundField>
                            <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="divT_HTL_HotelBookingLog" visible="false" runat="server" style="overflow-y: scroll; overflow-x: scroll;">
        <table>
            <tr>
                <td style='font-size: 13px; text-align: left; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">T_HTL_HotelBookingLog
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="T_HTL_HotelBookingLog" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None"
                        PageSize="30">
                        <Columns>
                            <asp:BoundField HeaderText="HTLOrderID" DataField="HTLOrderID"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLSearchReq" DataField="HTLSearchReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLSearchResp" DataField="HTLSearchResp"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLProBookingReq" DataField="HTLProBookingReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLProBookingResp" DataField="HTLProBookingResp"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLBookingReq" DataField="HTLBookingReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLBookingResp" DataField="HTLBookingResp"></asp:BoundField>
                            <asp:BoundField HeaderText="CreateDate" DataField="CreateDate"></asp:BoundField>
                            <asp:BoundField HeaderText="Provider" DataField="Provider"></asp:BoundField>
                            <asp:BoundField HeaderText="LoginID" DataField="LoginID"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLCancellationReq" DataField="HTLCancellationReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLCancellationResp" DataField="HTLCancellationResp"></asp:BoundField>
                            <asp:BoundField HeaderText="CancellationDate" DataField="CancellationDate"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLAgreementReq" DataField="HTLAgreementReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLAgreementResp" DataField="HTLAgreementResp"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLDetailsReq" DataField="HTLDetailsReq"></asp:BoundField>
                            <asp:BoundField HeaderText="HTLDetailsResp" DataField="HTLDetailsResp"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

