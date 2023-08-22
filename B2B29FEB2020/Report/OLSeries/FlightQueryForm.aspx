<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FlightQueryForm.aspx.vb"
    Inherits="SprReports_OLSeries_FlightQueryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script src="JS/JScript.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            color: #20313f;
            font-weight: bold;
            height: 20px;
        }
        .style2
        {
            font-family: Arial;
            font-size: 12px;
            height: 20px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="10" style="margin: auto" width="920px" align="center"
        id="tbl_query" runat="server">
        <tr>
            <td align="center" style="padding-top: 10px;" class="Heading">
                Post Your Query
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="10" width="920px" class="tbltbl">
                    <tr>
                        <td class="text1" height="20px" style="padding-left: 25px" width="160px">
                            Airline Name:
                        </td>
                        <td class="Textsmall">
                            <asp:Label ID="lblairline" runat="server"></asp:Label>
                        </td>
                        <td class="text1" width="160px">
                            &nbsp;Itinerary:
                        </td>
                        <td class="Textsmall">
                            <asp:Label ID="lblcode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text1" height="20px" style="padding-left: 25px">
                            Departure Date:
                        </td>
                        <td class="Textsmall">
                            <asp:Label ID="lbldeptdate" runat="server"></asp:Label>
                        </td>
                        <td class="text1">
                            Return Date:
                        </td>
                        <td class="Textsmall">
                            <asp:Label ID="lblretdate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="padding-left: 25px">
                            Available Seats:
                        </td>
                        <td class="style2">
                            <asp:Label ID="lblseats" runat="server"></asp:Label>
                        </td>
                        <td class="style1">
                            Sector:
                        </td>
                        <td class="style2">
                            <asp:Label ID="lblsector" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text1" height="20px" style="padding-left: 25px">
                            Amount:
                        </td>
                        <td class="Textsmall">
                            <asp:Label ID="lblamt" runat="server"></asp:Label>
                        </td>
                        <td class="text1">
                            No Of Adult:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtadult" runat="server" onkeypress="return ValidateNoPax(event);" style="width: 100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="text1" height="20px" style="padding-left: 25px">
                            No Of Child:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtchild" runat="server" onkeypress="return ValidateNoPax(event);" style="width: 100px"></asp:TextBox></td>
                        <td class="text1">
                            No Of Infant:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtinfant" runat="server" onkeypress="return ValidateNoPax(event);" style="width: 100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="text1" style="padding-left: 25px">
                            Contact Person:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtCntctPerson" runat="server"  style="width: 100px"></asp:TextBox></td>
                        <td class="text1">
                            Contact No: 
                        </td>
                        <td>
                        &nbsp;<asp:TextBox ID="txtCntctPersonNo" runat="server" onkeypress="return ValidateNoPax(event);"  style="width: 100px"></asp:TextBox></td>
                        
                    </tr>
                    <tr>
                    <td class="text1" style="padding-left: 25px">Contact Email Id:</td>
                    <td>&nbsp; <asp:TextBox ID="txtCntctEmailid" runat="server"  style="width: 100px"></asp:TextBox></td>
                                 
                                
                    </tr>
                    <tr>
                        <td class="text1" style="padding-left: 25px">
                            Remark:
                        </td>
                        <td colspan="3">
                            &nbsp;<asp:TextBox ID="txtremark" runat="server"   style="width: 400px; height: 40px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblsid" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblTrip" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblexecid" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btn_post" runat="server" OnClientClick="return ValidateSeries();"
                                Text="Post Query" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tbl_msg" runat="server">
        <tr>
            <td align="center" class="Heading">
                Query Posted Sucessfully
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
