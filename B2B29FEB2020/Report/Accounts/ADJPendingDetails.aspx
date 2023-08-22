<%@ Page Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="ADJPendingDetails.aspx.vb" Inherits="SprReports_Accounts_ADJPendingDetails"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
        input[type="text"], input[type="password"], select, radio, legend, fieldset, textarea
        {
            border: 1px solid #004b91;
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
    </style>--%>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

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
            background: url( 'PopupJS/BG.png' );
        }
        div#sk-lightbox-ui
        {
            background-color: #ffffff;
            -moz-box-shadow: 0px 5px 200px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0px 5px 20px rgba(0,0,0,0.5);
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 600px;
            height: 330px;
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
        .style1
        {
            height: 334px;
        }
        .style2
        {
            height: 10px;
        }
    </style>
    <link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <div class="divcls">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 40%">
                    <fieldset style="border: 1px solid #004b91; text-align: center; width: 100%; padding-top: 10px;
                        padding-bottom: 15px; padding-left: 15px;">
                        <legend style="padding: 2px 5px 5px 2px; font-family: arial, Helvetica, sans-serif;
                            font-size: 13px; font-weight: bold; color: #004b91; width: 470px; text-align: center;
                            background-color: #EAEAEA; vertical-align: middle;">Search Record By Type And Category</legend>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 15%">
                                </td>
                                <td style="width: 100%">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="padding: 10px 5px 10px 5px; font-weight: bold; font-family: arial, Helvetica, sans-serif;
                                                font-size: 13px;" width="125px" align="left">
                                                Upload Type :
                                            </td>
                                            <td align="left">
                                                <fieldset style="border: thin solid #004b91; width: 140px;">
                                                    <asp:RadioButtonList ID="RBL_Type" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                        CellPadding="2" CellSpacing="2" Font-Size="12px" Font-Names="Arial" Width="140px">
                                                    </asp:RadioButtonList>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr id="tr_Cat" runat="server">
                                            <td style="padding: 10px 5px 10px 5px; font-weight: bold; font-family: arial, Helvetica, sans-serif;
                                                font-size: 13px;" align="left">
                                                Upload Category :
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddl_Category" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td style="width: 30%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="divcls">
        <table cellspacing="10" cellpadding="0" border="0" align="center" class="tbltbl"
            width="700px">
            <tr>
                <td class="bodytext" style="padding: 10px; background: #fff;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                <td align="center" class="Heading" colspan="3" 
                                        style="font-size: 20px; padding-bottom: 10px; background-color: #EAEAEA; padding-top: 10px;">
                                Adjustment Pending Details
                                </td>
                                </tr>
                                 <tr>
                                <td style="height: 10px">
                                
                                </td>
                                </tr>
                                    <tr>
                                        <td width="190px" class="h2" style="padding-bottom: 3px; color: #004b91;" valign="bottom">
                                            Search By Agency Name&nbsp;&nbsp;
                                        </td>
                                        <td width="140px" style="padding-bottom: 3px" valign="bottom">
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Width="130px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td id="td_Reject" runat="server" visible="false" valign="top" align="right">
                                                        <fieldset style="padding: 5px 5px 5px 5px; border: 2px solid #004b91; width: 70%;">
                                                            <legend style="border: thin solid #004b91; width: 110px; font-family: arial, Helvetica, sans-serif;
                                                                font-size: 12px; font-weight: bold; color: #004b91;">&nbsp;&nbsp;Submit Comment&nbsp;&nbsp;</legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="center" style="padding-top: 10px">
                                                                        <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine" Height="60px" Width="350px"
                                                                            BackColor="#FFFFCC"></asp:TextBox><br />
                                                                        <br />
                                                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="buttonfltbks" />&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="buttonfltbk" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="contdtls">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grd_accdeposit" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_accdeposit_RowCommand"
                                    OnRowDataBound="grd_accdeposit_RowDataBound"  CssClass="table table-hover" GridLines="None" Font-Size="12px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <%--   <asp:BoundField HeaderText="&nbsp;&nbsp;ID&nbsp;&nbsp;" DataField="Counter" />--%>
                                        <asp:TemplateField HeaderText="Agency Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_agencyid" runat="server" Text='<%# Highlight(Eval("AgencyName").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="AgencyID" DataField="AgencyID" />--%>
                                        <asp:TemplateField HeaderText="Agency ID">
                                            <ItemTemplate>
                                                <div id="sk-lightbox-wrapper">
                                                    <div id="sk-lightbox-ui">
                                                        <div id="sk-lightbox-close">
                                                            <a href="javascript:void(0);">
                                                                <img alt="" src="PopupJS/close.png" style="border: 0px none;" border="0"></a>
                                                        </div>
                                                        <iframe id="sk-lightbox-iframe" frameborder="0" height="100%" width="100%"></iframe>
                                                    </div>
                                                </div>
                                                <div>
                                                    <a target="_self" href="UploadCredit.aspx?AgentID=<%#Eval("AgencyID")%> &ID=<%#Eval("Counter")%>#lightbox">
                                                    </a>
                                                </div>
                                                <asp:Label ID="lbl_uid" runat="server" Text='<%#Eval("AgencyID")%>' Font-Bold="True"
                                                    ForeColor="#004b91"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                        <asp:BoundField HeaderText="ModeOfPayment" DataField="ModeOfPayment" />
                                        <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                                        <asp:BoundField HeaderText="ChequeNo" DataField="ChequeNo" />
                                        <asp:BoundField HeaderText="ChequeDate" DataField="ChequeDate" />
                                        <asp:BoundField HeaderText="TransactionID" DataField="TransactionID" />
                                        <asp:BoundField HeaderText="Bank AreaCode" DataField="BankAreaCode" />
                                        <asp:BoundField HeaderText="Deposit City" DataField="DepositCity" />
                                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                        <asp:TemplateField HeaderText="Reject">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkaccept" runat="server" Text="Accept" CommandName="accept"
                                                    CommandArgument='<%#Eval("Counter") %>' ForeColor="#004b91" Font-Bold="True">Accept</asp:LinkButton>
                                         </br>
                                                ||</br>
                                                <asp:LinkButton ID="lnkcancel" runat="server" CommandName="reject" CommandArgument='<%#Eval("Counter") %>'
                                                    ForeColor="Red" Font-Bold="True">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />
    <asp:BoundField HeaderText="Date" DataField="Date" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td id="Dop1" runat="server" visible="false">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
