<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PassengerDetail.aspx.vb"
    Inherits="PassengerDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
     <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="90%" align="center">
        <tr>
            <td class="Heading" align="center" height="50px">
                Ticket Detail
            </td>
        </tr>
        <tr>
            <td style="border: thin solid #800000">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="Proxy" colspan="2">
                            &nbsp; Agent Detail
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                     <table border="0" cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                        <td class="TextBig" width="200px" height="20px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Agent ID
                        </td>
                        <td id="td_AgentID" runat="server" class="Text" width="215px">
                        </td>
                        <td class="TextBig" width="150px" height="20px">
                                        Available Card Limit                                     </td>
                                    <td id="td_CardLimit" runat="server" class="Text">
                                        &nbsp;</td>
                    </tr>
                     <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agent Name</td>
                                    <td id="td_AgentName" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig">
                                        Address
                                    </td>
                                    <td id="td_AgentAddress" runat="server" class="Text">
                                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Street</td>
                                    <td id="td_Street" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig">
                                        Mobile No
                                    </td>
                                    <td id="td_AgentMobNo" runat="server" class="Text">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email</td>
                                    <td id="td_Email" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig">
                                        Agency Name</td>
                                    <td  class="Text" id="td_AgencyName" runat="server">
                                        
                                        &nbsp;</td>
                                </tr>
                     </table>
                    </td>
                    
                    </tr>
                    <tr>
                        <td class="Proxy" colspan="2">
                            &nbsp; Booking Details
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td class="TextBig" width="250px" height="20">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Booking type :
                                    </td>
                                    <td id="td_BookingType" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px">
                                        &nbsp;&nbsp;&nbsp;Travel Type :
                                    </td>
                                    <td id="td_TravelType" runat="server" class="Text">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From :
                                    </td>
                                    <td id="td_From" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px">
                                        &nbsp;&nbsp;&nbsp;To :
                                    </td>
                                    <td id="td_To" runat="server" class="Text">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Departure Date :
                                    </td>
                                    <td id="td_DepartDate" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px" height="20px">
                                        &nbsp;&nbsp; Departure Time :
                                    </td>
                                    <td id="td_DepartTime" runat="server" class="Text">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Return Date :
                                    </td>
                                    <td id="td_RetDate" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px" height="20px">
                                        &nbsp;&nbsp;&nbsp;Return Time :
                                    </td>
                                    <td id="td_RetTime" runat="server" class="Text">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Proxy" colspan="2">
                            &nbsp; No of Passenger
                        </td>
                    </tr>
                    
                   <tr>
                   <td colspan="2">
                   <table width="100%" border="0" cellpadding="0" cellspacing="0">
                   
                    <tr>
                        <td class="TextBig" width="200px" height="20px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Adult :
                        </td>
                        <td id="td_Adult" runat="server" class="Text">
                        </td>
                         <td class="TextBig" width="200px" height="20px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Child :
                        </td>
                        <td id="td_Child" runat="server" class="Text">
                        </td>
                         <td class="TextBig" width="200px" height="20px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Infant :
                        </td>
                        <td id="td_Infrant" runat="server" class="Text">
                        </td>
                    </tr>
                    
                    
                   </table>
                   
                   </td>
                   </tr>
                    <tr>
                        <td class="Proxy" colspan="2">
                            &nbsp; Passenger Details
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="SubHeading" height="20px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="Text">
                                                    <asp:GridView ID="GridViewAdult" runat="server" AutoGenerateColumns="false" OnRowDataBound="RDBAdult"
                                                        Width="90%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SirName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Age" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="SubHeading" height="20px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="Text">
                                                    <asp:GridView ID="GridViewChild" runat="server" AutoGenerateColumns="false" Width="90%"
                                                        OnRowDataBound="RBDChild">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SirName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Age" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="SubHeading" height="20px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="Text">
                                                    <asp:GridView ID="GridViewInfrant" runat="server" AutoGenerateColumns="false" Width="90%"
                                                        OnRowDataBound="RDBInfrant">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SirName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SirName" runat="server" Text='<%#Eval("SirName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Age" runat="server" Text='<%#Eval("Age") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 30px">
                        </td>
                    </tr>
                    <tr>
                        <td class="Proxy" colspan="2">
                            &nbsp; Other Details
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Class :
                                    </td>
                                    <td id="td_Class" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Airline :
                                    </td>
                                    <td id="td_Airline" runat="server" class="Text">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Classes :
                                    </td>
                                    <td id="td_Classes" runat="server" class="Text">
                                    </td>
                                    <td class="TextBig" width="150px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Payment Mode :
                                    </td>
                                    <td id="td_PMode" runat="server" class="Text">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Remark :
                                    </td>
                                    <td id="td_Remark" runat="server" class="Text" colspan="3">
                                    </td>
                                </tr>
                                <tr runat="server" id="tr_reject">
                                    <td class="TextBig" width="200px" height="20px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reject Remark :
                                    </td>
                                    <td id="td_Reject" runat="server" class="Text" colspan="3">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
