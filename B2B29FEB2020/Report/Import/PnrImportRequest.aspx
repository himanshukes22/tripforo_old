<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="PnrImportRequest.aspx.vb" Inherits="Reports_Import_PnrImportRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../../css/main2.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>

    <style>
        table {
            background: none !important;
        }
    </style>

    
        <div class="card-main">
            <div class="card-header">
                <h3  class="main-heading">Import PNR</h3>
            </div>
        </div>
        <div class="inner-box import-pnr">
            <div class="row">
                <div class="col-md-12">
                            GDS :
                            <asp:RadioButtonList ID="Imp_Source" runat="server" RepeatDirection="Horizontal"
                                CellPadding="4" CellSpacing="4">
                                <asp:ListItem Selected="True" Value="1G">Galileo</asp:ListItem>
                            </asp:RadioButtonList>
                </div>
                
                <div class="col-md-6">
                            Choose Your Trip :
                            <asp:RadioButtonList ID="rdb_import" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="Domestic">Domestic</asp:ListItem>

                                <asp:ListItem Value="International">International</asp:ListItem>
                            </asp:RadioButtonList>
                </div>                
                <div class="col-md-6">
                            Choose Your Trip Type :
                            <asp:RadioButtonList ID="rdb_Triptype"  runat="server" RepeatDirection="Horizontal"
                                CellPadding="4" CellSpacing="4">
                                <asp:ListItem Selected="True" Value="O">OneWay</asp:ListItem>
                                <asp:ListItem Value="R">Round Trip</asp:ListItem>
                            </asp:RadioButtonList>
                 </div>
                
                <div class="col-md-6">
                    <label>PNR Number :</label>
                        <asp:TextBox ID="Txt_pnr" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
                   <div class="col-md-12">                       
                        <div class="btn-export">
                         <asp:Button ID="Btn_find" runat="server" CssClass="btn cmn-btn" Text="Import" OnClientClick="return validateimport();"/>
                      </div>
                  </div>
                    <div class="col-md-12 addional-notes">    
                        <p><b>Notes</b></p>
                        <p>1:- Galileo Pnr Import :(Please queue the PNR first as per below command)</p>
                        <p>2:- Domestic Pnr Queue Command : QEB/PCC00V/50</p>
                        <p>3:- International Pnr Queue Command : QEB/PCC00V/50</p>                                               
                     </div>
            </div>

          </div>
    <div class="container">
        <div class="col-md-9">
            <div>
                <div>
                    <div>
                      <%--  <div style="color: #004b91; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;">
                            GDS :
                        </div>
                        <div style="font-size: 12px; font-weight: bold; color: #000000; font-family: arial, Helvetica, sans-serif;">

                            <asp:RadioButtonList ID="Imp_Source" runat="server" RepeatDirection="Horizontal"
                                CellPadding="4" CellSpacing="4">
                                <asp:ListItem Selected="True" Value="1G">Galileo</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                    </div>
                    <div>
                       <%-- <div style="color: #004b91; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;">
                            Choose Your Trip :
                        </div>
                        <div style="font-size: 12px; font-weight: bold; color: #000000; font-family: arial, Helvetica, sans-serif;">
                            <asp:RadioButtonList ID="rdb_import" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="Domestic">Domestic</asp:ListItem>

                                <asp:ListItem Value="International">International</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                    </div>
                    <div>
                     <%--   <div style="color: #004b91; font-size: 12px; font-weight: bold;">
                            Choose Your Trip Type :
                        </div>
                        <div style="font-size: 12px; font-weight: bold; color: #000000; font-family: arial, Helvetica, sans-serif;">
                            <asp:RadioButtonList ID="rdb_Triptype"  runat="server" RepeatDirection="Horizontal"
                                CellPadding="4" CellSpacing="4">
                                <asp:ListItem Selected="True" Value="O">OneWay</asp:ListItem>
                                <asp:ListItem Value="R">Round Trip</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                    </div>
                    <div id="TBL_Projects" runat="server">
                        <div style="color: #004b91; font-size: 12px; font-weight: bold;">
                            Choose Project :
                        </div>
                        <div align="left" style="font-size: 14px; font-weight: bold; color: #000000; font-family: arial, Helvetica, sans-serif;"
                            width="120px">
                            <asp:DropDownList ID="DropDownListProject" runat="server" CssClass="textboxflight"
                                Height="20px" Width="120px">
                            </asp:DropDownList>
                        </div>
                        <div style="padding-left: 5px; color: #004b91; font-size: 13px; font-weight: bold;">
                            Booked By :
                        </div>
                        <div align="left" style="font-size: 14px; font-weight: bold; color: #000000; font-family: arial, Helvetica, sans-serif;">
                            <asp:DropDownList ID="DropDownListBookedBy" runat="server" CssClass="textboxflight"
                                Height="20px" Width="120px">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                      <%--  <div align="left" style="color: #004b91; font-size: 13px; font-weight: normal;">
                            <strong>Pnr Number : </strong>&nbsp;
                        </div>--%>
                        <div class="row">
                           <div class="col-md-3">
                           <%--    <label>PNR Number :</label>
                               <asp:TextBox ID="Txt_pnr" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                           </div>
                                                    
                         <%--   <div class="col-md-2">
                                <asp:Button ID="Btn_find" runat="server" CssClass="btn btn-danger" Text="Import" OnClientClick="return validateimport();" style="position: relative;top: 25px;"/>
                            </div>--%>
                            


                        </div>
                    </div>
                    <div style="display: none">
                        <div align="right">
                            <strong>ES Office ID :</strong> &nbsp;
                        </div>
                        <div>
                            <asp:TextBox ID="txtOfficeid" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <td align="center"></td>


            <div>
                <div>

