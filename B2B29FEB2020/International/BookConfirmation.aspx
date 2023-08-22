<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false" CodeFile="BookConfirmation.aspx.vb" Inherits="FlightInt_BookConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language='javascript'>
        function callprint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=750,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html><head><title>Booking Details</title><style type='text/css'>	 .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" + prtContent.innerHTML + "</body></html>");
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
           // prtContent.innerHTML = strOldOne;
        }
    </script>
     <div  ></div>
     <table cellpadding="0" cellspacing="0" class="w80 auto boxshadow" style="padding-top:20px;">
        
        <tr>
             <td style="padding: 20px;">
                <div class="w20 rgt">
                    <a href='javascript:;' class='lnk' onclick='javascript:callprint("divprint");'>Click
                        Here To Print</a></div>
                <div id='divprint'>
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

