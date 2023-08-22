<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="MISCSRVCHARGE.aspx.vb" Inherits="SprReports_Admin_MISCSRVCHARGE" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>"
        rel="stylesheet" />
    <%--<style type="text/css">
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
            -moz-border-radius: 3px 3px 3px 3px
            -o-border-radius: 3px 3px 3px 3px;
        }
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
            background-image: url(../images/cal.gif);
            background-repeat: no-repeat;
            background-position: right;
            cursor: pointer;
            border: 1px #D6D6D6 solid;
        }
        .button_edit
        {
            cursor: pointer;
            color: #fff;
            font-weight: bold;
            height: 28px;
            width: 50px;
            padding: 0 10px;
            border-radius: 3px;
            line-height: 25px;
            border: 0px;
            background: url(../../images/edit_new.png);
        }
        .button_delete
        {
            cursor: pointer;
            color: #fff;
            font-weight: bold;
            height: 28px;
            width: 56px;
            padding: 0 10px;
            border-radius: 3px;
            line-height: 25px;
            border: 0px;
            background: url(../../images/delete_new.png);
        }
        .button_update
        {
            cursor: pointer;
            color: #fff;
            font-weight: bold;
            height: 28px;
            width: 56px;
            padding: 0 10px;
            border-radius: 3px;
            line-height: 25px;
            border: 0px;
            background: url(../../images/update_new.png);
        }
        .button_cancel
        {
            cursor: pointer;
            color: #fff;
            font-weight: bold;
            height: 28px;
            width: 56px;
            padding: 0 10px;
            border-radius: 3px;
            line-height: 25px;
            border: 0px;
            background: url(../../images/cancel_new.png);
        }
    </style>
    <style type="text/css">
        input[type="text"], input[type="password"], select, radio, legend, fieldset
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
    </style>
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">

            <uc1:Settings runat="server" ID="Settings" />

        </div>
        <div class="large-8 medium-8 small-12 columns end">
            <div class="large-12 medium-12 small-12">

                <div class="large-12 medium-12 small-12 heading">
                    <div class="large-12 medium-12 small-12 heading1">
                        MISC SERVICE CHARGE
                    </div>
                    <div class="clear1"></div>
                    <div class="large-2 medium-2 small-4 columns">
                        Airline
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <%--<asp:TextBox ID="TXTAirline" runat="server" MaxLength="10"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddl_airline" runat="server">
                            <asp:ListItem Value="ALL">--ALL--</asp:ListItem>
                            <asp:ListItem Value="AI">Air India</asp:ListItem>
                            <asp:ListItem Value="9W">Jet Airways</asp:ListItem>
                            <asp:ListItem Value="UK">Vistara</asp:ListItem>
                            <asp:ListItem Value="6E">Indigo</asp:ListItem>
                            <asp:ListItem Value="G8">Goair</asp:ListItem>
                            <asp:ListItem Value="SG">Spicejet</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="large-2 medium-2 small-4 columns large-push-1 medium-push-1">
                        Trip
                    </div>
                    <div class="large-3 medium-3 small-8 columns large-push-1 medium-push-1">
                        <%-- <asp:TextBox ID="TXTTrip" onKeyPress="return isCharKey(event)" runat="server" 
                        MaxLength="1" ></asp:TextBox>--%>
                        <asp:DropDownList ID="ddl_TripType" runat="server">
                            <asp:ListItem Value="D">Domestic</asp:ListItem>
                            <asp:ListItem Value="I">International</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="clear1"></div>

                    <div class="large-2 medium-2 small-4 columns">
                        Agent Id
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <%--<asp:TextBox ID="TXTAgentId"  runat="server" MaxLength="50" ></asp:TextBox>--%>
                        <input type="text" id="txtAgencyName" name="txtAgencyName" onfocus="focusObjag(this);"
                            onblur="blurObjag(this);" defvalue="ALL" autocomplete="off" value="ALL" />
                        <input type="hidden" id="hidtxtAgencyName" name="hidtxtAgencyName" value="" />
                    </div>
                    <div class="large-2 medium-2 small-4 columns large-push-1 medium-push-1">
                        GroupType
                    </div>
                    <div class="large-3 medium-3 small-8 columns large-push-1 medium-push-1">
                        <%--<asp:TextBox ID="TXTGroupType"  runat="server" MaxLength="10" ></asp:TextBox>--%>
                        <asp:DropDownList ID="ddl_GroupType" runat="server">
                            <asp:ListItem Value="ALL">--ALL--</asp:ListItem>
                            <%-- <asp:ListItem Value="Type1">Type1</asp:ListItem>
                    <asp:ListItem Value="Type2">Type2</asp:ListItem>
                    <asp:ListItem Value="Type3">Type3</asp:ListItem>
                    <asp:ListItem Value="Type4">Type4</asp:ListItem>
                    <asp:ListItem Value="Type5">Type5</asp:ListItem>
                    <asp:ListItem Value="Type6">Type6</asp:ListItem>
                    <asp:ListItem Value="Type7">Type7</asp:ListItem>
                    <asp:ListItem Value="Type8">Type8</asp:ListItem>
                    <asp:ListItem Value="Type9">Type9</asp:ListItem>
                    <asp:ListItem Value="Type10">Type10</asp:ListItem>
                    <asp:ListItem Value="Type11">Type11</asp:ListItem>
                    <asp:ListItem Value="Type12">Type12</asp:ListItem>
                    <asp:ListItem Value="Type13">Type13</asp:ListItem>
                    <asp:ListItem Value="Type14">Type14</asp:ListItem>
                    <asp:ListItem Value="Type15">Type15</asp:ListItem>
                    <asp:ListItem Value="Type16">Type16</asp:ListItem>
                    <asp:ListItem Value="Type17">Type17</asp:ListItem>
                    <asp:ListItem Value="Type18">Type18</asp:ListItem>
                    <asp:ListItem Value="Type19">Type19</asp:ListItem>
                    <asp:ListItem Value="Type20">Type20</asp:ListItem>
                    <asp:ListItem Value="Type21">Type21</asp:ListItem>
                    <asp:ListItem Value="Type22">Type22</asp:ListItem>
                    <asp:ListItem Value="Type23">Type23</asp:ListItem>
                    <asp:ListItem Value="Type24">Type24</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                    <div class="clear1"></div>

                    <div class="large-2 medium-2 small-4 columns">
                        Origin
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <asp:TextBox ID="TXTOrg" runat="server" MaxLength="3" onfocus="focusObjag(this);"
                            onblur="blurObjag(this);" defvalue="ALL" autocomplete="off" value="ALL"></asp:TextBox>
                    </div>
                    <div class="large-2 medium-2 small-4 columns large-push-1 medium-push-1">
                        Destination
                    </div>
                    <div class="large-3 medium-3 small-8 columns large-push-1 medium-push-1">
                        <asp:TextBox ID="TXTDest" runat="server" MaxLength="3" onfocus="focusObjag(this);"
                            onblur="blurObjag(this);" defvalue="ALL" autocomplete="off" value="ALL"></asp:TextBox>
                    </div>

                    <div class="clear1"></div>

                    <div class="large-2 medium-2 small-4 columns">
                        Amount
                    </div>
                    <div class="large-3 medium-3 small-8 columns">
                        <asp:TextBox ID="TXTAmount" onKeyPress="return isNumberKey(event)" runat="server"></asp:TextBox>
                    </div>
                    <div class="clear1"></div>
                    <div class="large-12 medium-12 small-12">
                        <div class="large-2 medium-2 small-12 large-push-9 medium-push-9">
                            <asp:Button ID="save" runat="server" Text="Save" OnClientClick=" return validate()"
                                OnClick="SAVE_Click" />
                            <asp:Button ID="btnreset" runat="server" OnClick="btnreset_Click" Text="RESET" Visible="false" />
                        </div>
                    </div>
                    <div class="clear1"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear1"></div>

    <div class="large-10 medium-12 small-12 large-push-1">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdemp" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" GridLines="None" Font-Size="12px"  OnRowEditing="grdemp_RowEditing" OnRowUpdating="grdemp_RowUpdating"
                    OnRowCancelingEdit="grdemp_RowCancelingEdit" OnRowDeleting="grdemp_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lbtnCounter" runat="server" Text='<%#Bind("Counter")%>' CssClass="hide"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Airline">
                            <ItemTemplate>
                                <asp:Label ID="lbtnAirline" runat="server" Text='<%#Bind("Airline")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trip">
                            <ItemTemplate>
                                <asp:Label ID="lbtnTrip" runat="server" Text='<%#Bind("Trip")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AgentId">
                            <ItemTemplate>
                                <asp:Label ID="lbtnAgentId" runat="server" Text='<%#Bind("AgentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GroupType">
                            <ItemTemplate>
                                <asp:Label ID="lbtnGroupType" runat="server" Text='<%#Bind("GroupType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin">
                            <ItemTemplate>
                                <asp:Label ID="lbtnOrg" runat="server" Text='<%#Bind("Org")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Destination">
                            <ItemTemplate>
                                <asp:Label ID="lbtdest" runat="server" Text='<%#Bind("Dest")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbtnAmount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAmount" onKeyPress="return isNumberKey(event)" runat="server"
                                    Text='<%#Eval("Amount") %>' Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lbtncreatedDate" runat="server" Text='<%#Bind("createdDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="button_edit" Text="Edit" />
                                <%--<input type="button" id="btnEdit" runat="server" value="Edit" class="button_edit" />--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="button_update" Text="Update" />&nbsp;&nbsp
                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" CssClass="button_cancel" Text="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CssClass="button_delete" Text="Delete"
                                    OnClientClick="return confirm('Are you sure to delete it?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


    <script type="text/javascript">
        function isNumberKey(event) {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57 || charCode == 08 || charCode == 46) {
                return true;
            }
            else {
                alert("please enter 0 to 9 only ");
                return false;
            }
        }
        function isCharKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
                return true;
            }
            else {
                alert("please enter char type ");
                return false;
            }
        }
        function validate() {

            var Amount = document.getElementById('<%=TXTAmount.ClientID %>');

            //            if (Trip.value == "--Select--") {
            //                alert("Please select trip.");
            //                Airline.focus();
            //                return false;
            //            }
            //             if (Trip.value == "") {
            //                 alert("Enter Trip");
            //                 Trip.focus();
            //                 return false;
            //             }

            //            if (AgentId.value == "") {
            //                alert("Enter AgentId");
            //                AgentId.focus();
            //                return false;
            //            }


            //             if (GroupType.value == "") {
            //                 alert("Enter GroupType");
            //                 GroupType.focus();
            //                 return false;
            //             }
            //            if (Org.value == "") {
            //                alert("Enter Org");
            //                Org.focus();
            //                return false;
            //            }


            //            if (Dest.value == "") {
            //                alert("Enter Dest");
            //                Dest.focus();
            //                return false;
            //            }
            if (Amount.value == "") {
                alert("Enter Amount");
                Amount.focus();
                return false;


            }
            if (confirm("Are you sure??"))
                return true;
            return false;
        }
    </script>

    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AgencySearch.js") %>"></script>

    <script type="text/javascript">
        function focusObjag(obj) { if (obj.value == "ALL") obj.value = ""; }
        function blurObjag(obj) { if (obj.value == "") obj.value = "ALL"; }

        function focusObjorigin(obj) { if (obj.value == "ALL") obj.value = ""; }
        function blurObjorigin(obj) { if (obj.value == "") obj.value = "ALL"; }

        function focusObjdest(obj) { if (obj.value == "ALL") obj.value = ""; }
        function blurObjdest(obj) { if (obj.value == "") obj.value = "ALL"; }

    </script>

</asp:Content>
