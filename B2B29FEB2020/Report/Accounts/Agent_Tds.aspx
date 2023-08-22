<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="Agent_Tds.aspx.vb" Inherits="Reports_Accounts_Agent_Tds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
   <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <style type="text/css">
        .txtBox
        {
            width: 140px;
            height: 18px;
            line-height: 18px;
            border: 2px #D6D6D6 solid;
            padding: 0 3px;
            font-size: 11px;
        }
        .txtCalander
        {
            width: 100px;
            background-image: url(../../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
    </style>
    <style>
        input[type="text"], input[type="password"], select
        {
            border: 1px solid #808080;
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

   
            <div class="divcls">
                <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl"
                    width="900px">
                    <tr>
                        <td style="padding: 10px; background: #fff;">
                            <table width="100%" border="0" cellpadding="2" cellspacing="2" align="center">
                                <tr>
                                    <td class="text1" height="40px" width="100">
                                        Select Agent :
                                    </td>
                                    <td align="left">
                                       <input type="text" id="txtAgencyName" name="txtAgencyName" style="width: 200px" onfocus="focusObj(this);"
                                                        onblur="blurObj(this);" defvalue="Agency Name or ID" autocomplete="off" value="Agency Name or ID" />
                                                    <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                                        
                                        <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="buttonfltbks" />
                                        
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" />
                                                        <asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />
                                                        <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name" ControlStyle-CssClass="textboxflight1"
                                                            ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="user_id" HeaderText="Agent ID" ControlStyle-CssClass="textboxflight1"
                                                            ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="Agent_Type" HeaderText="Agent Type" ControlStyle-CssClass="textboxflight1"
                                                            ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="TDS" HeaderText="Normal TDS %" ControlStyle-CssClass="textboxflight1">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ExmptTDS" HeaderText="Exempted TDS %" ControlStyle-CssClass="textboxflight1">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ExmptTdsLimit" HeaderText="Exempted TDS Limit" ControlStyle-CssClass="textboxflight1">
                                                        </asp:BoundField>
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-Font-Bold="true"
                                                            Visible="false">
                                                            <ItemStyle Font-Bold="True"></ItemStyle>
                                                        </asp:CommandField>
                                                    </Columns>
                                                    
                                                  
                                                </asp:GridView>
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
                                                            <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                                            <br />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
      
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

</asp:Content>
