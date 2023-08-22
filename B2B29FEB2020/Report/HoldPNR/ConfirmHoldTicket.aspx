<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmHoldTicket.aspx.cs" Inherits="SprReports_HoldPNR_ConfirmHoldTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>     
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/JScript.js" type="text/javascript"></script>
    <link href="../../CSS/itz.css" rel="stylesheet" />

    <script type="text/javascript">
        var UrlBase = '/';
        function MyFunc() {
            alert("Request submitted successfully and executive updated within 45 minutes")
            window.open('HoldPnrReport.aspx', '_parent');
            window.close();
        }
        function TryAgain() {
            alert("try again")
            window.open('HoldPnrReport.aspx', '_parent');
            window.close();
        }

        function Cancel() {            
            window.open('HoldPnrReport.aspx', '_parent');
            window.close();
        }

        function ConfirmMsg() {
            var x = confirm("Are you sure you want to issue ticket?");
            if (x)
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UP" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div style="padding-top: 5px; padding-bottom: 5px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-right: 17px">
                                            <fieldset style="border: thin solid #004b91; padding-left: 10px">
                                                <legend style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #004b91;">
                                                    Issue Hold Booking Ticket</legend>
                                                <table border="0" cellpadding="2" cellspacing="2" width="100%" runat="server" id="TblUpdate">                                                   
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;" align="left">
                                                                        Order Id: <asp:Label ID="lblOrderId" runat="server"></asp:Label><br />
                                                                    </td>
                                                                     <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;" align="left">
                                                                       Ticket Status: <asp:Label ID="lblStatus" runat="server"></asp:Label><br />
                                                                    </td>
                                                                    </tr>
                                                               
                                                                <tr> <td style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;" align="left">
                                                                        Sector:<asp:Label ID="lblSector" runat="server"></asp:Label> <br />
                                                                    </td>
                                                                    <td align="left" class="h2" style="font-family: arial, Helvetica, sans-serif; font-weight: bold; color: #000000;">
                                                                      Amount:<asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                                        <asp:Label ID="lblAgentId" runat="server" Visible="false"></asp:Label>
                                                                    </td></tr>
                                                                <tr>
                                                                    <td>
                                                                         <b><asp:Label ID="lblMsg" runat="server"  ForeColor="Red"></asp:Label></b> 
                                                                        <br />
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Issue Ticket" OnClick="btnSubmit_Click" CssClass="buttonfltbks" OnClientClick="return ConfirmMsg();"  />
                                                                      <%-- <asp:ImageButton ID="minus" runat="server" ImageUrl="../../Images/btn_minus.gif"
                                                                            OnClientClick="return ConfirmMsg();" />--%>

                                                                      

                                                                    </td></tr>
                                                                    
                                                                
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
               
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
            <ProgressTemplate>
                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                    padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                    z-index: 1000;">
                </div>
                <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                    z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                    font-weight: bold; color: #000000">
                    Please Wait....<br />
                    <br />
                    <img alt="loading" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                    <br />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    </form>
</body>
</html>
