<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="LogTrack.aspx.cs" Inherits="SprReports_LogTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link  href='<%=ResolveClientUrl("~/Styles/XMLDisplay.css")%>'  type="text/css" rel="stylesheet"/>
          <script  type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/XMLDisplay.js")%>'  ></script>

    <script type="text/javascript">
        function GetXMLDetails(DivName, XML_String) {
            $("#GetXML_Details").show();
            if (XML_String != "") {
                LoadXMLString('GL', XML_String)
            }
            else {
                $("#GetXML_Details").hide();
                alert("No data found");
            }
        }
    </script>
    <style>
        div.close {
            background: url("/Images/closebox.png") no-repeat scroll 0 0 transparent;
            bottom: 24px;
            cursor: pointer;
            float: right;
            height: 30px;
            left: 27px;
            position: relative;
            width: 30px;
        }
    </style>
 <script type="text/javascript">

     function getjson(customerId) {
          
         var jsonObj = $.parseJSON(customerId);
         //var jsonHtmlTable = ConvertJsonToTable(customerId);
         var html = '<table border="1">';
         $.each(jsonObj, function (key, value) {
             html += '<tr>';
             html += '<td>' + key + '</td>';
             if (Array.isArray(value) == true) {

                 html += '<td><table border="1">';
                 for (var i = 0; i < value.length; i++) {
                      
                     $.each(value[i], function (key, value) {
                         html += '<tr>';
                         html += '<td>' + key + '</td>';
                         if (Array.isArray(value) == true) {
                              
                             html += '<td><table border="1">';
                             for (var i = 0; i < value.length; i++) {
                                 $.each(value[i], function (key, value) {
                                     html += '<tr>';
                                     html += '<td>' + key + '</td>';
                                     html += '<td>' + value + '</td>';
                                     html += '</tr>'
                                 })
                             }
                             html += '</td></table>';
                         }
                         html += '<td>' + value + '</td>';
                         html += '</tr>'
                     })
                 }
                 html += '</td></table>';
             }
             else {
                 if (typeof value == "string") {
                     html += '<td>' + value + '</td>';
                 }
                 else {
                     html += '<td><table border="1">';
                     $.each(value, function (key, value) {
                         html += '<tr>';
                         html += '<td>' + key + '</td>';
                         if (typeof value == "string") {
                             html += '<td>' + value + '</td>';
                         }
                         else {
                             html += '<td><table border="1">';
                             $.each(value, function (key, value) {
                                 html += '<tr>';
                                 html += '<td>' + key + '</td>';
                                 html += '<td>' + value + '</td>';
                                 html += '</tr>';
                             })
                             html += '</td></table>';
                         }
                         html += '</tr>'
                     })
                     html += '</td></table>';
                 }
             }
             html += '</tr>';
         });
         html += '</table>';
         $("#JsonDiv").show();
         $('#JsonDiv').html('<div class="close"></div>' + html);
     };
    </script>

     <div>
            <table border="1" style="width: 600px">
                <tr>
                    <td class="auto-style4" style="text-align: right">&nbsp;&nbsp;&nbsp; Order ID :
                        <asp:TextBox ID="txt_orderid" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4" style="text-align: left">&nbsp; PNR No :
                        <asp:TextBox ID="txt_pnrno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" style="text-align: center" colspan="2">
                        <asp:Button ID="btn_logtrack" runat="server" OnClick="btn_logtrack_Click" Text="Submit" Width="100px" />
                        <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

     <asp:Panel ID="Panel1" runat="server">
            <div>
                <asp:DataList ID="DataList1" runat="server" BackColor="Gray" BorderColor="#666666" BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
                    Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Horizontal"
                    Width="600px">
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderTemplate>
                        Log Tracker
                    </HeaderTemplate>
                    <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />
                    <ItemTemplate>
                        <b>OrderId:</b>
                        <asp:Label ID="OrderId" runat="server" Text='<%# Eval("OrderId") %>'></asp:Label>
                        <br />
                        <b>Pnr No:</b>
                        <asp:Label ID="Pnr" runat="server" Text='<%# Eval("Pnr") %>'></asp:Label>
                        <br />
                        <b>CreateDate:</b>
                        <asp:Label ID="CreateDate" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Label>
                        <br />
                        <b>BookingDate:</b>
                        <asp:Label ID="BookingDate" runat="server" Text='<%# Eval("BookingDate") %>'></asp:Label>
                        <br />
                        <b>Provider:</b>
                        <asp:Label ID="Provider" runat="server" Text='<%# Eval("Provider") %>'></asp:Label>
                        <br />
                        <b>SSReqequest:</b>
                        <asp:Label ID="SSReqequest" runat="server" Text="SSReqequest" OnClick='<%#SSRes(Convert.ToString(Eval("SSReq")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>SSResponse:</b>
                        <asp:Label ID="SSResponse" runat="server" Text="SSResponse" OnClick='<%#SSReq(Convert.ToString(Eval("SSRes")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNRRTRequest:</b>
                        <asp:Label ID="PNRRTRequest" runat="server" Text="PNRRTRequest" OnClick='<%#PNRRTReq(Convert.ToString(Eval("PNRRTReq")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNRRTResponse:</b>
                        <asp:Label ID="PNRRTResponse" runat="server" Text="PNRRTResponse" OnClick='<%#GetXML("GetXMLDetails", Convert.ToString(Eval("PNRRTRes")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                         <b>DOCPRDRequest:</b>
                        <asp:Label ID="DOCPRDRequest" runat="server" Text="DOCPRDRequest" OnClick='<%#PNRRTReq(Convert.ToString(Eval("DOCPRDReq")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>DOCPRDResponse:</b>
                        <asp:Label ID="DOCPRDResponse" runat="server" Text="DOCPRDResponse" OnClick='<%#DOCPRDResponse(Convert.ToString(Eval("DOCPRDRes")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNRRT2Request:</b>
                        <asp:Label ID="PNRRT2Request" runat="server" Text="PNRRT2Request" OnClick='<%#PNRRTReq(Convert.ToString(Eval("PNRRT2Req")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNRRT2Response:</b>
                        <asp:Label ID="PNRRT2Response" runat="server" Text="PNRRT2Response" OnClick='<%#PNRRTReq(Convert.ToString(Eval("PNRRT2Res")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>SERequest:</b>
                        <asp:Label ID="SERequest" runat="server" Text="SERequest" OnClick='<%#GetXML("SEReq", Convert.ToString(Eval("SEReq")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                       <%-- <b>SEResponse:</b>
                        <asp:Label ID="SEResponse" runat="server" Text="SEResponse" OnClick='<%#SERes(Convert.ToString(Eval("SERes")))%>' Style="cursor: pointer;"></asp:Label>
                       <br />--%>
                        <b>PNBFRequest1:</b>
                        <asp:Label ID="PNBFRequest1" runat="server" Text="PNBFRequest1" OnClick='<%#PNBFRequest1("GetXMLDetails", Convert.ToString(Eval("PNBFReq1")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNBFResponse1:</b>
                        <asp:Label ID="PNBFResponse1" runat="server" Text="PNBFResponse1" OnClick='<%#GetXML("GetXMLDetails", Convert.ToString(Eval("PNBFRes1")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNBFRequest2:</b>
                        <asp:Label ID="PNBFRequest2" runat="server" Text="PNBFRequest2" OnClick='<%#PNBFRequest1("GetXMLDetails", Convert.ToString(Eval("PNBFReq2")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNBFResponse2:</b>
                        <asp:Label ID="PNBFResponse2" runat="server" Text="PNBFResponse2" OnClick='<%#GetXML("GetXMLDetails", Convert.ToString(Eval("PNBFRes2")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNBFRequset3:</b>
                        <asp:Label ID="PNBFRequset3" runat="server" Text="PNBFRequset3" OnClick='<%#PNBFRequest1("GetXMLDetails", Convert.ToString(Eval("PNBFReq3")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>PNBFResponse3:</b>
                        <asp:Label ID="PNBFResponse3" runat="server" Text="PNBFResponse3" OnClick='<%#GetXML("GetXMLDetails", Convert.ToString(Eval("PNBFRes3")))%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="GetXML_Details" runat="server" style="text-align: left; width: 95%; height:85%; overflow-x:scroll; position: absolute; display: none; margin-top: 10px; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; border: 5px solid #d1d1d1; border-radius: 10px; z-index: 1;top: 1%">
                <div class="close"></div>
                <div id="GL"></div>
            </div>
        </asp:Panel>
       <%--<asp:Panel ID="Panel2" runat="server">
            <div>
                <asp:DataList ID="DataList2" runat="server" BackColor="Gray" BorderColor="#666666"
                    BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
                    Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Horizontal"
                    Width="600px">
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderTemplate>
                        Log Tracker
                    </HeaderTemplate>
                    <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />
                    <ItemTemplate>
                        <b>OrderId:</b>
                        <asp:Label ID="OrderId" runat="server" Text='<%# Eval("OrderId") %>'></asp:Label>
                        <br />
                        <b>Pnr No:</b>
                        <asp:Label ID="PnrNo" runat="server" Text='<%# Eval("PnrNo") %>'></asp:Label>
                        <br />
                        <%--<b>Provider:</b>
                        <asp:Label ID="Provider" runat="server" Text='<%# Eval("Provider") %>'></asp:Label>
                        <br />--%>
                       <%-- <b>BookingDate:</b>
                        <asp:Label ID="BookingDate" runat="server" Text='<%# Eval("BookingDate") %>'></asp:Label>
                        <br />
                        <b>Airline:</b>
                        <asp:Label ID="Airline" runat="server" Text='<%# Eval("Airline") %>'></asp:Label>
                        <br />
                        <b>BookReq:</b>
                        <asp:Label ID="BookReq" runat="server" Text="BookReq" OnClick='<%# ChangeVal( Eval("BookReq").ToString())%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>BookRes:</b>
                        <asp:Label ID="BookRes" runat="server" Text="BookRes" OnClick='<%# ChangeVal( Eval("BookRes").ToString())%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>RepriceReq:</b>
                        <asp:Label ID="RepriceReq" runat="server" Text="RepriceReq" OnClick='<%# ChangeVal( Eval("RepriceReq").ToString())%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>RePriceRes:</b>
                        <asp:Label ID="RePriceRes" runat="server" Text="RePriceRes" OnClick='<%# ChangeVal( Eval("RePriceRes").ToString())%>' Style="cursor: pointer;"></asp:Label>
                        <br />
                        <b>Others:</b>
                        <asp:Label ID="Others" runat="server" Text='<%# Eval("Others") %>'></asp:Label>
                        <br />
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="JsonDiv" runat="server" style="text-align: center; position: absolute; display: none; margin-top: 1px; background-color: #f9f9f9; width: 95%; height:85%; font-size: 12px; font-weight: bold; padding: 20px;  overflow-x:scroll; box-shadow: 0px 1px 5px #000; border: 5px solid #d1d1d1; border-radius: 10px; z-index: 1; top: 1%">
            </div>
        </asp:Panel>--%>
        <asp:Panel ID="Panel2" runat="server">
        <div>
        <asp:DataList ID="DataList2" runat="server" BackColor="Gray" BorderColor="#666666"
            BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Horizontal"
            Width="600px">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
               Log Tracker</HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />
            <ItemTemplate>
                <b>OrderId:</b>
                <asp:Label ID="OrderId" runat="server" Text='<%# Eval("OrderId") %>'></asp:Label>
                <br />
                <b>Pnr No:</b>
                <asp:Label ID="PnrNo" runat="server" Text='<%# Eval("PnrNo") %>'></asp:Label>
                <br />
                 <b>Provider:</b>
                <asp:Label ID="Provider" runat="server" Text='<%# Eval("Provider") %>'></asp:Label>
                <br />
                 <b>BookingDate:</b>  
                <asp:Label ID="BookingDate" runat="server" Text='<%# Eval("BookingDate") %>'></asp:Label>
                <br />
                 <b>Airline:</b>  
                <asp:Label ID="Airline" runat="server" Text='<%# Eval("Airline") %>'></asp:Label>
                <br />
                <b>BookReq:</b>
                </b><span class="BookReq" style="width:100%;"><%# Eval("BookReq")%></span><br />
                <b>BookRes:</b>
                </b><span class="BookReq"><%# Eval("BookRes")%></span><br />
                <b>RepriceReq:</b>
                </b><span class="RepriceReq"><%# Eval("RepriceReq")%></span><br />
                <b>RePriceRes:</b>
                </b><span class="PNRRTRes"><%# Eval("RePriceRes")%></span><br />
                <b>Others:</b>  
                <asp:Label ID="Others" runat="server" Text='<%# Eval("Others") %>'></asp:Label>
                <br />
                 <br />
            </ItemTemplate>
        </asp:DataList>
            <div id="GetXML" >
            </div>
        </div>
        </asp:Panel>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".close").live("click", function () {
                $("#JsonDiv").hide();
            });
        });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".close").live("click", function () {
            $("#GetXML_Details").hide();
        });
    });
</script>
</asp:Content>

