<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketInformation.aspx.cs" Inherits="TicketHtml" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        body {
  padding: 14px;
  text-align: center;
}

table {
  width: 100%;
  /*margin: 20px auto;*/
  table-layout: auto;
}

.fixed {
  table-layout: fixed;
}

table,
td,
th {
  border-collapse: collapse;
}

th,
td {
  padding: 10px;
  /*border: solid 1px;*/
  text-align: left;
}
    </style>

</head>
<body>
    <div id="divtkt" style="width: 97%; background-color: #FFFFFF;">
        <div class="large-12 medium-12 small-12">

            <div id="divprint" style="margin: 5px auto;width: 100%; background-color: #FFFFFF; padding: 5px;">
                <div id="div_mail">
                    <div style="clear: both;"></div>
                    <span id="LabelTkt">
                        <br>
                        <br>
                        <table style="width: 100%; font-family: arial; margin-top: -38px;">
                            <tbody>


                                <tr>
                                    <td style="text-align: left; font-size: 11px;">
                                                        <img src="Custom_Design/images/logo_new.png" alt="Logo" style="height: 54px; width: 300px"/><br>
                                                    </td>
                                </tr>

                                <th style='background: #0952a4;color: #fff;text-align: left;'>
                                    eTicket Itineary / Receipt
                                </th>

                                <tr>
                                    
                                    <td>
                                        <table style="width: 100%; font-family: arial;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div style="font-weight: bold; font-size: 14px;">Tripforo</div>
                                                        301, Hare Krishna Complex,<br>
                                                        Behind City Gold Theater,<br>
                                                        Ashram Road, Ahmedabad - 380009<br>
                                                        Mobile - 7949109999<br>
                                                        Email - info@richaworldtravels.com<br>
                                                    </td>

                                                    
                                                    
                                                    <td>
                                                        <div id="barcodeTarget" style="float: right; padding: 0px; overflow: auto; width: 187px;" class="barcodeTarget">
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 10px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 4px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 4px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 4px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 4px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 2px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 4px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 3px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 3px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 1px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 1px"></div>
                                                            <div style="float: left; font-size: 0px; width: 0; border-left: 2px solid #000000; height: 110px;"></div>
                                                            <div style="float: left; font-size: 0px; background-color: #FFFFFF; height: 110px; width: 10px"></div>
                                                            <div style="clear: both; width: 100%; background-color: #FFFFFF; color: #000000; text-align: center; font-size: 10px; margin-top: 5px;">AL20013115263088 </div>
                                                        </div>
                                                        <canvas id="canvasTarget" style="width: 150px; height: 150px; display: none;"></canvas>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                                    <td style="width: 100%; text-align: justify; color: #231f20; font-size: 11px; text-decoration:underline;">This is travel itinerary and E-ticket receipt. You may need to show this receipt to enter the airport and/or to show return or onward travel to customs and immigration officials.</td>
                                                </tr>
                                <tr>
                                   
                                     
                                                <tr>
                                                    <td class="custom" style="text-align: left; color: #2b2b2b; font-weight: bold;" colspan="6">PNR &amp; Ticket Information</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="font-size: 12px; width: 100%">
                                                        <table style="width: 100%;" border="1">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;" colspan="2">GDS PNR</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;">Airline PNR</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;">Class</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;">Status</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;">Date Of Issue</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;">Fare Type</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size: 11px; text-align: left; padding: 5px;" colspan="2">W99YMS</td>
                                                                    <td style="font-size: 11px; text-align: left; padding: 5px;">W99YMS</td>
                                                                    <td style="font-size: 13px; text-align: left; padding: 5px;">Economy</td>
                                                                    <td style="font-size: 11px; text-align: left; padding: 5px;">Ticketed</td>
                                                                    <td style="font-size: 11px; text-align: left; padding: 5px;">31 Jan 2020</td>
                                                                    <td style="font-size: 13px; text-align: left; padding: 5px;">Refundable</td>
                                                                </tr>

