<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="HotelItemDownload.aspx.vb" Inherits="SprReports_Hotel_HotelItemDownload" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Styles/jquery-ui-1.8.8.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Hotel/css/HotelStyleSheet.css" rel="stylesheet" type="text/css" />
     <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.8.custom.min.js" type="text/javascript"></script>
    <table cellpadding="0" cellspacing="0" align="center" class="tbltbl" style=" width: 560px;">
        <tr>
            <td  style="color: #004b91; font-size: 20px; font-weight: bold; padding-left: 20px;">
                Dowonload hotel item
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                <table cellpadding="11" cellspacing="11"  style="color: #004b91; font-size: 11px; font-weight: bold; width: 100%;">
                    <tr>
                        <td >
                            <input type="radio" name="D" value="rdbFullDownLoad" id="rdbFullDownLoad" onclick="ShowHidedates('rdbFullDownLoad')" />Full download
                            <input type="radio" name="D" value="rdbIncDownLoad" id="rdbIncDownLoad" 
                                checked="checked" onclick="ShowHidedates('rdbIncDownLoad')" />Incremental download
                            <input type="radio" name="D" value="rdbYestDownload" id="rdbYestDownload" onclick="ShowHidedates('rdbYestDownload')" /> Yesterday download
                            <input id="douwnloadeType" name="douwnloadeType" value="" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="datestbl" style="color: #004b91; font-size: 13px;">
                                <tr>
                                    <td >
                                        From:
                                    </td>
                                    <td style="color: #FF3300">
                                        <input type="text" name="From" id="From" class="txtCalander" value="" readonly="readonly" />
                                        <input id="hidtxtDepDate" name="hidtxtDepDate" value="" type="hidden" />
                                    </td>
                                    <td style="padding-left: 20px">
                                        To:
                                    </td>
                                    <td>
                                        <input type="text" name="To" id="To" class="txtCalander" value="" readonly="readonly" />
                                        <input id="hidtxtRetDate" name="hidtxtRetDate" value="" type="hidden" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >
                             <div id="downloadStatus" runat="server" style="float:left;" ></div>
                             <div id="div_Submit" style="float:right;"> 
                                <asp:Button ID="Downloads" runat="server" Text="Downloads" CssClass="buttonfltbk"  OnClientClick="return ShowWaitImage();"/>
                             </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_Progress" style="display: none"> 
                               Download In Progress.   Please do not 'refresh' or 'back' button <img alt="Booking In Progress" src="<%= ResolveUrl("../../Images/loading_bar.gif")%>"/>
                            </div>  
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var myDate = new Date();
        var currDate = (myDate.getFullYear()) + '-' + (myDate.getMonth() + 1) + '-' + myDate.getDate();
        document.getElementById("From").value = currDate;
        document.getElementById("To").value = currDate;
        document.getElementById("hidtxtDepDate").value = currDate;
        document.getElementById("hidtxtRetDate").value = currDate;
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
   
    <script src="../../Hotel/JS/HotelMarkup.js" type="text/javascript"></script>
</asp:Content>
