<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="BookConfirmation.aspx.vb" Inherits="Domestic_BookConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="css/transtour.css" rel="stylesheet" type="text/css" />
    <link href="css/core_style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/itz.css" rel="stylesheet" />
    <link href="../CSS/newcss/main.css" rel="stylesheet" />
    <link href="../CSS/foundation.css" rel="stylesheet" />

<%--    <script src='../Hotel/JS/jquery-1.3.2.min.js' type='text/javascript'></script>--%>
    <script src='../Hotel/JS/jquery-barcode.js' type='text/javascript'></script>


      <style type="text/css">
        @media print {
.pri{
    background-color: white !important;
    -webkit-print-color-adjust: exact; 
}}

@media print {
     .pri font, .pri {
    color: white !important;
}}

@media print {
     .pri2 {
         background-color: #f1f1f1 !important;
    -webkit-print-color-adjust: exact; 
    
}}
    </style>



    <style type="text/css">
        @media print {
  body * {
    visibility: hidden;
  }
  #divprint, #divprint * {
    visibility: visible;
  }
  #divprint {
    position: absolute;
    left: 0;
    top: 0;
  }
}
      </style>  




    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            //var prtContent = document.getElementById(strid);
            //var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            //WinPrint.document.write("<html><head><title>Booking Details</title><link href='../css/foundation.css' rel='stylesheet' type='text/css' /><style type='text/css'>	 .{border:1px solid  #20313f; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" + prtContent.innerHTML + "</body></html>");
            //WinPrint.document.close();
            //WinPrint.focus();
            //WinPrint.print();
            //WinPrint.close();
            // prtContent.innerHTML = strOldOne;


            var prtContent = $('#' + strid);
            var sst = '<html><head><title>Ticket Details</title></style></head><body>';


            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');



            WinPrint.document.write('<html><head><title>Ticket Details</title>');

            WinPrint.document.write('</head><body>' + prtContent.html() + '</body></html>');


            ////prtContent11.innerHTML = sst + prtContent.innerHTML + "</body></html>";
            //WinPrint.document.write(prtContent11.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
    </script>
     <div style="margin-top:30px;"></div>
    <table cellpadding="0" cellspacing="0" class="w80 auto boxshadow" style="padding-top:20px; background-color:none !important;">
       
        <tr>
           
            <td style="padding: 20px;">
                <div class="w20 rgt" style="margin-top: -35px;">
                    <a href='javascript:;' class='lnk' onclick='javascript:callprint("divprint");' style="background: #828282;padding: 5px;border-radius: 25px;color: #fff;text-align: center;">Click
                        Here To Print</a></div>
                <div id="divprint" style="margin: 5px auto; border: 1px #eee solid; width: 98%; background-color: #FFFFFF; padding: 5px;">
              <asp:Label ID="lblTkt" runat="server"></asp:Label>



    
                </div>
            </td>
            
        </tr>
       
    </table>
         
         
         <style type="text/css" >
        
         thead, tbody, tfoot{
             background:none !important;
             border:none !important;
         }
         table {
             border:none;
         }
         
    </style>
</asp:Content>

