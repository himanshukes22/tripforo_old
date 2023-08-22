<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="CallingMidService.aspx.cs" Inherits="CallingMidService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/JScript.js" type="text/javascript"></script>

    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />


           
    <div>
            <table align="center" class="w100">
                      
                   <tr>

                        <td align="left" style="font-weight: bold; padding-top: 7px;" colspan="4">Insert Caching Records
                        </td>
                    </tr>
                    <tr>
                        <td class="clear1" colspan="4"></td>
                    </tr>                
                    <tr>
                       <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="50%">
                                <tr>
                                    <td >From
                                    </td>
                                    <td>
                                        <input id="txtDepCity1" name="txtDepCity1" type="text" />
                                        <input type="hidden" id="hidtxtDepCity1" name="hidtxtDepCity1" value="" />
                                    </td>
                                    <td>To
                                    </td>
                                    <td>
                                        <input id="txtArrCity1" name="txtArrCity1" type="text" />
                                        <input type="hidden" id="hidtxtArrCity1" name="hidtxtArrCity1" value="" />
                                    </td>
                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear1"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="50%">
                                <tr>
                                    <td>From Date
                                    </td>
                                    <td>
                                        <input type="text" name="txtDepDate" id="txtDepDate" class="txtCalander" value=""
                                            readonly="readonly" />
                                        <input type="hidden" name="hidtxtDepDate" id="hidtxtDepDate" value="" />
                                    </td>
                                    <td>
                                        <div>
                                            To Date
                                        </div>
                                    </td>
                                    <td>
                                        <input type="text" name="txtRetDate" id="txtRetDate" class="txtCalander" value=""
                                            readonly="readonly" />
                                        <input type="hidden" name="hidtxtRetDate" id="hidtxtRetDate" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="clear1"></td>
                                </tr>
                                <tr>
                                    <td>Select Airline
                                    </td>
                                    <td class="Text  large-4 columns">
                                        <asp:DropDownList ID="ddl_SelectDDL" runat="server">          
                                            <asp:ListItem Value="6E">Indigo</asp:ListItem>
                                            <asp:ListItem Value="SG">Spicejet</asp:ListItem>
                                            <asp:ListItem Value="G8">Goair</asp:ListItem>
                                            <asp:ListItem Value="AK">AirAisa</asp:ListItem>                          
                                        </asp:DropDownList>
                                    </td>     
                                    
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1"  CssClass="buttonfltbks" />
                                    </td>
                                    
                                                          
                                </tr>
                            </table>
                        </td>
                 
                    </tr>


                   
                </table>

    </div>
   
     <script type="text/javascript">

        var myDate = new Date();
        var currDate = (myDate.getDate()) + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
        document.getElementById("txtDepDate").value = currDate;
        document.getElementById("hidtxtDepDate").value = currDate;
        document.getElementById("txtRetDate").value = currDate;
        document.getElementById("hidtxtRetDate").value = currDate;
        var UrlBase = '<%=ResolveUrl("~/") %>';
    
        document.getElementById("td_time").style.display = "none";
        document.getElementById("ctl00_ContentPlaceHolder1_ddl_ReturnAnytime").style.display = "none";
      
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Common.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Search3_off.js")%>"></script>


    </asp:Content>