</tbody>
                                                        </table>
                                                    </td>

                                                                <tr>
                                                                    <td style="text-align: left; color: #2b2b2b; font-weight: bold;" colspan="8">Passenger Information</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: #fff; width: 100%;">
                                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;" colspan="3">Passenger Name</td>
                                                                    <td style="font-size: 11px; font-weight: bold; text-align: left; padding: 5px;" colspan="2">Ticket Number</td>
                                                                    <td style="font-size: 11px; font-weight: bold; border-bottom: 1px solid #7f7f8c; text-align: left; padding: 5px;" colspan="2">Customer Info</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size: 11px; text-align: left; border-right: 1px solid #7f7f8c; padding: 5px; border-color: #cccce0;" colspan="3">MR  NIYAMUDDIN    ANSARI (ADT)</td>
                                                                    <td style="font-size: 11px; text-align: left; border-right: 1px solid #7f7f8c; padding: 5px; border-color: #cccce0;" colspan="2">214E426156</td>
                                                                    <td style="font-size: 11px; border-bottom: 1px solid #7f7f8c; text-align: left; border: 1px; padding: 5px;" colspan="2">8294795784<br>
                                                                        sarojkumarch@gmail.com</td>
                                                                </tr>
                                                                    </tbody>
                                                        </table>
                                                    </td>
                                                                    </tr>
                                                            
                                                </tr>
                                                <tr>
                                                   
                                                               
                                                                    <td style="text-align: left; color: #000; width: 25%; font-weight: bold;" colspan="1">Detailed Itinerary</td>
                                                               
                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="background-color: #fff; width: 100%;">
                                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tbody>
                                                                <tr style="">
                                                                    <td style="font-size: 10.5px; color: #424242; width: 10%; text-align: left; padding: 7px; font-weight: bold;">FLIGHT</td>
                                                                    <td style="font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 7px; font-weight: bold;">DEPART</td>
                                                                    <td style="font-size: 10.5px; color: #424242; width: 20%; text-align: left; padding: 7px; font-weight: bold;">ARRIVE</td>
                                                                    <td style="font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 7px; font-weight: bold;">DEPART AIRPORT</td>
                                                                    <td style="font-size: 10.5px; color: #424242; width: 25%; text-align: left; padding: 7px; font-weight: bold;">ARRIVE AIRPORT</td>
                                                                </tr>
                                                                <tr>
                                                                   <tr>
                                                                                    <td style="padding: 10px; font-size: 11px; width: 10%; text-align: left; vertical-align: top;">
                                                                                        <img alt="Logo Not Found" src="http://www.Tripforo.in/AirLogo/smSG.gif"><br>
                                                                                        SG 6284<br>
                                                                                        SpiceJet</td>
                                                                                    <td style="font-size: 11px; width: 20%; text-align: left; vertical-align: top;">12 Feb 20<br>
                                                                                        <br>
                                                                                        13 : 25</td>
                                                                                    <td style="font-size: 11px; width: 20%; text-align: left; vertical-align: top;">12 Feb 20<br>
                                                                                        <br>
                                                                                        16 : 05</td>
                                                                                    <td style="font-size: 11px; width: 25%; text-align: left; padding: 2px;">Patna                                             ( PAT)<br>
                                                                                        <br>
                                                                                        Patna Airport - Trml:1</td>
                                                                                    <td style="font-size: 11px; width: 25%; text-align: left; padding: 2px;">Mumbai                                             (BOM)<br>
                                                                                        <br>
                                                                                        Mumbai Airport - Trml:2</td>
                                                                                </tr>
                                                                </tr>
                                                                  <tr>
                                                                                    <td style="padding: 10px; font-size: 11px; width: 10%; text-align: left; vertical-align: top;">
                                                                                        <img alt="Logo Not Found" src="http://www.Tripforo.in/AirLogo/smSG.gif"><br>
                                                                                        SG 13<br>
                                                                                        SpiceJet</td>
                                                                                    <td style="font-size: 11px; width: 20%; text-align: left; vertical-align: top;">12 Feb 20<br>
                                                                                        <br>
                                                                                        23 : 05</td>
                                                                                    <td style="font-size: 11px; width: 20%; text-align: left; vertical-align: top;">13 Feb 20<br>
                                                                                        <br>
                                                                                        00 : 55</td>
                                                                                    <td style="font-size: 11px; width: 25%; text-align: left; padding: 2px;">Mumbai                                            ( BOM)<br>
                                                                                        <br>
                                                                                        Mumbai Airport - Trml:1</td>
                                                                                    <td style="font-size: 11px; width: 25%; text-align: left; padding: 2px;">Dubai                                              (DXB)<br>
                                                                                        <br>
                                                                                        Dubai Airport - Trml:2</td>
                                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                           
                                       
                                  
                                </tr>
                                <tr class="TRFI1 fclasshow">
                                    <td style=" color: #2b2b2b;font-weight: bold;text-align: left;">Redemption Details</td>
                                </tr>
                                <tr class="TRFI1 fclasshow">
                                    <td style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" border="1" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="font-size: 12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;">Pax Type</td>
                                                    <td style="font-size: 12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;">Pax Count</td>
                                                    <td style="font-size: 12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;">Base fare</td>
                                                    <td style="font-size: 12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;">Fuel Surcharge</td>
                                                    <td style="font-size: 12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;">Tax</td>
                                                    <td style="font-size: 12px; color: #424242; width: 10%; text-align: left; padding: 4px; font-weight: bold;">Trans Fee</td>
                                                    <td style="font-size: 12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;">Trans Charge</td>
                                                    <td style="font-size: 12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;">TOTAL</td>
                                                </tr>
                                                <tr class="TRFI1 fclasshow">
                                                    <td style="font-size: 11px; width: 10%; text-align: left; padding: 4px; vertical-align: top;">ADT</td>
                                                    <td style="font-size: 11px; width: 10%; text-align: left; padding: 4px; vertical-align: top;" id="td_adtcnt">1</td>
                                                    <td style="font-size: 11px; width: 15%; text-align: left; padding: 4px; vertical-align: top;">₹9592</td>
                                                    <td style="font-size: 11px; width: 15%; text-align: left; padding: 4px; vertical-align: top;">₹0</td>
                                                    <td style="font-size: 11px; width: 10%; text-align: left; padding: 4px; vertical-align: top;" id="td_taxadt">₹1401</td>
                                                    <td style="font-size: 11px; width: 10%; text-align: left; padding: 4px; vertical-align: top;">₹0</td>
                                                    <td style="font-size: 11px; width: 15%; text-align: left; padding: 4px; vertical-align: top;" id="td_tcadt">₹0</td>
                                                    <td style="font-size: 11px; width: 15%; text-align: left; padding: 4px; vertical-align: top;" id="td_adttot">₹10993</td>
                                                </tr>

                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="color: #000; font-size: 14px; width: 17%; text-align: left; vertical-align: top;">GRAND TOTAL</td>
                                                    <td style="color: #000; font-size: 14px; width: 10%; text-align: left; vertical-align: top;" id="td_grandtot">₹10993</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                 <%--   <td style="background-color: #cccce0;font-size: 14px; font-weight: bold; padding: 0px;">
                                        <table cellspacing="0" cellpadding="0" border="1" style="width: 100%;">
                                            <tbody>
                                                
                                            </tbody>
                                        </table>
                                    </td>--%>
                                </tr>
                               <%-- <tr class="TRFI1 fclasshow">
                                    
                                </tr>--%>
                                <tr>
                                    <td>
                                        <ul style="list-style-image: url(http://www.Tripforo.in/Images/bullet.png);">
                                            <li style="font-size: 10.5px;">Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=" background-color: #0952a4; color: #fff;  font-weight: bold;">TERMS AND CONDITIONS :</td>
                                </tr>
                                <tr>
                                    <td>
                                        <ul style="list-style-image: url(http://www.Tripforo.in/Images/bullet.png);">
                                            <li style="font-size: 10.5px;">Guests are requested to carry their valid photo identification for all guests, including children.</li>
                                            <li style="font-size: 10.5px;">We recommend check-in at least 2 hours prior to departure.</li>
                                            <li style="font-size: 10.5px;">Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</li>
                                            <li style="font-size: 10.5px;">Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</li>
                                            <li style="font-size: 10.5px;">Flight schedules are subject to change and approval by authorities.<br>
                                            </li>
                                            <li style="font-size: 10.5px;">Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.<br>
                                                Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.</li>
                                            <li style="font-size: 10.5px;">Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>
                                            <li style="font-size: 10.5px;">Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>
                                            <li style="font-size: 10.5px;">Cancellation amount will be charged as per airline rule.</li>
                                            <li style="font-size: 10.5px;">Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>
                                        </ul>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table style="width: 100%; font-family: arial;">
                            <tbody>
                                <tr>
                                    <td colspan="4" style="background-color: #0952a4; color: #fff; font-weight: bold;">BAGGAGE INFORMATION :</td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td colspan="2">Check-in baggage : 30 KG and Hand baggage : 7 KG</td>
                                </tr>
                            </tbody>
                        </table>
                        <script type="text/javascript"> decodeURI(window.location.search).substring(1).split('&'); var btype = 'code128'; var renderer = 'css'; var quietZone = false; if ($('#quietzone').is(':checked') || $('#quietzone').attr('checked')) { quietZone = true; } var settings = { output: renderer, bgColor: '#FFFFFF', color: '#000000', barWidth: '1', barHeight: '110', moduleSize: '5', posX: '10', posY: '20', addQuietZone: '1' }; $('#canvasTarget').hide(); $('#barcodeTarget').html('').show().barcode('AL20013115263088 ', btype, settings);  </script>
                    </span>
                    <div style="clear: both;"></div>

                </div>
            </div>


        </div>
    </div>
</body>
</html>
