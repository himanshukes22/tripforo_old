<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false" CodeFile="AdminMarkupset.aspx.vb" Inherits="Transfer_AdminMarkupset" Title="Untitled Page" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
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
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="../../js/chrome.js"></script>

    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>

    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">

        <div class="large-3 medium-3 small-12 columns">

            <uc1:Settings runat="server" ID="Settings" />

        </div>
        <div class="large-8 medium-8 small-12 columns end">
            <div class="large-12 medium-12 small-12 heading">
                <div class="large-12 medium-12 small-12 heading1">Transfer Markup</div>
                <div class="clear1"></div>
                <div class="large-12 medium-12 small-12 redlnk">

                    <div class="large-2 medium-2 small-4 columns">
                        Agent Type:
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <asp:TextBox runat="server" ID="uid" Text=""></asp:TextBox>
                    </div>

                    <div class="clear1"></div>

                    <div class="large-2 medium-2 small-4 columns">
                        MarkUp Per Pax:
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <asp:TextBox runat="server" ID="mk"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                            Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                    </div>
                    <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">
                        MarkUpType:
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <asp:DropDownList ID="ddl_mktyp" runat="server" class="ddlBoxpax">
                            <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                            <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox runat="server" ID="mktyp" Text="Fixed" ReadOnly="true" Style="width: 40px"></asp:TextBox>--%>
                    </div>
                </div>
                <div class="clear1"></div>
                <div class="large-12 medium-12 small-12">
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>


                            <div class="large-2 medium-3 small-12 large-push-9 medium-push-9 columns">
                                <asp:Button ID="btnAdd" runat="server" CssClass="buttonfltbk" OnClick="btnAdd_Click" Text="New Entry" />
                            </div>
                            <div class="large-12 medium-12 small-12">
                                <asp:Label ID="lbl" runat="server" Style="color: #CC0000;"></asp:Label>
                            </div>
                            <div class="clear1"></div>

                            <div class="large-12 medium-12 small-12">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                    CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:BoundField DataField="COUNTER" HeaderText="Sr.No" ReadOnly="True" />
                                        <asp:BoundField DataField="MARKUP_ON" HeaderText="Agent Type" ControlStyle-CssClass="textboxflight1"
                                            ReadOnly="true" />

                                        <asp:BoundField DataField="MARKUP_VALUE" HeaderText="Mark Up" ControlStyle-CssClass="textboxflight1" />
                                        <asp:BoundField DataField="MARKUP_TYPE" HeaderText="MarkUpType" ControlStyle-CssClass="textboxflight1"
                                            ReadOnly="true" />
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                   
                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
                        <ProgressTemplate>
                            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                            </div>
                            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                Please Wait....<br />
                                <br />
                                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                <br />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