<%--                    <div>
                        <div height="25px" style="font-size: 14px; color: #004b91;">
                            Notes :
                        </div>
                    </div>

                    <div>
                        <div>
                            Galileo Pnr Import :(Please queue the PNR first as per below command)
                        </div>
                    </div>
                    <div>
                        <td>2:- Domestic Pnr Queue Command : QEB/PCC00V/50
                        </td>
                    </div>
                    <div>
                        <div>
                            3:- International Pnr Queue Command : QEB/PCC00V/50
                        </div>
                    </div>--%>
                    <div>
                        <div>
                        </div>
                    </div>
                    <div>
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>





        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="98%">
                    <tr>
                        <td align="center" colspan="2" height="5px">
                            <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP1">
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <div>
        </div>
    </div>





    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <table class="w100">

                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" style="font-weight: bold; font-size: 12px">
                                    <asp:Label ID="lbl_Errormsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <asp:GridView ID="GridViewshow" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" Font-Size="12px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Orgin">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_orgin" runat="server" Text='<%#Eval("loc1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_destination" runat="server" Text='<%#Eval("loc2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departuredate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_depdate" runat="server" Text='<%#Eval("Depdate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departuretime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_depttime" runat="server" Text='<%#Eval("Deptime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrivaldate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_arrdate" runat="server" Text='<%#Eval("ArrDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrivaltime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_arrtime" runat="server" Text='<%#Eval("Arrtime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Airline">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Airline" runat="server" Text='<%#Eval("Airline") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FlightNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flno" runat="server" Text='<%#Eval("FlightNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RBD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_rbd" runat="server" Text='<%#Eval("RBD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AdtFareBasic">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_rbdadt" runat="server" Text='<%#Eval("AdtFareBasis")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ChdFareBasic">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_rbdchd" runat="server" Text='<%#Eval("ChdFareBasis")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="InfFareBasic">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_rbdinf" runat="server" Text='<%#Eval("InfFareBasis")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="12px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="OrderId">
                                                <ItemTemplate>
                                                    <asp:Label ID="OrderId_lb" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="Title_lb" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FName">
                                                <ItemTemplate>
                                                    <asp:Label ID="FName_lb" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MName">
                                                <ItemTemplate>
                                                    <asp:Label ID="MName_lb" runat="server" Text='<%#Eval("MName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LName">
                                                <ItemTemplate>
                                                    <asp:Label ID="LName_lb" runat="server" Text='<%#Eval("LName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaxType">
                                                <ItemTemplate>
                                                    <asp:Label ID="PaxType_lb" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB">
                                                <ItemTemplate>
                                                    <asp:Label ID="DOB_lb" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="Gender_lb" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" Font-Size="12px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:BoundField DataField="OrderId" HeaderText="OrderId"></asp:BoundField>
                                            <asp:BoundField DataField="BaseFare" HeaderText="BaseFare"></asp:BoundField>
                                            <asp:BoundField DataField="YQ" HeaderText="YQ"></asp:BoundField>
                                            <asp:BoundField DataField="YR" HeaderText="YR"></asp:BoundField>
                                            <asp:BoundField DataField="WO" HeaderText="WO"></asp:BoundField>
                                            <asp:BoundField DataField="GST" HeaderText="GST"></asp:BoundField>
                                            <asp:BoundField DataField="OT" HeaderText="OT"></asp:BoundField>
                                            <asp:BoundField DataField="Qtax" HeaderText="Qtax"></asp:BoundField>
                                            <asp:BoundField DataField="TotalTax" HeaderText="TotalTax"></asp:BoundField>
                                            <asp:BoundField DataField="TotalFare" HeaderText="TotalFare"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" Font-Size="12px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">

                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:BoundField DataField="OrderId" HeaderText="OrderId"></asp:BoundField>
                                            <asp:BoundField DataField="VC" HeaderText="VC"></asp:BoundField>
                                            <asp:BoundField DataField="GdsPnr" HeaderText="GdsPnr"></asp:BoundField>
                                            <asp:BoundField DataField="AirlinePnr" HeaderText="AirlinePnr"></asp:BoundField>
                                            <asp:BoundField DataField="Trip" HeaderText="Trip"></asp:BoundField>
                                            <asp:BoundField DataField="Adult" HeaderText="Adult"></asp:BoundField>

                                            <asp:BoundField DataField="Child" HeaderText="Child"></asp:BoundField>
                                            <asp:BoundField DataField="Infant" HeaderText="Infant"></asp:BoundField>
                                            <asp:BoundField DataField="PgTitle" HeaderText="Title"></asp:BoundField>
                                            <asp:BoundField DataField="PgFName" HeaderText="FName"></asp:BoundField>
                                            <asp:BoundField DataField="PgLName" HeaderText="LName"></asp:BoundField>
                                            <asp:BoundField DataField="PgMobile" HeaderText="Mobile"></asp:BoundField>
                                            <asp:BoundField DataField="PgEmail" HeaderText="PgEmail"></asp:BoundField>


                                        </Columns>

                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="display: none">
                                    <div style="display: none" id="paxdiv" runat="server">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btn_update" runat="server" Text="Update" CssClass="buttonfltbks" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function validateimport() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_Txt_pnr").value == "") {

                alert('Please enter PNR NO.');
                document.getElementById("ctl00_ContentPlaceHolder1_Txt_pnr").focus();
                return false;

            }
                <%--if (document.getElementById("ctl00_lblCamt").innerHTML.replace("INR", "") < 5000) {

                    alert('your minimum account balance should be greater than or equal to INR. 5000 to send import request.');
                    return false;


                }--%>

            if ($("#ctl00_ContentPlaceHolder1_DropDownListProject option:selected").val() == "Select") {
                alert("Please Select Project Id");
                $("#ctl00_ContentPlaceHolder1_DropDownListProject").focus();
                return false;
            }


            if ($("#ctl00_ContentPlaceHolder1_DropDownListBookedBy option:selected").val() == "Select") {
                alert("Please Select Booked By");
                $("#ctl00_ContentPlaceHolder1_DropDownListBookedBy").focus();
                return false;
            }


        }
    </script>
</asp:Content>
