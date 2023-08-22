<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="DomDiscountMaster.aspx.vb" Inherits="Reports_Admin_DomDiscountMaster" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style>
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
        function vali_number() {
            if ((event.keyCode >= 0 && event.keyCode < 46) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 128))
            { event.returnValue = false; }
            else
            { event.returnValue = true; }
        }

        function checkvld() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_grade").value == "") {
                alert("Please Select Agent Type");
                return false;
            }
            else if ((document.getElementById("ctl00_ContentPlaceHolder1_grade").value != "") && (document.getElementById("ctl00_ContentPlaceHolder1_Airlag").value == "")) {
                alert("Please Select AirLine");
                return false;
            }
            else { }
        }
    </script>
    <div class="mtop80"></div>
    <div class="large-12 medium-12 small-12">
        <div class="large-3 medium-3 small-12 columns">

            <uc1:Settings runat="server" ID="Settings" />

        </div>
        <div class="large-8 medium-8 small-12 columns heading end">
            <div class="large-12 medium-12 small-12 heading1">
                Domestic Discount Master 
            </div>
            <div class="clear1"></div>

            <asp:UpdatePanel ID="UP" runat="server">

                <ContentTemplate>

                    <div class="large-12 medium-12 small-12 ">

                        <div class="large-2 medium-2 small-4 columns">
                            Agent Type :
                        </div>
                        <div class="large-3 medium-3 small-8 columns">
                            <asp:DropDownList ID="grade" runat="server" AutoPostBack="true">
                                <%--<asp:ListItem Value="">Select Agent Type</asp:ListItem>
                                            <asp:ListItem Value="Type1">Type1</asp:ListItem>
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
                                            <asp:ListItem Value="Type15">Type15</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>

                        <div id="tr_Dis" runat="server" class="large-5 medium-5 small-12 columns">

                            <div class="large-4 medium-4 small-4 columns">
                                Airline :
                            </div>
                            <div class="large-6 medium-6 small-8 columns large-pull-2 medium-pull-2">
                                <asp:DropDownList ID="Airlag" runat="server" AutoPostBack="true">
                                    <asp:ListItem Value="">Select AirLine</asp:ListItem>
                                    <asp:ListItem Value="AI">AirIndia</asp:ListItem>
                                    <asp:ListItem Value="9W">JetAirways</asp:ListItem>
                                    <asp:ListItem Value="G8">GoAir</asp:ListItem>
                                    <asp:ListItem Value="6E">IndiGo</asp:ListItem>
                                    <asp:ListItem Value="SG">SpiceJet</asp:ListItem>
                                    <asp:ListItem Value="UK">Vistara</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="clear1"></div>
                    <div class="large-12 medium-12 small-12">
                        <div class="large-2 medium-2 small-6 columns mtop30">
                            Discount :
                        </div>

                        <div class="large-2 medium-2 small-6 columns">
                            Basic<br />
                            <asp:TextBox ID="txtDis" runat="server" CssClass="textboxflight" MaxLength="4"></asp:TextBox>
                        </div>
                        <div class="large-2 medium-2 small-6 columns">
                            YQ<br />
                            <asp:TextBox ID="txtDisYQ" runat="server" CssClass="textboxflight" MaxLength="4"></asp:TextBox>
                        </div>
                        <div class="large-2 medium-2 small-6 columns large-pull-4 medium-pull-4">
                            Basic+YQ<br />
                            <asp:TextBox ID="txtDisBYQ" runat="server" CssClass="textboxflight" MaxLength="4"></asp:TextBox>
                        </div>
                    </div>
                   <div class="clear1"></div>

                    <div class="large-14 medium-14 small-14">
                        <div class="large-2 medium-2 small-6 columns mtop30">
                            PLB  Discount :
                        </div>

                        <div class="large-2 medium-2 small-6 columns">
                            PLB Basic<br />
                            <asp:TextBox ID="txtPlbBasic" runat="server" CssClass="textboxflight" Width="50" MaxLength="4"></asp:TextBox>
                        </div>
                        
                        <div class="large-2 medium-2 small-6 columns">
                            PLB YQ<br />
                            <asp:TextBox ID="txtPlbYQ" runat="server" CssClass="textboxflight" Width="50" MaxLength="4"></asp:TextBox>
                        </div>
                        
                        <div class="large-2 medium-2 small-6 columns">
                            PLB Basic+YQ<br />
                            <asp:TextBox ID="txtPlbBasicYQ" runat="server" CssClass="textboxflight" Width="50" MaxLength="4"></asp:TextBox>
                        </div>

                        <div class="large-2 medium-2 small-6 columns large-pull-2 medium-pull-2"  >
                            Restrict RBD<br />
                            <asp:TextBox ID="txtPlbRbd" runat="server" CssClass="textboxflight" Width="160" MaxLength="50" ></asp:TextBox>
                        </div>

                    </div>

                   

                    <div class="clear1"></div>






                     <div class="clear"></div>




                    <div class="large-12 medium-12 small-12">
                        <div class="large-2 medium-2 small-4 columns mtop20">
                            Cash Back :
                        </div>
                        <div class="large-3 medium-3 small-8 columns">
                            &nbsp;<asp:TextBox ID="txtcb" runat="server" CssClass="textboxflight"></asp:TextBox>
                        </div>

                        <div class="large-4 medium-4 small-12 columns bodytext">
                            &nbsp;
                        </div>
                        <div class="large-3 medium-3 small-12 columns mtop20">
                            <asp:Button ID="UpdateAg" runat="server" Text="Update" />
                        </div>

                    </div>
                    <div class="clear1"></div>

                    <div class="large-12 medium-12 small-12">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            AllowSorting="True" CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                            <Columns>
                                <asp:BoundField DataField="Grade" HeaderText="Discount Types" />
                                <asp:BoundField DataField="Airline" HeaderText="Airline" />
                                <asp:BoundField DataField="Dis" HeaderText="Basic" />
                                <asp:BoundField DataField="Dis_YQ" HeaderText="YQ" />
                                <asp:BoundField DataField="Dis_B_YQ" HeaderText="Basic+YQ" />
                                <asp:BoundField DataField="CB" HeaderText="Cash Back" />
                                  <asp:BoundField DataField="PlbBasic" HeaderText="Plb&nbsp;Basic" />
                                      <asp:BoundField DataField="PlbYQ" HeaderText="Plb&nbsp;YQ" />
                                      <asp:BoundField DataField="PlbBYQ" HeaderText="Plb&nbsp;BYQ" />
                                      <asp:BoundField DataField="RBD" HeaderText="RBD" />
                                <%--  <asp:BoundField DataField="UpdatedBy" HeaderText="UPDATED BY" />
                                    <asp:BoundField DataField="UpdatedDate" HeaderText="UPDTED DATE" />--%>
                            </Columns>
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>

                    </div>


                    <div class="large-12 medium-12 small-12" style="display: none;">

                        <div class="bodytext large-2 medium-2 small-12 columns">
                            Distributor :
                        </div>
                        <div class="large-3 medium-3 small-12 columns">
                            <asp:DropDownList ID="dist" runat="server" CssClass="combobox" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="bodytext large-2 medium-2 small-12 columns large-push-2 medium-push-2">
                            Discount :
                        </div>
                        <div class="large-3 medium-3 small-12 columns">
                            <asp:DropDownList ID="Airlinedi" runat="server" CssClass="combobox" AutoPostBack="true">
                                <asp:ListItem>Select AirLine</asp:ListItem>
                                <asp:ListItem Value="AI">AirIndia</asp:ListItem>
                                <asp:ListItem Value="IC">Indian Airlines</asp:ListItem>
                                <asp:ListItem Value="9W">JetAirways</asp:ListItem>
                                <asp:ListItem Value="S2">JetLite</asp:ListItem>
                                <asp:ListItem Value="IT">KingFisher</asp:ListItem>
                                <asp:ListItem Value="GOAIR">GoAir</asp:ListItem>
                                <asp:ListItem Value="INDIGO">IndiGo</asp:ListItem>
                                <asp:ListItem Value="SPICEJET">SpiceJet</asp:ListItem>
                                <asp:ListItem Value="9Z">MDLR</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDdis" runat="server" Height="20" CssClass="textboxflight" MaxLength="2"></asp:TextBox>
                        </div>
                        <div class="clear1"></div>

                        <div class="bodytext large-2 medium-2 small-12 columns">
                            Tranasaction Fee :
                        </div>
                        <div class="large-3 medium-3 small-12 columns">
                            <asp:RadioButton ID="yes" Text="Y" Checked="True" GroupName="Fee" runat="server" />
                            <asp:RadioButton ID="No" Text="N" GroupName="Fee" runat="server" />
                        </div>

                        <div class="large-2 medium-2 small-12 columns large-push-2 medium-push-2">
                            &nbsp;
                        </div>
                        <div class="large-3 medium-3 small-12 columns">
                            <asp:Button ID="UpdateDi" runat="server" Text="Update" />
                        </div>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="UpdateAg" />
                </Triggers>
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
</asp:Content>
