<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookingSummaryHtl.aspx.vb"
    Inherits="Hotel_BookingSummaryHtl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link type="text/css" href="<%=ResolveUrl("css/HotelStyleSheet.css") %>" rel="stylesheet" />
     <script type="text/javascript" src="<%=ResolveUrl("../Scripts/jquery-1.4.4.min.js") %>"></script>
     <style>
      * {
          color:#7F7F7F;
          font-family:Arial,sans-serif;
          font-size:12px;
          font-weight:normal;
      }    
      #config{
          overflow: auto;
          margin-bottom: 10px;
      }
      .config{
          float: left;
          width: 200px;
          height: 250px;
          border: 1px solid #000;
          margin-left: 10px;
      }
      .config .title{
          font-weight: bold;
          text-align: center;
      }
      .config .barcode2D,
      #miscCanvas{
        display: none;
      }
      #submit{
          clear: both;
      }
      #barcodeTarget,
      #canvasTarget{
        margin-top: 20px;
      }        
    </style>
    
    <script src="JS/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="JS/jquery-barcode.js" type="text/javascript"></script>
    <script type="text/javascript">

function printSelection(node) {
    $(".CacncelSelection").hide();
  var content=node.innerHTML
  var pwin=window.open('','print_content','width=740,height=443');
  pwin.document.open();
  pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
  pwin.document.close();
  $(".CacncelSelection").show();
}

function printpage() { window.print(); }

function generateBarcode(value) {
    var btype = "code128";
    var renderer = "css";

    var quietZone = false;
    if ($("#quietzone").is(':checked') || $("#quietzone").attr('checked')) {
        quietZone = true;
    }

    var settings = {
        output: renderer,
        bgColor: "#FFFFFF",
        color: "#000000",
        barWidth: "1",
        barHeight: "110",
        moduleSize: "5",
        posX: "10",
        posY: "20",
        addQuietZone: "1"
    };

    $("#canvasTarget").hide();
    $("#barcodeTarget").html("").show().barcode(value, btype, settings);
} 
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" align="center" width="70%">
        <tr>
            <td id="hidtktCopy">
                <asp:Label runat="server" ID="lbl_summary"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="margin: 10px auto; border: 1px #223e53 solid; width: 98%; background-color: Window;
        padding: 10px;">
        <table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#223e53" style="height: 38px" align="center">
            <tr>
                <td width="13%" style="color: Silver; font-size: 12px;">
                    <strong style="padding-left: 20px">Send E-Mail:</strong>
                </td>
                <td width="31%" style="color: Silver; font-size: 12px; padding-left: 15px;">
                    Email-ID :
                    <asp:TextBox ID="txt_email" runat="server"  Width="155px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_email"
                        ErrorMessage="*" ForeColor="#EC2F2F" Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
                <td width="49%">
                    <asp:Button ID="btn" runat="server" Text="Send" OnClick="btn_Click" Width="49px" Height="22px"></asp:Button>
                    <asp:Label ID="mailmsg" runat="server" ForeColor="White"></asp:Label>
                    <span style="text-align: left; color: #EC2F2F">
                        <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txt_email"
                            ValidationExpression=".*@.*\..*" ErrorMessage="*Invalid E-Mail ID." Display="dynamic">*Invalid E-Mail ID.</asp:RegularExpressionValidator>
                    </span>
                </td>
                <td align="center">
                          <a href="" onclick="printSelection(document.getElementById('hidtktCopy'));return false">
                        <img src='../Images/print_booking.jpg' /></a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
