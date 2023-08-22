<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="AcceptAccountRequest.aspx.vb" Inherits="SprReports_OLSeries_AcceptAccountRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/Series.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../PopupJS/fat_eua.js">
    </script>

    <script>
        (new SK.Utils.SiteReferrer()).store();
    </script>

    <script type="text/javascript">

        $(window).addEvent('load', function() {
            new SK__LightBox();
        });

        var SK__LightBox = new Class({

            win: {}, // obj containing all lightbox DOM elements

            initialize: function() {
                this.initWindow();
                this.bindLinks();
            },

            initWindow: function() {
                this.win = {
                    wrapper: $('sk-lightbox-wrapper'),
                    close: $('sk-lightbox-close'),
                    iframe: $('sk-lightbox-iframe')
                };

                this.win.wrapper.setStyle('opacity', '0');
                this.win.wrapper.style.display = 'block';
                this.win.wrapper.inject(document.body);

                this.win.close.addEvent('click', function(e) {
                    e.preventDefault();
                    this.hideWindow();
                } .bind(this));
            },

            bindLinks: function() {
                var links = $$('a');
                links.each(function(link) {
                    if (link.href.match(/#lightbox$/)) {
                        link.addEvent('click', function(e) {
                            e.preventDefault();
                            var url = this.link.href.replace('#lightbox', '');
                            this.mgr.showWindow(url);
                        } .bind({ mgr: this, link: link }));
                    }
                } .bind(this));
            },

            showWindow: function(url) {
                this.loadContent(url);
                this.win.wrapper.fade('in');
            },

            hideWindow: function() {
                this.win.wrapper.fade('out');
                this.win.iframe.src = 'about:blank';
            },

            loadContent: function(url) {
                if (this.win.iframe.src != url) {
                    this.win.iframe.src = url;
                }
            }

        })

    </script>

    <style>
        div#sk-lightbox-wrapper
        {
            width: 100%;
            height: 100%;
            position: fixed;
            top: 1px;
            left: 1px;
            z-index: 10000;
            background-color: gray;
            background: url('../../PopupJS/BG.png');
        }
        div#sk-lightbox-ui
        {
            background-color: #ffffff;
            -moz-box-shadow: 0px 5px 200px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0px 5px 20px rgba(0,0,0,0.5);
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 700px;
            height: 290px;
            padding: 20px;
            border: 4px solid #004b91;
            margin: 10% auto;
            position: relative;
        }
        div#sk-lightbox-close
        {
            width: 50px;
            height: 50px;
            position: absolute;
            top: -25px;
            right: -25px;
            cursor: pointer;
        }
        div#sk-lightbox-wrapper
        {
            display: none;
        }
    </style>
    <div id="divrequest" runat="server">
        <table cellpadding="0" cellspacing="10" width="1000px" class="tbltbl">
            <tr>
                <td align="center" style="height: 50px;" class="Heading">
                    Request Account Panel<br />
                    <asp:LinkButton ID="lnkviewProcess" runat="server" Text="(Click here to see Inprocess details)"
                        Font-Bold="True" Font-Underline="False" ForeColor="#000099" Font-Size="12pt"></asp:LinkButton>&nbsp;
                </td>
            </tr>
            <tr>
                <td id="rejectTD" runat="server" visible="false">
                    <table cellpadding="0" cellspacing="10" class="tbltbl" width="700px">
                        <tr>
                            <td align="center" style="font-family: arial, Helvetica, sans-serif; font-size: small;
                                color: #004b91; font-weight: bold;">
                                Reject Comment:
                            </td>
                            <td colspan="1">
                                <asp:TextBox ID="txt_rejectRmk" runat="server" TextMode="MultiLine" CssClass="multitxt"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Button ID="btn_submit" runat="server" Text="Reject" CssClass="button" />
                                &nbsp;<asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grd_requestAcc" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    Width="90%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="AgencTID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblageniid" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agency Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagency" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Request Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Executive ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblexecid" runat="server" Text='<%#Eval("Executive_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Executive Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblexecrmk" runat="server" Text='<%#Eval("Executive_Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accept/Reject">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_accept" runat="server" Text="Accept" CommandName="accept" CommandArgument='<%#Eval("Counter") %>' />&nbsp;<asp:Button
                                                    ID="btn_reject" runat="server" Text="Reject" CommandName="reject" CommandArgument='<%#Eval("Counter") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divProcess" runat="server" visible="false">
        <table cellpadding="0" cellspacing="10" width="1000px" class="tbltbl">
            <tr>
                <td align="center" style="height: 50px;" class="Heading">
                    Process Series Departure<br />
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="(Click here to see Request details)"
                        Font-Bold="True" Font-Underline="False" ForeColor="#000099" Font-Size="12pt"></asp:LinkButton>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="grd_processAcc" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                        Width="90%">
                        <Columns>
                            <asp:TemplateField HeaderText="Counter" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AgentID">
                                <ItemTemplate>
                                    <asp:Label ID="lblagentid" runat="server" Text='<%#Eval("AgentID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agency Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblagency" runat="server" Text='<%#Eval("Agency_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Executive Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblexecrmk" runat="server" Text='<%#Eval("Executive_Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Executive ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Executive_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%--      <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="modify" CommandArgument='<%#Eval("Counter") %>'/>
--%>
                                    <div id="sk-lightbox-wrapper">
                                        <div id="sk-lightbox-ui">
                                            <div id="sk-lightbox-close">
                                                <a href="javascript:void(0);">
                                                    <img alt="" src="../../PopupJS/close.png" style="border: 0px none;" border="0"></a>
                                            </div>
                                            <iframe id="sk-lightbox-iframe" frameborder="0" height="100%" width="100%"></iframe>
                                        </div>
                                    </div>
                                    <div>
                                        <a target="_self" href="../Accounts/UploadCredit.aspx?AgentID=<%#Eval("AgentID")%>&Counter=<%#Eval("Counter") %>&Amt=<%#Eval("Amount") %>#lightbox"
                                            style="font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;
                                            color: #004b91">Upload</a>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
