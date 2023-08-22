<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Update_Agent.aspx.vb" Inherits="Update_Agent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Grade</title>
     <style>
        input[type="text"], input[type="password"], select, radio, fieldset,textarea
        {
            border: 1px solid #999999;
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
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/main2.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function validatetype() {
            if (document.getElementById("ddl_type").value == "--Select Type--") 
            {
                alert('Please Select Agent Type');
                document.getElementById("ddl_type").focus();
                return false;
            }
            if (confirm("Are You Sure!!"))
                return true;
            return false;
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="5" cellspacing="5" width="100%">
        <tr>
            <td style="padding-right: 17px">
                <fieldset style="padding: 5px 5px 5px 10px; width: 100%;">
                    <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;
                        text-align: center;">Agency Infromation</legend>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="style1" style="font-weight: bold; color: #000000;" width="80"
                                style="font-weight: bold">
                                Credit Limit&nbsp; :</td>
                            <td id="td_CrLimit" runat="server" width="80" class="style1" 
                                style="color: #000000">
                            </td>
                            <td class="style1" style="font-weight: bold; color: #000000;" width="120">
                                User ID
                                :</td>
                            <td id="td_AgentID" runat="server" width="90" class="style1" 
                                style="color: #000000">
                            </td>
                            <td class="style1" style="font-weight: bold; color: #000000;" width="100">
                                Agency Name
                                :</td>
                            <td style="color: #000000">
                            <asp:TextBox ID="txt_AgencyName"  runat="server" Enabled="False" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="style1" style="font-weight: bold; color: #000000;">
                                Tds
                                :</td>
                            <td id="td_tds" runat="server" 
                                style="color: #000000;">
                            </td>
                             <td class="style1" style="font-weight: bold; color: #000000;" width="120">
                                Agency ID
                                :</td>
                            <td id="td_AgencyID" runat="server" width="90" class="style1" 
                                style="color: #000000">
                            </td>
                            <td class="style1" style="font-weight: bold; color: #000000;" width="150">
                                Last Transaction Date
                                :</td>
                            <td id="td_LTDate" runat="server" width="120" style="color: #000000" 
                                class="style1" colspan="3">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 17px">
                <fieldset style="border: thin solid #999999;   padding: 5px 5px 5px 10px; width: 100%;">
                    <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;
                        text-align: center;">Agency Address</legend>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="90px" style="font-weight: bold; color: #000000;">
                        Address
                        </td>
                            <td height="15" colspan="3">
                                <asp:TextBox ID="txt_Address" runat="server" TextMode="MultiLine" Width="500px" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <%--  <td class="style1" width="120">
                        City
                        </td>
                        <td id="td_City" runat="server" width="100" class="fltdtls" style="color: #000000" >
                      
                        </td>
                         <td class="style1" width="100">
                        State
                        </td>
                        <td id="td_State" runat="server" class="fltdtls" style="color: #000000" >
                      
                        </td>--%>
                        </tr>
                        <tr>
                         <td class="fltdtls" style="font-weight: bold; color: #000000;" height="25px">
                        City
                        </td>
                            <td height="15">
                                <asp:TextBox ID="txt_City" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                           <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 10px;" 
                                    height="25px" width="100">
                                State</td>
                            <td height="15">
                                <asp:TextBox ID="txt_State" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                        <td class="fltdtls" style="font-weight: bold; color: #000000;">
                        
                             Zip Code</td>
                            <td width="120" 
                                height="15">
                                 <asp:TextBox ID="txt_zip" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 10px;" 
                                    height="25px" width="100">
                        Country
                        </td>
                            <td height="15">
                                <asp:TextBox ID="txt_Country" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 17px">
                <fieldset style="border: thin solid #999999;   padding-left: 10px; width: 100%;">
                    <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;
                        text-align: center;">Agent Details</legend>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="style1" style="font-weight: bold; color: #000000;" width="90px">
                                Name
                            </td>
                            <td width="250px" class="style1">
                              <asp:TextBox ID="txt_title" runat="server" Enabled="False" Width="40px"></asp:TextBox>  &nbsp;<asp:TextBox 
                                    ID="txt_Fname" runat="server" Enabled="False" Width="80px"></asp:TextBox>&nbsp;<asp:TextBox 
                                    ID="txt_Lname" runat="server" Enabled="False" Width="80px"></asp:TextBox></td>
                            <td class="style1" 
                                style="font-weight: bold; color: #000000; padding-left: 10px;" 
                                width="90px">
                                Mobile
                            </td>
                            <td width="120" class="style1" >
                              <asp:TextBox ID="txt_Mobile" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                            </td>
                            <td class="style1" 
                                style="font-weight: bold; color: #000000; padding-left: 10px;" width="80">
                                Email
                            
                            </td>
                            <td class="style1">
                              <asp:TextBox ID="txt_Email" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fltdtls" style="font-weight: bold; color: #000000;" height="25">
                                Fax No
                            </td>
                            <td width="120px">
                              <asp:TextBox ID="txt_Fax" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                           <td class="fltdtls" 
                                style="font-weight: bold; color: #000000; padding-left: 10px;" height="25">
  Pan No
                            </td>
                            <td >
                              <asp:TextBox ID="txt_Pan" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                            </td>
                            <td class="fltdtls" 
                                style="font-weight: bold; color: #000000; padding-left: 10px; display:none;" height="25" 
                                width="80" id="td_pwd" runat="server">
                                Password
                            
                            </td>
                            <td class="fltdtls" style="color: #000000;display:none;">
                             <asp:TextBox ID="txt_pwd"  runat="server" Enabled="False"></asp:TextBox>
                            </td>
                             
                        </tr>
                        
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 17px" id="td_update" runat="server">
                <fieldset style="border: thin solid #999999;   padding-left: 10px; width: 100%;">
                    <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;
                        text-align: center;">Update Details</legend>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 5px;"
                                height="25" width="90px">
                                Type
                            </td>
                            <td width="100px">
                                <asp:DropDownList ID="ddl_type" runat="server" Width="110px">
                                </asp:DropDownList>
                            </td>
                            <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 10px;"
                                height="25" width="140px">
                                Sales Ref
                            </td>
                            <td width="130">
                                <asp:TextBox ID="txt_saleref" runat="server"></asp:TextBox>
                            </td>                         
                            <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 10px;"
                                height="25">
                            </td>
                        </tr>
                        <tr>
                         <td style="font-weight: bold; color: #000000; padding-left: 5px;">
                                Activation
                            </td>
                         <td width="110px">
                                <asp:DropDownList ID="ddl_activation" runat="server">
                                    <asp:ListItem Value="ACTIVE">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="NOT ACTIVE">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>                         
                            <td style="font-weight: bold; color: #000000; padding-left: 10px;">
                               Ticketing Activation
                            </td>
                            <td width="110px">
                                <asp:DropDownList ID="ddl_TicketingActiv" runat="server">
                                    <asp:ListItem Value="ACTIVE">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="NOT ACTIVE">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fltdtls" style="font-weight: bold; color: #000000; padding-left: 10px;"
                                height="25">
                                <asp:Button ID="btn_update" runat="server" Text="Update" CssClass="menu" OnClientClick="return validatetype()" />
                            </td>
                            </tr>
                          
                        
                    </table>
                </fieldset>
            </td>
        </tr>
        <%--<tr>
<td style="padding-right: 17px">
                    <fieldset style="border: thin solid #999999;   padding-left: 10px">
                        <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;">Agent Details</legend>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td class="style1" height="25">
                        Name
                        </td>
                        <td id="td_Name" runat="server" width="100"  >
                        
                        </td>
                         <td width="120">
                        Mobile
                        </td>
                        <td id="td2" runat="server" width="100"  >
                      
                        </td>
                         <td  width="100">
                        Email
                        </td>
                        <td id="td4" runat="server"  >
                      
                        </td>
                        </tr>
                        <tr>
                        <td height="25px" width="100">
                        Alternate Email
                        </td>
                        <td id="td_AEmail" runat="server" width="100" >
                        
                        </td>
                         <td class="fltdtls" style="font-weight: bold; color: #000000;" width="120">
                        
                             Fax</td>
                        <td id="td8" runat="server" width="100"  >
                      
                        </td>
                        <td class="fltdtls" style="font-weight: bold; color: #000000;">
                      
                            User ID</td>
                        <td id="td9" runat="server" width="100"  >
                      
                        </td>
                        </tr>
                        </table>
                    </fieldset>
                    
                </td>
</tr>--%>
    </table>
    </form>
</body>
</html>
