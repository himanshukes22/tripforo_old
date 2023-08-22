<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="ProxyEmp.aspx.vb" Inherits="Reports_Proxy_ProxyEmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
        <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />

    <script src="../../JS/ProxyJS.js" type="text/javascript"></script>

      <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.draggable.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/alert.js")%>"></script>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td, th {
    padding: 3px !important;
}
    </style>
    <div align="center">
        <table cellspacing="12" cellpadding="0" border="0" class="tbltbl" width="1200px">
            <tr>
                <td id="td_Proxy" runat="server">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="35px" class="Proxy">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Offline Requests
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_Adult" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_Child" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_Infrant" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_OneWay" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tbl_Pax" runat="server">
                                    <tr>
                                        <td style="border: thin solid #004b91" id="td_Adult" runat="server">
                                            <asp:Repeater ID="Repeater_Adult" runat="server">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="background-color: #CCCCCC; color: #000000; padding-left: 20px;" height="30px"
                                                                align="left">
                                                                <asp:Label CssClass="TextBig" ID="pttextADT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                    Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35px">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 90px;">
                                                                            <asp:DropDownList ID="ddl_ATitle" CssClass="form-control" runat="server">
                                                                                <%--  <asp:ListItem Value="" Selected="True">Title</asp:ListItem>--%>
                                                                                <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                                                <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                                                <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                                <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                                <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="160px">
                                                                            <asp:TextBox ID="txtAFirstName" runat="server" Width="160px" value="First Name" CssClass="form-control" onfocus="focusObj(this);"
                                                                                onblur="blurObj(this);" defvalue="First Name" autocomplete="off" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <%--<td width="140px">
                                                                        <asp:TextBox ID="txtAMiddleName" runat="server" Width="120px" value="Middle Name"
                                                                            onfocus="focusObjM(this);" onblur="blurObjM(this);" defvalue="Middle Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                    </td>--%>
                                                                        <td width="120px">
                                                                            <asp:TextBox ID="txtALastName" runat="server" Width="110px" value="Last Name" onfocus="focusObj1(this);"
                                                                                onblur="blurObj1(this);" defvalue="Last Name" CssClass="form-control" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:110px;">
                                                                            <asp:DropDownList ID="ddl_Age" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Value="">Year</asp:ListItem>
                                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                                <asp:ListItem Value="31">31</asp:ListItem>
                                                                                <asp:ListItem Value="32">32</asp:ListItem>
                                                                                <asp:ListItem Value="33">33</asp:ListItem>
                                                                                <asp:ListItem Value="34">34</asp:ListItem>
                                                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                                                <asp:ListItem Value="36">36</asp:ListItem>
                                                                                <asp:ListItem Value="37">37</asp:ListItem>
                                                                                <asp:ListItem Value="38">38</asp:ListItem>
                                                                                <asp:ListItem Value="39">39</asp:ListItem>
                                                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                                                <asp:ListItem Value="41">41</asp:ListItem>
                                                                                <asp:ListItem Value="42">42</asp:ListItem>
                                                                                <asp:ListItem Value="43">43</asp:ListItem>
                                                                                <asp:ListItem Value="44">44</asp:ListItem>
                                                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                                                <asp:ListItem Value="46">46</asp:ListItem>
                                                                                <asp:ListItem Value="47">47</asp:ListItem>
                                                                                <asp:ListItem Value="48">48</asp:ListItem>
                                                                                <asp:ListItem Value="49">49</asp:ListItem>
                                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                                <asp:ListItem Value="51">51</asp:ListItem>
                                                                                <asp:ListItem Value="52">52</asp:ListItem>
                                                                                <asp:ListItem Value="53">53</asp:ListItem>
                                                                                <asp:ListItem Value="54">54</asp:ListItem>
                                                                                <asp:ListItem Value="55">55</asp:ListItem>
                                                                                <asp:ListItem Value="56">56</asp:ListItem>
                                                                                <asp:ListItem Value="57">57</asp:ListItem>
                                                                                <asp:ListItem Value="58">58</asp:ListItem>
                                                                                <asp:ListItem Value="59">59</asp:ListItem>
                                                                                <asp:ListItem Value="60">60</asp:ListItem>
                                                                                <asp:ListItem Value="61">61</asp:ListItem>
                                                                                <asp:ListItem Value="62">62</asp:ListItem>
                                                                                <asp:ListItem Value="63">63</asp:ListItem>
                                                                                <asp:ListItem Value="64">64</asp:ListItem>
                                                                                <asp:ListItem Value="65">65</asp:ListItem>
                                                                                <asp:ListItem Value="66">66</asp:ListItem>
                                                                                <asp:ListItem Value="67">67</asp:ListItem>
                                                                                <asp:ListItem Value="68">68</asp:ListItem>
                                                                                <asp:ListItem Value="69">69</asp:ListItem>
                                                                                <asp:ListItem Value="70">70</asp:ListItem>
                                                                                <asp:ListItem Value="71">71</asp:ListItem>
                                                                                <asp:ListItem Value="72">72</asp:ListItem>
                                                                                <asp:ListItem Value="73">73</asp:ListItem>
                                                                                <asp:ListItem Value="74">74</asp:ListItem>
                                                                                <asp:ListItem Value="75">75</asp:ListItem>
                                                                                <asp:ListItem Value="76">76</asp:ListItem>
                                                                                <asp:ListItem Value="77">77</asp:ListItem>
                                                                                <asp:ListItem Value="78">78</asp:ListItem>
                                                                                <asp:ListItem Value="79">79</asp:ListItem>
                                                                                <asp:ListItem Value="80">80</asp:ListItem>
                                                                                <asp:ListItem Value="81">81</asp:ListItem>
                                                                                <asp:ListItem Value="82">82</asp:ListItem>
                                                                                <asp:ListItem Value="83">83</asp:ListItem>
                                                                                <asp:ListItem Value="84">84</asp:ListItem>
                                                                                <asp:ListItem Value="85">85</asp:ListItem>
                                                                                <asp:ListItem Value="86">86</asp:ListItem>
                                                                                <asp:ListItem Value="87">87</asp:ListItem>
                                                                                <asp:ListItem Value="88">88</asp:ListItem>
                                                                                <asp:ListItem Value="89">89</asp:ListItem>
                                                                                <asp:ListItem Value="90">90</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                            <td width="100px">
                                                                            <asp:TextBox ID="ID_AFreqFlyerNO" runat="server" Width="100px" value="FreqFlyerNO" onfocus="focusObffno(this);"
                                                                                onblur="blurObpeffno(this);" defvalue="FreqFlyerNO"></asp:TextBox>
                                                                        </td>
                                                                     <td width="120px">
                                                                            <asp:TextBox ID="ID_ApassNo" runat="server" Width="120px" value="PassportNo" onfocus="focusObpno(this);"
                                                                                onblur="blurObno(this);" defvalue="PassportNo"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_APPEXP" runat="server" Width="90px" value="PPExp" onfocus="focusObpex(this);"
                                                                                onblur="blurObpex(this);" defvalue="PPExp" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_AVisaDet" runat="server" Width="90px" value="VisaDet" onfocus="focusObv(this);"
                                                                                onblur="blurObpev(this);" defvalue="VisaDet" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>

                                                                    </tr>
                                                                 
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border: thin solid #004b91" id="td_Child" visible="true" runat="server">
                                            <asp:Repeater ID="Repeater_Child" runat="server">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="background-color: #CCCCCC; color: #000000; padding-left: 20px;" height="30px"
                                                                align="left">
                                                                <asp:Label CssClass="TextBig" ID="pttextCHD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                    Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35px">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                     
                                                                        <td style="width: 90px;">
                                                                            <asp:DropDownList ID="ddl_CTitle" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                                <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="160px">
                                                                            <asp:TextBox ID="txtCFirstName" runat="server" Width="160px" CssClass="form-control" value="First Name" onfocus="focusObjCFName(this);"
                                                                                onblur="blurObjCFName(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <td width="120px">
                                                                            <asp:TextBox ID="txtCLastName" runat="server" Width="110px" value="Last Name" CssClass="form-control" onfocus="focusObjCLName(this);"
                                                                                onblur="blurObjCLName(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                       <td style="width:110px;">
                                                                            <asp:DropDownList ID="ddl_AgeChild" CssClass="form-control" runat="server">
                                                                                <asp:ListItem>Year</asp:ListItem>
                                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                         </td>
                                                                            <td width="100px">
                                                                            <asp:TextBox ID="ID_CFreqFlyerNO" runat="server" Width="100px" value="FreqFlyerNO" CssClass="form-control" onfocus="focusObffno(this);"
                                                                                onblur="blurObpeffno(this);" defvalue="FreqFlyerNO"></asp:TextBox>
                                                                        </td>
                                                                     <td width="120px">
                                                                            <asp:TextBox ID="ID_CpassNo" runat="server" Width="120px" value="PassportNo" CssClass="form-control" onfocus="focusObpno(this);"
                                                                                onblur="blurObno(this);" defvalue="PassportNo"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_CPPEXP" runat="server" Width="80px"  value="PPExp" onfocus="focusObpex(this);"
                                                                                onblur="blurObpex(this);" defvalue="PPExp" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_CVisaDet" runat="server" Width="80px" value="VisaDet" onfocus="focusObv(this);"
                                                                                onblur="blurObpev(this);" defvalue="VisaDet" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>

                                                                    </tr>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border: thin solid #004b91" id="td_Infant" visible="true" runat="server">
                                            <asp:Repeater ID="Repeater_Infant" runat="server">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="background-color: #CCCCCC; color: #000000; padding-left: 20px;" height="30px"
                                                                align="left">
                                                                <asp:Label CssClass="TextBig" ID="pttextINF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaxTP")%>'
                                                                    Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35px">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                       
                                                                        <td style="width: 90px;">
                                                                            <asp:DropDownList ID="ddl_ITitle" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Value="Mstr">Mstr.</asp:ListItem>
                                                                                <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="160px">
                                                                            <asp:TextBox ID="txtIFirstName" runat="server" Width="160px" value="First Name" CssClass="form-control" onfocus="focusObjIFName(this);"
                                                                                onblur="blurObjIFName(this);" defvalue="First Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <td width="120px">
                                                                            <asp:TextBox ID="txtILastName" runat="server" Width="110px" value="Last Name" CssClass="form-control" onfocus="focusObjILName(this);"
                                                                                onblur="blurObjILName(this);" defvalue="Last Name" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:110px;">
                                                                            <asp:DropDownList ID="ddl_AgeInfant" CssClass="form-control" runat="server">
                                                                                <asp:ListItem>Year</asp:ListItem>
                                                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                            </asp:DropDownList>
                         
                                                                        </td>

                                                                             </td>
                                                                            <td width="100px">
                                                                            <asp:TextBox ID="ID_IFreqFlyerNO" runat="server" Width="100px" value="FreqFlyerNO" CssClass="form-control" onfocus="focusObffno(this);"
                                                                                onblur="blurObpeffno(this);" defvalue="FreqFlyerNO"></asp:TextBox>
                                                                        </td>
                                                                     <td width="120px">
                                                                            <asp:TextBox ID="ID_IpassNo" runat="server" Width="120px" value="PassportNo" CssClass="form-control" onfocus="focusObpno(this);"
                                                                                onblur="blurObno(this);" defvalue="PassportNo"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_IPPEXP" runat="server" Width="80px" value="PPExp" onfocus="focusObpex(this);"
                                                                                onblur="blurObpex(this);" defvalue="PPExp" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                     <td width="90px">
                                                                            <asp:TextBox CssClass="datepicker" ID="ID_IVisaDet" runat="server" Width="80px" value="VisaDet" onfocus="focusObv(this);"
                                                                                onblur="blurObpev(this);" defvalue="VisaDet" onkeypress="return isCharKey(event)"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" height="60px">
                                            <div id="div_Submit">
                                                <asp:Button ID="Button2" runat="server" Text="Book My Flight" Font-Bold="True" Height="35px" CssClass="buttonfltbks"
                                                    Width="200px" OnClientClick="return paxValidate();" />
                                            </div>
                                            <div id="div_Progress" style="display: none">
                                                <b>Request In Progress.</b> Please do not 'refresh' or 'back' button
                                                <img alt="Booking In Progress" src="<%= ResolveUrl("~/images/loading_bar.gif")%>" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>


    <script type="text/javascript">
        $(function () {
            debugger;
            $(".datepicker").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                })
        });
    </script>
</asp:Content>
